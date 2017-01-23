////********************************************************************
// <copyright file="HttpVerbType.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Http Verb Types.</summary>
////********************************************************************

namespace Intuit.Ipp.Core
{
    /// <summary>
    /// Enumeration for the different values that can be used as value for HttpWebRequest method property. 
    /// </summary>
    public enum HttpVerbType
    {
        /// <summary>
        /// Http Get verb.
        /// </summary>
        GET,

        /// <summary>
        /// Http Put verb.
        /// </summary>
        PUT,

        /// <summary>
        /// Http Delete verb.
        /// </summary>
        DELETE,

        /// <summary>
        /// Http Post verb.
        /// </summary>
        POST
    }
}
