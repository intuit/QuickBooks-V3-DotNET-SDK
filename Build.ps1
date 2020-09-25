#!/usr/bin/env pwsh
$configuration = "Release"
$BuildId = "0"
$IsBuildEnv = $false
$CommitSha = 'LOCAL'
$Env:DOTNET_CLI_TELEMETRY_OPTOUT = 'true'

$originalDir = Get-Location

try {
    if (Test-Path Env:BUILD_BUILDID)
    {
        $BuildId = $Env:BUILD_BUILDID
        $IsBuildEnv = $true
    }

    if (Test-Path Env:GITHUB_RUN_NUMBER)
    {
        $BuildId = $Env:GITHUB_RUN_NUMBER
        $IsBuildEnv = $true
    }

    Remove-Item -Recurse -Force artifacts -ErrorAction SilentlyContinue
    Remove-Item -Recurse -Force tools -ErrorAction SilentlyContinue
    Get-ChildItem .\ -include bin,obj -Recurse | ForEach-Object ($_) { Remove-Item $_.fullname -Force -Recurse }

    dotnet tool restore
    dotnet dotnet-gitversion /nofetch

    $gitVersion = (dotnet dotnet-gitversion /nofetch) | Out-String | ConvertFrom-Json -AsHashtable

    if($IsBuildEnv)
    {
        $CommitSha = $gitVersion["ShortSha"]
    }

    $Version = "$($gitVersion["SemVer"]).$BuildId+$CommitSha"
    $Env:version = $Version

    Write-Output "Build version: $Version"
    Write-Output "##vso[build.updatebuildnumber]$Version"

    Remove-Item -Recurse -Force artifacts -ErrorAction SilentlyContinue
    Remove-Item -Recurse -Force tools -ErrorAction SilentlyContinue
    Get-ChildItem .\ -include bin,obj -Recurse | ForEach-Object ($_) { Remove-Item $_.fullname -Force -Recurse }

    Set-Location "IPPDotNetDevKitCSV3/Code"

    dotnet tool restore

    $numOfCsprojFiles = Get-ChildItem -Include *.sln -Recurse | Measure-Object

    if ($numOfCsprojFiles.Count -gt 0)
    {
        dotnet restore --locked-mode
        dotnet build --configuration $configuration --no-restore
        # dotnet test --logger "trx;LogFileName=../../../artifacts/test-results.trx"
        # dotnet publish --configuration $configuration # --no-build
        dotnet pack --configuration $configuration --output "../artifacts/nupkgs" # --no-build
    }
}
finally {
    Set-Location $originalDir 
}