using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace BoneLib.AssetLoader;

public static class EmbeddedBundle
{
    private static byte[] resource;
    private static AssetBundle bundle;
    public static AssetBundle LoadFromAssembly(Assembly assembly, string name)
    {
        string[] manifestResources = assembly.GetManifestResourceNames();
                
        if (manifestResources.Contains(name))
        {
            ModConsole.Msg($"Loading embedded resource data {name}...", LoggingMode.DEBUG);
            using (Stream str = assembly.GetManifestResourceStream(name)) 
            using (MemoryStream memoryStream = new MemoryStream()) 
            {
                str.CopyTo(memoryStream); 
                ModConsole.Msg("Done!", LoggingMode.DEBUG); 
                resource = memoryStream.ToArray();
            }
            ModConsole.Msg($"Loading assetBundle from data {name}, please be patient...", LoggingMode.DEBUG);
            bundle = AssetBundle.LoadFromMemory(resource);
            ModConsole.Msg("Done!", LoggingMode.DEBUG);
        }
        return bundle;
    }
    
    public static T LoadPersistentAsset<T>(this AssetBundle assetBundle, string name) where T : Object {
        var asset = assetBundle.LoadAsset(name);

        if (asset != null) {
            asset.hideFlags = HideFlags.DontUnloadUnusedAsset;
            return asset.TryCast<T>();
        }

        return null;
    }
}