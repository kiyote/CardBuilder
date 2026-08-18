#!/usr/bin/env pwsh
# Publishes self-contained, single-file executables for each supported platform.
# Output: artifacts/publish/<rid>/CardBuilder(.exe)

param(
	[string[]] $Rids = @('win-x64', 'linux-x64'),
	[string] $Configuration = 'Release'
)

$ErrorActionPreference = 'Stop'
$project = Join-Path $PSScriptRoot 'src/CardBuilder/Client/CardBuilder.Client/CardBuilder.Client.csproj'

foreach ($rid in $Rids) {
	$output = Join-Path $PSScriptRoot "artifacts/publish/$rid"
	Write-Host "Publishing $rid -> $output" -ForegroundColor Cyan
	dotnet publish $project -c $Configuration -r $rid -o $output `
		--self-contained true `
		-p:PublishSingleFile=true `
		-p:IncludeNativeLibrariesForSelfExtract=true
	if ($LASTEXITCODE -ne 0) { throw "Publish failed for $rid" }
}
