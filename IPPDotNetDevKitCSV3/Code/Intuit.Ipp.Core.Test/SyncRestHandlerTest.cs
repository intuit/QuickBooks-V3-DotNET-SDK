using System;
using System.Collections.Generic;
using System.Net;
using Intuit.Ipp.Core.Rest;
using Intuit.Ipp.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.Exception;
//using Intuit.Ipp.Retry;
using Intuit.Ipp.Core.Test.Common;
using Intuit.Ipp.Utility;

namespace Intuit.Ipp.Core.Test
{
    /// <summary>
    ///This is a test class for SyncRestHandlerTest and is intended
    ///to contain all SyncRestHandlerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SyncRestHandlerTest
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

        [ClassInitialize]
        public static void SyncRestHandlerInitialize(TestContext testContext)
        {
        }

        ///// <summary>
        ///// Private constructor test
        ///// </summary>
        //[TestMethod]
        //public void SyncRestHandlerEmptyConstructorTest()
        //{
        //    SyncRestHandler_Accessor actual = new SyncRestHandler_Accessor();
        //}

        

        /// <summary>
        /// Prepare request with updated QBO BaseURL.
        /// </summary>
        [TestMethod]
        public void SyncRestHandlerPrepareRequestUpdateBaseURLQBOTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            SyncRestHandler handler = new SyncRestHandler(serviceContext);
            string resourceUri = string.Format("v3/company/{0}/customer", serviceContext.RealmId);
            serviceContext.IppConfiguration.BaseUrl.Qbo = "http://www.intuit.com/";
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            HttpWebRequest request = handler.PrepareRequest(parameters, new Customer());
            string endpointUri = string.Format("{0}{1}", "http://www.intuit.com/", resourceUri);

