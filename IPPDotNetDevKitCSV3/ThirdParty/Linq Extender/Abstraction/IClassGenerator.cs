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

namespace Intuit.Ipp.LinqExtender.Abstraction
{

    /// <summary>
    /// Entry point interface for <see cref="ClassGenerator"/>
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public interface IClassGenerator
    {
        /// <summary>
        /// Builds the dynamic assembly.
        /// </summary>
        /// <returns></returns>
        IClassGenerator BuildDynamicAssembly();
        /// <summary>
        /// Builds a type in the dynamic assembly, if already the type is not created.
        /// </summary>
        /// <param name="parentType">type of object or interfae to implement</param>
        /// <param name="interfaceType">parent interface type.</param>
        /// <returns></returns>
        IClassGenerator CreateType(Type parentType, Type interfaceType);
        /// <summary>
        /// Adds properties to the dynamic type.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IClassGenerator AddProperty(string name, object value);
        /// <summary>
        /// Creates a new instance of the dynamically generated type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">source object from where to copy the properties.</param>
        /// <returns></returns>
        T CreateInstance<T>(object source);
    }
}