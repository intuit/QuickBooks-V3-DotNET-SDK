
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

namespace Intuit.Ipp.LinqExtender.Ast
{
    
    /// <summary>
    /// Reprensents Logical blocks.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class LogicalExpression : Expression
    {
        /// <summary>
        /// Intializes a new instance of the <see cref="LogicalExpression"/> class.
        /// </summary>
        /// <param name="@operator">Logical operator</param>
        internal LogicalExpression(LogicalOperator @operator)
        {
            this.@operator = @operator;
        }

        /// <summary>
        /// Gets the left expression.
        /// </summary>
        public Expression Left
        {
            get
            {
                return left;
            }
            internal set
            {
                left = value;
            }
        }

        /// <summary>
        /// Gets the right exprssison.
        /// </summary>
        public Expression Right
        {
            get
            {
                return right;
            }
            internal set
            {
                right = value;
            }
        }

        /// <summary>
        /// Gets value that indicates that the current is a child expression.
        /// </summary>
        public bool IsChild
        {
            get
            {
                return isChild;
            }
            internal set
            {
                isChild = value;
            }
        }

        /// <summary>
        /// Gets the operator joining the left and right expression.
        /// </summary>
        public LogicalOperator Operator
        {
            get
            {
                return @operator;
            }
        }

        /// <summary>
        /// Gets the type of the expression.
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.LogicalExpression; }
        }

        private readonly LogicalOperator @operator;
        private bool isChild;
        private Expression left;
        private Expression right;
    }
}
