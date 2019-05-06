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
using System.Reflection;

namespace Intuit.Ipp.LinqExtender.Ast
{
   
    /// <summary>
    /// Represents query members
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class MemberExpression : Expression
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberExpression"/> class.
        /// </summary>
        /// <param name="item">Target <see cref="BucketItem"/></param>
        internal MemberExpression(BucketItem item)
        {
            this.item = item;
            this.member = new MemberReference(item.MemberInfo);
        }

        /// <summary>
        /// Gets the member reference.
        /// </summary>
        public MemberReference Member
        {
            get
            {
                return member;
            }
        }

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        public string Name
        {
            get
            {
                return member.Name;
            }
        }

        /// <summary>
        /// Gets the full name of the member.
        /// </summary>
        public string FullName
        {
            get
            {
                return this.item.FullName;
            }
        }

        /// <summary>
        /// Gets the declaring type for the member.
        /// </summary>
        public Type DeclaringType
        {
            get
            {
                return this.item.DeclaringType;
            }
        }

        /// <summary>
        /// Finds the target custom attribute for the member.
        /// </summary>
        public T FindAttribute<T>()
        {
            return (T)item.FindAttribute(typeof(T));
        }

        /// <summary>
        /// Gets the type of the expression.
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.MemberExpression; }
        }

        private BucketItem item;
        private MemberReference member;
    }
}
