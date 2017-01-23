////********************************************************************
// <copyright file="ReportServiceCallBack.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains logic for REST request handler.</summary>
////********************************************************************

namespace Intuit.Ipp.Core
{
    /// <summary>
    /// Contains event handlers for call back events.
    /// </summary>
    /// <typeparam name="T">Generic Constraint </typeparam>
    public static class ReportServiceCallback<T> where T : Intuit.Ipp.Data.Report
    {
        /// <summary>
        /// Generic Event handler to handle Report asynchronous call back.
        /// </summary>
        /// <param name="sender">Sender of this event.</param>
        /// <param name="reportCallCompletedEventArgs">Report Call Completed Event Args.</param>
        public delegate void ReportCallCompletedEventHandler(object sender, ReportCallCompletedEventArgs<T> reportCallCompletedEventArgs);
    }
}
