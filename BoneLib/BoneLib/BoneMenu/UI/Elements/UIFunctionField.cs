using System;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

using BoneLib.BoneMenu.Elements;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIFunctionField : UIElement
    {
        public UIFunctionField(IntPtr ptr) : base(ptr) { }

        public override ElementType Type => ElementType.Function;

        private Button functionButton;
        private Button confirmerButton;

        private TextMeshPro confirmerText;

        private void Awake()
        {
            functionButton = transform.Find("Button").GetComponent<Button>();
            confirmerButton = transform.Find("Confirmer").GetComponent<Button>();
            confirmerText = confirmerButton.transform.Find("ConfirmValue").GetComponent<TextMeshPro>();

            Initialize();
        }

        private void Initialize()
        {
            Action onPressed = () =>
            {
                var functionElement = (FunctionElement)_element;
                confirmerText.text = functionElement.ConfirmText;
                functionElement.OnSelectElement();

                if (functionElement.Confirmer)
                {
                    confirmerButton.gameObject.SetActive(true);
                }
            };

            Action onConfirmerPressed = () =>
            {
                var functionElement = (FunctionElement)_element;
                functionElement.OnSelectConfirm();
                confirmerButton.gameObject.SetActive(false);
            };

            functionButton?.onClick.AddListener(onPressed);
            confirmerButton?.onClick.AddListener(onConfirmerPressed);
        }
    }
}
