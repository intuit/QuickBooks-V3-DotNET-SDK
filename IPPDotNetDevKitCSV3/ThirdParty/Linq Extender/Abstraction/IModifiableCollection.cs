////********************************************************************
//https://github.com/mehfuzh/LinqExtender/blob/master/License.txt
//Copyright (c) 2007- 2010 LinqExtender Toolkit Project. 
//Project Modified by Intuit
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
// 
////********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intuit.Ipp.LinqExtender.Abstraction
{
    
    /// <summary>
    /// Generic inteface for modifying collecion.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public interface IModifiableCollection
    {
        /// <summary>
        /// Clears out items from collection.
        /// </summary>
        void Clear();
        /// <summary>
        /// Sorts the collection, using the orderby clause used in query.
        /// </summary>
        void Sort();
    }

    /// <summary>
    /// Non generic interface for modifying colleciton items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public interface IModifiableCollection<T> : IMethodCall<T>, IMethodCall, IModifiableCollection
    {
        /// <summary>
        /// Marks an item to be removed.
        /// </summary>
        /// <param name="value">query object.</param>
        void Remove(T value);
        /// <summary>
        /// Addes a range of items to the collection.
        /// </summary>
        /// <param name="items"></param>
        void AddRange(IEnumerable<T> items);
        /// <summary>
        /// Adds items to the main collection and does a sort operation if any orderby is used in query.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="inMemorySort"></param>
        void AddRange(IEnumerable<T> items, bool inMemorySort);
        /// <summary>
        /// Adds a new item to the collection
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);
    }
}