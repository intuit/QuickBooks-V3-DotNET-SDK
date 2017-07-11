// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// Helper class for Identity Token Header
    /// </summary>
    public class IdTokenHeader
    {
        [JsonProperty("kid")]
        public string Kid { get; set; }


        [JsonProperty("alg")]
        public string Alg { get; set; }
    }

    /// <summary>
    /// Helper class for Identity Token Claims
    /// </summary>
    public class IdTokenJWTClaimTypes
    {
        [JsonProperty("sub")]
        public string Sub { get; set; }


        [JsonProperty("aud")]
        public List<string> Aud { get; set; }


        [JsonProperty("realmId")]
        public string RealmId { get; set; }


        [JsonProperty("auth_time")]
        public string Auth_time { get; set; }


        [JsonProperty("iss")]
        public string Iss { get; set; }


        [JsonProperty("exp")]
        public string Exp { get; set; }


        [JsonProperty("iat")]
        public string Iat { get; set; }
    }

    
}