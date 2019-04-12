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
using System.Linq;
using Intuit.Ipp.LinqExtender.Abstraction;
using Intuit.Ipp.LinqExtender.Attributes;

namespace Intuit.Ipp.LinqExtender
{

    /// <summary>
    /// Defines various helper method used throughout the project.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public static class Utility
    {
        internal static IDictionary<string, object> GetUniqueItemDefaultDetail(this IQueryObject obj)
        {
            Type runningType = obj.GetType();
           
            if (!uniqueDefaultValueMap.ContainsKey(runningType.Name))
            {
                IDictionary<string, object> uniqueDefaultValues = new Dictionary<string, object>();
                //clone the result.
                object runningObject = Activator.CreateInstance(runningType);

                PropertyInfo[] infos = runningType.GetProperties();
                
                int index = 0;

                foreach (PropertyInfo info in infos)
                {
                    object[] arg = info.GetCustomAttributes(typeof (UniqueIdentifierAttribute), true);

                    if (arg.Length > 0)
                    {
                        object value = info.GetValue(runningObject, null);

                        if (!uniqueDefaultValues.ContainsKey(info.Name))
                        {
                            uniqueDefaultValues.Add(info.Name, new {Index = index, Value = value});
                            break;
                        }
                    }
                    index++;
                }
                uniqueDefaultValueMap.Add(runningType.Name, uniqueDefaultValues);
            }
            return uniqueDefaultValueMap[runningType.Name];
        }


        internal static bool EqualsDefault(this object targetValue, string propertyName, object source)
        {
            if (targetValue != null && (targetValue.GetType().IsPrimitive || targetValue.GetType().IsEnum))
            {
                object @default = Activator.CreateInstance(source.GetType());
                return @default.GetType().GetProperty(propertyName).GetValue(@default, null).Equals(targetValue);
            }
            return false;
        }

        internal static T Cast<T>(object obj, T type)
        {
            return (T)obj;
        }
     
        /// <summary>
        /// tries to combine the values for a give a type . Ex User defined clasee
        /// and its properties.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Combine(this IList<BucketItem.QueryCondition> list, Type type)
        {
            object combinedObject = Activator.CreateInstance(type);
            foreach (var condition in list)
            {
                condition.Value.CopyRecursive(combinedObject);
            }
            return combinedObject;
        }

        /// <summary>
        /// recursively copies object properties to destination.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void CopyRecursive(this object source , object destination)
        {
            PropertyInfo[] sourceProperties = source.GetType().GetProperties();

            Type destType = destination.GetType();

            foreach (PropertyInfo prop in sourceProperties)
            {
                if (prop.PropertyType.FullName != null)
                    if (prop.PropertyType.IsClass && !(prop.PropertyType.FullName.IndexOf("System") >= 0))
                    {
                        object value = prop.GetValue(source, null);

                        var destProp = destType.GetProperty(prop.Name);

                        object destValue = destProp.GetValue(destination, null);

                        if (value != null)
                        {
                            try
                            {
                                if (destValue == null)
                                {
                                    destValue = Activator.CreateInstance(destProp.PropertyType);
                                }
                                // copy
                                value.CopyRecursive(destValue);

                                if (destProp.CanWrite)
                                {
                                    destProp.SetValue(destination, destValue, null);
                                }
                            }
                            catch(Exception ex)
                            {
                                #if DEBUG
                                    System.Diagnostics.Debug.WriteLine(ex.StackTrace);
                                #endif
                            }
                        }
                    }
                    else
                    {
                        bool isDefault = false;

                        object destValue = destType.GetProperty(prop.Name).GetValue(destination, null);

                        if (destValue != null)
                        {
                            object tempObject = Activator.CreateInstance(destType);
                            object tempValue = tempObject.GetType().GetProperty(prop.Name).GetValue(tempObject, null);
                            isDefault = tempValue.Equals(destValue);
                        }

                        if (destValue == null || isDefault)
                        {
                            destType.GetProperty(prop.Name).SetValue(destination, prop.GetValue(source, null), null);
                        }
                    }
            }
        }

        internal static IEnumerable<PropertyInfo> GetBaseProperties(this Type target)
        {
            foreach (PropertyInfo propertyInfo in target.GetProperties())
            {
                    yield return propertyInfo;
            }
        }

        internal static IQueryProvider GetQueryProvider<T>(this T objectBase) where T : IQueryProvider
        {
            string key = objectBase.GetType().FullName;

            if (!queryProviders.ContainsKey(key))
            {
                queryProviders.Add(key, objectBase);
            }
            return queryProviders[key] as IQueryProvider;
        }
    
        /// <summary>
        /// Invokes the specific method on the target
        /// </summary>
        /// <param name="methodName">Name of the method to invoke.</param>
        /// <param name="targetType">Type of the target</param>
        /// <param name="obj">Target object</param>
        /// <returns></returns>
        public static object InvokeMethod(string methodName, Type targetType, object obj)
        {
            MethodInfo[] mInfos = targetType.GetMethods();
            if (mInfos.Any(mInfo => string.Compare(methodName, mInfo.Name) == 0))
            {
                return targetType.InvokeMember(methodName, BindingFlags.InvokeMethod, null, obj, null);
            }
            return null;
        }

        internal static bool IsEqual(object obj1, object obj2)
        {
            bool equal = true;

            IEnumerable<PropertyInfo> infos = obj1.GetType().GetBaseProperties();

            foreach (PropertyInfo info in infos)
            {
                var queryAttribute = FindAttribute(typeof(IgnoreAttribute), info) as IgnoreAttribute;

                if (queryAttribute == null)
                {
                    object value1 = info.GetValue(obj1, null);
                    object value2 = info.GetValue(obj2, null);

                    if (Convert.ToString(value2) != Convert.ToString(value1))
                    {
                        equal = false;
                    }
                }
            }
            return equal;
        }


        internal static object FindAttribute(Type type, ICustomAttributeProvider info)
        {
            object[] arg = info.GetCustomAttributes(type, true);

            if (arg != null && arg.Length > 0)
            {
                return arg[0];
            }
            return null;
        }

        private static IDictionary<string, IQueryProvider> queryProviders = new Dictionary<string, IQueryProvider>();
        private static readonly IDictionary<string, IDictionary<string, object>> uniqueDefaultValueMap = new Dictionary<string, IDictionary<string, object>>();
    }
}
