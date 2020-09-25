using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Intuit.Ipp.Core.Test
{
    /// <summary>
    ///This is a test class for LogRequestsToDiskTest and is intended
    ///to contain all LogRequestsToDiskTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LogRequestsToDiskTest
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

        ///// <summary>
        /////A test for LogPlatformRequests
        /////</summary>
        //[TestMethod()]
        //public void LogPlatformRequestsTest()
        //{
        //    string folderPath = Path.GetTempPath();
        //    string strReqLogFiles = string.Format(CoreConstants.REQUESTFILENAME_FORMAT, folderPath, CoreConstants.SLASH_CHAR, "*");
        //    string strResLogFiles = string.Format(CoreConstants.RESPONSEFILENAME_FORMAT, folderPath, CoreConstants.SLASH_CHAR, "*");
        //    var allFiles = Directory.EnumerateFiles(folderPath, Path.GetFileName(strReqLogFiles)).ToList();
        //    allFiles.AddRange(Directory.EnumerateFiles(folderPath, Path.GetFileName(strResLogFiles)));
        //    allFiles.ForEach(file => File.Delete(file));

        //    string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?><IntuitResponse xmlns=\"http://schema.intuit.com/finance/v3\" requestId=\"583ec8368ff2421abe52c87faa4d5f06\" time=\"2012-06-01T10:03:08+00:00\"><Fault type=\"RequestProcessing\"><Error code=\"401\"><Message>Invalid security token</Message></Error></Fault></IntuitResponse>";
        //    LogRequestsToDisk_Accessor requestLogging = new LogRequestsToDisk_Accessor();
        //    requestLogging.EnableServiceRequestsLogging = true;
        //    requestLogging.ServiceRequestLoggingLocation = folderPath;
        //    requestLogging.LogPlatformRequests(xml, true);
        //    requestLogging.LogPlatformRequests(xml, false);
        //    DirectoryInfo info = new DirectoryInfo(folderPath);
            
        //    try
        //    {
        //        allFiles = Directory.EnumerateFiles(folderPath, Path.GetFileName(strReqLogFiles)).ToList();
        //        allFiles.AddRange(Directory.EnumerateFiles(folderPath, Path.GetFileName(strResLogFiles)));

        //        Assert.AreEqual(2, allFiles.Count);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Assert.Fail(ex.ToString());
        //    }
        //}

    }
}
