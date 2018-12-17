////******************************************************************************************************************************
// <copyright file="ExtendedRetryUnauthorizedAccessException.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Extended RetryUnauthorizedAccessException.</summary>
////******************************************************************************************************************************

namespace Intuit.Ipp.Retry.Test
{
    using System;
    using Intuit.Ipp.Core;
    //using Intuit.Ipp.Retry;

    /// <summary>
    /// Extended RetryUnauthorizedAccessException Test
    /// </summary>
    internal class ExtendedRetryUnauthorizedAccessException : IExtendedRetry
    {
        /// <summary>
        /// Determines whether [is retry exception] [the specified ex].
        /// </summary>
        /// <param name="ex">The exception object.</param>
        /// <returns>
        ///   <c>true</c> if [is retry exception] [the specified ex]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsRetryException(System.Exception ex)
        {
            bool flag = false;
            
            if (ex is UnauthorizedAccessException)
            {
              flag = true;
            }

            return flag;
        }
    }
}
