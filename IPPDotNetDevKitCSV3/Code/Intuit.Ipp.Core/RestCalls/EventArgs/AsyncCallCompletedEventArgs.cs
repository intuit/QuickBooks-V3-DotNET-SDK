////********************************************************************
// <copyright file="AsyncCallCompletedEventArgs.cs" company="Intuit">
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
// <summary>This file contains logic for REST request handler.</summary>
////********************************************************************

namespace Intuit.Ipp.Core
{
    using Intuit.Ipp.Exception;

    /// <summary>
    /// Asynchronous call completed event arguments
    /// </summary>
    public partial class AsyncCallCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {
        /// <summary>
        /// The result of the asynchronous operation.
        /// </summary>
        private string results;

        /// <summary>
        /// The result of the asynchronous operation in bytes.
        /// </summary>
        private byte[] byteResults;

        /// <summary>
        /// Ids Exception.
        /// </summary>
        private IdsException error;

        /// <summary>
        /// Initializes a new instance of the AsyncCallCompletedEventArgs class.
        /// /// </summary>
        /// <param name="result">Result of the asynchronous operation.</param>
        /// <param name="error">Ids Exception.</param>
        public AsyncCallCompletedEventArgs(string result, IdsException error, byte[] byteResults = null)
            : base(error, false, null)
        {
            this.results = result;
            this.error = error;
            this.byteResults = byteResults;
        }

        /// <summary>
        /// Gets Result returned from the Asynchronous call
        /// </summary>
        public string Result
        {
            get
            {
                if (this.results != null)
                {
                    return this.results;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets Byte Result returned from the Asynchronous call
        /// </summary>
        public byte[] ByteResult
        {
            get
            {
                if (this.byteResults != null)
                {
                    return this.byteResults;
                }
                else
                {
                    return new byte[0];
                }
            }
        }

        /// <summary>
        /// Gets Ids Exception.
        /// </summary>
        public new IdsException Error
        {
            get
            {
                return this.error;
            }
        }
    }
}
