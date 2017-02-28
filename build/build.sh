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
dotnet build -c Release IridiumIon.Analytics/src/IridiumIon.Analytics

echo "Running tests..."

# Run tests
dotnet test IridiumIon.Analytics/test/IridiumIon.Analytics.Tests

echo "Publishing project..."

# Publish
dotnet publish -c Release IridiumIon.Analytics/src/IridiumIon.Analytics