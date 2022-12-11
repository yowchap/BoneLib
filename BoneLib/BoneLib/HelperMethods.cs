using MelonLoader;
using System.Text.RegularExpressions;

namespace BoneLib
{
    public static class HelperMethods
    {
        /// <summary>
        /// Removes things like [2] and (Clone)
        /// </summary>
        public static string GetCleanObjectName(string name)
        {
            Regex regex = new Regex(@"\[\d+\]|\(\d+\)"); // Stuff like (1) or [24]
            name = regex.Replace(name, "");
            name = name.Replace("(Clone)", "");
            return name.Trim();
        }

        /// <summary>
        /// Checks if the user is running MelonLoader on Android
        /// </summary>
        public static bool IsAndroid() => MelonUtils.CurrentPlatform == (MelonPlatformAttribute.CompatiblePlatforms)3;
    }
}
