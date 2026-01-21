using HarmonyLib;
using MelonLoader;

using Il2CppSLZ.Marrow.SceneStreaming;
using Il2CppSLZ.Marrow.Utilities;
using Il2CppSLZ.Marrow.Warehouse;
using Il2CppSLZ.VRMK;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using UnityEngine;

using Il2CppSLZ.Marrow.AI;
using Il2CppSLZ.Marrow.PuppetMasta;
using Il2CppSLZ.Marrow;
using Il2CppSLZ.Bonelab;

namespace BoneLib
{
    public static class Hooking
    {
        private static HarmonyLib.Harmony baseHarmony;

        private static Queue<DelayedHookData> delayedHooks = new Queue<DelayedHookData>();

        // Marrow
        public static event Action OnMarrowGameStarted;
        public static event Action OnWarehouseReady;

        /// <summary>
        /// Called at the start of a loading screen.
        /// </summary>
        public static event Action<LevelInfo> OnLevelLoading;
        /// <summary>
        /// Called when the current Level is fully initialized.
        /// </summary>
        public static event Action<LevelInfo> OnLevelLoaded;
        /// <summary>
        /// Called when the current Level unloads.
        /// </summary>
        public static event Action OnLevelUnloaded;
        /// <summary>
        /// Called when the UIRig has been created.
        /// </summary>
        public static event Action OnUIRigCreated;

        public static event Action<Avatar> OnSwitchAvatarPrefix;
        public static event Action<Avatar> OnSwitchAvatarPostfix;

        public static event Action<RigManager, float> OnPlayerDamageReceived;
        public static event Action<RigManager> OnPlayerDeathImminent;
        public static event Action<RigManager> OnPlayerDeath;
        public static event Action<RigManager> OnPlayerResurrected;

        // Interaction
        public static event Action<GameObject, Hand> OnGrabObject;
        public static event Action<Hand> OnReleaseObject;

        public static event Action<Grip, Hand> OnGripAttached;
        public static event Action<Grip, Hand> OnGripDetached;

        public static event Action<Gun> OnPreFireGun;
        public static event Action<Gun> OnPostFireGun;

        // NPCs
        public static event Action<AIBrain> OnNPCBrainDie;
        public static event Action<AIBrain> OnNPCBrainResurrected;

        public static event Action<BehaviourBaseNav> OnNPCKillStart;
        public static event Action<BehaviourBaseNav> OnNPCKillEnd;

        private static bool currentLevelUnloaded = true;

        internal static void SetHarmony(HarmonyLib.Harmony harmony) => Hooking.baseHarmony = harmony;

        internal static void InitHooks()
        {
            MarrowGame.RegisterOnReadyAction(new Action(() => SafeActions.InvokeActionSafe(OnMarrowGameStarted)));
            AssetWarehouse.OnReady(new Action(() => SafeActions.InvokeActionSafe(OnWarehouseReady)));

            CreateHook(typeof(RigManager).GetMethod(nameof(RigManager.SwitchAvatar), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnAvatarSwitchPrefix), AccessTools.all), true);
            CreateHook(typeof(RigManager).GetMethod(nameof(RigManager.SwitchAvatar), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnAvatarSwitchPostfix), AccessTools.all));

