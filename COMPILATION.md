# BoneLib Compilation Instructions

## Setting the BONELAB_DIR environment variable

The "BONELAB_DIR" environment variable needs to be set to your Bonelab directory to compile BoneLib.
You can either set it manually through the Windows control panel or through Powershell with the command below.

```
[System.Environment]::SetEnvironmentVariable('BONELAB_DIR', 'C:\Program Files (x86)\Steam\steamapps\common\BONELAB', 'User')
```
Substitute the path in the command with wherever Bonelab is installed on your computer if it doesn't match the example.

## Compiling

There's 2 main ways to compile BoneLib, you can either compile it in Visual Studio or use Dotnet Build. Both methods will automatically copy the compiled files to your Bonelab directory.

### Visual Studio method
Launch Visual Studio and clone the BoneLib repository (https://github.com/yowchap/BoneLib.git) \
On the menu bar, select Build --> Build Solution


### Dotnet Build method

```
git clone https://github.com/yowchap/BoneLib.git
cd Bonelib/Bonelib
dotnet build
```
