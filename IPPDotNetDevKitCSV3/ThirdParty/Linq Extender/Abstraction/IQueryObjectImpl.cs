using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intuit.Ipp.LinqExtender.Abstraction
{

    /// <summary>
    /// Internal class for query object.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public interface IQueryObjectImpl : IQueryObject
    {
        /// <summary>
        /// Get/Sets if an item is delted from the collection.
        /// </summary>
        bool IsDeleted { get; set; }
        /// <summary>
        /// Gets/Sets if an item is newly added to the collection. 
        /// </summary>
        bool IsNewlyAdded { get; }
        /// <summary>
        /// Gets/Sets if an item is updated. 
        /// </summary>
        bool IsAltered { get; }

        /// <summary>
        /// fills up the bucket from current object.
        /// </summary>
        /// <param name="bucket"></param>
        /// <returns></returns>
        Bucket FillBucket(Bucket bucket);
        /// <summary>
        ///  fills the object from working bucket.
        /// </summary>
        /// <param name="source"></param>
        void FillObject(Bucket source);
        /// <summary>
        /// fills up the property of current object.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="returnType"></param>
        void FillProperty(string name, object value, Type returnType);
    }
}