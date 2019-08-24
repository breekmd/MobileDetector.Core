using System;
using MobileDetector.Core.Enums;

namespace MobileDetector.Core.Services
{
    public interface IMobileDetectorService
    {
        bool IsMobile();
        MobilePlatform? GetPlatform();
    }
}
