using System;
using MelonLoader;
using System.Text.RegularExpressions;
using BoneLib.Nullables;
using SLZ.Marrow.Data;
using SLZ.Marrow.Pool;
using SLZ.Marrow.SceneStreaming;
using SLZ.Marrow.Warehouse;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System.IO;

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
        public static void SpawnCrate(string barcode, Vector3 position, Quaternion rotation = default, Vector3 scale = default, bool ignorePolicy = true, Action<GameObject> spawnAction = null)
        {
            var crateRef = new SpawnableCrateReference(barcode);
            var spawnable = new Spawnable()
            {
                crateRef = crateRef,
            };
            AssetSpawner.Register(spawnable);
            AssetSpawner.Spawn(spawnable, position, rotation, new BoxedNullable<Vector3>(scale), ignorePolicy, new BoxedNullable<int>(null), spawnAction);
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
        public static void SpawnCrate(SpawnableCrateReference crateReference, Vector3 position, Quaternion rotation = default, Vector3 scale = default, bool ignorePolicy = false, Action<GameObject> spawnAction = null)
        {
            var spawnable = new Spawnable()
            {
                crateRef = crateReference,
            };
            AssetSpawner.Register(spawnable);
            AssetSpawner.Spawn(spawnable, position, rotation, new BoxedNullable<Vector3>(scale), ignorePolicy, new BoxedNullable<int>(null), spawnAction);
        }

        /// <summary>
        /// Checks if the player is in a loading screen or not
        /// </summary>
        /// <returns>True if player is loading, false if not</returns>
        public static bool IsLoading() => SceneStreamer.Session.Status == StreamStatus.LOADING;

        /// <summary>
        /// Loads an embedded assetbundle
        /// </summary>
        public static AssetBundle LoadEmbeddedAssetBundle(Assembly assembly, string name)
        {
            string[] manifestResources = assembly.GetManifestResourceNames();
            AssetBundle bundle = null;
            if (manifestResources.Contains(name))
            {
                ModConsole.Msg($"Loading embedded resource data {name}...", LoggingMode.DEBUG);
                using Stream str = assembly.GetManifestResourceStream(name);
                using MemoryStream memoryStream = new MemoryStream();

                str.CopyTo(memoryStream);
                ModConsole.Msg("Done!", LoggingMode.DEBUG);
                byte[] resource = memoryStream.ToArray();

                ModConsole.Msg($"Loading assetBundle from data {name}, please be patient...", LoggingMode.DEBUG);
                bundle = AssetBundle.LoadFromMemory(resource);
                ModConsole.Msg("Done!", LoggingMode.DEBUG);
            }
            return bundle;
        }

        /// <summary>
        /// Loads an asset from an assetbundle
        /// </summary>
        public static T LoadPersistentAsset<T>(this AssetBundle assetBundle, string name) where T : UnityEngine.Object
        {
            UnityEngine.Object asset = assetBundle.LoadAsset(name);

            if (asset != null)
            {
                asset.hideFlags = HideFlags.DontUnloadUnusedAsset;
                return asset.TryCast<T>();
            }

            return null;
        }

        /// <summary>
        /// Gets the raw bytes of an embedded resource
        /// </summary>
        public static byte[] GetResourceBytes(Assembly assembly, string name)
        {
            foreach (string resource in assembly.GetManifestResourceNames())
            {
                if (resource.Contains(name))
                {
                    using (Stream resFilestream = assembly.GetManifestResourceStream(resource))
                    {
                        if (resFilestream == null) return null;
                        byte[] byteArr = new byte[resFilestream.Length];
                        resFilestream.Read(byteArr, 0, byteArr.Length);
                        return byteArr;
                    }
                }
            }
            return null;
        }
        
        ///<summary>
        /// Checks if an assembly is loaded from name
        /// </summary>
        public static bool CheckIfAssemblyLoaded(string name)
        {
            return AppDomain.CurrentDomain.GetAssemblies().Any(asm => asm.GetName().Name.ToLower().Contains(name.ToLower()));
        }
    }
}