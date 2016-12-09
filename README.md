
# KQ Analytics 3

KQ Analytics version 3.x

## About

KQ Analytics provides a simple and open analytics solution for your internet service.
KQ can be used everywhere from webpages to desktop apps to mobile apps to gain valuable analytics data.

## Install

For now, while KQ is in an early development phase, you will have to build
from source.

Eventually, you can get binary releases [here](https://github.com/0xFireball/KQAnalytics3/releases).

1. Install .NET Core tooling
1. `git clone --recursive https://github.com/0xFireball/KQAnalytics3.git`
1. `dotnet restore` to get dependencies (you may need ImageSharp NuGet feed)
1. `dotnet publish -c Release` to build portable binaries
1. `dotnet KQAnalytics3.dll` to run the portable application. 

## Features

- Rich data logging
- Data tagging and indexing
- Comprehensive configuration options
  - Blacklist and Whitelist
  - CORS configuration for external hosting
- High performance NoSQL database backend
- REST API for retreiving data

## Rearchitecturing from v2

[KQ Analytics 2](https://github.com/exaphaser/KQAnalytics)
was built on PHP.
KQ Analytics 3 is being rebuilt from scratch as a partial port of v2.0, but will be designed in a more extensible way.
It will have its own standalone server which should be reverse proxied to the outside world. As a result:

- Installation will be slightly more complex (it's no longer a simple bunch of PHP scripts)
- KQ will be **much** more customizable
- Overall security will be better (as a result of using a standalone application)

## Licensing

Copyright &copy; 2016 0xFireball, IridiumIon Software. All Rights Reserved.  
Licensed under the AGPLv3.