            CreateHook(typeof(Gun).GetMethod(nameof(Gun.OnFire), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnFirePrefix), AccessTools.all), true);
            CreateHook(typeof(Gun).GetMethod(nameof(Gun.OnFire), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnFirePostfix), AccessTools.all));

            CreateHook(typeof(Hand).GetMethod(nameof(Hand.AttachObject), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnAttachObjectPostfix), AccessTools.all));
            CreateHook(typeof(Hand).GetMethod(nameof(Hand.DetachObject), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnDetachObjectPostfix), AccessTools.all));

            CreateHook(typeof(Grip).GetMethod(nameof(Grip.OnAttachedToHand), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnGripAttachedPostfix), AccessTools.all));
            CreateHook(typeof(Grip).GetMethod(nameof(Grip.OnDetachedFromHand), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnGripDetachedPostfix), AccessTools.all));

            CreateHook(typeof(RigManager).GetMethod(nameof(RigManager.OnDestroy), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnRigManagerDestroyed), AccessTools.all));

            CreateHook(typeof(UIRig).GetMethod(nameof(UIRig.Awake), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnUIRigAwake), AccessTools.all));

            CreateHook(typeof(AIBrain).GetMethod(nameof(AIBrain.OnDeath), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnBrainNPCDie), AccessTools.all));
            CreateHook(typeof(AIBrain).GetMethod(nameof(AIBrain.OnResurrection), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnBrainNPCResurrected), AccessTools.all));

            CreateHook(typeof(BehaviourBaseNav).GetMethod(nameof(BehaviourBaseNav.KillStart), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnKillNPCStart), AccessTools.all));
            CreateHook(typeof(BehaviourBaseNav).GetMethod(nameof(BehaviourBaseNav.KillEnd), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnKillNPCEnd), AccessTools.all));

            CreateHook(typeof(PlayerMarker).GetMethod(nameof(PlayerMarker.OnPlayerSpawned), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnPlayerSpawned), AccessTools.all));

            CreateHook(typeof(Player_Health).GetMethod(nameof(Player_Health.TAKEDAMAGE), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnPlayerDamageReceivedPostfix), AccessTools.all));
            CreateHook(typeof(Player_Health).GetMethod(nameof(Player_Health.ApplyKillDamage), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnPlayerDeathImminentPostfix), AccessTools.all));
            CreateHook(typeof(Player_Health).GetMethod(nameof(Player_Health.Death), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnPlayerDeathPostfix), AccessTools.all));
            CreateHook(typeof(Player_Health).GetMethod(nameof(Player_Health.LifeSavingDamgeDealt), AccessTools.all), typeof(Hooking).GetMethod(nameof(OnPlayerResurrectedPostfix), AccessTools.all));

            while (delayedHooks.Count > 0)
            {
                DelayedHookData data = delayedHooks.Dequeue();
                CreateHook(data.original, data.hook, data.isPrefix);
            }
        }

        /// <summary>
        /// Hooks the <paramref name="original"/> method and logs some debug info.
        /// </summary>
        /// <param name="original">Method to be patched</param>
        /// <param name="hook">Method to be applied as a patch</param>
        /// <param name="isPrefix">
        /// Controls whether <paramref name="hook"/> is applied as a Prefix or Postfix patch
        /// </param>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void CreateHook(MethodInfo original, MethodInfo hook, bool isPrefix = false)
        {
            if (baseHarmony == null)
            {
                delayedHooks.Enqueue(new DelayedHookData(original, hook, isPrefix));
                return;
            }

            Assembly callingAssembly = Assembly.GetCallingAssembly();
            MelonMod callingMod = MelonMod.RegisteredMelons.FirstOrDefault(x => x.MelonAssembly.Assembly.FullName == callingAssembly.FullName);
            HarmonyLib.Harmony harmony = callingMod != null ? callingMod.HarmonyInstance : baseHarmony;

            HarmonyMethod prefix = isPrefix ? new HarmonyMethod(hook) : null;
            HarmonyMethod postfix = isPrefix ? null : new HarmonyMethod(hook);
            harmony.Patch(original, prefix: prefix, postfix: postfix);

            ModConsole.Msg($"New {(isPrefix ? "PREFIX" : "POSTFIX")} on {original.DeclaringType.Name}.{original.Name} to {hook.DeclaringType.Name}.{hook.Name}", LoggingMode.DEBUG);
        }

        // private static void OnRigManagerAwake(RigManager __instance)
        // {
        //     if (Player.HandsExist)
        //         return;

        //     if (Player.FindObjectReferences(__instance))
        //     {
        //         // @Todo(Parzival): Some levels aren't done loading when RigManager.Awake is called!
        //         // Ideally this should be invoked right before the loading screen dissapears, but this is
        //         // the closest I can get it for now.
        //         // You could use Player_Health.MakeVignette, that's almost always called when the RM is fully ready and the level's loaded.
        //         SafeActions.InvokeActionSafe(OnLevelLoaded, new LevelInfo(SceneStreamer.Session.Level));
        //     }
        // }

        private static void OnPlayerSpawned(GameObject go)
        {
            if (Player.HandsExist)
            {
                return;
            }

            if (Player.FindObjectReferences())
            {
                SafeActions.InvokeActionSafe(OnLevelLoaded, new LevelInfo(SceneStreamer.Session.Level));
            }
        }

        private static void OnRigManagerDestroyed()
        {
            currentLevelUnloaded = true;
            SafeActions.InvokeActionSafe(OnLevelUnloaded);
        }

        private static void OnUIRigAwake()
        {
            if (Player.FindUIRigReferences())
            {
                SafeActions.InvokeActionSafe(OnUIRigCreated);
            }
        }

        internal static void OnBONELABLevelLoading()
        {
            if (currentLevelUnloaded)
            {
                SafeActions.InvokeActionSafe(OnLevelLoading, new LevelInfo(SceneStreamer.Session.Level));
                currentLevelUnloaded = false;
            }
        }

        private static void OnAvatarSwitchPrefix(Avatar newAvatar) => SafeActions.InvokeActionSafe(OnSwitchAvatarPrefix, newAvatar);
        private static void OnAvatarSwitchPostfix(Avatar newAvatar) => SafeActions.InvokeActionSafe(OnSwitchAvatarPostfix, newAvatar);

        private static void OnFirePrefix(Gun __instance) => SafeActions.InvokeActionSafe(OnPreFireGun, __instance);
        private static void OnFirePostfix(Gun __instance) => SafeActions.InvokeActionSafe(OnPostFireGun, __instance);

        private static void OnAttachObjectPostfix(GameObject objectToAttach, Hand __instance) => SafeActions.InvokeActionSafe(OnGrabObject, objectToAttach, __instance);
        private static void OnDetachObjectPostfix(Hand __instance) => SafeActions.InvokeActionSafe(OnReleaseObject, __instance);

        private static void OnGripAttachedPostfix(Grip __instance, Hand hand) => SafeActions.InvokeActionSafe(OnGripAttached, __instance, hand);
        private static void OnGripDetachedPostfix(Grip __instance, Hand hand) => SafeActions.InvokeActionSafe(OnGripDetached, __instance, hand);

        private static void OnBrainNPCDie(AIBrain __instance) => SafeActions.InvokeActionSafe(OnNPCBrainDie, __instance);
        private static void OnBrainNPCResurrected(AIBrain __instance) => SafeActions.InvokeActionSafe(OnNPCBrainResurrected, __instance);

        private static void OnKillNPCStart(BehaviourBaseNav __instance) => SafeActions.InvokeActionSafe(OnNPCKillStart, __instance);
        private static void OnKillNPCEnd(BehaviourBaseNav __instance) => SafeActions.InvokeActionSafe(OnNPCKillEnd, __instance);

        private static void OnPlayerDamageReceivedPostfix(Player_Health __instance, float damage) => SafeActions.InvokeActionSafe(OnPlayerDamageReceived, __instance._rigManager, damage);
        private static void OnPlayerDeathPostfix(Player_Health __instance) => SafeActions.InvokeActionSafe(OnPlayerDeath, __instance._rigManager);
        private static void OnPlayerDeathImminentPostfix(Player_Health __instance) => SafeActions.InvokeActionSafe(OnPlayerDeathImminent, __instance._rigManager);
        private static void OnPlayerResurrectedPostfix(Player_Health __instance) => SafeActions.InvokeActionSafe(OnPlayerResurrected, __instance._rigManager);

        private struct DelayedHookData
        {
            public MethodInfo original;
            public MethodInfo hook;
            public bool isPrefix;

            public DelayedHookData(MethodInfo original, MethodInfo hook, bool isPrefix)
            {
                this.original = original;
                this.hook = hook;
                this.isPrefix = isPrefix;
            }
        }
    }
}
