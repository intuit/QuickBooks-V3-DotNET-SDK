////*********************************************************
// <copyright file="IConfigReader.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains interface for configuration reader.</summary>
////*********************************************************

namespace Intuit.Ipp.Core
{
    using Intuit.Ipp.Core.Configuration;

    /// <summary>
    /// Interface to read the configuration file and convert them to local custom objects.
    /// </summary>
    public interface IConfigReader
    {
        /// <summary>
        /// Reads the configuration from the config file and converts it to custom 
        /// config objects which the end developer will use to get or set the properties.
        /// </summary>
        /// <returns>The custom config object.</returns>
        IppConfiguration ReadConfiguration();
    }
}
