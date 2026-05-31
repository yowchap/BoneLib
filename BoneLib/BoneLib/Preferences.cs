using MelonLoader;

namespace BoneLib
{
    internal static class Preferences
    {
        private static MelonPreferences_Category category = MelonPreferences.CreateCategory("BoneLib");

        public static ModPref<LoggingMode> loggingMode;
        public static ModPref<bool> skipIntro;
        public static ModPref<bool> infiniteAmmo;

        public static void Setup()
        {
            skipIntro = new ModPref<bool>(category, "SkipIntro", false);
            loggingMode = new ModPref<LoggingMode>(category, "LoggingMode", LoggingMode.NORMAL);
            infiniteAmmo = new ModPref<bool>(category, "InfiniteAmmo", false);
            Save();
            ModConsole.Msg("Finished preferences setup", LoggingMode.DEBUG);
        }

        public static void Save()
        {
            category.SaveToFile(false);
        }
    }
}
