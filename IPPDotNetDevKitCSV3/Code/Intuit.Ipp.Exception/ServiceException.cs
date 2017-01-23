////*********************************************************
// <copyright file="ServiceException.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains ServiceException.</summary>
////*********************************************************

namespace Intuit.Ipp.Exception
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Intuit.Ipp.Exception.Properties;

    /// <summary>
    /// Represents an exception raised by the Intuit Service.
    /// </summary>
    [System.Serializable]
    public class ServiceException : IdsException
    {


        /// <summary>
        /// Initializes a new instance of the ServiceException class.
        /// </summary>
        /// <param name="innerExceptions">Inner Exceptions.</param>
        public ServiceException(IList<IdsError> innerExceptions)
            : base(Resources.ServiceExceptionDefaultMessage, innerExceptions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ServiceException class.
        /// </summary>
        public ServiceException()
            : base(Resources.ServiceExceptionDefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ServiceException class.
        /// </summary>
        /// <param name="errorMessage">Error Message.</param>
        public ServiceException(string errorMessage)
            : base(errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ServiceException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="innerException">Inner Exception.</param>
        public ServiceException(string errorMessage, System.Exception innerException)
            : base(errorMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ServiceException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="errorCode">Error Codes (400, 200, 404).</param>
        /// <param name="source">Source of the exception.</param>
        public ServiceException(string errorMessage, string errorCode, string source)
            : base(errorMessage, errorCode, source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ServiceException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="errorCode">Error Codes (400, 200, 404).</param>
        /// <param name="source">Source of the exception.</param>
        /// <param name="innerException">Inner Exception.</param>
        public ServiceException(string errorMessage, string errorCode, string source, System.Exception innerException)
            : base(errorMessage, errorCode, source, innerException)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the ServiceException class.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        protected ServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
