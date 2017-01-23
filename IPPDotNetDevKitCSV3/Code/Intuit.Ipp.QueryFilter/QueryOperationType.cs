////********************************************************************
// <copyright file="QueryOperationType.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>Contains enumeration value for Query Operation Type.</summary>
////********************************************************************

namespace Intuit.Ipp.QueryFilter
{
    /// <summary>
    /// Contains enumeration value for Query Operation Type.
    /// </summary>
    public enum QueryOperationType
    {
        /// <summary>
        /// Entity query.
        /// </summary>
        query,
        
        /// <summary>
        /// Report query.
        /// </summary>
        report,

        /// <summary>
        /// Change Data query.
        /// </summary>
        changedata
    }
}
