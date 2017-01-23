////*********************************************************
// <copyright file="IncrementalRetryElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Incremental retry element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Request element.
    /// </summary>
    public class IncrementalRetryElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the retry count.
        /// </summary>
        [ConfigurationProperty("retryCount", DefaultValue = 0)]
        public int RetryCount
        {
            get
            {
                return (int)this["retryCount"];
            }
        }

        /// <summary>
        /// Gets the initial interval.
        /// </summary>
        [ConfigurationProperty("initialInterval")]
        public TimeSpan InitialInterval
        {
            get
            {
                return (TimeSpan)this["initialInterval"];
            }
        }

        /// <summary>
        /// Gets the incremental time.
        /// </summary>
        [ConfigurationProperty("increment")]
        public TimeSpan Increment
        {
            get
            {
                return (TimeSpan)this["increment"];
            }
        }
    }
}
