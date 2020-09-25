using System;
using System.Collections.Generic;
using System.Text;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    using System.IO;
    using System;
    using Serilog;
    using Serilog.Sinks;
    using Serilog.Core;
    using Serilog.Events;
    using System.Globalization;

    /// <summary>
    /// Contains properties used to indicate whether request and response messages are to be logged.
    /// </summary>
    public class OAuthAdvancedLogging
    {
        /// <summary>
        /// request logging location.
        /// </summary>
        private string serviceRequestLoggingLocationForFile;

        /// <summary>
        /// request Azure Document DB url.
        /// </summary>
        private Uri serviceRequestAzureDocumentDBUrl;

        /// <summary>
        /// request Azure Document DB Secure Key
        /// </summary>
        private string serviceRequestAzureDocumentDBSecureKey;

        /// <summary>
        /// request TTL-time to live for all logs 
        /// </summary>
        public double ServiceRequestAzureDocumentDBTTL { get; set; }

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
        /// Gets or sets a value indicating whether to enable reqeust response logging for Rolling logs.
        /// </summary>
        public bool EnableSerilogRequestResponseLoggingForRollingFile { get; set; }



        ///// <summary>
        ///// Gets or sets a value indicating whether to enable reqeust response logging for Rolling logs.
        ///// </summary>
        //public bool EnableSerilogRequestResponseLoggingForFile { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether to enable reqeust response logging for Azure Doc DB logs.
        /// </summary>
        public bool EnableSerilogRequestResponseLoggingForAzureDocumentDB { get; set; }




        /// <summary>
        /// Gets or sets the service request logging location for File, Rolling File.
        /// </summary>
        public string ServiceRequestLoggingLocationForFile
        {
            get
            {
                return serviceRequestLoggingLocationForFile;
            }

            set
            {
                if (!Directory.Exists(value))
                {
                    serviceRequestLoggingLocationForFile = Path.GetTempPath();
                }

                serviceRequestLoggingLocationForFile = value;
            }
        }

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
        /// Serilog Logger
        /// </summary>
        public Logger log;





        /// <summary>
        /// Initializes a new instance of AdvanceLogging
        /// </summary>

        public OAuthAdvancedLogging()
            : this(enableSerilogRequestResponseLoggingForDebug: true, enableSerilogRequestResponseLoggingForTrace: true, enableSerilogRequestResponseLoggingForConsole: true, enableSerilogRequestResponseLoggingForRollingFile: false, serviceRequestLoggingLocationForFile: null)
        {
        }

        /// <summary>
        /// Initializes a new instance of Advanced logger
        /// </summary>
        /// <param name="enableSerilogRequestResponseLoggingForDebug"></param>
        /// <param name="enableSerilogRequestResponseLoggingForTrace"></param>
        /// <param name="enableSerilogRequestResponseLoggingForConsole"></param>
        /// <param name="enableSerilogRequestResponseLoggingForRollingFile"></param>
        /// <param name="serviceRequestLoggingLocationForFile"></param>
        public OAuthAdvancedLogging(bool enableSerilogRequestResponseLoggingForDebug, bool enableSerilogRequestResponseLoggingForTrace, bool enableSerilogRequestResponseLoggingForConsole, bool enableSerilogRequestResponseLoggingForRollingFile, string serviceRequestLoggingLocationForFile)
        {
            EnableSerilogRequestResponseLoggingForDebug = enableSerilogRequestResponseLoggingForDebug;
            EnableSerilogRequestResponseLoggingForTrace = enableSerilogRequestResponseLoggingForTrace;
            EnableSerilogRequestResponseLoggingForConsole = enableSerilogRequestResponseLoggingForConsole;
            EnableSerilogRequestResponseLoggingForRollingFile = enableSerilogRequestResponseLoggingForRollingFile;
            //this.EnableSerilogRequestResponseLoggingForAzureDocumentDB = enableSerilogRequestResponseLoggingForAzureDocumentDB;

            ServiceRequestLoggingLocationForFile = serviceRequestLoggingLocationForFile;
            //this.ServiceRequestAzureDocumentDBUrl = serviceRequestAzureDocumentDBUrl;
            //this.ServiceRequestAzureDocumentDBSecureKey = serviceRequestAzureDocumentDBSecureKey;
            //this.ServiceRequestAzureDocumentDBTTL = serviceRequestAzureDocumentDBTTL;



            string filePath = string.Empty;

            if (EnableSerilogRequestResponseLoggingForRollingFile)
            {
                //Assign tempath if no location found
                if (string.IsNullOrWhiteSpace(ServiceRequestLoggingLocationForFile))
                {
                    ServiceRequestLoggingLocationForFile = Path.GetTempPath();
                }


                //Log file path for widows n ios
                filePath = Path.Combine(ServiceRequestLoggingLocationForFile, "QBOApiLogs-" + DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture) + ".txt");

            }

            //Setting logger config for Serilog
            var loggerConfig = new LoggerConfiguration()
                 .MinimumLevel.Verbose();


            //Enabling console log
            if (EnableSerilogRequestResponseLoggingForConsole == true)
            {
                loggerConfig = loggerConfig.WriteTo.Console();
                loggerConfig = loggerConfig.WriteTo.LiterateConsole();
            }

            //Enabling Trace log
            if (EnableSerilogRequestResponseLoggingForTrace == true)
            {
                loggerConfig = loggerConfig.WriteTo.Trace();
            }

            //Enabling Debug log
            if (EnableSerilogRequestResponseLoggingForDebug == true)
            {
                loggerConfig = loggerConfig.WriteTo.Debug();

            }

            //Enabling Rolling file log
            if (!string.IsNullOrEmpty(ServiceRequestLoggingLocationForFile) && EnableSerilogRequestResponseLoggingForRollingFile == true)
            {
                loggerConfig = loggerConfig.WriteTo.RollingFile(filePath);
            }


            ////Enabling AzureDocumentDB
            //if (!string.IsNullOrEmpty(this.ServiceRequestAzureDocumentDBSecureKey) && this.ServiceRequestAzureDocumentDBUrl != null)
            //{
            //    loggerConfig = loggerConfig.WriteTo.AzureDocumentDB(this.ServiceRequestAzureDocumentDBUrl, this.ServiceRequestAzureDocumentDBSecureKey, timeToLive: TimeSpan.FromDays(this.ServiceRequestAzureDocumentDBTTL));//add DB

            //}

            //Creating the Logger for Serilog
            log = loggerConfig.CreateLogger();



            //Logging first info
            log.Information("Logger is initialized");

        }

        /// <summary>
        /// Logging message from SDK
        /// </summary>
        /// <param name="messageToWrite"></param>
        public void Log(string messageToWrite)
        {

            log.Write(LogEventLevel.Verbose, messageToWrite);
        }
    }
}
