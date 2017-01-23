////*********************************************************
// <copyright file="ObjectToEnumConvertor.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains JSON serialization convertor for enums.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Reflection;
    using Newtonsoft.Json;

    /// <summary>
    ///  This convertor is fired to correctly serialize enum field for JSON using XmlEnumAttribute. 
    /// </summary>
    public class ObjectToEnumConverter : JsonConverter
    {
        /// <summary>
        /// This method is used for serialization of enum field.
        /// </summary>
        /// <param name="writer"> json writer.</param>
        /// <param name="value"> Type of object being serialized. </param>
        /// <param name="serializer"> json serializer. </param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            FieldInfo[] fields = value.GetType().GetFields();
            foreach (var field in fields)
            {
                if (String.Compare(field.Name, value.ToString(),StringComparison.Ordinal) == 0)
                {
                    object[] attr = field.GetCustomAttributes(typeof(System.Xml.Serialization.XmlEnumAttribute), false);
                    if (attr.Length > 0)
                    {
                        System.Xml.Serialization.XmlEnumAttribute xmlAttribute = (System.Xml.Serialization.XmlEnumAttribute)attr[0];
                        serializer.Serialize(writer, xmlAttribute.Name);
                        return;
                    }

                    serializer.Serialize(writer, field.Name);
                    return;
                }
            }
        }

        /// <summary>
        /// This method is used for deserialization of enum field.
        /// </summary>
        /// <param name="reader"> json reader object. </param>
        /// <param name="objectType"> Type of object being serialized. </param>
        /// <param name="existingValue"> existing value. </param>
        /// <param name="serializer"> json serializer. </param>
        /// <returns> returns an object </returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            FieldInfo[] fields = objectType.GetFields();
            foreach (var field in fields)
            {
                object[] attr = field.GetCustomAttributes(typeof(System.Xml.Serialization.XmlEnumAttribute), false);
                if (attr.Length > 0)
                {
                    foreach (var attrItem in attr)
                    {
                        if (attrItem.GetType() == typeof(System.Xml.Serialization.XmlEnumAttribute))
                        {
                            System.Xml.Serialization.XmlEnumAttribute xmlAttribute = (System.Xml.Serialization.XmlEnumAttribute)attr[0];
                            if (String.Compare(xmlAttribute.Name, reader.Value.ToString(),StringComparison.Ordinal) == 0)
                            {
                                return Enum.Parse(objectType, field.Name);
                            }
                        }
                    }
                }
            }

            return Enum.Parse(objectType, reader.Value.ToString());
        }

        /// <summary>
        /// This method is used to check if it is of enum type.
        /// </summary>
        /// <param name="objectType"> Type of object being serialized. </param>
        /// <returns> retrun true or false. </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType.BaseType.Name == "Enum";
        }
    }
}
