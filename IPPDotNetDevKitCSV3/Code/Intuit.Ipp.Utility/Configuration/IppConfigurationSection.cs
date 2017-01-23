////*********************************************************
// <copyright file="IppConfigurationSection.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Intuit Ipp configuration section.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System.Configuration;

    /// <summary>
    /// Intuit Ipp configuration section.
    /// </summary>
    public class IppConfigurationSection : ConfigurationSection
    {
        /// <summary>
        /// Gets the Instance of IppConfigurationSection.
        /// </summary>
        public static IppConfigurationSection Instance
        {
            get
            {
                return (IppConfigurationSection)ConfigurationManager.GetSection("intuit/ipp");
            }
        }

        /// <summary>
        /// Gets the Logger element.
        /// </summary>
        [ConfigurationProperty("logger")]
        public LoggerElement Logger
        {
            get
            {
                return (LoggerElement)this["logger"];
            }
        }

        /// <summary>
        /// Gets the Security element.
        /// </summary>
        [ConfigurationProperty("security")]
        public SecurityElement Security
        {
            get
            {
                return (SecurityElement)this["security"];
            }
        }

        /// <summary>
        /// Gets the Message element.
        /// </summary>
        [ConfigurationProperty("message")]
        public MessageElement Message
        {
            get
            {
                return (MessageElement)this["message"];
            }
        }

        /// <summary>
        /// Gets the Retry element.
        /// </summary>
        [ConfigurationProperty("retry")]
        public RetryElement Retry
        {
            get
            {
                return (RetryElement)this["retry"];
            }
        }

        /// <summary>
        /// Gets the Service element.
        /// </summary>
        [ConfigurationProperty("service")]
        public ServiceElement Service
        {
            get
            {
                return (ServiceElement)this["service"];
            }
        }


        /// <summary>
        /// Gets the WebhooksService element.
        /// </summary>
        [ConfigurationProperty("webhooksService")]
        public WebhooksServiceElement WebhooksService
        {
            get
            {
                return (WebhooksServiceElement)this["webhooksService"];
            }
        }

    }
}
