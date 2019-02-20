////*********************************************************
// <copyright file="ICompressor.cs" company="Intuit">
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
// <summary>This file contains interface for compressor.</summary>
////*********************************************************

//namespace Intuit.Ipp.Core  
namespace Intuit.Ipp.Utility
{
    using System.IO;
    //using Intuit.Ipp.Core.Compression; 
    


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
