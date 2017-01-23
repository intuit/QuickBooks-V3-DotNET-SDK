////*********************************************************
// <copyright file="WebhooksServiceElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Webhooks Service element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System.Configuration;

    /// <summary>
    /// WebhooksService
    /// </summary>
    public class WebhooksServiceElement : ConfigurationElement
    {
        /// <summary>
        /// Gets Webhooks Service verifierToken
        /// </summary>
        [ConfigurationProperty("verfierToken")]
        public WebhooksVerifierElement WebhooksVerifier
        {
            get
            {
                return (WebhooksVerifierElement)this["verfierToken"];
            }
        }

        
    }
}
