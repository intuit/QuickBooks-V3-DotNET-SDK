////*********************************************************
// <copyright file="ResponseElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Response element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Response element.
    /// </summary>
    public class ResponseElement : ConfigurationElement
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

                return SerializationFormat.Xml;
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

                return Utility.CompressionFormat.None;
            }
        }
    }
}
