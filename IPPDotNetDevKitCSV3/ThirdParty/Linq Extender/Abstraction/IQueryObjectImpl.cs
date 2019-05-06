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
    /// Internal class for query object.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public interface IQueryObjectImpl : IQueryObject
    {
        /// <summary>
        /// Get/Sets if an item is delted from the collection.
        /// </summary>
        bool IsDeleted { get; set; }
        /// <summary>
        /// Gets/Sets if an item is newly added to the collection. 
        /// </summary>
        bool IsNewlyAdded { get; }
        /// <summary>
        /// Gets/Sets if an item is updated. 
        /// </summary>
        bool IsAltered { get; }

        /// <summary>
        /// fills up the bucket from current object.
        /// </summary>
        /// <param name="bucket"></param>
        /// <returns></returns>
        Bucket FillBucket(Bucket bucket);
        /// <summary>
        ///  fills the object from working bucket.
        /// </summary>
        /// <param name="source"></param>
        void FillObject(Bucket source);
        /// <summary>
        /// fills up the property of current object.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="returnType"></param>
        void FillProperty(string name, object value, Type returnType);
    }
}