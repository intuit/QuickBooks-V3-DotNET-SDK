
////***************************************************************************************************
// <copyright file="IWebhooksService.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains interface for WebHooks service.</summary>
////***************************************************************************************************


namespace Intuit.Ipp.WebhooksService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This interface for WebhooksService
    /// </summary>
    public interface IWebhooksService
    {
        /// <summary>
        /// VerifyPayload func to verify token for Webhooks response
        /// </summary>
        bool VerifyPayload(string intuitHeader, string payload);

        /// <summary>
        /// GetWebhooksEvents fucn to deserialize json response from Webhooks.
        /// </summary>
        WebhooksEvent GetWebooksEvents(string payload);

    }
}
