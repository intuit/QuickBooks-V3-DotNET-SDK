////********************************************************************
// <copyright file="GlobalTaxServiceCallCompletedEventArgs.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains logic for REST request handler.</summary>
////********************************************************************

namespace Intuit.Ipp.Core
{
    using Intuit.Ipp.Exception;

    /// <summary>
    /// Contains events for call back methods and corresponding fields
    /// </summary>
    /// <typeparam name="T">Generic constraint of type TaxService.</typeparam>
    public class GlobalTaxServiceCallCompletedEventArgs<T> where T : Intuit.Ipp.Data.TaxService
    {
        /// <summary>
        /// Initializes a new instance of the CallCompletedEventArgs class.
        /// </summary>
        public GlobalTaxServiceCallCompletedEventArgs()
        {
        }

        /// <summary>
        /// Gets or sets the Entity of type T.
        /// </summary>
        public T TaxService { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        public IdsException Error { get; set; }
    }
}
