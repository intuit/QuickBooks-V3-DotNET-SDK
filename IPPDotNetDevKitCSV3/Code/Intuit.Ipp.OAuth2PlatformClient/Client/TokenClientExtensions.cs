// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// TokenClientExtensions class
    /// </summary>
    public static class TokenClientExtensions
    {

        /// <summary>
        /// RequestTokenFromCodeAsync call
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="code">code</param>
        /// <param name="redirectUri">redirectUri</param>
        /// <param name="codeVerifier">codeVerifier</param>
        /// <param name="extra">extra</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>task of TokenResponse</returns>
        public static Task<TokenResponse> RequestTokenFromCodeAsync(this TokenClient client, string code, string redirectUri, string codeVerifier = null, object extra = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var fields = new Dictionary<string, string>
            {
                { OidcConstants.TokenRequest.GrantType, OidcConstants.GrantTypes.AuthorizationCode },
                { OidcConstants.TokenRequest.Code, code },
                { OidcConstants.TokenRequest.RedirectUri, redirectUri }
            };

      

            return client.RequestAsync(Merge(client, fields, extra), cancellationToken);
        }


        /// <summary>
        /// RequestRefreshTokenAsync call
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="refreshToken">refreshToken</param>
        /// <param name="extra">extra</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>task of TokenResponse</returns>
        public static Task<TokenResponse> RequestRefreshTokenAsync(this TokenClient client, string refreshToken, object extra = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var fields = new Dictionary<string, string>
            {
                { OidcConstants.TokenRequest.GrantType, OidcConstants.GrantTypes.RefreshToken },
                { OidcConstants.TokenRequest.RefreshToken, refreshToken }
            };

            return client.RequestAsync(Merge(client, fields, extra), cancellationToken);
        }


        /// <summary>
        /// Merge call
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="explicitValues">explicitValues</param>
        /// <param name="extra">extra</param>
        /// <returns>Dictionary</returns>
        private static Dictionary<string, string> Merge(TokenClient client, Dictionary<string, string> explicitValues, object extra = null)
        {
            var merged = explicitValues;

           

            var additionalValues = ObjectToDictionary(extra);

            if (additionalValues != null)
            {
                merged =
                    explicitValues.Concat(additionalValues.Where(add => !explicitValues.ContainsKey(add.Key)))
                                         .ToDictionary(final => final.Key, final => final.Value);
            }

            return merged;
        }


        /// <summary>
        /// ObjectToDictionary call
        /// </summary>
        /// <param name="values">values</param>
        /// <returns>Dictionary</returns>
        private static Dictionary<string, string> ObjectToDictionary(object values)
        {
            if (values == null)
            {
                return null;
            }

            var dictionary = values as Dictionary<string, string>;
            if (dictionary != null) return dictionary;

            dictionary = new Dictionary<string, string>();

            foreach (var prop in values.GetType().GetRuntimeProperties())
            {
                var value = prop.GetValue(values) as string;
                if (!string.IsNullOrEmpty(value))
                {
                    dictionary.Add(prop.Name, value);
                }
            }

            return dictionary;
        }
    }
}