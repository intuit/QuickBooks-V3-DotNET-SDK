////*********************************************************
// <copyright file="VerifierToken.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Verifier Token for Webhooks Service</summary>
////***


namespace Intuit.Ipp.Core.Configuration
{
    using System.Configuration;

    /// <summary>
    /// VerifierToken for Webhooks 
    /// </summary>
    public class VerifierToken
    {
        /// <summary>
        /// Gets or sets the value of  Verifier token for Webhooks Service
        /// </summary>        
        public string Value { get; set; }
    }
}
