// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// JsonToken Class
    /// </summary>
    public class JsonToken
    {
        public string token { get; set; }
    }

    /// <summary>
    /// TokenRevocationClient Class
    /// </summary>
    public class TokenRevocationClient
    {
        /// <summary>
        /// Client
        /// </summary>
        protected HttpClient Client;

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// AuthenticationStyle
        /// </summary>
        public AuthenticationStyle AuthenticationStyle { get; set; }

        /// <summary>
        /// ClientId
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// ClientSecret
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// TokenRevocationClient
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="innerHttpMessageHandler"></param>
        public TokenRevocationClient(string endpoint, string clientId = "", string clientSecret = "", HttpMessageHandler innerHttpMessageHandler = null)
        {
            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));
            if (innerHttpMessageHandler == null) innerHttpMessageHandler = new HttpClientHandler();

            Client = new HttpClient(innerHttpMessageHandler)
            {
                BaseAddress = new Uri(endpoint)

            };

            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));        
            if (!string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(clientSecret))
            {
                Client.SetBasicAuthentication(clientId, clientSecret);

            

            }
            
            Address = endpoint;
    }

        /// <summary>
        /// Timeout
        /// </summary>
        public TimeSpan Timeout
        {
            set
            {
                Client.Timeout = value;
            }
        }

        /// <summary>
        /// RevokeAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task<TokenRevocationResponse></returns>
        public virtual async Task<TokenRevocationResponse> RevokeAsync(
            TokenRevocationRequest request,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (string.IsNullOrWhiteSpace(request.Token)) throw new ArgumentNullException(nameof(request.Token));

            HttpRequestMessage msgRequest = new HttpRequestMessage(HttpMethod.Post, Address);
       
            JsonToken jobject = new JsonToken();
            jobject.token = request.Token;//Refresh token or bearer access token
            var json = JsonConvert.SerializeObject(jobject);
            var stringContent = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

            msgRequest.Content = stringContent;
            



            try
            {
               
                var response = await Client.PostAsync("", msgRequest.Content).ConfigureAwait(false);
             

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return new TokenRevocationResponse();
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    return new TokenRevocationResponse(content); //errorDetail can be added here if required.
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
                        return new TokenRevocationResponse(response.StatusCode, response.ReasonPhrase + ": " + errorDetail);

                    }
                    else
                    {
                        return new TokenRevocationResponse(response.StatusCode, response.ReasonPhrase);
                    }

                }
            }
            catch (Exception ex)
            {
                return new TokenRevocationResponse(ex);
            }
        }
    }
}