$OriginalProgressPreference = $Global:ProgressPreference
$Global:ProgressPreference = 'SilentlyContinue'

$repo = "yowchap/BoneLib"
$pluginFile = "BoneLibUpdater.dll"

$releases = "https://api.github.com/repos/$repo/releases"

$gameDir = $args[0]

# https://stackoverflow.com/questions/49120179/run-a-powershell-script-that-monitors-a-file-thats-lock-but-once-unlocked-runs
function checkLock {
    Param(
        [parameter(Mandatory=$true)]
        $filename
    )
    $file = Get-Item (Resolve-Path $filename) -Force
    if ($file -is [IO.FileInfo]) {
        trap {
            return $true
            continue
        }
        $stream = New-Object system.IO.StreamReader $file
        if ($stream) {$stream.Close()}
    }
    return $false
}

Write-Host "Updating $pluginFile"

try
{
	While ($True) {
		if ((checkLock $filename) -eq $true) {
			Write-Host "$pluginFile locked, waiting..."
		}
		else {
			Write-Host "Downloading $pluginFile..."
			
			$latestTag = (Invoke-WebRequest $releases -UseBasicParsing | ConvertFrom-Json)[0].tag_name
			$download = "https://github.com/$repo/releases/download/$latestTag/$pluginFile"
			$path = [IO.Path]::Combine($gameDir, "Plugins", $pluginFile)
			Invoke-WebRequest $download -Out $path
			
			break
		}
	
		start-sleep -seconds 2
	
	}
}
catch [System.Exception]
{
	Write-Host "An error occurred, BoneLibUpdater was not updated"
	Write-Host $error
}
finally
{
	$Global:ProgressPreference = $OriginalProgressPreference
}