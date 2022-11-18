using BoneLib.MonoBehaviours;
using BoneLib.RandomShit;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;

using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;

namespace BoneLib
{
    public static class BuildInfo
    {
        public const string Name = "BoneLib"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "Gnonme"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.4.0"; // Version of the Mod.  (MUST BE SET)
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

            MenuManager.SetRoot(new MenuCategory("BoneMenu", Color.white));

            SetupBoneMenu();
            TestCategory();

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
            new GameObject("[BoneMenu] - UI Manager").AddComponent<BoneMenu.UI.UIManager>();
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

        private void SetupBoneMenu()
        {
            var mainCategory = MenuManager.CreateCategory("BoneLib", Color.white);
            mainCategory.CreateFunctionElement("Spawn Ad", Color.yellow, () => PopupBoxManager.CreateBaseAd());
            mainCategory.CreateBoolElement("Test Bool", "#FF33E6", false);
        }

        private void TestCategory()
        {
            var testCategory = MenuManager.CreateCategory("Test Category", "#7012a3");
            testCategory.CreateCategory("Sub Category", Color.white);
            testCategory.CreateBoolElement("Bool Element", Color.white, false);
            testCategory.CreateEnumElement<ElementType>("Enum Element", Color.white);
            testCategory.CreateFloatElement("Float Element", Color.white, 0f, 1f, 0f, 10f);
            testCategory.CreateIntElement("Int Element", Color.white, 0, 1, 0, 10);
            testCategory.CreateFunctionElement("Function Element (with confirmer)", Color.white, null, "You will delete everything if you confirm!");
        }
    }
}