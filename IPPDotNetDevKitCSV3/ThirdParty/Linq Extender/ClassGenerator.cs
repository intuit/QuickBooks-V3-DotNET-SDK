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
using System.Reflection;
using System.Reflection.Emit;
using Intuit.Ipp.LinqExtender.Abstraction;

namespace Intuit.Ipp.LinqExtender
{
    /// <summary>
    /// Generates a new object from existing one using the user's setup.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal class ClassGenerator : IClassGenerator
    {
        /// <summary>
        /// Creates and gets a new instance <see cref="ClassGenerator"/> class.
        /// </summary>
        public static ClassGenerator Instance
        {
            get
            {
                return new ClassGenerator();
            }
        }
        /// <summary>
        /// Builds dynamic assembly.
        /// </summary>
        /// <returns></returns>
        public IClassGenerator BuildDynamicAssembly()
        {
            AssemblyName assemblyName = new AssemblyName("ExtenderProxy");
            AssemblyBuilder createdAssembly =  AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            // define module
            this.moduleBuilder = createdAssembly.DefineDynamicModule(assemblyName.Name);

            this.isTypeCreated = false;

            return this;
        }

        /// <summary>
        /// Builds a type in the dynamic assembly, if already the type is not created.
        /// </summary>
        /// <param name="parent">type of object or interfae to implement</param>
        /// <param name="interfaceType">parent interface type.</param>
        /// <returns></returns>
        public IClassGenerator CreateType(Type parent, Type interfaceType)
        {
            // create the list.
            properties = new Dictionary<string, object>();

            // define a type only if it is not previously defined.
            if (parent != null && this.moduleBuilder.GetType(parent.FullName) == null)
            {
                if (parent.IsInterface)
                {
                    this.typeBuilder = this.moduleBuilder.DefineType(parent.FullName, TypeAttributes.Class | TypeAttributes.Public);
                    this.typeBuilder.AddInterfaceImplementation(parent);
                }
                else
                {
                    this.typeBuilder = this.moduleBuilder.DefineType(parent.FullName, TypeAttributes.Class | TypeAttributes.Public, parent);
                }

                if (interfaceType != null)
                {
                    this.typeBuilder.AddInterfaceImplementation(interfaceType);
                }

                ConstructorBuilder constructorBuilder = this.typeBuilder.DefineConstructor(MethodAttributes.Public,
                                                                                           CallingConventions.Standard,
                                                                                           Type.EmptyTypes);

                ILGenerator ilGenerator = constructorBuilder.GetILGenerator();

                ConstructorInfo baseConstructor = null;
                ConstructorInfo[] constructorInfos = parent.GetConstructors();

                bool containsDefaultConstructor = false;

                foreach (ConstructorInfo info in constructorInfos)
                {
                    if (info.GetParameters().Length == 0)
                    {
                        baseConstructor = info;
                        containsDefaultConstructor = true;
                        break;
                    }
                }

                if (!containsDefaultConstructor)
                    throw new Exception(Messages.MustHaveADefaultConstructor);

                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Call, baseConstructor);
                ilGenerator.Emit(OpCodes.Ret);
            }

            return this;
        }

        /// <summary>
        /// Adds a property dynamically to the type, with specific type.
        /// </summary>
        /// <param name="name">name of the property</param>
        /// <param name="value">value of the property</param>
        /// <returns><see cref="IClassGenerator"/></returns>
        public IClassGenerator AddProperty(string name, object value)
        {
            if (this.typeBuilder != null && !this.isTypeCreated)
            {
                Type propertyType = GetTypeFromValue(value);

                PropertyBuilder builder = typeBuilder.DefineProperty(name, PropertyAttributes.HasDefault, propertyType, null);

                FieldBuilder backingField = typeBuilder.DefineField(name.ToLower(), value.GetType(),
                                                                    FieldAttributes.Private);

                // The property "set" and property "get" methods require a special
                // set of attributes.
                const MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.NewSlot|
                                                          MethodAttributes.SpecialName | MethodAttributes.HideBySig | MethodAttributes.Virtual;

                // Define the "get" accessor. 
                MethodBuilder getAccessor = typeBuilder.DefineMethod(
                    "get_" + name,
                    methodAttributes,
                    propertyType,
                    Type.EmptyTypes);

                ILGenerator getILGenerator = getAccessor.GetILGenerator();

                getILGenerator.Emit(OpCodes.Ldarg_0);
                getILGenerator.Emit(OpCodes.Ldfld, backingField);
                getILGenerator.Emit(OpCodes.Ret);

                // Define the "set" accessor.
                MethodBuilder setAccessor = typeBuilder.DefineMethod(
                    "set_" + name,
                    methodAttributes,
                    null,
                    new Type[] { propertyType });

                ILGenerator setILGenerator = setAccessor.GetILGenerator();

                setILGenerator.Emit(OpCodes.Ldarg_0);
                setILGenerator.Emit(OpCodes.Ldarg_1);
                setILGenerator.Emit(OpCodes.Stfld, backingField);
                setILGenerator.Emit(OpCodes.Ret);

                // Last, map the "get" and "set" accessor methods to the 
                // PropertyBuilder. The property is now complete. 
                builder.SetGetMethod(getAccessor);
                builder.SetSetMethod(setAccessor);
            }
            properties.Add(name, value);
          
            return this;
        }
        /// <summary>
        /// Creates a new instance of the dynamically generated type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">source object from where to copy the properties.</param>
        /// <returns></returns>
        public T CreateInstance<T>(object source)
        {
            Type reflectedType;

            if (isTypeCreated && this.moduleBuilder.GetType(typeof(T).FullName) != null)
            {
                reflectedType = this.moduleBuilder.GetType(typeof(T).FullName);
            }
            else
            {
                reflectedType = this.typeBuilder.CreateType();
                isTypeCreated = true;
            }

            object obj = Activator.CreateInstance(reflectedType);

            foreach (string key in properties.Keys)
            {
                PropertyInfo pi = reflectedType.GetProperty(key);
                pi.SetValue(obj, properties[key], null);
            }

            // fill from existing object.
            foreach (var property in obj.GetType().GetBaseProperties())
            {
                if (property.CanWrite)
                {
                    property.SetValue(obj, property.GetValue(source, null), null);
                }
            }

            return (T)obj;
        }

        /// <summary>
        /// Gets a <see cref="Type"/> from provided <value>value</value>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Type GetTypeFromValue(object value)
        {
            return value.GetType();
        }

        private TypeBuilder typeBuilder;
        private ModuleBuilder moduleBuilder;
        private IDictionary<string, object> properties;
        private bool isTypeCreated;
    }
}