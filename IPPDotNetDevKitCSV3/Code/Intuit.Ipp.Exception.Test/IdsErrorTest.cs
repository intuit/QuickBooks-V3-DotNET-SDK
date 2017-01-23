using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Intuit.Ipp.Exception.Test
{
    /// <summary>
    ///This is a test class for IdsErrorTest and is intended
    ///to contain all IdsErrorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IdsErrorTest
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
        ///A test for IdsError Constructor
        ///</summary>
        [TestMethod()]
        public void IdsErrorConstructorTest()
        {
            string errorMessage = "Length exceeds limit";
            string errorCode = "2050";
            string element = "firstName";
            string detail = "Length of the field exceeds 21 chars";
            IdsError target = new IdsError(errorMessage, errorCode, element, detail);
            Assert.AreEqual(target.Message, errorMessage);
            Assert.AreEqual(target.ErrorCode, errorCode);
            Assert.AreEqual(target.Element, element);
            Assert.AreEqual(target.Detail, detail);
        }

        /// <summary>
        ///A test for IdsError Constructor
        ///</summary>
        [TestMethod()]
        public void IdsErrorConstructorTest1()
        {
            string errorMessage = "Length exceeds limit";
            string errorCode = "2050";
            string element = "firstName";
            string detail = "Length of the field exceeds 21 chars";
            IdsError target = new IdsError(errorMessage, errorCode, element, detail);
            Assert.AreEqual(target.Message, errorMessage);
            Assert.AreEqual(target.ErrorCode, errorCode);
            Assert.AreEqual(target.Element, element);
            Assert.AreEqual(target.Detail, detail);
        }

        /// <summary>
        ///A test for IdsError Constructor
        ///</summary>
        [TestMethod()]
        public void IdsErrorConstructorTest2()
        {
            string errorMessage = "Length exceeds limit";
            string errorCode = "2050";
            string element = "firstName";
            string detail = "Length of the field exceeds 21 chars";
            IdsError target = new IdsError(errorMessage, errorCode, element, detail);
            IdsError newTarget = null;
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, target);
                s.Position = 0; // Reset stream position
                newTarget = (IdsError)formatter.Deserialize(s);
            }

            Assert.IsNotNull(newTarget);
            Assert.AreEqual(newTarget.Message, errorMessage);
            Assert.AreEqual(newTarget.ErrorCode, errorCode);
            Assert.AreEqual(newTarget.Element, element);
            Assert.AreEqual(newTarget.Detail, detail);
        }

        /// <summary>
        ///A test for IdsError Constructor
        ///</summary>
        [TestMethod()]
        public void IdsErrorConstructorTest3()
        {
            string message = "IdsError was thrown.";
            IdsError target = new IdsError();
            Assert.AreEqual(target.Message, message);
        }

        /// <summary>
        ///A test for IdsError Constructor
        ///</summary>
        [TestMethod()]
        public void IdsErrorConstructorTest4()
        {
            string errorMessage = "This is an error message.";
            IdsError target = new IdsError(errorMessage);
            Assert.AreEqual(target.Message, errorMessage);
        }

        /// <summary>
        ///A test for IdsError Constructor
        ///</summary>
        [TestMethod()]
        public void IdsErrorConstructorTest5()
        {
            IdsError target = new IdsError("errorMessage", "errorCode", "element", "detail");
            Assert.ReferenceEquals(target.Message, "errorMessage");
            Assert.ReferenceEquals(target.ErrorCode, "errorCode");
            Assert.ReferenceEquals(target.Element, "element");
            Assert.ReferenceEquals(target.Detail, "detail");
        }

        /// <summary>
        ///A test for IdsError set ErrorCode Test
        ///</summary>
        [TestMethod()]
        public void IdsErrorConstructorTest6()
        {
            string errorMessage = "This is an error message.";
            IdsError target = new IdsError(errorMessage);
            target.ErrorCode = "401";
            Assert.AreEqual(target.Message, errorMessage);
            Assert.ReferenceEquals(target.ErrorCode, "401");
        }

    }
}
