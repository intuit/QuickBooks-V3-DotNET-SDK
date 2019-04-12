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
namespace Intuit.Ipp.LinqExtender
{

    /// <summary>
    /// Represents the relational query operator equavalent.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public enum BinaryOperator
    {
        /// <summary>
        /// Eqavalent of "=="
        /// </summary>
        Equal = 0,
        /// <summary>
        /// Eqavalent of ">"
        /// </summary>
        GreaterThan,
        /// <summary>
        /// Eqavalent of <![CDATA[ < ]]>
        /// </summary>
        LessThan,
        /// <summary>
        /// Eqavalent of ">="
        /// </summary>
        GreaterThanEqual,
        /// <summary>
        /// Eqavalent of <![CDATA[<=]]>
        /// </summary>
        LessThanEqual,
        /// <summary>
        /// Eqavalent of "!="
        /// </summary>
        NotEqual,
        /// <summary>
        /// Defines the Contains operation in expression.
        /// </summary>
        Contains,
        Not,
        /// <summary>
        /// Default value for first where clause item
        /// </summary>
        NotApplicable

    }
}