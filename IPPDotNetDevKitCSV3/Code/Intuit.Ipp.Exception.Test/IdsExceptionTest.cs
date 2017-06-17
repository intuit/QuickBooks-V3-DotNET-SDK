using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Intuit.Ipp.Exception.Test
{
    /// <summary>
    ///This is a test class for IdsExceptionTest and is intended
    ///to contain all IdsExceptionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IdsExceptionTest
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
        ///A test for IdsException Constructor
        ///</summary>
        [TestMethod()]
        public void IdsExceptionConstructorTest()
        {
            string errorMessage = "Unauthorized";
            string errorCode = "401";
            string source = "Intuit.Ipp.Test";
            IdsException target = new IdsException(errorMessage, errorCode, source);
            Assert.AreEqual(target.Message, errorMessage);
            Assert.AreEqual(target.ErrorCode, errorCode);
            Assert.AreEqual(target.Source, source);
        }

        /// <summary>
        ///A test for IdsException Constructor
        ///</summary>
        [TestMethod()]
        public void IdsExceptionConstructorTest1()
        {
            string errorMessage = "Unauthorized";
            string errorCode = "401";
            string source = "Intuit.Ipp.Test";
            System.Exception innerException = new ArgumentNullException();
            IdsException target = new IdsException(errorMessage, errorCode, source, innerException);
            Assert.AreEqual(target.Message, errorMessage);
            Assert.AreEqual(target.ErrorCode, errorCode);
            Assert.AreEqual(target.Source, source);
            Assert.ReferenceEquals(target.InnerException, innerException);
        }

        /// <summary>
        ///A test for IdsException Constructor
        ///</summary>
        [TestMethod()]
        public void IdsExceptionConstructorTest2()
        {
            string errorMessage = "Unauthorized";
            string errorCode = "401";
            string source = "Intuit.Ipp.Test";
            System.Exception innerException = new ArgumentNullException();
            IdsException target = new IdsException(errorMessage, errorCode, source, innerException);
            IdsException newTarget = null;
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, target);
                s.Position = 0; // Reset stream position
                newTarget = (IdsException)formatter.Deserialize(s);
            }

            Assert.IsNotNull(newTarget);
            Assert.AreEqual(newTarget.Message, errorMessage);
            Assert.AreEqual(newTarget.ErrorCode, errorCode);
            Assert.AreEqual(newTarget.Source, source);
            Assert.ReferenceEquals(newTarget.InnerException, innerException);
        }

        /// <summary>
        ///A test for IdsException Constructor
        ///</summary>
        [TestMethod()]
        public void IdsExceptionConstructorTest3()
        {
            string message = "Ids exception was thrown.";
            IdsException target = new IdsException();
            Assert.AreEqual(target.Message, message);
        }

        /// <summary>
        ///A test for IdsException Constructor
        ///</summary>
        [TestMethod()]
        public void IdsExceptionConstructorTest4()
        {
            string errorMessage = "This is an error message.";
            IdsException target = new IdsException(errorMessage);
            Assert.AreEqual(target.Message, errorMessage);
        }

        /// <summary>
        ///A test for IdsException Constructor
        ///</summary>
        [TestMethod()]
        public void IdsExceptionConstructorTest5()
        {
            string errorMessage = "This is an error message.";
            System.Exception innerException = new ArgumentNullException();
            IdsException target = new IdsException(errorMessage, innerException);
            Assert.AreEqual(target.Message, errorMessage);
            Assert.ReferenceEquals(target.InnerException, innerException);
        }

        /// <summary>
        ///A test for IdsException set ErrorCode Test
        ///</summary>
        [TestMethod()]
        public void IdsExceptionSetErrorCodeTest()
        {
            string errorMessage = "This is an error message.";
            IdsException target = new IdsException(errorMessage);
            target.ErrorCode = "401";
            Assert.AreEqual(target.Message, errorMessage);
            Assert.ReferenceEquals(target.ErrorCode, "401");
        }

        /// <summary>
        ///A test for IdsException Constructor
        ///</summary>
        [TestMethod()]
        public void IdsExceptionConstructorTest6()
        {
            string errorMessage = "This is an error message.";
            List<IdsError> innerExceptions = new List<IdsError> { new IdsError(errorMessage) };
            IdsException target = new IdsException(errorMessage, innerExceptions);

            IdsError idsError = innerExceptions[0];
            Assert.AreEqual(target.Message, errorMessage+"Details:.");
            Assert.ReferenceEquals(target.InnerExceptions, innerExceptions);
        }

        /// <summary>
        ///A test for IdsException Constructor null innerExceptions
        ///</summary>
        [TestMethod()]
        public void IdsExceptionConstructorTest7()
        {
            string errorMessage = "This is an error message.";
            IdsException target = new IdsException(errorMessage: errorMessage, innerExceptions: null);
            Assert.AreEqual(target.Message, errorMessage);
            Assert.ReferenceEquals(target.InnerExceptions, null);
        }
    }
}
