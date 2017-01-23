////*********************************************************
// <copyright file="RetryMode.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Retry mode enumeration.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    /// <summary>
    /// Retry mode enumeration.
    /// </summary>
    public enum RetryMode
    {
        /// <summary>
        /// No retry model.
        /// </summary>
        None,

        /// <summary>
        /// Linear retry model.
        /// </summary>
        Linear,

        /// <summary>
        /// Incremental retry model.
        /// </summary>
        Incremental,

        /// <summary>
        /// Exponential retry model.
        /// </summary>
        Exponential
    }
}
