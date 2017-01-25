////*********************************************************
// <copyright file="XmlObjectSerializer.cs" company="Intuit">
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
// <summary>This file contains Xml serializer implementation.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Exception;

    /// <summary>
    /// Xml Serialize(r) to serialize and de serialize.
    /// </summary>
    public class XmlObjectSerializer : IEntitySerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="XmlObjectSerializer"/> class.
        /// </summary>
        public XmlObjectSerializer()
        {
            this.IDSLogger = new TraceLogger(); 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlObjectSerializer"/> class.
        /// </summary>
        /// <param name="idsLogger">The ids logger.</param>
        public XmlObjectSerializer(ILogger idsLogger)
        {
            this.IDSLogger = idsLogger;
        }

        /// <summary>
        /// Gets or sets IDS Logger.
        /// </summary>
        internal ILogger IDSLogger { get; set; }

        /// <summary>
        /// Serializes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Returns the serialize entity in string format.
        /// </returns>
        public string Serialize(object entity)
        {
            string data = string.Empty;

            try
            {
                UTF8Encoding encoder = new UTF8Encoding();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(entity.GetType());
                    xmlSerializer.Serialize(memoryStream, entity);
                    data = encoder.GetString(memoryStream.ToArray());
                }
            }
            catch (SystemException ex)
            {
                SerializationException serializationException = new SerializationException(ex.Message, ex);
                this.IDSLogger.Log(TraceLevel.Error, serializationException.ToString());
                IdsExceptionManager.HandleException(serializationException);
            }
            data = data.Replace("T00:00:00Z", "");
            data = data.Replace("T00:00:00", "");
            return data;
        }

        /// <summary>
        /// DeSerializes the specified action entity type.
        /// </summary>
        /// <typeparam name="T">The type to be  serialize to</typeparam>
        /// <param name="message">The message.</param>
        /// <returns>
        /// Returns the de serialized object.
        /// </returns>
        public object Deserialize<T>(string message)
        {
            object deserializedObject = null;

            // Initialize serialize for object
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            try
            {
                using (TextReader reader = new StringReader(message))
                {
                    // de serialization of message.
                    deserializedObject = serializer.Deserialize(reader);
                }
            }
            catch (SystemException ex)
            {
                SerializationException serializationException = new SerializationException(ex.Message, ex);
                this.IDSLogger.Log(TraceLevel.Error, serializationException.ToString());

                IdsExceptionManager.HandleException(serializationException);
            }

            return deserializedObject;
        }
    }
}
