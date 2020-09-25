////***************************************************************************************************
// <copyright file="GlobalAsyncService.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2016 Intuit
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
// <summary>This file contains methods for crud operations that supports asynchronous call.</summary>
////***************************************************************************************************
namespace Intuit.Ipp.GlobalTaxService
{
    using System;
    using System.Globalization;
    using System.Net;
    using Core;
    using Core.Rest;
    using Data;
    using Properties;
    using Diagnostics;
    using Exception;
    using Utility;

    /// <summary>
    /// Async Global TaxService for QBO.
    /// </summary>
    internal class GlobalAsyncTaxService
    {
        /// <summary>
        /// The Service context object.
        /// </summary>
        private ServiceContext serviceContext;

        /// <summary>
        /// GlobalTax Async Service
        /// </summary>
        /// <param name="serviceContext">IPP Service Context</param>
        public GlobalAsyncTaxService(ServiceContext serviceContext)
        {
            if (serviceContext == null)
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException("serviceContext", "The Service Context cannot be null."));
                IdsExceptionManager.HandleException(exception);
            }

            if (serviceContext.IppConfiguration.Logger == null)
            {
                IdsException exception = new IdsException("The Logger cannot be null.");
                IdsExceptionManager.HandleException(exception);
            }

            if (string.IsNullOrWhiteSpace(serviceContext.RealmId))
            {
                InvalidRealmException exception = new InvalidRealmException();
                IdsExceptionManager.HandleException(exception);
            }

            this.serviceContext = serviceContext;
        }

        /// <summary>
        /// call back event for find all
        /// </summary>
        public event GlobalTaxServiceCallback<TaxService>.GlobalTaxServiceCallCompletedEventHandler OnAddTaxCodeAsyncCompleted;

        #region add

        /// <summary>
        /// Adds an entity (asynchronously) under the specified realm in an asynchronous manner. The realm must be set in the context.
        /// </summary>
        /// <param name="taxCode">Entity to Add</param>
        public void AddTaxCodeAsync(TaxService taxCode)
        {
            serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, "Called Method Add Asynchronously.");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(AddTaxCodeAsyncCompleted);
            GlobalTaxServiceCallCompletedEventArgs<TaxService> taxServiceCallCompletedEventArgs = new GlobalTaxServiceCallCompletedEventArgs<TaxService>();
            string resourceString = taxCode.GetType().Name.ToLower(CultureInfo.InvariantCulture);
          

           
            try
            {
                // Builds resource Uri
                string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}/taxcode", CoreConstants.VERSION, serviceContext.RealmId, resourceString);

                // Create request parameters
                RequestParameters parameters;
                if (serviceContext.IppConfiguration.Message.Request.SerializationFormat == Core.Configuration.SerializationFormat.Json)
                {
                    parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
                }
                else
                {
                    parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
                }

                // Prepare request
                HttpWebRequest request = asyncRestHandler.PrepareRequest(parameters, taxCode);

                //// get response
                asyncRestHandler.GetResponse(request);
            }
            catch (SystemException systemException)
            {
                serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                taxServiceCallCompletedEventArgs.Error = idsException;
                OnAddTaxCodeAsyncCompleted(this, taxServiceCallCompletedEventArgs);
            }
        }

        #endregion

        #region IdsException
        /// <summary>
        /// Creates the ids exception.
        /// </summary>
        /// <param name="applicationException">The application exception.</param>
        /// <returns>Returns the IdsException.</returns>
        private static IdsException CreateIdsException(Exception applicationException)
        {
            IdsException idsException = new IdsException(applicationException.Message);
            return idsException;
        }

        #endregion

        #region Async Completed methods

        /// <summary>
        /// call back method for asynchronously Executing a Report
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Intuit.Ipp.Core.AsyncCallCompletedEventArgs"/> instance containing the event data.</param>
        private void AddTaxCodeAsyncCompleted(object sender, AsyncCallCompletedEventArgs eventArgs)
        {
            GlobalTaxServiceCallCompletedEventArgs<TaxService> taxServiceCallCompletedEventArgs = new GlobalTaxServiceCallCompletedEventArgs<TaxService>();
            if (eventArgs.Error == null)
            {
                try
                {
                    IEntitySerializer responseSerializer = CoreHelper.GetSerializer(serviceContext, false);
                    IntuitResponse restResponse = (IntuitResponse)responseSerializer.Deserialize<IntuitResponse>(eventArgs.Result);
                    taxServiceCallCompletedEventArgs.TaxService = restResponse.AnyIntuitObject as TaxService;
                    serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, "Finished Executing event AddTaxCodeAsyncCompleted in AyncService object.");
                    OnAddTaxCodeAsyncCompleted(this, taxServiceCallCompletedEventArgs);
                }
                catch (SystemException systemException)
                {
                    IdsException idsException = CreateIdsException(systemException);
                    serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    taxServiceCallCompletedEventArgs.Error = idsException;
                    OnAddTaxCodeAsyncCompleted(this, taxServiceCallCompletedEventArgs);
                }
            }
            else
            {
                taxServiceCallCompletedEventArgs.Error = eventArgs.Error;
                OnAddTaxCodeAsyncCompleted(this, taxServiceCallCompletedEventArgs);
            }
        }

        #endregion
    }
}
