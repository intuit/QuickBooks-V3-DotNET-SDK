////*********************************************************
// <copyright file="CustomSecurityElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Custom security element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System.Configuration;

    /// <summary>
    /// Custom security element.
    /// </summary>
    public class CustomSecurityElement : ConfigurationElement
    {
        /// <summary>
        /// Gets Name for the custom security mechanism.
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
        /// Gets Type for the custom security mechanism.
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
        /// Gets a value indicating whether this security mechanism is to be used.
        /// </summary>
        [ConfigurationProperty("enable")]
        public bool Enable
        {
            get
            {
                return (bool)this["enable"];
            }
        }

        /// <summary>
        /// Gets the parameters for this security mechanism.
        /// </summary>
        [ConfigurationProperty("params")]
        public string Params
        {
            get
            {
                return this["params"].ToString();
            }
        }
    }
}
