using Intuit.Ipp.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Intuit.Ipp.Core.Compression;
using System.Text;
using System.IO.Compression;

namespace Intuit.Ipp.Core.Test
{
    
    
    /// <summary>
    ///This is a test class for DeflateCompressorTest and is intended
    ///to contain all DeflateCompressorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DeflateCompressorTest
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
        ///A test for DeflateCompressor Constructor
        ///</summary>
        [TestMethod()]
        public void DeflateCompressorConstructorTest()
        {
            DeflateCompressor target = new DeflateCompressor();
            Assert.IsTrue(target.DataCompressionFormat == DataCompressionFormat.Deflate);
        }

        /// <summary>
        ///A test for Compress
        ///</summary>
        [TestMethod()]
        public void CompressTest()
        {
            DeflateCompressor target = new DeflateCompressor(); // TODO: Initialize to an appropriate value
            byte[] content = Encoding.ASCII.GetBytes("Hello World");
            using (Stream requestStream = new MemoryStream())
            {
                target.Compress(content, requestStream);
            }
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Decompress
        ///</summary>
        [TestMethod()]
        public void DecompressTest()
        {
            DeflateCompressor target = new DeflateCompressor();
            using (Stream responseStream = new MemoryStream())
            {
                Stream actual = target.Decompress(responseStream);
                Assert.IsNotNull(actual);
            }
        }

        /// <summary>
        ///A test for DataCompressionFormat
        ///</summary>
        [TestMethod()]
        public void DataCompressionFormatTest()
        {
            DeflateCompressor target = new DeflateCompressor(); // TODO: Initialize to an appropriate value
            Assert.IsTrue(target.DataCompressionFormat == DataCompressionFormat.Deflate);
        }

        /// <summary>
        /// Tests Decompression with an actual compressed data
        /// </summary>
        [TestMethod()]
        public void CompressDecompressTest()
        {
            DeflateCompressor target = new DeflateCompressor();
            string strContent = "Hello World";
            byte[] content = Encoding.ASCII.GetBytes(strContent);
            using (Stream requestStream = new MemoryStream())
            {
                var compressedStream = new DeflateStream(requestStream, CompressionMode.Compress);
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
