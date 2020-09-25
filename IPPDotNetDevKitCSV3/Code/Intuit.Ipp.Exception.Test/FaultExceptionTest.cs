using Intuit.Ipp.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Intuit.Ipp.Exception.Test
{
    /// <summary>
    ///This is a test class for FaultExceptionTest and is intended
    ///to contain all FaultExceptionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FaultExceptionTest
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
        ///A test for FaultException Constructor
        ///</summary>
        [TestMethod()]
        public void FaultExceptionConstructorTest()
        {
            string errorMessage = "Unauthorized";
            string errorCode = "401";
            string source = "Intuit.Ipp.Test";
            FaultException target = new FaultException(errorMessage, errorCode, source);
            Assert.AreEqual(target.Message, errorMessage);
            Assert.AreEqual(target.ErrorCode, errorCode);
            Assert.AreEqual(target.Source, source);
        }

        /// <summary>
        ///A test for FaultException Constructor
        ///</summary>
        [TestMethod()]
        public void FaultExceptionConstructorTest1()
        {
            string errorMessage = "Unauthorized";
            string errorCode = "401";
            string source = "Intuit.Ipp.Test";
            System.Exception innerException = new ArgumentNullException();
            FaultException target = new FaultException(errorMessage, errorCode, source, innerException);
            Assert.AreEqual(target.Message, errorMessage);
            Assert.AreEqual(target.ErrorCode, errorCode);
            Assert.AreEqual(target.Source, source);
            ReferenceEquals(target.InnerException, innerException);
        }

        /// <summary>
        ///A test for FaultException Constructor
        ///</summary>
        [TestMethod()]
        public void FaultExceptionConstructorTest2()
        {
            string errorMessage = "Unauthorized";
            string errorCode = "401";
            string source = "Intuit.Ipp.Test";
            System.Exception innerException = new ArgumentNullException();
            FaultException target = new FaultException(errorMessage, errorCode, source, innerException);
            FaultException newTarget = null;
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, target);
                s.Position = 0; // Reset stream position
                newTarget = (FaultException)formatter.Deserialize(s);
            }

            Assert.IsNotNull(newTarget);
            Assert.AreEqual(newTarget.Message, errorMessage);
            Assert.AreEqual(newTarget.ErrorCode, errorCode);
            Assert.AreEqual(newTarget.Source, source);
            ReferenceEquals(newTarget.InnerException, innerException);
        }

        /// <summary>
        ///A test for FaultException Constructor
        ///</summary>
        [TestMethod()]
        public void FaultExceptionConstructorTest3()
        {
            string message = "Fault exception was raised.";
            FaultException target = new FaultException();
            Assert.AreEqual(target.Message, message);
        }

        /// <summary>
        ///A test for FaultException Constructor
        ///</summary>
        [TestMethod()]
        public void FaultExceptionConstructorTest4()
        {
            string errorMessage = "This is an error message.";
            FaultException target = new FaultException(errorMessage);
            Assert.AreEqual(target.Message, errorMessage);
        }

        /// <summary>
        ///A test for FaultException Constructor
        ///</summary>
        [TestMethod()]
        public void FaultExceptionConstructorTest5()
        {
            string errorMessage = "This is an error message.";
            System.Exception innerException = new ArgumentNullException();
            FaultException target = new FaultException(errorMessage, innerException);
            Assert.AreEqual(target.Message, errorMessage);
            ReferenceEquals(target.InnerException, innerException);
        }
    }
}
