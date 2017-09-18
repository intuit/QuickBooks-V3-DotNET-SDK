// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// UserInfoResponse Class to map response from UserInfo call
    /// </summary>
    public class UserInfoResponse
    {
        public string Raw { get; }
        public JObject Json { get; }
        public IEnumerable<Claim> Claims { get; }

        public bool IsError { get; }
        public string Error { get; }

        public HttpStatusCode HttpStatusCode { get; }
        public Exception Exception { get; }
        public ResponseErrorType ErrorType { get; set; }
        
        /// <summary>
        /// Handles successful raw response from UserInfo api call
        /// </summary>
        /// <param name="raw">raw</param>
        public UserInfoResponse(string raw)
        {
            Raw = raw;
            HttpStatusCode = HttpStatusCode.OK;
            IsError = false;

            try
            {
                Json = JObject.Parse(raw);
                Claims = Json.ToClaims();
            }
            catch (Exception ex)
            {
                IsError = true;
                Error = ex.Message;
                Exception = ex;
                ErrorType = ResponseErrorType.Exception;
            }
        }

        /// <summary>
        /// Handles exception response from UserInfo api call
        /// </summary>
        /// <param name="statusCode">statusCode</param>
        /// <param name="reason">reason</param>
        public UserInfoResponse(HttpStatusCode statusCode, string httpErrorReason)
        {
            IsError = true;

            HttpStatusCode = statusCode;
            ErrorType = ResponseErrorType.Http;
            Error = httpErrorReason;
        }

        /// <summary>
        /// UserInfoResponse
        /// </summary>
        /// <param name="exception">exception</param>
        public UserInfoResponse(Exception exception)
        {
            IsError = true;

            Error = exception.Message;
            Exception = exception;
            ErrorType = ResponseErrorType.Exception;
        }
    }
}