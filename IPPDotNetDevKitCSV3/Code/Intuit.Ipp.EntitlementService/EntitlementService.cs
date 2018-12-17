////*********************************************************
// <copyright file="EntitlementService.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2018 Intuit
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/
// <summary>This file contains EntitlementService which performs Get operations on V3 Entitlements endpoints.</summary>
////*********************************************************
namespace Intuit.Ipp.EntitlementService
{
    using System;
    using Intuit.Ipp.Core;
    using Intuit.Ipp.Core.Rest;
    using Intuit.Ipp.Data;
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Exception;
    using System.Globalization;
    using System.Net;
    using Intuit.Ipp.Utility;
    using Intuit.Ipp.EntitlementService.Properties;

    /// <summary>
    /// This class file contains EntitlementService which performs Get operation for Entitlements
    /// </summary>
    public class EntitlementService : IEntitlementService
    {
        /// <summary>
        /// The Service context object.
        /// </summary>
        private ServiceContext serviceContext;

        /// <summary>
        /// Rest Request Handler.
        /// </summary>
        private IRestHandler restHandler;

        /// <summary>
        /// Serialization Format 
        /// </summary>
        private Core.Configuration.SerializationFormat orginialSerializationFormat;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitlementService"/> class.
        /// </summary>
        /// <param name="serviceContext"></param>
        public EntitlementService(ServiceContext serviceContext)
        {

            ServiceContextValidation(serviceContext);
            this.serviceContext = serviceContext;
            restHandler = new SyncRestHandler(this.serviceContext);
            // Set the Service type to QBO by calling a method.
            this.serviceContext.UseDataServices();
        }

        #region Sync Method

        /// <summary>
        /// Gets entitlements against a specified realm. The realm must be set in the context.
        /// </summary>
        /// <param name="entitlementBaseUrl">Base Url of the Entitlements API for OAuth1 vs OAuth2. Default is set to OAuth2 prod environment.</param>
        /// <returns>Returns EntitlementsResponse</returns>
        public EntitlementsResponse GetEntitlements(string entitlementBaseUrl = Utility.CoreConstants.ENTITLEMENT_BASEURL)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method GetEntitlements.");
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/entitlements/{1}/{2}", entitlementBaseUrl, Utility.CoreConstants.VERSION, serviceContext.RealmId);

            orginialSerializationFormat = this.serviceContext.IppConfiguration.Message.Response.SerializationFormat;
            
            // Only XML format is supported by Entitlements API
            serviceContext.IppConfiguration.Message.Response.SerializationFormat = Core.Configuration.SerializationFormat.Xml;
            // Creates request parameters
            RequestParameters parameters = new RequestParameters(uri, HttpVerbType.GET, Utility.CoreConstants.CONTENTTYPE_APPLICATIONXML);

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

        #region Async Method
        /// <summary>
        /// Gets entitlements against a specified realm. The realm must be set in the context.
        /// </summary>
        /// <param name="entitlementBaseUrl">Base Url of the Entitlements API for OAuth1 vs OAuth2. Default is set to OAuth2 prod environment</param>
        /// <returns>Returns EntitlementsResponse</returns>
        public void GetEntitlementsAsync(string entitlementBaseUrl = Utility.CoreConstants.ENTITLEMENT_BASEURL)
        {
            Console.Write("GetEntitlementsAsync started \n");
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method GetEntitlements Asynchronously.");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(this.serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(this.GetEntitlementsAsyncCompleted);

            EntitlementCallCompletedEventArgs<Intuit.Ipp.Data.EntitlementsResponse> entitlementCallCompletedEventArgs = new EntitlementCallCompletedEventArgs<Intuit.Ipp.Data.EntitlementsResponse>();
            try
            {
                string uri = string.Format(CultureInfo.InvariantCulture, "{0}/entitlements/{1}/{2}", entitlementBaseUrl, Utility.CoreConstants.VERSION, serviceContext.RealmId);

                orginialSerializationFormat = this.serviceContext.IppConfiguration.Message.Response.SerializationFormat;
                // Only XML format is supported by Entitlements API
                serviceContext.IppConfiguration.Message.Response.SerializationFormat = Core.Configuration.SerializationFormat.Xml;
                // Creates request parameters
                RequestParameters parameters = new RequestParameters(uri, HttpVerbType.GET, Utility.CoreConstants.CONTENTTYPE_APPLICATIONXML);

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

        #region Async helpers
        /// <summary>
        /// Gets or sets the call back event for Get Entitlements method in asynchronous call.
        /// </summary>
        /// <value>
        /// The OnGetEntilementAsyncCompleted call back.
        /// </value>
        public event EntitlementServiceCallback<Intuit.Ipp.Data.EntitlementsResponse>.EntitlementCallCompletedEventHandler OnGetEntilementAsyncCompleted;

        /// <summary>
        /// GetEntitlements Asynchronous call back method
        /// </summary>
        /// <param name="sender">Rest handler class</param>
        /// <param name="eventArgs">callback event arguments</param>
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

        #endregion

        #region Helper 
        /// <summary>
        /// Validates the Service context.
        /// </summary>
        /// <param name="serviceContext">Service Context.</param>
        internal static void ServiceContextValidation(ServiceContext serviceContext)
        {
            if (serviceContext == null)
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.ServiceContextParameterName, Resources.ServiceContextNotNullMessage));
                IdsExceptionManager.HandleException(exception);
            }
        }
        #endregion
    }
}
