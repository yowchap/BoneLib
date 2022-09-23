using MelonLoader;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle(BoneLibLoader.BuildInfo.Name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(BoneLibLoader.BuildInfo.Company)]
[assembly: AssemblyProduct(BoneLibLoader.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + BoneLibLoader.BuildInfo.Author)]
[assembly: AssemblyTrademark(BoneLibLoader.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
//[assembly: Guid("")]
[assembly: AssemblyVersion(BoneLibLoader.BuildInfo.Version)]
[assembly: AssemblyFileVersion(BoneLibLoader.BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonInfo(typeof(BoneLibLoader.Main), BoneLibLoader.BuildInfo.Name, BoneLibLoader.BuildInfo.Version, BoneLibLoader.BuildInfo.Author, BoneLibLoader.BuildInfo.DownloadLink)]


// Create and Setup a MelonModGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonModGameAttribute is found or any of the Values for any MelonModGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonModGame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("Stress Level Zero", "BONELAB")]