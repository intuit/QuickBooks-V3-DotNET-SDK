////********************************************************************
// <copyright file="GlobalTaxServiceCallback.cs" company="Intuit">
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
    public static class GlobalTaxServiceCallback<T> where T : Intuit.Ipp.Data.TaxService
    {
        /// <summary>
        /// Generic Event handler to handle TaxService asynchronous call back.
        /// </summary>
        /// <param name="sender">Sender of this event.</param>
        /// <param name="taxServiceCallCompletedEventArgs">TaxService Call Completed Event Args.</param>
        public delegate void GlobalTaxServiceCallCompletedEventHandler(object sender, GlobalTaxServiceCallCompletedEventArgs<T> taxServiceCallCompletedEventArgs);
    }
}