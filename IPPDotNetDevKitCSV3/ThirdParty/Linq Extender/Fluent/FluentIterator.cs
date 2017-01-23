using System;
using System.Linq.Expressions;
using System.Reflection;
using Intuit.Ipp.LinqExtender.Abstraction;

namespace Intuit.Ipp.LinqExtender.Fluent
{
 
    /// <summary>
    /// Fluent iterator entry point.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class FluentIterator : ExpressionVisitor
    {
        /// <summary>
        /// Create a new instance of <see cref="FluentIterator"/> for <see cref="bucket"/>
        /// </summary>
        /// <param name="bucket"></param>
        internal FluentIterator(IBucket bucket)
        {
            this.bucket = bucket;
        }
        /// <summary>
        /// Fluent Item collection implementation.
        /// </summary>
        public class ItemCollection
        {
            /// <summary>
            /// Create a new instance of fluent bucket item.
            /// </summary>
            /// <param name="bucket"></param>
            internal ItemCollection(IBucket bucket)
            {
                this.bucket = bucket;
            }
            /// <summary>
            /// Matches an <see cref="BucketItem"/> for a predicate.
            /// </summary>
            /// <param name="m"></param>
            /// <returns></returns>
            public ItemCollection Match(Predicate<BucketItem> m)
            {
                this.match = m;
                return this;
            }
            /// <summary>
            /// Raises a callback.
            /// </summary>
            /// <param name="callback"></param>
            public ItemCollection Process(Callback callback)
            {
                if (callback == null)
                {
                    throw new ProviderException(Messages.MustProvideACallback);
                }

                foreach (string key in bucket.Items.Keys)
                {
                    BucketItem item = bucket.Items[key];

                    if (match != null)
                    {
                        if (match.Invoke(item))
                        {
                            callback.Invoke(item);
                        }
                    }
                    else
                    {
                        callback.Invoke(item);
                    }
                }
                // tear down 
                match = null;

                return this;
            }

            private readonly IBucket bucket;
            private Predicate<BucketItem> match;
            /// <summary>
            /// Callback delegate from <see cref="BucketItem"/>
            /// </summary>
            /// <param name="item"></param>
            public delegate void Callback(BucketItem item);
            
        }
        /// <summary>
        /// Gets fluent <see cref="BucketItem"/> collection.
        /// </summary>
        public ItemCollection EachItem
        {
            get
            {
                if (collecton == null)
                {
                     collecton = new ItemCollection(bucket);                
                }
                return collecton;
            }
        }
       
        ///<summary>
        /// Gets a <see cref="BucketItem"/> for name
        ///</summary>
        ///<param name="itemName"></param>
        ///<returns></returns>
        public BucketItem Item(string itemName)
        {
            if (bucket.Items.ContainsKey(itemName))
            {
                return bucket.Items[itemName];
            }
            return new BucketItem();
        }

        /// <summary>
        /// Gets <see cref="BucketItem"/> for a property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns><see cref="BucketItem"/></returns>
        public BucketItem Item<T>(Expression<Func<T, object>> expression)
        {
            if (this.memberInfo != null)
            {
                return Item(memberInfo.Name);
            }
            return null;
        }

        /// <summary>
        /// Sets memberInfo to return Expression
        /// </summary>
        public override Expression VisitMemberAccess(MemberExpression expression)
        {
            this.memberInfo = expression.Member;
            return expression;
        }

        private MemberInfo memberInfo;
        private ItemCollection collecton;
        private readonly IBucket bucket;
    }
}