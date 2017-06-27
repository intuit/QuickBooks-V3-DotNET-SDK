using Newtonsoft.Json;
using System.Collections.Generic;

namespace OAuth2_SampleApp_Dotnet
{
    public class IdTokenHeader
    {
        [JsonProperty("kid")]
        public string Kid { get; set; }


        [JsonProperty("alg")]
        public string Alg { get; set; }
    }

    public class IdTokenPayload
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