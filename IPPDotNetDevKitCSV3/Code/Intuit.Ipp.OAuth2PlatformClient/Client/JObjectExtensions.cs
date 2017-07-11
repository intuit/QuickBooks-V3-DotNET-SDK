// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// Json Object extension
    /// </summary>
    public static class JObjectExtensions
    {


        /// <summary>
        /// Converts Json object to Claims
        /// </summary>
        /// <param name="json"></param>
        /// <returns>IEnumerable<Claim></returns>
        public static IEnumerable<Claim> ToClaims(this JObject json)
        {
            var claims = new List<Claim>();

            foreach (var x in json)
            {
                var array = x.Value as JArray;

                if (array != null)
                {
                    foreach (var item in array)
                    {
                        claims.Add(new Claim(x.Key, item.ToString()));
                    }
                }
                else
                {
                    claims.Add(new Claim(x.Key, x.Value.ToString()));
                }
            }

            return claims;
        }


        /// <summary>
        /// Helper for Json object
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
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

        /// <summary>
        /// Helper for Json object
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
        /// <returns>string</returns>
        public static string TryGetString(this JObject json, string name)
        {
            JToken value = json.TryGetValue(name);
            return value?.ToString() ?? null;
        }

        /// <summary>
        /// Helper for Json object
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
        /// <returns>bool</returns>
        public static bool? TryGetBoolean(this JObject json, string name)
        {
            var value = json.TryGetString(name);

            bool result;
            if (bool.TryParse(value, out result))
            {
                return result;
            }

            return null;
        }

        /// <summary>
        /// Helper for Json object
        /// </summary>
        /// <param name="json"></param>
        /// <param name="name"></param>
        /// <returns>IEnumerable<string></returns>
        public static IEnumerable<string> TryGetStringArray(this JObject json, string name)
        {
            var values = new List<string>();

            var array = json.TryGetValue(name) as JArray;
            if (array != null)
            {
                foreach (var item in array)
                {
                    values.Add(item.ToString());
                }
            }

            return values;
        }
    }
}