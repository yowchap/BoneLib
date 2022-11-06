using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UICategoryField : UIElement
    {
        public UICategoryField(IntPtr ptr) : base(ptr) { }

        private Button _categoryButton;

        private void Awake()
        {
            _categoryButton = transform.Find("[Button] - Category").GetComponent<Button>();
            SetupListeners();
        }

        private void SetupListeners()
        {
            Action action = () =>
            {
                MenuManager.SelectCategory((MenuCategory)_element);
            };

            if(_categoryButton != null)
            {
                _categoryButton.onClick.AddListener(action);
            }
        }
    }
}
