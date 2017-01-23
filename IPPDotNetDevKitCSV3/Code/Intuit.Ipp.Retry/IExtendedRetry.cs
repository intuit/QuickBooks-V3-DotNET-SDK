////*********************************************************
// <copyright file="IExtendedRetry.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains IExtendedRetryExceptions contracts.</summary>
////***************************************************
namespace Intuit.Ipp.Retry
{
    using System;

    /// <summary>
    /// Custom exception retry strategy contracts.
    /// </summary>
    public interface IExtendedRetry
    {
        /// <summary>
        /// Determines whether [is retry exception] [the specified ex].
        /// </summary>
        /// <param name="ex">The exception object.</param>
        /// <returns>
        ///   <c>true</c> if [is parameter (ex) is retry exception]; otherwise, <c>false</c>.
        /// </returns>
        bool IsRetryException(Exception ex);
    }
}
