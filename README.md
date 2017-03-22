
# IridiumIon Analytics

An analytics service, formerly KQAnalytics3. Under redesign for integration into the [AlphaOsmium](https://iridiumion.xyz/#/projects/alphaosmium) platform.

[![Build Status](https://travis-ci.org/0xFireball/IIA.svg?branch=master)](https://travis-ci.org/0xFireball/IIA)

## About

IridiumIon Analytics provides a simple and open analytics solution for your internet service.
It can be used everywhere from webpages to desktop apps to mobile apps to gain valuable analytics data.

## Install

### From Source

1. Install latest .NET Core tooling (SDK >1.1)
1. Clone the sources: `git clone --recursive https://github.com/0xFireball/IridiumIon.Analytics.git`
1. `./build/build.sh` Use the included build/test script (requires `dotnet`). This
  script is used in automated CI builds and will also run tests and publish portable release binaries
  to a `publish/` subdirectory in the output path.
1. `dotnet IridiumIon.Analytics.dll` to run the portable application.

## Features

- Rich, automated data logging
- Data tagging and indexing for fast retrieval
  - Tags allow filtering event types during analysis
  - Ability to store arbitrary custom data specific to your application
- Comprehensive configuration options
  - Blacklist and Whitelist
  - CORS configuration for external hosting
    - Block unwanted origins from triggering events
    - Protect against spam data
- High performance NoSQL database backend
  - Powered by LiteDB, which is highly optimized
  - In-memory cache for faster responses and processing
- Powerful, fully-featured REST API for retreiving data
  - Set custom API keys, and use them to retrieve collected data
  - API keys can have permission scopes and granular permissions,
    allowing you to build clients that have varying levels of access to data
  - Filter stored data and events by tag and custom fields
  - Versatile analysis tools that operate on dynamically retrieved data
  can be built on the API
- Easy integration with existing webpages and applications with client libraries
  - Client side web library automatically sets up basic events, and provides
  an client API allowing creators to integrate `IridiumIon.Analytics` into the application
- Cross-platform service, deploy to cloud, Docker, and more
  - Built on the .NET Core platform
- (WIP) Plugin support for dynamically loading custom code

## Editions

`IridiumIon.Analytics` comes in several editions for different purposes.

- **`IridiumIon.Analytics` standalone community edition** - this project
- **`IridiumIon.Analytics` integrated AlphaOsmium edition** - will be available exclusively as a component of the [AlphaOsmium platform](https://iridiumion.xyz/#/projects/alphaosmium). It will be largely identical to the community edition except for changes needed to integrate into the larger project.

## Licensing (Standalone Community Edition)

Copyright &copy; 2016-2017 Nihal Talur (0xFireball), IridiumIon Software. All Rights Reserved.

Licensed under the AGPLv3.
