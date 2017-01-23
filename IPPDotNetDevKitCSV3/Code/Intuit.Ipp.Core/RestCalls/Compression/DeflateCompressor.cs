////*********************************************************
// <copyright file="DeflateCompressor.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains implementation for deflate compressor.</summary>
////*********************************************************

namespace Intuit.Ipp.Core.Compression
{
    using System.IO;
    using System.IO.Compression;
    using System.Net;

    /// <summary>
    /// Deflate compressor.
    /// </summary>
    public class DeflateCompressor : ICompressor
    {
        /// <summary>
        /// Gets format of the data compression.
        /// </summary>
        public DataCompressionFormat DataCompressionFormat
        {
            get
            {
                return DataCompressionFormat.Deflate;
            }
        }

        /// <summary>
        /// Compresses the input byte array into stream.
        /// </summary>
        /// <param name="content">Input data.</param>
        /// <param name="requestStream">Request stream.</param>
        public void Compress(byte[] content, Stream requestStream)
        {
            using (var compressedStream = new DeflateStream(requestStream, CompressionMode.Compress))
            {
                compressedStream.Write(content, 0, content.Length);
            }
        }

        /// <summary>
        /// Decompresses the output response stream.
        /// </summary>
        /// <param name="responseStream">Response stream.</param>
        /// <returns>Decompressed stream.</returns>
        public Stream Decompress(Stream responseStream)
        {
            var decompressedStream = new DeflateStream(responseStream, CompressionMode.Decompress);
            return decompressedStream;
        }
    }
}
