////*********************************************************
// <copyright file="RetryElement.cs" company="Intuit">
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
