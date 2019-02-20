using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.Core.Compression;
using System.IO.Compression;
using Intuit.Ipp.Utility;

namespace Intuit.Ipp.Core.Test
{
    /// <summary>
    ///This is a test class for GZipCompressorTest and is intended
    ///to contain all GZipCompressorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GZipCompressorTest
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
        ///A test for GZipCompressor Constructor
        ///</summary>
        [TestMethod()]
        public void GZipCompressorConstructorTest()
        {
            GZipCompressor target = new GZipCompressor();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for Compress
        ///</summary>
        [TestMethod()]
        public void CompressTest()
        {
            GZipCompressor target = new GZipCompressor(); // TODO: Initialize to an appropriate value
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] content = encoding.GetBytes("abc@123"); // TODO: Initialize to an appropriate value
            Stream requestStream = new MemoryStream(); // TODO: Initialize to an appropriate value
            target.Compress(content, requestStream);
            Assert.IsNotNull(requestStream);
        }

        ///// <summary>
        /////A test for Decompress
        /////</summary>
        //[TestMethod()]
        //public void DecompressTest()
        //{
        //    GZipCompressor target = new GZipCompressor(); // TODO: Initialize to an appropriate value
        //    Stream responseStream = null; // TODO: Initialize to an appropriate value
        //    Stream expected = null; // TODO: Initialize to an appropriate value
        //    Stream actual;
        //    actual = target.Decompress(responseStream);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for DataCompressionFormat
        ///</summary>
        [TestMethod()]
        public void DataCompressionFormatTest()
        {
            GZipCompressor target = new GZipCompressor();
            Assert.AreEqual(DataCompressionFormat.GZip, target.DataCompressionFormat);
        }

        /// <summary>
        /// Tests Decompression with an actual compressed data
        /// </summary>
        [TestMethod()]
        public void CompressDecompressTest()
        {
            GZipCompressor target = new GZipCompressor();
            string strContent = "Hello World";
            byte[] content = Encoding.ASCII.GetBytes(strContent);
            using (Stream requestStream = new MemoryStream())
            {
                var compressedStream = new GZipStream(requestStream, CompressionMode.Compress);
                compressedStream.Write(content, 0, content.Length);
                Stream actual = target.Decompress(requestStream);
                Assert.IsNotNull(actual);
                actual.Read(content, 0, content.Length);
                string dString = Encoding.ASCII.GetString(content);
                Assert.IsTrue(string.Compare(strContent, dString, false) == 0);
            }
        }
    }
}
