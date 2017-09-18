// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// Represents a Json Web Key as defined in http://tools.ietf.org/html/rfc7517.
    /// </summary>
    [JsonObject]
    public class JsonWebKey
    {
        // kept private to hide that a List is used.
        // public member returns an IList.
        private IList<string> _certificateClauses = new List<string>();
        private IList<string> _keyops = new List<string>();

        /// <summary>
        /// Initializes an new instance of <see cref="JsonWebKey"/>.
        /// </summary>
        public JsonWebKey()
        { }

        /// <summary>
        /// Initializes an new instance of <see cref="JsonWebKey"/> from a json string.
        /// </summary>
        /// <param name="json">a string that contains JSON Web Key parameters in JSON format.</param>
        public JsonWebKey(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) throw new ArgumentNullException(nameof(json));

            var key = JsonConvert.DeserializeObject<JsonWebKey>(json);
            Copy(key);
        }

        private void Copy(JsonWebKey key)
        {
            
           
            
            this.Kty = key.Kty;
            this.E = key.E;
            this.Use = key.Use;
            this.Kid = key.Kid;
            this.Alg = key.Alg;
            this.N = key.N;

        }
            


        /// <summary>
        /// Gets or sets the 'kty' (Key Type)..
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = JsonWebKeyParameterNames.Kty, Required = Required.Default)]
        public string Kty { get; set; }

        /// <summary>
        /// Gets or sets the 'e' (RSA - Exponent)..
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = JsonWebKeyParameterNames.E, Required = Required.Default)]
        public string E { get; set; }


        /// <summary>
        /// Gets or sets the 'use' (Public Key Use)..
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = JsonWebKeyParameterNames.Use, Required = Required.Default)]
        public string Use { get; set; }


        /// <summary>
        /// Gets or sets the 'kid' (Key ID)..
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = JsonWebKeyParameterNames.Kid, Required = Required.Default)]
        public string Kid { get; set; }
        

        /// <summary>
        /// Gets or sets the 'alg' (KeyType)..
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = JsonWebKeyParameterNames.Alg, Required = Required.Default)]
        public string Alg { get; set; }


        /// <summary>
        /// Gets or sets the 'n' (RSA - Modulus)..
        /// </summary>
        /// <remarks> value is formated as: Base64urlEncoding</remarks>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = JsonWebKeyParameterNames.N, Required = Required.Default)]
        public string N { get; set; }

      

        

        /// <summary>
        /// Returns KeySize
        /// </summary>
        public int KeySize
        {
            get
            {
                if (Kty == JsonWebAlgorithmsKeyTypes.RSA)
                    return Base64Url.Decode(N).Length * 8;
                //else if (Kty == JsonWebAlgorithmsKeyTypes.EllipticCurve)
                //    return Base64Url.Decode(X).Length * 8;
                //else if (Kty == JsonWebAlgorithmsKeyTypes.Octet)
                //    return Base64Url.Decode(K).Length * 8;
                else
                    return 0;
            }
        }

    }
}