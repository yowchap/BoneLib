using HarmonyLib;
using MelonLoader;
using PuppetMasta;
using SLZ.AI;
using SLZ.Interaction;
using SLZ.Marrow.SceneStreaming;
using SLZ.Marrow.Utilities;
using SLZ.Props.Weapons;
using SLZ.Rig;
using SLZ.UI;
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

        private static readonly Queue<DelayedHookData> delayedHooks = new();

        // Marrow
        public static event Action OnMarrowGameStarted;

        /// <summary>
        /// Called at the start of a loading screen.
        /// </summary>
        public static event Action<LevelInfo> OnLevelLoading;
        /// <summary>
        /// Called slightly after OnLevelIntialized (when Player_Health.MakeVignette is called) so all RigManager references should be fully ready.
        /// </summary>
        public static event Action<RigManager> OnPlayerFullyCreated;
        /// <summary>
        /// Called when the current Level is fully initialized.
        /// </summary>
        public static event Action<LevelInfo> OnLevelInitialized;
        /// <summary>
        /// Called when the current Level unloads.
        /// </summary>
        public static event Action OnLevelUnloaded;

        public static event Action<Avatar> OnSwitchAvatarPrefix;
        public static event Action<Avatar> OnSwitchAvatarPostfix;

        public static event Action<PopUpMenuView> OnPopUpMenuOpenPreFix;
        public static event Action<PopUpMenuView> OnPopUpMenuOpenPostFix;
        public static event Action<PopUpMenuView> OnPopUpMenuClosedPreFix;
        public static event Action<PopUpMenuView> OnPopUpMenuClosedPostFix;

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

        private static bool hasPlayerLoaded = false;

        internal static void SetHarmony(HarmonyLib.Harmony harmony) => Hooking.baseHarmony = harmony;
        internal static void InitHooks()
        {
            MarrowGame.RegisterOnReadyAction(new Action(() => SafeActions.InvokeActionSafe(OnMarrowGameStarted)));

            CreateHook(typeof(RigManager).GetMethod("SwitchAvatar", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnAvatarSwitchPrefix), AccessTools.all), true);
            CreateHook(typeof(RigManager).GetMethod("SwitchAvatar", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnAvatarSwitchPostfix), AccessTools.all));

            CreateHook(typeof(PopUpMenuView).GetMethod("Activate", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnPopUpMenuOpenedPrefix), AccessTools.all), true);
            CreateHook(typeof(PopUpMenuView).GetMethod("Activate", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnPopUpMenuOpenedPostfix), AccessTools.all));

            CreateHook(typeof(PopUpMenuView).GetMethod("Deactivate", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnPopUpMenuClosedPrefix), AccessTools.all), true);
            CreateHook(typeof(PopUpMenuView).GetMethod("Deactivate", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnPopUpMenuClosedPostfix), AccessTools.all));

            CreateHook(typeof(Gun).GetMethod("OnFire", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnFirePrefix), AccessTools.all), true);
            CreateHook(typeof(Gun).GetMethod("OnFire", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnFirePostfix), AccessTools.all));

            CreateHook(typeof(Hand).GetMethod("AttachObject", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnAttachObjectPostfix), AccessTools.all));
            CreateHook(typeof(Hand).GetMethod("DetachObject", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnDetachObjectPostfix), AccessTools.all));

            CreateHook(typeof(Grip).GetMethod("OnAttachedToHand", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnGripAttachedPostfix), AccessTools.all));
            CreateHook(typeof(Grip).GetMethod("OnDetachedFromHand", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnGripDetachedPostfix), AccessTools.all));

            CreateHook(typeof(Player_Health).GetMethod("MakeVignette", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnMakeVignette), AccessTools.all));
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

        private static void OnMakeVignette(Player_Health __instance)
        {
            hasPlayerLoaded = true;

            SafeActions.InvokeActionSafe(OnPlayerFullyCreated, Player.rigManager);
        }

        private static void OnRigManagerAwake(RigManager __instance)
        {
            if (Player.handsExist)
                return;

            if (Player.FindObjectReferences(__instance))
            {
                // @Todo(Parzival): Some levels aren't done loading when RigManager.Awake is called!
                // Ideally this should be invoked right before the loading screen dissapears, but this is
                // the closest I can get it for now.
                // You could use Player_Health.MakeVignette, that's almost always called when the RM is fully ready and the level's loaded.
                SafeActions.InvokeActionSafe(OnLevelInitialized, new LevelInfo(SceneStreamer.Session.Level));
            }
        }

        private static void OnRigManagerDestroyed()
        {
            hasPlayerLoaded = false;

            SafeActions.InvokeActionSafe(OnLevelUnloaded);
        }

        private static void OnPopUpMenuOpenedPrefix(PopUpMenuView __instance)
        {
            if (hasPlayerLoaded)
                SafeActions.InvokeActionSafe(OnPopUpMenuOpenPreFix, __instance);
        }
        private static void OnPopUpMenuOpenedPostfix(PopUpMenuView __instance)
        {
            if (hasPlayerLoaded)
                SafeActions.InvokeActionSafe(OnPopUpMenuOpenPostFix, __instance);
        }

        private static void OnAvatarSwitchPrefix(Avatar newAvatar) => SafeActions.InvokeActionSafe(OnSwitchAvatarPrefix, newAvatar);
        private static void OnAvatarSwitchPostfix(Avatar newAvatar) => SafeActions.InvokeActionSafe(OnSwitchAvatarPostfix, newAvatar);

        private static void OnPopUpMenuClosedPrefix(PopUpMenuView __instance)
        {
            if (hasPlayerLoaded)
            SafeActions.InvokeActionSafe(OnPopUpMenuClosedPreFix, __instance);
        }
        private static void OnPopUpMenuClosedPostfix(PopUpMenuView __instance)
        {
            if (hasPlayerLoaded)
                SafeActions.InvokeActionSafe(OnPopUpMenuClosedPostFix, __instance);
        }

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
