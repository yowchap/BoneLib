using MelonLoader;
using System;
using System.IO;

namespace BoneLibUpdater
{
    public static class BuildInfo
    {
        public const string Name = "BoneLibUpdater"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "Gnonme"; // Author of the Mod.  (Set as null if none)
        public const string Company = null; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.1.1"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = null; // Download Link for the Mod.  (Set as null if none)
    }

    public class Main : MelonPlugin
    {
        private MelonPreferences_Category prefsCategory = MelonPreferences.CreateCategory("BoneLibUpdater");
        private MelonPreferences_Entry<bool> offlineModePref;
        private bool isOffline => offlineModePref.Value;

        public static readonly string boneLibAssemblyPath = Path.Combine(MelonHandler.ModsDirectory, "BoneLib.dll");
        public static readonly string boneLibUpdaterAssemblyPath = Path.Combine(MelonHandler.PluginsDirectory, "BoneLibUpdater.dll");

        public static MelonLogger.Instance Logger { get; private set; }


        public override void OnPreInitialization()
        {
            Logger = LoggerInstance;

            offlineModePref = prefsCategory.CreateEntry("OfflineMode", false);
            prefsCategory.SaveToFile(false);

            LoggerInstance.Msg(isOffline ? ConsoleColor.Yellow : ConsoleColor.Green, isOffline ? "BoneLib is in OFFLINE mode" : "BoneLib is in ONLINE mode");

            if (isOffline)
            {
                if (!File.Exists(boneLibAssemblyPath))
                {
                    LoggerInstance.Warning("BoneLib.dll was not found in the Mods folder");
                    LoggerInstance.Warning("Download it from github or switch to ONLINE mode");
                    LoggerInstance.Warning("https://github.com/yowchap/BoneLib/releases");
                }
            }
            else
            {
                Updater.UpdateMod();
            }
        }

        public override void OnApplicationQuit()
        {
            Updater.UpdatePlugin();
        }
    }
}
