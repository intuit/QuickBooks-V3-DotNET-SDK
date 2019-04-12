////********************************************************************
//https://github.com/mehfuzh/LinqExtender/blob/master/License.txt
//Copyright (c) 2007- 2010 LinqExtender Toolkit Project. 
//Project Modified by Intuit
/*******************************************************************************
 * Copyright 2016 Intuit
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/
// 
////********************************************************************

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Intuit.Ipp.LinqExtender.Attributes;

namespace Intuit.Ipp.LinqExtender
{
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal class BucketImpl : Bucket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BucketImpl"/> class.
        /// </summary>
        /// <param name="targetType"></param>
        public BucketImpl(Type targetType)
        {
            this.targetType = targetType;
        }

        static internal BucketImpl NewInstance(Type targetType)
        {
            return new BucketImpl(targetType);
        }

        /// <summary>
        /// marks if the bucket is already prepared or not.
        /// </summary>
        internal bool Processed { get; set; }
        
        /// <summary>
        /// internal use : to check if the bucket object should be sorted in asc or dsc
        /// </summary>
        internal bool IsAsc { get; set; }

        /// <summary>
        /// Defines the current expression node.
        /// </summary>
        internal ExpressionType CurrentExpessionType { get; set; }
        
        /// <summary>
        /// number of items queried in <c>Where</c> caluse
        /// </summary>
        internal int ClauseItemCount { get; set; }
        
        /// <summary>
        /// gets the Level of the clause item
        /// </summary>
        internal int Level { get; set; }

        
        internal BucketImpl Init()
        {
            object[] attr = targetType.GetCustomAttributes(typeof(NameAttribute), true);
            if (attr.Length > 0)
            {
                var originalEntityNameAtt = attr[0] as NameAttribute;
                if (originalEntityNameAtt != null) this.Name = originalEntityNameAtt.Name;
            }
            else
            {
                 Name = targetType.Name;
            }
            // clear out;
            Clear();

            // create new items
            Items = CreateItems(targetType);

            return this;
        }


        public Stack<TreeNodeInfo> SyntaxStack
        {
            get
            {
                if (syntaxStack == null)
                    syntaxStack = new Stack<TreeNodeInfo>();

                return syntaxStack;
            }
        }

        internal class TreeNodeInfo
        {
            public int Level
            {
                get;
                set;
            }
            public LogicalOperator CompoundOperator { get; set; }
            /// <summary>
            /// identifier
            /// </summary>
            public Guid Id { get; set; }
            public Guid ParentId { get; set; }
        }

        internal BinaryOperator Relation
        {
            get
            {
                BinaryOperator relType = BinaryOperator.Equal;

                switch (CurrentExpessionType)
                {
                    case ExpressionType.Equal:
                        relType = BinaryOperator.Equal;
                        break;
                    case ExpressionType.GreaterThan:
                        relType = BinaryOperator.GreaterThan;
                        break;
                    case ExpressionType.LessThan:
                        relType = BinaryOperator.LessThan;
                        break;
                    case ExpressionType.NotEqual:
                        relType = BinaryOperator.NotEqual;
                        break;
                    case ExpressionType.LessThanOrEqual:
                        relType = BinaryOperator.LessThanEqual;
                        break;
                    case ExpressionType.GreaterThanOrEqual:
                        relType = BinaryOperator.GreaterThanEqual;
                        break;
                }
                return relType;
            }

        }

        /// <summary>
        /// clear outs the data.
        /// </summary>
        protected new void Clear()
        {
            base.Clear();

            ClauseItemCount = 0;
            CurrentExpessionType = ExpressionType.Equal;
            Processed = false;
        }


        public BucketImpl InstanceImpl
        {
            get
            {
                return this;
            }
        }

        private IDictionary<string, BucketItem> CreateItems(Type target)
        {
            PropertyInfo[] infos = target.GetProperties();

            IDictionary<string, BucketItem> list = new Dictionary<string, BucketItem>();
          
            foreach (PropertyInfo info in infos)
            {
                if (info.CanRead && info.CanWrite)
                {

                    string fieldName = string.Empty;

                    object[] arg = info.GetCustomAttributes(typeof (IgnoreAttribute), false);

                    if (arg.Length == 0)
                    {
                        const bool visible = true;

                        arg = info.GetCustomAttributes(typeof (NameAttribute), false);

                        if (arg.Length > 0)
                        {
                            var fieldNameAttr = arg[0] as NameAttribute;

                            if (fieldNameAttr != null)
                                fieldName = fieldNameAttr.Name;
                        }
                        else
                        {
                            fieldName = info.Name;
                        }

                        arg = info.GetCustomAttributes(typeof (UniqueIdentifierAttribute), true);

                        bool isUnique = arg.Length > 0;

                        // only if not already added.
                        if (!list.ContainsKey(info.Name))
                        {
                            var newItem = new BucketItem(target, fieldName, info.Name, info.PropertyType, null, isUnique,
                                                         BinaryOperator.Equal, visible) {Container = this};
                            list.Add(info.Name, newItem);
                        }
                    }
                }
            }
            return list;
        }

        private Stack<TreeNodeInfo> syntaxStack;
        private Type targetType;
    }
    /// <summary>
    /// internal implementation of bucket object
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal class BucketImpl<T> : BucketImpl 
    {
        public BucketImpl(): base(typeof(T)) {}
   
        #region Fluent BucketImpl

        static new internal BucketImpl<T> NewInstance
        {
            get
            {
                return new BucketImpl<T>();
            }
        }

        #endregion
    }
}