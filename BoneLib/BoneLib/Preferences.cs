using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;

namespace BoneLib
{
    internal static class Preferences
    {
        private static MelonPreferences_Category category = MelonPreferences.GetCategory("BoneLib");

        public static ModPref<LoggingMode> loggingMode;


        public static void Setup()
        {
            loggingMode = new ModPref<LoggingMode>(category, "LoggingMode", LoggingMode.NORMAL);
        }
    }
}
