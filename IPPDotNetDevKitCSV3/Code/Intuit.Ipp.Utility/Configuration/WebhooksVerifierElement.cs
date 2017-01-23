using System;
////*********************************************************
// <copyright file="VerifierTokenElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Webhooks VerifierToken element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{

    using System.Configuration;

    /// <summary>
    /// This class file contains WebhooksVerifierElement to process verification of webhooks token
    /// </summary>
    public class WebhooksVerifierElement : ConfigurationElement
    {

        /// <summary>
        /// Gets verifier token for webhooks
        /// </summary>
        [ConfigurationProperty("value")]
        public string Value
        {
            get
            {
                return this["value"].ToString();
            }
        }
    }
}
