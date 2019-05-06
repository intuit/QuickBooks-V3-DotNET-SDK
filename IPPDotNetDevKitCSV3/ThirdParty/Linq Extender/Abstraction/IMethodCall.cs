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

namespace Intuit.Ipp.LinqExtender.Abstraction
{

    /// <summary>
    /// Query item interface for direct calls on collection.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public interface IMethodCall<T>
    {
        /// <summary>
        /// Returns a single item from the collection.
        /// </summary>
        /// <returns></returns>
        T Single();
        /// <summary>
        /// Returns a single item or default value if empty.
        /// </summary>
        /// <returns></returns>
        T SingleOrDefault();
        /// <summary>
        /// Returns the first item from the collection.
        /// </summary>
        /// <returns></returns>
        T First();
        /// <summary>
        /// Returns first item or default value if empty.
        /// </summary>
        /// <returns></returns>
        T FirstOrDefault();
        /// <summary>
        /// Returns the last item from the collection.
        /// </summary>
        /// <returns></returns>
        T Last();
        /// <summary>
        /// Returns last item or default value if empty.
        /// </summary>
        /// <returns></returns>
        T LastOrDefault();
    }

    /// <summary>
    /// Non generic query call interface.
    /// </summary>
    public interface IMethodCall
    {
        /// <summary>
        /// Return true if there is any item in collection.
        /// </summary>
        /// <returns></returns>
        bool Any();
        /// <summary>
        /// Returns the count of items in the collection.
        /// </summary>
        /// <returns></returns>
        object Count();
    }
}