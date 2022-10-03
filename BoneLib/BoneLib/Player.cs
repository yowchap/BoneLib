using SLZ.Interaction;
using SLZ.Props.Weapons;
using SLZ.Rig;
using SLZ.VRMK;
using UnityEngine;

namespace BoneLib
{
    public static class Player
    {
        private static readonly string rigManagerName = "[RigManager (Blank)]";
        private static readonly string playerHeadPath = "[PhysicsRig]/Head"; // The path to the head relative to the rig manager

        private static GameObject rigManager;
        private static GameObject playerHead;

        public static Hand leftHand { get; private set; }
        public static Hand rightHand { get; private set; }
        public static bool handsExist => leftHand != null && rightHand != null;

        public static BaseController leftController { get; private set; }
        public static BaseController rightController { get; private set; }
        public static bool controllersExist => leftController != null && rightController != null;
        private static ControllerRig controllerRig;

        private static PhysicsRig physicsRig;

        internal static void FindObjectReferences()
        {
            Hand[] hands = GameObject.FindObjectsOfType<Hand>();
            foreach (Hand hand in hands)
            {
                if (hand.name.ToLower().Contains("left"))
                    leftHand = hand;
                else if (hand.name.ToLower().Contains("right"))
                    rightHand = hand;
            }
            BaseController[] baseControllers = GameObject.FindObjectsOfType<BaseController>();
            foreach (BaseController baseController in baseControllers)
            {
                if (baseController.name.ToLower().Contains("left"))
                    leftController = baseController;
                else if (baseController.name.ToLower().Contains("right"))
                    rightController = baseController;
            }
            controllerRig = GameObject.FindObjectOfType<ControllerRig>();
        }
        /// <summary>
        /// Returns the root gameobject in the player rig manager.
        /// </summary>
        public static GameObject GetRigManager()
        {
            if (rigManager == null)
                rigManager = GameObject.Find(rigManagerName);

            return rigManager;
        }

        /// <summary>
        /// Returns the gameobject of the player's head.
        /// </summary>
        public static GameObject GetPlayerHead()
        {
            if (playerHead == null)
            {
                GameObject rig = GetRigManager();
                if (rig != null)
                    playerHead = rig.transform.Find(playerHeadPath).gameObject;
            }
            return playerHead;
        }
        /// <summary>
        /// Returns the PhysicsRig.
        /// </summary>
        public static PhysicsRig GetPhysicsRig()
        {
            if (physicsRig == null)
                physicsRig = GameObject.FindObjectOfType<PhysicsRig>();
            return physicsRig;
        }

        /// <summary>
        /// Returns the Player's current avatar.
        /// </summary>
        public static Avatar GetCurrentAvatar() => GetRigManager().GetComponent<RigManager>().avatar;

        /// <summary>
        /// Generic method for getting any component on the object the player is holding.
        /// </summary>
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
        /// Returns the object the given hand is holding or null if the hand is null.
        /// </summary>
        public static GameObject GetObjectInHand(Hand hand) => hand?.m_CurrentAttachedGO;

        /// <summary>
        /// Positive values: Clockwise rotation
        /// Negative values: Counterclockwise rotation
        /// </summary>
        public static void RotatePlayer(float rotation)
        {
            if (controllerRig != null)
                controllerRig.SetTwist(rotation);
        }
    }
}
