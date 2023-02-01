////*********************************************************
// <copyright file="ReportService.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2016 Intuit
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *C:\Users\nshrivastava\Documents\Git\QuickBooks-V3-DotNET-SDK\IPPDotNetDevKitCSV3\Code\Intuit.Ipp.ReportService\ReportService.cs
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/
// <summary>This file contains ReportService which performs Read operations on V3 Reports endpoints.</summary>
////*********************************************************
namespace Intuit.Ipp.ReportService
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
    using Intuit.Ipp.ReportService.Properties;
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Exception;
    using Intuit.Ipp.Utility;
    using System.Text;
    using System.IO;

    /// <summary>
    /// This class file contains ReportService which performs Read operation for Reports
    /// </summary>
    public class ReportService : IReportService
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
        /// Initializes a new instance of the <see cref="ReportService"/> class.
        /// </summary>
        /// <param name="serviceContext">IPP Service Context</param>
        public ReportService(ServiceContext serviceContext)
        {
            ServiceContextValidation(serviceContext);
            this.serviceContext = serviceContext;
            this.restHandler = new SyncRestHandler(this.serviceContext);

            // Set the Service type to QBO by calling a method.
            this.serviceContext.UseDataServices();
        }

        #region Async handlers

        /// <summary>
        /// Gets or sets the call back event for Report method in asynchronous call.
        /// </summary>
        /// <value>
        /// The OnExecuteReportAsyncCompleted call back.
        /// </value>
        public ReportServiceCallback<Report>.ReportCallCompletedEventHandler OnExecuteReportAsyncCompleted { get; set; }

        #endregion

        #region Sync Methods

        /// <summary>
        /// Executes a report against a specified realm. The realm must be set in the context.
        /// </summary>
        /// <param name="reportName">Name of Report to Run.</param>
        /// <returns>Returns an updated version of the entity with updated identifier and sync token.</returns>
        public Report ExecuteReport(string reportName)
        {
           
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method ExecuteReport.");

            // Validate parameter
            if (string.IsNullOrEmpty(reportName))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.StringParameterNullOrEmpty));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            string resourceString = reportName;

            //Build Query Parameters

            // Builds resource Uri
            string uri = "";
            string reportsQueryParameters = GetReportQueryParameters();
            if (reportsQueryParameters.Length > 0)
            { 
                uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/reports/{2}?{3}", Utility.CoreConstants.VERSION, this.serviceContext.RealmId, resourceString, reportsQueryParameters);
            }
            else
            {
                uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/reports/{2}", Utility.CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);
            }

            // Creates request parameters
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.GET, Utility.CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.GET, Utility.CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }

            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, null);

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
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method ExecuteReport.");
            return (Report)(restResponse.AnyIntuitObject as Report);
        }

        #endregion

        #region Async methods

        #region Async ExecuteReport

        /// <summary>
        /// Executes a Report
        /// </summary>
        /// <param name="reportName">Name of Report to Run.</param>
        public void ExecuteReportAsync(string reportName)
        {
            Console.Write("ExecuteReport started \n");
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method ExecuteReport Asynchronously.");
            ReportCallCompletedEventArgs<Report> reportCallCompletedEventArgs = new ReportCallCompletedEventArgs<Report>();
            Console.Write("callCompletedEventArgs instantiated \n");
            if (string.IsNullOrEmpty(reportName))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.StringParameterNullOrEmpty));
                Console.Write("IdsException instantiated \n");
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                reportCallCompletedEventArgs.Error = exception;
                this.OnExecuteReportAsyncCompleted(this, reportCallCompletedEventArgs);
            }
            else
            {
                try
                {
                    AsyncService asyncService = new AsyncService(this.serviceContext);
                    asyncService.OnExecuteReportAsyncCompleted += new ReportServiceCallback<Report>.ReportCallCompletedEventHandler(this.ExecuteReportAsyncCompleted);
                    string reportsQueryParameters = GetReportQueryParameters();
                    asyncService.ExecuteReportAsync(reportName as string, reportsQueryParameters as string);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);
                    reportCallCompletedEventArgs.Error = idsException;
                    this.OnExecuteReportAsyncCompleted(this, reportCallCompletedEventArgs);
                }
            }
        }

        #endregion


        #endregion

        #region Private Methods

        /// <summary>
        /// Get and map Report query parameters
        /// </summary>
        private string GetReportQueryParameters()
        {
            List<string[]> uriParametersList = new List<string[]>();
            if (!string.IsNullOrEmpty(start_date)) { uriParametersList.Add(new string[] { "start_date", start_date }); }
            if (!string.IsNullOrEmpty(end_date)) { uriParametersList.Add(new string[] { "end_date", end_date }); }
            if (!string.IsNullOrEmpty(date_macro)) { uriParametersList.Add(new string[] { "date_macro", date_macro }); }
            if (!string.IsNullOrEmpty(accounting_method)) { uriParametersList.Add(new string[] { "accounting_method", accounting_method }); }
            if (!string.IsNullOrEmpty(summarize_column_by)) { uriParametersList.Add(new string[] { "summarize_column_by", summarize_column_by }); }
            if (!string.IsNullOrEmpty(customer)) { uriParametersList.Add(new string[] { "customer", customer }); }
            if (!string.IsNullOrEmpty(vendor)) { uriParametersList.Add(new string[] { "vendor", vendor }); }
            if (!string.IsNullOrEmpty(item)) { uriParametersList.Add(new string[] { "item", item }); }
            if (!string.IsNullOrEmpty(classid)) { uriParametersList.Add(new string[] { "class", classid }); }
            if (!string.IsNullOrEmpty(department)) { uriParametersList.Add(new string[] { "department", department }); }
            if (!string.IsNullOrEmpty(qzurl)) { uriParametersList.Add(new string[] { "qzurl", qzurl }); }
            if (!string.IsNullOrEmpty(aging_period)) { uriParametersList.Add(new string[] { "aging_period", aging_period }); }
            if (!string.IsNullOrEmpty(num_periods)) { uriParametersList.Add(new string[] { "num_periods", num_periods }); }

            if (!string.IsNullOrEmpty(report_date)) { uriParametersList.Add(new string[] { "report_date", report_date }); }
            if (!string.IsNullOrEmpty(columns)) { uriParametersList.Add(new string[] { "columns", columns }); }
            if (!string.IsNullOrEmpty(aging_method)) { uriParametersList.Add(new string[] { "aging_method", aging_method }); }
            if (!string.IsNullOrEmpty(past_due)) { uriParametersList.Add(new string[] { "past_due", past_due }); }
            if (!string.IsNullOrEmpty(end_duedate)) { uriParametersList.Add(new string[] { "end_duedate", end_duedate }); }
            if (!string.IsNullOrEmpty(start_duedate)) { uriParametersList.Add(new string[] { "start_duedate", start_duedate }); }
            if (!string.IsNullOrEmpty(term)) { uriParametersList.Add(new string[] { "term", term }); }
            if (!string.IsNullOrEmpty(custom1)) { uriParametersList.Add(new string[] { "custom1", custom1 }); }
            if (!string.IsNullOrEmpty(custom2)) { uriParametersList.Add(new string[] { "custom2", custom2 }); }
            if (!string.IsNullOrEmpty(custom3)) { uriParametersList.Add(new string[] { "custom3", custom3 }); }
            if (!string.IsNullOrEmpty(shipvia)) { uriParametersList.Add(new string[] { "shipvia", shipvia }); }
            if (!string.IsNullOrEmpty(sort_by)) { uriParametersList.Add(new string[] { "sort_by", sort_by }); }
            if (!string.IsNullOrEmpty(sort_order)) { uriParametersList.Add(new string[] { "sort_order", sort_order }); }
            if (!string.IsNullOrEmpty(showrows)) { uriParametersList.Add(new string[] { "showrows", showrows }); }

            if (!string.IsNullOrEmpty(low_pp_date)) { uriParametersList.Add(new string[] { "low_pp_date", low_pp_date }); }
            if (!string.IsNullOrEmpty(high_pp_date)) { uriParametersList.Add(new string[] { "high_pp_date", high_pp_date }); }
            if (!string.IsNullOrEmpty(custom_pp)) { uriParametersList.Add(new string[] { "custom_pp", custom_pp }); }


            if (!string.IsNullOrEmpty(end_createdate)) { uriParametersList.Add(new string[] { "end_createdate", end_createdate }); }
            if (!string.IsNullOrEmpty(start_createdate)) { uriParametersList.Add(new string[] { "start_createdate", start_createdate }); }
            if (!string.IsNullOrEmpty(journal_code)) { uriParametersList.Add(new string[] { "journal_code", journal_code }); }
            if (!string.IsNullOrEmpty(agency_id)) { uriParametersList.Add(new string[] { "agency_id", agency_id }); }

            if (!string.IsNullOrEmpty(account)) { uriParametersList.Add(new string[] { "account", account }); }
            if (!string.IsNullOrEmpty(source_account)) { uriParametersList.Add(new string[] { "source_account", source_account }); }
            if (!string.IsNullOrEmpty(account_type)) { uriParametersList.Add(new string[] { "account_type", account_type }); }
            if (!string.IsNullOrEmpty(source_account_type)) { uriParametersList.Add(new string[] { "source_account_type", source_account_type }); }
            if (!string.IsNullOrEmpty(duedate_macro)) { uriParametersList.Add(new string[] { "duedate_macro", duedate_macro }); }
            if (!string.IsNullOrEmpty(appaid)) { uriParametersList.Add(new string[] { "appaid", appaid }); }
            if (!string.IsNullOrEmpty(createdate_macro)) { uriParametersList.Add(new string[] { "createdate_macro", createdate_macro }); }
            if (!string.IsNullOrEmpty(end_createddate)) { uriParametersList.Add(new string[] { "end_createddate", end_createddate }); }
            if (!string.IsNullOrEmpty(start_createddate)) { uriParametersList.Add(new string[] { "start_createddate", start_createddate }); }
            if (!string.IsNullOrEmpty(moddate_macro)) { uriParametersList.Add(new string[] { "moddate_macro", moddate_macro }); }
            if (!string.IsNullOrEmpty(end_moddate)) { uriParametersList.Add(new string[] { "end_moddate", end_moddate }); }
            if (!string.IsNullOrEmpty(start_moddate)) { uriParametersList.Add(new string[] { "start_moddate", start_moddate }); }
            if (!string.IsNullOrEmpty(account_status)) { uriParametersList.Add(new string[] { "account_status", account_status }); }

            if (!string.IsNullOrEmpty(groupby)) { uriParametersList.Add(new string[] { "groupby", groupby }); }
            if (!string.IsNullOrEmpty(group_by)) { uriParametersList.Add(new string[] { "group_by", group_by }); }
            if (!string.IsNullOrEmpty(payment_method)) { uriParametersList.Add(new string[] { "payment_method", payment_method }); }
            if (!string.IsNullOrEmpty(name)) { uriParametersList.Add(new string[] { "name", name }); }
            if (!string.IsNullOrEmpty(transaction_type)) { uriParametersList.Add(new string[] { "transaction_type", transaction_type }); }
            if (!string.IsNullOrEmpty(cleared)) { uriParametersList.Add(new string[] { "cleared", cleared }); }
            if (!string.IsNullOrEmpty(arpaid)) { uriParametersList.Add(new string[] { "arpaid", arpaid }); }
            if (!string.IsNullOrEmpty(printed)) { uriParametersList.Add(new string[] { "printed", printed }); }
            if (!string.IsNullOrEmpty(bothamount)) { uriParametersList.Add(new string[] { "bothamount", bothamount }); }
            if (!string.IsNullOrEmpty(memo)) { uriParametersList.Add(new string[] { "memo", memo }); }
            if (!string.IsNullOrEmpty(docnum)) { uriParametersList.Add(new string[] { "docnum", docnum }); }
            if (!string.IsNullOrEmpty(add_due_date)) { uriParametersList.Add(new string[] { "add_due_date", add_due_date }); }
            if (!string.IsNullOrEmpty(attachmentType)) { uriParametersList.Add(new string[] { "attachmentType", attachmentType }); }

            if (!string.IsNullOrEmpty(subcol_py)) { uriParametersList.Add(new string[] { "subcol_py", subcol_py }); }
            if (!string.IsNullOrEmpty(subcol_py_chg)) { uriParametersList.Add(new string[] { "subcol_py_chg", subcol_py_chg }); }
            if (!string.IsNullOrEmpty(subcol_py_pct_chg)) { uriParametersList.Add(new string[] { "subcol_py_pct_chg", subcol_py_pct_chg }); }
            if (!string.IsNullOrEmpty(subcol_pp)) { uriParametersList.Add(new string[] { "subcol_pp", subcol_pp }); }
            if (!string.IsNullOrEmpty(subcol_pp_chg)) { uriParametersList.Add(new string[] { "subcol_pp_chg", subcol_pp_chg }); }
            if (!string.IsNullOrEmpty(subcol_pp_pct_chg)) { uriParametersList.Add(new string[] { "subcol_pp_pct_chg", subcol_pp_pct_chg }); }
            if (!string.IsNullOrEmpty(subcol_pct_ytd)) { uriParametersList.Add(new string[] { "subcol_pct_ytd", subcol_pct_ytd }); }
            if (!string.IsNullOrEmpty(subcol_ytd)) { uriParametersList.Add(new string[] { "subcol_ytd", subcol_ytd }); }
            if (!string.IsNullOrEmpty(subcol_pct_inc)) { uriParametersList.Add(new string[] { "subcol_pct_inc", subcol_pct_inc }); }
            if (!string.IsNullOrEmpty(subcol_pct_exp)) { uriParametersList.Add(new string[] { "subcol_pct_exp", subcol_pct_exp }); }
            if (!string.IsNullOrEmpty(subcol_pct_inc)) { uriParametersList.Add(new string[] { "subcol_pct_inc", subcol_pct_inc }); }
            if (!string.IsNullOrEmpty(subcol_pct_exp)) { uriParametersList.Add(new string[] { "adjusted_gain_loss", adjusted_gain_loss }); }



            StringBuilder uriParameters = new StringBuilder();
            foreach (string[] uriParameter in uriParametersList)
            {
                if (uriParameters.Length > 0) { uriParameters.Append("&"); }
                uriParameters.Append(uriParameter[0].Trim());
                uriParameters.Append("=");
                uriParameters.Append(uriParameter[1].Trim());
            }

            return uriParameters.ToString();
        }

        #endregion

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

        #region Async Completed methods

        /// <summary>
        /// ExecuteReport Asynchronous call back method
        /// </summary>
        /// <param name="sender">Rest handler class</param>
        /// <param name="eventArgs">callback event arguments</param>
        private void ExecuteReportAsyncCompleted(object sender, ReportCallCompletedEventArgs<Report> eventArgs)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method ExecuteReport Async.");
            this.OnExecuteReportAsyncCompleted(sender, eventArgs);
        }
        
        #endregion

        #region properties

        /// <summary>
        /// Gets or sets the StartDate
        /// </summary>
        public string start_date {get; set;}

        /// <summary>
        /// Gets or sets the end_date
        /// </summary>
        public string end_date  {get; set;}

        /// <summary>
        /// Gets or sets the date_macro
        /// </summary>
        public string date_macro {get; set;}

        /// <summary>
        /// Gets or sets the accounting_method
        /// </summary>
        public string accounting_method  {get; set;}

        /// <summary>
        /// Gets or sets the summarize_column_by
        /// </summary>
        public string summarize_column_by  {get; set;}

        /// <summary>
        /// Gets or sets the customer
        /// </summary>
        public string  customer  {get; set;}

        /// <summary>
        /// Gets or sets the vendor
        /// </summary>
        public string vendor  {get; set;}

        /// <summary>
        /// Gets or sets the item
        /// </summary>
        public string item  {get; set;}

        /// <summary>
        /// Gets or sets the class
        /// </summary>
        public string classid  {get; set;}

        /// <summary>
        /// Gets or sets the department
        /// </summary>
        public string department  {get; set;}

        /// <summary>
        /// Gets or sets the qzurl
        /// </summary>
        public string qzurl  {get; set;}

        /// <summary>
        /// Gets or sets the aging_period
        /// </summary>
        public string  aging_period  {get; set;}

        /// <summary>
        /// Gets or sets the num_periods
        /// </summary>
        public string num_periods { get; set; }


        #region new query params reports

        /// <summary>
        /// Gets or sets the report_date
        /// </summary>
        public string end_createdate { get; set; }

        /// <summary>
        /// Gets or sets the report_date
        /// </summary>
        public string start_createdate { get; set; }

        /// <summary>
        /// Gets or sets the report_date
        /// </summary>
        public string journal_code { get; set; }

        /// <summary>
        /// Gets or sets the report_date
        /// </summary>
        public string agency_id { get; set; }

        /// <summary>
        /// Gets or sets the report_date
        /// </summary>
        public string report_date { get; set; }

        /// <summary>
        /// Gets or sets the columns
        /// </summary>
        public string columns { get; set; }

        /// <summary>
        /// Gets or sets the aging_method
        /// </summary>
        public string aging_method { get; set; }

        /// <summary>
        /// Gets or sets the past_due
        /// </summary>
        public string past_due { get; set; }

        /// <summary>
        /// Gets or sets the end_duedate
        /// </summary>
        public string end_duedate { get; set; }

        /// <summary>
        /// Gets or sets the start_duedate
        /// </summary>
        public string start_duedate { get; set; }

        /// <summary>
        /// Gets or sets the term
        /// </summary>
        public string term { get; set; }

        /// <summary>
        /// Gets or sets the custom1
        /// </summary>
        public string custom1 { get; set; }

        /// <summary>
        /// Gets or sets the custom2
        /// </summary>
        public string custom2 { get; set; }

        /// <summary>
        /// Gets or sets the custom3
        /// </summary>
        public string custom3 { get; set; }

        /// <summary>
        /// Gets or sets the shipvia
        /// </summary>
        public string shipvia { get; set; }

        /// <summary>
        /// Gets or sets the sort_by
        /// </summary>
        public string sort_by { get; set; }

        /// <summary>
        /// Gets or sets the sort_order
        /// </summary>
        public string sort_order { get; set; }

        /// <summary>
        /// Gets or sets the account
        /// </summary>
        public string account { get; set; }

        /// <summary>
        /// Gets or sets the source_account
        /// </summary>
        public string source_account { get; set; }

        /// <summary>
        /// Gets or sets the account_type
        /// </summary>
        public string account_type { get; set; }

        /// <summary>
        /// Gets or sets the source_account_type
        /// </summary>
        public string source_account_type { get; set; }

        /// <summary>
        /// Gets or sets the duedate_macro
        /// </summary>
        public string duedate_macro { get; set; }

        /// <summary>
        /// Gets or sets the appaid
        /// </summary>
        public string appaid { get; set; }

        /// <summary>
        /// Gets or sets the createdate_macro
        /// </summary>
        public string createdate_macro { get; set; }

        /// <summary>
        /// Gets or sets the end_createddate
        /// </summary>
        public string end_createddate { get; set; }

        /// <summary>
        /// Gets or sets the start_createddate
        /// </summary>
        public string start_createddate { get; set; }

        /// <summary>
        /// Gets or sets the moddate_macro
        /// </summary>
        public string moddate_macro { get; set; }

        /// <summary>
        /// Gets or sets the end_moddate
        /// </summary>
        public string end_moddate { get; set; }

        /// <summary>
        /// Gets or sets the start_moddate
        /// </summary>
        public string start_moddate { get; set; }

        /// <summary>
        /// Gets or sets the account_status
        /// </summary>
        public string account_status { get; set; }

        /// <summary>
        /// Gets or sets the group_by
        /// </summary>
        public string group_by { get; set; }


        /// <summary>
        /// Gets or sets the payment_method
        /// </summary>
        public string payment_method { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Gets or sets the transaction_type
        /// </summary>
        public string transaction_type { get; set; }

        /// <summary>
        /// Gets or sets the cleared
        /// </summary>
        public string cleared { get; set; }

        /// <summary>
        /// Gets or sets the arpaid
        /// </summary>
        public string arpaid { get; set; }

        /// <summary>
        /// Gets or sets the printed
        /// </summary>
        public string printed { get; set; }

        /// <summary>
        /// Gets or sets the bothamount
        /// </summary>
        public string bothamount { get; set; }

        /// <summary>
        /// Gets or sets the memo
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// Gets or sets the docnum
        /// </summary>
        public string docnum { get; set; }

        /// <summary>
        /// Gets or sets the subcol_py
        /// </summary>
        public string subcol_py { get; set; }

        /// <summary>
        /// Gets or sets the subcol_py_chg
        /// </summary>
        public string subcol_py_chg { get; set; }

        /// <summary>
        /// Gets or sets the subcol_py_pct_chg
        /// </summary>
        public string subcol_py_pct_chg { get; set; }

        /// <summary>
        /// Gets or sets the subcol_pp
        /// </summary>
        public string subcol_pp { get; set; }

        /// <summary>
        /// Gets or sets the subcol_pp_chg
        /// </summary>
        public string subcol_pp_chg { get; set; }

        /// <summary>
        /// Gets or sets the subcol_pp_pct_chg
        /// </summary>
        public string subcol_pp_pct_chg { get; set; }

        /// <summary>
        /// Gets or sets the subcol_pct_ytd
        /// </summary>
        public string subcol_pct_ytd { get; set; }

        /// <summary>
        /// Gets or sets the subcol_ytd
        /// </summary>
        public string subcol_ytd { get; set; }

        /// <summary>
        /// Gets or sets the subcol_pct_inc
        /// </summary>
        public string subcol_pct_inc { get; set; }

        /// <summary>
        /// Gets or sets the subcol_pct_exp
        /// </summary>
        public string subcol_pct_exp { get; set; }

        /// <summary>
        /// Gets or sets the adjusted gain and loss
        /// </summary>
        public string adjusted_gain_loss { get; set; }

        /// <summary>
        /// Gets or sets the low_pp_date
        /// </summary>
        public string low_pp_date { get; set; }

        /// <summary>
        /// Gets or sets the high_pp_date
        /// </summary>
        public string high_pp_date { get; set; }

        /// <summary>
        /// Gets or sets the custom_pp
        /// </summary>
        public string custom_pp { get; set; }

        /// <summary>
        /// Gets or sets the showrows
        /// </summary>
        public string showrows { get; set; }

        /// <summary>
        /// Gets or sets the add_due_date
        /// </summary>
        public string add_due_date { get; set; }

        /// <summary>
        /// Gets or sets the attachmentType
        /// </summary>
        public string attachmentType { get; set; }

        /// <summary>
        /// Gets or sets the groupby
        /// </summary>
        public string groupby { get; set; }

        #endregion


        #endregion
    }
}
