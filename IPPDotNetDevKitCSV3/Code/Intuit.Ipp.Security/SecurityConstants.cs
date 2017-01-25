////*********************************************************
// <copyright file="SecurityConstants.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2016 Intuit
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/
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
