// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// Modified for Intuit's Oauth2 implementation

using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// TokenResponse class to map response from Token call
    /// </summary>
    public class TokenResponse
    {
        /// <summary>
        /// Handles success raw response from Token api call
        /// </summary>
        /// <param name="raw">raw</param>
        public TokenResponse(string raw)
        {
            Raw = raw;

            try
            {
                Json = JObject.Parse(raw);
            }
            catch (System.Exception ex)
            {
                IsError = true;
                ErrorType = ResponseErrorType.Exception;
                Exception = ex;

                return;
            }

            if (string.IsNullOrWhiteSpace(Error))
            {
                IsError = false;
                HttpStatusCode = HttpStatusCode.OK;
            }
            else
            {
                IsError = true;
                HttpStatusCode = HttpStatusCode.BadRequest;
                ErrorType = ResponseErrorType.Protocol;
            }
        }

        /// <summary>
        /// Handles exception response from Token api call
        /// </summary>
        /// <param name="exception">exception</param>
        public TokenResponse(System.Exception exception)
        {
            IsError = true;

            Exception = exception;
            ErrorType = ResponseErrorType.Exception;
        }


        /// <summary>
        /// Handles exception response from Token api call
        /// </summary>
        /// <param name="statusCode">statusCode</param>
        /// <param name="reason">reason</param> 
        public TokenResponse(HttpStatusCode statusCode, string reason)
        {
            IsError = true;

            ErrorType = ResponseErrorType.Http;
            HttpStatusCode = statusCode;
            HttpErrorReason = reason;
        }

        /// <summary>
        /// Raw
        /// </summary> 
        public string Raw { get; }

        /// <summary>
        /// Json
        /// </summary> 
        public JObject Json { get; }

        /// <summary>
        /// Exception
        /// </summary> 
        public System.Exception Exception { get; set; }

        /// <summary>
        /// Is error
        /// </summary> 
        public bool IsError { get; }

        /// <summary>
        /// Error Type
        /// </summary> 
        public ResponseErrorType ErrorType { get; } = ResponseErrorType.None;

        /// <summary>
        /// Http status code
        /// </summary> 
        public HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// Http error reason
        /// </summary> 
        public string HttpErrorReason { get; }

        /// <summary>
        /// Access Token
        /// </summary> 
        public string AccessToken => TryGet(OidcConstants.TokenResponse.AccessToken);

        /// <summary>
        /// Identity Token
        /// </summary> 
        public string IdentityToken => TryGet(OidcConstants.TokenResponse.IdentityToken);

        /// <summary>
        /// Token Type
        /// </summary> 
        public string TokenType => TryGet(OidcConstants.TokenResponse.TokenType);

        /// <summary>
        /// Refresh Token
        /// </summary> 
        public string RefreshToken => TryGet(OidcConstants.TokenResponse.RefreshToken);

        /// <summary>
        /// Error Description
        /// </summary> 
        public string ErrorDescription => TryGet(OidcConstants.TokenResponse.ErrorDescription);

        /// <summary>
        /// Returns Access Token expiry value
        /// </summary>
        public long AccessTokenExpiresIn
        {
            get
            {
                var value = TryGet(OidcConstants.TokenResponse.AccessTokenExpiresIn);

                if (value != null)
                {
                    long longValue;
                    if (long.TryParse(value.ToString(), out longValue))
                    {
                        return longValue;
                    }
                }

                return 0;
            }
        }


        /// <summary>
        /// Returns RefreshToken Expiry Value
        /// </summary>   
        public long RefreshTokenExpiresIn
        {
            get
            {
                var value = TryGet(OidcConstants.TokenResponse.RefreshTokenExpiresIn);

                if (value != null)
                {
                    long longValue;
                    if (long.TryParse(value.ToString(), out longValue))
                    {
                        return longValue;
                    }
                }

                return 0;
            }
        }


        /// <summary>
        /// Handles error
        /// </summary>   
        public string Error
        {
            get
            {
                if (ErrorType == ResponseErrorType.Http)
                {
                    return HttpErrorReason;
                }
                else if (ErrorType == ResponseErrorType.Exception)
                {
                    return Exception.Message;
                }

                return TryGet(OidcConstants.TokenResponse.Error);
            }
        }


        /// <summary>
        /// Helper to get Name
        /// </summary>
        public string TryGet(string name) => Json.TryGetString(name);
    }
}