using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.Data;
using Intuit.Ipp.Core;
using Intuit.Ipp.Security;

namespace Intuit.Ipp.EntitlementService.Test
{
    [TestClass]
    public class EntitlementServiceTest
    {
        private static EntitlementService entitlementServiceTestCases;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {   
            string accessTokenQBO = ConfigurationManager.AppSettings["AccessTokenQBO"];
            string accessTokenSecretQBO = ConfigurationManager.AppSettings["AccessTokenSecretQBO"];
            string consumerKeyQBO = ConfigurationManager.AppSettings["ConsumerKeyQBO"];
            string ConsumerSecretQBO = ConfigurationManager.AppSettings["ConsumerSecretQBO"];
            string realmIAQBO = ConfigurationManager.AppSettings["RealmIAQBO"];
            OAuthRequestValidator oAuthRequestValidator = new OAuthRequestValidator(accessTokenQBO, accessTokenSecretQBO, consumerKeyQBO, ConsumerSecretQBO);
            ServiceContext context = new ServiceContext(realmIAQBO, IntuitServicesType.QBO, oAuthRequestValidator);
            
            entitlementServiceTestCases = new EntitlementService(context);

        }

        [TestMethod][Ignore]
        public void GetEntitlementsTest()
        {
            try
            {
                EntitlementsResponse entitlements = entitlementServiceTestCases.GetEntitlements("https://qbo.sbfinance.intuit.com/manage");
                Assert.IsNotNull(entitlements);

            }
            catch (SystemException ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
    }
}
