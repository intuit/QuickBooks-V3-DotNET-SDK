// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// Enum for Response Error
    /// </summary>
    public enum ResponseErrorType
    {
        None,
        Protocol,
        Http,
        Exception,
        PolicyViolation
    }
}