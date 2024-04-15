using BoneLib.BoneMenu.Elements;
using System;
using TMPro;
using UnityEngine.UI;

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
                FunctionElement functionElement = (FunctionElement)element;
                confirmerText.text = functionElement.ConfirmText;

                if (functionElement.Confirmer)
                {
                    confirmerButton.gameObject.SetActive(true);
                    confirmerText.gameObject.SetActive(true);
                }
                else
                {
                    functionElement.OnSelectElement();
                }
            };

            Action onConfirmerPressed = () =>
            {
                FunctionElement functionElement = (FunctionElement)element;
                functionElement.OnSelectConfirm();
                confirmerButton.gameObject.SetActive(false);
            };

            functionButton?.onClick.AddListener(onPressed);
            confirmerButton?.onClick.AddListener(onConfirmerPressed);
        }
    }
}
