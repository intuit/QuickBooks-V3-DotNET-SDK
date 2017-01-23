

////*********************************************************
// <copyright file="ReportService.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains WebhooksEvent Class which deserliaizes Webhooks Events</summary>
////*********************************************************
namespace Intuit.Ipp.WebhooksService
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// WebhooksEvent class for WebhooksService
    /// </summary>
    public class WebhooksEvent
    {
        /// <summary>
        /// Gets list of EventNotifications
        /// </summary>
        [JsonProperty("eventNotifications")]
        public List<EventNotification> EventNotifications { get; set; }
    }
}
