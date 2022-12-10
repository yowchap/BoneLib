using BoneLib.BoneMenu.Elements;
using System;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UICategoryField : UIElement
    {
        public UICategoryField(IntPtr ptr) : base(ptr) { }

        private Button categoryButton;

        private void Awake()
        {
            categoryButton = transform.Find("Button").GetComponent<Button>();

            SetupListeners();
        }

        private void SetupListeners()
        {
            Action action = () =>
            {
                var category = (MenuCategory)element;
                MenuManager.SelectCategory(category);
            };

            if (categoryButton != null)
            {
                categoryButton.onClick.AddListener(action);
            }
        }
    }
}
