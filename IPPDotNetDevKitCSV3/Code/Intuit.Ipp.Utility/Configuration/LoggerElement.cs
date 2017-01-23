////*********************************************************
// <copyright file="LoggerElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Logger element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System.Configuration;

    /// <summary>
    /// Logger element.
    /// </summary>
    public class LoggerElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the RequestLog element.
        /// </summary>
        [ConfigurationProperty("requestLog")]
        public RequestLogElement RequestLog
        {
            get
            {
                return (RequestLogElement)this["requestLog"];
            }
        }

        /// <summary>
        /// Gets the CustomLogger element.
        /// </summary>
        [ConfigurationProperty("customLogger")]
        public CustomLoggerElement CustomLogger
        {
            get
            {
                return (CustomLoggerElement)this["customLogger"];
            }
        }
    }
}
