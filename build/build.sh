#!/usr/bin/env bash

#exit if any command fails
#set -e

artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then  
  rm -R $artifactsFolder
fi

# Dotnet info

dotnet --version

echo "Restoring packages..."
# Restore packages
dotnet restore --configfile ./build/NuGet.config

echo "Building project..."

# Build
dotnet build -c Release KQAnalytics3/src/KQAnalytics3

echo "Running tests..."

# Run tests
dotnet test KQAnalytics3/test/KQAnalytics3.Tests

echo "Publishing project..."

# Publish
dotnet publish -c Release KQAnalytics3/src/KQAnalytics3