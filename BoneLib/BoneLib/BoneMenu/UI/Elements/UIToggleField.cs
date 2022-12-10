using System;
using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using BoneLib.BoneMenu.Elements;

using SLZ.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIToggleField : UIElement
    {
        public UIToggleField(IntPtr ptr) : base(ptr) { }

        private Button toggleButton;

        private void Awake()
        {
            toggleButton = transform.Find("Button").GetComponent<Button>();

            SetupListeners();
        }

        private void SetupListeners()
        {
            Action action = () =>
            {
                element?.OnSelectElement();
                SetText(element.Name, element.DisplayValue);
            };

            if(toggleButton != null)
            {
                toggleButton.onClick.AddListener(action);
            }
        }
    }
}
