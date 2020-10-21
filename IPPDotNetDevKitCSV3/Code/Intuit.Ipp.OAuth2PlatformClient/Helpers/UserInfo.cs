// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// Modified for Intuit's Oauth2 implementation
using Newtonsoft.Json;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// Class for deserializing the UserInfoResponse
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// sub
        /// </summary>
        [JsonProperty("sub")]
        public string Sub { get; set; }

        /// <summary>
        /// email
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// emailVerified
        /// </summary>
        [JsonProperty("emailVerified")]
        public string EmailVerified { get; set; }

        /// <summary>
        /// givenName
        /// </summary>
        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        /// <summary>
        /// familyName
        /// </summary>
        [JsonProperty("familyName")]
        public string FamilyName { get; set; }

        /// <summary>
        /// phoneNumber
        /// </summary>
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// phoneNumberVerified
        /// </summary>
        [JsonProperty("phoneNumberVerified")]
        public string PhoneNumberVerified { get; set; }

        /// <summary>
        /// address
        /// </summary>
        [JsonProperty("address")]
        public Address Address { get; set; }
    }


    /// <summary>
    /// Class for deserializing the Address returned
    /// </summary>
    public class Address
    {
        /// <summary>
        /// streetAddress
        /// </summary>
        [JsonProperty("streetAddress")]
        public string StreetAddress { get; set; }

        /// <summary>
        /// locality
        /// </summary>
        [JsonProperty("locality")]
        public string Locality { get; set; }

        /// <summary>
        /// region
        /// </summary>
        [JsonProperty("region")]
        public string Region { get; set; }

        /// <summary>
        /// postalCode
        /// </summary>
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// country
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

    }
}