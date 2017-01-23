using Intuit.Ipp.LinqExtender.Abstraction;
using System;
namespace Intuit.Ipp.LinqExtender.Fluent
{
   
    /// <summary>
    /// Fluent implementation for the bucket object.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal class FluentBucket
    {
        /// <summary>
        /// Create a new instance of <see cref="FluentBucket"/> for a <see cref="bucket"/>
        /// </summary>
        /// <param name="bucket"></param>
        public FluentBucket(IBucket bucket)
        {
            this.bucket = bucket;
        }

        /// <summary>
        /// Creates a fluent wrapper of the original bucket object.
        /// </summary>
        /// <param name="bucket"></param>
        /// <returns><see cref="FluentBucket"/></returns>
       
        public static FluentBucket As(IBucket bucket)
        {
            return new FluentBucket(bucket);
        }

        /// <summary>
        /// Creates and gets a new fluent entity object.
        /// </summary>
        public FluentEntity Entity
        {
            get
            {
                if (entity == null)
                {
                    entity = new FluentEntity(bucket);
                }
                return entity;
            }
        }


        public FluentMethod Method
        {
            get
            {
                if (method == null)
                {
                    method = new FluentMethod(bucket);
                }

                return method;
            }
        }


        /// <summary>
        /// Gets true if any where clause is used.
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return bucket.IsDirty;
            }
        }
       

        /// <summary>
        /// Gets the node representing  <see cref="BucketItem"/> and their relational info.
        /// </summary>
        public FluentExpressionTree ExpressionTree
        {
            get
            {
                return new FluentExpressionTree((bucket as Bucket).CurrentNode);
            }
        }

        /// <summary>
        /// enables BucketItem
        /// </summary>
        public FluentIterator For
        {
            get
            {
                return new FluentIterator(bucket);
            }
        }
 
        private IBucket bucket;
        private FluentEntity entity;
        private FluentMethod method;
    }
}