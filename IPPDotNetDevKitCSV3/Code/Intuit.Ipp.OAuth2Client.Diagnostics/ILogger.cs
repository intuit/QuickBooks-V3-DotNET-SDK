using System;
using System.Web;

namespace Intuit.Ipp.OAuth2PlatformClient.Diagnostics
{
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
