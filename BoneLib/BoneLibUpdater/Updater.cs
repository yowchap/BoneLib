using MelonLoader;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
        private static readonly string updaterAppName = "updater.exe";

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
                string updaterScriptPath = Path.Combine(dataDir, updaterAppName);

                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = assembly.GetManifestResourceNames().First(x => x.Contains(updaterAppName));
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (FileStream fileStream = File.Create(updaterScriptPath))
                        stream.CopyTo(fileStream);
                }

                Process process = new Process();
                process.StartInfo.FileName = updaterScriptPath;
                process.StartInfo.Arguments = $"{localVersion} \"{Main.boneLibAssemblyPath}\" \"{Main.boneLibUpdaterAssemblyPath}\" \"false\"";
                process.Start();
                process.WaitForExit();
                ExitCode code = (ExitCode)process.ExitCode;
                
                switch(code)
                {
                    case ExitCode.Success:
                        Main.Logger.Msg("BoneLib.dll updated successfully!");
                        pluginNeedsUpdating = true;
                        break;
                    case ExitCode.UpToDate:
                        Main.Logger.Msg("BoneLib.dll is already up to date");
                        break;
                    case ExitCode.Error:
                        Main.Logger.Error("BoneLib.dll failed to update");
                        break;
                }
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
                string updaterScriptPath = Path.Combine(dataDir, updaterAppName);

                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = assembly.GetManifestResourceNames().First(x => x.Contains(updaterAppName));
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (FileStream fileStream = File.Create(updaterScriptPath))
                        stream.CopyTo(fileStream);
                }

                Process process = new Process();
                process.StartInfo.FileName = updaterScriptPath;
                process.StartInfo.Arguments = $"{new Version(0, 0, 0)} \"{Main.boneLibAssemblyPath}\" \"{Main.boneLibUpdaterAssemblyPath}\" true";
                process.Start();
            }
        }
    }

    enum ExitCode
    {
        Success = 0,
        UpToDate = 1,
        Error = 2
    }
}
