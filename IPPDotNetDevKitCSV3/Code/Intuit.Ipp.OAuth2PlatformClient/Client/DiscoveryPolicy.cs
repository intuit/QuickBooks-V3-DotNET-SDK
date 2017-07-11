// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Configuration;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    public class DiscoveryPolicy
    {

        internal string Authority = ConfigurationManager.AppSettings["DiscoveryAuthority"];       


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