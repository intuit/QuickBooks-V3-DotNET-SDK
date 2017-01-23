using System.Text;
using Intuit.Ipp.Core.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Intuit.Ipp.Core.Test
{
    /// <summary>
    ///This is a test class for EncodingFixerTest and is intended
    ///to contain all EncodingFixerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class EncodingFixerTest
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
        ///A test for FixQuickBaseEncoding
        ///</summary>
        [TestMethod]
        public void EncodingFixerExpectedExceptionTest()
        {
            byte[] bytes = new byte[] { 0xFF, 150 };
            string encodingFixedString = EncodingFixer.FixQuickBaseEncoding(bytes);
            Assert.AreEqual(encodingFixedString, "255–");
        }

        /// <summary>
        ///A test for FixQuickBaseEncoding
        ///</summary>
        [TestMethod]
        public void EncodingFixerSuccessTest()
        {
            UTF8Encoding encoder = new UTF8Encoding();
            byte[] bytes = encoder.GetBytes("a");
            string encodingFixedString = EncodingFixer.FixQuickBaseEncoding(bytes);
            Assert.AreEqual(encodingFixedString, "a");
        }
    }
}
