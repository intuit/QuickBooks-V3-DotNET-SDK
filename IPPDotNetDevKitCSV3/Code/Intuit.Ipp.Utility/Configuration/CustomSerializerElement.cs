////*********************************************************
// <copyright file="CustomSerializerElement.cs" company="Intuit">
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
// <summary>This file contains Custom Serializer element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System.Configuration;

    /// <summary>
    /// Custom serializer element.
    /// </summary>
    public class CustomSerializerElement : ConfigurationElement
    {
        /// <summary>
        /// Gets Name for the custom serialization mechanism.
        /// </summary>
        [ConfigurationProperty("name")]
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }
        }

        /// <summary>
        /// Gets Type for the custom serialization mechanism.
        /// </summary>
        [ConfigurationProperty("type")]
        public string Type
        {
            get
            {
                return this["type"].ToString();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this serialization mechanism is to be used.
        /// </summary>
        [ConfigurationProperty("enable")]
        public bool Enable
        {
            get
            {
                return (bool)this["enable"];
            }
        }
    }
}
