////*********************************************************
// <copyright file="ServiceReturnedNoInformationException.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
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
