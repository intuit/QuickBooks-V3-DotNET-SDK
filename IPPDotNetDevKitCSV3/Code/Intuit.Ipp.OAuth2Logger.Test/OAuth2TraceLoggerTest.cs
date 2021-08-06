using Intuit.Ipp.OAuth2Logger;
using Intuit.Ipp.OAuth2Logger.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Intuit.Ipp.OAuth2Logger.Test
{
    /// <summary>
    ///This is a test class for TraceLoggerTest and is intended
    ///to contain all TraceLoggerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OAuth2TraceLoggerTest
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
        ///A test for TraceLogger Constructor
        ///</summary>
        [TestMethod()]
        public void TraceLoggerConstructorTest()
        {
            try
            {
                OAuth2TraceLogger target = new OAuth2TraceLogger();
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for Log Error
        ///</summary>
        [TestMethod()]
        public void LogErrorTest()
        {
            OAuth2TraceLogger target = new OAuth2TraceLogger();
            string messageToWrite = "Error message.";
            target.Log(OAuth2TraceLevel.Error, messageToWrite);
        }

        /// <summary>
        ///A test for Log Information
        ///</summary>
        [TestMethod()]
        public void LogInfoTest()
        {
            OAuth2TraceLogger target = new OAuth2TraceLogger();
            string messageToWrite = "Information message.";
            target.Log(OAuth2TraceLevel.Info, messageToWrite);
        }

        /// <summary>
        ///A test for Log Verbose
        ///</summary>
        [TestMethod()]
        public void LogVerboseTest()
        {
            OAuth2TraceLogger target = new OAuth2TraceLogger();
            string messageToWrite = "Verbose message.";
            target.Log(OAuth2TraceLevel.Verbose, messageToWrite);
        }

        /// <summary>
        ///A test for Log Warning
        ///</summary>
        [TestMethod()]
        public void LogWarningTest()
        {
            OAuth2TraceLogger target = new OAuth2TraceLogger();
            string messageToWrite = "Warning message.";
            target.Log(OAuth2TraceLevel.Warning, messageToWrite);
        }

        /// <summary>
        ///A test for Log Off
        ///</summary>
        [TestMethod()]
        public void LogOffTest()
        {
            OAuth2TraceLogger target = new OAuth2TraceLogger();
            string messageToWrite = "Warning message.";
            target.Log(OAuth2TraceLevel.Off, messageToWrite);
        }
    }
}
