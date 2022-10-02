using MelonLoader;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;

namespace BoneLibUpdater
{
    internal static class Updater
    {
        //private static readonly string releaseApi = "https://api.github.com/repos/yowchap/BoneLib/releases";
        private static readonly string dataDir = Path.Combine(MelonUtils.UserDataDirectory, "BoneLibUpdater");
        private static readonly string modUpdaterScriptName = "modupdater.ps1";
        private static readonly string pluginUpdaterScriptName = "pluginupdater.ps1";

        private static bool pluginNeedsUpdating = false;


        public static void UpdateMod()
        {
            // Check for local version of mod and read version if it exists
            Version localVersion = new Version(0, 0, 0);
            if (File.Exists(Main.boneLibAssemblyPath))
            {
                AssemblyName localAssemblyInfo = AssemblyName.GetAssemblyName(Main.boneLibAssemblyPath);
                localVersion = new Version(localAssemblyInfo.Version.Major, localAssemblyInfo.Version.Minor, localAssemblyInfo.Version.Build); // Remaking the object so there's no 4th number
                Main.Logger.Msg($"BoneLib.dll found in Mods folder. Version: {localVersion}");
            }

            try
            {
                Directory.CreateDirectory(dataDir);
                string updaterScriptPath = Path.Combine(dataDir, modUpdaterScriptName);

                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = assembly.GetManifestResourceNames().First(x => x.Contains(modUpdaterScriptName));
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (FileStream fileStream = File.Create(updaterScriptPath))
                        stream.CopyTo(fileStream);
                }

                // Thanks trev
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.Arguments = $"-file \"{updaterScriptPath}\" {localVersion} \"{MelonUtils.GameDirectory}\"";

                process.Start();
                string returnVal = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                Main.Logger.Msg(returnVal);

                pluginNeedsUpdating = returnVal.Contains("Downloaded");
            }
            catch (Exception e)
            {
                Main.Logger.Error("Error while running BoneLib updater");
                Main.Logger.Error(e.ToString());
            }
        }

        public static void UpdatePlugin()
        {
            if (pluginNeedsUpdating)
            {
                Directory.CreateDirectory(dataDir);
                string updaterScriptPath = Path.Combine(dataDir, pluginUpdaterScriptName);

                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = assembly.GetManifestResourceNames().First(x => x.Contains(pluginUpdaterScriptName));
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (FileStream fileStream = File.Create(updaterScriptPath))
                        stream.CopyTo(fileStream);
                }

                // Thanks trev
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.FileName = "powershell.exe";
                process.StartInfo.Arguments = $"-file \"{updaterScriptPath}\" \"{MelonUtils.GameDirectory}\"";

                process.Start();
            }
        }
    }
}
