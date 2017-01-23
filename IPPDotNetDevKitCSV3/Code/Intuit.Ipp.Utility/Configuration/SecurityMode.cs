////*********************************************************
// <copyright file="SecurityMode.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Security mode enumeration.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    /// <summary>
    /// Security mode enumeration.
    /// </summary>
    public enum SecurityMode
    {
        /// <summary>
        /// No Security mode.
        /// </summary>
        None,

        /// <summary>
        /// Open Authentication security mode.
        /// </summary>
        OAuth,


        /// <summary>
        /// Custom security mode.
        /// </summary>
        Custom
    }
}
