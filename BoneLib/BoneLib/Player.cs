using Il2CppSLZ.Bonelab;
using Il2CppSLZ.Interaction;
using Il2CppSLZ.Rig;
using Il2CppSLZ.VRMK;
using UnityEngine;

namespace BoneLib
{
    public static class Player
    {
        public static Avatar Avatar => RigManager.avatar;
        public static RigManager RigManager { get; private set; }
        public static PhysicsRig PhysicsRig { get; private set; }
        public static OpenControllerRig ControllerRig { get; private set; }
        public static RemapRig RemapRig { get; private set; }
        public static UIRig UIRig { get; private set; }

        public static Hand LeftHand { get; private set; }
        public static Hand RightHand { get; private set; }
        public static bool HandsExist => LeftHand != null && RightHand != null;

        public static BaseController LeftController { get; private set; }
        public static BaseController RightController { get; private set; }
        public static bool ControllersExist => LeftController != null && RightController != null;

        public static Transform Head { get; private set; }
        public static Transform Chest { get; private set; }

        internal static bool FindObjectReferences(RigManager manager = null)
        {
            ModConsole.Msg("Finding player object references");

            if (manager == null)
            {
                manager = GameObject.FindObjectOfType<RigManager>();
            }

            RigManager = manager;
            PhysicsRig = RigManager.physicsRig;
            ControllerRig = RigManager.ControllerRig.Cast<OpenControllerRig>();
            RemapRig = RigManager.remapHeptaRig;
            UIRig = UIRig.Instance;

            LeftController = RigManager.ControllerRig?.leftController;
            RightController = RigManager.ControllerRig?.rightController;

            LeftHand = PhysicsRig.leftHand;
            RightHand = PhysicsRig.rightHand;

            Head = PhysicsRig.m_head;

            ModConsole.Msg("Found player object references", LoggingMode.DEBUG);

            return ControllersExist && HandsExist && ControllerRig != null;
        }

        /// <summary>
        /// Returns the <see cref="PhysicsRig"/>.
        /// </summary>
        public static PhysicsRig GetPhysicsRig()
        {
            if (PhysicsRig == null)
            {
                PhysicsRig = GameObject.FindObjectOfType<PhysicsRig>();
            }

            return PhysicsRig;
        }

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
                    {
                        value = heldObject.GetComponentInChildren<T>();
                    }
                }
            }
            return value;
        }
        
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
            if (RemapRig == null)
            {
                return;
            }

            RemapRig.SetTwist(degrees);
        }
    }
}