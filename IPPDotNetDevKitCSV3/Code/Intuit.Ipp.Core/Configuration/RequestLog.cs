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
    public class RequestLog
    {
        /// <summary>
        /// request logging location.
        /// </summary>
        private string serviceRequestLoggingLocation;

        /// <summary>
        /// Gets or sets a value indicating whether to enable reqeust response logging.
        /// </summary>
        [Obsolete("EnableRequestResponsLogging is deprecated, please use EnableRequestResponseLogging instead.", true)]
        public bool EnableRequestResponsLogging { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable reqeust response logging.
        /// </summary>
        public bool EnableRequestResponseLogging { get; set; }

        /// <summary>
        /// Gets or sets the service request logging location.
        /// </summary>
        public string ServiceRequestLoggingLocation
        {
            get
            {
                return this.serviceRequestLoggingLocation;
            }

            set
            {
                if (!Directory.Exists(value))
                {
                    IdsException exception = new IdsException(Properties.Resources.ValidDirectoryPathMessage, new DirectoryNotFoundException());
                    IdsExceptionManager.HandleException(exception);
                }

                this.serviceRequestLoggingLocation = value;
            }
        }
    }
}
