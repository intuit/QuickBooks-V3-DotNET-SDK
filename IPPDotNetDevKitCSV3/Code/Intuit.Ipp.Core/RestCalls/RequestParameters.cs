////********************************************************************
// <copyright file="RequestParameters.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains RequestParameters details.</summary>
////********************************************************************

namespace Intuit.Ipp.Core.Rest
{
    /// <summary>
    /// Parameters for calling Rest calls.
    /// </summary>
    public class RequestParameters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestParameters"/> class.
        /// </summary>
        /// <param name="resourceUri">The resource URI.</param>
        /// <param name="verb">The http verb.</param>
        /// <param name="contentType">Type of the content.</param>
        public RequestParameters(string resourceUri, HttpVerbType verb, string contentType)
            : this(resourceUri, verb, contentType, string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestParameters"/> class.
        /// </summary>
        /// <param name="resourceUri">The resource URI.</param>
        /// <param name="verb">The http verb.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <param name="apiName">Name of the API.</param>
        public RequestParameters(string resourceUri, HttpVerbType verb, string contentType, string apiName)
        {
            this.ResourceUri = resourceUri;
            this.Verb = verb;
            this.ContentType = contentType;
            this.ApiName = apiName;
        }

        /// <summary>
        /// Gets the resource URI.
        /// </summary>
        /// <value>
        /// The resource URI.
        /// </value>
        public string ResourceUri { get; private set; }

        /// <summary>
        /// Gets the verb.
        /// </summary>
        /// <value>
        /// The http verb.
        /// </value>
        public HttpVerbType Verb { get; private set; }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public string ContentType { get; private set; }

        /// <summary>
        /// Gets the name of the API.
        /// </summary>
        /// <value>
        /// The name of the API.
        /// </value>
        public string ApiName { get; private set; }
    }
}
