using System.Configuration;
using Intuit.Ipp.Core;
using Intuit.Ipp.Exception;
using Intuit.Ipp.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Intuit.Ipp.DataService.Test
{
    /// <summary>
    ///This is a test class for DataServiceConstructorTest
    ///</summary>
    [TestClass()]
    public class DataServiceConstructorTest
    {
        private TestContext testContextInstance;
        private static ServiceContext context;

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

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            string accessTokenQBO = ConfigurationManager.AppSettings["AccessTokenQBO"];
            string realmIAQBO = ConfigurationManager.AppSettings["RealmIAQBO"];

            OAuth2RequestValidator oAuthRequestValidator = new OAuth2RequestValidator(accessTokenQBO);
            context = new ServiceContext(realmIAQBO, IntuitServicesType.QBO, oAuthRequestValidator);
        }

        /// <summary>
        ///A test for DataService Constructor
        ///</summary>
        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void DataServiceConstructorNullParameterTest()
        {
            ServiceContext serviceContext = null;
            DataService target = new DataService(serviceContext);
            Assert.Fail();
        }

        [TestMethod()]
        public void DataServiceConstructorPositiveTest()
        {
            DataService target = new DataService(context);
            Assert.IsNotNull(target);
        }
    }
}
