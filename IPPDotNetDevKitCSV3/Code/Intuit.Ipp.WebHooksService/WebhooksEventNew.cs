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
    public class WebhooksEventNew
    {
        /// <summary>
        /// Gets list of EventNotifications
        /// </summary>
        [JsonProperty("specversion")]
        public string SpecVersion { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("datacontenttype")]
        public string DataContentType { get; set; }

        [JsonProperty("time")]
        public DateTime Time { get; set; }

        [JsonProperty("intuitentityid")]
        public string IntuitEntityId { get; set; }

        [JsonProperty("intuitaccountid")]
        public string IntuitAccountId { get; set; }

        [JsonProperty("data")]
        public Dictionary<string, object> Data { get; set; }
    }
}
