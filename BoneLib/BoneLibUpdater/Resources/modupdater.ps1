$OriginalProgressPreference = $Global:ProgressPreference
$Global:ProgressPreference = 'SilentlyContinue'

$repo = "yowchap/BoneLib"
$modFile = "BoneLib.dll"

$releases = "https://api.github.com/repos/$repo/releases"

$currentVersion = $args[0]
$gameDir = $args[1]

Write-Host "Checking GitHub for latest release..."

try
{
	$latestTag = (Invoke-WebRequest $releases -UseBasicParsing | ConvertFrom-Json)[0].tag_name
	$latestVersion = $latestTag -replace 'v',''
	$isOutdated = ([System.Version]$latestVersion -gt [System.Version]$currentVersion)

	if ($isOutdated)
	{
		Write-Host "BoneLib is outdated. Updating..."
		
		$download = "https://github.com/$repo/releases/download/$latestTag/$modFile"
		$path = [IO.Path]::Combine($gameDir, "Mods", $modFile)
		Invoke-WebRequest $download -Out $path
		
		Write-Host "Downloaded latest version"
	}
	else
	{
		Write-Host "BoneLib is up to date"
	}
}
catch [System.Exception]
{
	Write-Host "An error occurred, BoneLib was not updated"
	Write-Host $error
}
finally
{
	$Global:ProgressPreference = $OriginalProgressPreference
}