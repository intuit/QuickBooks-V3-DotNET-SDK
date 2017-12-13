
////*********************************************************
// <copyright file="MigratedTokenResponse.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2016 Intuit
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.***************************************************/

namespace Intuit.Ipp.Security
{

    using Newtonsoft.Json;
    using System;
    using System.Net;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Helper class for deserializing the migration api response
    /// </summary>
    public class MigratedTokenResponse
    {
        /// <summary>
        /// Handles success raw response from Token api call
        /// </summary>
        /// <param name="raw">raw</param>
        public MigratedTokenResponse(string raw)
        {
            Raw = raw;

            try
            {
                Json = JObject.Parse(raw);
            }
            catch (Exception ex)
            {
                IsError = true;
                ErrorType = MigrationResponseErrorType.Exception;
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
                ErrorType = MigrationResponseErrorType.Protocol;
            }
        }

        /// <summary>
        /// Handles exception response from Token api call
        /// </summary>
        /// <param name="exception">exception</param>
        public MigratedTokenResponse(Exception exception)
        {
            IsError = true;
            HttpStatusCode = ((HttpWebResponse)((WebException)exception).Response).StatusCode;
            Exception = exception;
            ErrorType = MigrationResponseErrorType.Exception;
        }


        /// <summary>
        /// Handles exception response from Token api call
        /// </summary>
        /// <param name="statusCode">statusCode</param>
        /// <param name="reason">reason</param> 
        public MigratedTokenResponse(HttpStatusCode statusCode, string reason)
        {
            IsError = true;

            ErrorType = MigrationResponseErrorType.Http;
            HttpStatusCode = statusCode;
            HttpErrorReason = reason;
        }

        /// <summary>
        /// Raw json response from Migration api call 
        /// </summary>
        public string Raw { get; }

        /// <summary>
        /// Json object
        /// </summary>
        public JObject Json { get; }

        /// <summary>
        /// Exception
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Chekc if there is an error response
        /// </summary>
        public bool IsError { get; }

        /// <summary>
        /// ErrorType
        /// </summary>
        public MigrationResponseErrorType ErrorType { get; } = MigrationResponseErrorType.None;

        /// <summary>
        /// HttpStatusCode
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; }

        /// <summary>
        /// HttpErrorReason
        /// </summary>
        public string HttpErrorReason { get; }


        /// <summary>
        /// Returns AccessToken
        /// </summary>
        public string AccessToken => TryGet("access_token");

        /// <summary>
        /// Returns IdToken
        /// </summary>
        public string IdentityToken => TryGet("id_token");

        /// <summary>
        /// Returns TokenType
        /// </summary>
        public string TokenType => TryGet("token_type");

        /// <summary>
        /// Returns RefreshToken
        /// </summary>
        public string RefreshToken => TryGet("refresh_token");

        /// <summary>
        /// Returns RealmId
        /// </summary>
        public string RealmId => TryGet("realmId");

        /// <summary>
        /// Returns ErrorDescription
        /// </summary>
        public string ErrorDescription => TryGet("error_description");


        /// <summary>
        /// Returns Access Token expiry value
        /// </summary>
        public long AccessTokenExpiresIn
        {
            get
            {
                var value = TryGet("expires_in");

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
                var value = TryGet("x_refresh_token_expires_in");

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
                if (ErrorType == MigrationResponseErrorType.Http)
                {
                    return HttpErrorReason;
                }
                else if (ErrorType == MigrationResponseErrorType.Exception)
                {
                    return Exception.Message;
                }

                return TryGet("error");
            }
        }

        /// <summary>
        /// Helper to get Name
        /// </summary>
        public string TryGet(string name) => Json.TryGetString(name);


    }

    /// <summary>
    /// Json Object extension
    /// </summary>
    public static class MigrationJObjectExtensions
    {
        

        /// <summary>
        /// Helper for Json object 
        /// </summary>
        /// <param name="json">json</param>
        /// <param name="name">name</param>
        /// <returns>string</returns>
        public static string TryGetString(this JObject json, string name)
        {
            JToken value = json.TryGetValue(name);
            return value?.ToString() ?? null;
        }

        /// <summary>
        /// Helper for Json object
        /// </summary>
        /// <param name="json">json</param>
        /// <param name="name">name</param>
        /// <returns>JToken</returns>
        public static JToken TryGetValue(this JObject json, string name)
        {
            JToken value;
            if (json != null && json.TryGetValue(name, StringComparison.OrdinalIgnoreCase, out value))
            {
                return value;
            }

            return null;
        }
    }

    /// <summary>
    /// Enum for Response Error
    /// </summary>
    public enum MigrationResponseErrorType
    {
        None,
        Protocol,
        Http,
        Exception,
        PolicyViolation
    }
}
