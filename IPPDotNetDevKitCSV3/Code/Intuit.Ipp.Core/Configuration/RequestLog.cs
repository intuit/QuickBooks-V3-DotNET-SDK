// -----------------------------------------------------------------------
// <copyright file="RequestLog.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
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
