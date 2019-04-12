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

using System.Reflection;
using System;

namespace Intuit.Ipp.LinqExtender.Ast
{
   
    /// <summary>
    /// Defines method calls on the query
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class MethodCallExpression : Expression
    {
        internal MethodCallExpression(MethodCall methodCall)
        {
            this.methodCall = methodCall;
        }

        /// <summary>
        /// Gets the target
        /// </summary>
        public object Target
        {
            get
            {
                return methodCall.Target;
            }
        }

        /// <summary>
        /// Gets the underlying method info.
        /// </summary>
        public MethodInfo Method
        {
            get
            {
                return methodCall.Method;
            }
        }

        /// <summary>
        /// Gets a value indicating that it is a take call.
        /// </summary>
        public bool IsTake
        {
            get
            {
                return methodCall.Method.Name == MethodNames.Take;
            }
        }

        /// <summary>
        /// Gets a value indicating that it is a skip method.
        /// </summary>
        public bool IsSkip
        {
            get
            {
                return methodCall.Method.Name == MethodNames.Skip;
            }
        }

        /// <summary>
        /// Gets the method parameters.
        /// </summary>
        public MethodCall.Parameter [] Paramters
        {
            get
            {
                return methodCall.Parameters;
            }
        }

        /// <summary>
        /// Override member
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.MethodCallExpression; }
        }

        private readonly MethodCall methodCall;
    }
}
