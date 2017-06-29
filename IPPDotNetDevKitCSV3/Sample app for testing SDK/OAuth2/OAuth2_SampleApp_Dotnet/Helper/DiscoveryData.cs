using Newtonsoft.Json;
using System.Collections.Generic;

namespace OAuth2_SampleApp_Dotnet
{
    public class DiscoveryData
    {

        [JsonProperty("issuer")]
        public string Issuer { get; set; }


        [JsonProperty("authorization_endpoint")]
        public string Authorization_endpoint { get; set; }


        [JsonProperty("token_endpoint")]
        public string Token_endpoint { get; set; }


        [JsonProperty("userinfo_endpoint")]
        public string Userinfo_endpoint { get; set; }


        [JsonProperty("revocation_endpoint")]
        public string Revocation_endpoint { get; set; }


        [JsonProperty("jwks_uri")]
        public string JWKS_uri { get; set; }


        [JsonProperty("response_types_supported")]
        public List<string> Response_types_supported { get; set; }


        [JsonProperty("subject_types_supported")]
        public List<string> Subject_types_supported { get; set; }


        [JsonProperty("id_token_signing_alg_values_supported")]
        public List<string> Id_token_signing_alg_values_supported { get; set; }


        [JsonProperty("scopes_supported")]
        public List<string> Scopes_supported { get; set; }


        [JsonProperty("token_endpoint_auth_methods_supported")]
        public List<string> Token_endpoint_auth_methods_supported { get; set; }


        [JsonProperty("claims_supported")]
        public List<string> Claims_supported { get; set; }
    }

   
}