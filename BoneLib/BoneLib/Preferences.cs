using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Data;
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
            infiniteAmmo.entry.OnEntryValueChanged.Subscribe((_, value) =>
            {
                // Gives Ammo once the preference is toggled on
                if (value)
                {
                    GiveAmmo();
                }
            });
            if (infiniteAmmo.entry.Value)
            {
                GiveAmmo();
            }
            Save();
            ModConsole.Msg("Finished preferences setup", LoggingMode.DEBUG);
        }

        // Added from hahoos comment
        internal static void GiveAmmo()
        {
            if (infiniteAmmo.entry.Value)
            {
                // Gets the ammo inventory
                AmmoInventory ammoInventory = AmmoInventory.Instance;
                // Creates an array of groups and carts.
                AmmoGroup[] ammoGroups = { ammoInventory.lightAmmoGroup, ammoInventory.mediumAmmoGroup, ammoInventory.heavyAmmoGroup };
                string[] ammoCartridges = { "light", "medium", "heavy" };
                for (int i = 0; i < 3; i++)
                {
                    // Adds cart. if there is no ammo
                    if (ammoInventory.GetCartridgeCount(ammoCartridges[i]) < 1) ammoInventory.AddCartridge(ammoGroups[i], short.MaxValue);
                }
            }
        }

        public static void Save()
        {
            category.SaveToFile(false);
        }
    }
}
