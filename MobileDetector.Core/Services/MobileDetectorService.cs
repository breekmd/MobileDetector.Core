using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using MobileDetector.Core.Enums;

namespace MobileDetector.Core.Services
{
    public class MobileDetectorService : IMobileDetectorService
    {
        private readonly Lazy<MobilePlatform?> _platform;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly Dictionary<string, MobilePlatform> _platformNames = new Dictionary<string, MobilePlatform>
        {
            { "palm os", MobilePlatform.PalmOs },
            { "windows ce", MobilePlatform.WindowsCe}
        };

        public MobileDetectorService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            _platform = new Lazy<MobilePlatform?>(() => { return Get(); });
        }

        public bool IsMobile()
        {
            return _platform.Value != null;
        }

        public MobilePlatform? GetPlatform()
        {
            return _platform.Value;
        }

        private MobilePlatform? Get()
        {
            string userAgent = _httpContextAccessor.HttpContext.Request.Headers["User-Agent"];

            if (string.IsNullOrEmpty(userAgent))
            {
                return null;
            }

            Regex nonMobilePartRegex = new Regex(@"android|bb\d+|meego", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex platformRegex = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino",
                RegexOptions.IgnoreCase | RegexOptions.Multiline);

            Match platformMatch = platformRegex.Match(userAgent);

            if (platformMatch.Success)
            {
                string platformString = platformMatch.Value;

                Match nonMobilePartPlatformMatch = nonMobilePartRegex.Match(platformString);

                Console.WriteLine(platformString);

                if (nonMobilePartPlatformMatch.Success)
                {
                    string nonMobilePartPlatformValue = nonMobilePartPlatformMatch.Value;

                    if (Enum.TryParse<MobilePlatform>(nonMobilePartPlatformValue, true, out var nonMobilePartPlatform))
                    {
                        return nonMobilePartPlatform;
                    }
                }

                if (Enum.TryParse<MobilePlatform>(platformString, true, out var platform))
                {
                    return platform;
                }

                if (_platformNames.TryGetValue(platformString.ToLower().Trim(), out var mappedPlatform))
                {
                    return mappedPlatform;
                }

                return MobilePlatform.Unmapped;
            }

            return null;
        }

    }
}
