
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
    /// TODO: Update summary.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class NotExpression : Ast.Expression
    {
        public string PropertyName { get; set; }

        /// <summary>
        /// Member NotExpression
        /// </summary>
        public NotExpression(string propName)
        {
            this.PropertyName = propName;
        }

        /// <summary>
        /// Override Member CodeType
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.NotExpression; }
        }
    }
}
