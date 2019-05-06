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
using Intuit.Ipp.LinqExtender.Abstraction;
using Intuit.Ipp.LinqExtender.Collection;
using System;

namespace Intuit.Ipp.LinqExtender
{
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal class QueryContextImpl<T> : IQueryContextImpl<T>
    {
        public QueryContextImpl(IQueryContext<T> queryContext)
        {
            this.queryContext = queryContext;
            collection = new QueryCollection<T>();
        }

        public IModifiableCollection<T> Collection
        {
            get
            {
                return collection;
            }
        }

        IEnumerable<T> IQueryContext<T>.Execute(Ast.Expression exprssion, bool isToIdsQueryMethod, out string idsQuery)
        {
            return queryContext.Execute(exprssion, isToIdsQueryMethod, out idsQuery);
        }

        private readonly IModifiableCollection<T> collection;
        private readonly IQueryContext<T> queryContext;
    }
}
