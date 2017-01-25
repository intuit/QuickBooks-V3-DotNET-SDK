////*********************************************************
// <copyright file="ServiceReturnedNoInformationException.cs" company="Intuit">
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
// <summary>This file contains Exception class raised when Service Returns No Information.</summary>
////*********************************************************

namespace Intuit.Ipp.Exception
{
    using System.Runtime.Serialization;
    using Intuit.Ipp.Exception.Properties;

    /// <summary>
    /// Represents an Exception raised when ServiceContext is Null.
    /// </summary>
    [System.Serializable]
    public class ServiceReturnedNoInformationException : SdkException
    {
        /// <summary>
        /// Initializes a new instance of the ServiceReturnedNoInformationException class.
        /// </summary>
        public ServiceReturnedNoInformationException()
            : base(Properties.Resources.ServiceReturnedNoInformationExceptionDefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ServiceReturnedNoInformationException class.
        /// </summary>
        /// <param name="errorMessage">Error Message.</param>
        public ServiceReturnedNoInformationException(string errorMessage)
            : base(errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ServiceReturnedNoInformationException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="innerException">Inner Exception.</param>
        public ServiceReturnedNoInformationException(string errorMessage, System.Exception innerException)
            : base(errorMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ServiceReturnedNoInformationException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="errorCode">Error Code.</param>
        /// <param name="source">Source of the exception.</param>
        public ServiceReturnedNoInformationException(string errorMessage, string errorCode, string source)
            : base(errorMessage, errorCode, source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ServiceReturnedNoInformationException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="errorCode">Error Code.</param>
        /// <param name="source">Source of the exception.</param>
        /// <param name="innerException">Inner Exception.</param>
        public ServiceReturnedNoInformationException(string errorMessage, string errorCode, string source, System.Exception innerException)
            : base(errorMessage, errorCode, source, innerException)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the ServiceReturnedNoInformationException class.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        protected ServiceReturnedNoInformationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
