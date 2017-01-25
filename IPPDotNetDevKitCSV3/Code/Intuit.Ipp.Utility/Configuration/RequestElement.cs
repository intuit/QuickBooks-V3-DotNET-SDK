////*********************************************************
// <copyright file="RequestElement.cs" company="Intuit">
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
// <summary>This file contains Request element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Request element.
    /// </summary>
    public class RequestElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the Serialization Format.
        /// </summary>
        [ConfigurationProperty("serializationFormat")]
        public SerializationFormat SerializationFormat
        {
            get
            {
                SerializationFormat serializationFormat;
                if (Enum.TryParse(this["serializationFormat"].ToString(), true, out serializationFormat))
                {
                    return serializationFormat;
                }

                return Utility.SerializationFormat.Xml;
            }
        }

        /// <summary>
        /// Gets the Compression Format.
        /// </summary>
        [ConfigurationProperty("compressionFormat")]
        public CompressionFormat CompressionFormat
        {
            get
            {
                CompressionFormat compressionFormat;
                if (Enum.TryParse(this["compressionFormat"].ToString(), true, out compressionFormat))
                {
                    return compressionFormat;
                }

                return CompressionFormat.None;
            }
        }
    }
}
