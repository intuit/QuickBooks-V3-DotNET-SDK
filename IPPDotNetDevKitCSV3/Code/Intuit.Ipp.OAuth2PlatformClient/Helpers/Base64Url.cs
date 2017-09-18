// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// Helper class for Base64 Url conversions
    /// </summary>
    public static class Base64Url
    {
        /// <summary>
        /// Encodes byte array to Base 64 string
        /// </summary>
        /// <param name="arg">arg</param>       
        public static string Encode(byte[] arg)
        {
            string s = Convert.ToBase64String(arg); // Standard base64 encoder
            
            s = s.Split('=')[0]; // Remove any trailing '='s
            s = s.Replace('+', '-'); // 62nd char of encoding
            s = s.Replace('/', '_'); // 63rd char of encoding
            
            return s;
        }

        /// <summary>
        /// Converts from Base 64 string to byte array
        /// </summary>
        /// <param name="arg">arg</param>
        public static byte[] Decode(string arg)
        {
            string s = arg;
            s = s.Replace('-', '+'); // 62nd char of encoding
            s = s.Replace('_', '/'); // 63rd char of encoding
            
            switch (s.Length % 4) // Pad with trailing '='s
            {
                case 0: break; // No pad chars in this case
                case 2: s += "=="; break; // Two pad chars
                case 3: s += "="; break; // One pad char
                default: throw new Exception("Illegal base64url string!");
            }
            
            return Convert.FromBase64String(s); // Standard base64 decoder
        }
    }
}