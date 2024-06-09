using System.Collections.Generic;
using BoneLib.Notifications;
using BoneLib.RandomShit;
using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Data;
using Il2CppSLZ.Marrow.Data;
using Il2CppSLZ.Marrow.Pool;
using Il2CppSLZ.Marrow.SceneStreaming;
using Il2CppSLZ.Marrow.Warehouse;
using Il2CppSLZ.Player;
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

        public static void CreateDefaultElements()
        {
            Elements.MenuCategory mainCategory = MenuManager.CreateCategory("BoneLib", Color.white);

            Elements.SubPanelElement ammo = mainCategory.CreateSubPanel("Ammo Settings", Color.yellow);
            Elements.SubPanelElement itemSpawning = mainCategory.CreateSubPanel("Item Spawning", Color.white);
            Elements.SubPanelElement funstuff = mainCategory.CreateSubPanel("Fun Stuff", "#e600ff");

            ammo.CreateFunctionElement("Add Light Ammo", Color.white, () => AmmoInventory.AddCartridge(LightAmmo, lightAmmoValue));
            ammo.CreateFunctionElement("Add Medium Ammo", Color.white, () => AmmoInventory.AddCartridge(MediumAmmo, mediumAmmoValue));
            ammo.CreateFunctionElement("Add Heavy Ammo", Color.white, () => AmmoInventory.AddCartridge(HeavyAmmo, heavyAmmoValue));

            ammo.CreateIntElement("Light Ammo", "#ffe11c", lightAmmoValue, 100, 0, int.MaxValue, (value) => lightAmmoValue = value);
            ammo.CreateIntElement("Medium Ammo", "#ff9d1c", mediumAmmoValue, 100, 0, int.MaxValue, (value) => mediumAmmoValue = value);
            ammo.CreateIntElement("Heavy Ammo", "#ff2f1c", heavyAmmoValue, 100, 0, int.MaxValue, (value) => heavyAmmoValue = value);

            itemSpawning.CreateFunctionElement("Spawn Utility Gun", Color.white, SpawnUtilityGun);
            itemSpawning.CreateFunctionElement("Spawn Nimbus Gun", Color.white, SpawnNimbusGun);
            itemSpawning.CreateFunctionElement("Spawn Random Gun", Color.white, SpawnRandomGun);
            itemSpawning.CreateFunctionElement("Spawn Random Melee", Color.white, SpawnRandomMelee);
            itemSpawning.CreateFunctionElement("Spawn Random NPC", Color.white, SpawnRandomNPC);
            itemSpawning.CreateFunctionElement("Load Random Level", Color.white, LoadRandomLevel);

            funstuff.CreateFunctionElement("Spawn Ad", Color.white, () => PopupBoxManager.CreateNewPopupBox());
            funstuff.CreateFunctionElement("Spawn Shibe Ad", Color.white, () => PopupBoxManager.CreateNewShibePopup());
            funstuff.CreateFunctionElement("Spawn Bird Ad", Color.white, () => PopupBoxManager.CreateNewBirdPopup());
            funstuff.CreateFunctionElement("Spawn Cat Ad", Color.white, () => PopupBoxManager.CreateNewCatPopup());
            funstuff.CreateFunctionElement("Notification Test", Color.white, () =>
            {
                var notif = new Notification()
                {
                    Title = "Hello!",
                    Message = "Fuck you!",
                    Type = NotificationType.Error,
                    ShowTitleOnPopup = true,
                    PopupLength = 5f
                };
                Notifier.Send(notif);
            });
        }

        internal static void SpawnUtilityGun()
        {
            Transform head = Player.playerHead.transform;
            HelperMethods.SpawnCrate(CommonBarcodes.Misc.SpawnGun, head.position + head.forward, default, Vector3.one, false, null);
        }

        internal static void SpawnNimbusGun()
        {
            Transform head = Player.playerHead.transform;
            HelperMethods.SpawnCrate(CommonBarcodes.Misc.NimbusGun, head.position + head.forward, default, Vector3.one, false, null);
        }
        
        internal static void SpawnRandomGun()
        {
            Transform head = Player.playerHead.transform;

            int index = Random.RandomRangeInt(0, CommonBarcodes.Guns.All.Count);
            string barcode = CommonBarcodes.Guns.All[index];

            HelperMethods.SpawnCrate(barcode, head.position + head.forward, default, Vector3.one, false, null);
        }

        internal static void SpawnRandomMelee()
        {
            Transform head = Player.playerHead.transform;
            
            int index = Random.RandomRangeInt(0, CommonBarcodes.Melee.All.Count);
            string barcode = CommonBarcodes.Melee.All[index];
            
            HelperMethods.SpawnCrate(barcode, head.position + head.forward, default, Vector3.one, false, null);
        }
        
        internal static void SpawnRandomNPC()
        {
            Transform player = Player.rigManager.physicsRig.artOutput.transform;
            int index = Random.RandomRangeInt(0, CommonBarcodes.NPCs.All.Count);
            string barcode = CommonBarcodes.NPCs.All[index];
            
            HelperMethods.SpawnCrate(barcode, player.position + player.forward, default, Vector3.one, false, null);
        }

        internal static void LoadRandomLevel()
        {
            int index = Random.RandomRangeInt(0, CommonBarcodes.Maps.All.Count);
            string barcode = CommonBarcodes.Maps.All[index];
            
            SceneStreamer.Load(barcode, CommonBarcodes.Maps.LoadDefault);
        }
    }
}
