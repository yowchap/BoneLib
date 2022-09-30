using MelonLoader;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle(BoneLibUpdater.BuildInfo.Name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(BoneLibUpdater.BuildInfo.Company)]
[assembly: AssemblyProduct(BoneLibUpdater.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + BoneLibUpdater.BuildInfo.Author)]
[assembly: AssemblyTrademark(BoneLibUpdater.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
//[assembly: Guid("")]
[assembly: AssemblyVersion(BoneLibUpdater.BuildInfo.Version)]
[assembly: AssemblyFileVersion(BoneLibUpdater.BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonInfo(typeof(BoneLibUpdater.Main), BoneLibUpdater.BuildInfo.Name, BoneLibUpdater.BuildInfo.Version, BoneLibUpdater.BuildInfo.Author, BoneLibUpdater.BuildInfo.DownloadLink)]


// Create and Setup a MelonModGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonModGameAttribute is found or any of the Values for any MelonModGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonModGame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("Stress Level Zero", "BONELAB")]