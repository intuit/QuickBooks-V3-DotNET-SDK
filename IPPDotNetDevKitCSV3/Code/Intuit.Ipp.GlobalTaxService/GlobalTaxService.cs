////*********************************************************
// <copyright file="GlobalTaxService.cs" company="Intuit">
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
// <summary>This file contains GlobalTaxService which consists of apis that
// can be used to setup tax based entities in qbo.</summary>
////*********************************************************

namespace Intuit.Ipp.GlobalTaxService
{

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using Intuit.Ipp.Core;
    using Intuit.Ipp.Core.Rest;
    using Intuit.Ipp.Data; 
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Exception;
    using Intuit.Ipp.Utility;
    using System.Text;
    using System.IO;
    using Intuit.Ipp.GlobalTaxService.Properties;
    using Intuit.Ipp.GlobalTaxService;


    /// <summary>
    /// Global TaxService for QBO.
    /// </summary>
    public class GlobalTaxService
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
        /// GlobalTaxService class
        /// </summary>
        /// <param name="serviceContext">IPP Service Context</param>
        public GlobalTaxService(ServiceContext serviceContext)
        {
            ServiceContextValidation(serviceContext);
            this.serviceContext = serviceContext;
            this.restHandler = new SyncRestHandler(this.serviceContext);

            // Set the Service type to either QBO by calling a method.
            this.serviceContext.UseDataServices();
        }


        #region Static Methods

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

       
        #region Async handlers
        /// <summary>
        /// Gets or sets the call back event for AddTaxCode method in asynchronous call.
        /// </summary>
        /// <value>
        /// The OnAddTaxCodeAsyncCompleted call back.
        /// </value>
        public GlobalTaxServiceCallback<Intuit.Ipp.Data.TaxService>.GlobalTaxServiceCallCompletedEventHandler OnAddTaxCodeAsyncCompleted { get; set; }
        #endregion

        #region Sync Methods

        /// <summary>
        /// Adds a TaxCode under the specified realm. The realm must be set in the context.
        /// </summary>     
        /// <param name="taxCode">TaxCode to Add.</param>
        /// <returns>Returns an updated version of the entity with updated identifier.</returns>
        public Intuit.Ipp.Data.TaxService AddTaxCode(Intuit.Ipp.Data.TaxService taxCode)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method AddTaxCode for TaxService.");

       
            // Validate parameter
            if (!GlobalTaxServiceHelper.IsTypeNull(taxCode))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

          
            string resourceString = taxCode.GetType().Name.ToLower(CultureInfo.InvariantCulture);
          

            // Builds resource Uri
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}/taxcode", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

            // Creates request parameters
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }

            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, taxCode );

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
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(response);             
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Add.");
            return (Intuit.Ipp.Data.TaxService)(restResponse.AnyIntuitObject as Intuit.Ipp.Data.TaxService);
            
            
        }

        #endregion


        #region Async AddTaxCode

        /// <summary>
        /// Adds a TaxCode under the specified realm. The realm must be set in the context.
        /// </summary>     
        /// <param name="taxCode">TaxCode to Add.</param>        
        public void AddTaxCodeAsync(Intuit.Ipp.Data.TaxService taxCode)
        {
            Console.Write("AddAsync started \n");
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Add Asynchronously.");
            GlobalTaxServiceCallCompletedEventArgs<Intuit.Ipp.Data.TaxService> callCompletedEventArgs = new GlobalTaxServiceCallCompletedEventArgs<Intuit.Ipp.Data.TaxService>();
            Console.Write("callCompletedEventArgs instantiated \n");
           
            if (!GlobalTaxServiceHelper.IsTypeNull(taxCode))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                Console.Write("IdsException instantiated \n");
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                callCompletedEventArgs.Error = exception;
                this.OnAddTaxCodeAsyncCompleted(this, callCompletedEventArgs);
            }
            else
            {
                try
                {
                    GlobalAsyncTaxService asyncService = new GlobalAsyncTaxService(this.serviceContext);                    
                    asyncService.OnAddTaxCodeAsyncCompleted += new GlobalTaxServiceCallback<Intuit.Ipp.Data.TaxService>.GlobalTaxServiceCallCompletedEventHandler(this.AddTaxCodeAsyncCompleted);
                    asyncService.AddTaxCodeAsync(taxCode as Intuit.Ipp.Data.TaxService);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);
                    callCompletedEventArgs.Error = idsException;
                    this.OnAddTaxCodeAsyncCompleted(this, callCompletedEventArgs);
                }
            }
        }

        #endregion


        /// <summary>
        /// Add Asynchronous call back method
        /// </summary>
        /// <param name="sender">Rest handler class</param>
        /// <param name="eventArgs">callback event arguments</param>
        private void AddTaxCodeAsyncCompleted(object sender, GlobalTaxServiceCallCompletedEventArgs<Intuit.Ipp.Data.TaxService> eventArgs)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method AddTaxCode Async.");
            this.OnAddTaxCodeAsyncCompleted(sender, eventArgs);
        }
    }
}
