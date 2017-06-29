// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// RefreshTokenHandler Class
    /// </summary>
    public class RefreshTokenHandler : DelegatingHandler
    {
        private static readonly TimeSpan _lockTimeout = TimeSpan.FromSeconds(2);

        private readonly SemaphoreSlim _lock = new SemaphoreSlim(1, 1);
        private readonly TokenClient _tokenClient;

        private string _accessToken;
        private string _refreshToken;
        private bool _disposed;

        /// <summary>
        /// Gets the current access token
        /// </summary>
        public string AccessToken
        {
            get
            {
                if (_lock.Wait(_lockTimeout))
                {
                    try
                    {
                        return _accessToken;
                    }
                    finally
                    {
                        _lock.Release();
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the current refresh token
        /// </summary>
        public string RefreshToken
        {
            get
            {
                if (_lock.Wait(_lockTimeout))
                {
                    try
                    {
                        return _refreshToken;
                    }
                    finally
                    {
                        _lock.Release();
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tokenEndpoint"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="refreshToken"></param>
        /// <param name="accessToken"></param>
        /// <param name="innerHandler"></param>
        public RefreshTokenHandler(string tokenEndpoint, string clientId, string clientSecret, string refreshToken, string accessToken = null, HttpMessageHandler innerHandler = null)
            : this(new TokenClient(tokenEndpoint, clientId, clientSecret), refreshToken, accessToken, innerHandler)
        {

            ClientId = clientId;
            ClientSecret = clientSecret;


        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="client"></param>
        /// <param name="refreshToken"></param>
        /// <param name="accessToken"></param>
        /// <param name="innerHandler"></param>
        public RefreshTokenHandler(TokenClient client, string refreshToken, string accessToken = null, HttpMessageHandler innerHandler = null)
        {
            _tokenClient = client;
            _refreshToken = refreshToken;
            _accessToken = accessToken;

            InnerHandler = innerHandler ?? new HttpClientHandler();
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
        /// Override methos for SendAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task<HttpResponseMessage></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var accessToken = await GetAccessTokenAsync(cancellationToken);
            //if (string.IsNullOrEmpty(accessToken))
            //{
                if (await RefreshTokensAsync(cancellationToken) == false)
                {
                    return new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            //}

            //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
           
            request.Headers.Authorization = new BasicAuthenticationHeaderValue(ClientId, ClientSecret);
            request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
                       

            return response;
        }


        protected override void Dispose(bool disposing)
        {
          if (disposing && !_disposed) {
              _disposed = true;
              _lock.Dispose();
          }

          base.Dispose(disposing);
        }

        private async Task<bool> RefreshTokensAsync(CancellationToken cancellationToken)
        {
            var refreshToken = RefreshToken;
            if (string.IsNullOrEmpty(refreshToken))
            {
                return false;
            }

            if (await _lock.WaitAsync(_lockTimeout, cancellationToken).ConfigureAwait(false))
            {
                try
                {
                    var response = await _tokenClient.RequestRefreshTokenAsync(refreshToken, cancellationToken: cancellationToken).ConfigureAwait(false);

                    if (!response.IsError)
                    {
                        _accessToken = response.AccessToken;
                        _refreshToken = response.RefreshToken;

                        return true;
                    }
                }
                finally
                {
                    _lock.Release();
                }
            }

            return false;
        }

        private async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken)
        {
            if (await _lock.WaitAsync(_lockTimeout, cancellationToken).ConfigureAwait(false))
            {
                try
                {
                    return _accessToken;
                }
                finally
                {
                    _lock.Release();
                }
            }

            return null;
        }
    }
}