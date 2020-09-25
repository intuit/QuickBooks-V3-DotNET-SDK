using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Intuit.Ipp.Utility.Test
{
    /// <summary>
    ///This is a test class for IppConfigurationSection and is intended
    ///to contain all IppConfigurationSectionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IppConfigurationSectionTest
    {
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for IppConfigurationSection Constructor
        ///</summary>
        [TestMethod()]
        public void IppConfigurationSectionConstructorTest()
        {
            IppConfigurationSection target = new IppConfigurationSection();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for Instance
        ///</summary>
        [TestMethod()][Ignore]
        public void InstanceTest()
        {
            IppConfigurationSection actual = IppConfigurationSection.Instance;
   
            Assert.IsNotNull(actual.WebhooksService.WebhooksVerifier.Value);
            Assert.IsNotNull(actual);
            Assert.IsNotNull(actual.Logger);
            Assert.IsNotNull(actual.Logger.RequestLog);
            Assert.IsTrue(actual.Logger.RequestLog.EnableRequestResponseLogging);
            Assert.IsNotNull(actual.Logger.RequestLog.RequestResponseLoggingDirectory);
            Assert.IsNotNull(actual.Logger.CustomLogger);
            Assert.IsNotNull(actual.Logger.CustomLogger.Name);
            Assert.IsNotNull(actual.Logger.CustomLogger.Type);
            Assert.IsTrue(actual.Logger.CustomLogger.Enable);
            Assert.IsNotNull(actual.Security);
            Assert.IsNotNull(actual.Security.Mode);
            Assert.IsNotNull(actual.Security.OAuth);
            Assert.IsNotNull(actual.Security.OAuth.AccessToken);
            Assert.IsNotNull(actual.Security.OAuth.AccessTokenSecret);
            Assert.IsNotNull(actual.Security.OAuth.ConsumerKey);
            Assert.IsNotNull(actual.Security.OAuth.ConsumerSecret);          
            Assert.IsNotNull(actual.Security.CustomSecurity);
            Assert.IsNotNull(actual.Security.CustomSecurity.Name);
            Assert.IsNotNull(actual.Security.CustomSecurity.Type);
            Assert.IsNotNull(actual.Security.CustomSecurity.Params);
            Assert.IsTrue(actual.Security.CustomSecurity.Enable);
            Assert.IsNotNull(actual.Message);
            Assert.IsNotNull(actual.Message.Request);
            Assert.IsNotNull(actual.Message.Request.CompressionFormat);
            Assert.IsNotNull(actual.Message.Request.SerializationFormat);
            Assert.IsNotNull(actual.Message.Response);
            Assert.IsNotNull(actual.Message.Response.CompressionFormat);
            Assert.IsNotNull(actual.Message.Response.SerializationFormat);
            Assert.IsNotNull(actual.Message.CustomSerializer);
            Assert.IsNotNull(actual.Message.CustomSerializer.Name);
            Assert.IsNotNull(actual.Message.CustomSerializer.Type);
            Assert.IsTrue(actual.Message.CustomSerializer.Enable);
            Assert.IsNotNull(actual.Retry);
            Assert.IsNotNull(actual.Retry.Mode);
            Assert.IsNotNull(actual.Retry.LinearRetry);
            Assert.IsNotNull(actual.Retry.LinearRetry.RetryCount);
            Assert.IsNotNull(actual.Retry.LinearRetry.RetryInterval);
            Assert.IsNotNull(actual.Retry.IncrementatlRetry);
            Assert.IsNotNull(actual.Retry.IncrementatlRetry.RetryCount);
            Assert.IsNotNull(actual.Retry.IncrementatlRetry.InitialInterval);
            Assert.IsNotNull(actual.Retry.IncrementatlRetry.Increment);
            Assert.IsNotNull(actual.Retry.ExponentialRetry);
            Assert.IsNotNull(actual.Retry.ExponentialRetry.RetryCount);
            Assert.IsNotNull(actual.Retry.ExponentialRetry.MinBackoff);
            Assert.IsNotNull(actual.Retry.ExponentialRetry.MaxBackoff);
            Assert.IsNotNull(actual.Retry.ExponentialRetry.DeltaBackoff);
            Assert.IsNotNull(actual.Service);
            Assert.IsNotNull(actual.Service.BaseUrl);         
            Assert.IsNotNull(actual.Service.BaseUrl.Qbo);
            Assert.IsNotNull(actual.Service.BaseUrl.Ips);
            Assert.IsNotNull(actual.Service.BaseUrl.UserNameAuthentication);
            Assert.IsNotNull(actual.Service.BaseUrl.OAuthAccessTokenUrl);
        }

        /// <summary>
        ///A test for Logger
        ///</summary>
        [TestMethod()]
        public void LoggerTest()
        {
            IppConfigurationSection target = new IppConfigurationSection(); // TODO: Initialize to an appropriate value
            LoggerElement actual = target.Logger;
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for Message
        ///</summary>
        [TestMethod()]
        public void MessageTest()
        {
            IppConfigurationSection target = new IppConfigurationSection(); // TODO: Initialize to an appropriate value
            MessageElement actual = target.Message;
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for Retry
        ///</summary>
        [TestMethod()]
        public void RetryTest()
        {
            IppConfigurationSection target = new IppConfigurationSection(); // TODO: Initialize to an appropriate value
            RetryElement actual = target.Retry;
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for Security
        ///</summary>
        [TestMethod()]
        public void SecurityTest()
        {
            IppConfigurationSection target = new IppConfigurationSection(); // TODO: Initialize to an appropriate value
            SecurityElement actual = target.Security;
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for Service
        ///</summary>
        [TestMethod()]
        public void ServiceTest()
        {
            IppConfigurationSection target = new IppConfigurationSection(); // TODO: Initialize to an appropriate value
            ServiceElement actual = target.Service;
            Assert.IsNotNull(actual);
        }
    }
}
