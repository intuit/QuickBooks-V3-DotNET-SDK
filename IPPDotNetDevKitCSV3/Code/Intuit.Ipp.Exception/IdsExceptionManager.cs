////*********************************************************
// <copyright file="IdsExceptionManager.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains IdsExceptionManager.</summary>
////*********************************************************

namespace Intuit.Ipp.Exception
{
    /// <summary>
    /// Manages all the exceptions thrown to the user.
    /// </summary>
    public static class IdsExceptionManager
    {
        /// <summary>
        /// Handles exception thrown to the user.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        public static void HandleException(string errorMessage)
        {
            throw new IdsException(errorMessage);
        }

        /// <summary>
        /// Handles Exception thrown to the user.
        /// </summary>
        /// <param name="idsException">Ids Exception</param>
        public static void HandleException(IdsException idsException)
        {
            throw idsException;
        }

        /// <summary>
        /// Handles Exception thrown to the user.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="innerException">Ids Exception</param>
        public static void HandleException(string errorMessage, IdsException innerException)
        {
            throw new IdsException(errorMessage, innerException);
        }

        /// <summary>
        /// Handles Exception thrown to the user.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="errorCode">Error Code.</param>
        /// <param name="source">Source of the exception.</param>
        public static void HandleException(string errorMessage, string errorCode, string source)
        {
            throw new IdsException(errorMessage, errorCode, source);
        }

        /// <summary>
        /// Handles Exception thrown to the user.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="errorCode">Error Code.</param>
        /// <param name="source">Source of the exception.</param>
        /// <param name="innerException">Ids Exception</param>
        public static void HandleException(string errorMessage, string errorCode, string source, IdsException innerException)
        {
            throw new IdsException(errorMessage, errorCode, source, innerException);
        }
    }
}
