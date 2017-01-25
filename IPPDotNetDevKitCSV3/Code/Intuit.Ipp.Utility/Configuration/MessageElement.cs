////*********************************************************
// <copyright file="MessageElement.cs" company="Intuit">
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
// <summary>This file contains Message element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Message element.
    /// </summary>
    public class MessageElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the Request element.
        /// </summary>
        [ConfigurationProperty("request")]
        public RequestElement Request
        {
            get
            {
                return (RequestElement)this["request"];
            }
        }

        /// <summary>
        /// Gets the Response element.
        /// </summary>
        [ConfigurationProperty("response")]
        public ResponseElement Response
        {
            get
            {
                return (ResponseElement)this["response"];
            }
        }

        /// <summary>
        /// Gets the CustomSerializer element.
        /// </summary>
        [ConfigurationProperty("customSerializer")]
        public CustomSerializerElement CustomSerializer
        {
            get
            {
                return (CustomSerializerElement)this["customSerializer"];
            }
        }
    }
}
