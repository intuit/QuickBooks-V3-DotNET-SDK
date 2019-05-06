////*********************************************************
// <copyright file="IdsExceptionManager.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2016 Intuit
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/
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
