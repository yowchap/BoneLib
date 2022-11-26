using System;
using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using BoneLib.BoneMenu.Elements;

using SLZ.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UIToggleField : UIElement
    {
        public UIToggleField(IntPtr ptr) : base(ptr) { }

        private Button toggleButton;

        private ButtonHoverClick toggleFeedback;

        private void Awake()
        {
            toggleButton = transform.Find("Button").GetComponent<Button>();

            toggleFeedback = toggleButton.GetComponent<ButtonHoverClick>();

            toggleFeedback.feedback_audio = DataManager.Player.UIRig.feedbackAudio;
            toggleFeedback.feedback_tactile = DataManager.Player.UIRig.feedbackTactile;

            SetupListeners();
        }

        private void SetupListeners()
        {
            Action action = () =>
            {
                _element?.OnSelectElement();
                SetText(_element.Name, _element.DisplayValue);
            };

            if(toggleButton != null)
            {
                toggleButton.onClick.AddListener(action);
            }
        }
    }
}
