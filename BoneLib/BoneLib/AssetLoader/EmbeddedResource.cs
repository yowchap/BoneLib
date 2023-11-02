using System.Reflection;

namespace BoneLib.AssetLoader;

public class EmbeddedResource
{
    public static byte[] GetResourceBytes(string name)
    {
        var assembly = Assembly.GetExecutingAssembly();
        foreach (var resource in assembly.GetManifestResourceNames())
        {
            if (resource.Contains(name))
            {
                using (var resFilestream = assembly.GetManifestResourceStream(resource))
                {
                    if (resFilestream == null) return null;
                    var ba = new byte[resFilestream.Length];
                    resFilestream.Read(ba, 0, ba.Length);
                    return ba;
                }
            }
        }
        return null;
    }
}