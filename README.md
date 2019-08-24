# MobileDetector.Core

Asp.Net Core package to detect whether requests are coming from a mobile platform and which platform is that (using the User-Agent header from the request)

Mostly based on http://detectmobilebrowsers.com/ 

Nuget package: https://www.nuget.org/packages/MobileDetector.Core/1.0.0

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
