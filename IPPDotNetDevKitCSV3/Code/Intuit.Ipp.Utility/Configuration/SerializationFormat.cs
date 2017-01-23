////*********************************************************
// <copyright file="SerializationFormat.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Serialization format enumeration.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    /// <summary>
    /// Serialization format enumeration.
    /// </summary>
    public enum SerializationFormat
    {
        /// <summary>
        /// Default value used to indicate that compression is not specified in the config.
        /// </summary>
        DEFAULT,

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
