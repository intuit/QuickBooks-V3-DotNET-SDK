// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// Modified for Intuit's Oauth2 implementation

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Intuit.Ipp.Core.Rest;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// TokenClient Class
    /// </summary>
    public class TokenClient : IDisposable
    {
        protected HttpClient Client;
        private bool _disposed;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpoint">endpoint</param>
        public TokenClient(string endpoint)
            : this(endpoint, new HttpClientHandler())
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpoint">endpoint</param>
        /// <param name="innerHttpMessageHandler">innerHttpMessageHandler</param>
        public TokenClient(string endpoint, HttpMessageHandler innerHttpMessageHandler)
        {
            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));
            if (innerHttpMessageHandler == null) throw new ArgumentNullException(nameof(innerHttpMessageHandler));

            Client = new HttpClient(innerHttpMessageHandler);

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            AuthenticationStyle = AuthenticationStyle.OAuth2;
            Address = endpoint;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpoint">endpoint</param>
        /// <param name="clientId">clientId</param>
        /// <param name="clientSecret">clientSecret</param>
        /// <param name="style">style</param>
        public TokenClient(string endpoint, string clientId, string clientSecret, AuthenticationStyle style = AuthenticationStyle.OAuth2)
            : this(endpoint, clientId, clientSecret, new HttpClientHandler(), style)
        { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="endpoint">endpoint</param>
        /// <param name="clientId">clientId</param>
        /// <param name="clientSecret">clientSecret</param>
        /// <param name="innerHttpMessageHandler">innerHttpMessageHandler</param>
        /// <param name="style"></param>
        public TokenClient(string endpoint, string clientId, string clientSecret, HttpMessageHandler innerHttpMessageHandler, AuthenticationStyle style = AuthenticationStyle.OAuth2)
            : this(endpoint, innerHttpMessageHandler)
        {
            if (string.IsNullOrEmpty(clientId)) throw new ArgumentNullException(nameof(clientId));

            AuthenticationStyle = style;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }

        /// <summary>
        /// ClientId
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// ClientSecret
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// AuthenticationStyle
        /// </summary>
        public AuthenticationStyle AuthenticationStyle { get; set; }

        /// <summary>
        /// TimeOut
        /// </summary>
        public TimeSpan Timeout
        {
            set
            {
                Client.Timeout = value;
            }
        }

        /// <summary>
        /// RequestAsync call
        /// </summary>
        /// <param name="form">form</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>task of TokenResponse</returns>
        public virtual async Task<TokenResponse> RequestAsync(IDictionary<string, string> form, CancellationToken cancellationToken = default(CancellationToken))
        {
            HttpResponseMessage response;

            var request = new HttpRequestMessage(HttpMethod.Post, Address);
          
            request.Content = new FormUrlEncodedContent(form);

            if (AuthenticationStyle == AuthenticationStyle.OAuth2)
            {
                request.Headers.Authorization = new BasicAuthenticationHeaderValue(ClientId, ClientSecret);
                request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded");
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            OAuth2Client.AdvancedLogger.Log("Request url- "+ Address);
            OAuth2Client.AdvancedLogger.Log("Request headers- ");
            OAuth2Client.AdvancedLogger.Log("Authorization Header: " + request.Headers.Authorization.ToString());
            OAuth2Client.AdvancedLogger.Log("ContentType header: " + request.Content.Headers.ContentType.ToString());
            OAuth2Client.AdvancedLogger.Log("Accept header: " + "application/json");
            OAuth2Client.AdvancedLogger.Log("Request Body: " + await request.Content.ReadAsStringAsync().ConfigureAwait(false));
            try
            {
                response = await Client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (System.Exception ex)
            {
                return new TokenResponse(ex);
            }

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
            {
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);//errorDetail can be added here if required for BadRequest.
                OAuth2Client.AdvancedLogger.Log("Response Status Code- " + response.StatusCode+", Response Body- "+content);
                return new TokenResponse(content);
            }
            else
            {
                string errorDetail = "";

                HttpResponseHeaders headers = response.Headers;
                if (headers.WwwAuthenticate != null)
                {
                    errorDetail = headers.WwwAuthenticate.ToString();
                }

                if (errorDetail != null && errorDetail != "")
                {
                    OAuth2Client.AdvancedLogger.Log("Response: Status Code- " + response.StatusCode + ", Error Details- " + response.ReasonPhrase + ": " + errorDetail);
                    return new TokenResponse(response.StatusCode, response.ReasonPhrase + ": " + errorDetail);
                }
                else
                {
                    OAuth2Client.AdvancedLogger.Log("Response: Status Code- " + response.StatusCode + ", Error Details- " + response.ReasonPhrase);
                    return new TokenResponse(response.StatusCode, response.ReasonPhrase);
                }
            }
        }

        /// <summary>
        /// Dispose call
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Virtual Dispose call
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                Client.Dispose();

                _disposed = true;
            }
        }
    }
}