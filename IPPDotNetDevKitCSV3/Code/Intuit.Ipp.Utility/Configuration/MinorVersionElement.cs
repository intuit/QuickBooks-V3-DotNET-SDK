////*********************************************************
// <copyright file="MinorVersion.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains MinorVersion element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System.Configuration;

    /// <summary>
    /// MinorVersion
    /// </summary>
    public class MinorVersionElement : ConfigurationElement
    {

        /// <summary>
        /// Gets minorVersion for QuickBooks Online Rest Service.
        /// </summary>
        [ConfigurationProperty("qbo")]
        public string Qbo
        {
            get
            {
                return this["qbo"].ToString();
            }
        }
    }
}
