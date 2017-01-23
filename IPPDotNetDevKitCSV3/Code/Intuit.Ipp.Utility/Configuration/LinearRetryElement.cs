////*********************************************************
// <copyright file="LinearRetryElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Linear retry element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Linear retry element.
    /// </summary>
    public class LinearRetryElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the Retry Count.
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
        /// Gets the Retry interval.
        /// </summary>
        [ConfigurationProperty("retryInterval")]
        public TimeSpan RetryInterval
        {
            get
            {
                return (TimeSpan)this["retryInterval"];
            }
        }
    }
}