            Assert.AreEqual(endpointUri.Trim(), request.RequestUri.ToString().Replace(request.RequestUri.Query, "").Trim());
            Assert.AreEqual(parameters.Verb.ToString(), request.Method.ToString());
            Assert.AreEqual(parameters.ContentType, request.ContentType);
        }

        /// <summary>
        /// Test case for get response
        /// </summary>
        [TestMethod]
        public void GetResponseSuccessTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            IRestHandler handler = new SyncRestHandler(serviceContext);
            string AccountId = "1";
            string resourceUri = string.Format("v3/company/{0}/account/{1}", serviceContext.RealmId, AccountId);
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_TEXTXML);
            HttpWebRequest request = handler.PrepareRequest(parameters, null);
            string response = handler.GetResponse(request);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(response));
        }

        /// <summary>
        /// Test case for Get response with Retry policy
        /// </summary>
        [TestMethod]
        public void GetResponseWithRetrySuccessTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            serviceContext.IppConfiguration.RetryPolicy = new IntuitRetryPolicy(3, TimeSpan.FromSeconds(2));
            IRestHandler handler = new SyncRestHandler(serviceContext);
            string AccountId = "1";
            string resourceUri = string.Format("v3/company/{0}/account/{1}", serviceContext.RealmId, AccountId);
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            HttpWebRequest request = handler.PrepareRequest(parameters, null);
            string response = handler.GetResponse(request);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(response));
        }

       
        /// <summary>
        /// Generates status code 401
        /// </summary>
        [TestMethod][Ignore]//no exception for oauth2 as we try to refresh token everytime
        [ExpectedException(typeof(InvalidTokenException))]
        public void GetResponseInvalidTokenExceptionTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            IRestHandler handler = new SyncRestHandler(serviceContext);
            string resourceUri = string.Format("v3/company/{0}/customer", AuthorizationKeysQBO.realmIdIAQBO);
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            Customer customer = new Customer();
            HttpWebRequest request = handler.PrepareRequest(parameters, customer);
            string response = handler.GetResponse(request);
        }

        

        /// <summary>
        /// Test case for SyncRestHandler Timeout=10 expected Timeout 
        /// </summary>
        [TestMethod]
        public void GetResponseTimeoutTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            //Set timeout 10 milliseconds
            serviceContext.Timeout = 10;
            IRestHandler handler = new SyncRestHandler(serviceContext);
            string AccountId = "1";
            string resourceUri = string.Format("v3/company/{0}/account/{1}", serviceContext.RealmId, AccountId);
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_TEXTXML);
            HttpWebRequest request = handler.PrepareRequest(parameters, null);
            string response = handler.GetResponse(request);
            Assert.IsTrue(string.IsNullOrWhiteSpace(response));
        }

        /// <summary>
        /// Test case for SyncRestHandler Timeout=200 seconds. No timeout
        /// </summary>
        [TestMethod]
        public void GetResponseTimeoutNoTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            //Set timeout 200 seconds
            serviceContext.Timeout = 200000; 
            IRestHandler handler = new SyncRestHandler(serviceContext);
            string AccountId = "1";
            string resourceUri = string.Format("v3/company/{0}/account/{1}", serviceContext.RealmId, AccountId);
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_TEXTXML);
            HttpWebRequest request = handler.PrepareRequest(parameters, null);
            string response = handler.GetResponse(request);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(response));
        }

        [TestMethod]
        public void GetResponseStreamSuccessTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            IRestHandler handler = new SyncRestHandler(serviceContext);
            List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(serviceContext, new SalesReceipt());
            Assert.IsTrue(salesReceipts.Count > 0);
            string resourceUri = string.Format("v3/company/{0}/salesreceipt/{1}/pdf", serviceContext.RealmId, salesReceipts[0].Id);
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            HttpWebRequest request = handler.PrepareRequest(parameters, null,includeRequestId: false);
            request.Accept = CoreConstants.CONTENTTYPE_APPLICATIONPDF;
            byte[] response = handler.GetResponseStream(request);
            Assert.IsTrue(response.Length > 0);
        }

        [TestMethod]
        public void GetResponseStreamCompressedSuccessTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            serviceContext.IppConfiguration.Message.Response.CompressionFormat = Configuration.CompressionFormat.GZip;
            IRestHandler handler = new SyncRestHandler(serviceContext);
            List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(serviceContext, new SalesReceipt());
            Assert.IsTrue(salesReceipts.Count > 0);
            string resourceUri = string.Format("v3/company/{0}/salesreceipt/{1}/pdf", serviceContext.RealmId, salesReceipts[0].Id);
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            HttpWebRequest request = handler.PrepareRequest(parameters, null, includeRequestId: false);
            request.Accept = CoreConstants.CONTENTTYPE_APPLICATIONPDF;
            byte[] response = handler.GetResponseStream(request);
            Assert.IsTrue(response.Length > 0);
        }

        [TestMethod]
        public void GetResponseStreamRetrySuccessTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            serviceContext.IppConfiguration.RetryPolicy = new IntuitRetryPolicy(3, new TimeSpan(0, 0, 5));
            IRestHandler handler = new SyncRestHandler(serviceContext);
            List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(serviceContext, new SalesReceipt());
            Assert.IsTrue(salesReceipts.Count > 0);
            string resourceUri = string.Format("v3/company/{0}/salesreceipt/{1}/pdf", serviceContext.RealmId, salesReceipts[0].Id);
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            HttpWebRequest request = handler.PrepareRequest(parameters, null, includeRequestId: false);
            request.Accept = CoreConstants.CONTENTTYPE_APPLICATIONPDF;
            byte[] response = handler.GetResponseStream(request);
            Assert.IsTrue(response.Length > 0);
        }

        [TestMethod]
        public void GetResponseStreamFailureTest()
        {
            try
            {
                ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
                IRestHandler handler = new SyncRestHandler(serviceContext);
                string AccountId = "1";
                string resourceUri = string.Format("v3/company/{0}/salesreceipt/3/", serviceContext.RealmId);
                RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);
                HttpWebRequest request = handler.PrepareRequest(parameters, null, includeRequestId: false);
                request.Accept = CoreConstants.CONTENTTYPE_APPLICATIONPDF;
                byte[] response = handler.GetResponseStream(request);
                Assert.IsTrue(response.Length > 0);
            }
            catch (IdsException idsEx)
            {
                Assert.IsNotNull(idsEx);
                
            }
            
        }

    }
}
