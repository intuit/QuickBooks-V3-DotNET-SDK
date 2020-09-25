using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Intuit.Ipp.Diagnostics.Test
{
    /// <summary>
    ///This is a test class for TraceLoggerTest and is intended
    ///to contain all TraceLoggerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TraceLoggerTest
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
                TraceLogger target = new TraceLogger();
            }
            catch (Exception ex)
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
            TraceLogger target = new TraceLogger();
            string messageToWrite = "Error message.";
            target.Log(TraceLevel.Error, messageToWrite);
        }

        /// <summary>
        ///A test for Log Information
        ///</summary>
        [TestMethod()]
        public void LogInfoTest()
        {
            TraceLogger target = new TraceLogger();
            string messageToWrite = "Information message.";
            target.Log(TraceLevel.Info, messageToWrite);
        }

        /// <summary>
        ///A test for Log Verbose
        ///</summary>
        [TestMethod()]
        public void LogVerboseTest()
        {
            TraceLogger target = new TraceLogger();
            string messageToWrite = "Verbose message.";
            target.Log(TraceLevel.Verbose, messageToWrite);
        }

        /// <summary>
        ///A test for Log Warning
        ///</summary>
        [TestMethod()]
        public void LogWarningTest()
        {
            TraceLogger target = new TraceLogger();
            string messageToWrite = "Warning message.";
            target.Log(TraceLevel.Warning, messageToWrite);
        }

        /// <summary>
        ///A test for Log Off
        ///</summary>
        [TestMethod()]
        public void LogOffTest()
        {
            TraceLogger target = new TraceLogger();
            string messageToWrite = "Warning message.";
            target.Log(TraceLevel.Off, messageToWrite);
        }
    }
}
