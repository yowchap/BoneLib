using BoneLib.RandomShit;
using SLZ.Data;
using SLZ.Marrow.Warehouse;
using SLZ.Player;
using System.Collections.Generic;
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

            itemSpawning.CreateFunctionElement("Spawn Utility Gun", Color.white, () => SpawnUtilityGun());
            itemSpawning.CreateFunctionElement("Spawn Nimbus Gun", Color.white, () => SpawnNimbusGun());
            itemSpawning.CreateFunctionElement("Spawn Random Gun", Color.white, () => SpawnRandomGun());

            funstuff.CreateFunctionElement("Spawn Ad", Color.white, () => PopupBoxManager.CreateNewPopupBox());
            funstuff.CreateFunctionElement("Spawn Shibe Ad", Color.white, () => PopupBoxManager.CreateNewShibePopup());
            funstuff.CreateFunctionElement("Spawn Bird Ad", Color.white, () => PopupBoxManager.CreateNewBirdPopup());
            funstuff.CreateFunctionElement("Spawn Cat Ad", Color.white, () => PopupBoxManager.CreateNewCatPopup());
            
        }

        internal static void SpawnUtilityGun()
        {
            Transform head = Player.playerHead.transform;
            string barcode = "c1534c5a-5747-42a2-bd08-ab3b47616467";
            HelperMethods.SpawnCrate(barcode, head.position + head.forward, default, Vector3.one, false, null);
        }

        internal static void SpawnNimbusGun()
        {
            Transform head = Player.playerHead.transform;
            string barcode = "c1534c5a-6b38-438a-a324-d7e147616467";
            HelperMethods.SpawnCrate(barcode, head.position + head.forward, default, Vector3.one, false, null);
        }
        internal static void SpawnRandomGun()
        {
            Transform head = Player.playerHead.transform;

            List<Barcode> barcodes = new List<Barcode>();
            foreach (var val in AssetWarehouse.Instance.InventoryRegistry.Values)
            {
                var crateRef = new SpawnableCrateReference(val.Barcode);
                var crate = crateRef.Crate;
                if (crate != null && crate.Tags.Contains("Gun"))
                    barcodes.Add(val.Barcode);
            }

            int index = Random.RandomRangeInt(0, barcodes.Count);
            Barcode barcode = barcodes[index];

            HelperMethods.SpawnCrate(barcode, head.position + head.forward, default, Vector3.one, false, null);
        }
    }
}
