////*********************************************************
// <copyright file="ExponentialRetryElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains exponential retry element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Exponential retry element.
    /// </summary>
    public class ExponentialRetryElement : ConfigurationElement
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
        /// Gets the Minimum Backoff time.
        /// </summary>
        [ConfigurationProperty("minBackoff")]
        public TimeSpan MinBackoff
        {
            get
            {
                return (TimeSpan)this["minBackoff"];
            }
        }

        /// <summary>
        /// Gets the Maximum Backoff time.
        /// </summary>
        [ConfigurationProperty("maxBackoff")]
        public TimeSpan MaxBackoff
        {
            get
            {
                return (TimeSpan)this["maxBackoff"];
            }
        }

        /// <summary>
        /// Gets the Delta Backoff time.
        /// </summary>
        [ConfigurationProperty("deltaBackoff")]
        public TimeSpan DeltaBackoff
        {
            get
            {
                return (TimeSpan)this["deltaBackoff"];
            }
        }
    }
}
