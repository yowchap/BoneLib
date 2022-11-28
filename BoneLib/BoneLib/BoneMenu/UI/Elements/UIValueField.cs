using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using BoneLib.BoneMenu.Elements;

using SLZ.UI;

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
                element?.OnSelectLeft();
                SetText(element.Name, element.DisplayValue);
            };

            Action onRightPressed = () =>
            {
                element?.OnSelectRight();
                SetText(element.Name, element.DisplayValue);
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
