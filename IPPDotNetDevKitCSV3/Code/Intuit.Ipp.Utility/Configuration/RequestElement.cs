////*********************************************************
// <copyright file="RequestElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
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
