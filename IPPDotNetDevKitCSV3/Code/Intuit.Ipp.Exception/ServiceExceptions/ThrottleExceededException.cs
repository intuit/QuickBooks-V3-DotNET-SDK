﻿////*********************************************************
// <copyright file="ThrottleExceeded.cs" company="Intuit">
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
// <summary>This file contains ThrottleExceededException.</summary>
////*********************************************************

namespace Intuit.Ipp.Exception
{
    using System.Runtime.Serialization;
    using Properties;
    /// <summary>
    /// Represents an exception raised when the Throttle is Exceeded and cannot process user requests.
    /// </summary>
    [System.Serializable]
    public class ThrottleExceededException : ServiceException
    {
        /// <summary>
        /// Initializes a new instance of the ThrottleExceededException class.
        /// </summary>
        public ThrottleExceededException()
            : base(Resources.ThrottleExceededExceptionDefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ThrottleExceededException class.
        /// </summary>
        /// <param name="errorMessage">Error Message.</param>
        public ThrottleExceededException(string errorMessage)
            : base(errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ThrottleExceededException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="innerException">Inner Exception.</param>
        public ThrottleExceededException(string errorMessage, System.Exception innerException)
            : base(errorMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ThrottleExceededException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="errorCode">Error Code.</param>
        /// <param name="source">Source of the exception.</param>
        public ThrottleExceededException(string errorMessage, string errorCode, string source)
            : base(errorMessage, errorCode, source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ThrottleExceededException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="errorCode">Error Code.</param>
        /// <param name="source">Source of the exception.</param>
        /// <param name="innerException">Inner Exception.</param>
        public ThrottleExceededException(string errorMessage, string errorCode, string source, System.Exception innerException)
            : base(errorMessage, errorCode, source, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ThrottleExceededException class.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        protected ThrottleExceededException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

