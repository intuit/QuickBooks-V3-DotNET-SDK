// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// Modified for Intuit's Oauth2 implementation

using System;
using System.Collections.Generic;
using System.Net;
using IdentityModel.Internal;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// AuthorizeResponse Class to map response from Authroize call
    /// </summary>
    public class AuthorizeResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeResponse"/> class.
        /// </summary>
        /// <param name="raw">The raw response URL.</param>
        public AuthorizeResponse(string raw)
        {
            Raw = raw;
            ParseRaw();
        }

        /// <summary>
        /// Gets the raw response URL.
        /// </summary>
        /// <value>
        /// The raw.
        /// </value>
        public string Raw { get; }

        /// <summary>
        /// Gets the key/value pairs of the response.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public Dictionary<string, string> Values { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Gets the authorization code.
        /// </summary>
        /// <value>
        /// The authorization code.
        /// </value>
        public string Code => TryGet(OidcConstants.AuthorizeResponse.Code);

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State => TryGet(OidcConstants.AuthorizeResponse.State);

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error => TryGet(OidcConstants.AuthorizeResponse.Error);

        /// <summary>
        /// Gets the error description.
        /// </summary>
        /// <value>
        /// The error description.
        /// </value>
        public string ErrorDescription => TryGet(OidcConstants.AuthorizeResponse.ErrorDescription);

        /// <summary>
        /// Gets a value indicating whether the response is an error.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the response is an error; otherwise, <c>false</c>.
        /// </value>
        public bool IsError => !string.IsNullOrEmpty(Error);//Error.IsPresent();

        /// <summary>
        /// Gets the RealmId
        /// </summary>
        public string RealmId          => TryGet(OidcConstants.AuthorizeResponse.RealmId);
        
        /// <summary>
        /// Gets the Url
        /// </summary>
        public string Url              => TryGet(OidcConstants.AuthorizeResponse.Url);
        
        
     

        private void ParseRaw()
        {
            string[] fragments;

            // query string encoded
            if (Raw.Contains("?"))
            {
                fragments = Raw.Split('?');

                var additionalHashFragment = fragments[1].IndexOf('#');
                if (additionalHashFragment >= 0)
                {
                    fragments[1] = fragments[1].Substring(0, additionalHashFragment);
                }
            }
            // fragment encoded
            else if (Raw.Contains("#"))
            {
                fragments = Raw.Split('#');
            }
            // form encoded
            else
            {
                fragments = new[] { "", Raw };
            }

            var qparams = fragments[1].Split('&');

            foreach (var param in qparams)
            {
                var parts = param.Split('=');

                if (parts.Length == 2)
                {
                    Values.Add(parts[0], parts[1]);
                }
                else if (parts.Length == 3)
                {
                    Values.Add(parts[0], parts[1]+'='+parts[2]);
                }
                else
                {
                    throw new InvalidOperationException("Malformed callback URL.");
                }
            }
        }


        /// <summary>
        /// Decodes url
        /// </summary>
        /// <param name="type"></param>
        /// <returns>string</returns>
        public string TryGet(string type)
        {
            string value;
            if (Values.TryGetValue(type, out value))
            {
                return WebUtility.UrlDecode(value);
            }

            return null;
        }
    }
}