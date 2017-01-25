////*********************************************************
// <copyright file="IRequestResponse.cs" company="Intuit">
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
// <summary>This file contains SdkException.</summary>
// <summary>This file contains IRequestReponse interface.</summary>
////*********************************************************

namespace Intuit.Ipp.Core.Configuration
{
    /// <summary>
    /// Contains properties common to Request and Response.
    /// </summary>
    public class IRequestResponse
    {
        /// <summary>
        /// Gets or sets the Serialization mechanism like Json, Xml.
        /// </summary>
        public SerializationFormat SerializationFormat { get; set; }

        /// <summary>
        /// Gets or sets the Compression Format like GZip, Deflate or None.
        /// </summary>
        public CompressionFormat CompressionFormat { get; set; }
    }
}
