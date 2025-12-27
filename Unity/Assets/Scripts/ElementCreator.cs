using BoneLib.BoneMenu;
using BoneLib.BoneMenu.UI;
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

        funStuffPage.CreateFunction("Make Dialog (New)", Color.white, () =>
        {
            DialogData dialog = new DialogData()
            {
                Title = "Test",
                Message = "This is a test message.",
                Primary = Dialog.DefaultPrimaryColor,
                Secondary = Color.red * 0.5f,
                Icon = null,
                Confirm = () =>
                {
                    Debug.Log("Hello from the Dialog confirm option!");
                }
            };

            Menu.DisplayDialog(dialog);
        });

        

        Page tooltipPage = funStuffPage.CreatePage("Tooltip Page", Color.white);

        var funcElementTooltip = tooltipPage.CreateFunction("Test Tooltip", Color.white, null);
        funcElementTooltip.SetTooltip("This function actually does nothing.");

        var intElementTooltip = tooltipPage.CreateInt("Test Tooltip (Int)", Color.white, 0, 1, 0, 10, null);
        intElementTooltip.SetTooltip("This int stuff does nothing.");

        var floatElementTooltip = tooltipPage.CreateFloat("Test Tooltip (Float)", Color.white, 0, 1, 0, 10, null);
        floatElementTooltip.SetTooltip("This float stuff does nothing.");

        var enumElementTooltip = tooltipPage.CreateEnum("Test Tooltip (Enum)", Color.white, ElementProperties.Default, null);
        enumElementTooltip.SetTooltip("ElementProperties comes in two flavors: Password and NoBorder.");

        var stringElementTooltip = tooltipPage.CreateString("Test Tooltip (String)", Color.white, "None", null);
        stringElementTooltip.SetTooltip("This string element stuff is cool. There's a keyboard too.");

        var boolElementTooltip = tooltipPage.CreateBool("Test Tooltip (Bool)", Color.white, false, null);
        boolElementTooltip.SetTooltip("This bool stuff does nothing.");
    }
}
