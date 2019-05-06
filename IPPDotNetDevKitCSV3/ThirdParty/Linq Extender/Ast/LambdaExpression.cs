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
    /// Defines lambda expression.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class LambdaExpression : Expression 
    {
        /// <summary>
        /// Initailizes a new instance of the <see cref="LambdaExpression"/> class.
        /// </summary>
        /// <param name="type">Target object type.</param>
        public LambdaExpression(Type type)
        {
            this.type = type;
        }

        /// <summary>
        /// Gets the underlying type of the expression.
        /// </summary>
        public Type Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Gets the body of the expression.
        /// </summary>
        public Expression Body
        {
            get
            {
                return body;
            }
            internal set
            {
                body = value;
            }
        }

        /// <summary>
        /// Gets the type of the expression.
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.LambdaExpresion; }
        }

        Type type;
        Expression body;
     
    }
}
