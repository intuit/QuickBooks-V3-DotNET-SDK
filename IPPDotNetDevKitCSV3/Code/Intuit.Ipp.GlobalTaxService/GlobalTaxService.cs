﻿////*********************************************************
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
    using Core;
    using Core.Rest;
    using Data; 
    using Diagnostics;
    using Exception;
    using Utility;
    using System.Text;
    using System.IO;
    using Properties;
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
            restHandler = new SyncRestHandler(this.serviceContext);

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
        public GlobalTaxServiceCallback<TaxService>.GlobalTaxServiceCallCompletedEventHandler OnAddTaxCodeAsyncCompleted { get; set; }
        #endregion

        #region Sync Methods

        /// <summary>
        /// Adds a TaxCode under the specified realm. The realm must be set in the context.
        /// </summary>     
        /// <param name="taxCode">TaxCode to Add.</param>
        /// <returns>Returns an updated version of the entity with updated identifier.</returns>
        public TaxService AddTaxCode(TaxService taxCode)
        {
            serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, "Called Method AddTaxCode for TaxService.");

       
            // Validate parameter
            if (!GlobalTaxServiceHelper.IsTypeNull(taxCode))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

          
            string resourceString = taxCode.GetType().Name.ToLower(CultureInfo.InvariantCulture);
          

            // Builds resource Uri
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}/taxcode", CoreConstants.VERSION, serviceContext.RealmId, resourceString);

            // Creates request parameters
            RequestParameters parameters;
            if (serviceContext.IppConfiguration.Message.Request.SerializationFormat == Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }

            // Prepares request
            HttpWebRequest request = restHandler.PrepareRequest(parameters, taxCode );

            string response = string.Empty;
            try
            {
                // gets response
                response = restHandler.GetResponse(request);
            }
            catch (IdsException ex)
            {
                IdsExceptionManager.HandleException(ex);
            }

            CoreHelper.CheckNullResponseAndThrowException(response);

            // de serialize object
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(serviceContext, false).Deserialize<IntuitResponse>(response);             
            serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, "Finished Executing Method Add.");
            return (TaxService)(restResponse.AnyIntuitObject as TaxService);
            
            
        }

        #endregion


        #region Async AddTaxCode

        /// <summary>
        /// Adds a TaxCode under the specified realm. The realm must be set in the context.
        /// </summary>     
        /// <param name="taxCode">TaxCode to Add.</param>        
        public void AddTaxCodeAsync(TaxService taxCode)
        {
            Console.Write("AddAsync started \n");
            serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, "Called Method Add Asynchronously.");
            GlobalTaxServiceCallCompletedEventArgs<TaxService> callCompletedEventArgs = new GlobalTaxServiceCallCompletedEventArgs<TaxService>();
            Console.Write("callCompletedEventArgs instantiated \n");
           
            if (!GlobalTaxServiceHelper.IsTypeNull(taxCode))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                Console.Write("IdsException instantiated \n");
                serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                callCompletedEventArgs.Error = exception;
                OnAddTaxCodeAsyncCompleted(this, callCompletedEventArgs);
            }
            else
            {
                try
                {
                    GlobalAsyncTaxService asyncService = new GlobalAsyncTaxService(serviceContext);                    
                    asyncService.OnAddTaxCodeAsyncCompleted += new GlobalTaxServiceCallback<TaxService>.GlobalTaxServiceCallCompletedEventHandler(AddTaxCodeAsyncCompleted);
                    asyncService.AddTaxCodeAsync(taxCode as TaxService);
                }
                catch (SystemException systemException)
                {
                    serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);
                    callCompletedEventArgs.Error = idsException;
                    OnAddTaxCodeAsyncCompleted(this, callCompletedEventArgs);
                }
            }
        }

        #endregion


        /// <summary>
        /// Add Asynchronous call back method
        /// </summary>
        /// <param name="sender">Rest handler class</param>
        /// <param name="eventArgs">callback event arguments</param>
        private void AddTaxCodeAsyncCompleted(object sender, GlobalTaxServiceCallCompletedEventArgs<TaxService> eventArgs)
        {
            serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, "Finished Executing Method AddTaxCode Async.");
            OnAddTaxCodeAsyncCompleted(sender, eventArgs);
        }
    }
}
