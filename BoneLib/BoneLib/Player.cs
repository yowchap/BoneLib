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
        public static ControllerRig controllerRig { get; private set; }

        private static PhysicsRig physicsRig;

        
        internal static bool FindObjectReferences(RigManager rigManager = null)
        {
            ModConsole.Msg("Finding player object references", LoggingMode.DEBUG);

            if (controllersExist && handsExist && controllerRig != null)
                return false;
            if (rigManager == null)
                rigManager = GameObject.FindObjectOfType<RigManager>();

            leftController = rigManager?.ControllerRig?.leftController;
            rightController = rigManager?.ControllerRig?.rightController;

            controllerRig = rigManager?.ControllerRig;

            leftHand = rigManager?.physicsRig?.leftHand;
            rightHand = rigManager?.physicsRig?.rightHand;
            physicsRig = rigManager?.physicsRig;

            ModConsole.Msg("Found player object references", LoggingMode.DEBUG);

            return controllersExist && handsExist && controllerRig != null;
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

        /// <summary>
        /// Positive values: Clockwise rotation 
        /// <para/>
        /// Negative values: Counterclockwise rotation
        /// </summary>
        public static void RotatePlayer(float degrees)
        {
            if (controllerRig != null)
                controllerRig.SetTwist(degrees);
        }
    }
}
