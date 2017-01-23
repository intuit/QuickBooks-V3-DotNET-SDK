////********************************************************************
// <copyright file="BatchExecutionCompletedEventArgs.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains the event arguments for batch execution completion.</summary>
////********************************************************************

namespace Intuit.Ipp.DataService
{
    using Intuit.Ipp.Exception;

    /// <summary>
    /// Contains events for call back methods and corresponding fields
    /// </summary>
    public class BatchExecutionCompletedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BatchExecutionCompletedEventArgs"/> class.
        /// </summary>
        public BatchExecutionCompletedEventArgs()
        {
        }

        /// <summary>
        /// Gets or sets the batch.
        /// </summary>
        /// <value>
        /// The batch.
        /// </value>
        public Batch Batch { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        public IdsException Error { get; set; }
    }
}
