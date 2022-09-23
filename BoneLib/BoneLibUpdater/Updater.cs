using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;

namespace BoneLibUpdater
{
    internal static class Updater
    {
        private static readonly string releaseApi = "https://api.github.com/repos/yowchap/BoneLib/releases";

        public static void UpdateMod()
        {
            Version localVersion = new Version(0, 0, 0);
            if (File.Exists(Main.boneLibAssemblyPath))
            {
                AssemblyName localAssemblyInfo = AssemblyName.GetAssemblyName(Main.boneLibAssemblyPath);
                localVersion = new Version(localAssemblyInfo.Version.Major, localAssemblyInfo.Version.Minor, localAssemblyInfo.Version.Build); // Remaking the object so there's no 4th number
                Main.Logger.Msg($"BoneLib.dll found in Mods folder. Version: {localVersion}");
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(releaseApi);
                    request.Accept = "application/vnd.github.v3.raw";
                    request.UserAgent = "BoneLibUpdater";
                    WebResponse response = request.GetResponse();
                    Encoding encoding = Encoding.UTF8;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                    {
                        string fileContent = reader.ReadToEnd();
                        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                        dynamic releases = jsonSerializer.Deserialize<dynamic>(fileContent);

                        Version latestVersion = new Version(0, 0, 0);
                        dynamic latestRelease = null;
                        foreach (var release in releases)
                        {
                            Version version = new Version(((string)release["tag_name"]).Replace("v", ""));
                            if (version >= latestVersion)
                            {
                                latestVersion = version;
                                latestRelease = release;
                            }
                        }

                        Main.Logger.Msg($"Latest version of BoneLib is {latestVersion}");

                        if (latestVersion > localVersion)
                        {
                            Main.Logger.Msg("Downloading latest version...");
                            foreach (var asset in latestRelease["assets"])
                            {
                                if (asset["name"] == "BoneLib.dll")
                                {
                                    string downloadUrl = asset["browser_download_url"];
                                    using (HttpClient downloadClient = new HttpClient())
                                    {
                                        HttpWebRequest downloadRequest = (HttpWebRequest)WebRequest.Create(downloadUrl);
                                        downloadRequest.Accept = "application/vnd.github.v3.raw";
                                        downloadRequest.UserAgent = "BoneLibUpdater";
                                        WebResponse downloadResponse = downloadRequest.GetResponse();
                                        using (Stream downloadStream = downloadResponse.GetResponseStream())
                                        {
                                            using (FileStream fileStream = new FileStream(Main.boneLibAssemblyPath, FileMode.Create, FileAccess.Write))
                                            {
                                                downloadStream.CopyTo(fileStream);
                                                Main.Logger.Msg("Download complete");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Main.Logger.Msg("Local version is up to date");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Main.Logger.Error("Error while running BoneLib updater");
                Main.Logger.Error(e.ToString());
            }
        }
    }
}
