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

namespace Intuit.Ipp.LinqExtender.Ast
{
  
    /// <summary>
    /// Combines a number expressions sequentially
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class BlockExpression : Ast.Expression
    {
        internal BlockExpression()
        {
            expressions = new List<Expression>();
        }

        /// <summary>
        /// Gets the list of expression associated with the current query.
        /// </summary>
        public IList<Expression> Expressions
        {
            get
            {
                return expressions;
            }
        }

        /// <summary>
        /// Gets a value indicating the type of expression.
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.BlockExpression; }
        }

        private IList<Expression> expressions;
    }
}
