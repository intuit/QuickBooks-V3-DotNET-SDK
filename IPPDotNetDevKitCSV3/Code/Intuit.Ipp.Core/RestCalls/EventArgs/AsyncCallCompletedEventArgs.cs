////********************************************************************
// <copyright file="AsyncCallCompletedEventArgs.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
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
