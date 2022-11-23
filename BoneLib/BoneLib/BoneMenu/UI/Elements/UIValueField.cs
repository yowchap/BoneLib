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

        private Button leftButton;
        private Button rightButton;

        private void Awake()
        {
            leftButton = transform.Find("ButtonUp").GetComponent<Button>();
            rightButton = transform.Find("ButtonDown").GetComponent<Button>();

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

            if (leftButton != null)
            {
                leftButton.onClick.AddListener(onLeftPressed);
            }

            if (rightButton != null)
            {
                rightButton.onClick.AddListener(onRightPressed);
            }
        }
    }
}
