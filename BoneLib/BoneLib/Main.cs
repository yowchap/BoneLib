using MelonLoader;

namespace BoneLib
{
    public static class BuildInfo
    {
        public const string Name = "BoneLib"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "Gnonme"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "0.2.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            ModConsole.Setup(LoggerInstance);
            Preferences.Setup();

            ModConsole.Msg("BoneLib loaded");
        }
    }
}
