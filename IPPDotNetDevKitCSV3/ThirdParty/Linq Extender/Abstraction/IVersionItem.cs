using System;
namespace Intuit.Ipp.LinqExtender.Abstraction
{
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    interface IVersionItem
    {
        void Commit();
        void Revert();
        object Item { get; }
    }
}