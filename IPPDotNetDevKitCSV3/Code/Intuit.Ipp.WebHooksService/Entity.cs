////*********************************************************
// <copyright file="ReportService.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Entity Class which deserliaizes Webhooks Events</summary>
////*********************************************************

namespace Intuit.Ipp.WebhooksService
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Entity class for WebhooksService
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// Name member
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Id member
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Operation member
        /// </summary>
        [JsonProperty("operation")]
        public string Operation { get; set; }

        /// <summary>
        /// LastUpdated member
        /// </summary>
        [JsonProperty("lastUpdated")]
        public string LastUpdated { get; set; }
    }
}
