////********************************************************************
// <copyright file="IRestHandler.cs" company="Intuit">
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
// <summary>This file contains interface for REST Async Await request handler.</summary>
////********************************************************************

namespace Intuit.Ipp.Core.Rest
{
    using System.Net.Http;
    using System.Threading.Tasks;

    public interface IAsyncAwaitHandler
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
        HttpRequestMessage PrepareRequestMessage(RequestParameters requestParameters, object requestBody, string oauthRequestUri = null, bool includeRequestId = true);


        /// <summary>
        /// Returns the response by calling REST service using HttpRequestMessage.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Response from REST service.</returns>
        Task<string> GetResponseAsync(HttpClient client, HttpRequestMessage request);

        /// <summary>
        /// Returns the response as streamn by calling REST service using HttpRequestMessage.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Response from REST service.</returns>
        Task<byte[]> GetResponseStreamAsync(HttpClient client, HttpRequestMessage request);
    }
}
