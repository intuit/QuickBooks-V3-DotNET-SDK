// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// TokenRevocationResponse to handle response from Token Revoke call
    /// </summary>
    public class TokenRevocationResponse
    {
        public string Raw { get; }
        public JObject Json { get; }
        public bool IsError { get; }
        public HttpStatusCode HttpStatusCode { get; }
        public string HttpErrorReason { get; }
        public ResponseErrorType ErrorType { get;  }
        public Exception Exception { get;  }

        /// <summary>
        /// Handles successful raw response from Token Revoke api call
        /// </summary>

        public TokenRevocationResponse()
        {
            IsError = false;
            HttpStatusCode = HttpStatusCode.OK;
        }

        /// <summary>
        /// Handles successful raw response from Token Revoke api call
        /// </summary>
        /// <param name="raw">raw</param>
        public TokenRevocationResponse(string raw)
        {
            Raw = raw;
            IsError = false;
            HttpStatusCode = HttpStatusCode.OK;

            try
            {
                Json = JObject.Parse(raw);

                if (!string.IsNullOrEmpty(Json.TryGetString(OidcConstants.TokenResponse.Error)))
                {
                    IsError = true;
                    ErrorType = ResponseErrorType.Protocol;
                    HttpStatusCode = HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorType = ResponseErrorType.Exception;
                Exception = ex;
            }
        }




        /// <summary>
        /// Handles exception response from Token Revoke api call
        /// </summary>
        /// <param name="statusCode">statusCode</param>
        /// <param name="reason">reason</param>

        public TokenRevocationResponse(HttpStatusCode statusCode, string reason)
        {
            IsError = true;

            ErrorType = ResponseErrorType.Http;
            HttpStatusCode = statusCode;
            HttpErrorReason = reason;
        }


        /// <summary>
        /// Handles exception response from UserInfo api call
        /// </summary>
        /// <param name="exception">exception</param>
        public TokenRevocationResponse(Exception exception)
        {
            IsError = true;

            Exception = exception;
            ErrorType = ResponseErrorType.Exception;
        }


        /// <summary>
        /// Handles Error
        /// </summary>

        public string Error
        {
            get
            {
                if (ErrorType == ResponseErrorType.Http)
                {
                    return HttpErrorReason;
                }
                else if(ErrorType == ResponseErrorType.Exception)
                {
                    return Exception.Message;
                }

                return Json.TryGetString(OidcConstants.TokenResponse.Error);
            }
        }
    }
}