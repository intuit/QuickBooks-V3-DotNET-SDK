////********************************************************************
// <copyright file="LogRequestsToDisk.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains logic for Logging API Requests/Responses To Disk</summary>
////********************************************************************

namespace Intuit.Ipp.Core.Rest
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using Intuit.Ipp.Core;
    using Intuit.Ipp.Exception;

    /// <summary>
    /// Logs API Requests/Responses To Disk
    /// </summary>
    internal class LogRequestsToDisk
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogRequestsToDisk"/> class.
        /// </summary>
        public LogRequestsToDisk()
            : this(false, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogRequestsToDisk"/> class.
        /// </summary>
        /// <param name="enableServiceRequestLogging">Value indicating whether to log request response messages.</param>
        /// <param name="serviceRequestLoggingLocation">Request Response logging locationl</param>
        public LogRequestsToDisk(bool enableServiceRequestLogging, string serviceRequestLoggingLocation)
        {
            this.EnableServiceRequestsLogging = enableServiceRequestLogging;
            this.ServiceRequestLoggingLocation = serviceRequestLoggingLocation;
        }

        /// <summary>
        /// Gets a value indicating whether Service Requests Logging should be enabled.
        /// </summary>
        internal bool EnableServiceRequestsLogging { get; private set; }

        /// <summary>
        /// Gets the Service Request Logging Location.
        /// </summary>
        internal string ServiceRequestLoggingLocation { get; private set; }

        /// <summary>
        /// Logs the Platform Request to Disk.
        /// </summary>
        /// <param name="xml">The xml to log.</param>
        /// <param name="isRequest">Specifies whether the xml is request or response.</param>
        internal void LogPlatformRequests(string xml, bool isRequest)
        {
            if (this.EnableServiceRequestsLogging)
            {
                if (string.IsNullOrWhiteSpace(this.ServiceRequestLoggingLocation))
                {
                    this.ServiceRequestLoggingLocation = Path.GetTempPath();
                }

                string filePath = string.Empty;
                if (isRequest)
                {
                    filePath = string.Format(CultureInfo.InvariantCulture, CoreConstants.REQUESTFILENAME_FORMAT, this.ServiceRequestLoggingLocation, CoreConstants.SLASH_CHAR, DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture));
                }
                else
                {
                    filePath = string.Format(CultureInfo.InvariantCulture, CoreConstants.RESPONSEFILENAME_FORMAT, this.ServiceRequestLoggingLocation, CoreConstants.SLASH_CHAR, DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture));
                }

                try
                {
                    Encoding encoder = Encoding.GetEncoding("utf-8", new EncoderExceptionFallback(), new DecoderExceptionFallback());
                    byte[] data = encoder.GetBytes(xml);
                    using (FileStream fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
                    {
                        fs.Write(data, 0, data.Length);
                    }
                }
                catch (System.Exception exception)
                {
                    IdsException idsException = new IdsException("Exception has been generated. Check inner exception for details.", exception);
                    IdsExceptionManager.HandleException(idsException);
                }
            }
        }
    }
}
