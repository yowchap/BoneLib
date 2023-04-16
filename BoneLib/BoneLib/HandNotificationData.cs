using Il2CppSLZ.Bonelab;
using UnityEngine;

namespace BoneLib
{
    public struct HandNotificationData
    {
        public TutorialRig.InputHighlight inputHighlightLeft;
        public TutorialRig.InputHighlight inputHighlightRight;

        public TutorialRig.LocationHighlight leftHighlightLocation;
        public TutorialRig.LocationHighlight rightHighlightLocation;

        public TutorialRig.SpecificHand hand;

        public bool isRightHand;

        public string leftTutorialText;
        public string rightTutorialText;

        public int flashRate;

        public float holdTime;

        public Sprite leftSpriteImage;
        public Sprite rightSpriteImage;

        public AudioClip tutorialClip;
    }
}
