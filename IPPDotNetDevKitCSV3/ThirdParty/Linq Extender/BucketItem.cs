using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Intuit.Ipp.LinqExtender.Abstraction;

namespace Intuit.Ipp.LinqExtender
{
    
    /// <summary>
    /// Contains the detail for quried or valued items.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class BucketItem : IContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BucketItem"/> class.
        /// </summary>
        public BucketItem()
        {
            // intentionally left blank.
        }

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        public IContainer Container { get; set; }

        /// <summary>
        /// Get or sets the child.
        /// </summary>
        public Bucket Child { get; set; }

        /// <summary>
        /// Represents the conditioal operators and its compared values for where clause.
        /// </summary>
        public class QueryCondition
        {
            /// <summary>
            /// Creates a new instance of the <see cref="QueryCondition"/>
            /// </summary>
            /// <param name="value">value</param>
            /// <param name="type">type of operation</param>
            public QueryCondition(object value, BinaryOperator type)
            {
                Value = value;
                QueryOperator = type;
            }

            /// <summary>
            /// Gets the query item value.
            /// </summary>
            public object Value
            {
                get
                {
                    return value;
                }
                set
                {
                    // not a first time visit and , value altered.
                    if (this.value != null && this.value.Equals(value))
                    {
                        changed = false;
                    }
                    else
                    {
                        changed = true;
                        this.value = value;
                    }
                }
            }
            /// <summary>
            /// Represents the comparison operators like !=, >=, etc
            /// </summary>
            public BinaryOperator QueryOperator { get; set; }

            internal bool Changed
            {
                get
                {
                    return changed;
                }
            }

            private object value = null;
            private bool changed = false;
        }


        /// <summary>
        /// Creates a new bucket object.
        /// </summary>
        /// <param name="underlyingType">Type of the bucket object</param>
        /// <param name="name">Name of the item that maps with source</param>
        /// <param name="propertyName">Name of property regardless of OriginalFieldNameAttribute</param>
        /// <param name="propertyType">Type of the underlying property.</param>
        /// <param name="value">Value of the item</param>
        /// <param name="unique">if unique attriube is defined for item</param>
        /// <param name="queryOperator">Eelation type , defines what type of expression, equal, lessthan or other.</param>
        /// <param name="queryVisible">Marks that it is to be used in query expression</param>
        internal BucketItem(Type underlyingType, string name, string propertyName, Type propertyType, object value, bool unique, BinaryOperator queryOperator, bool queryVisible)
        {
            this.declaringType = underlyingType;
            // new condtions
            Values.Clear();

            // if value is provided.
            if (value != null)
            {
                Values.Add(new QueryCondition(value, queryOperator));
            }

            // set values.
            this.unique = unique;
            Name = name;
            ProperyName = propertyName;
            Visible = queryVisible;
            this.propertyType = propertyType;
        }

        private IList<QueryCondition> _conditions;
        /// <summary>
        /// Return multiple values if item quried with different values in same where clause.
        /// </summary>
        public IList<QueryCondition> Values
        {
            get
            {
                if (_conditions == null)
                    _conditions = new List<QueryCondition>();
                return _conditions;
            }
        }

        /// <summary>
        /// Return values for quried item in where clause. 
        /// Optionally, tries to combine the value for nested class queries.
        /// Optionally, creates object[] for system and abstract types.
        /// Return values in raw format regardless of its releation type.
        /// </summary>
        public object Value
        {
            get
            {
                if (Values.Count > 1)
                {
                    Type targetType = Values[0].Value.GetType();

                    if (targetType.IsAbstract
                        || targetType.IsEnum
                        || targetType.IsPrimitive
                        || targetType.FullName.IndexOf("System") >= 0)
                    {

                        Array array = Array.CreateInstance(targetType, Values.Count);

                        int index = 0;

                        foreach (QueryCondition value in Values)
                        {
                            array.SetValue(value.Value, index);
                            index++;
                        }
                        return array;
                    }

                    if (targetType.IsClass)
                    {
                        return Values.Combine(targetType);
                    }
                }

                if (Values.Count > 0)
                    return Values[0].Value;
                return null;
            }
            set
            {
                if (Values.Count == 1)
                {
                    Values[0].Value = value;
                    Values[0].QueryOperator = BinaryOperator.Equal;
                }
                else if (Values.Count == 0)
                {
                    Values.Add(new QueryCondition(value, BinaryOperator.Equal));
                }
            }
        }

        /// <summary>
        /// Determines if the item is used multiple times in where clause.
        /// </summary>
        public bool HasMultipleValues
        {
            get
            {
                return Values.Count > 1;
            }
        }

        /// <summary>
        /// Return <c>RelationType</c> enum for the quried item.
        /// </summary>
        public BinaryOperator Operator
        {
            get
            {
                if (Values.Count > 0)
                    return Values[0].QueryOperator;
                return LinqExtender.BinaryOperator.NotApplicable;
            }
        }

        /// <summary>
        ///  Name of the property or value of <c>OriginalFieldNameAttribute</c> if used.
        /// </summary>
        public string Name
        {
            get
            {
                // if its a method call.
                if (this.Method != null)
                    name = Convert.ToString(this.Method.DynamicInvoke());
                return name;
            }
            set
            {
                name = value;
            }
        }

