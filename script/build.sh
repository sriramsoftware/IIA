#!/usr/bin/env bash

PROJECT_NAME="IridiumIon.Analytics"
SOLUTION="src/$PROJECT_NAME.sln"
DEPLOY_PROJECT="src/$PROJECT_NAME"

ARTIFACTS_FOLDER="./artifacts"

if [ -d $ARTIFACTS_FOLDER ]; then  
  rm -R $ARTIFACTS_FOLDER
fi

# dotnet CLI info

dotnet --version

echo "Restoring packages for solution..."
# Restore packages
dotnet restore $SOLUTION --configfile src/NuGet.config

echo "Building $PROJECT_NAME..."

# Build
dotnet build -c Release $DEPLOY_PROJECT

echo "Running tests..."

echo "Publishing $PROJECT_NAME..."

# Publish
dotnet publish -c Release $DEPLOY_PROJECT
