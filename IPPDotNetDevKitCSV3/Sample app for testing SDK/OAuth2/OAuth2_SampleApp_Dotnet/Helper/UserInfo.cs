
using Newtonsoft.Json;

namespace OAuth2_SampleApp_Dotnet
{
    public class UserInfo
    {

        [JsonProperty("sub")]
        public string Sub { get; set; }
        

        [JsonProperty("email")]
        public string Email { get; set; }


        [JsonProperty("emailVerified")]
        public string EmailVerified { get; set; }


        [JsonProperty("givenName")]
        public string GivenName { get; set; }


        [JsonProperty("familyName")]
        public string FamilyName { get; set; }


        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }


        [JsonProperty("phoneNumberVerified")]
        public string PhoneNumberVerified { get; set; }


        [JsonProperty("address")]
        public Address Address { get; set; }


    }

    public class Address
    {

        [JsonProperty("streetAddress")]
        public string StreetAddress { get; set; }


        [JsonProperty("locality")]
        public string Locality { get; set; }


        [JsonProperty("region")]
        public string Region { get; set; }


        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }


        [JsonProperty("country")]
        public string Country { get; set; }

    }

}