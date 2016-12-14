#!/usr/bin/env bash

#exit if any command fails
#set -e

artifactsFolder="./artifacts"

if [ -d $artifactsFolder ]; then  
  rm -R $artifactsFolder
fi

# Dotnet info

dotnet --version

# Enter project

cd KQAnalytics3

# Restore packages
dotnet restore --configfile ../build/NuGet.config

# Build
dotnet build -c Release KQAnalytics3/KQAnalytics3.sln

# Publish
dotnet publish -c Release KQAnalytics3/KQAnalytics3.sln

# Return

cd ..