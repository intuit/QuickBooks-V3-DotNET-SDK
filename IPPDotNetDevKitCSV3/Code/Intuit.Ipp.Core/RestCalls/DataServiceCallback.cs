////********************************************************************
// <copyright file="DataServiceCallBack.cs" company="Intuit">
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
    public static class DataServiceCallback<T> where T : Intuit.Ipp.Data.IEntity
    {
        /// <summary>
        /// Generic event handler to handle multiple call backs.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="callCompletedEventArgs">Call Completed Event Args.</param>
        public delegate void CallCompletedEventHandler(object sender, CallCompletedEventArgs<T> callCompletedEventArgs);

        /// <summary>
        /// Generic event handler to handle multiple pdf call backs.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="pdfCallCompletedEventArgs">Pdf Call Completed Event Args.</param>
        public delegate void PdfCallCompletedEventHandler(object sender, PdfCallCompletedEventArgs pdfCallCompletedEventArgs);

        /// <summary>
        /// Event handler to handle FindAll asynchronous call back.
        /// </summary>
        /// <param name="sender">Sender of this event.</param>
        /// <param name="findAllCallCompletedEventArgs">FindAll Call Completed Event Args.</param>
        public delegate void FindAllCallCompletedEventHandler(object sender, FindAllCallCompletedEventArgs findAllCallCompletedEventArgs);

        /// <summary>
        /// Event handler to handle CDC asynchronous call back.
        /// </summary>
        /// <param name="sender">Sender of this event.</param>
        /// <param name="cdcCallCompletedEventArgs">CDC Call Completed Event Args.</param>
        public delegate void CDCCallCompletedEventHandler(object sender, CDCCallCompletedEventArgs cdcCallCompletedEventArgs);

        /// <summary>
        /// Generic Event handler to handle asynchronous call back.
        /// </summary>
        /// <param name="sender">Sender of this event.</param>
        /// <param name="asyncCallCompletedEventArgs">Async Call Completed Event Args.</param>
        public delegate void AsyncCallCompletedEventHandler(object sender, AsyncCallCompletedEventArgs asyncCallCompletedEventArgs);
    }
}
