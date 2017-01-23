////*********************************************************
// <copyright file="CompressionFormat.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains enumeration for compression format.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    /// <summary>
    /// Format used to compress data.
    /// </summary>
    public enum CompressionFormat
    {
        /// <summary>
        /// Default value used to indicate that compression is not specified in the config.
        /// </summary>
        DEFAULT,

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
