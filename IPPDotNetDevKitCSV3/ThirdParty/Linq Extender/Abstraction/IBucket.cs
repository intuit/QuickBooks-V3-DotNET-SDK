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
using System.Collections.Generic;
using System;

namespace Intuit.Ipp.LinqExtender.Abstraction
{
    
    /// <summary>
    /// Interface defining Bucket object and its accesible proeprties.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal interface IBucket
    {
        /// <summary>
        /// Gets the name of the <see cref="Bucket"/> object, either the class name or value of <c>OriginalEntityName</c>, if used.
        /// </summary>
        string Name { get;}

        /// <summary>
        /// Gets/Sets <value>true</value> if an where is clause used.
        /// </summary>
        bool IsDirty { get; }

        /// <summary>
        /// Gets/Sets Items to Take from collection.
        /// </summary>
        int? ItemsToTake { get;}

        /// <summary>
        /// Gets/ Sets items to skip from start.
        /// </summary>
        int ItemsToSkip { get; }

        /// <summary>
        /// Gets <see cref="BucketItem"/> for property.
        /// </summary>
        IDictionary<string, BucketItem> Items { get;}

        /// <summary>
        /// Gets a list of methods executed on the query.
        /// </summary>
        IList<MethodCall> Methods { get; }

        /// <summary>
        /// Gets the order by information set in query.
        /// </summary>
        IList<Bucket.OrderByInfo> OrderByItems { get; }

        IList<Bucket.SelectInfo> SelectItems { get; }

        IList<Bucket.NotInfo> NotItems { get; }

        /// <summary>
        /// Returns property name for which the UniqueIdentifierAttribute is defined.
        /// </summary>
        string[] UniqueItems { get; }
    }
}
