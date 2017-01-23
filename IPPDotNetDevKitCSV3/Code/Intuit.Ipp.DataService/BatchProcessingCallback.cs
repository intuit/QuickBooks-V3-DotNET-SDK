////********************************************************************
// <copyright file="BatchProcessingCallback.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Contains event handler for batch processing call back event.</summary>
////********************************************************************

namespace Intuit.Ipp.DataService
{
    /// <summary>
    /// Contains event handler for call back event.
    /// </summary>

    public static class BatchProcessingCallback 
    {
        /// <summary>
        /// event handler to handle call back for batch exceution completion
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="eventArgs">The <see cref="Intuit.Ipp.DataService.BatchExecutionCompletedEventArgs"/> instance containing the event data.</param>
        public delegate void BatchExecutionCompletedEventHandler(object sender, BatchExecutionCompletedEventArgs eventArgs);
    }
}
