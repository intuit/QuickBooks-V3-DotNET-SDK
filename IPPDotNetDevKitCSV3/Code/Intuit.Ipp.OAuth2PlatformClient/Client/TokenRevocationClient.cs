// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// Modified for Intuit's Oauth2 implementation

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
    public class TokenRevocationClient : IDisposable
    {
        private bool _disposed;

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
        /// <param name="endpoint">endpoint</param>
        /// <param name="clientId">clientId</param>
        /// <param name="clientSecret">clientSecret></param>
        /// <param name="innerHttpMessageHandler">innerHttpMessageHandler</param>
        public TokenRevocationClient(string endpoint, string clientId = "", string clientSecret = "", HttpMessageHandler innerHttpMessageHandler = null)
        {
            if (OAuth2Client.AdvancedLoggerEnabled == false)
            {
                //Intialize Logger
                OAuth2Client.AdvancedLogger = LogHelper.GetAdvancedLogging(enableSerilogRequestResponseLoggingForDebug: false, enableSerilogRequestResponseLoggingForTrace: false, enableSerilogRequestResponseLoggingForConsole: false, enableSerilogRequestResponseLoggingForRollingFile: false, serviceRequestLoggingLocationForFile: System.IO.Path.GetTempPath());
            }

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
        /// <param name="request">request</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>Task of TokenRevocationResponse</returns>
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
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            msgRequest.Content = stringContent;

            if (OAuth2Client.AdvancedLoggerEnabled)
            {
                OAuth2Client.AdvancedLogger.Log("Request url- " + Address);
                OAuth2Client.AdvancedLogger.Log("Request headers- ");
                OAuth2Client.AdvancedLogger.Log("Authorization Header: " + Client.DefaultRequestHeaders.Authorization.ToString());//check
                OAuth2Client.AdvancedLogger.Log("ContentType header: " + "application/json");
                OAuth2Client.AdvancedLogger.Log("Accept header: " + "application/json");
                OAuth2Client.AdvancedLogger.Log("Request Body: " + await msgRequest.Content.ReadAsStringAsync().ConfigureAwait(false));
            }

            try
            {
                var response = await Client.PostAsync("", msgRequest.Content).ConfigureAwait(false);
                HttpResponseHeaders headers = response.Headers;

              
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string intuit_tid;
                      IEnumerable<string> values;
                    if (headers.TryGetValues("intuit_tid", out values))
                    {
                        intuit_tid = values.First();
                    }
                    else
                    {
                        intuit_tid = "None";
                    }

                    if (OAuth2Client.AdvancedLoggerEnabled)
                    {
                        if (OAuth2Client.ShowInfoLogs)//log just intuit_tid for info logging mode
                            OAuth2Client.AdvancedLogger.Log("Response Intuit_Tid header - " + intuit_tid + ", Response Status Code- " + response.StatusCode + ", Token Revoked successfully");
                        else
                            OAuth2Client.AdvancedLogger.Log("Response Intuit_Tid header - " + intuit_tid + ", Response Status Code- " + response.StatusCode + ", Token Revoked successfully");
                    }
                    return new TokenRevocationResponse();
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    string intuit_tid;
                    IEnumerable<string> values;
                    if (headers.TryGetValues("intuit_tid", out values))
                    {
                        intuit_tid = values.First();
                    }
                    else
                    {
                        intuit_tid = "None";
                    }

                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    if (OAuth2Client.AdvancedLoggerEnabled)
                    {
                        if (OAuth2Client.ShowInfoLogs)//log just intuit_tid for info logging mode
                            OAuth2Client.AdvancedLogger.Log("Response Intuit_Tid header - " + intuit_tid + ", Response Status Code- " + response.StatusCode);
                        else
                            OAuth2Client.AdvancedLogger.Log("Response Intuit_Tid header - " + intuit_tid + ", Response Status Code- " + response.StatusCode + ", Response Body- " + content);
                    }
                    return new TokenRevocationResponse(content); //errorDetail can be added here if required.
                }
                else
                {
                    string errorDetail = "";


                    if (headers.WwwAuthenticate != null)
                    {
                        errorDetail = headers.WwwAuthenticate.ToString();
                    }

                    if (errorDetail != null && errorDetail != "")
                    {
                        if (OAuth2Client.AdvancedLoggerEnabled)
                        {
                            if (OAuth2Client.ShowInfoLogs)//log just intuit_tid for info logging mode
                                OAuth2Client.AdvancedLogger.Log("Response: Status Code- " + response.StatusCode);
                            else
                                OAuth2Client.AdvancedLogger.Log("Response: Status Code- " + response.StatusCode + ", Error Details- " + response.ReasonPhrase + ": " + errorDetail);
                        }
                        return new TokenRevocationResponse(response.StatusCode, response.ReasonPhrase + ": " + errorDetail);
                    }
                    else
                    {
                        if (OAuth2Client.AdvancedLoggerEnabled)
                        {
                            if (OAuth2Client.ShowInfoLogs)//log just intuit_tid for info logging mode
                                OAuth2Client.AdvancedLogger.Log("Response: Status Code- " + response.StatusCode);
                            else
                                OAuth2Client.AdvancedLogger.Log("Response: Status Code- " + response.StatusCode + ", Error Details- " + response.ReasonPhrase + ": " + errorDetail);
                        }
                        return new TokenRevocationResponse(response.StatusCode, response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                return new TokenRevocationResponse(ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

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