////*********************************************************
// <copyright file="IntuitCDCResponse.cs" company="Intuit">
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
// <summary>This file contains code related to the Change Data Capture operation.</summary>
////*********************************************************


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intuit.Ipp.Data;
using Intuit.Ipp.Exception;

namespace Intuit.Ipp.DataService
{
    /// <summary>
    /// This class processes the CDC request.
    /// </summary>
    public class IntuitCDCResponse
    {
        /// <summary>
        ///  list of entities.
        /// </summary>
        public Dictionary<string, List<IEntity>> entities;

        /// <summary>
        /// IdsException in case of ResponseType is exception. 
        /// </summary>
        public Dictionary<string, IdsException> exceptions;

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="IntuitBatchResponse"/> class.
        /// </summary>
        public IntuitCDCResponse()
        {
            this.entities = new Dictionary<string, List<IEntity>>();
            this.exceptions = new Dictionary<string, IdsException>();
        }

        #endregion

        /// <summary>
        /// Gets the List of entity value with particular key
        /// </summary>
        /// <param name="key">key.</param>
        public List<IEntity> getEntity(string key)
        {
            return entities.FirstOrDefault(x => x.Key == key).Value;
        }
    }
}
