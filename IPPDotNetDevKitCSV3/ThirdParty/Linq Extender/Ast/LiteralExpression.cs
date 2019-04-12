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
    /// Represents the extracted value of a query item.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class LiteralExpression : Expression
    {
        
        internal LiteralExpression(Type type, object value)
        {
            this.type = new TypeReference(type);
            this.value = value;
        }

        /// <summary>
        /// Gets the target type.
        /// </summary>
        public TypeReference Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Gets the value that is evaulated from linq query.
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
        }

        /// <summary>
        /// Gets the type of the expression.
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.LiteralExpression; }
        }

        private object value;
        private TypeReference type;
    }
}
