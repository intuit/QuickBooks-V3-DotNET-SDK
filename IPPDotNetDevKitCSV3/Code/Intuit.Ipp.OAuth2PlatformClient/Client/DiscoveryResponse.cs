// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Intuit.Ipp.OAuth2PlatformClient;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;


namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// DiscoveryResponse class to handle response from Discovery  call
    /// </summary>
    public class DiscoveryResponse
    {
        public string Raw { get; }
        public JObject Json { get; }

        public bool IsError { get; } = false;
        public HttpStatusCode StatusCode { get; }
        public string Error { get; }
        public ResponseErrorType ErrorType { get; set; } = ResponseErrorType.None;
        public Exception Exception { get; }

        public JsonWebKeySet KeySet { get; set; }


        /// <summary>
        /// Handles success raw response from Token api call
        /// </summary>
        /// <param name="raw">raw</param>
        /// <param name="policy">policy</param>
        public DiscoveryResponse(string raw, DiscoveryPolicy policy = null)
        {
            if (policy == null) policy = new DiscoveryPolicy();

            IsError = false;
            StatusCode = HttpStatusCode.OK;
            Raw = raw;

            try
            {
                Json = JObject.Parse(raw);
                var validationError = Validate(policy);

                if (!string.IsNullOrEmpty(validationError))
                {
                    IsError = true;
                    Json = null;

                    ErrorType = ResponseErrorType.PolicyViolation;
                    Error = validationError;
                }
            }
            catch (Exception ex)
            {
                IsError = true;

                ErrorType = ResponseErrorType.Exception;
                Error = ex.Message;
                Exception = ex;
            }
        }

        /// <summary>
        /// Handles exception response from Token api call
        /// </summary>
        /// <param name="statusCode">statusCode</param>
        /// <param name="reason">reason</param>
        public DiscoveryResponse(HttpStatusCode statusCode, string reason)
        {
            IsError = true;

            ErrorType = ResponseErrorType.Http;
            StatusCode = statusCode;
            Error = reason;
        }

        /// <summary>
        /// Handles exception response from Token api call
        /// </summary>
        /// <param name="exception">exception</param>
        /// <param name="errorMessage">errorMessage</param>
        public DiscoveryResponse(Exception exception, string errorMessage)
        {
            IsError = true;

            ErrorType = ResponseErrorType.Exception;
            Exception = exception;
            Error = $"{errorMessage}: {exception.Message}";
        }
        /// <summary>
        /// Strongly typed getters
        /// </summary>
        public string Issuer => TryGetString(OidcConstants.Discovery.Issuer);
        public string AuthorizeEndpoint => TryGetString(OidcConstants.Discovery.AuthorizationEndpoint);
        public string TokenEndpoint => TryGetString(OidcConstants.Discovery.TokenEndpoint);
        public string UserInfoEndpoint => TryGetString(OidcConstants.Discovery.UserInfoEndpoint);
        public string RevocationEndpoint => TryGetString(OidcConstants.Discovery.RevocationEndpoint);
        public string JwksUri => TryGetString(OidcConstants.Discovery.JwksUri);
        public IEnumerable<string> ResponseTypesSupported => TryGetStringArray(OidcConstants.Discovery.ResponseTypesSupported);
        public IEnumerable<string> SubjectTypesSupported => TryGetStringArray(OidcConstants.Discovery.SubjectTypesSupported);
        public IEnumerable<string> ScopesSupported => TryGetStringArray(OidcConstants.Discovery.ScopesSupported);
        public IEnumerable<string> IdTokenSigningAlgValuesSupported => TryGetStringArray(OidcConstants.Discovery.IdTokenSigningAlgValuesSupported);
        public IEnumerable<string> TokenEndpointAuthenticationMethodsSupported => TryGetStringArray(OidcConstants.Discovery.TokenEndpointAuthenticationMethodsSupported);
        public IEnumerable<string> ClaimsSupported => TryGetStringArray(OidcConstants.Discovery.ClaimsSupported);
       
        
        
    
        /// <summary>
        /// Generic getters
        /// </summary> 
        public JToken TryGetValue(string name) => Json.TryGetValue(name);
        public string TryGetString(string name) => Json.TryGetString(name);
        public bool? TryGetBoolean(string name) => Json.TryGetBoolean(name);
        public IEnumerable<string> TryGetStringArray(string name) => Json.TryGetStringArray(name);


        /// <summary>
        /// Validates Discovery policy
        /// </summary>
        /// <param name="policy">policy</param>
        /// <returns>string</returns>
        private string Validate(DiscoveryPolicy policy)
        {
            if (policy.ValidateIssuerName)
            {
                if (string.IsNullOrWhiteSpace(Issuer)) return "Issuer name is missing";

                var isValid = ValidateIssuerName(Issuer.RemoveTrailingSlash(), policy.Authority.RemoveTrailingSlash());
                if (!isValid) return "Issuer name does not match authority: " + Issuer;
            }

            var error = ValidateEndpoints(Json, policy);
            if (!string.IsNullOrEmpty(error)) return error;

            return string.Empty;
        }



        /// <summary>
        /// Validates Issuer Name
        /// </summary>
        /// <param name="issuer">issuer</param>
        /// <param name="authority">authority</param>
        /// <returns>bool</returns>
        public bool ValidateIssuerName(string issuer, string authority)
        {
            return string.Equals(issuer, authority, StringComparison.Ordinal);
        }

        /// <summary>
        /// Validates Endpoints
        /// </summary>
        /// <param name="json">json</param>
        /// <param name="policy">policy</param>
        /// <returns>bool</returns>
        public string ValidateEndpoints(JObject json, DiscoveryPolicy policy)
        {
            //var authorityHost = new Uri(policy.Authority).Authority;

            foreach (var element in json)
            {
                if (element.Key.EndsWith("Endpoint", StringComparison.OrdinalIgnoreCase) ||
                    element.Key.Equals(OidcConstants.Discovery.JwksUri, StringComparison.OrdinalIgnoreCase))
                {
                    var endpoint = element.Value.ToString();
                    Uri uri;

                    var isValidUri = Uri.TryCreate(endpoint, UriKind.Absolute, out uri);
                    if (!isValidUri)//Uri not valid
                    {
                        return $"Malformed endpoint: {endpoint}";
                    }

                    if (!DiscoveryUrlHelper.IsValidScheme(uri))//Scheme not valid
                    {
                        return $"Malformed endpoint: {endpoint}";
                    }

                    if (!DiscoveryUrlHelper.IsSecureScheme(uri, policy))//Scheme not secure
                    {
                        return $"Endpoint does not use HTTPS: {endpoint}";
                    }

                    
                }
            }


            return string.Empty;
        }
    }
}