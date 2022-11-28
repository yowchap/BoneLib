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

        private ButtonHoverClick functionFeedback;
        private ButtonHoverClick confirmerFeedback;

        private void Awake()
        {
            functionButton = transform.Find("Button").GetComponent<Button>();
            confirmerButton = transform.Find("Confirmer").GetComponent<Button>();
            confirmerText = confirmerButton.transform.Find("ConfirmValue").GetComponent<TextMeshPro>();

            confirmerButton.gameObject.SetActive(false);
            confirmerText.gameObject.SetActive(false);

            functionFeedback = functionButton.GetComponent<ButtonHoverClick>();
            confirmerFeedback = confirmerButton.GetComponent<ButtonHoverClick>();

            functionFeedback.feedback_audio = Player.GetRigManager().GetComponent<SLZ.Rig.RigManager>().uiRig.feedbackAudio;
            functionFeedback.feedback_tactile = Player.GetRigManager().GetComponent<SLZ.Rig.RigManager>().uiRig.feedbackTactile;

            confirmerFeedback.feedback_audio = Player.GetRigManager().GetComponent<SLZ.Rig.RigManager>().uiRig.feedbackAudio;
            confirmerFeedback.feedback_tactile = Player.GetRigManager().GetComponent<SLZ.Rig.RigManager>().uiRig.feedbackTactile;

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
