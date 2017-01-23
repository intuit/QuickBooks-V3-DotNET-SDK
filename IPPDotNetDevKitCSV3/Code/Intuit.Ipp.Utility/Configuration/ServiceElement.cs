////*********************************************************
// <copyright file="ServiceElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Service element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System.Configuration;

    /// <summary>
    /// Service element.
    /// </summary>
    public class ServiceElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the BaseUrl Element.
        /// </summary>
        [ConfigurationProperty("baseUrl")]
        public BaseUrlElement BaseUrl
        {
            get
            {
                return (BaseUrlElement)this["baseUrl"];
            }
        }

        /// <summary>
        /// Gets the MinorVersion Element.
        /// </summary>
        [ConfigurationProperty("minorVersion")]
        public MinorVersionElement MinorVersion
        {
            get
            {
                return (MinorVersionElement)this["minorVersion"];
            }
        }
    }
}
