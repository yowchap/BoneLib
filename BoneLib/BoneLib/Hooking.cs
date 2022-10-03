using System;
using SLZ.VRMK;
using System.Reflection;
using MelonLoader;
using HarmonyLib;
using SLZ.Interaction;
using SLZ.Props.Weapons;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SLZ.Rig;

namespace BoneLib
{
    public static class Hooking
    {
        private static HarmonyLib.Harmony baseHarmony;

        private static Queue<DelayedHookData> delayedHooks = new Queue<DelayedHookData>();

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

        internal static void SetHarmony(HarmonyLib.Harmony harmony) => Hooking.baseHarmony = harmony;
        internal static void InitHooks()
        {
            CreateHook(typeof(RigManager).GetMethod("SwitchAvatar", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnAvatarSwitchPrefix), AccessTools.all), true);
            CreateHook(typeof(RigManager).GetMethod("SwitchAvatar", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnAvatarSwitchPostfix), AccessTools.all));

            CreateHook(typeof(Gun).GetMethod("OnFire", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnFirePrefix), AccessTools.all), true);
            CreateHook(typeof(Gun).GetMethod("OnFire", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnFirePostfix), AccessTools.all));

            CreateHook(typeof(Hand).GetMethod("AttachObject", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnAttachObjectPostfix), AccessTools.all));
            CreateHook(typeof(Hand).GetMethod("DetachObject", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnDetachObjectPostfix), AccessTools.all));

            CreateHook(typeof(Grip).GetMethod("OnAttachedToHand", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnGripAttachedPostfix), AccessTools.all));
            CreateHook(typeof(Grip).GetMethod("OnDetachedFromHand", AccessTools.all), typeof(Hooking).GetMethod(nameof(OnGripDetachedPostfix), AccessTools.all));

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

        private static void OnAvatarSwitchPrefix(Avatar newAvatar) => OnSwitchAvatarPrefix?.Invoke(newAvatar);
        private static void OnAvatarSwitchPostfix(Avatar newAvatar) => OnSwitchAvatarPostfix?.Invoke(newAvatar);

        private static void OnFirePrefix(Gun __instance) => OnPreFireGun?.Invoke(__instance);
        private static void OnFirePostfix(Gun __instance) => OnPostFireGun?.Invoke(__instance);

        private static void OnAttachObjectPostfix(GameObject objectToAttach, Hand __instance) => OnGrabObject?.Invoke(objectToAttach, __instance);
        private static void OnDetachObjectPostfix(Hand __instance) => OnReleaseObject?.Invoke(__instance);

        private static void OnGripAttachedPostfix(Grip __instance, Hand hand) => OnGripAttached?.Invoke(__instance, hand);
        private static void OnGripDetachedPostfix(Grip __instance, Hand hand) => OnGripDetached?.Invoke(__instance, hand);

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
