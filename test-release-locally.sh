#!/bin/bash
# Local test script to verify the release build process

set -e

echo "=== Testing Release Build Process Locally ==="
echo

# Save current version
ORIGINAL_VERSION=$(grep -oP '(?<=<VersionPrefix>)[^<]+' Directory.Build.props)
echo "Current version: $ORIGINAL_VERSION"

# Test version
TEST_VERSION="9.9.9"
echo "Test version: $TEST_VERSION"
echo

# Create backup
cp Directory.Build.props Directory.Build.props.backup

# Update version
echo "Updating version to $TEST_VERSION..."
sed -i "s/<VersionPrefix>[^<]*<\/VersionPrefix>/<VersionPrefix>$TEST_VERSION<\/VersionPrefix>/" Directory.Build.props

# Run build script
echo "Running build script..."
export nextRelease_version="$TEST_VERSION"
bash .github/scripts/build-release.sh

# Check version in built files
echo
echo "Checking version in built EXE files..."
FOUND_VERSION=false
for exe in build/*/win-x64/RemMeter.exe; do
    if [ -f "$exe" ]; then
        echo -n "Checking $exe... "
        if strings "$exe" 2>/dev/null | grep -q "$TEST_VERSION"; then
            echo "✅ Found version $TEST_VERSION"
            FOUND_VERSION=true
        else
            echo "❌ Version $TEST_VERSION NOT found"
        fi
    fi
done

# Restore original version
echo
echo "Restoring original version..."
mv Directory.Build.props.backup Directory.Build.props

# Clean up
echo "Cleaning up build artifacts..."
rm -rf build/

# Report result
echo
if [ "$FOUND_VERSION" = true ]; then
    echo "✅ TEST PASSED: Version was correctly embedded in EXE files"
    exit 0
else
    echo "❌ TEST FAILED: Version was not found in EXE files"
    exit 1
fi