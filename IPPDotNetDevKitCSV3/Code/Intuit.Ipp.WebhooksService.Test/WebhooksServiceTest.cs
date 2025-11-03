using System;
using Intuit.Ipp.WebhooksService;
using Intuit.Ipp.Exception;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Intuit.Ipp.WebhooksService.Test
{

    using Intuit.Ipp.Core;
    using Intuit.Ipp.Utility;
    using System.Collections.Generic;

    [TestClass]
    public class WebhooksServiceTest
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

       private static WebhooksService webhooksServiceTestCases;

       [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            webhooksServiceTestCases = new WebhooksService();

        }

        [TestMethod]
        public void ExecuteverifyToken()
        {
            try
            {

                string intuitHeaderSignature ="6q+xGyMirJupwKTh2/WBl2NcdxzxqdIbk3sneKTxLWM=";
                string payload = "{\"eventNotifications\":[{\"realmId\":\"1269959970\",\"dataChangeEvent\":{\"entities\":[{\"name\":\"Vendor\",\"id\":\"40\",\"operation\":\"Update\",\"lastUpdated\":\"2016-10-05T03:09:04.000Z\"},{\"name\":\"Customer\",\"id\":\"43\",\"operation\":\"Create\",\"lastUpdated\":\"2016-10-05T03:07:04.000Z\"}]}}]}";
                bool isWebhooksOk = webhooksServiceTestCases.VerifyPayload(intuitHeaderSignature, payload);

                Assert.AreEqual(isWebhooksOk, true);



            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }


        }

        [TestMethod]
        public void GetWebooksEvents()
        {
            try
            {

                string wehooksResponsePayload = "{\"eventNotifications\":[{\"realmId\":\"1269959970\",\"dataChangeEvent\":{\"entities\":[{\"name\":\"Vendor\",\"id\":\"40\",\"operation\":\"Update\",\"lastUpdated\":\"2016-10-05T03:09:04.000Z\"},{\"name\":\"Customer\",\"id\":\"43\",\"operation\":\"Create\",\"lastUpdated\":\"2016-10-05T03:07:04.000Z\"}]}}]}";

                WebhooksEvent webhooksEvent = webhooksServiceTestCases.GetWebooksEvents(wehooksResponsePayload);

                Assert.AreEqual(webhooksEvent.EventNotifications[0].RealmId, "1269959970");
                Assert.AreEqual(webhooksEvent.EventNotifications[0].DataChangeEvent.Entities[0].Name, "Vendor");




            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }


        }

        [TestMethod]
        public void GetWebhooksCloudEvents()
        {
            try
            {

                string wehooksResponsePayloadNew = "[{\"specversion\":\"1.0\",\"id\":\"d1a3aedd-9670-41bf-a4f9-c148a1cc4e03\",\"source\":\"intuit.dsnBgbseACLLRZNxo2dfc4evmEJdxde58xeeYcZliOU=\",\"type\":\"qbo.class.created.v1\",\"time\":\"2025-10-07T19:59:07.034359333Z\",\"intuitentityid\":\"1234\",\"intuitaccountid\":\"310687\"}]";

                List<WebhooksCloudEvent> webhooksEvent = webhooksServiceTestCases.GetWebhooksCloudEvents(wehooksResponsePayloadNew);

                Assert.AreEqual(webhooksEvent[0].SpecVersion, "1.0");
                Assert.AreEqual(webhooksEvent[0].Id, "d1a3aedd-9670-41bf-a4f9-c148a1cc4e03");

            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }


        }
    }
}
