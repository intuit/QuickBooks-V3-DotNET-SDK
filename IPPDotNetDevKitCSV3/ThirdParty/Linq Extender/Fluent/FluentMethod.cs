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
using Intuit.Ipp.LinqExtender.Abstraction;

namespace Intuit.Ipp.LinqExtender.Fluent
{
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal class FluentMethod
    {
        /// <summary>
        /// Initializes the instance of <see cref="FluentMethod"/> class.
        /// </summary>
        /// <param name="bucket">Target bucket</param>
        public FluentMethod(IBucket bucket)
        {
            this.bucket = bucket;
        }

        internal void ForEach(Action<MethodCall> action)
        {
            for (int index = 0; index < bucket.Methods.Count; index++)
            {
                action(bucket.Methods[index]);
            }
        }

        private readonly IBucket bucket;
    }
}
