////*********************************************************
// <copyright file="GZipCompressor.cs" company="Intuit">
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
// <summary>This file contains SdkException.</summary>
// <summary>This file contains implementation for gzip compressor.</summary>
////*********************************************************

//namespace Intuit.Ipp.Core.Compression  
namespace Intuit.Ipp.Utility
{
    using System.IO;
    using System.IO.Compression;
    using System.Net;

    /// <summary>
    /// GZip compressor.
    /// </summary>
    public class GZipCompressor : ICompressor
    {
        /// <summary>
        /// Gets format of the data compression.
        /// </summary>
        public DataCompressionFormat DataCompressionFormat
        {
            get
            {
                return DataCompressionFormat.GZip;
            }
        }

        /// <summary>
        /// Compresses the input byte array into stream.
        /// </summary>
        /// <param name="content">Input data.</param>
        /// <param name="requestStream">Request stream.</param>
        public void Compress(byte[] content, Stream requestStream)
        {
            using (var compressedStream = new GZipStream(requestStream, CompressionMode.Compress))
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
            var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress);
            return decompressedStream;
        }
    }
}
