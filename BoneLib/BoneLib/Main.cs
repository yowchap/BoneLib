using BoneLib.MonoBehaviours;
using BoneLib.RandomShit;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;

using BoneLib.BoneMenu;

using UnityEngine;

namespace BoneLib
{
    public static class BuildInfo
    {
        public const string Name = "BoneLib"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "Gnonme"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.3.1"; // Version of the Mod.  (MUST BE SET)
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

            MenuManager.SetRoot(MenuManager.CreateCategory("BoneMenu", Color.white));

            TestBonemenuStuff();

            DataManager.Bundles.Init();
            DataManager.UI.AddComponents();

            Hooking.OnPlayerReferencesFound += OnPlayerReferencesFound;
            Hooking.OnMarrowSceneLoaded += OnMarrowSceneLoaded;

            ClassInjector.RegisterTypeInIl2Cpp<PopupBox>();

            //PopupBoxManager.StartCoroutines();

            ModConsole.Msg("BoneLib loaded");
        }

        private void OnMarrowSceneLoaded(MarrowSceneInfo info)
        {
            if (info.LevelTitle == "00 - Main Menu" || info.LevelTitle == "15 - Void G114")
            {
                SkipIntro();
            }

            DataManager.UI.InitializeReferences();
            new BoneMenu.UI.UIManager();
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

        private void OnPlayerReferencesFound()
        {
            PopupBoxManager.CreateBaseAd();
        }

        private void TestBonemenuStuff()
        {
            var testCat = MenuManager.RootCategory.CreateCategory("Test", Color.white);
            testCat.CreateFunctionElement("Test Function", Color.green, () => LoggerInstance.Msg("I have been ran. Run."));
        }
    }
}