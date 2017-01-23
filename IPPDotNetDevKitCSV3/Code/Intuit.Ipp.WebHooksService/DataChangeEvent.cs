////*********************************************************
// <copyright file="ReportService.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains DataChangeEvent Class which deserliaizes Webhooks Events</summary>
////*********************************************************


namespace Intuit.Ipp.WebhooksService
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// DataChangeEvent class for WebhooksService
    /// </summary>
    public class DataChangeEvent
    {
        /// <summary>
        /// Get List of Entities from Webhooks Response 
        /// </summary>
        [JsonProperty("entities")]
        public List<Entity> Entities { get; set; }
    }
}
