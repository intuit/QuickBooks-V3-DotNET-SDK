////*********************************************************
// <copyright file="ILogger.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains an interface for Logging.</summary>
////*********************************************************

namespace Intuit.Ipp.Diagnostics
{
    /// <summary>
    /// Interface used to log messages. 
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs messages depending on the ids trace level.
        /// </summary>
        /// <param name="idsTraceLevel">IDS Trace Level.</param>
        /// <param name="messageToWrite">The message to write.</param>
        void Log(TraceLevel idsTraceLevel, string messageToWrite);
    }
}
