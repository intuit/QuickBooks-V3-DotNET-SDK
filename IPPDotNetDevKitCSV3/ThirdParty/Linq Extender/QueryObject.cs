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
using Intuit.Ipp.LinqExtender.Abstraction;
using Intuit.Ipp.LinqExtender.Attributes;

namespace Intuit.Ipp.LinqExtender
{

    /// <summary>
    /// Defines a query object.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class QueryObject
    {
        /// <summary>
        /// Contains the reference to the current query object.
        /// </summary>
        internal object ReferringObject { get; set; }
    }
    /// <summary>
    /// Query object implemenatation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public sealed class QueryObject<T> : QueryObject, IVersionItem, IQueryObjectImpl
    {
        /// <summary>
        /// Creates a new instance of the <see cref="QueryObject{T}"/> for its underlying object.
        /// </summary>
        /// <param name="baseObject"></param>
        public QueryObject(T baseObject)
        {
            this.baseObject = baseObject;
        }

        #region Tracking properties

        /// <summary>
        /// determines if an item is removed from collection.
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// deternmines if the object is altered , thus call UpdateItemFormat.
        /// </summary>
        [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        public bool IsAltered
        {
            get
            {
                try
                {
                    Type runningType = baseObject.GetType();
                    PropertyInfo[] infos = runningType.GetProperties();

                    object item = (this as IVersionItem).Item;

                    int index = 0;

                    foreach (PropertyInfo info in infos)
                    {
                        if (info.CanWrite)
                        {
                            object source = info.GetValue(baseObject, null);

                            PropertyInfo[] targetInfo = item.GetType().BaseType.GetProperties();

                            object target = index < targetInfo.Length ? targetInfo[index].GetValue(item, null) : null;

                            if (source == null)
                            {
                                 if (target != null)
                                     return true;
                            }
                            else if (!source.Equals(target))
                            {
                                return true;
                            }
                        }
                        index++;
                    }
                }
                catch
                {
                    return false;
                }
                return false;
            }
        }
        /// <summary>
        /// determines if an item is newly added in the collection.
        /// </summary>

        public bool IsNewlyAdded
        {
            get
            {
                // Loads the uniqueKey mapping from extention method.
                IDictionary<string, object> uniqueDefaultValues = (ReferringObject as IQueryObject).GetUniqueItemDefaultDetail();

                foreach (string key in uniqueDefaultValues.Keys)
                {
                    PropertyInfo[] infos = typeof(T).GetProperties();
                    // create a hollow anonymous type and cast with result.
                    var item = Utility.Cast(uniqueDefaultValues[key], new { Index = 0, Value = default(object) });

                    object obj = infos[item.Index].GetValue(baseObject, null);
                
                    if (obj != null)
                    {
                        // the property is not nullable, check for the default value.
                        isNew = obj.Equals(item.Value);
                    }
                }
                return isNew;
            }
        }

     
        #endregion
        /// <summary>
        /// Gets/Sets the underlying ref object for the query object implementation.
        /// </summary>
        public new T ReferringObject
        {
            get { return (T)base.ReferringObject; }
            set
            {
                base.ReferringObject = value;
            }
        }
      
        #region IDisposable Members

        /// <summary>
        /// Disposes the query object.
        /// </summary>
        public void Dispose()
        {
            ReferringObject = default(T);
        }

 
        #endregion

  
        #region IVersionItem Members

        /// <summary>
        /// updates the cached object with update object
        /// </summary>
        void IVersionItem.Commit()
        {
            //First we create an instance of this specific type.
            CopyObjectTo(ReferringObject, baseObject);
        }
        /// <summary>
        /// converts the current object to cachedObject.
        /// </summary>
        void IVersionItem.Revert()
        {
            CopyObjectTo(ReferringObject, baseObject);    
        }

        /// <summary>
        /// Copies the source object to one or more destinaton object.
        /// </summary>
        /// <param name="sourceObject">single object to be copied.</param>
        /// <param name="targets">array of objects</param>
        private static void CopyObjectTo(object sourceObject , params object[] targets)
        {
            Type sourceType = sourceObject.GetType();

            IEnumerable<PropertyInfo> infos = sourceType.GetBaseProperties();

            foreach (PropertyInfo info in infos)
            {
                if (info.CanRead && info.CanWrite)
                {
                    foreach (object target in targets)
                    {
                        info.SetValue(target, info.GetValue(sourceObject, null), null);
                    }
                }
            }
        }

        object IVersionItem.Item
        {
            get
            {
                return ReferringObject;
            }
        }

        #endregion


        #region Bucket Fill ups
        /// <summary>
        /// Takes bucket reference and fills it up with new values.
        /// </summary>
        /// <param name="bucket"></param>
        /// <returns></returns>
        [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        public Bucket FillBucket(Bucket bucket)
        {
            IEnumerable<PropertyInfo> infos = baseObject.GetType().GetProperties();
        
            foreach (PropertyInfo info in infos)
            {
                object[] arg = info.GetCustomAttributes(typeof(IgnoreAttribute), true);

                if (arg.Length == 0)
                {
                    if (info.CanRead)
                    {
                        try
                        {
                            object oldValue = info.GetValue(baseObject, null);
                            object value = (ReferringObject != null) ? info.GetValue(ReferringObject, null) : null;

                            if (value != null)
                            {
                                if (!value.Equals(oldValue))
                                    bucket.Items[info.Name].IsModified = true;

                                if (bucket.Items[info.Name].PropertyType == info.PropertyType)
                                {
                                    bucket.Items[info.Name].Values.Clear();
                                    bucket.Items[info.Name].Values.Add(new BucketItem.QueryCondition(value, BinaryOperator.Equal));
                                }
                            }
                        }
                        catch
                        {
                            // skip, failed to parse, this happens for some null reference properties.
                        }
                    }
                }
            }
            return bucket;
        }

        /// <summary>
        /// Fill value for a property name.
        /// </summary>
        /// <param name="name">Name of the property, accepts original property or Modified by OriginalFieldNameAttribute</param>
        /// <param name="value">the value of the property , retrived from property get accessor.</param>
        /// <param name="returnType">Return type of the underlying property.</param>
        public void FillProperty(string name, object value, Type returnType)
        {
            PropertyInfo info = ReferringObject.GetType().GetProperty(name, returnType);

            if (info.CanWrite)
            {
                info.SetValue(ReferringObject, value, null);
            }
        }
        /// <summary>
        /// Fills object from its underlying bucket.
        /// </summary>
        /// <param name="source"></param>
        [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
        public void FillObject(Bucket source)
        {
            foreach (string property in source.Items.Keys)
            {
                BucketItem item = source.Items[property];
                // first make sure it is not turned off by user.
                if (item.Visible)
                {
                    // people can set only once condition from Query<T>.SelectItemFormat
                    // check if the propety has some value, if so then proceed.
                    if (item.Values.Count > 0 && item.Values[0].Changed)
                    {
                        // change the item.
                        FillProperty(property, item.Value, item.PropertyType);
                    }
                }
            }
        }
        #endregion        

        private bool isNew = true;
        private readonly T baseObject;
    }
}
