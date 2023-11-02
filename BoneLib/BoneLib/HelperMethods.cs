using System;
using MelonLoader;
using System.Text.RegularExpressions;
using BoneLib.Nullables;
using SLZ.Marrow.Data;
using SLZ.Marrow.Pool;
using SLZ.Marrow.SceneStreaming;
using SLZ.Marrow.Warehouse;
using UnityEngine;

namespace BoneLib
{
    public static class HelperMethods
    {
        /// <summary>
        /// Removes things like [2] and (Clone)
        /// </summary>
        public static string GetCleanObjectName(string name)
        {
            Regex regex = new Regex(@"\[\d+\]|\(\d+\)"); // Stuff like (1) or [24]
            name = regex.Replace(name, "");
            name = name.Replace("(Clone)", "");
            return name.Trim();
        }

        /// <summary>
        /// Checks if the user is running MelonLoader on Android
        /// </summary>
        public static bool IsAndroid() => isAndroid;

        private static readonly bool isAndroid = MelonUtils.CurrentPlatform == (MelonPlatformAttribute.CompatiblePlatforms)3;

        /// <summary>
        /// Spawns a crate from barcode.
        /// </summary>
        /// <param name="barcode">The barcode of the crate</param>
        /// <param name="position">The position to spawn the crate at</param>
        /// <param name="rotation">The rotation of the spawned object</param>
        /// <param name="scale">The scale of the spawned object</param>
        /// <param name="ignorePolicy">Ignore spawn policy or not</param>
        /// <param name="spawnAction">Code to run once the spawnable is placed</param>
        public static void SpawnCrate(string barcode, Vector3 position, Quaternion rotation, Vector3 scale, bool ignorePolicy, Action<GameObject> spawnAction)
        {
            var crateRef = new SpawnableCrateReference(barcode);
            var spawnable = new Spawnable()
            {
                crateRef = crateRef,
            };
            AssetSpawner.Register(spawnable);
            AssetSpawner.Spawn(spawnable, position, rotation, new BoxedNullable<Vector3>(scale), ignorePolicy,new BoxedNullable<int>(null), spawnAction);
        }
        
        /// <summary>
        /// Spawns a crate from a crate reference.
        /// </summary>
        /// <param name="crateReference">The crate reference to spawn</param>
        /// <param name="position">The position to spawn the crate at</param>
        /// <param name="rotation">The rotation of the spawned object</param>
        /// <param name="scale">The scale of the spawned object</param>
        /// <param name="ignorePolicy">Ignore spawn policy or not</param>
        /// <param name="spawnAction">Code to run once the spawnable is placed</param>
        public static void SpawnCrate(SpawnableCrateReference crateReference, Vector3 position, Quaternion rotation, Vector3 scale, bool ignorePolicy, Action<GameObject> spawnAction)
        {
            var spawnable = new Spawnable()
            {
                crateRef = crateReference,
            };
            AssetSpawner.Register(spawnable);
            AssetSpawner.Spawn(spawnable, position, rotation, new BoxedNullable<Vector3>(scale), ignorePolicy,new BoxedNullable<int>(null), spawnAction);
        }
        
        /// <summary>
        /// Checks if the player is in a loading screen or not
        /// </summary>
        /// <returns>True if player is loading, false if not</returns>
        public static bool IsLoading()
        {
            return SceneStreamer.Session.Status == StreamStatus.LOADING;
        }
    }
}
