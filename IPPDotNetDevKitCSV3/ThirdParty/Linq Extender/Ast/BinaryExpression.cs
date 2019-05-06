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
    /// Represents the binary operation.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class BinaryExpression : Expression
    {
        private BinaryOperator @operator;
        private Expression left;
        private Expression right;

        /// <summary>
        /// Initalizes a new instance of the <see cref="BinaryExpression"/> class.
        /// </summary>
        /// <param name="@operator">Target operator</param>
        internal BinaryExpression(BinaryOperator @operator)
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
        /// Gets the right expression
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
        /// Gets the binary operator.
        /// </summary>
        public BinaryOperator Operator
        {
            get
            {
                return @operator;
            }
        }

        /// <summary>
        /// Gets a value indicating the type of expression.
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.BinaryExpression; }
        }
    }
}
