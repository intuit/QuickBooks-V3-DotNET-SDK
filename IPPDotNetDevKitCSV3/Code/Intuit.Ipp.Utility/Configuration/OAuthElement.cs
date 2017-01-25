////*********************************************************
// <copyright file="OAuthElement.cs" company="Intuit">
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
// <summary>This file contains oAuth element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System.Configuration;

    /// <summary>
    /// OAuth element.
    /// </summary>
    public class OAuthElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the Access Token.
        /// </summary>
        [ConfigurationProperty("accessToken")]
        public string AccessToken
        {
            get
            {
                return this["accessToken"].ToString();
            }
        }

        /// <summary>
        /// Gets the Access Token Secret.
        /// </summary>
        [ConfigurationProperty("accessTokenSecret")]
        public string AccessTokenSecret
        {
            get
            {
                return this["accessTokenSecret"].ToString();
            }
        }

        /// <summary>
        /// Gets the Consumer Key.
        /// </summary>
        [ConfigurationProperty("consumerKey")]
        public string ConsumerKey
        {
            get
            {
                return this["consumerKey"].ToString();
            }
        }

        /// <summary>
        /// Gets the Consumer Secret.
        /// </summary>
        [ConfigurationProperty("consumerSecret")]
        public string ConsumerSecret
        {
            get
            {
                return this["consumerSecret"].ToString();
            }
        }
    }
}
