using System;
using MelonLoader;
using System.Text.RegularExpressions;
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
        /// Loads a level from a crate reference
        /// </summary>
        /// <param name="level">The crate reference to load</param>
        public static void LoadLevel(LevelCrateReference level)
        {
            LoadLevel(level.Barcode.ID, CommonBarcodes.Maps.LoadDefault);
        }

        /// <summary>
        /// Loads a level from a barcode
        /// </summary>
        /// <param name="barcode">The barcode of the level</param>
        public static void LoadLevel(string barcode)
        {
            LoadLevel(barcode, CommonBarcodes.Maps.LoadDefault);
        }

        /// <summary>
        /// Loads a level from a crate reference
        /// </summary>
        /// <param name="level">The crate reference to load</param>
        /// <param name="loadLevel">The crate reference for the loading scene</param>
        public static void LoadLevel(LevelCrateReference level, LevelCrateReference loadLevel)
        {
            LoadLevel(level.Barcode.ID, loadLevel.Barcode.ID);
        }

        /// <summary>
        /// Loads a level from a barcode
        /// </summary>
        /// <param name="levelBarcode">The barcode of the level</param>
        /// <param name="loadLevelBarcode">The barcode of the loading scene</param>
        public static void LoadLevel(string levelBarcode, string loadLevelBarcode)
        {
            SceneStreamer.Load(new Barcode(levelBarcode), new Barcode(loadLevelBarcode));
        }

        /// <summary>
        /// Loads a level from a crate reference with load fade
        /// </summary>
        /// <param name="level">The crate reference to load</param>
        /// <param name="fadeFast">When true, loads with a faster fade</param>
        public static void FadeLoadLevel(LevelCrateReference level, bool fastFade = false)
        {
            FadeLoadLevel(level.Barcode.ID, CommonBarcodes.Maps.LoadDefault, fastFade);
        }

        /// <summary>
        /// Loads a level from a barcode with load fade
        /// </summary>
        /// <param name="barcode">The barcode of the level</param>
        /// <param name="fadeFast">When true, loads with a faster fade</param>
        public static void FadeLoadLevel(string barcode, bool fastFade = false)
        {
            FadeLoadLevel(barcode, CommonBarcodes.Maps.LoadDefault, fastFade);
        }

        /// <summary>
        /// Loads a level from a crate reference with load fade
        /// </summary>
        /// <param name="level">The crate reference to load</param>
        /// <param name="loadLevel">The crate reference for the loading scene</param>
        /// <param name="fadeFast">When true, loads with a faster fade</param>
        public static void FadeLoadLevel(LevelCrateReference level, LevelCrateReference loadLevel, bool fastFade = false)
        {
            FadeLoadLevel(level.Barcode.ID, loadLevel.Barcode.ID, fastFade);
        }

        /// <summary>
        /// Loads a level from a barcode with load fade
        /// </summary>
        /// <param name="levelBarcode">The barcode of the level</param>
        /// <param name="loadLevelBarcode">The barcode of the loading scene</param>
        /// <param name="fadeFast">When true, loads with a faster fade</param>
        public static void FadeLoadLevel(string levelBarcode, string loadLevelBarcode, bool fastFade = false)
        {
            MelonCoroutines.Start(FadeIntoLevel(new Barcode(levelBarcode), new Barcode(loadLevelBarcode), fastFade));
        }

        private static IEnumerator FadeIntoLevel(Barcode level, Barcode loadScene, bool fastFade = false)
        {
            if(fadeFast)
            {
                SpawnCrate(CommonBarcodes.Misc.LoadFadeFast, Vector3.zero);
                yield return new WaitForSeconds(1); // Need to ensure timing is correct, might be 0.5
            }
            else
            {
                SpawnCrate(CommonBarcodes.Misc.LoadFade, Vector3.zero);
                yield return new WaitForSeconds(2);
            }
            LoadLevel(level, loadScene);
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
    }
}
