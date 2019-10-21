////*********************************************************
// <copyright file="IntuitConverter.cs" company="Intuit">
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
// <summary>This file contains Intuit Converter.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{


    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Newtonsoft.Json;
    using System.Reflection;
    using Newtonsoft.Json.Linq;
    //using Intuit.Ipp.Data;
    using System.Collections;
    using System.Diagnostics;

    /// <summary>
    /// JSON.Net extention for handling Json serialization/deserialization of POCO classes generated for Intuit XSD.
    /// </summary>
    public class IntuitConverter : JsonConverter
    {

        /// <summary>
        /// WriteJson
        /// </summary>  
        /// <param name="writer">json writer</param>
        /// <param name="value">object value</param>
        /// <param name="serializer">json serilaizer value</param>
        /// <returns>void</returns>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.GetType().BaseType == Type.GetType("System.Array"))
            {
                writer.WriteStartArray();
                foreach (var arr in value as Array)
                {
                    serializer.Serialize(writer, arr);
                }
                writer.WriteEndArray();
                return;
            }

            if (value.GetType().GetProperties().Count() > 0)
            {
                writer.WriteStartObject();
                foreach (PropertyInfo propertyInfo in value.GetType().GetProperties())
                {
                    if (propertyInfo.CanRead && ((propertyInfo.PropertyType.IsArray && propertyInfo.PropertyType.GetElementType() == Type.GetType("System.Object")) || (propertyInfo.PropertyType == Type.GetType("System.Object"))))
                    {
                        object val = propertyInfo.GetValue(value, null);
                        if (!(val == null && (serializer.NullValueHandling == NullValueHandling.Ignore)))
                        {
                            string propName = propertyInfo.Name + "Specified";
                            PropertyInfo specifiedProp = value.GetType().GetProperty(propName);
                            if (specifiedProp != null && ((bool)specifiedProp.GetValue(value, null) == true))
                            {
                                writer.WritePropertyName(val.GetType().Name);
                                serializer.Serialize(writer, propertyInfo.GetValue(value, null));
                            }
                            else if (specifiedProp == null)
                            {
                                System.Xml.Serialization.XmlChoiceIdentifierAttribute[] attrs = (System.Xml.Serialization.XmlChoiceIdentifierAttribute[])propertyInfo.GetCustomAttributes((new System.Xml.Serialization.XmlChoiceIdentifierAttribute()).GetType(), false);
                                if (attrs.Count() > 0)
                                {
                                    System.Xml.Serialization.XmlChoiceIdentifierAttribute attr = attrs[0];

                                    PropertyInfo choiceNameProp = value.GetType().GetProperty(attr.MemberName);
                                    if (choiceNameProp.PropertyType.IsArray)
                                    {   //AnyIntuitObjects
                                        var TypeofValue = choiceNameProp.GetValue(value, null);
                                        Array choiceNameArr = (TypeofValue as Array);
                                        Array valueArr = (propertyInfo.GetValue(value, null) as Array);
                                        for (int i = 0; i < choiceNameArr.Length; i++)
                                        {
                                            writer.WritePropertyName(choiceNameArr.GetValue(i).ToString());
                                            serializer.Serialize(writer, valueArr.GetValue(i));
                                        }
                                    }
                                    else
                                    {   //AnyIntuitObject
                                        var TypeofValue = choiceNameProp.GetValue(value, null);
                                        writer.WritePropertyName(TypeofValue.ToString());
                                        serializer.Serialize(writer, propertyInfo.GetValue(value, null));
                                    }
                                }
                                else
                                {
                                    Type valueType = null;

                                    if (val.GetType().IsArray)
                                    {
                                        if ((val as Array).Length > 0)
                                        {
                                            valueType = (val as Array).GetValue(0).GetType();
                                        }
                                    }
                                    else
                                    {
                                        valueType = val.GetType();
                                    }

                                    System.Xml.Serialization.XmlElementAttribute[] elementAttrs = (System.Xml.Serialization.XmlElementAttribute[])propertyInfo.GetCustomAttributes((new System.Xml.Serialization.XmlElementAttribute()).GetType(), false);
                                    foreach (System.Xml.Serialization.XmlElementAttribute attr in elementAttrs)
                                    {
                                        if (valueType.Name == attr.Type.Name)
                                        {
                                            writer.WritePropertyName(attr.ElementName);
                                            serializer.Serialize(writer, propertyInfo.GetValue(value, null));
                                        }
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        if ((propertyInfo.GetValue(value, null) != null) && (serializer.NullValueHandling == NullValueHandling.Ignore))
                        {
                            if (propertyInfo.GetCustomAttributes(false).Contains(new Newtonsoft.Json.JsonIgnoreAttribute()) != true)
                            {
                                if (propertyInfo.GetCustomAttributes(false).Contains(new System.Xml.Serialization.XmlTextAttribute()) && propertyInfo.Name == "Value")
                                {
                                    writer.WritePropertyName("value");
                                    writer.WriteValue(propertyInfo.GetValue(value, null).ToString());
                                }
                                else
                                {
                                    string propName = propertyInfo.Name + "Specified";
                                    PropertyInfo specifiedProp = value.GetType().GetProperty(propName);
                                    if (specifiedProp != null && (bool)specifiedProp.GetValue(value, null) == true)
                                    {
                                        writer.WritePropertyName(propertyInfo.Name);
                                        serializer.Serialize(writer, propertyInfo.GetValue(value, null));
                                    }
                                    else if (specifiedProp == null)
                                    {
                                        //check if this has choiceIdentifier put key name as its value else put name of property
                                        System.Xml.Serialization.XmlChoiceIdentifierAttribute[] choiceattrs = (System.Xml.Serialization.XmlChoiceIdentifierAttribute[])propertyInfo.GetCustomAttributes((new System.Xml.Serialization.XmlChoiceIdentifierAttribute()).GetType(), false);
                                        if (choiceattrs.Count() > 0)
                                        {
                                            foreach (
                                                System.Xml.Serialization.XmlChoiceIdentifierAttribute choiceAttr in
                                                    choiceattrs)
                                            {
                                                PropertyInfo choiceProp =
                                                    value.GetType().GetProperty(choiceAttr.MemberName);
                                                writer.WritePropertyName(choiceProp.GetValue(value, null).ToString());
                                            }
                                        }
                                        else
                                        {
                                            writer.WritePropertyName(propertyInfo.Name);
                                        }

                                        //check if its XmlArrayItem (NameValue)
                                        System.Xml.Serialization.XmlArrayItemAttribute[] arrayItemAttrs = (System.Xml.Serialization.XmlArrayItemAttribute[])propertyInfo.GetCustomAttributes((new System.Xml.Serialization.XmlArrayItemAttribute()).GetType(), false);
                                        if (arrayItemAttrs.Count() > 0)
                                        {
                                            writer.WriteStartObject();
                                            //if XmlArrayitem has name else data type will be the name
                                            if (!string.IsNullOrEmpty(arrayItemAttrs[0].ElementName))
                                                writer.WritePropertyName(arrayItemAttrs[0].ElementName);
                                            else
                                                writer.WritePropertyName(propertyInfo.GetValue(value, null).GetType().GetElementType().Name);

                                            serializer.Serialize(writer, propertyInfo.GetValue(value, null));
                                            writer.WriteEndObject();
                                        }
                                        else
                                        {
                                            serializer.Serialize(writer, propertyInfo.GetValue(value, null));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteValue(value);
            }
        }

        /// <summary>
        /// ReadJson
        /// </summary>  
        /// <param name="reader">json reader</param>
        /// <param name="objectType">objectType value</param>
        /// <param name="existingValue">existing value</param>
        /// <param name="serliazer">serliazer value</param>
        /// <returns>void</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            object target;
            if (objectType.IsAbstract)
            {
                target = GetInstanceofAbstractType(objectType, jsonObject);
            }
            else
            {
                target = Activator.CreateInstance(objectType);
            }

            foreach (JProperty prop in jsonObject.Properties())
            {
                PropertyInfo propExist = objectType.GetProperty(prop.Name);
                //test if property exist in object with node name
                if (propExist == null)
                {
                    //property with node name does not exist. Look for XMLAttribute definition with node name.
                    //iterate each property and find one with XmlElementAttribute having name as node name
                    foreach (PropertyInfo objectTypeProp in objectType.GetProperties())
                    {
                        //ReferenceType has a property with Value which is serialized as value (small v).
                        //This property is annotated with XmlTextAttribute
                        System.Xml.Serialization.XmlTextAttribute[] attrsText = (System.Xml.Serialization.XmlTextAttribute[])objectTypeProp.GetCustomAttributes((new System.Xml.Serialization.XmlTextAttribute()).GetType(), false);
                        foreach (System.Xml.Serialization.XmlTextAttribute attr in attrsText)
                        {
                            if (prop.Name == "value")
                            {
                                PropertyInfo targetProp = target.GetType().GetProperty(objectTypeProp.Name);
                                targetProp.SetValue(target, prop.Value.ToString(), null);
                            }

                        }

                        System.Xml.Serialization.XmlElementAttribute[] attrs = (System.Xml.Serialization.XmlElementAttribute[])objectTypeProp.GetCustomAttributes((new System.Xml.Serialization.XmlElementAttribute()).GetType(), false);
                        foreach (System.Xml.Serialization.XmlElementAttribute attr in attrs)
                        {
                            if (prop.Name == attr.ElementName || (prop.Name == "TaxService" && objectTypeProp.Name == "AnyIntuitObject"))
                            {
                                //check if this property is of type ChoiceElement
                                System.Xml.Serialization.XmlChoiceIdentifierAttribute[] choiceattrs = (System.Xml.Serialization.XmlChoiceIdentifierAttribute[])objectTypeProp.GetCustomAttributes((new System.Xml.Serialization.XmlChoiceIdentifierAttribute()).GetType(), false);
                                if (choiceattrs.Count() > 0)
                                {
                                    //get the property
                                    foreach (System.Xml.Serialization.XmlChoiceIdentifierAttribute choiceAttr in choiceattrs)
                                    {
                                        PropertyInfo choiceProp = target.GetType().GetProperty(choiceAttr.MemberName);
                                        if (choiceProp.PropertyType.IsArray)
                                        {
                                            PropertyInfo targetProp = target.GetType().GetProperty(objectTypeProp.Name);
                                            Array choiceArr = choiceProp.GetValue(target, null) as Array;
                                            Array objectArr = targetProp.GetValue(target, null) as Array;
                                            if (choiceArr == null)
                                            {
                                                choiceArr = Array.CreateInstance(choiceProp.PropertyType.GetElementType(), 1);
                                                objectArr = Array.CreateInstance(targetProp.PropertyType.GetElementType(), 1);
                                            }
                                            else
                                            {
                                                choiceArr = ResizeArray(choiceArr, choiceArr.Length + 1);
                                                objectArr = ResizeArray(objectArr, objectArr.Length + 1);
                                            }
                                            choiceArr.SetValue(Enum.Parse(choiceProp.PropertyType.GetElementType(), attr.ElementName), choiceArr.Length - 1);
                                            choiceProp.SetValue(target, choiceArr, null);

                                            int propertyValueCount = prop.Value.Count();
                                            int elementIndex = 0;
                                            Array elementArray = Array.CreateInstance(attr.Type, propertyValueCount);
                                            foreach (var ele in prop.Value)
                                            {
                                                if (ele.Type != JTokenType.Property)
                                                {
                                                    if (attr.Type.IsArray)
                                                    {
                                                        elementArray.SetValue(ele.ToObject(attr.Type.GetElementType(), serializer), elementIndex);
                                                    }
                                                    else
                                                    {
                                                        elementArray.SetValue(ele.ToObject(attr.Type, serializer), elementIndex);
                                                    }
                                                    elementIndex++;
                                                }
                                            }
                                            if (elementIndex == 0)
                                            {
                                                objectArr.SetValue(prop.Value.ToObject(attr.Type, serializer), objectArr.Length - 1);
                                            }
                                            else
                                            {
                                                objectArr.SetValue(elementArray, objectArr.Length - 1);
                                            }
                                            targetProp.SetValue(target, objectArr, null);
                                        }
                                        else
                                        {
                                            Type type = Assembly.Load("Intuit.Ipp.Data").GetTypes().Where(a => a.Name == objectTypeProp.PropertyType.Name).FirstOrDefault();
                                            AssignValueToProperty(target, prop, type, objectTypeProp.Name, serializer);

                                            PropertyInfo specifiedProp = target.GetType().GetProperty(choiceAttr.MemberName);
                                            if (specifiedProp != null)
                                            {
                                                specifiedProp.SetValue(target, Enum.Parse(choiceProp.PropertyType, prop.Name), null);
                                            }

                                        }
                                    }
                                }
                                else
                                {   // Added if and else if statement to fix SDK-304 bug ( https://jira.intuit.com/browse/SDK-304)
                                    // This bug is due to CheckPayment and CreditCardPayment being a type and ElementName at the same time.
                                    // Due to this the serializer converts the CheckPayment to type CheckPayment instead of BillCheckPayment. Same as with CreditCardPayment
                                    if (attr.ElementName == "CheckPayment" && objectType.Name == "BillPayment" && objectTypeProp.Name == "AnyIntuitObject")
                                    {
                                        Type type = Assembly.Load("Intuit.Ipp.Data").GetTypes().Where(a => a.Name == "BillPaymentCheck").FirstOrDefault();
                                        AssignValueToProperty(target, prop, type, objectTypeProp.Name, serializer);
                                    }
                                    else if (attr.ElementName == "CreditCardPayment" && objectType.Name == "BillPayment" && objectTypeProp.Name == "AnyIntuitObject")
                                    {
                                        Type type = Assembly.Load("Intuit.Ipp.Data").GetTypes().Where(a => a.Name == "BillPaymentCreditCard").FirstOrDefault();
                                        AssignValueToProperty(target, prop, type, objectTypeProp.Name, serializer);
                                    }

                                    else
                                    {
                                        //actual type in object which is annotated with name in XmlElementAttribute
                                        Type type = Assembly.Load("Intuit.Ipp.Data").GetTypes().Where(a => a.Name == prop.Name).FirstOrDefault();
                                        AssignValueToProperty(target, prop, type, objectTypeProp.Name, serializer);
                                    }
                                    
                                }
                            }

                        }


                    }
                }
                else
                {
                    // code to handle Unix timestamp returned as Integer in case of 401 error
                    if (prop.Name == "time")
                    {
                        if (prop.Value.Type == JTokenType.Integer)
                        {
                            long timeValue;
                            if (prop.Value.GetType() != typeof(long))
                                timeValue = long.Parse(prop.Value.ToString());
                            else
                                timeValue = (long)prop.Value;
                            prop.Value = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(timeValue);
                        }
                    }
                    //property with node name exist. Assign the value to property.
                    Type type = target.GetType().GetProperty(prop.Name).PropertyType;
                    Type typ1 = target.GetType().GetProperty(prop.Name).PropertyType.GetElementType();
                    AssignValueToProperty(target, prop, type, prop.Name, serializer);
                }
            }
            return target;
        }

        /// <summary>
        /// CanConvert
        /// </summary>  
        /// <param name="objectType">object Type value</param>
        /// <returns>void</returns>
        public override bool CanConvert(Type objectType)
        {
            return (objectType.Namespace == "Intuit.Ipp.Data");
        }

        /// <summary>
        /// AssignValueToProperty
        /// </summary>  
        /// <param name="target">target value</param>
        /// <param name="prop">prop value</param>
        /// <param name="type">type value</param>
        /// <param name="propName">propName value</param>
        /// <param name="serializer">json serilaizer value</param>
        /// <returns>void</returns>
        public void AssignValueToProperty(object target, JProperty prop, Type type, string propName, JsonSerializer serializer)
        {
            PropertyInfo targetProp = target.GetType().GetProperty(propName);
            if (!targetProp.PropertyType.IsArray)
            {
                if (prop.Value.Type.ToString().ToLower() != "null")
                {
                    targetProp.SetValue(target, prop.Value.ToObject(type, serializer), null);
                }
            }
            else
            {
                Array arr = Array.CreateInstance(targetProp.PropertyType.GetElementType(), prop.Value.Count());
                int arrCount = 0;
                foreach (var ele in prop.Value)
                {
                    if (ele.Type == JTokenType.Property)
                    {
                        foreach (var propInProperty in ele)
                        {
                            arr = Array.CreateInstance(targetProp.PropertyType.GetElementType(), propInProperty.Count());
                            foreach (var eleInProperty in propInProperty)
                            {
                                arr.SetValue(eleInProperty.ToObject(type.GetElementType(), serializer), arrCount);
                                arrCount++;
                            }
                        }
                    }
                    else
                    {
                        if (type.IsArray)
                        {
                            arr.SetValue(ele.ToObject(type.GetElementType(), serializer), arrCount);
                        }
                        else
                        {
                            arr.SetValue(ele.ToObject(type, serializer), arrCount);
                        }
                        arrCount++;
                    }
                }
                targetProp.SetValue(target, arr, null);
            }

            if (prop.Value.Type.ToString().ToLower() != "null")
            {
                //find specified field and set it true
                propName = targetProp.Name + "Specified";
                PropertyInfo specifiedProp = target.GetType().GetProperty(propName);
                if (specifiedProp != null)
                {
                    specifiedProp.SetValue(target, true, null);
                }
            }
        }

        /// <summary>
        /// ResizeArray
        /// </summary>  
        public static System.Array ResizeArray(System.Array oldArray, int newSize)
        {
            int oldSize = oldArray.Length;
            System.Type elementType = oldArray.GetType().GetElementType();
            System.Array newArray = System.Array.CreateInstance(elementType, newSize);

            int preserveLength = System.Math.Min(oldSize, newSize);

            if (preserveLength > 0)
                System.Array.Copy(oldArray, newArray, preserveLength);

            return newArray;
        }

        /// <summary>
        /// GetInstanceofAbstarctType
        /// </summary>  
        public object GetInstanceofAbstractType(Type objectType, JObject jsonObject)
        {
            foreach (JProperty prop in jsonObject.Properties())
            {
                PropertyInfo propExist = objectType.GetProperty(prop.Name);
                if (propExist != null)
                {
                    foreach (var obj in prop.Value)
                    {
                        string typeName = obj.Value<string>("Type");
                        if (!string.IsNullOrEmpty(typeName))
                        {
                            Type type = Assembly.Load("Intuit.Ipp.Data").GetTypes().Where(a => a.Name.Equals(typeName + objectType.Name)).FirstOrDefault();
                            return Activator.CreateInstance(type);
                        }

                    }
                }
            }
            return new Object();
        }
    }

}
