using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using Intuit.Ipp.Core.Rest;
using Intuit.Ipp.Core.Test.Common;
using Intuit.Ipp.Data;
using Intuit.Ipp.Exception;
//using Intuit.Ipp.Retry;
using Intuit.Ipp.Security;
using Intuit.Ipp.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Intuit.Ipp.Core.Test
{
    /// <summary>
    ///This is a test class for AsyncRestHandlerTest and is intended
    ///to contain all AsyncRestHandlerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AsyncRestHandlerTest
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

        ///// <summary>
        ///// Private constructor test
        ///// </summary>
        //[TestMethod]
        //public void AsyncRestHandlerEmptyConstructorTest()
        //{
        //    AsyncRestHandler_Accessor actual = new AsyncRestHandler_Accessor();
        //}


        [TestMethod]
        public void GetResponseSuccessTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            AsyncRestHandler handler = new AsyncRestHandler(serviceContext);
            string query = "select * from Account startPosition 1 maxResults 10";
            string resourceUri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/query", CoreConstants.VERSION, serviceContext.RealmId);

            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONTEXT);
            HttpWebRequest request = handler.PrepareRequest(parameters, query);
            GetResponseHelper(handler, request);
        }

        [TestMethod]
        public void GetResponseCompressedSuccessTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            serviceContext.IppConfiguration.Message.Response.CompressionFormat = Configuration.CompressionFormat.GZip;
            AsyncRestHandler handler = new AsyncRestHandler(serviceContext);
            string query = "select * from Account startPosition 1 maxResults 10";
            string resourceUri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/query", CoreConstants.VERSION, serviceContext.RealmId);
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONTEXT);
            HttpWebRequest request = handler.PrepareRequest(parameters, query);
            GetResponseHelper(handler, request);
        }

        [TestMethod]
        public void GetResponseStreamSuccessTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            AsyncRestHandler handler = new AsyncRestHandler(serviceContext);
            List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(serviceContext, new SalesReceipt());
            Assert.IsTrue(salesReceipts.Count > 0);
            string resourceUri = string.Format("v3/company/{0}/salesreceipt/{1}/pdf", serviceContext.RealmId, salesReceipts[0].Id);
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            HttpWebRequest request = handler.PrepareRequest(parameters, null, includeRequestId: false);
            request.Accept = CoreConstants.CONTENTTYPE_APPLICATIONPDF;
            GetResponseStreamHelper(handler, request);
        }
        [TestMethod]
        public void GetResponseStreamCompressedSuccessTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            serviceContext.IppConfiguration.Message.Response.CompressionFormat = Configuration.CompressionFormat.GZip;
            AsyncRestHandler handler = new AsyncRestHandler(serviceContext);
            List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(serviceContext, new SalesReceipt());
            Assert.IsTrue(salesReceipts.Count > 0);
            string resourceUri = string.Format("v3/company/{0}/salesreceipt/{1}/pdf", serviceContext.RealmId, salesReceipts[0].Id);
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            HttpWebRequest request = handler.PrepareRequest(parameters, null, includeRequestId: false);
            request.Accept = CoreConstants.CONTENTTYPE_APPLICATIONPDF;
            GetResponseStreamHelper(handler, request);
        }
        [TestMethod]
        public void GetResponseStreamRetrySuccessTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            serviceContext.IppConfiguration.RetryPolicy = new IntuitRetryPolicy(3, new TimeSpan(0, 0, 5));
            AsyncRestHandler handler = new AsyncRestHandler(serviceContext);
            List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(serviceContext, new SalesReceipt());
            Assert.IsTrue(salesReceipts.Count > 0);
            string resourceUri = string.Format("v3/company/{0}/salesreceipt/{1}/pdf", serviceContext.RealmId, salesReceipts[0].Id);
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            HttpWebRequest request = handler.PrepareRequest(parameters, null, includeRequestId: false);
            request.Accept = CoreConstants.CONTENTTYPE_APPLICATIONPDF;
            GetResponseStreamHelper(handler, request);
        }

        [TestMethod]
        public void GetResponseStreamFailureTest()
        {
            try
            {
                ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
                AsyncRestHandler handler = new AsyncRestHandler(serviceContext);
                List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(serviceContext, new SalesReceipt());
                Assert.IsTrue(salesReceipts.Count > 0);
                string resourceUri = string.Format("v3/company/{0}/salesreceipt/{1}", serviceContext.RealmId, salesReceipts[0].Id);
                RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);
                HttpWebRequest request = handler.PrepareRequest(parameters, null, includeRequestId: false);
                request.Accept = CoreConstants.CONTENTTYPE_APPLICATIONPDF;
                GetResponseStreamHelper(handler, request);
            }
            catch (IdsException idsEx)
            {

                Assert.IsNotNull(idsEx);
            }
        }

        private void GetResponseStreamHelper(AsyncRestHandler handler, HttpWebRequest request, bool generatesException = false)
        {
            IdsException exception = null;
            byte[] response = new byte[0];
            handler.OnCallCompleted += (sender, e) =>
            {
                if (e.Error == null)
                {
                    response = e.ByteResult;
                }
                else
                {
                    exception = e.Error;
                }
            };

            handler.GetResponseStream(request);
            System.Threading.Thread.Sleep(10000);
            if (exception != null)
            {
                if (!generatesException)
                {
                    //Assert.Fail(exception.ToString());
                    throw exception;
                }
            }
            else
            {
                Assert.IsTrue(response.Length > 0);
            }
        }

        private void GetResponseHelper(AsyncRestHandler handler, HttpWebRequest request, bool generatesException = false)
        {
            IdsException exception = null;
            string response = null;
            handler.OnCallCompleted += (sender, e) =>
            {
                if (e.Error == null)
                {
                    response = e.Result;
                }
                else
                {
                    exception = e.Error;
                }
            };

            handler.GetResponse(request);
            System.Threading.Thread.Sleep(10000);
            if (exception != null)
            {
                if (!generatesException)
                {
                    Assert.Fail(exception.ToString());
                }
            }
            else
            {
                Assert.IsTrue(!string.IsNullOrWhiteSpace(response));
            }
        }

        [TestMethod]
        public void GetResponseWithRetryPolicySuccessTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            serviceContext.IppConfiguration.RetryPolicy = new IntuitRetryPolicy(2, TimeSpan.FromSeconds(2));
            AsyncRestHandler handler = new AsyncRestHandler(serviceContext);
            string AccountId = "1";
            string resourceUri = string.Format("v3/company/{0}/account/{1}", serviceContext.RealmId, AccountId);
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_TEXTXML);
            HttpWebRequest request = handler.PrepareRequest(parameters, null);
            GetResponseHelper(handler, request);
        }

        

        /// <summary>
        /// Generates status code 401
        /// </summary>
        [TestMethod]        
        public void GetResponseInvalidTokenExceptionTest()
        {
            //OAuthRequestValidator validator = new OAuthRequestValidator("adfas", "afd", "adfas", "asdfa");
            OAuth2RequestValidator validator = new OAuth2RequestValidator("bearertoken");
            string realmId = AuthorizationKeysQBO.realmIdIAQBO;
            ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, validator);
            AsyncRestHandler handler = new AsyncRestHandler(serviceContext);
            string resourceUri = string.Format("v3/company/{0}/customer", serviceContext.RealmId);
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            Customer customer = new Customer();
            HttpWebRequest request = handler.PrepareRequest(parameters, customer);
            GetResponseHelper(handler, request, true);
        }

       
    }
}
