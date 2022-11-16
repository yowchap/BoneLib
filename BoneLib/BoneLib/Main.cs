using BoneLib.MonoBehaviours;
using BoneLib.RandomShit;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace BoneLib
{
    public static class BuildInfo
    {
        public const string Name = "BoneLib"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "Gnonme"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.5.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class Main : MelonMod
    {
        public override void OnInitializeMelon()
        {
            ModConsole.Setup(LoggerInstance);
            Preferences.Setup();

            Hooking.SetHarmony(HarmonyInstance);
            Hooking.InitHooks();

            Hooking.OnLevelLoading += (info) => MelonLogger.Msg($"OnLevelLoading: {info.title} | {info.barcode}");
            Hooking.OnLevelUnloaded += () => MelonLogger.Msg("OnLevelUnloaded");
            Hooking.OnLevelInitialized += OnLevelInitialized;

            ClassInjector.RegisterTypeInIl2Cpp<PopupBox>();

            //PopupBoxManager.StartCoroutines();

            ModConsole.Msg("BoneLib loaded");
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            MelonLogger.Msg("OnSceneWasLoaded: " + sceneName);
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            MelonLogger.Msg("OnSceneWasInitialized: " + sceneName);
        }

        /// <summary>
        /// Dynamic MelonLoader Callback. Do not call!
        /// </summary>
        private void BONELAB_OnLoadingScreen()
        {
            Hooking.OnBONELABLevelLoading();
        }

        private void OnLevelInitialized(LevelInfo info)
        {
            MelonLogger.Msg($"OnLevelInitialized: {info.title} | {info.barcode}");

            PopupBoxManager.CreateBaseAd();

            if(info.title == "00 - Main Menu" || info.title == "15 - Void G114")
            {
                SkipIntro();
            }
        }

        private void SkipIntro()
        {
            if (!Preferences.skipIntro)
            {
                return;
            }

            GameObject uiRoot = GameObject.Find("CANVAS_UX");

            GameObject slzWobble = uiRoot.transform.Find("SLZ_ROOT").gameObject;
            GameObject marrowWobble = uiRoot.transform.Find("CREDITS_ROOT").gameObject;
            GameObject boneBeaker = uiRoot.transform.Find("BONELAB_ANIM").gameObject;

            GameObject.Destroy(slzWobble);
            GameObject.Destroy(marrowWobble);

            boneBeaker.transform.localPosition = Vector3.up * 0.375f;
            boneBeaker.transform.localScale = Vector3.one * 0.5f;

            GameObject requiredUI = uiRoot.transform.Find("REQUIRED").gameObject;
            requiredUI.SetActiveRecursively(true);

            GameObject menuUI = uiRoot.transform.Find("MENU").gameObject;
            menuUI.gameObject.SetActive(true);

            menuUI.transform.Find("group_Enter").gameObject.SetActive(false);
            menuUI.transform.Find("group_Options").gameObject.SetActive(false);
            menuUI.transform.Find("group_Mods").gameObject.SetActive(false);
            menuUI.transform.Find("group_Info").gameObject.SetActive(false);
            menuUI.transform.Find("group_BETA").gameObject.SetActive(false);
        }
    }
}