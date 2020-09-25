using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Intuit.Ipp.Core.Test
{
    /// <summary>
    ///This is a test class for FaultHandlerTest and is intended
    ///to contain all FaultHandlerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FaultHandlerTest
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
        /////A test for IterateFaultAndPrepareException
        /////</summary>
        //[TestMethod()]
        //public void IterateFaultAndPrepareExceptionTest()
        //{
        //    FaultHandler_Accessor target = new FaultHandler_Accessor(); 
        //    Fault fault = new Fault();
        //    fault.type = FaultTypeEnum.ValidationFault.ToString();
        //    Error error1 = new Error { code = "2050", element = "firstname", Message = "Length exceeds limit", Detail = "Length of the field exceeds 21 chars" };
        //    Error error2 = new Error { code = "2080", element = "postalcode", Message = "Illegal number format", Detail = "ZipCode should be a number with at least 5 digits" };
        //    fault.Error = new Error[] { error1, error2 };

        //    List<IdsError> commonExceptions = new List<IdsError>();
        //    commonExceptions.Add(new IdsError("Length exceeds limit", "2050", "firstname", "Length of the field exceeds 21 chars"));
        //    commonExceptions.Add(new IdsError("Illegal number format", "2080", "postalcode", "ZipCode should be a number with at least 5 digits"));

        //    ValidationException validationException = new ValidationException(commonExceptions);
        //    IdsException actual = target.IterateFaultAndPrepareException(fault);
        //    Assert.ReferenceEquals(validationException, actual);

        //    fault.type = "Service";
        //    ServiceException serviceException = new ServiceException(commonExceptions);
        //    IdsException actual1 = target.IterateFaultAndPrepareException(fault);
        //    Assert.ReferenceEquals(serviceException, actual1);

        //    fault.type = FaultTypeEnum.AuthenticationFault.ToString();
        //    SecurityException serurityException = new SecurityException(commonExceptions);
        //    IdsException actual2 = target.IterateFaultAndPrepareException(fault);
        //    Assert.ReferenceEquals(serurityException, actual2);

        //    fault.type = FaultTypeEnum.SystemFault.ToString();
        //    IdsException idsException = new IdsException("Fault Exception of type: SystemFault has been generated.", commonExceptions);
        //    IdsException actual3 = target.IterateFaultAndPrepareException(fault);
        //    Assert.ReferenceEquals(idsException, actual3);
        //}

        ///// <summary>
        /////A test for IterateFaultAndPrepareException
        /////</summary>
        //[TestMethod()]
        //public void IterateFaultAndPrepareExceptionNullFaultTest()
        //{
        //    FaultHandler_Accessor target = new FaultHandler_Accessor(); 
        //    IdsException actual = target.IterateFaultAndPrepareException(null);
        //    Assert.IsNull(actual);
        //}

        ///// <summary>
        /////A test for IterateFaultAndPrepareException
        /////</summary>
        //[TestMethod()]
        //public void IterateFaultAndPrepareConditionsTest()
        //{
        //    FaultHandler_Accessor target = new FaultHandler_Accessor();
        //    Fault fault = new Fault();
        //    fault.Error = new Error[] {  };

        //    fault.type = FaultTypeEnum.ValidationFault.ToString();
        //    IdsException actual = target.IterateFaultAndPrepareException(fault);
        //    Assert.ReferenceEquals(null, actual);

        //    fault.type = "Service";
        //    IdsException actual1 = target.IterateFaultAndPrepareException(fault);
        //    Assert.ReferenceEquals(null, actual1);

        //    fault.type = FaultTypeEnum.AuthenticationFault.ToString();
        //    IdsException actual2 = target.IterateFaultAndPrepareException(fault);
        //    Assert.ReferenceEquals(null, actual2);

        //    fault.type = FaultTypeEnum.SystemFault.ToString();
        //    IdsException actual3 = target.IterateFaultAndPrepareException(fault);
        //    Assert.ReferenceEquals(null, actual3);

        //    fault.Error = null;

        //    fault.type = FaultTypeEnum.ValidationFault.ToString();
        //    IdsException actual11 = target.IterateFaultAndPrepareException(fault);
        //    Assert.ReferenceEquals(null, actual1);

        //    fault.type = "Service";
        //    IdsException actual12 = target.IterateFaultAndPrepareException(fault);
        //    Assert.ReferenceEquals(null, actual12);

        //    fault.type = FaultTypeEnum.AuthenticationFault.ToString();
        //    IdsException actual21 = target.IterateFaultAndPrepareException(fault);
        //    Assert.ReferenceEquals(null, actual21);

        //    fault.type = FaultTypeEnum.SystemFault.ToString();
        //    IdsException actual31 = target.IterateFaultAndPrepareException(fault);
        //    Assert.ReferenceEquals(null, actual31);
        //}

       

        ///// <summary>
        /////A test for ExtractFaultFromResponse
        /////</summary>
        //[TestMethod()]
        //public void ExtractFaultFromResponseNullErrorResponseTest()
        //{
        //    FaultHandler_Accessor target = new FaultHandler_Accessor();
        //    string errorString = "";
        //    Fault actual = target.ExtractFaultFromResponse(errorString);
        //    Assert.IsNull(actual);
        //}

        

        ///// <summary>
        /////A test for ParseErrorResponseAndPrepareException
        /////</summary>
        //[TestMethod()]
        //public void ParseErrorResponseAndPrepareExceptionEmptyErrorResponseTest()
        //{
        //    FaultHandler_Accessor target = new FaultHandler_Accessor();
        //    string errorString = "";
        //    IdsException actual = target.ParseErrorResponseAndPrepareException(errorString);
        //    Assert.IsNull(actual);
        //}

        ///// <summary>
        /////A test for ParseResponseAndThrowException
        /////</summary>
        //[TestMethod()]
        //public void ParseResponseAndThrowExceptionNullWebExceptionTest()
        //{
        //    FaultHandler_Accessor target = new FaultHandler_Accessor();
        //    IdsException actual= target.ParseResponseAndThrowException(null);
        //    Assert.IsNull(actual);
        //}

       
    }
}
