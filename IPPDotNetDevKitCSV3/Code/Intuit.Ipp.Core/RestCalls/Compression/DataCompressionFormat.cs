////*********************************************************
// <copyright file="DataCompressionFormat.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains enumeration for data compression format.</summary>
////*********************************************************

namespace Intuit.Ipp.Core.Compression
{
    /// <summary>
    /// Format used to compress data.
    /// </summary>
    public enum DataCompressionFormat
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
