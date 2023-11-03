using System.IO;
using System.Reflection;

namespace BoneLib.AssetLoader
{
    public class EmbeddedResource
    {
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
    }
}