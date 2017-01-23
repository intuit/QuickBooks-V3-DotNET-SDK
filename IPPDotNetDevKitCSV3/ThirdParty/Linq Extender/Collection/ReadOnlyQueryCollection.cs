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
