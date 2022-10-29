using HarmonyLib;
using MelonLoader;
using PuppetMasta;
using SLZ.AI;
using SLZ.Interaction;
using SLZ.Marrow.SceneStreaming;
using SLZ.Marrow.Utilities;
using SLZ.Marrow.Warehouse;
using SLZ.Props.Weapons;
using SLZ.Rig;
using SLZ.VRMK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace BoneLib
{
    public static class Hooking
    {
        private static HarmonyLib.Harmony baseHarmony;

        private static Queue<DelayedHookData> delayedHooks = new Queue<DelayedHookData>();

        // Marrow
        public static event Action OnMarrowGameStarted;

        public static event Action<MarrowSceneInfo> OnMarrowSceneInitialized;
        public static event Action<MarrowSceneInfo> OnMarrowSceneLoaded;
        public static event Action<MarrowSceneInfo, MarrowSceneInfo> OnMarrowSceneUnloaded;

        // Player
        public static event Action OnPlayerReferencesFound;

        public static event Action<Avatar> OnSwitchAvatarPrefix;
        public static event Action<Avatar> OnSwitchAvatarPostfix;

        public static event Action<float> OnPlayerDamageRecieved;
        public static event Action<bool> OnPlayerDeathImminent;
        public static event Action OnPlayerDeath;

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


        internal static MarrowSceneInfo lastScene;
        internal static MarrowSceneInfo currentScene;
        internal static MarrowSceneInfo nextScene;


        internal static void SetHarmony(HarmonyLib.Harmony harmony) => Hooking.baseHarmony = harmony;
        internal static void InitHooks()
        {
            MarrowGame.RegisterOnReadyAction(new Action(() => SafeActions.InvokeActionSafe(OnMarrowGameStarted)));

            CreateHook(typeof(SceneStreamer).GetMethod("Load", new Type[] { typeof(LevelCrateReference), typeof(LevelCrateReference) }), typeof(Hooking).GetMethod(nameof(OnSceneMarrowInitialized), AccessTools.all));

            CreateHook(typeof(RigManager).GetMethod("SwitchAvatar", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnAvatarSwitchPrefix), AccessTools.all), true);
            CreateHook(typeof(RigManager).GetMethod("SwitchAvatar", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnAvatarSwitchPostfix), AccessTools.all));

            CreateHook(typeof(Gun).GetMethod("OnFire", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnFirePrefix), AccessTools.all), true);
            CreateHook(typeof(Gun).GetMethod("OnFire", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnFirePostfix), AccessTools.all));

            CreateHook(typeof(Hand).GetMethod("AttachObject", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnAttachObjectPostfix), AccessTools.all));
            CreateHook(typeof(Hand).GetMethod("DetachObject", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnDetachObjectPostfix), AccessTools.all));

            CreateHook(typeof(Grip).GetMethod("OnAttachedToHand", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnGripAttachedPostfix), AccessTools.all));
            CreateHook(typeof(Grip).GetMethod("OnDetachedFromHand", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnGripDetachedPostfix), AccessTools.all));

            CreateHook(typeof(RigManager).GetMethod("Awake", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnRigManagerAwake), AccessTools.all));
            CreateHook(typeof(RigManager).GetMethod("OnDestroy", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnRigManagerDestroyed), AccessTools.all));

            CreateHook(typeof(AIBrain).GetMethod("OnDeath", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnBrainNPCDie), AccessTools.all));
            CreateHook(typeof(AIBrain).GetMethod("OnResurrection", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnBrainNPCResurrected), AccessTools.all));

            CreateHook(typeof(BehaviourBaseNav).GetMethod("KillStart", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnKillNPCStart), AccessTools.all));
            CreateHook(typeof(BehaviourBaseNav).GetMethod("KillEnd", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnKillNPCEnd), AccessTools.all));

            Player_Health.add_OnPlayerDamageReceived(OnPlayerDamageRecieved);
            Player_Health.add_OnDeathImminent(OnPlayerDeathImminent);
            Player_Health.add_OnPlayerDeath(OnPlayerDeath);

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

        private static void OnRigManagerAwake(RigManager __instance)
        {
            OnSceneMarrowLoaded();

            if (Player.FindObjectReferences(__instance))
                SafeActions.InvokeActionSafe(OnPlayerReferencesFound);
        }

        private static void OnSceneMarrowInitialized(LevelCrateReference level, LevelCrateReference loadLevel)
        {
            MarrowSceneInfo info = new MarrowSceneInfo()
            {
                LevelTitle = level.Crate.Title,
                Barcode = level.Barcode.ID,
                MarrowScene = level.Crate.MainAsset.Cast<MarrowScene>()
            };

            nextScene = info;
            SafeActions.InvokeActionSafe(OnMarrowSceneInitialized, info);
        }

        private static void OnSceneMarrowLoaded()
        {
            LevelCrate level = SceneStreamer.Session.Level;

            MarrowSceneInfo info = new MarrowSceneInfo()
            {
                LevelTitle = level.Title,
                MarrowScene = level.MainScene,
                Barcode = level.Barcode.ID
            };

            currentScene = info;
            lastScene = currentScene;

            SafeActions.InvokeActionSafe(OnMarrowSceneLoaded, currentScene);
        }

        private static void OnRigManagerDestroyed(RigManager __instance) => SafeActions.InvokeActionSafe(OnMarrowSceneUnloaded, lastScene, nextScene);

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


        struct DelayedHookData
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
