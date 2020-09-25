
////***************************************************************************************************
// <copyright file="IWebhooksService.cs" company="Intuit">
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
// <summary>This file contains interface for WebHooks service.</summary>
////***************************************************************************************************


namespace Intuit.Ipp.WebhooksService
{
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
