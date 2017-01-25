////********************************************************************
// <copyright file="GlobalTaxServiceCallCompletedEventArgs.cs" company="Intuit">
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
    /// <typeparam name="T">Generic constraint of type TaxService.</typeparam>
    public class GlobalTaxServiceCallCompletedEventArgs<T> where T : Intuit.Ipp.Data.TaxService
    {
        /// <summary>
        /// Initializes a new instance of the CallCompletedEventArgs class.
        /// </summary>
        public GlobalTaxServiceCallCompletedEventArgs()
        {
        }

        /// <summary>
        /// Gets or sets the Entity of type T.
        /// </summary>
        public T TaxService { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        public IdsException Error { get; set; }
    }
}