        /// <summary>
        /// Gets the fully qualified name of the <see cref="BucketItem"/>.
        /// </summary>
        public string FullName
        {
            get
            {
                if (this.Method != null)
                {
                    return (string)this.Method.DynamicInvoke();
                }
                return GetFullContainerName(this.Container).TrimStart(new char[] { '.' }) + "." + this.Name;
            }
        }
        /// <summary>
        /// Name of property that bucketItem represents.
        /// </summary>
        public string ProperyName { get; internal set; }
        /// <summary>
        /// Gets or sets the method used in query.
        /// </summary>
        public ExtenderMethod Method { get; internal set; }

        /// <summary>
        /// Gets or sets the member info.
        /// </summary>
        public MemberInfo MemberInfo { get; internal set; }

        /// <summary>
        /// Type of the property that bucketItem represents.
        /// </summary>
        public Type PropertyType
        {
            get
            {
                return this.propertyType;
            }
            set
            {
                this.propertyType = value;
            }
        }
        /// <summary>
        /// Gets if a property is unique.
        /// </summary>
        public bool Unique
        {
            get
            {
                return unique;
            }
            set
            {
                unique = value;
            }
        }

        /// <summary>
        /// <value>true</value> if user updatas the property value manually.
        /// </summary>
        public bool IsModified
        {
            get;
            internal set;
        }

        /// <summary>
        /// Determines if the item is not maked to be Ignored by <c>IgnoreAttribute</c>
        /// </summary>
        internal bool Visible { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if an item is already visited.
        /// </summary>
        internal bool IsVisited { get; set; }

        /// <summary>
        /// Gets the underlying object type.
        /// </summary>
        public Type DeclaringType
        {
            get
            {
                return declaringType;
            }
            internal set
            {
                declaringType = value;
            }
        }

        /// <summary>
        /// Sets the value to target object.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public void SetValue(object target, object value)
        {
            var info = target.GetType().GetProperty(ProperyName, this.propertyType);
            if (info != null) info.SetValue(target, value, null);
        }

        /// <summary>
        /// Gets the value for a target object.
        /// </summary>
        /// <param name="target"></param>
        public object GetValue(object target)
        {
            var info = target.GetType().GetProperty(ProperyName, this.propertyType);
            return info.GetValue(target, null);
        }

        /// <summary>
        /// finds the attribute, within property
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object FindAttribute(Type type)
        {
            var info = declaringType.GetProperty(ProperyName, this.propertyType);

            if (info != null)
            {
                object[] args = info.GetCustomAttributes(type, true);

                if (args.Length == 0)
                {
                    return null;
                }
                return args[0];
            }

            return null;
        }

        /// <summary>
        /// Validates and finds the active item with value.
        /// </summary>
        /// <returns></returns>
        public BucketItem GetActiveItem()
        {
            // no child return what is there.
            if (this.Child == null)
                return this;
            return ProcessNestedBucket(this.Child);
        }

        private BucketItem ProcessNestedBucket(Bucket bucket)
        {
            foreach (string key in bucket.Items.Keys)
            {
                BucketItem childItem = bucket.Items[key];

                if (childItem.Value != null && !childItem.IsVisited)
                {
                    childItem.IsVisited = true;
                    return childItem;
                }

                if (childItem.Child != null)
                {
                    return ProcessNestedBucket(childItem.Child);
                }
            }

            return null;
        }

        internal IDictionary<string, BucketItem> Children
        {
            get
            {
                if (children == null)
                    children = new Dictionary<string, BucketItem>();
                return children;
            }
        }

        private string GetFullContainerName(IContainer container)
        {
            string containerName = string.Empty;

            if (container.Container != null)
            {
                containerName = GetFullContainerName(container.Container);
                if (container is Bucket)
                {
                    // move on.
                }
                else
                {
                    containerName = String.Format("{0}.{1}", containerName.TrimEnd(new[] { '.' }), container.Name);
                }
            }
            //else
            //{
            //    if (container is BucketItem || container.Container == null)
            //        containerName = container.Name;
            //}

            return containerName;
        }

        /// <summary>
        /// Cotains the detail of the method used in query.
        /// </summary>
        public class ExtenderMethod
        {
            /// <summary>
            /// Gets Name of the method.
            /// </summary>
            public string Name { get; internal set; }
            /// <summary>
            /// Gets / Sets the arguments
            /// </summary>
            internal MethodInfo Method { get; set; }
            internal IList<Expression> Arguments { get; set; }

            /// <summary>
            /// Gets the return type.
            /// </summary>
            public object ReturnType
            {
                get
                {
                    return Method.ReturnType;
                }
            }
            /// <summary>
            /// Dynamically invokes the method.
            /// </summary>
            /// <returns></returns>
            public object DynamicInvoke()
            {
                var args = new object[Arguments.Count];

                int index = 0;
                foreach (Expression @argument in Arguments)
                {
                    args[index++] = Expression.Lambda(@argument).Compile().DynamicInvoke();
                }

                object target = Activator.CreateInstance(Method.DeclaringType);

                return Method.Invoke(target, args);
            }
        }


        private bool unique;
        private Type declaringType;
        private Type propertyType;
        private IDictionary<string, BucketItem> children;
        private string name;
    }
}
