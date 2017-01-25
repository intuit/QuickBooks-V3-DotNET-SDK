////*********************************************************
// <copyright file="IncrementalRetryElement.cs" company="Intuit">
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
