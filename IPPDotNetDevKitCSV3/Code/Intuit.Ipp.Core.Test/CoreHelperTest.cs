using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;

namespace Intuit.Ipp.Core.Test
{
    /// <summary>
    /// Summary description for CoreHelperTest
    /// </summary>
    [TestClass]
    public class CoreHelperTest
    {
        public CoreHelperTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        /// Test for ParseResponseIntoXml API
        /// </summary>
        [TestMethod]
        public void ParseResponseIntoXmlTest()
        {
            string response = "<?xml version=\"1.0\"?> <Node1></Node1>";
            XmlDocument doc = CoreHelper.ParseResponseIntoXml(response);
            Assert.IsNotNull(doc);
        }

        

        /// <summary>
        /// Test for IsInvalidaLinearRetryMode API
        /// </summary>
        [TestMethod]
        public void IsInvalidaLinearRetryModeTest()
        {
            bool isInvalid = (bool)InvokeHelper.RunStaticMethod(typeof(CoreHelper), "IsInvalidaLinearRetryMode", new object[] { 0, new TimeSpan() });
            Assert.IsTrue(isInvalid);

            isInvalid = (bool)InvokeHelper.RunStaticMethod(typeof(CoreHelper), "IsInvalidaLinearRetryMode", new object[] { 0, new TimeSpan(1) });
            Assert.IsTrue(isInvalid);

            isInvalid = (bool)InvokeHelper.RunStaticMethod(typeof(CoreHelper), "IsInvalidaLinearRetryMode", new object[] { 1, new TimeSpan(1) });
            Assert.IsFalse(isInvalid);
        }
    }
}
