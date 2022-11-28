using SLZ.Interaction;
using SLZ.Props.Weapons;
using SLZ.Rig;
using SLZ.VRMK;
using UnityEngine;

namespace BoneLib
{
    public static class Player
    {
        private static GameObject playerHead;

        public static RigManager rigManager
        {
            get
            {
                if(_rigManager == null)
                {
                    _rigManager = GameObject.FindObjectOfType<RigManager>();
                }

                return _rigManager;
            }
        }

        public static PhysicsRig physicsRig { get => rigManager?.physicsRig; }
        public static ControllerRig controllerRig { get => rigManager?.ControllerRig; }
        public static UIRig uiRig { get => rigManager?.uiRig; }

        public static Hand leftHand { get => physicsRig?.leftHand; }
        public static Hand rightHand { get => physicsRig?.rightHand; }
        public static bool handsExist => leftHand != null && rightHand != null;

        public static BaseController leftController { get => controllerRig?.leftController; }
        public static BaseController rightController { get => controllerRig?.rightController; }
        public static bool controllersExist => leftController != null && rightController != null;

        private static RigManager _rigManager;

        /// <summary>
        /// Returns the root gameobject of the Player's <see cref="RigManager"/>.
        /// </summary>
        public static GameObject GetRigManager()
        {
            return rigManager.gameObject;
        }

        /// <summary>
        /// Returns the gameobject of the Player's head.
        /// </summary>
        public static GameObject GetPlayerHead()
        {
            return physicsRig.m_head.gameObject;
        }

        /// <summary>
        /// Returns the <see cref="PhysicsRig"/>.
        /// </summary>
        public static PhysicsRig GetPhysicsRig()
        {
            return physicsRig;
        }

        /// <summary>
        /// Returns the Player's current <see cref="Avatar"/>.
        /// </summary>
        public static Avatar GetCurrentAvatar() => GetRigManager().GetComponent<RigManager>().avatar;

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