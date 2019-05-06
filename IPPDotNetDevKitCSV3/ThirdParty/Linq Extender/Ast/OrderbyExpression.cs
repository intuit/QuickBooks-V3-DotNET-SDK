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
using System.Linq;
using System.Text;

namespace Intuit.Ipp.LinqExtender.Ast
{
    /// <summary>
    /// Represents order by query.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class OrderbyExpression : Ast.Expression
    {
        /// <summary>
        /// Intializes a new instance of the <see cref="OrderbyExpression"/> class.
        /// </summary>
        /// <param name="memberReference">Target memberReference</param>
        /// <param name="ascending">Sort order</param>
        internal OrderbyExpression(MemberReference memberReference, bool ascending)
        {
            this.memberReference = memberReference;
            this.ascending = ascending;
        }

        internal OrderbyExpression(MemberReference memberReference, string suffix, bool ascending)
        {
            this.memberReference = memberReference;
            this.ascending = ascending;
            this.Suffix = suffix;
        }

        /// <summary>
        /// Gets the associated member.
        /// </summary>
        public MemberReference Member
        {
            get
            {
                return memberReference;
            }
        }

        /// <summary>
        /// Gets a value indicating if the order should be made in ascending order.
        /// </summary>
        public bool Ascending
        {
            get
            {
                return ascending;
            }
        }

        /// <summary>
        /// Gets the type of the expression.
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.OrderbyExpression; }
        }

        private MemberReference memberReference;
        private bool ascending;

        /// <summary>
        /// Get or set Suffix
        /// </summary>
        public string Suffix { get; private set; }
    }
}
