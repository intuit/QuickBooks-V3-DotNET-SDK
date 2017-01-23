////*********************************************************
// <copyright file="IppConfiguration.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Ipp Configuration.</summary>
////*********************************************************

namespace Intuit.Ipp.Core.Configuration
{
    using System.IO;
    using Intuit.Ipp.Core.Rest;
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Exception;
    using Intuit.Ipp.Retry;
    using Intuit.Ipp.Security;
    using Intuit.Ipp.Utility;

    /// <summary>
    /// Ipp configuration.
    /// </summary>
    public class IppConfiguration
    {
        /// <summary>
        /// Gets or sets the Logger mechanism.
        /// </summary>
        public Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the Security mechanism like OAuth.
        /// </summary>
        public IRequestValidator Security { get; set; }

        /// <summary>
        /// Gets or sets the Message settings like Compression, Serialization.
        /// </summary>
        public Message Message { get; set; }

        /// <summary>
        /// Gets or sets the Retry Policy used to retry service calls when Retry-able Exceptions are generated.
        /// </summary>
        public IntuitRetryPolicy RetryPolicy { get; set; }

        /// <summary>
        /// Gets or sets the Base Urls like Pre-Production url's.
        /// </summary>
        public BaseUrl BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the Webhooks Verifier token.
        /// </summary>
        public VerifierToken VerifierToken { get; set; }

        /// <summary>
        /// Gets or sets the minorVersion
        /// </summary>
        public MinorVersion MinorVersion { get; set; }
    }
}
