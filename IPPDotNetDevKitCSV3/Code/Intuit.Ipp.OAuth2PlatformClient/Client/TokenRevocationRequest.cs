// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// TokenRevocationRequest Class
    /// </summary>
    public class TokenRevocationRequest
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
       
        /// <summary>
        /// ClientId
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// ClientSecret
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public TokenRevocationRequest()
        {
            //RefreshToken or Bearer Access Token 
            Token = ""; 
            ClientId = "";
            ClientSecret = "";
        }
    }
}