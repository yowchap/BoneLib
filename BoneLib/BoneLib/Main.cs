using BoneLib.MonoBehaviours;
using BoneLib.RandomShit;
using MelonLoader;
using UnhollowerRuntimeLib;

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
            Hooking.OnMarrowSceneLoaded += (data) => OnMarrowSceneLoaded();

            ClassInjector.RegisterTypeInIl2Cpp<PopupBox>();

            //PopupBoxManager.StartCoroutines();

            ModConsole.Msg("BoneLib loaded");
        }

        private void OnMarrowSceneLoaded()
        {
            DataManager.UI.InitializeReferences();
            new GameObject("[BoneMenu] - UI Manager").AddComponent<BoneMenu.UI.UIManager>();
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