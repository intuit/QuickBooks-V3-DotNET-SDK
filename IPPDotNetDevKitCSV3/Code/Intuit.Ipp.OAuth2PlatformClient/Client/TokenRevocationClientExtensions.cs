﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// Modified for Intuit's Oauth2 implementation

using System.Threading;
using System.Threading.Tasks;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// Extension Class for TokenRevocationClient
    /// </summary>
    public static class TokenRevocationClientExtensions
    {
        /// <summary>
        /// RevokeAccessTokenAsync
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="token">token</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>Task of TokenRevocationResponse</returns>
        public static Task<TokenRevocationResponse> RevokeAccessTokenAsync(this TokenRevocationClient client, string token, CancellationToken cancellationToken = default(CancellationToken))
        {
            client.Logger.Log("Revoke Access token request initiated");
            return client.RevokeAsync(new TokenRevocationRequest
            {
                Token = token,
            }, cancellationToken);
        }

        /// <summary>
        /// RevokeRefreshTokenAsync
        /// </summary>
        /// <param name="client">client</param>
        /// <param name="token">token</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns>Task of TokenRevocationResponse</returns>
        public static Task<TokenRevocationResponse> RevokeRefreshTokenAsync(this TokenRevocationClient client, string token, CancellationToken cancellationToken = default(CancellationToken))
        {
            client.Logger.Log("Revoke Refresh token request initiated");
            return client.RevokeAsync(new TokenRevocationRequest
            {
                Token = token,
            }, cancellationToken);
        }
    }
}