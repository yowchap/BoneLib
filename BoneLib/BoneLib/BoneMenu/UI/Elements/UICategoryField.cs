using System;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

using BoneLib.BoneMenu.Elements;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UICategoryField : UIElement
    {
        public UICategoryField(IntPtr ptr) : base(ptr) { }

        private Button _categoryButton;

        private void Awake()
        {
            _categoryButton = transform.Find("Button").GetComponent<Button>();
            SetupListeners();
        }

        private void SetupListeners()
        {
            Action action = () =>
            {
                var category = (MenuCategory)_element;
                MenuManager.SelectCategory(category);
            };

            if (_categoryButton != null)
            {
                _categoryButton.onClick.AddListener(action);
            }
        }
    }
}
