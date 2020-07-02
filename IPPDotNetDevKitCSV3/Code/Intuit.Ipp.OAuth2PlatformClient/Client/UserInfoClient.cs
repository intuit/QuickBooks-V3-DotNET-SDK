// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// Modified for Intuit's Oauth2 implementation

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
            if (OAuth2Client.AdvancedLoggerEnabled == false)
            {
                //Intialize Logger
                OAuth2Client.AdvancedLogger = LogHelper.GetAdvancedLogging(enableSerilogRequestResponseLoggingForDebug: false, enableSerilogRequestResponseLoggingForTrace: false, enableSerilogRequestResponseLoggingForConsole: false, enableSerilogRequestResponseLoggingForRollingFile: false, serviceRequestLoggingLocationForFile: System.IO.Path.GetTempPath());
            }

            if (endpoint == null) throw new ArgumentNullException(nameof(endpoint));
            if (innerHttpMessageHandler == null) throw new ArgumentNullException(nameof(innerHttpMessageHandler));

            _client = new HttpClient(innerHttpMessageHandler)
            {
                BaseAddress = new Uri(endpoint)
            };

            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if (OAuth2Client.AdvancedLoggerEnabled != false)
            {
                OAuth2Client.AdvancedLogger.Log("UserInfo request initiated");
                OAuth2Client.AdvancedLogger.Log("Request url- " + endpoint);
            }
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

            if (OAuth2Client.AdvancedLoggerEnabled != false)
            {
                OAuth2Client.AdvancedLogger.Log("Request headers- ");
                OAuth2Client.AdvancedLogger.Log("Authorization Header: " + request.Headers.Authorization.ToString());

                OAuth2Client.AdvancedLogger.Log("Accept header: " + "application/json");
            }


            HttpResponseMessage response;
            try
            {
                response = await _client.SendAsync(request, cancellationToken).ConfigureAwait(false);
                HttpResponseHeaders headers = response.Headers;
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

                string errorDetail = "";


                if (!response.IsSuccessStatusCode)
                {

                   


                    if (headers.WwwAuthenticate != null)
                    {
                        errorDetail = headers.WwwAuthenticate.ToString();
                    }

                    if (errorDetail != null && errorDetail != "")
                    {
                        if (OAuth2Client.AdvancedLoggerEnabled != false)
                        {
                            if (OAuth2Client.ShowInfoLogs == true)//log just intuit_tid for info logging mode
                                OAuth2Client.AdvancedLogger.Log("Response Intuit_Tid header - " + intuit_tid + ", Response: Status Code- " + response.StatusCode);
                            else
                                OAuth2Client.AdvancedLogger.Log("Response Intuit_Tid header - " + intuit_tid + ", Response: Status Code- " + response.StatusCode + ", Error Details- " + response.ReasonPhrase + ": " + errorDetail);

                        }
                        return new UserInfoResponse(response.StatusCode, response.ReasonPhrase + ": " + errorDetail);

                    }
                    else
                    {
                        if (OAuth2Client.AdvancedLoggerEnabled != false)
                        {

                            if (OAuth2Client.ShowInfoLogs == true)//log just intuit_tid for info logging mode
                                OAuth2Client.AdvancedLogger.Log("Response Intuit_Tid header - " + intuit_tid + ", Response: Status Code- " + response.StatusCode);
                            else
                                OAuth2Client.AdvancedLogger.Log("Response Intuit_Tid header - " + intuit_tid + ", Response: Status Code- " + response.StatusCode + ", Error Details- " + response.ReasonPhrase);

                        }
                        return new UserInfoResponse(response.StatusCode, response.ReasonPhrase);
                    }
                }

                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (OAuth2Client.AdvancedLoggerEnabled != false)
                {
                    if (OAuth2Client.ShowInfoLogs == true)//log just intuit_tid for info logging mode
                        OAuth2Client.AdvancedLogger.Log("Response Intuit_Tid header - " + intuit_tid + ", Response Status Code- " + response.StatusCode);
                    else
                        OAuth2Client.AdvancedLogger.Log("Response Intuit_Tid header - " + intuit_tid + ", Response Status Code- " + response.StatusCode + ", Response Body- " + content);
                }
                return new UserInfoResponse(content);
            }
            catch (System.Exception ex)
            {
                return new UserInfoResponse(ex);
            }
        }
    }
}