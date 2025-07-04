#!/bin/bash
set -e

echo "Building release artifacts with version ${nextRelease.version}..."

# Restore dependencies
dotnet restore wpf/RemMeter.csproj

# Build
dotnet build wpf/RemMeter.csproj --no-restore --configuration Release

# Create build directory
mkdir -p build

# Framework-dependent builds
echo "Building framework-dependent versions..."
dotnet publish wpf/RemMeter.csproj --configuration Release --runtime win-x64 --self-contained false --output ./build/framework-dependent/win-x64 --property:PublishSingleFile=true
dotnet publish wpf/RemMeter.csproj --configuration Release --runtime win-x86 --self-contained false --output ./build/framework-dependent/win-x86 --property:PublishSingleFile=true

# Self-contained builds
echo "Building self-contained versions..."
dotnet publish wpf/RemMeter.csproj --configuration Release --runtime win-x64 --self-contained true --output ./build/self-contained/win-x64 --property:PublishSingleFile=true --property:PublishTrimmed=false
dotnet publish wpf/RemMeter.csproj --configuration Release --runtime win-x86 --self-contained true --output ./build/self-contained/win-x86 --property:PublishSingleFile=true --property:PublishTrimmed=false

echo "Build completed successfully!"