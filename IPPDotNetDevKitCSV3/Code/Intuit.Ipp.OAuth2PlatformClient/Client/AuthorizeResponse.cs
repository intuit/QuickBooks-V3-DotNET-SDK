// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// Modified for Intuit's Oauth2 implementation

using System;
using System.Collections.Generic;
using System.Net;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// AuthorizeResponse Class to map response from Authroize call
    /// </summary>
    public class AuthorizeResponse
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="raw"></param>
        public AuthorizeResponse(string raw)
        {
            Raw = raw;
            ParseRaw();
        }

        /// <summary>
        /// Raw
        /// </summary>
        public string Raw { get; }

        /// <summary>
        /// Values
        /// </summary>
        public Dictionary<string, string> Values { get; } = new Dictionary<string, string>();
        
        /// <summary>
        /// Code
        /// </summary>
        public string Code             => TryGet(OidcConstants.AuthorizeResponse.Code);

        /// <summary>
        /// RealmId
        /// </summary>
        public string RealmId          => TryGet(OidcConstants.AuthorizeResponse.RealmId);

        /// <summary>
        /// Error
        /// </summary>
        public string Error            => TryGet(OidcConstants.AuthorizeResponse.Error); 

        /// <summary>
        /// State
        /// </summary>  
        public string State            => TryGet(OidcConstants.AuthorizeResponse.State);

        /// <summary>
        /// Url
        /// </summary>
        public string Url              => TryGet(OidcConstants.AuthorizeResponse.Url);

        /// <summary>
        /// Error Description
        /// </summary>
        public string ErrorDescription => TryGet(OidcConstants.AuthorizeResponse.ErrorDescription);

        /// <summary>
        /// Is Error
        /// </summary>
        public bool IsError            => !string.IsNullOrEmpty(Error);

     
        /// <summary>
        /// Parse Raw input
        /// </summary>
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