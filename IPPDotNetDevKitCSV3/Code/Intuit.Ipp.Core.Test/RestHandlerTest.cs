using System.Net;
using Intuit.Ipp.Core.Rest;
using Intuit.Ipp.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.Utility;
using Intuit.Ipp.Core.Test.Common;

namespace Intuit.Ipp.Core.Test
{
    /// <summary>
    ///This is a test class for RestHandlerTest and is intended
    ///to contain all RestHandlerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RestHandlerTest
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

        [TestMethod]
        public void PreparRequestSuccessTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            IRestHandler handler = new SyncRestHandler(serviceContext);
            string resourceUri = string.Format("v3/company/{0}/customer", serviceContext.RealmId);
           

            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);

           

            HttpWebRequest request = handler.PrepareRequest(parameters, new Customer());

            
            string endpointUri = string.Format("{0}{1}", serviceContext.BaseUrl, resourceUri);

          
            Assert.AreEqual(endpointUri.Trim(), request.RequestUri.ToString().Replace(request.RequestUri.Query, "").Trim());
            Assert.AreEqual(parameters.Verb.ToString(), request.Method.ToString());

           

            Assert.AreEqual(parameters.ContentType, request.ContentType);
        }

        [TestMethod]
        public void PreparRequestSuccessNullRequestBodyTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            SyncRestHandler handler = new SyncRestHandler(serviceContext);
            string resourceUri = string.Format("v3/company/{0}/customer", serviceContext.RealmId);
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            HttpWebRequest request = handler.PrepareRequest(parameters, null);
            string endpointUri = string.Format("{0}{1}", serviceContext.BaseUrl, resourceUri);
            Assert.AreEqual(endpointUri.Trim(), request.RequestUri.ToString().Replace(request.RequestUri.Query, "").Trim());
            Assert.AreEqual(parameters.Verb.ToString(), request.Method.ToString());
            Assert.AreEqual(parameters.ContentType, request.ContentType);
        }

        [TestMethod]
        public void PreparRequestSuccessStringRequestBodyTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            IRestHandler handler = new SyncRestHandler(serviceContext);
            string resourceUri = string.Format("v3/company/{0}/customer", serviceContext.RealmId);

           
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            HttpWebRequest request = handler.PrepareRequest(parameters, "Query *");
           
            string endpointUri = string.Format("{0}{1}", serviceContext.BaseUrl, resourceUri);

            Assert.AreEqual(endpointUri.Trim(), request.RequestUri.ToString().Replace(request.RequestUri.Query, "").Trim());
            Assert.AreEqual(parameters.Verb.ToString(), request.Method.ToString());
            Assert.AreEqual(parameters.ContentType, request.ContentType);
        }

        [TestMethod]
        public void PreparRequestGZipCompressionTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            serviceContext.IppConfiguration.Message.Request.CompressionFormat =  Configuration.CompressionFormat.GZip;
            serviceContext.IppConfiguration.Message.Response.CompressionFormat = Configuration.CompressionFormat.GZip;
            IRestHandler handler = new SyncRestHandler(serviceContext);
            string resourceUri = string.Format("v3/company/{0}/customer", serviceContext.RealmId);
            RequestParameters parameters = new RequestParameters(resourceUri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            HttpWebRequest request = handler.PrepareRequest(parameters, new Customer());
            Assert.AreEqual(DataCompressionFormat.GZip.ToString().ToLowerInvariant(), request.Headers[CoreConstants.CONTENTENCODING]);
            Assert.AreEqual(DataCompressionFormat.GZip.ToString().ToLowerInvariant(), request.Headers[CoreConstants.ACCEPTENCODING]);
        }
    }
}
