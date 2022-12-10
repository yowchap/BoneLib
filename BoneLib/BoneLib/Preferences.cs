using MelonLoader;

namespace BoneLib
{
    internal static class Preferences
    {
        private static MelonPreferences_Category category = MelonPreferences.CreateCategory("BoneLib");

        public static ModPref<LoggingMode> loggingMode;
        public static ModPref<bool> skipIntro;

        public static void Setup()
        {
            skipIntro = new ModPref<bool>(category, "SkipIntro", false);
            loggingMode = new ModPref<LoggingMode>(category, "LoggingMode", LoggingMode.NORMAL);

            category.SaveToFile(false);
            ModConsole.Msg("Finished preferences setup", LoggingMode.DEBUG);
        }
    }
}
