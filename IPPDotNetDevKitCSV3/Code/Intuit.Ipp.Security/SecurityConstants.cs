////*********************************************************
// <copyright file="SecurityConstants.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Constants for security assembly.</summary>
////*********************************************************

namespace Intuit.Ipp.Security
{
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;

    /// <summary>
    /// Constants for security assembly.
    /// </summary>
    public static class SecurityConstants
    {
        

        /// <summary>
        /// Semi Colon(;) string value.
        /// </summary>
        public const string SEMICOLONSTRING = "; ";

        /// <summary>
        /// Equals(=) string value;
        /// </summary>
        public const string EQUALSSTRINGVALUE = "=";

        /// <summary>
        /// Request Uri for User name authentication model.
        /// </summary>
        public static readonly string USERNAMEAUTHREQUESTURI;

        /// <summary>
        /// Request token Uri for OAuth authentication model.
        /// </summary>
        public static readonly string OAUTHREQUESTTOKENURI;

        /// <summary>
        /// Authorize Request Uri for OAuth authentication model.
        /// </summary>
        public static readonly string OAUTHAUTHORIZEREQUESTURL;

        /// <summary>
        /// Access token Uri for OAuth authentication model.
        /// </summary>
        public static readonly string OAUTHACCESSTOKENURL;

        /// <summary>
        /// Initializes static members of the <see cref="SecurityConstants" /> class.
        /// </summary>
        static SecurityConstants()
        {
            var securityAssembly = Assembly.GetExecutingAssembly();
            using (var stream = securityAssembly.GetManifestResourceStream("Intuit.Ipp.Security.SecurityConstants.xml"))
            {
                var securityConstantsXDocument = XDocument.Load(stream);
                var uriElement = securityConstantsXDocument.Descendants("Uri").Single();
                USERNAMEAUTHREQUESTURI = uriElement.Element("UsernameAuthRequestUri").Value;
                OAUTHREQUESTTOKENURI = uriElement.Element("OauthRequestTokenUri").Value;
                OAUTHAUTHORIZEREQUESTURL = uriElement.Element("OauthauthorizeRequestUrl").Value;
                OAUTHACCESSTOKENURL = uriElement.Element("OauthAccessTokenUrl").Value;
            }
        }
    }
}
