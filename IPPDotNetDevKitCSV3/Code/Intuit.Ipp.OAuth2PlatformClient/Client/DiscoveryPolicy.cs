// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// Modified for Intuit's Oauth2 implementation

using System.Collections.Generic;
using System.Configuration;
using Intuit.Ipp.OAuth2PlatformClient.Helpers;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// Validates Discovery doc values
    /// </summary>
    public class DiscoveryPolicy
    {
        internal string Authority = OidcConstants.Discovery.IssuerUrl;

        /// <summary>
        /// Constructor
        /// </summary>
        public DiscoveryPolicy()
        {

        }



        /// <summary>
        /// Sets the discovery authority if not present in application configuration
        /// </summary>        
        public void SetAuthority(string authority = OidcConstants.Discovery.IssuerUrl)
        {
            Authority = authority;
        }

        /// <summary>
        /// Sets the discovery authority if not present in application configuration
        /// </summary>        
        public void SetAuthority(AppEnvironment appEnvironment)
        {
            string authority = "";
            if (appEnvironment == AppEnvironment.Production|| appEnvironment == AppEnvironment.Sandbox)
            {
                authority = OidcConstants.Discovery.IssuerUrl;
            }
            //else if (appEnvironment == AppEnvironment.E2EProduction)
            //{
            //    authority = OidcConstants.Discovery.IssuerUrlE2E;
            //}
            //else if (appEnvironment == AppEnvironment.E2ESandbox)
            //{
            //    authority = OidcConstants.Discovery.IssuerUrlE2E;
            //}
            else
            {
                authority = "";//ignore authority validation for all other env
            }
            Authority = authority;
        }

        /// <summary>
        /// Specifies if HTTPS is enforced on all endpoints. Defaults to true.
        /// </summary>
        public bool RequireHttps { get; set; } = true;

        /// <summary>
        /// Specifies if the issuer name is checked to be identical to the authority. Defaults to true.
        /// </summary>
        public bool ValidateIssuerName { get; set; } = true;

    }
}
