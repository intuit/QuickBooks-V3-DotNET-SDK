////*********************************************************
// <copyright file="ReportService.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains EventNotification Class which deserliaizes Webhooks Events</summary>
////*********************************************************

namespace Intuit.Ipp.WebhooksService
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// EventNotification class for WebhooksService
    /// </summary>
    public class EventNotification
    {
        /// <summary>
        /// RealmId member
        /// </summary>
        [JsonProperty("realmId")]
        public string RealmId { get; set; }

        /// <summary>
        /// DataChangeEvent member
        /// </summary>
        [JsonProperty("dataChangeEvent")]
        public DataChangeEvent DataChangeEvent { get; set; }

    }
}
