// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    public static class DiscoveryUrlHelper
    {
        /// <summary>
        /// Validate url scheme
        /// </summary>
        /// <param name="url"></param>
        /// <returns>boolean value</returns>
        public static bool IsValidScheme(Uri url)
        {
            if (string.Equals(url.Scheme, "https", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Validate if url scheme is https or not
        /// </summary>
        /// <param name="url"></param>
        /// <param name="policy"></param>
        /// <returns>boolean value</returns>
        public static bool IsSecureScheme(Uri url, DiscoveryPolicy policy)
        {
            if (policy.RequireHttps == true)
            {
          
                return string.Equals(url.Scheme, "https", StringComparison.OrdinalIgnoreCase);
            }

            return true;
        }
    }
}