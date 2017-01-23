using System.Collections.Generic;
using System;

namespace Intuit.Ipp.LinqExtender
{
    
    /// <summary>
    /// Entry point interface for defining a custom provider.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public interface IQueryContext<T>
    {
        /// <summary>
        /// Executes the current Linq query.
        /// </summary>
        /// <param name="exprssion"></param>
        /// <returns></returns>
        IEnumerable<T> Execute(Ast.Expression exprssion, bool isToIdsQueryMethod, out string idsQuery);
    }
}
