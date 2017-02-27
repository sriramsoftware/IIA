
# [IridiumIon Analytics](#)

An analytics service, formerly KQAnalytics3. Under redesign for integration into the [AlphaOsmium](https://iridiumion.xyz/#/projects/alphaosmium) platform.

## About

IridiumIon Analytics provides a simple and open analytics solution for your internet service.
It can be used everywhere from webpages to desktop apps to mobile apps to gain valuable analytics data.

## Install

### From Source

1. Install latest .NET Core tooling (SDK >1.1)
1. Clone the sources: `git clone --recursive https://github.com/0xFireball/KQAnalytics3.git`
1. `./build/build.sh` Use the included build/test script (requires `dotnet`). This
  script is used in automated CI builds and will also run tests and publish portable release binaries
  to a `publish/` subdirectory in the output path.
1. `dotnet KQAnalytics3.dll` to run the portable application.

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
  an client API allowing creators to integrate KQAnalytics3 into the application
- Cross-platform service, deploy to cloud, Docker, and more
  - Built on the .NET Core platform
- (WIP) Plugin support for dynamically loading custom code

## Licensing (Community Edition)

Copyright &copy; 2016 0xFireball, IridiumIon Software. All Rights Reserved.  
Licensed under the AGPLv3.
