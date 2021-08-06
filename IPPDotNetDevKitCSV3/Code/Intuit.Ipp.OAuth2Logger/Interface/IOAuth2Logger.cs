using Intuit.Ipp.OAuth2Logger.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intuit.Ipp.OAuth2Logger.Interface
{
   public interface IOAuth2Logger
    {
        /// <summary>
        /// Logs messages depending on the ids trace level.
        /// </summary>
        /// <param name="idsTraceLevel">IDS Trace Level.</param>
        /// <param name="messageToWrite">The message to write.</param>
        void Log(OAuth2TraceLevel idsTraceLevel, string messageToWrite);
    }
}
