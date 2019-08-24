# MobileDetector.Core

[![Coverage Status](https://coveralls.io/repos/github/breekmd/MobileDetector.Core/badge.svg?branch=)](https://coveralls.io/github/breekmd/MobileDetector.Core?branch=)

Asp.Net Core package to detect whether requests are coming from a mobile platform and which platform is that (using the User-Agent header from the request)

Mostly based on http://detectmobilebrowsers.com/ 

Nuget package: https://www.nuget.org/packages/MobileDetector.Core

To make this available for injection add the following to ConfigureServices method in Startup.cs

```
services.AddMobileDetector();
```

The service has two methods:

```
bool IsMobile();
MobilePlatform? GetPlatform();
```

MobilePlatform enum has a few common platforms explicitly enumarated, however if it detects a mobile platform but can't parse the enum it will return ```MobilePlatform.Unmapped```
