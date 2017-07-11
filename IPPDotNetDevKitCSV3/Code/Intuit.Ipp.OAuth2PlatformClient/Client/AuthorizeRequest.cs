// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// Helper class fo creating Authorize url
    /// </summary>
    public class AuthorizeRequest
    {
        private readonly Uri _authorizeEndpoint;

        /// <summary>
        /// Maps authorize endpoint
        /// </summary>
        /// <param name="authorizeEndpoint"></param>
        public AuthorizeRequest(Uri authorizeEndpoint)
        {
            _authorizeEndpoint = authorizeEndpoint;
        }

        /// <summary>
        /// Maps authorize endpoint
        /// </summary>
        /// <param name="authorizeEndpoint"></param>
        public AuthorizeRequest(string authorizeEndpoint)
        {
            _authorizeEndpoint = new Uri(authorizeEndpoint);
        }

        /// <summary>
        /// Formats values to the required url format
        /// </summary>
        /// <param name="values"></param>
        public string Create(IDictionary<string, string> values)
        {
            var qs = string.Join("&", values.Select(kvp => string.Format("{0}={1}", WebUtility.UrlEncode(kvp.Key), WebUtility.UrlEncode(kvp.Value))).ToArray());

            if (_authorizeEndpoint.IsAbsoluteUri)
            {
                return string.Format("{0}?{1}", _authorizeEndpoint.AbsoluteUri, qs);
            }
            else
            {
                return string.Format("{0}?{1}", _authorizeEndpoint.OriginalString, qs);
            }
        }
    }
}