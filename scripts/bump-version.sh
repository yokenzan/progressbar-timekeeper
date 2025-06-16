#!/bin/bash

# Script to bump version in Directory.Build.props
# Usage: ./bump-version.sh [major|minor|patch]

set -e

VERSION_TYPE=${1:-patch}

if [[ ! "$VERSION_TYPE" =~ ^(major|minor|patch)$ ]]; then
    echo "Usage: $0 [major|minor|patch]"
    echo "Default: patch"
    exit 1
fi

# Get current version from Directory.Build.props
CURRENT_VERSION=$(grep -oP '(?<=<VersionPrefix>)[^<]+' ../Directory.Build.props)

if [ -z "$CURRENT_VERSION" ]; then
    echo "Error: Could not find current version in Directory.Build.props"
    exit 1
fi

# Split version into components
IFS='.' read -r -a VERSION_PARTS <<< "$CURRENT_VERSION"
MAJOR="${VERSION_PARTS[0]}"
MINOR="${VERSION_PARTS[1]}"
PATCH="${VERSION_PARTS[2]}"

echo "Current version: $CURRENT_VERSION"

# Increment based on version type
case $VERSION_TYPE in
    major)
        MAJOR=$((MAJOR + 1))
        MINOR=0
        PATCH=0
        ;;
    minor)
        MINOR=$((MINOR + 1))
        PATCH=0
        ;;
    patch)
        PATCH=$((PATCH + 1))
        ;;
esac

NEW_VERSION="$MAJOR.$MINOR.$PATCH"
echo "New version: $NEW_VERSION"

# Update Directory.Build.props
sed -i "s/<VersionPrefix>.*<\/VersionPrefix>/<VersionPrefix>$NEW_VERSION<\/VersionPrefix>/" ../Directory.Build.props

echo "Version updated successfully!"
echo
echo "Next steps:"
echo "1. Review the changes: git diff"
echo "2. Commit the change: git commit -am \"chore: bump version to $NEW_VERSION\""
echo "3. Create a tag: git tag v$NEW_VERSION"
echo "4. Push changes: git push && git push --tags"