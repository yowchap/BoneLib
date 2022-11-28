using System;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

using BoneLib.BoneMenu.Elements;

using SLZ.UI;

namespace BoneLib.BoneMenu.UI
{
    [MelonLoader.RegisterTypeInIl2Cpp]
    public class UICategoryField : UIElement
    {
        public UICategoryField(IntPtr ptr) : base(ptr) { }

        private Button categoryButton;

        private ButtonHoverClick categoryFeedback;

        private void Awake()
        {
            categoryButton = transform.Find("Button").GetComponent<Button>();

            categoryFeedback = categoryButton.GetComponent<ButtonHoverClick>();

            categoryFeedback.feedback_audio = Player.GetRigManager().GetComponent<SLZ.Rig.RigManager>().uiRig.feedbackAudio;
            categoryFeedback.feedback_tactile = Player.GetRigManager().GetComponent<SLZ.Rig.RigManager>().uiRig.feedbackTactile;

            SetupListeners();
        }

        private void SetupListeners()
        {
            Action action = () =>
            {
                var category = (MenuCategory)_element;
                MenuManager.SelectCategory(category);
            };

            if (categoryButton != null)
            {
                categoryButton.onClick.AddListener(action);
            }
        }
    }
}
