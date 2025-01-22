﻿////*********************************************************
// <copyright file="ILogger.cs" company="Intuit">
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
// <summary>This file contains advanced logger for IPP .Net OAuth2 SDK.</summary>
////*********************************************************

namespace Intuit.Ipp.OAuth2PlatformClient.Diagnostics
{
    using System.IO;
    using System;
    using Serilog;
    using Serilog.Sinks.File;
    using Serilog.Core;
    using Serilog.Events;
    using System.Globalization;
    using System.Net.Http;

    /// <summary>
    /// Contains properties used to indicate whether request and response messages are to be logged.
    /// </summary>
    [Obsolete("Serilog configuration for Advanced Logging deprecated.")]
    public class OAuthAdvancedLogging : IOAuthAdvancedLogger, IOAuthLogger
    {
        /// <summary>
        /// request logging location.
        /// </summary>
        private string serviceRequestLoggingLocationForFile;

        /// <summary>
        /// Gets or sets a value indicating whether to enable reqeust response logging for Debug logs.
        /// </summary>
        public bool EnableSerilogRequestResponseLoggingForDebug { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether to enable reqeust response logging for Trace logs.
        /// </summary>
        public bool EnableSerilogRequestResponseLoggingForTrace { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether to enable reqeust response logging for Console logs.
        /// </summary>
        public bool EnableSerilogRequestResponseLoggingForConsole { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether to enable reqeust response logging for File logs.
        /// </summary>
        public bool EnableSerilogRequestResponseLoggingForFile { get; set; }


        /// <summary>
        /// Gets or sets the service request logging location for File, Rolling File.
        /// </summary>
        public string ServiceRequestLoggingLocationForFile
        {
            get
            {
                return this.serviceRequestLoggingLocationForFile;
            }

            set
            {
                if (!Directory.Exists(value))
                {
                    this.serviceRequestLoggingLocationForFile = System.IO.Path.GetTempPath();
                }

                this.serviceRequestLoggingLocationForFile = value;
            }
        }


        /// <summary>
        /// Serilog Logger
        /// </summary>
        private Serilog.ILogger logger;

        #region support later
        ///// <summary>
        ///// Gets or sets the service request logging location for File, Rolling File.
        ///// </summary>
        //public Uri ServiceRequestAzureDocumentDBUrl
        //{
        //    get
        //    {
        //        return this.serviceRequestAzureDocumentDBUrl;
        //    }

        //    set
        //    {
        //        if (EnableSerilogRequestResponseLoggingForAzureDocumentDB == true)
        //        {
        //            if (value == null)
        //            {
        //                IdsException exception = new IdsException(Properties.Resources.AzureDocumentDBUrlNullEmptyMessage, new ArgumentNullException());
        //                IdsExceptionManager.HandleException(exception);
        //            }
        //        }

        //        this.serviceRequestAzureDocumentDBUrl = value;

        //    }
        //}

        ///// <summary>
        ///// Gets or sets the service request logging location for File, Rolling File.
        ///// </summary>
        //public string ServiceRequestAzureDocumentDBSecureKey
        //{
        //    get
        //    {
        //        return this.serviceRequestAzureDocumentDBSecureKey;
        //    }

        //    set
        //    {
        //        if (EnableSerilogRequestResponseLoggingForAzureDocumentDB == true)
        //        {
        //            if (value == null && value == "")
        //            {
        //                IdsException exception = new IdsException(Properties.Resources.AzureDocumentDBSecureKeyNullEmptyMessage, new ArgumentNullException());
        //                IdsExceptionManager.HandleException(exception);
        //            }
        //        }

        //        this.serviceRequestAzureDocumentDBSecureKey = value;
        //    }
        //}

        #endregion


        /// <summary>
        /// Initializes a new instance of AdvanceLogging
        /// </summary>
        public OAuthAdvancedLogging()
            : this(enableSerilogRequestResponseLoggingForDebug: true, enableSerilogRequestResponseLoggingForTrace: true, enableSerilogRequestResponseLoggingForConsole: true, enableSerilogRequestResponseLoggingForFile: false, serviceRequestLoggingLocationForFile: null)
        {
        }

        /// <summary>
        /// Initializes a new instance of Advanced logging class        
        /// </summary>
        /// <param name="customLogger"></param>
        public OAuthAdvancedLogging(Serilog.ILogger customLogger)
        {
            this.logger = customLogger;
            //Logging first info
            logger.Information("Custom Logger is initialized");
        }


        /// <summary>
        /// Initializes a new instance of Advanced logger
        /// </summary>
        /// <param name="enableSerilogRequestResponseLoggingForDebug"></param>
        /// <param name="enableSerilogRequestResponseLoggingForTrace"></param>
        /// <param name="enableSerilogRequestResponseLoggingForConsole"></param>
        /// <param name="enableSerilogRequestResponseLoggingForFile"></param>
        /// <param name="serviceRequestLoggingLocationForFile"></param>
        public OAuthAdvancedLogging(bool enableSerilogRequestResponseLoggingForDebug, bool enableSerilogRequestResponseLoggingForTrace, bool enableSerilogRequestResponseLoggingForConsole, bool enableSerilogRequestResponseLoggingForFile, string serviceRequestLoggingLocationForFile)
        {
            this.EnableSerilogRequestResponseLoggingForDebug = enableSerilogRequestResponseLoggingForDebug;
            this.EnableSerilogRequestResponseLoggingForTrace = enableSerilogRequestResponseLoggingForTrace;
            this.EnableSerilogRequestResponseLoggingForConsole = enableSerilogRequestResponseLoggingForConsole;
            this.EnableSerilogRequestResponseLoggingForFile = enableSerilogRequestResponseLoggingForFile;
            this.ServiceRequestLoggingLocationForFile = serviceRequestLoggingLocationForFile;
     



            string filePath = string.Empty;

            if (this.EnableSerilogRequestResponseLoggingForFile)
            {
                //Assign tempath if no location found
                if (string.IsNullOrWhiteSpace(this.ServiceRequestLoggingLocationForFile))
                {
                    this.ServiceRequestLoggingLocationForFile = Path.GetTempPath();
                }


                //Log file path for widows n ios
                filePath = Path.Combine(this.ServiceRequestLoggingLocationForFile, "QBOApiLogs-" + DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture) + ".txt");

            }

            //Setting logger config for Serilog
            var loggerConfig = new LoggerConfiguration()
                 .MinimumLevel.Verbose();


            //Enabling console log
            if (this.EnableSerilogRequestResponseLoggingForConsole == true)
            {
                loggerConfig = loggerConfig.WriteTo.Console();
            }

            //Enabling Trace log
            if (this.EnableSerilogRequestResponseLoggingForTrace == true)
            {
                loggerConfig = loggerConfig.WriteTo.Trace();
            }

            //Enabling Debug log
            if (this.EnableSerilogRequestResponseLoggingForDebug == true)
            {
                loggerConfig = loggerConfig.WriteTo.Debug();

            }

            //Enabling file log
            if (!string.IsNullOrEmpty(this.ServiceRequestLoggingLocationForFile) && this.EnableSerilogRequestResponseLoggingForFile == true)
            {
                loggerConfig = loggerConfig.WriteTo.File(filePath);
            }

            //Creating the Logger for Serilog
            logger = loggerConfig.CreateLogger();

            //Logging first info
            logger.Information("Logger is initialized");

        }

        /// <summary>
        /// Should response body be logged?
        /// </summary>
        public bool ShowInfoLogs { get; set; }

        /// <summary>
        /// Logging message from SDK
        /// </summary>
        /// <param name="messageToWrite"></param>
        public void Log(string messageToWrite)
        {
            logger.Write(LogEventLevel.Information, messageToWrite);
        }

        void IOAuthLogger.LogRequest(HttpClient httpClient, HttpRequestMessage request)
        {
            logger.Write(LogEventLevel.Information, "Request url- " + request.RequestUri);
            logger.Write(LogEventLevel.Debug, "Request headers- ");
            var authorization = request.Headers.Authorization ?? httpClient.DefaultRequestHeaders.Authorization;
            logger.Write(LogEventLevel.Debug, "Authorization Header: " + authorization);
            logger.Write(LogEventLevel.Debug, "ContentType header: " + request.Content.Headers.ContentType);
            var accept = request.Headers.Accept ?? httpClient.DefaultRequestHeaders.Accept;
            logger.Write(LogEventLevel.Debug, "Accept header: " + accept);
        }

        bool IOAuthLogger.ShouldLogRequestBody()
        {
            return logger.IsEnabled(LogEventLevel.Verbose);
        }

        void IOAuthLogger.LogRequestBody(string body)
        {
            logger.Write(LogEventLevel.Verbose, "Request Body: " + body);
        }

        void IOAuthLogger.LogResponse(HttpResponseMessage response, string intuit_tid, string message, string body)
        {
            logger.Write(LogEventLevel.Information,
                "Response Intuit_Tid header - " + intuit_tid + ", Response Status Code- " + response.StatusCode +
                message == null ? "" : ", " + message);

            if (body != null && !ShowInfoLogs && logger.IsEnabled(LogEventLevel.Debug))
            {
                logger.Write(LogEventLevel.Debug, "Response Body- " + body);
            }
        }

        void IOAuthLogger.LogResponseError(HttpResponseMessage response, string errorDetail)
        {
            logger.Write(LogEventLevel.Warning, "Response: Status Code- " + response.StatusCode);
            logger.Write(LogEventLevel.Information, "Response: Error Details- " + response.ReasonPhrase + ": " + errorDetail);
        }
    }
}
