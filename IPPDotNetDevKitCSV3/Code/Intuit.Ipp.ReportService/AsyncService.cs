////***************************************************************************************************
// <copyright file="AsyncService.cs" company="Intuit">
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
namespace Intuit.Ipp.ReportService
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
    using Intuit.Ipp.ReportService.Properties;
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Exception;
    using Intuit.Ipp.Utility;

    /// <summary>
    /// Intuit Partner Platform Services for QBO.
    /// </summary>
    internal class AsyncService
    {
        /// <summary>
        /// The Service context object.
        /// </summary>
        private ServiceContext serviceContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncService"/> class.
        /// </summary>
        /// <param name="serviceContext">IPP Service Context</param>
        public AsyncService(ServiceContext serviceContext)
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
        public event ReportServiceCallback<Report>.ReportCallCompletedEventHandler OnExecuteReportAsyncCompleted;

        #region ExecuteReport

        /// <summary>
        /// Executes a Report (asynchronously) under the specified realm in an asynchronous manner. The realm must be set in the context.
        /// </summary>
        /// <param name="reportName">Name of the Report to Run</param>
        /// <param name="reportsQueryParameters">Report Parameters for query string</param>
        public void ExecuteReportAsync(string reportName, string reportsQueryParameters)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method ExecuteReport Asynchronously.");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(this.serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(this.ExecuteReportAsynCompleted);
            ReportCallCompletedEventArgs<Report> reportCallCompletedEventArgs = new ReportCallCompletedEventArgs<Report>();
            string resourceString = reportName;
            try
            {
                // Builds resource Uri
                string uri = "";
                if (!string.IsNullOrEmpty(reportsQueryParameters))
                {
                    uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/reports/{2}?{3}", Utility.CoreConstants.VERSION, this.serviceContext.RealmId, resourceString, reportsQueryParameters);
                }
                else
                {
                    uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/reports/{2}", Utility.CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);
                }

                // Create request parameters
                RequestParameters parameters;
                if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
                {
                    parameters = new RequestParameters(uri, HttpVerbType.GET, Utility.CoreConstants.CONTENTTYPE_APPLICATIONJSON);
                }
                else
                {
                    parameters = new RequestParameters(uri, HttpVerbType.GET, Utility.CoreConstants.CONTENTTYPE_APPLICATIONXML);
                }

                // Prepare request
                HttpWebRequest request = asyncRestHandler.PrepareRequest(parameters, new Report());

                //// get response
                asyncRestHandler.GetResponse(request);
            }
            catch (SystemException systemException)
            {
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                reportCallCompletedEventArgs.Error = idsException;
                this.OnExecuteReportAsyncCompleted(this, reportCallCompletedEventArgs);
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
        private void ExecuteReportAsynCompleted(object sender, AsyncCallCompletedEventArgs eventArgs)
        {
            ReportCallCompletedEventArgs<Report> reportCallCompletedEventArgs = new ReportCallCompletedEventArgs<Report>();
            if (eventArgs.Error == null)
            {
                try
                {
                    IEntitySerializer responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);
                    string response = eventArgs.Result;
                    if (!response.StartsWith("{\"Report\":")) { response = "{\"Report\":" + response + "}"; }
                    IntuitResponse restResponse = (IntuitResponse)responseSerializer.Deserialize<IntuitResponse>(response);
                    reportCallCompletedEventArgs.Report = restResponse.AnyIntuitObject as Report;
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing event Execute ReportAsynCompleted in AsyncService object.");
                    this.OnExecuteReportAsyncCompleted(this, reportCallCompletedEventArgs);
                }
                catch (SystemException systemException)
                {
                    IdsException idsException = CreateIdsException(systemException);
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    reportCallCompletedEventArgs.Error = idsException;
                    this.OnExecuteReportAsyncCompleted(this, reportCallCompletedEventArgs);
                }
            }
            else
            {
                reportCallCompletedEventArgs.Error = eventArgs.Error;
                this.OnExecuteReportAsyncCompleted(this, reportCallCompletedEventArgs);
            }
        }

        #endregion
    }
}
