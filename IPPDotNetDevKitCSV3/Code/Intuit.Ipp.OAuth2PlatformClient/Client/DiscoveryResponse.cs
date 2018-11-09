// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel.Jwk;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using IdentityModel.Internal;
using System.Linq;
using IdentityModel.Client;

#pragma warning disable 1591

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// Models the response from an OpenID Connect discovery endpoint
    /// </summary>
    public class DiscoveryResponse:IdentityModel.Client.DiscoveryResponse
    {
        /// <summary>
        /// Gets the raw response.
        /// </summary>
        /// <value>
        /// The raw.
        /// </value>
        public new string Raw { get; }

        /// <summary>
        /// Gets the response as a JObject.
        /// </summary>
        /// <value>
        /// The json.
        /// </value>
        public new JObject Json { get; }

        /// <summary>
        /// Gets a value indicating whether an error occurred.
        /// </summary>
        /// <value>
        ///   <c>true</c> if an error occurred; otherwise, <c>false</c>.
        /// </value>
        public new bool IsError { get; } = false;

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public new HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public new string Error { get; }

        /// <summary>
        /// Gets or sets the type of the error.
        /// </summary>
        /// <value>
        /// The type of the error.
        /// </value>
        public new ResponseErrorType ErrorType { get; set; } = ResponseErrorType.None;

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public new Exception Exception { get; }

        /// <summary>
        /// Gets or sets the JSON web key set.
        /// </summary>
        /// <value>
        /// The key set.
        /// </value>
        public new JsonWebKeySet KeySet { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoveryResponse"/> class.
        /// </summary>
        /// <param name="raw">The raw response.</param>
        /// <param name="policy">The security policy.</param>
        public DiscoveryResponse(string raw, DiscoveryPolicy policy = null):base(raw, null)
        {
            if (policy == null) policy = new DiscoveryPolicy();

            IsError = false;
            StatusCode = HttpStatusCode.OK;
            Raw = raw;

            try
            {
                Json = JObject.Parse(raw);
                var validationError = Validate(policy);

                if (validationError.IsPresent())
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
        /// Initializes a new instance of the <see cref="DiscoveryResponse"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="reason">The reason.</param>
        public DiscoveryResponse(HttpStatusCode statusCode, string reason):base(statusCode, reason)
        {
            IsError = true;

            ErrorType = ResponseErrorType.Http;
            StatusCode = statusCode;
            Error = reason;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoveryResponse" /> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="reason">The reason.</param>
        /// <param name="content">The content.</param>
        public DiscoveryResponse(HttpStatusCode statusCode, string reason, string content):base(statusCode, reason, content)
        {
            IsError = true;

            ErrorType = ResponseErrorType.Http;
            StatusCode = statusCode;
            Error = reason;
            Raw = content;

            try
            {
                Json = JObject.Parse(content);
            }
            catch { }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiscoveryResponse"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="errorMessage">The error message.</param>
        public DiscoveryResponse(Exception exception, string errorMessage):base(exception, errorMessage)
        {
            IsError = true;

            ErrorType = ResponseErrorType.Exception;
            Exception = exception;
            Error = $"{errorMessage}: {exception.Message}";
        }

        // strongly typed
        public new string Issuer => TryGetString(OidcConstants.Discovery.Issuer);
        public new string AuthorizeEndpoint => TryGetString(OidcConstants.Discovery.AuthorizationEndpoint);
        public new string TokenEndpoint => TryGetString(OidcConstants.Discovery.TokenEndpoint);
        public new string UserInfoEndpoint => TryGetString(OidcConstants.Discovery.UserInfoEndpoint);
        public new string IntrospectionEndpoint => TryGetString(OidcConstants.Discovery.IntrospectionEndpoint);
        public new string RevocationEndpoint => TryGetString(OidcConstants.Discovery.RevocationEndpoint);
        //public string DeviceAuthorizationEndpoint => TryGetString(OidcConstants.Discovery.DeviceAuthorizationEndpoint);

        public new string JwksUri => TryGetString(OidcConstants.Discovery.JwksUri);
        //public string EndSessionEndpoint => TryGetString(OidcConstants.Discovery.EndSessionEndpoint);
        //public string CheckSessionIframe => TryGetString(OidcConstants.Discovery.CheckSessionIframe);
        //public string RegistrationEndpoint => TryGetString(OidcConstants.Discovery.RegistrationEndpoint);
        //public bool? FrontChannelLogoutSupported => TryGetBoolean(OidcConstants.Discovery.FrontChannelLogoutSupported);
        //public bool? FrontChannelLogoutSessionSupported => TryGetBoolean(OidcConstants.Discovery.FrontChannelLogoutSessionSupported);
        //public IEnumerable<string> GrantTypesSupported => TryGetStringArray(OidcConstants.Discovery.GrantTypesSupported);
        //public IEnumerable<string> CodeChallengeMethodsSupported => TryGetStringArray(OidcConstants.Discovery.CodeChallengeMethodsSupported);
        public new IEnumerable<string> ScopesSupported => TryGetStringArray(OidcConstants.Discovery.ScopesSupported);
        public new IEnumerable<string> SubjectTypesSupported => TryGetStringArray(OidcConstants.Discovery.SubjectTypesSupported);
        //public IEnumerable<string> ResponseModesSupported => TryGetStringArray(OidcConstants.Discovery.ResponseModesSupported);
        public new IEnumerable<string> ResponseTypesSupported => TryGetStringArray(OidcConstants.Discovery.ResponseTypesSupported);
        public new IEnumerable<string> ClaimsSupported => TryGetStringArray(OidcConstants.Discovery.ClaimsSupported);
        public new IEnumerable<string> TokenEndpointAuthenticationMethodsSupported => TryGetStringArray(OidcConstants.Discovery.TokenEndpointAuthenticationMethodsSupported);

        // generic
        public new JToken TryGetValue(string name) => Json.TryGetValue(name);
        public new string TryGetString(string name) => Json.TryGetString(name);
        public new bool? TryGetBoolean(string name) => Json.TryGetBoolean(name);
        public new IEnumerable<string> TryGetStringArray(string name) => Json.TryGetStringArray(name);

        private string Validate(DiscoveryPolicy policy)
        {
            if (policy.ValidateIssuerName)
            {
                if (string.IsNullOrWhiteSpace(Issuer)) return "Issuer name is missing";

                var isValid = ValidateIssuerName(Issuer.RemoveTrailingSlash(), policy.Authority.RemoveTrailingSlash(), policy.AuthorityNameComparison);
                if (!isValid) return "Issuer name does not match authority: " + Issuer;
            }

            var error = ValidateEndpoints(Json, policy);
            if (error.IsPresent()) return error;

            return string.Empty;
        }

        /// <summary>
        /// Checks if the issuer matches the authority.
        /// </summary>
        /// <param name="issuer">The issuer.</param>
        /// <param name="authority">The authority.</param>
        /// <returns></returns>
        public new bool ValidateIssuerName(string issuer, string authority)
        {
            return ValidateIssuerName(issuer, authority, StringComparison.Ordinal);
        }

        /// <summary>
        /// Checks if the issuer matches the authority.
        /// </summary>
        /// <param name="issuer">The issuer.</param>
        /// <param name="authority">The authority.</param>
        /// <param name="nameComparison">The comparison mechanism that should be used when performing the match.</param>
        /// <returns></returns>
        public new bool ValidateIssuerName(string issuer, string authority, StringComparison nameComparison)
        {
            return string.Equals(issuer, authority, nameComparison);
        }

        /// <summary>
        /// Validates the endoints and jwks_uri according to the security policy.
        /// </summary>
        /// <param name="json">The json.</param>
        /// <param name="policy">The policy.</param>
        /// <returns></returns>
        public string ValidateEndpoints(JObject json, DiscoveryPolicy policy)
        {
            // allowed hosts
            var allowedHosts = new HashSet<string>(policy.AdditionalEndpointBaseAddresses.Select(e => new Uri(e).Authority))
            {
                new Uri(policy.Authority).Authority
            };

            // allowed authorities (hosts + base address)
            var allowedAuthorities = new HashSet<string>(policy.AdditionalEndpointBaseAddresses)
            {
                policy.Authority
            };

            foreach (var element in json)
            {
                //if (element.Key.EndsWith("endpoint", StringComparison.OrdinalIgnoreCase) ||
                //    element.Key.Equals(OidcConstants.Discovery.JwksUri, StringComparison.OrdinalIgnoreCase) ||
                //    element.Key.Equals(OidcConstants.Discovery.CheckSessionIframe, StringComparison.OrdinalIgnoreCase))
                if (element.Key.EndsWith("endpoint", StringComparison.OrdinalIgnoreCase) ||
                    element.Key.Equals(OidcConstants.Discovery.JwksUri, StringComparison.OrdinalIgnoreCase))    
                {
                    var endpoint = element.Value.ToString();

                    var isValidUri = Uri.TryCreate(endpoint, UriKind.Absolute, out Uri uri);
                    if (!isValidUri)
                    {
                        return $"Malformed endpoint: {endpoint}";
                    }

                    if (!DiscoveryEndpoint.IsValidScheme(uri))
                    {
                        return $"Malformed endpoint: {endpoint}";
                    }

                    if (!DiscoveryEndpoint.IsSecureScheme(uri, policy))
                    {
                        return $"Endpoint does not use HTTPS: {endpoint}";
                    }

                    if (policy.ValidateEndpoints)
                    {
                        // if endpoint is on exclude list, don't validate
                        if (policy.EndpointValidationExcludeList.Contains(element.Key)) continue;

                        bool isAllowed = false;
                        foreach (var host in allowedHosts)
                        {
                            if (string.Equals(host, uri.Authority))
                            {
                                isAllowed = true;
                            }
                        }

                        if (!isAllowed)
                        {
                            return $"Endpoint is on a different host than authority: {endpoint}";
                        }


                        isAllowed = false;
                        foreach (var authority in allowedAuthorities)
                        {
                            if (endpoint.StartsWith(authority, policy.AuthorityNameComparison))
                            {
                                isAllowed = true;
                            }
                        }

                        if (!isAllowed)
                        {
                            return $"Endpoint belongs to different authority: {endpoint}";
                        }
                    }
                }
            }

            if (policy.RequireKeySet)
            {
                if (string.IsNullOrWhiteSpace(JwksUri))
                {
                    return "Keyset is missing";
                }
            }

            return string.Empty;
        }
    }
}