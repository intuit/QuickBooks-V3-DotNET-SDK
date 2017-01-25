////*********************************************************
// <copyright file="IppConfigurationSection.cs" company="Intuit">
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
