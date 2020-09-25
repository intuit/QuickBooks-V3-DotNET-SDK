﻿using Intuit.Ipp.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Intuit.Ipp.Exception.Test
{
    /// <summary>
    ///This is a test class for InvalidTokenExceptionTest and is intended
    ///to contain all InvalidTokenExceptionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class InvalidTokenExceptionTest
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
        ///A test for InvalidTokenException Constructor
        ///</summary>
        [TestMethod()]
        public void InvalidTokenExceptionConstructorTest()
        {
            string errorMessage = "Unauthorized";
            string errorCode = "401";
            string source = "Intuit.Ipp.Test";
            InvalidTokenException target = new InvalidTokenException(errorMessage, errorCode, source);
            Assert.AreEqual(target.Message, errorMessage);
            Assert.AreEqual(target.ErrorCode, errorCode);
            Assert.AreEqual(target.Source, source);
        }

        /// <summary>
        ///A test for InvalidTokenException Constructor
        ///</summary>
        [TestMethod()]
        public void InvalidTokenExceptionConstructorTest1()
        {
            string errorMessage = "Unauthorized";
            string errorCode = "401";
            string source = "Intuit.Ipp.Test";
            System.Exception innerException = new ArgumentNullException();
            InvalidTokenException target = new InvalidTokenException(errorMessage, errorCode, source, innerException);
            Assert.AreEqual(target.Message, errorMessage);
            Assert.AreEqual(target.ErrorCode, errorCode);
            Assert.AreEqual(target.Source, source);
            ReferenceEquals(target.InnerException, innerException);
        }

        /// <summary>
        ///A test for InvalidTokenException Constructor
        ///</summary>
        [TestMethod()]
        public void InvalidTokenExceptionConstructorTest2()
        {
            string errorMessage = "Unauthorized";
            string errorCode = "401";
            string source = "Intuit.Ipp.Test";
            System.Exception innerException = new ArgumentNullException();
            InvalidTokenException target = new InvalidTokenException(errorMessage, errorCode, source, innerException);
            InvalidTokenException newTarget = null;
            using (Stream s = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(s, target);
                s.Position = 0; // Reset stream position
                newTarget = (InvalidTokenException)formatter.Deserialize(s);
            }

            Assert.IsNotNull(newTarget);
            Assert.AreEqual(newTarget.Message, errorMessage);
            Assert.AreEqual(newTarget.ErrorCode, errorCode);
            Assert.AreEqual(newTarget.Source, source);
            ReferenceEquals(newTarget.InnerException, innerException);
        }

        /// <summary>
        ///A test for InvalidTokenException Constructor
        ///</summary>
        [TestMethod()]
        public void InvalidTokenExceptionConstructorTest3()
        {
            string message = "Security Token is invalid.";
            InvalidTokenException target = new InvalidTokenException();
            Assert.AreEqual(target.Message, message);
        }

        /// <summary>
        ///A test for InvalidTokenException Constructor
        ///</summary>
        [TestMethod()]
        public void InvalidTokenExceptionConstructorTest4()
        {
            string errorMessage = "This is an error message.";
            InvalidTokenException target = new InvalidTokenException(errorMessage);
            Assert.AreEqual(target.Message, errorMessage);
        }

        /// <summary>
        ///A test for InvalidTokenException Constructor
        ///</summary>
        [TestMethod()]
        public void InvalidTokenExceptionConstructorTest5()
        {
            string errorMessage = "This is an error message.";
            System.Exception innerException = new ArgumentNullException();
            InvalidTokenException target = new InvalidTokenException(errorMessage, innerException);
            Assert.AreEqual(target.Message, errorMessage);
            ReferenceEquals(target.InnerException, innerException);
        }
    }
}
