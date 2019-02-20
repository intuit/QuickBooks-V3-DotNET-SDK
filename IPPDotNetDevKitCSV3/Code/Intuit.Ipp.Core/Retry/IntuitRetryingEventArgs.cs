////*********************************************************
// <copyright file="IntuitRetryingEventArgs.cs" company="Intuit">
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
// <summary>This file contains information required for the <see cref="IntuitRetryPolicy.Retrying"/> event.</summary>
////***************************************************

//namespace Intuit.Ipp.Retry  
namespace Intuit.Ipp.Core
{
    using System;

    /// <summary>
    /// Contains information required for the IntuitRetryPolicy retrying event.
    /// </summary>
    public class IntuitRetryingEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IntuitRetryingEventArgs"/> class.
        /// </summary>
        /// <param name="currentRetryCount">The current retry attempt count.</param>
        /// <param name="delay">The delay indicating how long the current thread will be suspended for before the next iteration will be invoked.</param>
        /// <param name="lastException">The exception which caused the retry conditions to occur.</param>
        public IntuitRetryingEventArgs(int currentRetryCount, TimeSpan delay, System.Exception lastException)
        {
            IntuitRetryHelper.IsArgumentNull(lastException, "lastException");

            this.CurrentRetryCount = currentRetryCount;
            this.Delay = delay;
            this.LastException = lastException;
        }

        /// <summary>
        /// Gets the current retry count.
        /// </summary>
        public int CurrentRetryCount { get; private set; }

        /// <summary>
        /// Gets the delay which indicates how long the current thread will be suspended for before the next iteration will be invoked.
        /// </summary>
        public TimeSpan Delay { get; private set; }

        /// <summary>
        /// Gets the exception which caused the retry conditions to occur.
        /// </summary>
        public System.Exception LastException { get; private set; }
    }
}
