using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIFunctionField : UIElement
    {
        public UIFunctionField(System.IntPtr ptr) : base(ptr) { }

        public override string Type => ElementType.Function;

        private Button _functionButton;

        private void Awake()
        {
            _functionButton = transform.Find("Button").GetComponent<Button>();

            SetupListeners();
        }

        private void SetupListeners()
        {
            Action onPressed = () => _element?.OnSelectElement();

            if(_functionButton != null)
            {
                _functionButton.onClick.AddListener(onPressed);
            }
        }
    }
}
