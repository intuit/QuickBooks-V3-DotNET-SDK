////*********************************************************
// <copyright file="IRequestResponse.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
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
