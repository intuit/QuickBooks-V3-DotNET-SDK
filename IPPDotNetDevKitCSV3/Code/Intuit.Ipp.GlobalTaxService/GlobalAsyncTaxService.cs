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
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using Intuit.Ipp.Core;
    using Intuit.Ipp.Core.Rest;
    using Intuit.Ipp.Data;
    using Intuit.Ipp.GlobalTaxService.Properties;
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Exception;
    using Intuit.Ipp.Utility;

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
        public event GlobalTaxServiceCallback<Intuit.Ipp.Data.TaxService>.GlobalTaxServiceCallCompletedEventHandler OnAddTaxCodeAsyncCompleted;

        #region add

        /// <summary>
        /// Adds an entity (asynchronously) under the specified realm in an asynchronous manner. The realm must be set in the context.
        /// </summary>
        /// <param name="taxCode">Entity to Add</param>
        public void AddTaxCodeAsync(Intuit.Ipp.Data.TaxService taxCode)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Add Asynchronously.");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(this.serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(this.AddTaxCodeAsyncCompleted);
            GlobalTaxServiceCallCompletedEventArgs<Intuit.Ipp.Data.TaxService> taxServiceCallCompletedEventArgs = new GlobalTaxServiceCallCompletedEventArgs<Intuit.Ipp.Data.TaxService>();
            string resourceString = taxCode.GetType().Name.ToLower(CultureInfo.InvariantCulture);
          

           
            try
            {
                // Builds resource Uri
                string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}/taxcode", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

                // Create request parameters
                RequestParameters parameters;
                if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
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
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                taxServiceCallCompletedEventArgs.Error = idsException;
                this.OnAddTaxCodeAsyncCompleted(this, taxServiceCallCompletedEventArgs);
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
            GlobalTaxServiceCallCompletedEventArgs<Intuit.Ipp.Data.TaxService> taxServiceCallCompletedEventArgs = new GlobalTaxServiceCallCompletedEventArgs<Intuit.Ipp.Data.TaxService>();
            if (eventArgs.Error == null)
            {
                try
                {
                    IEntitySerializer responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);
                    IntuitResponse restResponse = (IntuitResponse)responseSerializer.Deserialize<IntuitResponse>(eventArgs.Result);
                    taxServiceCallCompletedEventArgs.TaxService = restResponse.AnyIntuitObject as Intuit.Ipp.Data.TaxService;
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing event AddTaxCodeAsyncCompleted in AyncService object.");
                    this.OnAddTaxCodeAsyncCompleted(this, taxServiceCallCompletedEventArgs);
                }
                catch (SystemException systemException)
                {
                    IdsException idsException = CreateIdsException(systemException);
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    taxServiceCallCompletedEventArgs.Error = idsException;
                    this.OnAddTaxCodeAsyncCompleted(this, taxServiceCallCompletedEventArgs);
                }
            }
            else
            {
                taxServiceCallCompletedEventArgs.Error = eventArgs.Error;
                this.OnAddTaxCodeAsyncCompleted(this, taxServiceCallCompletedEventArgs);
            }
        }

        #endregion
    }
}
