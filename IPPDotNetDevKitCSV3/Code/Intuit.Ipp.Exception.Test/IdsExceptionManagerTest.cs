using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Intuit.Ipp.Exception.Test
{
    /// <summary>
    ///This is a test class for IdsExceptionManagerTest and is intended
    ///to contain all IdsExceptionManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IdsExceptionManagerTest
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
        ///A test for HandleException
        ///</summary>
        [TestMethod()]
        public void HandleExceptionTest()
        {
            string errorMessage = "Unauthorized";
            string errorCode = "401";
            string source = "Intuit.Ipp.Test";
            try
            {
                IdsExceptionManager.HandleException(errorMessage, errorCode, source);
            }
            catch (IdsException target)
            {
                Assert.AreEqual(target.Message, errorMessage);
                Assert.AreEqual(target.ErrorCode, errorCode);
                Assert.AreEqual(target.Source, source);
            }
        }

        /// <summary>
        ///A test for HandleException
        ///</summary>
        [TestMethod()]
        public void HandleExceptionTest1()
        {
            string errorMessage = "Unauthorized";
            string errorCode = "401";
            string source = "Intuit.Ipp.Test";
            InvalidRealmException innerException = new InvalidRealmException();
            try
            {
                IdsExceptionManager.HandleException(errorMessage, errorCode, source, innerException);
            }
            catch (IdsException target)
            {
                Assert.AreEqual(target.Message, errorMessage);
                Assert.AreEqual(target.ErrorCode, errorCode);
                Assert.AreEqual(target.Source, source);
                ReferenceEquals(target.InnerException, innerException);
            }
        }

        /// <summary>
        ///A test for HandleException
        ///</summary>
        [TestMethod()]
        public void HandleExceptionTest2()
        {
            string errorMessage = "Unauthorized";
            InvalidServiceRequestException innerException = new InvalidServiceRequestException();
            try
            {
                IdsExceptionManager.HandleException(errorMessage, innerException);
            }
            catch (IdsException target)
            {
                Assert.AreEqual(target.Message, errorMessage);
                ReferenceEquals(target.InnerException, innerException);
            }
        }

        /// <summary>
        ///A test for HandleException
        ///</summary>
        [TestMethod()]
        public void HandleExceptionTest3()
        {
            string errorMessage = "Unauthorized";
            try
            {
                IdsExceptionManager.HandleException(errorMessage);
            }
            catch (IdsException target)
            {
                Assert.AreEqual(target.Message, errorMessage);
            }
        }

        /// <summary>
        ///A test for HandleException
        ///</summary>
        [TestMethod()]
        public void HandleExceptionTest4()
        {
            InvalidTokenException exception = new InvalidTokenException();
            try
            {
                IdsExceptionManager.HandleException(exception);
            }
            catch (IdsException target)
            {
                ReferenceEquals(target, exception);
            }
        }
    }
}
