param(
    [int]$major,
    [int]$minor,
    [int]$patch,
    [string]$tag,
    [string]$refName
)

$version = Get-Content "$PSScriptRoot\..\.version"
if (!$major) {
    $major = $version.Split(".")[0]
}
if (!$minor) {
    $minor = $version.Split(".")[1]
}
if (!$patch) {
    $patch = $version.Split(".")[2]
}

if ($patch -eq "*") {
    $version = Get-Content "./.github/workflows/.version"
    git remote set-url origin https://$($env:GB_NUGET_TOKEN)@github.com/$($env:GITHUB_REPOSITORY).git
    $tag = git ls-remote --tags | Select-String -Pattern refs/tags/v$version$

    $remote_refs = git ls-remote
    $tags = $remote_refs | Where-Object { $_.ToLower().Contains('refs/tags/v1.0.') }
    $latest_tag = $tags | Sort-Object -Property @{ Expression = { [version]::new($_.Split('/v', [StringSplitOptions]::RemoveEmptyEntries)[1]) } } -Descending | Select-Object -First 1
    $latest_patch = $latest_tag.Split(".")[-1]
    if ($tag -eq "dev") {
        $patch = $latest_patch
    }else {
        $patch = [int]$latest_patch + 1
    }
}

$ProductMajorVersion = $major
$ProductMinorVersion = $minor
$ProductpatchVersion = $patch
$MyCustomBuildVersion = "$ProductMajorVersion.$ProductMinorVersion.$ProductpatchVersion"

if ($refName) {
    $tag = $refName.Replace("_","-")
}
if ($tag -and $tag -ne "master") {
    $BaselineYear = 2023
    $CurrentDate = (Get-Date).ToUniversalTime()
    $StartOfDay = Get-Date -Date $CurrentDate -Hour 0 -Minute 00 -Second 00
    $BuildMajorVersion = ($CurrentDate.Year - $BaselineYear) * 12 + $CurrentDate.Month
    $BuildMajorVersion = $BuildMajorVersion * 31 + $CurrentDate.Day
    $BuildMinorVersion = [math]::floor(((New-TimeSpan -Start $StartOfDay -End $CurrentDate).TotalSeconds) / 2)
    $MyCustomBuildVersion = "$MyCustomBuildVersion-$tag.$BuildMajorVersion.$BuildMinorVersion"
}
$env:BUILD_BUILDNUMBER = $MyCustomBuildVersion
[Environment]::SetEnvironmentVariable("BUILD_BUILDNUMBER", $MyCustomBuildVersion, "Machine")
echo "BUILD_BUILDNUMBER=$MyCustomBuildVersion" >> $env:GITHUB_OUTPUT
echo "BUILD_BUILDNUMBER=$MyCustomBuildVersion" >> $env:GITHUB_ENV
Write-Host "Setting the value of current build version :  $MyCustomBuildVersion"

dir env: