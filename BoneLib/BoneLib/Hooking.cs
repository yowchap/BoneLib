using HarmonyLib;
using MelonLoader;
using SLZ.AI;
using SLZ.Interaction;
using SLZ.Props.Weapons;
using SLZ.Rig;
using SLZ.VRMK;
using SLZ.SceneStreaming;
using SLZ.Marrow.Utilities;
using SLZ.Marrow.Warehouse;
using PuppetMasta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using SLZ.Marrow.SceneStreaming;

namespace BoneLib
{
    public static class Hooking
    {
        private static HarmonyLib.Harmony baseHarmony;

        private static Queue<DelayedHookData> delayedHooks = new Queue<DelayedHookData>();

        public static event Action OnMarrowGameStarted;

        public static event Action<MarrowSceneInfo> OnMarrowSceneInitialized;
        public static event Action<MarrowSceneInfo> OnMarrowSceneLoaded;
        public static event Action OnMarrowSceneUnloaded;

        public static event Action<Avatar> OnSwitchAvatarPrefix;
        public static event Action<Avatar> OnSwitchAvatarPostfix;

        public static event Action<Gun> OnPreFireGun;
        public static event Action<Gun> OnPostFireGun;

        public static event Action<GameObject, Hand> OnGrabObject;
        public static event Action<Hand> OnReleaseObject;

        public static event Action<Grip, Hand> OnGripAttached;
        public static event Action<Grip, Hand> OnGripDetached;

        public static event Action<float> OnPlayerDamageRecieved;
        public static event Action<bool> OnPlayerDeathImminent;
        public static event Action OnPlayerDeath;

        public static event Action<AIBrain> OnNPCBrainDie;
        public static event Action<AIBrain> OnNPCBrainResurrected;

        public static event Action<BehaviourBaseNav> OnNPCKillStart;
        public static event Action<BehaviourBaseNav> OnNPCKillEnd;

        public static event Action OnPlayerReferencesFound;

        internal static void SetHarmony(HarmonyLib.Harmony harmony) => Hooking.baseHarmony = harmony;
        internal static void InitHooks()
        {
            MarrowGame.RegisterOnReadyAction(OnMarrowGameStarted);

            CreateHook(typeof(SceneStreamer).GetMethod("Load", new Type[] {typeof(LevelCrateReference), typeof(LevelCrateReference)}), typeof(Hooking).GetMethod(nameof(OnSceneMarrowInitialized), AccessTools.all));

            CreateHook(typeof(RigManager).GetMethod("SwitchAvatar", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnAvatarSwitchPrefix), AccessTools.all), true);
            CreateHook(typeof(RigManager).GetMethod("SwitchAvatar", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnAvatarSwitchPostfix), AccessTools.all));

            CreateHook(typeof(Gun).GetMethod("OnFire", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnFirePrefix), AccessTools.all), true);
            CreateHook(typeof(Gun).GetMethod("OnFire", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnFirePostfix), AccessTools.all));

            CreateHook(typeof(Hand).GetMethod("AttachObject", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnAttachObjectPostfix), AccessTools.all));
            CreateHook(typeof(Hand).GetMethod("DetachObject", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnDetachObjectPostfix), AccessTools.all));

            CreateHook(typeof(Grip).GetMethod("OnAttachedToHand", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnGripAttachedPostfix), AccessTools.all));
            CreateHook(typeof(Grip).GetMethod("OnDetachedFromHand", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnGripDetachedPostfix), AccessTools.all));

            CreateHook(typeof(RigManager).GetMethod("Awake", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnRigManagerAwake), AccessTools.all));
            CreateHook(typeof(RigManager).GetMethod("OnDestroy", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnRigManagerDestroy), AccessTools.all));

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
        /// Hooks the method and debug logs some info.
        /// </summary>
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

        private static void OnSceneMarrowInitialized(LevelCrateReference level, LevelCrateReference loadLevel)
        {
            MarrowSceneInfo sceneInfo = new MarrowSceneInfo()
            {
                LevelTitle = level.Crate.Title,
                Barcode = level.Barcode.ID,
                MarrowScene = level.Crate.MainAsset.Cast<MarrowScene>()
            };

            OnMarrowSceneInitialized?.Invoke(sceneInfo);
        }

        private static void OnSceneMarrowLoaded()
        {
            var level = SceneStreamer.Session.Level;

            MarrowSceneInfo info = new MarrowSceneInfo()
            {
                LevelTitle = level.Title,
                MarrowScene = level.MainScene,
                Barcode = level.Barcode.ID
            };

            OnMarrowSceneLoaded?.Invoke(info);
        }

        private static void OnSceneMarrowUnloaded()
        {
            OnMarrowSceneUnloaded?.Invoke();
        }

        private static void OnAvatarSwitchPrefix(Avatar newAvatar) => OnSwitchAvatarPrefix?.Invoke(newAvatar);
        private static void OnAvatarSwitchPostfix(Avatar newAvatar) => OnSwitchAvatarPostfix?.Invoke(newAvatar);

        private static void OnFirePrefix(Gun __instance) => OnPreFireGun?.Invoke(__instance);
        private static void OnFirePostfix(Gun __instance) => OnPostFireGun?.Invoke(__instance);

        private static void OnAttachObjectPostfix(GameObject objectToAttach, Hand __instance) => OnGrabObject?.Invoke(objectToAttach, __instance);
        private static void OnDetachObjectPostfix(Hand __instance) => OnReleaseObject?.Invoke(__instance);

        private static void OnGripAttachedPostfix(Grip __instance, Hand hand) => OnGripAttached?.Invoke(__instance, hand);
        private static void OnGripDetachedPostfix(Grip __instance, Hand hand) => OnGripDetached?.Invoke(__instance, hand);
        private static void OnRigManagerAwake(RigManager __instance)
        {
            OnSceneMarrowLoaded();

            if (Player.FindObjectReferences(__instance))
                OnPlayerReferencesFound?.Invoke();
        }

        private static void OnRigManagerDestroy(RigManager __instance)
        {
            OnSceneMarrowUnloaded();
        }

        private static void OnBrainNPCDie(AIBrain __instance) => OnNPCBrainDie?.Invoke(__instance);
        private static void OnBrainNPCResurrected(AIBrain __instance) => OnNPCBrainResurrected?.Invoke(__instance);

        private static void OnKillNPCStart(BehaviourBaseNav __instance) => OnNPCKillStart?.Invoke(__instance);
        private static void OnKillNPCEnd(BehaviourBaseNav __instance) => OnNPCKillEnd?.Invoke(__instance);

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
