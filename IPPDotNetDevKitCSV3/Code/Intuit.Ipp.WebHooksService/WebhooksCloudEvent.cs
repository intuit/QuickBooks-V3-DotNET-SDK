////*********************************************************
// <copyright file="ReportService.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2016 Intuit
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/
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
    public class WebhooksCloudEvent
    {
        /// <summary>
        /// Gets list of EventNotifications
        /// </summary>
        [JsonProperty("specversion")]
        public string SpecVersion { get; set; }

        /// <summary>
        /// Event Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Event source
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Event type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Data content type
        /// </summary>
        [JsonProperty("datacontenttype")]
        public string DataContentType { get; set; }

        /// <summary>
        /// Time
        /// </summary>
        [JsonProperty("time")]
        public DateTime Time { get; set; }

        /// <summary>
        /// Intuit Entity Id
        /// </summary>
        [JsonProperty("intuitentityid")]
        public string IntuitEntityId { get; set; }

        /// <summary>
        /// Intuit Account id
        /// </summary>
        [JsonProperty("intuitaccountid")]
        public string IntuitAccountId { get; set; }

        /// <summary>
        /// Event data
        /// </summary>
        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }
    }
}
