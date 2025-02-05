using BoneLib.Notifications;
using BoneLib.RandomShit;

using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.SceneStreaming;
using Il2CppSLZ.Marrow.Warehouse;

using UnityEngine;

namespace BoneLib.BoneMenu
{
    internal static class DefaultMenu
    {
        private static AmmoInventory AmmoInventory => AmmoInventory.Instance;

        private static AmmoGroup LightAmmo => AmmoInventory.lightAmmoGroup;

        private static AmmoGroup MediumAmmo => AmmoInventory.mediumAmmoGroup;

        private static AmmoGroup HeavyAmmo => AmmoInventory.heavyAmmoGroup;

        private static int lightAmmoValue = 100;
        private static int mediumAmmoValue = 100;
        private static int heavyAmmoValue = 100;
        private static string textValue = "";

        public static void CreateDefaultElements()
        {
            Page mainPage = Page.Root.CreatePage("BoneLib", Color.white);

            Page ammoPage = mainPage.CreatePage("Ammo Settings", Color.yellow);
            Page itemSpawningPage = mainPage.CreatePage("Item Spawning", Color.white);
            Page funStuffPage = mainPage.CreatePage("Fun Stuff", Color.white);

            ammoPage.CreateFunction("Add Light Ammo", Color.white, () => AmmoInventory.AddCartridge(LightAmmo, lightAmmoValue));
            ammoPage.CreateFunction("Add Medium Ammo", Color.white, () => AmmoInventory.AddCartridge(MediumAmmo, mediumAmmoValue));
            ammoPage.CreateFunction("Add Heavy Ammo", Color.white, () => AmmoInventory.AddCartridge(HeavyAmmo, heavyAmmoValue));

            ammoPage.CreateInt("Light Ammo", Color.white, lightAmmoValue, 100, 0, int.MaxValue, (value) => lightAmmoValue = value);
            ammoPage.CreateInt("Medium Ammo", Color.white, mediumAmmoValue, 100, 0, int.MaxValue, (value) => mediumAmmoValue = value);
            ammoPage.CreateInt("Heavy Ammo", Color.white, heavyAmmoValue, 100, 0, int.MaxValue, (value) => heavyAmmoValue = value);

            itemSpawningPage.CreateFunction("Spawn Utility Gun", Color.white, SpawnUtilityGun);
            itemSpawningPage.CreateFunction("Spawn Nimbus Gun", Color.white, SpawnNimbusGun);
            itemSpawningPage.CreateFunction("Spawn Random Gun", Color.white, SpawnRandomGun);
            itemSpawningPage.CreateFunction("Spawn Random Melee", Color.white, SpawnRandomMelee);
            itemSpawningPage.CreateFunction("Spawn Random NPC", Color.white, SpawnRandomNPC);
            itemSpawningPage.CreateFunction("Load Random Level", Color.white, LoadRandomLevel);
            itemSpawningPage.CreateFunction("Change Into Random Avatar", Color.white, ChangeIntoRandomAvatar);

            funStuffPage.CreateFunction("Spawn Ad", Color.white, () => PopupBoxManager.CreateNewPopupBox());
            funStuffPage.CreateFunction("Spawn Shibe Ad", Color.white, () => PopupBoxManager.CreateNewShibePopup());
            funStuffPage.CreateFunction("Spawn Bird Ad", Color.white, () => PopupBoxManager.CreateNewBirdPopup());
            funStuffPage.CreateFunction("Spawn Cat Ad", Color.white, () => PopupBoxManager.CreateNewCatPopup());
            funStuffPage.CreateString("Notification Text", Color.white, "None", (input) => textValue = input);
            funStuffPage.CreateFunction("Notification Test", Color.white, () =>
            {
                var notif = new Notification()
                {
                    Title = "Hello!",
                    Message = textValue,
                    Type = NotificationType.Error,
                    ShowTitleOnPopup = true,
                    PopupLength = 5f
                };
                Notifier.Send(notif);
            });

            funStuffPage.CreateFunction("Make Dialog", Color.white, () =>
            {
                Menu.DisplayDialog(
                    "Test",
                    "This is a test message. Don't worry about it.",
                    null,
                    () =>
                    {
                        ModConsole.Msg("Hello from the Dialog confirm option!");
                    });
            });
        }

        internal static void SpawnUtilityGun()
        {
            Transform head = Player.Head;
            HelperMethods.SpawnCrate(CommonBarcodes.Misc.SpawnGun, head.position + head.forward, default, Vector3.one, false, null);
        }

        internal static void SpawnNimbusGun()
        {
            Transform head = Player.Head;
            HelperMethods.SpawnCrate(CommonBarcodes.Misc.NimbusGun, head.position + head.forward, default, Vector3.one, false, null);
        }

        internal static void SpawnRandomGun()
        {
            Transform head = Player.Head;

            if (!AssetWarehouse.ready)
                return;

            var guns = AssetWarehouse.Instance.GetCrates<SpawnableCrate>();
            Barcode Gun = null;
            while (Gun == null)
            {
                var selected = guns[Random.RandomRangeInt(0, guns.Count)];
                if (selected?.Tags.Contains("Gun") == true)
                    Gun = selected.Barcode;
            }

            HelperMethods.SpawnCrate(Gun.ID, head.position + head.forward, default, Vector3.one, false, null);
        }

        internal static void SpawnRandomMelee()
        {
            Transform head = Player.Head;

            if (!AssetWarehouse.ready)
                return;

            var melees = AssetWarehouse.Instance.GetCrates<SpawnableCrate>();
            Barcode Melee = null;
            while (Melee == null)
            {
                var selected = melees[Random.RandomRangeInt(0, melees.Count)];
                if (selected?.Tags.Contains("Melee") == true)
                    Melee = selected.Barcode;
            }

            HelperMethods.SpawnCrate(Melee.ID, head.position + head.forward, default, Vector3.one, false, null);
        }

        internal static void SpawnRandomNPC()
        {
            Transform player = Player.PhysicsRig.artOutput.transform;

            if (!AssetWarehouse.ready)
                return;

            var NPCs = AssetWarehouse.Instance.GetCrates<SpawnableCrate>();
            Barcode NPC = null;
            while (NPC == null)
            {
                var selected = NPCs[Random.RandomRangeInt(0, NPCs.Count)];
                if (selected?.Tags.Contains("NPC") == true)
                    NPC = selected.Barcode;
            }

            HelperMethods.SpawnCrate(NPC.ID, player.position + player.forward, default, Vector3.one, false, null);
        }

        internal static void LoadRandomLevel()
        {
            if (!AssetWarehouse.ready)
                return;

            var levels = AssetWarehouse.Instance.GetCrates<LevelCrate>();
            var level = levels[Random.RandomRangeInt(0, levels.Count)];

            SceneStreamer.Load(level.Barcode, new Barcode(CommonBarcodes.Maps.LoadDefault));
        }

        internal static void ChangeIntoRandomAvatar()
        {
            if (!AssetWarehouse.ready)
                return;

            var avatars = AssetWarehouse.Instance.GetCrates<AvatarCrate>();
            var avatar = avatars[Random.RandomRangeInt(0, avatars.Count)];

            Player.RigManager.SwapAvatarCrate(avatar.Barcode, true);
        }
    }
}