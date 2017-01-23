////*********************************************************
// <copyright file="IntuitRetryingEventArgs.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains information required for the <see cref="IntuitRetryPolicy.Retrying"/> event.</summary>
////***************************************************

namespace Intuit.Ipp.Retry
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
