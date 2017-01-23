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
