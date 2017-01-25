////*********************************************************
// <copyright file="BaseUrlElement.cs" company="Intuit">
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
// <summary>This file contains base url element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System.Configuration;

    /// <summary>
    /// Base url element.
    /// </summary>
    public class BaseUrlElement : ConfigurationElement
    {
       
        /// <summary>
        /// Gets Url for QuickBooks Online Rest Service.
        /// </summary>
        [ConfigurationProperty("qbo")]
        public string Qbo
        {
            get
            {
                return this["qbo"].ToString();
            }
        }

        /// <summary>
        /// Gets Url for Platform Rest Service.
        /// </summary>
        [ConfigurationProperty("ips")]
        public string Ips
        {
            get
            {
                return this["ips"].ToString();
            }
        }

        /// <summary>
        /// Gets Url for OAuth Authentication server.
        /// </summary>
        [ConfigurationProperty("oAuthAccessTokenUrl")]
        public string OAuthAccessTokenUrl
        {
            get
            {
                return this["oAuthAccessTokenUrl"].ToString();
            }
        }

        /// <summary>
        /// Gets Url for UserName Authentication server.
        /// </summary>
        [ConfigurationProperty("userNameAuthentication")]
        public string UserNameAuthentication
        {
            get
            {
                return this["userNameAuthentication"].ToString();
            }
        }
    }
}
