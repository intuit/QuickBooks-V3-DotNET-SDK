////*********************************************************
// <copyright file="SecurityElement.cs" company="Intuit">
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
// <summary>This file contains Security element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Security element.
    /// </summary>
    public class SecurityElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the Security mode.
        /// </summary>
        [ConfigurationProperty("mode")]
        public SecurityMode Mode
        {
            get
            {
                SecurityMode mode;
                if (Enum.TryParse(this["mode"].ToString(), true, out mode))
                {
                    return mode;
                }

                return SecurityMode.None;
            }
        }

        /// <summary>
        /// Gets the OAuth element.
        /// </summary>
        [ConfigurationProperty("oauth")]
        public OAuthElement OAuth
        {
            get
            {
                return (OAuthElement)this["oauth"];
            }
        }



        /// <summary>
        /// Gets the custom security element.
        /// </summary>
        [ConfigurationProperty("customSecurity")]
        public CustomSecurityElement CustomSecurity
        {
            get
            {
                return (CustomSecurityElement)this["customSecurity"];
            }
        }
    }
}
