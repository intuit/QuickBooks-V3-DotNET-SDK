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