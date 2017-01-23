////*********************************************************
// <copyright file="CustomLoggerElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains custom logger element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System.Configuration;

    /// <summary>
    /// Custom logger element.
    /// </summary>
    public class CustomLoggerElement : ConfigurationElement
    {
        /// <summary>
        /// Gets Name for the custom logging mechanism.
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
        /// Gets Name Type of custom logging mechanism.
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
        /// Gets a value indicating whether this logging mechanism is to be used.
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
