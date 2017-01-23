////*********************************************************
// <copyright file="RequestLogElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Request log element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System.Configuration;

    /// <summary>
    /// Request log element.
    /// </summary>
    public class RequestLogElement : ConfigurationElement
    {
        /// <summary>
        /// Gets a value indicating whether to log request and response messages.
        /// </summary>
        [ConfigurationProperty("enableRequestResponseLogging")]
        public bool EnableRequestResponseLogging
        {
            get
            {
                return (bool)this["enableRequestResponseLogging"];
            }
        }

        /// <summary>
        /// Gets the logging directory.
        /// </summary>
        [ConfigurationProperty("requestResponseLoggingDirectory")]
        public string RequestResponseLoggingDirectory
        {
            get
            {
                return this["requestResponseLoggingDirectory"].ToString();
            }
        }
    }
}
