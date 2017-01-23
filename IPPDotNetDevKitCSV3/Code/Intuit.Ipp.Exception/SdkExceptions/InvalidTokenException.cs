////*********************************************************
// <copyright file="InvalidTokenException.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains SDK exception InvalidTokenException.</summary>
////*********************************************************

namespace Intuit.Ipp.Exception
{
    using System.Runtime.Serialization;
    using Intuit.Ipp.Exception.Properties;

    /// <summary>
    /// Represents an Exception raised when an invalid token was generated.
    /// </summary>
    [System.Serializable]
    public class InvalidTokenException : SdkException
    {
        /// <summary>
        /// Initializes a new instance of the InvalidTokenException class.
        /// </summary>
        public InvalidTokenException()
            : base(Resources.InvalidTokenExceptionDefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the InvalidTokenException class.
        /// </summary>
        /// <param name="errorMessage">Error Message.</param>
        public InvalidTokenException(string errorMessage)
            : base(errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the InvalidTokenException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="innerException">Inner Exception.</param>
        public InvalidTokenException(string errorMessage, System.Exception innerException)
            : base(errorMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the InvalidTokenException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="errorCode">Error Code.</param>
        /// <param name="source">Source of the exception.</param>
        public InvalidTokenException(string errorMessage, string errorCode, string source)
            : base(errorMessage, errorCode, source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the InvalidTokenException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="errorCode">Error Code.</param>
        /// <param name="source">Source of the exception.</param>
        /// <param name="innerException">Inner Exception.</param>
        public InvalidTokenException(string errorMessage, string errorCode, string source, System.Exception innerException)
            : base(errorMessage, errorCode, source, innerException)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the InvalidTokenException class.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        protected InvalidTokenException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
