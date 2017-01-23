////*********************************************************
// <copyright file="CustomSerializerElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Custom Serializer element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System.Configuration;

    /// <summary>
    /// Custom serializer element.
    /// </summary>
    public class CustomSerializerElement : ConfigurationElement
    {
        /// <summary>
        /// Gets Name for the custom serialization mechanism.
        /// </summary>
        [ConfigurationProperty("name")]
        public string Name
        {
            get
            {
                return this["name"].ToString();
            }
        }

        /// <summary>
        /// Gets Type for the custom serialization mechanism.
        /// </summary>
        [ConfigurationProperty("type")]
        public string Type
        {
            get
            {
                return this["type"].ToString();
            }
        }

        /// <summary>
        /// Gets a value indicating whether this serialization mechanism is to be used.
        /// </summary>
        [ConfigurationProperty("enable")]
        public bool Enable
        {
            get
            {
                return (bool)this["enable"];
            }
        }
    }
}
