using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.Data;
using Intuit.Ipp.Core;
using Intuit.Ipp.Security;
using Intuit.Ipp.EntitlementService.Test.Common;

namespace Intuit.Ipp.EntitlementService.Test
{
    [TestClass]
    public class EntitlementServiceTest
    {
        private static EntitlementService entitlementServiceTestCases;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            ServiceContext context = Initializer.InitializeServiceContextQbo(); 
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
