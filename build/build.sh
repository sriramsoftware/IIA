#!/usr/bin/env bash

#exit if any command fails
#set -e

artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then  
  rm -R $artifactsFolder
fi

# Restore packages
dotnet restore --configfile ./build/NuGet.config

# Change to project dir
cd KQAnalytics3/src/KQAnalytics3

# Build
dotnet build -c Release

# Publish
dotnet publish -c Release