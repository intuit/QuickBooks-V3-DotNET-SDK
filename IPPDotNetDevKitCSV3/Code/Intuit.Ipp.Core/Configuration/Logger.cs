////*********************************************************
// <copyright file="Logger.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Logger.</summary>
////*********************************************************

namespace Intuit.Ipp.Core.Configuration
{
    using Intuit.Ipp.Diagnostics;

    /// <summary>
    /// Contains properties used to set the Logging mechanism.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Gets or sets the Request logging mechanism.
        /// </summary>
        public RequestLog RequestLog { get; set; }

        /// <summary>
        /// Gets or sets the Custom logger implementation class.
        /// </summary>
        public ILogger CustomLogger { get; set; }
    }
}
