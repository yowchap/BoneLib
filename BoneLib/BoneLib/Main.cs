using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;
using BoneLib.MonoBehaviours;
using BoneLib.Notifications;
using BoneLib.RandomShit;
using MelonLoader;
using Il2CppSLZ.Bonelab;
using UnityEngine;
using Il2CppInterop.Runtime.Injection;

namespace BoneLib
{
    public static class BuildInfo
    {
        public const string Name = "BoneLib"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "Gnonme"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "2.4.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    internal class Main : MelonMod
    {
        public override void OnInitializeMelon()
        {
            ModConsole.Setup(LoggerInstance);
            Preferences.Setup();

            Hooking.SetHarmony(HarmonyInstance);
            Hooking.InitHooks();

            MenuManager.SetRoot(new MenuCategory("BoneMenu", Color.white));

            DefaultMenu.CreateDefaultElements();

            NotifAssets.SetupBundles();

            DataManager.Bundles.Init();
            DataManager.UI.AddComponents();

            Hooking.OnLevelInitialized += OnLevelInitialized;

            ClassInjector.RegisterTypeInIl2Cpp<PopupBox>();

            //PopupBoxManager.StartCoroutines();

            ModConsole.Msg("BoneLib loaded");
        }

        public override void OnUpdate()
        {
            Notifier.OnUpdate();
        }

        private void OnLevelInitialized(LevelInfo info)
        {
            PopupBoxManager.CreateBaseAd();
            Audio.GetAudioMixers();

            if (info.title == "00 - Main Menu" || info.title == "15 - Void G114")
                SkipIntro();

            DataManager.UI.InitializeReferences();
            new GameObject("[BoneMenu] - UI Manager").AddComponent<BoneMenu.UI.UIManager>();
        }

        private void SkipIntro()
        {
            if (!Preferences.skipIntro)
                return;

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

            GameControl_MenuVoidG114 controller = GameObject.FindObjectOfType<GameControl_MenuVoidG114>();

            if (controller != null)
            {
                controller.holdTime = 0;
                controller.holdTime_SLZ = 0;
                controller.holdTime_Credits = 0;
                controller.holdTime_GameTitle = 0;
                controller.timerHold = 0;
                controller.holdTime_Rest = 0;
                controller.canClick = true;
                controller.fadeVolume.gameObject.SetActive(false); // Set black fog volume to inactive to prevent it never fading out
            }
        }
    }
}
