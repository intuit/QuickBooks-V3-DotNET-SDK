// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// Modified for Intuit's Oauth2 implementation
using System;

namespace Intuit.Ipp.OAuth2PlatformClient.Helpers
{
    /// <summary>
    /// This attribute is used to represent a string value
    /// for a value in an enum.
    /// </summary>
    public class StringValueAttribute : Attribute
    {
        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }
    }

    /// <summary>
    /// AppEnvironment enum
    /// </summary>
    public enum AppEnvironment : int
    {
        [StringValue("sandbox")]
        Sandbox = 1,
        [StringValue("production")]
        Production = 2,
        [StringValue("e2esandbox")]
        E2ESandbox = 3,
        [StringValue("e2eproduction")]
        E2EProduction = 4,
        [StringValue("betasandbox")]
        BetaSandbox = 5,
        [StringValue("betaproduction")]
        BetaProduction = 6,
        [StringValue("default")]
        Default=7,
        [StringValue("custom")]
        Custom,

    }
}
