////*********************************************************
// <copyright file="WebhooksServiceElement.cs" company="Intuit">
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
