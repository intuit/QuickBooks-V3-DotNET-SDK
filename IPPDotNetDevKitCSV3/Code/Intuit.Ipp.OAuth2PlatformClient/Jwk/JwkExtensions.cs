
// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

// Modified for Intuit's Oauth2 implementation


using Newtonsoft.Json;
using System.Text;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// Extension class for Json Wek Key
    /// </summary>
    public static class JsonWebKeyExtensions
    {
        //Encodes JWK
        public static string ToJwkString(this JsonWebKey key)
        {
            var json = JsonConvert.SerializeObject(key);            
            return Base64Url.Encode(Encoding.UTF8.GetBytes(json));
        }
    }
}