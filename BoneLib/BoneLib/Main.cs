using MelonLoader;

namespace BoneLib
{
    public static class BuildInfo
    {
        public const string Name = "BoneLib"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "Gnonme"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
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

            ModConsole.Msg("BoneLib loaded");
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            Player.FindObjectReferences();
        }

    }
}