
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
using Intuit.Ipp.LinqExtender.Abstraction;

namespace Intuit.Ipp.LinqExtender.Fluent
{
 
    /// <summary>
    /// Contains Entity Info.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class FluentEntity
    {
        /// <summary>
        /// Creates a new instance of <see cref="FluentEntity"/>
        /// </summary>
        /// <param name="bucket"></param>
        internal FluentEntity(IBucket bucket)
        {
            this.bucket = bucket;   
        }
        /// <summary>
        ///  Name of the entity, can be overriden by <c>OriginalEntityNameAttribute</c>
        /// </summary>
        public string Name
        {
            get
            {
                return bucket.Name;
            }
        }
        /// <summary>
        /// Gets items to fetch from source.
        /// </summary>
        public int? ItemsToFetch
        {
            get
            {
                return bucket.ItemsToTake;
            }
        }
        /// <summary>
        /// Default  0, number of items to skip from start.
        /// </summary>
        public int ItemsToSkipFromStart
        {
            get
            {
                return bucket.ItemsToSkip;
            }
        }
        /// <summary>
        /// List of unique column name.
        /// </summary>
        public string UniqueAttribte
        {
            get
            {
                return bucket.UniqueItems[0];
            }
        }
        /// <summary>
        /// Defines a fluent implentation for order by query.
        /// </summary>
        public class FluentOrderBy
        {
            /// <summary>
            /// Creates a new instance of <see cref="FluentOrderBy"/>
            /// </summary>
            /// <param name="bucket"></param>
            internal FluentOrderBy(IBucket bucket)
            {
                this.bucket = bucket;
            }
            
            /// <summary>
            /// Checks if orderby is used in query and calls action delegate to 
            /// execute user's code and internally marks <value>true</value> for ifUsed field
            /// to be used by <see cref="FluentOrderByItem"/> iterator.
            /// </summary>
            /// <param name="action"></param>
            /// <returns></returns>
            public FluentOrderBy IfUsed(Action action)
            {
                ifUsed = bucket.OrderByItems.Count > 0;

                if (ifUsed && action != null)
                    action.DynamicInvoke();
                return this;
            }
            /// <summary>
            /// Iterator for order by items.
            /// </summary>
            public FluentOrderByItem ForEach
            {
                get
                {
                    return new FluentOrderByItem(bucket);
                }
            }
         
            private bool ifUsed = false;
            /// <summary>
            /// Callback handler for <see cref="FluentOrderBy"/>
            /// </summary>
            /// <param name="member">Target member</param>
            /// <param name="ascending">bool for sort order</param>
            public delegate void Callback(MemberReference member, bool ascending);

            /// <summary>
            /// Callback handler for <see cref="FluentOrderBy"/>
            /// </summary>
            /// <param name="member">Target member</param>
            /// <param name="suffix">suffix</param>
            /// <param name="ascending">bool for sort order</param>
            public delegate void CallbackSuffix(MemberReference member, string suffix, bool ascending);

            private readonly IBucket bucket;

            /// <summary>
            /// Order by iterator.
            /// </summary>
            public class FluentOrderByItem
            {
                /// <summary>
                /// Creates a new instance of <see cref="FluentOrderBy"/>
                /// </summary>
                /// <param name="bucket"></param>
                internal FluentOrderByItem(IBucket bucket)
                {
                    this.bucket = bucket;
                }

                /// <summary>
                /// Does a callback to process the order by used in where clause.
                /// </summary>
                /// <param name="callback"></param>
                public void Process(Callback callback)
                {
                    foreach (Bucket.OrderByInfo info in bucket.OrderByItems)
                    {
                        callback.Invoke(info.Member, info.IsAscending);
                    }
                }

                public void Process(CallbackSuffix callback)
                {
                    foreach (Bucket.OrderByInfo info in bucket.OrderByItems)
                    {
                        callback.Invoke(info.Member, info.Suffix, info.IsAscending);
                    }
                }

                private readonly IBucket bucket;
            }
        }

        public class FluentSelect
        {
            /// <summary>
            /// Creates a new instance of <see cref="FluentOrderBy"/>
            /// </summary>
            /// <param name="bucket"></param>
            internal FluentSelect(IBucket bucket)
            {
                this.bucket = bucket;
            }

            /// <summary>
            /// Checks if orderby is used in query and calls action delegate to 
            /// execute user's code and internally marks <value>true</value> for ifUsed field
            /// to be used by <see cref="FluentOrderByItem"/> iterator.
            /// </summary>
            /// <param name="action"></param>
            /// <returns></returns>
            public FluentSelect IfUsed(Action action)
            {
                ifUsed = bucket.SelectItems.Count > 0;

                if (ifUsed && action != null)
                    action.DynamicInvoke();
                return this;
            }
            /// <summary>
            /// Iterator for order by items.
            /// </summary>
            public FluentSelectItem ForEach
            {
                get
                {
                    return new FluentSelectItem(bucket);
                }
            }

            private bool ifUsed = false;
            /// <summary>
            /// Callback handler for <see cref="FluentOrderBy"/>
            /// </summary>
            /// <param name="member">Target member</param>
            /// <param name="ascending">bool for sort order</param>
            public delegate void Callback(string propName);

            private readonly IBucket bucket;

            /// <summary>
            /// Select iterator.
            /// </summary>
            public class FluentSelectItem
            {
                /// <summary>
                /// Creates a new instance of <see cref="FluentOrderBy"/>
                /// </summary>
                /// <param name="bucket"></param>
                internal FluentSelectItem(IBucket bucket)
                {
                    this.bucket = bucket;
                }

                /// <summary>
                /// Does a callback to process the order by used in where clause.
                /// </summary>
                /// <param name="callback"></param>
                public void Process(Callback callback)
                {
                    foreach (Bucket.SelectInfo info in bucket.SelectItems)
                    {
                        callback.Invoke(info.PropertyName);
                    }
                }

                private readonly IBucket bucket;
            }
        }

        public class FluentNot
        {
            /// <summary>
            /// Creates a new instance of <see cref="FluentNot"/>
            /// </summary>
            /// <param name="bucket"></param>
            internal FluentNot(IBucket bucket)
            {
                this.bucket = bucket;
            }

            /// <summary>
            /// Checks if orderby is used in query and calls action delegate to 
            /// execute user's code and internally marks <value>true</value> for ifUsed field
            /// to be used by <see cref="FluentOrderByItem"/> iterator.
            /// </summary>
            /// <param name="action"></param>
            /// <returns></returns>
            public FluentNot IfUsed(Action action)
            {
                ifUsed = bucket.SelectItems.Count > 0;

                if (ifUsed && action != null)
                    action.DynamicInvoke();
                return this;
            }
            /// <summary>
            /// Iterator for order by items.
            /// </summary>
            public FluentNotItem ForEach
            {
                get
                {
                    return new FluentNotItem(bucket);
                }
            }

            private bool ifUsed = false;
            /// <summary>
            /// Callback handler for <see cref="FluentOrderBy"/>
            /// </summary>
            /// <param name="member">Target member</param>
            /// <param name="ascending">bool for sort order</param>
            public delegate void Callback(string propName);

            private readonly IBucket bucket;

            /// <summary>
            /// Select iterator.
            /// </summary>
            public class FluentNotItem
            {
                /// <summary>
                /// Creates a new instance of <see cref="FluentOrderBy"/>
                /// </summary>
                /// <param name="bucket"></param>
                internal FluentNotItem(IBucket bucket)
                {
                    this.bucket = bucket;
                }

                /// <summary>
                /// Does a callback to process the order by used in where clause.
                /// </summary>
                /// <param name="callback"></param>
                public void Process(Callback callback)
                {
                    foreach (Bucket.NotInfo info in bucket.NotItems)
                    {
                        callback.Invoke(info.PropertyName);
                    }
                }

                private readonly IBucket bucket;
            }
        }

        /// <summary>
        /// Gets an intance for the <see cref="FluentOrderBy"/>
        /// </summary>
        public FluentOrderBy OrderBy
        {
            get
            {
                return new FluentOrderBy(bucket);
            }
        }
        
        public FluentSelect Select
        {
            get
            {
                return new FluentSelect(bucket);
            }
        }
        
        public FluentNot Nots
        {
            get
            {
                return new FluentNot(bucket);
            }
        }

        private readonly IBucket bucket;
    }
}