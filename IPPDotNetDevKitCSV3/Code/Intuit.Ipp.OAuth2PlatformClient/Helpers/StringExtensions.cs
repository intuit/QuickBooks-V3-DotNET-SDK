// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Diagnostics;
using System;
using System.Reflection;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// String extensions class
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Ensures trailing slash at the end of the url
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>string</returns>
        [DebuggerStepThrough]
        public static string EnsureTrailingSlash(this string url)
        {
            if (!url.EndsWith("/"))
            {
                return url + "/";
            }

            return url;
        }

        /// <summary>
        /// Removes trailing slash at the end of the url
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>string</returns>
        [DebuggerStepThrough]
        public static string RemoveTrailingSlash(this string url)
        {
            if (url != null && url.EndsWith("/"))
            {
                url = url.Substring(0, url.Length - 1);
            }

            return url;
        }



        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>string</returns>
        [DebuggerStepThrough]
        public static string GetStringValue(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get fieldinfo for this type
            FieldInfo fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            StringValueAttribute[] attribs = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].StringValue : null;
        }
    }
}