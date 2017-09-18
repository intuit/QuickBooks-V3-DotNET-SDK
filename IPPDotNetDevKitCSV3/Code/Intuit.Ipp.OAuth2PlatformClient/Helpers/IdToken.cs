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
        /// <summary>
        /// kid
        /// </summary>
        [JsonProperty("kid")]
        public string Kid { get; set; }


        /// <summary>
        /// alg
        /// </summary>
        [JsonProperty("alg")]
        public string Alg { get; set; }
    }

    /// <summary>
    /// Helper class for Identity Token Claims
    /// </summary>
    public class IdTokenJWTClaimTypes
    {
        /// <summary>
        /// sub
        /// </summary>
        [JsonProperty("sub")]
        public string Sub { get; set; }

        /// <summary>
        /// aud
        /// </summary>
        [JsonProperty("aud")]
        public List<string> Aud { get; set; }

        /// <summary>
        /// realmId
        /// </summary>
        [JsonProperty("realmId")]
        public string RealmId { get; set; }

        /// <summary>
        /// auth_time
        /// </summary>
        [JsonProperty("auth_time")]
        public string Auth_time { get; set; }

        /// <summary>
        /// iss
        /// </summary>
        [JsonProperty("iss")]
        public string Iss { get; set; }

        /// <summary>
        /// exp
        /// </summary>
        [JsonProperty("exp")]
        public string Exp { get; set; }

        /// <summary>
        /// iat
        /// </summary>
        [JsonProperty("iat")]
        public string Iat { get; set; }
    }

    
}