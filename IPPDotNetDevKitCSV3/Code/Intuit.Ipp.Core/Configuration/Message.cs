////*********************************************************
// <copyright file="Message.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Message.</summary>
////*********************************************************

namespace Intuit.Ipp.Core.Configuration
{
    /// <summary>
    /// Contains properties about the Requst and Response configuration settings.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Gets or sets the Request configuration settings.
        /// </summary>
        public Request Request { get; set; }

        /// <summary>
        /// Gets or sets the Response configuration settings.
        /// </summary>
        public Response Response { get; set; }
    }
}
