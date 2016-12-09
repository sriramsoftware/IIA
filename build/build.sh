#!/usr/bin/env bash

#exit if any command fails
set -e

artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then  
  rm -R $artifactsFolder
fi

# Restore packages
dotnet restore --configfile ./build/NuGet.config

# Build
dotnet build -c Release KQAnalytics3/src/KQAnalytics3

# Publish
dotnet publish -c Release KQAnalytics3/src/KQAnalytics3