// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

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
            catch (Exception ex)
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
        public TokenResponse(Exception exception)
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

        public string Raw { get; }
        public JObject Json { get; }
        public Exception Exception { get; set; }

        public bool IsError { get; }
        public ResponseErrorType ErrorType { get; } = ResponseErrorType.None;
        public HttpStatusCode HttpStatusCode { get; }
        public string HttpErrorReason { get; }

        public string AccessToken => TryGet(OidcConstants.TokenResponse.AccessToken);
        public string IdentityToken => TryGet(OidcConstants.TokenResponse.IdentityToken);
        public string TokenType => TryGet(OidcConstants.TokenResponse.TokenType);
        public string RefreshToken => TryGet(OidcConstants.TokenResponse.RefreshToken);
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