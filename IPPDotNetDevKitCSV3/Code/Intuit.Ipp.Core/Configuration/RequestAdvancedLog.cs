// -----------------------------------------------------------------------
// <copyright file="RequestLog.cs" company="Microsoft">
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
// <summary>This file contains Request Log configuration.</summary>
// -----------------------------------------------------------------------

using System;

namespace Intuit.Ipp.Core.Configuration
{
    using System.IO;
    using Intuit.Ipp.Exception;

    /// <summary>
    /// Contains properties used to indicate whether request and response messages are to be logged.
    /// </summary>
    public class RequestAdvancedLog
    {
        /// <summary>
        /// request logging location.
        /// </summary>
        private string serviceRequestLoggingLocationForFile;

        ///// <summary>
        ///// request Azure Document DB url.
        ///// </summary>
        //private Uri serviceRequestAzureDocumentDBUrl;

        ///// <summary>
        ///// request Azure Document DB Secure Key
        ///// </summary>
        //private string serviceRequestAzureDocumentDBSecureKey;

        ///// <summary>
        ///// request TTL-time to live for all logs 
        ///// </summary>
        //public int ServiceRequestAzureDocumentDBTTL { get; set; }

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
                return this.serviceRequestLoggingLocationForFile;
            }

            set
            {
                if (!Directory.Exists(value))
                {
                    IdsException exception = new IdsException(Properties.Resources.ValidDirectoryPathMessage, new DirectoryNotFoundException());
                    IdsExceptionManager.HandleException(exception);
                }

                this.serviceRequestLoggingLocationForFile= value;
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
        //            if (value == null || value == "")
        //            {
        //                IdsException exception = new IdsException(Properties.Resources.AzureDocumentDBSecureKeyNullEmptyMessage, new ArgumentNullException());
        //                IdsExceptionManager.HandleException(exception);
        //            }
        //        }

        //        this.serviceRequestAzureDocumentDBSecureKey = value;
        //    }
        //}
        #endregion



    }
}
