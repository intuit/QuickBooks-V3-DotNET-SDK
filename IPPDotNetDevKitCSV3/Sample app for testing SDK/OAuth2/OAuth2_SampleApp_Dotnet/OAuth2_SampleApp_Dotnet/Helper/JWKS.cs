using Newtonsoft.Json;
using System.Collections.Generic;

namespace OAuth2_SampleApp_Dotnet
{
    public class JWKS
    {

        [JsonProperty("keys")]
        public List<Key> Keys { get; set; }

    }

    public class Key
    {
        [JsonProperty("kty")]
        public string Kty { get; set; }

        //exponent value
        [JsonProperty("e")]
        public string E { get; set; }


        [JsonProperty("use")]
        public string Use { get; set; }


        [JsonProperty("kid")]
        public string Kid { get; set; }

        [JsonProperty("alg")]
        public string Alg { get; set; }

        //modulus value
        [JsonProperty("n")]
        public string N { get; set; }
    }

}