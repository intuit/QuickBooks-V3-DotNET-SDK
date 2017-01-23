////*********************************************************
// <copyright file="ICompressor.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains interface for compressor.</summary>
////*********************************************************

namespace Intuit.Ipp.Core
{
    using System.IO;
    using Intuit.Ipp.Core.Compression;

    /// <summary>
    /// Interface for compression methods.
    /// </summary>
    public interface ICompressor
    {
        /// <summary>
        /// Gets format of the data compression.
        /// </summary>
        DataCompressionFormat DataCompressionFormat { get; }

        /// <summary>
        /// Compresses the input byte array into stream.
        /// </summary>
        /// <param name="content">Input data.</param>
        /// <param name="requestStream">Request stream.</param>
        void Compress(byte[] content, Stream requestStream);

        /// <summary>
        /// Decompresses the output response stream.
        /// </summary>
        /// <param name="responseStream">Response stream.</param>
        /// <returns>Decompressed stream.</returns>
        Stream Decompress(Stream responseStream);
    }
}
