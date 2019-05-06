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
using System.Linq.Expressions;

namespace Intuit.Ipp.LinqExtender
{
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class MethodCall
    {
        /// <summary>
        /// Initalizes the instance of <see cref="MethodCall"/> class.
        /// </summary>
        /// <param name="name">Name of the method</param>
        /// <param name="parameters">Method arguments.</param>
        internal MethodCall(object target, MethodInfo methodInfo, Parameter[] parameters)
        {
            this.target = target;
            this.methodInfo = methodInfo;
            this.parameters = parameters;
        }

        /// <summary>
        /// Gets the target expression.
        /// </summary>
        public object Target
        {
            get
            {
                return target;
            }
        }

        /// <summary>
        /// Gets the underlying method info.
        /// </summary>
        public MethodInfo Method
        {
            get
            {
                return methodInfo;
            }
        }

        /// <summary>
        /// Gets the array of parameter.
        /// </summary>
        public Parameter[] Parameters
        {
            get
            {
                return parameters;
            }
        }

        public class Parameter
        {
            /// <summary>
            /// Initalizes the new instance of <see cref="Argument"/> class.
            /// </summary>
            /// <param name="type">Type of the argument</param>
            /// <param name="value">Value of the argument</param>
            internal Parameter(Type type, object value)
            {
                this.type = type;
                this.value = value;
            }

            /// <summary>
            /// Gets the parameter value
            /// </summary>
            public object Value
            {
                get
                {
                    return value;
                }
            }

            /// <summary>
            /// Gets the underlying type.
            /// </summary>
            public Type Type
            {
                get
                {
                    return type;
                }
            }


            private readonly Type type;
            private readonly object value;
        }


        private readonly object target;
        private readonly MethodInfo methodInfo;
        private readonly Parameter[] parameters;
    }
}
