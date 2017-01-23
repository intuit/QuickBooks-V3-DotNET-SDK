////*********************************************************
// <copyright file="RetryElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Retry element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Retry element.
    /// </summary>
    public class RetryElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the Retry mode.
        /// </summary>
        [ConfigurationProperty("mode")]
        public RetryMode Mode
        {
            get
            {
                RetryMode mode;
                if (Enum.TryParse(this["mode"].ToString(), true, out mode))
                {
                    return mode;
                }

                return RetryMode.None;
            }
        }

        /// <summary>
        /// Gets the LinearRetry Element.
        /// </summary>
        [ConfigurationProperty("linearRetry")]
        public LinearRetryElement LinearRetry
        {
            get
            {
                return (LinearRetryElement)this["linearRetry"];
            }
        }

        /// <summary>
        /// Gets the IncrementalRetry Element.
        /// </summary>
        [ConfigurationProperty("incrementalRetry")]
        public IncrementalRetryElement IncrementatlRetry
        {
            get
            {
                return (IncrementalRetryElement)this["incrementalRetry"];
            }
        }

        /// <summary>
        /// Gets the ExponentialRetry Element.
        /// </summary>
        [ConfigurationProperty("exponentialRetry")]
        public ExponentialRetryElement ExponentialRetry
        {
            get
            {
                return (ExponentialRetryElement)this["exponentialRetry"];
            }
        }
    }
}
