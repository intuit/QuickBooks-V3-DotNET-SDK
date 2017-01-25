////********************************************************************
// <copyright file="RequestParameters.cs" company="Intuit">
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
// <summary>This file contains SdkException.</summary>
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
