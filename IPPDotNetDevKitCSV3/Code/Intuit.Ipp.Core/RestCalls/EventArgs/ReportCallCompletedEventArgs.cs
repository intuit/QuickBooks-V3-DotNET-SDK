////********************************************************************
// <copyright file="CallCompletedEventArgs.cs" company="Intuit">
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
    /// <typeparam name="T">Generic constraint of type IEntity.</typeparam>
    public class ReportCallCompletedEventArgs<T> where T : Intuit.Ipp.Data.Report
    {
        /// <summary>
        /// Initializes a new instance of the CallCompletedEventArgs class.
        /// </summary>
        public ReportCallCompletedEventArgs()
        {
        }

        /// <summary>
        /// Gets or sets the Entity of type T.
        /// </summary>
        public T Report { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        public IdsException Error { get; set; }
    }
}
