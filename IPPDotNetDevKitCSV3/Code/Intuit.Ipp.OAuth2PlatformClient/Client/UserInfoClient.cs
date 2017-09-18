// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// UserInfoClient class
    /// </summary>
    public class UserInfoClient
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpoint">endpoint</param>
        public UserInfoClient(string endpoint)
            : this(endpoint, new HttpClientHandler())
        { }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpoint">endpoint</param>
        /// <param name="innerHttpMessageHandler">innerHttpMessageHandler</param>
        public UserInfoClient(string endpoint, HttpMessageHandler innerHttpMessageHandler)
        {
            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));
            if (innerHttpMessageHandler == null) throw new ArgumentNullException(nameof(innerHttpMessageHandler));

            _client = new HttpClient(innerHttpMessageHandler)
            {
                BaseAddress = new Uri(endpoint)
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Timeout
        /// </summary>
        public TimeSpan Timeout
        {
            set
            {
                _client.Timeout = value;
            }
        }


        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>Task of UserInfoResponse</returns>
        public async Task<UserInfoResponse> GetAsync(string token, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(token)) throw new ArgumentNullException(nameof(token));

            var request = new HttpRequestMessage(HttpMethod.Get, "");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response;
            try
            {
                response = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return new UserInfoResponse(ex);
            }

            if (!response.IsSuccessStatusCode)
            {
              
                string errorDetail = "";

                HttpResponseHeaders headers = response.Headers;
                if (headers.WwwAuthenticate != null)
                {
                    errorDetail = headers.WwwAuthenticate.ToString();
                }

                if (errorDetail != null && errorDetail !="")
                {
                    return new UserInfoResponse(response.StatusCode, response.ReasonPhrase + ": " + errorDetail);

                }
                else
                {
                    return new UserInfoResponse(response.StatusCode, response.ReasonPhrase);
                }
            }

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new UserInfoResponse(content);
        }
    }
}