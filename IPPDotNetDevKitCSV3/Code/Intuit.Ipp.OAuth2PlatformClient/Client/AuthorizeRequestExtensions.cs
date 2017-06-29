// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// Extension class for AuthorizeRequest
    /// </summary>
    public static class AuthorizeRequestExtensions
    {
        /// <summary>
        /// Create Authorize request
        /// </summary>
        /// <param name="request"></param>
        /// <param name="values"></param>
        /// <returns>string</returns>
        public static string Create(this AuthorizeRequest request, object values)
        {
            return request.Create(ObjectToDictionary(values));
        }

        /// <summary>
        /// Create Authorize Url
        /// </summary>
        /// <param name="request"></param>
        /// <param name="clientId"></param>
        /// <param name="responseType"></param>
        /// <param name="scope"></param>
        /// <param name="redirectUri"></param>
        /// <param name="state"></param>
        /// <param name="extra"></param>
        /// <returns></returns>
        public static string CreateAuthorizeUrl(this AuthorizeRequest request,
            string clientId,
            string responseType,
            string scope = null,
            string redirectUri = null,
            string state = null,        
            object extra = null
            )
           {
            var values = new Dictionary<string, string>
            {
                { OidcConstants.AuthorizeRequest.ClientId, clientId },
                { OidcConstants.AuthorizeRequest.ResponseType, responseType }
            };

            if (!string.IsNullOrWhiteSpace(scope))
            {
                values.Add(OidcConstants.AuthorizeRequest.Scope, scope);
            }

            if (!string.IsNullOrWhiteSpace(redirectUri))
            {
                values.Add(OidcConstants.AuthorizeRequest.RedirectUri, redirectUri);
            }

            if (!string.IsNullOrWhiteSpace(state))
            {
                values.Add(OidcConstants.AuthorizeRequest.State, state);
            }

            

            return request.Create(Merge(values, ObjectToDictionary(extra)));
        }


        /// <summary>
        /// Helper class to map values to Dictionary
        /// </summary>
        /// <param name="values"></param>
        /// <returns>Dictionary<string, string></returns>
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


        /// <summary>
        /// Helper class to map values to Dictionary
        /// </summary>
        /// <param name="explicitValues"></param>
        /// <param name="additionalValues"></param>
        /// <returns>Dictionary<string, string></returns>
        private static Dictionary<string, string> Merge(Dictionary<string, string> explicitValues, Dictionary<string, string> additionalValues = null)
        {
            var merged = explicitValues;

            if (additionalValues != null)
            {
                merged =
                    explicitValues.Concat(additionalValues.Where(add => !explicitValues.ContainsKey(add.Key)))
                                         .ToDictionary(final => final.Key, final => final.Value);
            }

            return merged;
        }
    }
}