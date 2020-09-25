using System;
//using Intuit.Ipp.Retry;
using Intuit.Ipp.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using Intuit.Ipp.Exception;
using Intuit.Ipp.Core.Test.Common;

namespace Intuit.Ipp.Core.Test
{
    /// <summary>
    ///This is a test class for ServiceContextTest and is intended
    ///to contain all ServiceContextTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ServiceContextTest
    {
        private TestContext testContextInstance;

        

        //private data members that is used for QBO related operations
        private string accessTokenQbo = string.Empty;
        private string accessTokenSecretQbo = string.Empty;
        private string consumerKeyQbo = string.Empty;
        private string consumerSecretQbo = string.Empty;
        private string realmIdIAQbo = string.Empty;      
        private string appTokenQbo = string.Empty;    
        private string userNameQbo = string.Empty;
        private string passwordQbo = string.Empty;
   

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

        public ServiceContextTest()
        {
       

            //initialising private data members that is used for QBO related operations
            accessTokenQbo = ConfigurationManager.AppSettings["AccessTokenQBO"];
            accessTokenSecretQbo = ConfigurationManager.AppSettings["AccessTokenSecretQBO"];
            consumerKeyQbo = ConfigurationManager.AppSettings["ConsumerKeyQBO"];
            consumerSecretQbo = ConfigurationManager.AppSettings["ConsumerSecretQBO"];
            realmIdIAQbo = ConfigurationManager.AppSettings["RealmIAQBO"];            
            appTokenQbo = ConfigurationManager.AppSettings["AppTokenQBO"];            
            userNameQbo = ConfigurationManager.AppSettings["UserNameQBO"];
            passwordQbo = ConfigurationManager.AppSettings["PasswordQBO"];
            
        }

        #region Intuit Anywhere tests

      

        /// <summary>
        ///A test for ServiceContext Constructor
        ///</summary>
        [TestMethod()]
        public void ServiceContextConstructorForQBOTest()
        {
            try
            {
                //OAuthRequestValidator oauthValidator = new OAuthRequestValidator(accessTokenQbo, accessTokenSecretQbo, consumerKeyQbo, consumerSecretQbo);
                //OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator("bearertoken");
                //ServiceContext context = new ServiceContext(realmIdIAQbo, IntuitServicesType.QBO, oauthValidator);
                ServiceContext context = Initializer.InitializeServiceContextQbo();
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        

        /// <summary>
        ///A test for ServiceContext Constructor
        ///</summary>
        [TestMethod()][Ignore]
        public void ServiceContextConstructorWithAppTokenForQBOTest()
        {
            try
            {
                //OAuthRequestValidator oauthValidator = new OAuthRequestValidator(accessTokenQbo, accessTokenSecretQbo, consumerKeyQbo, consumerSecretQbo);
                OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator("bearertoken");
                ServiceContext context = new ServiceContext(appTokenQbo, realmIdIAQbo, IntuitServicesType.QBO, oauthValidator);
                
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for ServiceContext Constructor
        ///</summary>
        [TestMethod()][Ignore]
        [ExpectedException(typeof(InvalidTokenException))]
        public void ServiceContextConstructorWithAppTokenNullValidatorTest()
        {
            ServiceContext context = new ServiceContext(appTokenQbo, realmIdIAQbo, IntuitServicesType.QBO, null);
        }

        /// <summary>
        ///A test for ServiceContext Constructor
        ///</summary>
        [TestMethod()]
        public void ServiceContextConstructorWithAppTokenNullAppTokenTest()
        {
            try
            {
                //OAuthRequestValidator oauthValidator = new OAuthRequestValidator(accessTokenQbo, accessTokenSecretQbo, consumerKeyQbo, consumerSecretQbo);
                OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator("bearertoken");
                ServiceContext context = new ServiceContext(null, realmIdIAQbo, IntuitServicesType.QBO, oauthValidator);
            }
            catch (System.Exception ex)
            {
                if (ex.InnerException == null || ex.InnerException.GetType() != typeof(ArgumentNullException))
                {
                    Assert.Fail(ex.ToString());
                }
            }
        }

        /// <summary>
        ///A test for ServiceContext Constructor
        ///</summary>
        [TestMethod()][Ignore]
        public void ServiceContextConstructorWithAppTokenNullRealmTest()
        {
            try
            {
                //OAuthRequestValidator oauthValidator = new OAuthRequestValidator(accessTokenQbo, accessTokenSecretQbo, consumerKeyQbo, consumerSecretQbo);
                OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator("bearertoken");
                ServiceContext context = new ServiceContext(appTokenQbo, null, IntuitServicesType.QBO, oauthValidator);
            }
            catch (InvalidRealmException) { }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for ServiceContext timeout Set and Get
        ///</summary>
        [TestMethod()]
        public void ServiceContextTimeoutTest()
        {
            try
            {
                //OAuthRequestValidator oauthValidator = new OAuthRequestValidator(accessTokenQbo, accessTokenSecretQbo, consumerKeyQbo, consumerSecretQbo);
                ServiceContext context = Initializer.InitializeServiceContextQbo();
                context.Timeout = 100;
                Assert.AreEqual(100, context.Timeout);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for ServiceContext timeout Set and Get Null
        ///</summary>
        [TestMethod()]
        public void ServiceContextTimeoutNullTest()
        {
            try
            {
                //OAuthRequestValidator oauthValidator = new OAuthRequestValidator(accessTokenQbo, accessTokenSecretQbo, consumerKeyQbo, consumerSecretQbo);
                ServiceContext context = Initializer.InitializeServiceContextQbo();
                context.Timeout = null;
                Assert.IsNull(context.Timeout);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        #endregion
        
 
    }
}
