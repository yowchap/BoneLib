using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Interaction;
using Il2CppSLZ.Props.Weapons;
using Il2CppSLZ.Rig;
using Il2CppSLZ.VRMK;
using UnityEngine;

namespace BoneLib
{
    public static class Player
    {
        private static readonly string playerHeadPath = "[PhysicsRig]/Head"; // The path to the head relative to the rig manager

        public static RigManager rigManager { get; private set; }
        public static PhysicsRig physicsRig { get; private set; }
        public static ControllerRig controllerRig { get; private set; }
        public static UIRig uiRig { get; private set; }
        public static TutorialRig tutorialRig { get; private set; }

        public static Hand leftHand { get; private set; }
        public static Hand rightHand { get; private set; }
        public static bool handsExist => leftHand != null && rightHand != null;

        public static BaseController leftController { get; private set; }
        public static BaseController rightController { get; private set; }
        public static bool controllersExist => leftController != null && rightController != null;

        public static Transform playerHead { get; private set; }

        internal static bool FindObjectReferences(RigManager manager = null)
        {
            ModConsole.Msg("Finding player object references", LoggingMode.DEBUG);

            if (controllersExist && handsExist && controllerRig != null)
                return false;
            if (manager == null)
                manager = GameObject.FindObjectOfType<RigManager>();

            rigManager = manager;
            physicsRig = manager?.physicsRig;
            controllerRig = manager?.ControllerRig;
            uiRig = manager?.uiRig;
            tutorialRig = manager?.tutorialRig;

            leftController = manager?.ControllerRig?.leftController;
            rightController = manager?.ControllerRig?.rightController;

            leftHand = manager?.physicsRig?.leftHand;
            rightHand = manager?.physicsRig?.rightHand;

            playerHead = rigManager.transform.Find(playerHeadPath);

            ModConsole.Msg("Found player object references", LoggingMode.DEBUG);

            return controllersExist && handsExist && controllerRig != null;
        }

        /// <summary>
        /// Returns the <see cref="PhysicsRig"/>.
        /// </summary>
        public static PhysicsRig GetPhysicsRig()
        {
            if (physicsRig == null)
                physicsRig = GameObject.FindObjectOfType<PhysicsRig>();
            return physicsRig;
        }

        /// <summary>
        /// Returns the Player's current <see cref="Avatar"/>.
        /// </summary>
        public static Avatar GetCurrentAvatar() => rigManager.avatar;

        /// <summary>
        /// Generic method for getting any component on the object the Player is holding.
        /// </summary>
        /// <returns>null if there is no component of type <typeparamref name="T"/>, or <paramref name="hand"/> is null.</returns>
        public static T GetComponentInHand<T>(Hand hand) where T : Component
        {
            T value = null;
            if (hand != null)
            {
                GameObject heldObject = hand.m_CurrentAttachedGO;
                if (heldObject != null)
                {
                    value = heldObject.GetComponentInParent<T>();
                    if (!value)
                        value = heldObject.GetComponentInChildren<T>();
                }
            }
            return value;
        }

        // Figured I'd include this since getting a gun is probably the most common use of GetComponentInHand
        public static Gun GetGunInHand(Hand hand) => GetComponentInHand<Gun>(hand);

        /// <summary>
        /// Returns the object <paramref name="hand"/> is holding or null if <paramref name="hand"/> is null.
        /// </summary>
        public static GameObject GetObjectInHand(Hand hand) => hand?.m_CurrentAttachedGO;

        public static void SendHandNotification(HandNotificationData notificationData)
        {
            tutorialRig.CUSTOMTUTORIAL(
                notificationData.inputHighlightLeft,
                notificationData.inputHighlightRight,
                notificationData.leftHighlightLocation,
                notificationData.rightHighlightLocation,
                notificationData.hand,
                notificationData.isRightHand,
                notificationData.leftTutorialText,
                notificationData.rightTutorialText,
                notificationData.flashRate,
                notificationData.holdTime,
                notificationData.leftSpriteImage,
                notificationData.rightSpriteImage,
                notificationData.tutorialClip,
                null);
        }

        public static void SendHeadNotification(HeadNotificationData notificationData)
        {
            tutorialRig.gameObject.SetActive(true);
            tutorialRig.headTitles.gameObject.SetActive(true);

            tutorialRig.headTitles.CUSTOMDISPLAY(
                notificationData.title,
                notificationData.subtitle,
                notificationData.mainSprite,
                notificationData.holdTime,
                notificationData.clip,
                notificationData.isAvatarLevel,
                notificationData.spriteA,
                notificationData.spriteB,
                notificationData.spriteC,
                notificationData.spriteD);
        }

        /// <summary>
        /// Positive values: Clockwise rotation 
        /// <para/>
        /// Negative values: Counterclockwise rotation
        /// </summary>
        public static void RotatePlayer(float degrees)
        {
            //if (rigManager.virtualHeptaRig != null)
                //virtualHeptaRig.SetTwist(degrees);
        }
    }
}