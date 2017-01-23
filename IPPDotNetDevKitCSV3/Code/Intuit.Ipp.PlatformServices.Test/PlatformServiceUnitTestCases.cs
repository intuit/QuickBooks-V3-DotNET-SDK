namespace Intuit.Ipp.PlatformServices.Test
{
    using System;
    using Intuit.Ipp.PlatformService;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass()]
    public class PlatformServiceUniteTestCases
    {
        public PlatformServiceUniteTestCases()
        {

        }
        [TestMethod]
        public void PaseDisconnectResponse1Test()
        {
            string response = @"<PlatformResponse xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://platform.intuit.com/api/v1""><ErrorCode>0</ErrorCode><ServerTime>2011-11-23T17:15:27.21097Z</ServerTime></PlatformResponse>";
            try
            {
                PlatformService.ParseDisconnectResponse(response);
            }
            catch (PlatformException pex)
            {
                Console.WriteLine(@"ErrorCode:" + pex.ErrorCode);
                Console.WriteLine(@"ErrorMessage:" + pex.ErrorMessage);
                Console.WriteLine(@"ServerTime:" + pex.ServerTime);
                Assert.Fail();
            }

        }
        [TestMethod]
        public void PaseDisconnectResponse2Test()
        {
            string response = @"<PlatformResponse xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns=""http://platform.intuit.com/api/v1""><ErrorMessage>OAuth Token rejected</ErrorMessage><ErrorCode>270</ErrorCode><ServerTime>2011-11-24T17:45:27.11097Z</ServerTime></PlatformResponse>";
            try
            {
                PlatformService.ParseDisconnectResponse(response);
            }
            catch (PlatformException pex)
            {
                Console.WriteLine("ErrorCode:" + pex.ErrorCode);
                Console.WriteLine("ErrorMessage:" + pex.ErrorMessage);
                Console.WriteLine("ServerTime:" + pex.ServerTime);

                Assert.AreEqual(pex.ErrorCode, "270");
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void DisconnectUnitTest()
        {
            try
            {
                PlatformService.Disconnect("qyprdhtVWI7KWRSePnCA0D3FcL9UFN", "Nt6oZvlQJxZYB3abCs9P6U6J4kFTenDlw8a4SQOs",
                    "qyprdav0hYdwXThhyghyMobDN06e14JXACiO9vpAX6Cls539", "bpkv1HvUzEwhkHNGEjtsy2Bh4yfeIzMWdjfhp2Q6");
            }
            catch (PlatformException pex)
            {
                Console.WriteLine("errCode:" + pex.ErrorCode + " errMsg:" + pex.ErrorMessage + " serverTime:" + pex.ServerTime);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void ReconnectUnitTest()
        {
            try
            {
                PlatformService.Reconnect("qyprdhtVWI7KWRSePnCA0D3FcL9UFN", "Nt6oZvlQJxZYB3abCs9P6U6J4kFTenDlw8a4SQOs",
                    "qyprdav0hYdwXThhyghyMobDN06e14JXACiO9vpAX6Cls539", "bpkv1HvUzEwhkHNGEjtsy2Bh4yfeIzMWdjfhp2Q6");

            }
            catch (PlatformException pex)
            {
                Console.WriteLine("errCode:" + pex.ErrorCode + " errMsg:" + pex.ErrorMessage + " serverTime:" + pex.ServerTime);
                return;
            }
            Assert.Fail();
        }
        [TestMethod]
        public void GetCurrentUserUnitTest()
        {
            try
            {
                PlatformService.GetCurrentUser("qyprdhtVWI7KWRSePnCA0D3FcL9UFN", "Nt6oZvlQJxZYB3abCs9P6U6J4kFTenDlw8a4SQOs",
                    "qyprdav0hYdwXThhyghyMobDN06e14JXACiO9vpAX6Cls539", "bpkv1HvUzEwhkHNGEjtsy2Bh4yfeIzMWdjfhp2Q6");

            }
            catch (PlatformException pex)
            {
                Console.WriteLine("errCode:" + pex.ErrorCode + " errMsg:" + pex.ErrorMessage + " serverTime:" + pex.ServerTime);
                return;
            }
            Assert.Fail();
        }
    }

}