////********************************************************************
// <copyright file="CallCompletedEventArgs.cs" company="Intuit">
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
    /// Contains events for call back methods and corresponding fields
    /// </summary>
    /// <typeparam name="T">Generic constraint of type IEntity.</typeparam>
    public class CallCompletedEventArgs<T> where T : Intuit.Ipp.Data.IEntity
    {
        /// <summary>
        /// Initializes a new instance of the CallCompletedEventArgs class.
        /// </summary>
        public CallCompletedEventArgs()
        {
        }

        /// <summary>
        /// Gets or sets the Entity of type T.
        /// </summary>
        public T Entity { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        public IdsException Error { get; set; }
    }

    /// <summary>
    /// Contains events for call pdf back methods and corresponding fields
    /// </summary>
    public class PdfCallCompletedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the CallCompletedEventArgs class.
        /// </summary>
        public PdfCallCompletedEventArgs()
        {
        }

        /// <summary>
        /// Gets or sets the Entity of type T.
        /// </summary>
        public byte[] PdfBytes { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        public IdsException Error { get; set; }
    }
}
