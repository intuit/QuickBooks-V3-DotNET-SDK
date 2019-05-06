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