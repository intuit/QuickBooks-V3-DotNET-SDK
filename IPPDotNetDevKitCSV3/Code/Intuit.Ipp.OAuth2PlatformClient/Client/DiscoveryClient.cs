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
    /// Discovery Client ot get details from Discovery Url
    /// </summary>
    public class DiscoveryClient 
    {
        /// <summary>
        /// GetAsync call for Discovery Url
        /// </summary>
        /// <param name="authority">authority</param>
        public static async Task<DiscoveryResponse> GetAsync(string authority)
        {
            var client = new DiscoveryClient(authority);
            return await client.GetAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// HttpClient
        /// </summary>
        private readonly HttpClient _client;


        /// <summary>
        /// Authority
        /// </summary>
        public string Authority { get; }


        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Policy
        /// </summary>
        public DiscoveryPolicy Policy { get; set; } = new DiscoveryPolicy();

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
        /// DiscoveryClient Method to call discoery url
        /// </summary>
        /// <param name="authority">authority</param>
        /// <param name="innerHandler">innerHandler</param>
        public DiscoveryClient(string authority, HttpMessageHandler innerHandler = null)
        {
            var handler = innerHandler ?? new HttpClientHandler();

            Uri uri;
            var success = Uri.TryCreate(authority, UriKind.Absolute, out uri);
            if (success == false)
            {
                throw new InvalidOperationException("Malformed authority URL");
            }

            if (!DiscoveryUrlHelper.IsValidScheme(uri))
            {
                throw new InvalidOperationException("Malformed authority URL");
            }

            var url = authority.RemoveTrailingSlash();
            if (url.EndsWith(OidcConstants.Discovery.ProdDiscoveryEndpoint, StringComparison.OrdinalIgnoreCase))
            {
                Url = url;
                Authority = url.Substring(0, url.Length - OidcConstants.Discovery.ProdDiscoveryEndpoint.Length - 1);
            }
            else if (url.EndsWith(OidcConstants.Discovery.SandboxDiscoveryEndpoint, StringComparison.OrdinalIgnoreCase))
            {
                Url = url;
                Authority = url.Substring(0, url.Length - OidcConstants.Discovery.SandboxDiscoveryEndpoint.Length - 1);
            }
            else
            {
                Authority = url;
                Url = url.EnsureTrailingSlash() + OidcConstants.Discovery.ProdDiscoveryEndpoint;
            }

            _client = new HttpClient(handler);
        }

        /// <summary>
        /// GetAsync call for Discovery
        /// </summary>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>Task for Discoverresponse</returns>
        public async Task<DiscoveryResponse> GetAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            //Policy.Authority = Authority;
            string jwkUrl = "";

            if (!DiscoveryUrlHelper.IsSecureScheme(new Uri(Url), Policy))
            {
                return new DiscoveryResponse(new InvalidOperationException("HTTPS required"), $"Error connecting to {Url}");
            }

            try
            {
                var response = await _client.GetAsync(Url, cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    string errorDetail = "";

                    HttpResponseHeaders headers = response.Headers;
                    if (headers.WwwAuthenticate != null)
                    {
                        errorDetail = headers.WwwAuthenticate.ToString();
                    }

                  
                    if (errorDetail != null && errorDetail != "")
                    {
                        return new DiscoveryResponse(response.StatusCode, $"Error connecting to {Url}: {response.ReasonPhrase}: { errorDetail}");

                    }
                    else
                    {
                        return new DiscoveryResponse(response.StatusCode, $"Error connecting to {Url}: {response.ReasonPhrase}");
                    }
                }

                var disco = new DiscoveryResponse(await response.Content.ReadAsStringAsync().ConfigureAwait(false), Policy);
                if (disco.IsError)
                {
                    return disco;
                }

                
                try
                {
                    jwkUrl = disco.JwksUri;
                    if (jwkUrl != null)
                    {
                        response = await _client.GetAsync(jwkUrl, cancellationToken).ConfigureAwait(false);

                        if (!response.IsSuccessStatusCode)
                        {
                            return new DiscoveryResponse(response.StatusCode, $"Error connecting to {jwkUrl}: {response.ReasonPhrase}");
                        }

                        var jwk = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        disco.KeySet = new JsonWebKeySet(jwk);
                    }

                    return disco;
                }
                catch (Exception ex)
                {
                    return new DiscoveryResponse(ex, $"Error connecting to {jwkUrl}");
                }
            }
            catch (Exception ex)
            {
                return new DiscoveryResponse(ex, $"Error connecting to {Url}");
            }
        }
    }
}