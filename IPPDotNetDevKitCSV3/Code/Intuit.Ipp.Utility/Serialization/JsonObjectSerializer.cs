////*********************************************************
// <copyright file="JsonObjectSerializer.cs" company="Intuit">
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
// <summary>This file contains JSON serialization implementation.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Reflection;
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Exception;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Linq;

    /// <summary>
    /// JSON Serialize(r) to serialize and de serialize.
    /// </summary>
    public class JsonObjectSerializer : IEntitySerializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonObjectSerializer"/> class.
        /// </summary>
        public JsonObjectSerializer()
        {
            this.IDSLogger = new TraceLogger();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonObjectSerializer"/> class.
        /// </summary>
        /// <param name="idsLogger">The ids logger.</param>
        public JsonObjectSerializer(ILogger idsLogger)
        {
            this.IDSLogger = idsLogger;
        }

        /// <summary>
        /// Gets or sets IDS Logger.
        /// </summary>
        internal ILogger IDSLogger { get; set; }

        /// <summary>
        /// Serializes the specified entity in Json Format.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// Returns the serialize entity in string format.
        /// </returns>
        public string Serialize(object entity)
        {
            string data = string.Empty;
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new ObjectToEnumConverter());
            settings.Converters.Add(new IntuitConverter());
        
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.MissingMemberHandling = MissingMemberHandling.Error;
            settings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            settings.DateFormatString = "yyyy-MM-ddTHH:mm:ssK";
            settings.MaxDepth = 256;
            
            try
            {
                data = JsonConvert.SerializeObject(entity, settings);
            }
            catch (Exception ex)
            {
                SerializationException serializationException = new SerializationException(ex.Message, ex);
                this.IDSLogger.Log(Diagnostics.TraceLevel.Error, serializationException.ToString());
                IdsExceptionManager.HandleException(serializationException);
            }
            data = data.Replace("T00:00:00Z", "");
            data = data.Replace("T00:00:00", "");
            return data;
        }

        /// <summary>
        /// DeSerializes the specified action entity type in Json Format.
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
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Converters.Add(new ObjectToEnumConverter());
            settings.Converters.Add(new IntuitConverter());
           
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;

            try
            {
                // de serialization of message.
              /*  JObject o = JObject.Parse(message);
                string key = o.Properties().Select(p => p.Name).Single();
                string entityString = o[key].ToString();*/
                deserializedObject = JsonConvert.DeserializeObject<T>(message, settings);
            }
            catch (SystemException ex)
            {
                SerializationException serializationException = new SerializationException(ex.Message, ex);
                this.IDSLogger.Log(Diagnostics.TraceLevel.Error, serializationException.ToString());
                IdsExceptionManager.HandleException(serializationException);
            }

            return deserializedObject;
        }
    }
}
