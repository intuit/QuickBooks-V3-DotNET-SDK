////********************************************************************
// <copyright file="LogRequestsToDisk.cs" company="Intuit">
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
// <summary>This file contains SdkException.</summary>
// <summary>This file contains logic for Logging API Requests/Responses To Disk</summary>
////********************************************************************

namespace Intuit.Ipp.Core.Rest
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    //using Intuit.Ipp.Core; 
    using Intuit.Ipp.Exception;
    using Intuit.Ipp.Utility;

    /// <summary>
    /// Logs API Requests/Responses To Disk
    /// </summary>
    public class LogRequestsToDisk
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
        public void LogPlatformRequests(string xml, bool isRequest)
        {
            if (this.EnableServiceRequestsLogging)
            {
                if (string.IsNullOrWhiteSpace(this.ServiceRequestLoggingLocation))
                {
                    this.ServiceRequestLoggingLocation = Path.GetTempPath();
                }

                string filePath = string.Empty;
                if (this.ServiceRequestLoggingLocation.Contains("\\"))
                {
                    if (isRequest)
                    {
                        filePath = string.Format(CultureInfo.InvariantCulture, Utility.CoreConstants.REQUESTFILENAME_FORMAT, this.ServiceRequestLoggingLocation, Utility.CoreConstants.SLASH_CHAR, DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        filePath = string.Format(CultureInfo.InvariantCulture, Utility.CoreConstants.RESPONSEFILENAME_FORMAT, this.ServiceRequestLoggingLocation, Utility.CoreConstants.SLASH_CHAR, DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture));
                    }
                }
                else
                {
                    if (isRequest)
                    {
                        filePath = Path.Combine(this.ServiceRequestLoggingLocation,"Request-" + DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture) + ".txt");
                        
                    }
                    else
                    {
                        filePath = Path.Combine(this.ServiceRequestLoggingLocation, "Response-" + DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture) + ".txt");
                    }

                }

                try
                {
                    Encoding encoder = Encoding.GetEncoding("utf-8", new EncoderExceptionFallback(), new DecoderExceptionFallback());
                    byte[] data = encoder.GetBytes(xml);
                    using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
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