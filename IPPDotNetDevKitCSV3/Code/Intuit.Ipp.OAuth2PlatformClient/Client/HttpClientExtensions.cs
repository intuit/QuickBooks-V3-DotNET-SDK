// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Net.Http.Headers;

namespace System.Net.Http
{
    /// <summary>
    /// HttpClientExtensions for Headers
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Sets Basic Authentication header value
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="clientId">clientId</param>
        /// <param name="clientSecret">clientSecret</param>
        public static void SetBasicAuthentication(this HttpClient client, string clientId, string clientSecret)
        {
            client.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(clientId, clientSecret);
        }

        /// <summary>
        /// Sets Token value
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="scheme">scheme</param>
        /// <param name="token">token</param>
        public static void SetToken(this HttpClient client, string scheme, string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, token);
        }


        /// <summary>
        /// Sets BearerToken value
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="token">token</param>
        public static void SetBearerToken(this HttpClient client, string token)
        {
            client.SetToken("Bearer", token);
        }
    }
}