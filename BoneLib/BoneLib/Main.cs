using BoneLib.BoneMenu;
using BoneLib.MonoBehaviours;
using BoneLib.Notifications;
using BoneLib.RandomShit;
using Il2CppInterop.Runtime.Injection;
using MelonLoader;

namespace BoneLib
{
    public static class BuildInfo
    {
        public const string Name = "BoneLib"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "The BONELAB Modding Community"; // Author of the Mod.  (Set as null if none)
        public const string Company = "The BONELAB Modding Community"; // Company that made the Mod.  (Set as null if none)
        public const string Version = "3.1.4"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = "https://thunderstore.io/c/bonelab/p/bonelib/BoneLib"; // Download Link for the Mod.  (Set as null if none)
    }

    internal class Main : MelonMod
    {
        public override void OnInitializeMelon()
        {
            ModConsole.Setup(LoggerInstance);
            Preferences.Setup();

            Hooking.SetHarmony(HarmonyInstance);
            Hooking.InitHooks();

            MenuBootstrap.InitializeBundles();

            Menu.Initialize();

            DefaultMenu.CreateDefaultElements();

            NotifAssets.SetupBundles();

            Hooking.OnLevelLoaded += OnLevelLoaded;
            Hooking.OnUIRigCreated += OnUIRigCreated;

            ClassInjector.RegisterTypeInIl2Cpp<PopupBox>();

            //PopupBoxManager.StartCoroutines();

            ModConsole.Msg("BoneLib loaded");
        }

        public override void OnUpdate()
        {
            if (Player.ControllerRig != null && !Player.ControllerRig.quickmenuEnabled)
            {
                Player.ControllerRig.quickmenuEnabled = true;
            }

            Notifier.OnUpdate();
        }

        private void OnLevelLoaded(LevelInfo info)
        {
            PopupBoxManager.CreateBaseAd();
        }

        private void OnUIRigCreated()
        {
            MenuBootstrap.InitializeReferences();
        }
    }
}
