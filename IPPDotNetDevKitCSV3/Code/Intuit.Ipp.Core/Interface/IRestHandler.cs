////********************************************************************
// <copyright file="IRestHandler.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains interface for REST request handler.</summary>
////********************************************************************

using System.IO;

namespace Intuit.Ipp.Core.Rest
{
    using System.Net;

    /// <summary>
    /// IRestHandler contains the methods for preparing the REST request, calls REST services and returns the response.
    /// </summary>
    public interface IRestHandler
    {
        /// <summary>
        /// Prepares the HttpWebRequest along with authentication header added to the request.
        /// </summary>
        /// <param name="requestParameters">The parameters.</param>
        /// <param name="requestBody">The request entity.</param>
        /// <param name="oauthRequestUri">The OAuth request uri.</param>
        /// <returns>
        /// Http web request object.
        /// </returns>
        HttpWebRequest PrepareRequest(RequestParameters requestParameters, object requestBody, string oauthRequestUri = null, bool includeRequestId = true);

        /// <summary>
        /// Returns the response by calling REST service.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Response from REST service.</returns>
        string GetResponse(HttpWebRequest request);

        /// <summary>
        /// Returns the response as streamn by calling REST service.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Response from REST service.</returns>
        byte[] GetResponseStream(HttpWebRequest request);
    }
}
