// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

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
        /// <param name="client"></param>
        /// <param name="token"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task<TokenRevocationResponse></returns>
        public static Task<TokenRevocationResponse> RevokeAccessTokenAsync(this TokenRevocationClient client, string token, CancellationToken cancellationToken = default(CancellationToken))
        {
            return client.RevokeAsync(new TokenRevocationRequest
            {
                Token = token,
            }, cancellationToken);
        }

        /// <summary>
        /// RevokeRefreshTokenAsync
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task<TokenRevocationResponse></returns>
        public static Task<TokenRevocationResponse> RevokeRefreshTokenAsync(this TokenRevocationClient client, string token, CancellationToken cancellationToken = default(CancellationToken))
        {
            return client.RevokeAsync(new TokenRevocationRequest
            {
                Token = token,
            }, cancellationToken);
        }
    }
}