////*********************************************************
// <copyright file="ChannelTerminatedException.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains ChannelTerminatedException.</summary>
////*********************************************************

namespace Intuit.Ipp.Exception
{
    using System.Runtime.Serialization;
    using Intuit.Ipp.Exception.Properties;

    /// <summary>
    /// Represents an Exception raised when the communicating channel was terminated due to an unknown error.
    /// </summary>
    [System.Serializable]
    public class ChannelTerminatedException : ServiceException
    {
        /// <summary>
        /// Initializes a new instance of the ChannelTerminatedException class.
        /// </summary>
        public ChannelTerminatedException()
            : base(Resources.ChannelTerminatedExceptionDefaultMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ChannelTerminatedException class.
        /// </summary>
        /// <param name="errorMessage">Error Message.</param>
        public ChannelTerminatedException(string errorMessage)
            : base(errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ChannelTerminatedException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="innerException">Inner Exception.</param>
        public ChannelTerminatedException(string errorMessage, System.Exception innerException)
            : base(errorMessage, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ChannelTerminatedException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="errorCode">Error Code.</param>
        /// <param name="source">Source of the exception.</param>
        public ChannelTerminatedException(string errorMessage, string errorCode, string source)
            : base(errorMessage, errorCode, source)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ChannelTerminatedException class.
        /// </summary>
        /// <param name="errorMessage">Error Message</param>
        /// <param name="errorCode">Error Code.</param>
        /// <param name="source">Source of the exception.</param>
        /// <param name="innerException">Inner Exception.</param>
        public ChannelTerminatedException(string errorMessage, string errorCode, string source, System.Exception innerException)
            : base(errorMessage, errorCode, source, innerException)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the ChannelTerminatedException class.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information about the source or destination.</param>
        protected ChannelTerminatedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
