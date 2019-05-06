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

using System.Collections.Generic;
using System.Linq;
using Intuit.Ipp.LinqExtender.Abstraction;
using System;

namespace Intuit.Ipp.LinqExtender.Collection
{

    /// <summary>
    /// Contains projected read-only query objects.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public abstract class ReadOnlyQueryCollection<T> : ExpressionVisitor, IMethodCall<T>, IMethodCall
    {
        /// <summary>
        /// collection items 
        /// </summary>
        protected List<T> Items = new List<T>();

        #region IQueryReadOnly<T> Members

        /// <summary>
        /// Returns a single item from the collection.
        /// </summary>
        /// <returns></returns>
        T IMethodCall<T>.Single()
        {
            return Items.Single();
        }

        /// <summary>
        /// Returns a single item or default value if empty.
        /// </summary>
        /// <returns></returns>
        public T SingleOrDefault()
        {
            if (Items.Count == 1)
                return Items.Single();
            if (Items.Count > 1)
                throw new ProviderException(Messages.MultipleElementInColleciton);
            return default(T);
        }

        /// <summary>
        /// Return true if there is any item in collection.
        /// </summary>
        /// <returns></returns>
        public bool Any()
        {
            return Items.Count > 0;
        }

        /// <summary>
        /// Returns the count of items in the collection.
        /// </summary>
        /// <returns></returns>
        public object Count()
        {
            return Items.Count;
        }

        /// <summary>
        /// Returns the first item from the collection.
        /// </summary>
        /// <returns></returns>
        T IMethodCall<T>.First()
        {
            return Items.First();
        }

        /// <summary>
        /// Returns first item or default value if empty.
        /// </summary>
        /// <returns></returns>
        public T FirstOrDefault()
        {
            if (Items.Count > 0)
                return Items.First();
            return default(T);
        }

        /// <summary>
        /// Returns the last item from the collection.
        /// </summary>
        /// <returns></returns>
        T IMethodCall<T>.Last()
        {
            return Items.Last();
        }

        /// <summary>
        /// Returns last item or default value if empty.
        /// </summary>
        /// <returns></returns>
        public T LastOrDefault()
        {
            if (Items.Count > 0)
                return Items.Last();
            return default(T);
        }

        #endregion
    }
}
