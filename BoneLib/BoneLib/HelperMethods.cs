using System;
using MelonLoader;
using System.Text.RegularExpressions;
using Il2CppInterop.Runtime.InteropTypes;
using Il2CppSLZ.Marrow.Data;
using Il2CppSLZ.Marrow.Pool;
using Il2CppSLZ.Marrow.SceneStreaming;
using Il2CppSLZ.Marrow.Warehouse;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Collections.Generic;

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
        /// <param name="ignorePolicy">Ignore spawn policy or not</param>
        /// <param name="spawnAction">Code to run once the spawnable is placed</param>
        public static void SpawnCrate(string barcode, Vector3 position, Quaternion rotation = default, Vector3 scale = default, bool ignorePolicy = false, Action<GameObject> spawnAction = null, Action<GameObject> despawnAction = null)
        {
            SpawnableCrateReference crateReference = new SpawnableCrateReference(barcode);
            SpawnCrate(crateReference, position, rotation, scale, ignorePolicy, spawnAction, despawnAction);
        }

        /// <summary>
        /// Spawns a crate from a crate reference.
        /// </summary>
        /// <param name="crateReference">The crate reference to spawn</param>
        /// <param name="position">The position to spawn the crate at</param>
        /// <param name="rotation">The rotation of the spawned object</param>
        /// <param name="ignorePolicy">Ignore spawn policy or not</param>
        /// <param name="spawnAction">Code to run once the spawnable is placed</param>
        public static void SpawnCrate(SpawnableCrateReference crateReference, Vector3 position, Quaternion rotation = default, Vector3 scale = default, bool ignorePolicy = false, Action<GameObject> spawnAction = null, Action<GameObject> despawnAction = null)
        {
            Spawnable spawnable = new Spawnable()
            {
                crateRef = crateReference
            };

            AssetSpawner.Register(spawnable);
            AssetSpawner.Spawn(spawnable, position, rotation, new Il2CppSystem.Nullable<Vector3>(scale), null, ignorePolicy, new Il2CppSystem.Nullable<int>(), spawnAction, despawnAction);
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
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < assemblies.Length; i++)
            {
                string asmName = assemblies[i].GetName().Name;
                if (asmName.ToLower() == name.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the collection contains a specific Il2Cpp object.
        /// On Android it compares the <see cref="Il2CppObjectBase.Pointer"/> of each object
        /// instead of the objects themselves.
        /// On Windows it behaves identically to <see cref="ICollection{T}.Contains(T)"/>.
        /// </summary>
        public static bool ContainsIl2Cpp<T>(this ICollection<T> coll, T item) where T : Il2CppObjectBase
        {
            if (MelonUtils.IsWindows)
                return coll.Contains(item);
            foreach (var x in coll)
                if (x.Pointer == item.Pointer)
                    return true;
            return false;
        }

        /// <summary>
        /// Removes the first occurrence of a specific Il2Cpp object from the collection.
        /// On Android it compares the <see cref="Il2CppObjectBase.Pointer"/> of each object
        /// instead of the objects themselves.
        /// On Windows it behaves identically to <see cref="ICollection{T}.Remove(T)"/>.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if <paramref name="item"/> was removed; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool RemoveIl2Cpp<T>(this ICollection<T> coll, T item) where T : Il2CppObjectBase
        {
            if (MelonUtils.IsWindows)
            {
                return coll.Remove(item);
            }
            else if (coll is IList<T> list)
            {
                for (int i = 0; i < list.Count; i++)
                    if (list[i].Pointer == item.Pointer)
                    {
                        list.RemoveAt(i);
                        return true;
                    }
                return false;
            }
            else
            {
                T match = null;
                bool found = false;
                foreach (var x in coll)
                    if (x.Pointer == item.Pointer)
                    {
                        match = x;
                        found = true;
                        break;
                    }
                if (found)
                    return coll.Remove(match);
                return false;
            }
        }

        /// <summary>
        /// Gets the index of a specific Il2Cpp object in the list.
        /// On Android it compares the <see cref="Il2CppObjectBase.Pointer"/> of each object
        /// instead of the objects themselves.
        /// On Windows it behaves identically to <see cref="IList{T}.IndexOf(T)"/>.
        /// </summary>
        /// <returns>
        /// The index of <paramref name="item"/> if found in the list; otherwise, -1.
        /// </returns>
        public static int IndexOfIl2Cpp<T>(this IList<T> list, T item) where T : Il2CppObjectBase
        {
            if (MelonUtils.IsWindows)
                return list.IndexOf(item);
            for (int i = 0; i < list.Count; i++)
                if (list[i].Pointer == item.Pointer)
                    return i;
            return -1;
        }
    }
}
