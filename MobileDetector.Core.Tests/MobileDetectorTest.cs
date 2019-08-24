using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MobileDetector.Core.Enums;
using MobileDetector.Core.Services;
using Moq;

namespace MobileDetector.Core.Tests
{
    [TestClass]
    public class MobileDetectorTest
    {
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private MobileDetectorService _mobileDetectorService;

        [TestInitialize]
        public void Initialise()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        }

        [TestMethod]
        public void TestIfMobileTrueForAndroid()
        {
            Setup(@"Mozilla/5.0 (Linux; U; Android 4.4.2; en-us; SCH-I535 Build/KOT49H) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30");

            Assert.IsTrue(_mobileDetectorService.IsMobile());
        }

        [TestMethod]
        public void TestIfMobileTrueForIphone()
        {
            Setup(@"Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_2 like Mac OS X) AppleWebKit/603.2.4 (KHTML, like Gecko) FxiOS/7.5b3349 Mobile/14F89 Safari/603.2.4");

            Assert.IsTrue(_mobileDetectorService.IsMobile());
        }

        [TestMethod]
        public void TestIfMobileTrueForUnmapped()
        {
            Setup(@"Mozilla/5.0 (Danger hiptop 3.4; U; AvantGo 3.2)");

            Assert.IsTrue(_mobileDetectorService.IsMobile());
        }

        [TestMethod]
        public void TestIfMobileFalseForMacos()
        {
            Setup(@"Mozilla/5.0 (Macintosh; Intel Mac OS X 10.6; rv:16.0) Gecko/20100101 Firefox/16.0");

            Assert.IsFalse(_mobileDetectorService.IsMobile());
        }

        [TestMethod]
        public void TestIfMobileFalseForWindows()
        {
            Setup(@"Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:62.0) Gecko/20100101 Firefox/62.0");

            Assert.IsFalse(_mobileDetectorService.IsMobile());
        }

        [TestMethod]
        public void TestIfMobileFalseForUbuntu()
        {
            Setup(@"Mozilla/5.0 (X11; U; Linux i686; pt-BR; rv:1.9.0.15) Gecko/2009102815 Ubuntu/9.04 (jaunty) Firefox/3.0.15");

            Assert.IsFalse(_mobileDetectorService.IsMobile());
        }

        [TestMethod]
        public void TestPlatformIphone()
        {
            Setup(@"Mozilla/5.0 (iPhone; CPU iPhone OS 10_3_1 like Mac OS X) AppleWebKit/603.1.30 (KHTML, like Gecko) Version/10.0 Mobile/14E304 Safari/602.1");

            Assert.AreEqual(MobilePlatform.iPhone, _mobileDetectorService.GetPlatform());
        }

        [TestMethod]
        public void TestPlatformAndroid ()
        {
            Setup(@"Mozilla/5.0 (Linux; Android 5.1.1; SM-N750K Build/LMY47X; ko-kr) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Mobile Safari/537.36 Puffin/6.0.8.15804AP");

            Assert.AreEqual(MobilePlatform.Android, _mobileDetectorService.GetPlatform());
        }

        [TestMethod]
        public void TestPlatformBB10()
        {
            Setup(@"Mozilla/5.0 (BB10; Kbd) AppleWebKit/537.35+ (KHTML, like Gecko) Version/10.3.3.2205 Mobile Safari/537.35+");

            Assert.AreEqual(MobilePlatform.BB10, _mobileDetectorService.GetPlatform());
        }

        [TestMethod]
        public void TestPlatformAndroidOnWindowsPhone()
        {
            Setup(@"Mozilla/5.0 (Windows Phone 10.0; Android 6.0.1; Microsoft; Lumia 950) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116 Mobile Safari/537.36 Edge/15.14977");

            Assert.AreEqual(MobilePlatform.Android, _mobileDetectorService.GetPlatform());
        }

        [TestMethod]
        public void TestPlatformIpod()
        {
            Setup(@"Inception 1.4 (iPod touch; iPhone OS 4.2.1; en_US)");

            Assert.AreEqual(MobilePlatform.iPod, _mobileDetectorService.GetPlatform());
        }

        [TestMethod]
        public void TestPlatformWindowsCe()
        {
            Setup(@"Mozilla/2.0 (compatible; MSIE 3.02; Windows CE; 240x320)");

            Assert.AreEqual(MobilePlatform.WindowsCe, _mobileDetectorService.GetPlatform());
        }

        [TestMethod]
        public void TestPlatformUnmapped()
        {
            Setup(@"Mozilla/4.0 (compatible; MSIE 6.0; Windows 95; PalmSource; Blazer 3.0) 16; 160x160");

            Assert.AreEqual(MobilePlatform.Unmapped, _mobileDetectorService.GetPlatform());
        }

        private void Setup(string userAgent)
        {
            Dictionary<string, StringValues> headerDict = new Dictionary<string, StringValues>
            {
                { "User-Agent", new StringValues(userAgent) }
            };

            _httpContextAccessorMock.Setup(x => x.HttpContext.Request.Headers)
                                    .Returns(new HeaderDictionary(headerDict));

            _mobileDetectorService = new MobileDetectorService(_httpContextAccessorMock.Object);
        }
    }
}
