using System.Collections.Generic;
using System.Linq;
using Intuit.Ipp.LinqExtender.Abstraction;
using System;

namespace Intuit.Ipp.LinqExtender
{
   
    /// <summary>
    /// Bucket is stuctured representation of the orignal query object.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class Bucket : IBucket, IContainer
    {
        /// <summary>
        /// Gets/ sets the name of the container.
        /// </summary>
        public IContainer Container { get; internal set; }

        /// <summary>
        /// Gets the name of the <see cref="Bucket"/> object, either the class name or value of <c>OriginalEntityName</c>, if used.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            internal set
            {
                name = value;
            }
        }

        /// <summary>
        /// Gets/Sets <value>true</value> if an where is clause used.
        /// </summary>
        public bool IsDirty { get; internal set; }
        /// <summary>
        /// Gets/Sets Items to Take from collection.
        /// </summary>
        public int? ItemsToTake { get; internal set; }
        /// <summary>
        /// Gets/ Sets items to skip from start.
        /// </summary>
        public int ItemsToSkip { get; internal set; }

        /// <summary>
        /// Returns property name for which the UniqueIdentifierAttribute is defined.
        /// </summary>
        public string[] UniqueItems
        {
            get
            {
                if (uniqueItemNames == null)
                {
                    var query = from prop in items
                                where prop.Value.Unique
                                select prop.Value.Name;

                    uniqueItemNames = query.ToArray();
                }
                return uniqueItemNames;
            }
        }
        /// <summary>
        /// Contains property items for current bucket.
        /// </summary>
        public IDictionary<string, BucketItem> Items
        {
            get
            {
                if (items == null)
                    items = new Dictionary<string, BucketItem>();
                return items;
            }
            internal set
            {
                items = value;
            }
        }

        /// <summary>
        /// Gets the first tree node fro simplied expression tree.
        /// </summary>
        internal TreeNode CurrentNode
        {
            get
            {
                if (node == null)
                    node = new TreeNode();
                return node;
            }
            set
            {
                node = value;
            }
        }
        /// <summary>
        /// Gets/Sets the current <see cref="CurrentNode"/>
        /// </summary>
        internal TreeNode CurrentTreeNode
        {
            get; set;
        }

        /// <summary>
        /// Gets a list of methods executed on the query.
        /// </summary>
        public IList<MethodCall> Methods
        {
            get
            {
                if (methods == null)
                    methods = new List<MethodCall>();

                return methods;
            }
        }
      
        /// <summary>
        /// The Filled up with query order by information.
        /// </summary>
        public class OrderByInfo
        {
            /// <summary>
            /// Gets the member associated with the current orderby call.
            /// </summary>
            public MemberReference Member
            {
                get
                {
                    return memberReference;
                }
            }
            /// <summary>
            /// Gets true if the order by is ascending.
            /// </summary>
            public bool IsAscending
            {
                get
                {
                    return asc;
                }
            }

            /// <summary>
            /// Sets memeberReference, asc
            /// </summary>
            internal OrderByInfo(MemberReference memberReference, bool asc)
            {
                this.memberReference = memberReference;
                this.asc = asc;
            }

            /// <summary>
            /// Sets memeberReference, Suffix, asc
            /// </summary>
            public OrderByInfo(MemberReference memberReference, string suffix, bool asc)
            {
                // TODO: Complete member initialization
                this.memberReference = memberReference;
                this.Suffix = suffix;
                this.asc = asc;
            }

            private readonly bool asc;
            private readonly MemberReference memberReference;

            /// <summary>
            /// Get Suffix
            /// </summary>
            public string Suffix { get; private set; }
        }

        public class NotInfo
        {
            public string PropertyName { get; set; }
        }

        public class SelectInfo
        {
            public string PropertyName { get; set; }
        }

        /// <summary>
        /// Contains the group by detail.
        /// </summary>
        public class GroupByContainer
        {
            /// <summary>
            /// Gets or set the key by which group by will be made.
            /// </summary>
            public string Key { get; set; }
        }

        /// <summary>
        /// Holds order by information.
        /// </summary>
        public IList<OrderByInfo> OrderByItems
        {
            get
            {
                if (orderByItems == null)
                    orderByItems = new List<OrderByInfo>();
                return orderByItems;
            }
        }

        public IList<SelectInfo> SelectItems
        {
            get
            {
                if (true)
                {
                    if (selectItems == null)
                        selectItems = new List<SelectInfo>();
                    return selectItems;
                }
            }
        }

        public IList<NotInfo> NotItems
        {
            get
            {
                if (true)
                {
                    if (notItems == null)
                        notItems = new List<NotInfo>();
                    return notItems;
                }
            }
        }

        /// <summary>
        /// Gets or set the group container.
        /// </summary>
        public GroupByContainer Group { get; set; }
       

        /// <summary>
        /// Gets unique identifier properties.
        /// </summary>
        internal string[] UniqueProperties
        {
            get
            {
                if (uniquePropertyNames == null)
                {
                    var query = from prop in items
                                where prop.Value.Unique
                                select prop.Value.ProperyName;

                    uniquePropertyNames = query.ToArray();
                }
                return uniquePropertyNames;
            }
        }

        /// <summary>
        /// Clears out any used properties.
        /// </summary>
        protected void Clear()
        {
            ItemsToSkip = 0;
            ItemsToTake = null;
            OrderByItems.Clear();
            IsDirty = false;
        }

        private string name = string.Empty;
        private IList<MethodCall> methods;
        private string[] uniqueItemNames;
        private string[] uniquePropertyNames;
        private IDictionary<string, BucketItem> items;
        private IList<OrderByInfo> orderByItems;
        private IList<SelectInfo> selectItems;
        private IList<NotInfo> notItems;
        private TreeNode node;

       
    }
}
