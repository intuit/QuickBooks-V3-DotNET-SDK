

namespace Intuit.Ipp.EntitlementService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Intuit.Ipp.Core;
    using Intuit.Ipp.Core.Rest;
    using Intuit.Ipp.Core.Configuration;
    using Intuit.Ipp.Data;
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Exception;
    using System.Globalization;
    using System.Net;
    using Intuit.Ipp.Utility;

    public class EntitlementService : IEntitlementService
    {
        private ServiceContext serviceContext;
        private IRestHandler restHandler;
        private Core.Configuration.SerializationFormat orginialSerializationFormat;

        public EntitlementService(ServiceContext serviceContext)
        {

            //CommonHelper.ServiceContextValidation(serviceContext);
            this.serviceContext = serviceContext;
            restHandler = new SyncRestHandler(this.serviceContext);
        }

        #region Sync Methods
        public EntitlementsResponse GetEntitlements(string entitlementBaseUrl = CoreConstants.ENTITLEMENT_BASEURL)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method GetEntitlements.");
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/entitlements/{1}/{2}", entitlementBaseUrl, CoreConstants.VERSION, serviceContext.RealmId);

            orginialSerializationFormat = this.serviceContext.IppConfiguration.Message.Response.SerializationFormat;
            
            // Only XML format is supported by Entitlements API
            serviceContext.IppConfiguration.Message.Response.SerializationFormat = Core.Configuration.SerializationFormat.Xml;
            // Creates request parameters
            RequestParameters parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);

            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, null, uri);

            string response = string.Empty;
            try
            {
                // gets response
                response = this.restHandler.GetResponse(request);
            }
            catch (IdsException ex)
            {
                IdsExceptionManager.HandleException(ex);
            }

            CoreHelper.CheckNullResponseAndThrowException(response);

            // de serialize object
            EntitlementsResponse restResponse = (EntitlementsResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<EntitlementsResponse>(response);
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method GetEntitlements.");

            // change Response Serialization Format back to Config value
            serviceContext.IppConfiguration.Message.Response.SerializationFormat = orginialSerializationFormat;

            return restResponse;
        }
        #endregion

        #region Async Methods
        public void GetEntitlementsAsync(string entitlementBaseUrl = CoreConstants.ENTITLEMENT_BASEURL)
        {
            Console.Write("GetEntitlementsAsync started \n");
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method GetEntitlements Asynchronously.");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(this.serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(this.GetEntitlementsAsyncCompleted);

            EntitlementCallCompletedEventArgs<Intuit.Ipp.Data.EntitlementsResponse> entitlementCallCompletedEventArgs = new EntitlementCallCompletedEventArgs<Intuit.Ipp.Data.EntitlementsResponse>();
            try
            {
                string uri = string.Format(CultureInfo.InvariantCulture, "{0}/entitlements/{1}/{2}", entitlementBaseUrl, CoreConstants.VERSION, serviceContext.RealmId);

                orginialSerializationFormat = this.serviceContext.IppConfiguration.Message.Response.SerializationFormat;
                // Only XML format is supported by Entitlements API
                serviceContext.IppConfiguration.Message.Response.SerializationFormat = Core.Configuration.SerializationFormat.Xml;
                // Creates request parameters
                RequestParameters parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);

                HttpWebRequest request = asyncRestHandler.PrepareRequest(parameters, null, uri);
                asyncRestHandler.GetResponse(request);

            }
            catch (SystemException systemException)
            {
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                entitlementCallCompletedEventArgs.Error = idsException;
                this.OnGetEntilementAsyncCompleted(this, entitlementCallCompletedEventArgs);
            }
        }
        #endregion

        public event EntitlementServiceCallback<Intuit.Ipp.Data.EntitlementsResponse>.EntitlementCallCompletedEventHandler OnGetEntilementAsyncCompleted;

        private void GetEntitlementsAsyncCompleted(object sender, AsyncCallCompletedEventArgs eventArgs)
        {
            EntitlementCallCompletedEventArgs<Intuit.Ipp.Data.EntitlementsResponse> entitlementCallCompletedEventArgs = new EntitlementCallCompletedEventArgs<EntitlementsResponse>();
            if (eventArgs.Error == null)
            {
                try
                {
                    IEntitySerializer responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);
                    EntitlementsResponse restResponse = (EntitlementsResponse)responseSerializer.Deserialize<EntitlementsResponse>(eventArgs.Result);
                    entitlementCallCompletedEventArgs.EntitlementsResponse = restResponse;
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing event GetEntitlementsAsync in AyncService object.");

                    // change Response Serialization Format back to Config value
                    serviceContext.IppConfiguration.Message.Response.SerializationFormat = orginialSerializationFormat;

                    this.OnGetEntilementAsyncCompleted(this, entitlementCallCompletedEventArgs);
                }
                catch (SystemException systemException)
                {
                    IdsException idsException = new IdsException(systemException.Message);
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    entitlementCallCompletedEventArgs.Error = idsException;
                    this.OnGetEntilementAsyncCompleted(this, entitlementCallCompletedEventArgs);
                }
            }
            else
            {
                entitlementCallCompletedEventArgs.Error = eventArgs.Error;
                this.OnGetEntilementAsyncCompleted(this, entitlementCallCompletedEventArgs);
            }
        }
    }
}
