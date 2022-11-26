using UnityEngine;

using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;

using BoneLib.RandomShit;
using BoneLib.Nullables;

using SLZ.Player;
using SLZ.Data;

using SLZ.Marrow;
using SLZ.Marrow.Data;
using SLZ.Marrow.Pool;
using SLZ.Marrow.Warehouse;

namespace BoneLib.BoneMenu
{
    internal static class DefaultMenu
    {
        static AmmoInventory ammoInventory => AmmoInventory.Instance;

        static AmmoGroup lightAmmo
        {
            get
            {
                return ammoInventory?.lightAmmoGroup;
            }
        }

        static AmmoGroup mediumAmmo
        {
            get
            {
                return ammoInventory?.mediumAmmoGroup;
            }
        }

        static AmmoGroup heavyAmmo
        {
            get
            {
                return ammoInventory?.heavyAmmoGroup;
            }
        }

        static int lightAmmoValue = 100;
        static int mediumAmmoValue = 100;
        static int heavyAmmoValue = 100;

        public static void CreateDefaultElements()
        {
            var mainCategory = MenuManager.CreateCategory("BoneLib", Color.white);

            var ammo = mainCategory.CreateSubPanel("Ammo Settings", Color.yellow);
            var itemSpawning = mainCategory.CreateSubPanel("Item Spawning", Color.white);
            var funstuff = mainCategory.CreateSubPanel("Fun Stuff", "#e600ff");

            ammo.CreateFunctionElement("Add Light Ammo", Color.white, () => ammoInventory.AddCartridge(lightAmmo, lightAmmoValue));
            ammo.CreateFunctionElement("Add Medium Ammo", Color.white, () => ammoInventory.AddCartridge(mediumAmmo, mediumAmmoValue));
            ammo.CreateFunctionElement("Add Heavy Ammo", Color.white, () => ammoInventory.AddCartridge(heavyAmmo, heavyAmmoValue));

            ammo.CreateIntElement("Light Ammo", "#d6d13e", lightAmmoValue, 100, 0, int.MaxValue, (value) => lightAmmoValue = value);
            ammo.CreateIntElement("Medium Ammo", "#d69e3e", mediumAmmoValue, 100, 0, int.MaxValue, (value) => mediumAmmoValue = value);
            ammo.CreateIntElement("Heavy Ammo", "#d63e3e", heavyAmmoValue, 100, 0, int.MaxValue, (value) => heavyAmmoValue = value);

            itemSpawning.CreateFunctionElement("Spawn Utility Gun", Color.white, () => SpawnUtilityGun());
            itemSpawning.CreateFunctionElement("Spawn Nimbus Gun", Color.white, () => SpawnNimbusGun());

            funstuff.CreateFunctionElement("Spawn Ad", Color.white, () => PopupBoxManager.CreateNewPopupBox(PopupBoxManager.adMessages.GetRandom()));
            funstuff.CreateFunctionElement("Spawn Shibe Ad", Color.white, () => PopupBoxManager.CreateNewShibePopup());
            funstuff.CreateFunctionElement("Spawn Bird Ad", Color.white, () => PopupBoxManager.CreateNewBirdPopup());
            funstuff.CreateFunctionElement("Spawn Cat Ad", Color.white, () => PopupBoxManager.CreateNewCatPopup());
        }

        internal static void SpawnUtilityGun()
        {
            var head = Player.GetPlayerHead().transform;

            string barcode = "c1534c5a-5747-42a2-bd08-ab3b47616467";
            SpawnableCrateReference reference = new SpawnableCrateReference(barcode);

            Spawnable spawnable = new Spawnable()
            {
                crateRef = reference
            };

            AssetSpawner.Register(spawnable);
            AssetSpawner.Spawn(spawnable, head.position + head.forward, default, new BoxedNullable<Vector3>(Vector3.one), false, new BoxedNullable<int>(null), null, null);
        }

        internal static void SpawnNimbusGun()
        {
            var head = Player.GetPlayerHead().transform;

            string barcode = "c1534c5a-6b38-438a-a324-d7e147616467";
            SpawnableCrateReference reference = new SpawnableCrateReference(barcode);

            Spawnable spawnable = new Spawnable()
            {
                crateRef = reference
            };

            AssetSpawner.Register(spawnable);
            AssetSpawner.Spawn(spawnable, head.position + head.forward, default, new BoxedNullable<Vector3>(Vector3.one), false, new BoxedNullable<int>(null), null, null);
        }
    }
}
