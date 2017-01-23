////*********************************************************
// <copyright file="CompressionFormat.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains enumeration for compression format.</summary>
////*********************************************************

namespace Intuit.Ipp.Core.Configuration
{
    /// <summary>
    /// Format used to compress data.
    /// </summary>
    public enum CompressionFormat
    {
        /// <summary>
        /// No compression.
        /// </summary>
        None,

        /// <summary>
        /// GZip compression.
        /// </summary>
        GZip,

        /// <summary>
        /// Deflate compression.
        /// </summary>
        Deflate
    }
}
