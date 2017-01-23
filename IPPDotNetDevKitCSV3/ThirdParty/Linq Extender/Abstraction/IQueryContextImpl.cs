using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intuit.Ipp.LinqExtender.Abstraction
{
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal interface IQueryContextImpl<T> : IQueryContext<T>
    {
        IModifiableCollection<T> Collection { get; }
    }
}
