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

        private ButtonHoverClick leftFeedback;
        private ButtonHoverClick rightFeedback;

        private void Awake()
        {
            leftButton = transform.Find("ButtonUp").GetComponent<Button>();
            rightButton = transform.Find("ButtonDown").GetComponent<Button>();

            leftFeedback = leftButton.GetComponent<ButtonHoverClick>();
            rightFeedback = rightButton.GetComponent<ButtonHoverClick>();

            leftFeedback.feedback_audio = DataManager.Player.UIRig.feedbackAudio;
            leftFeedback.feedback_tactile = DataManager.Player.UIRig.feedbackTactile;

            rightFeedback.feedback_audio = DataManager.Player.UIRig.feedbackAudio;
            rightFeedback.feedback_tactile = DataManager.Player.UIRig.feedbackTactile;

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
