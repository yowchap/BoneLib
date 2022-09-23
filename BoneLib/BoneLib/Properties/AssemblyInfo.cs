using MelonLoader;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle(BoneLib.BuildInfo.Name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(BoneLib.BuildInfo.Company)]
[assembly: AssemblyProduct(BoneLib.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + BoneLib.BuildInfo.Author)]
[assembly: AssemblyTrademark(BoneLib.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
//[assembly: Guid("")]
[assembly: AssemblyVersion(BoneLib.BuildInfo.Version)]
[assembly: AssemblyFileVersion(BoneLib.BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonInfo(typeof(BoneLib.Main), BoneLib.BuildInfo.Name, BoneLib.BuildInfo.Version, BoneLib.BuildInfo.Author, BoneLib.BuildInfo.DownloadLink)]


// Create and Setup a MelonModGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonModGameAttribute is found or any of the Values for any MelonModGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonModGame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("Stress Level Zero", "BONEWORKS")]