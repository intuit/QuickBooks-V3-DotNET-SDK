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