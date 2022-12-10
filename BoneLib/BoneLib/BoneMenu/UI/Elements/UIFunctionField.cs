using System;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

using BoneLib.BoneMenu.Elements;

using SLZ.UI;

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

            confirmerButton.gameObject.SetActive(false);
            confirmerText.gameObject.SetActive(false);

            Initialize();
        }

        private void Initialize()
        {
            Action onPressed = () =>
            {
                var functionElement = (FunctionElement)element;
                confirmerText.text = functionElement.ConfirmText;
                functionElement.OnSelectElement();

                if (functionElement.Confirmer)
                {
                    confirmerButton.gameObject.SetActive(true);
                }
            };

            Action onConfirmerPressed = () =>
            {
                var functionElement = (FunctionElement)element;
                functionElement.OnSelectConfirm();
                confirmerButton.gameObject.SetActive(false);
            };

            functionButton?.onClick.AddListener(onPressed);
            confirmerButton?.onClick.AddListener(onConfirmerPressed);
        }
    }
}
