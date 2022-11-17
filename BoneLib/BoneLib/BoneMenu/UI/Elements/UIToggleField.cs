using System;
using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using BoneLib.BoneMenu.Elements;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIToggleField : UIElement
    {
        public UIToggleField(IntPtr ptr) : base(ptr) { }

        private Button _toggleButton;

        private void Awake()
        {
            _toggleButton = transform.Find("Button").GetComponent<Button>();
            SetupListeners();
        }

        private void SetupListeners()
        {
            Action action = () =>
            {
                _element?.OnSelectElement();
                SetText(_element.Name, _element.DisplayValue);
            };

            if(_toggleButton != null)
            {
                _toggleButton.onClick.AddListener(action);
            }
        }
    }
}
