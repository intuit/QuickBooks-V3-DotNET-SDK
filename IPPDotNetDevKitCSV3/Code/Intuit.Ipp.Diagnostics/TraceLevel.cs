////*********************************************************
// <copyright file="TraceLevel.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file defines the trace levels..</summary>
////*********************************************************

namespace Intuit.Ipp.Diagnostics
{
    /// <summary>
    /// Specifies what level of messages to output.
    /// </summary>
    public enum TraceLevel
    {
        /// <summary>
        /// Output no tracing and debugging messages.
        /// </summary>
        Off = 0,
        
        /// <summary>
        /// Output error-handling messages.
        /// </summary>
        Error = 1,
        
        /// <summary>
        /// Output warnings and error-handling messages.
        /// </summary>
        Warning = 2,
        
        /// <summary>
        /// Output informational messages, warnings, and error-handling messages.
        /// </summary>
        Info = 3,
        
        /// <summary>
        /// Output all debugging and tracing messages.
        /// </summary>
        Verbose = 4,
    }
}
