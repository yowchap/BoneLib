using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using BoneLib.BoneMenu.Elements;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIValueField : UIElement
    {
        public UIValueField(IntPtr ptr) : base(ptr) { }

        public override ElementType Type => ElementType.Value;

        private Button _leftButton;
        private Button _rightButton;

        private void Awake()
        {
            _leftButton = transform.Find("ButtonUp").GetComponent<Button>();
            _rightButton = transform.Find("ButtonDown").GetComponent<Button>();

            SetupListeners();
        }

        private void SetupListeners()
        {
            Action onLeftPressed = () =>
            {
                _element?.OnSelectLeft();
                SetText(_element.Name, _element.DisplayValue);
            };

            Action onRightPressed = () =>
            {
                _element?.OnSelectRight();
                SetText(_element.Name, _element.DisplayValue);
            };

            if (_leftButton != null)
            {
                _leftButton.onClick.AddListener(onLeftPressed);
            }

            if (_rightButton != null)
            {
                _rightButton.onClick.AddListener(onRightPressed);
            }
        }
    }
}
