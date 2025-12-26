using BoneLib.BoneMenu;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.Playables;

public class ElementCreator : MonoBehaviour
{
    public Texture2D background;
    public Texture2D customBackground;
    public Page ParentPage;
    public Page CurrentPage;

    private void Awake()
    {
        Menu.Initialize();
        Menu.OnPageOpened += (page) => {
            ParentPage = page.Parent;
            CurrentPage = page;
        };
    }

    private void Start()
    {
        Page mainPage = Page.Root.CreatePage("BoneLib", Color.white);

        Page ammoPage = mainPage.CreatePage("Ammo Settings", Color.yellow);
        Page itemSpawningPage = mainPage.CreatePage("Item Spawning", Color.white);
        Page funStuffPage = mainPage.CreatePage("Fun Stuff", Color.white);

        ammoPage.CreateFunction("Add Light Ammo", Color.white, null);
        ammoPage.CreateFunction("Add Medium Ammo", Color.white, null);
        ammoPage.CreateFunction("Add Heavy Ammo", Color.white, null);

        ammoPage.CreateInt("Light Ammo", Color.white, 0, 100, 0, int.MaxValue, null);
        ammoPage.CreateInt("Medium Ammo", Color.white, 0, 100, 0, int.MaxValue, null);
        ammoPage.CreateInt("Heavy Ammo", Color.white, 0, 100, 0, int.MaxValue, null);

        itemSpawningPage.CreateFunction("Spawn Utility Gun", Color.white, null);
        itemSpawningPage.CreateFunction("Spawn Nimbus Gun", Color.white, null);
        itemSpawningPage.CreateFunction("Spawn Random Gun", Color.white, null);
        itemSpawningPage.CreateFunction("Spawn Random Melee", Color.white, null);
        itemSpawningPage.CreateFunction("Spawn Random NPC", Color.white, null);
        itemSpawningPage.CreateFunction("Load Random Level", Color.white, null);
        itemSpawningPage.CreateFunction("Change Into Random Avatar", Color.white, null);

        funStuffPage.CreateFunction("Spawn Ad", Color.white, null);
        funStuffPage.CreateFunction("Spawn Shibe Ad", Color.white, null);
        funStuffPage.CreateFunction("Spawn Bird Ad", Color.white, null);
        funStuffPage.CreateFunction("Spawn Cat Ad", Color.white, null);
        funStuffPage.CreateString("Notification Text", Color.white, "None", null);
        funStuffPage.CreateFunction("Notification Test", Color.white, null);

        funStuffPage.CreateFunction("Make Dialog", Color.white, () =>
        {
            Menu.DisplayDialog(
                "Test",
                "This is a test message. Don't worry about it.",
                null,
                () =>
                {
                    Debug.Log("Hello from the Dialog confirm option!");
                });
        });
    }
}
