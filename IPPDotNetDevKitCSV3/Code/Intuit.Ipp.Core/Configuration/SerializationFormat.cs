////*********************************************************
// <copyright file="SerializationFormat.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains serialization format enumeration.</summary>
////*********************************************************

namespace Intuit.Ipp.Core.Configuration
{
    /// <summary>
    /// Serialization format enumeration.
    /// </summary>
    public enum SerializationFormat
    {
        /// <summary>
        /// Xml Serialization Format.
        /// </summary>
        Xml,

        /// <summary>
        /// Java Script Obejct Notation Serialization Format.
        /// </summary>
        Json,

        /// <summary>
        /// Custom serialization format.
        /// </summary>
        Custom
    }
}
