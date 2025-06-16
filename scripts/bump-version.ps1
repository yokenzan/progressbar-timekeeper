# Script to bump version in Directory.Build.props
# Usage: .\bump-version.ps1 [major|minor|patch]

param(
    [Parameter(Position = 0)]
    [ValidateSet("major", "minor", "patch")]
    [string]$VersionType = "patch"
)

$ErrorActionPreference = "Stop"

# Get current version from Directory.Build.props
$propsPath = Join-Path $PSScriptRoot "..\Directory.Build.props"
$content = Get-Content $propsPath -Raw

if ($content -match '<VersionPrefix>([^<]+)</VersionPrefix>') {
    $currentVersion = $matches[1]
} else {
    Write-Error "Could not find current version in Directory.Build.props"
    exit 1
}

Write-Host "Current version: $currentVersion" -ForegroundColor Cyan

# Split version into components
$versionParts = $currentVersion.Split('.')
$major = [int]$versionParts[0]
$minor = [int]$versionParts[1]
$patch = [int]$versionParts[2]

# Increment based on version type
switch ($VersionType) {
    "major" {
        $major++
        $minor = 0
        $patch = 0
    }
    "minor" {
        $minor++
        $patch = 0
    }
    "patch" {
        $patch++
    }
}

$newVersion = "$major.$minor.$patch"
Write-Host "New version: $newVersion" -ForegroundColor Green

# Update Directory.Build.props
$content = $content -replace '<VersionPrefix>[^<]+</VersionPrefix>', "<VersionPrefix>$newVersion</VersionPrefix>"
Set-Content -Path $propsPath -Value $content -NoNewline

Write-Host "`nVersion updated successfully!" -ForegroundColor Green
Write-Host "`nNext steps:" -ForegroundColor Yellow
Write-Host "1. Review the changes: git diff"
Write-Host "2. Commit the change: git commit -am `"chore: bump version to $newVersion`""
Write-Host "3. Create a tag: git tag v$newVersion"
Write-Host "4. Push changes: git push && git push --tags"