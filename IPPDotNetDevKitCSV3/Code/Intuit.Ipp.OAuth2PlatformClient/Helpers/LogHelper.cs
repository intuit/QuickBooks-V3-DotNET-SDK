using System;
using System.Collections.Generic;
using System.Text;



namespace Intuit.Ipp.OAuth2PlatformClient
{ 
    /// <summary>
    /// Helper class for advanced logger/serilogger
    /// </summary>
    public static class LogHelper
    {
        

        /// <summary>
        /// Gets the Request Response Logging mechanism for advanced logging using serilog.
        /// </summary>
        /// <returns>Returns value which specifies the request response logging mechanism.</returns>
        public static OAuthAdvancedLogging GetAdvancedLogging(bool enableSerilogRequestResponseLoggingForDebug,bool enableSerilogRequestResponseLoggingForTrace, bool enableSerilogRequestResponseLoggingForConsole, bool enableSerilogRequestResponseLoggingForRollingFile,string serviceRequestLoggingLocationForFile)
        {
            OAuthAdvancedLogging advancedLogger;
            advancedLogger = new OAuthAdvancedLogging(enableSerilogRequestResponseLoggingForDebug: enableSerilogRequestResponseLoggingForDebug, enableSerilogRequestResponseLoggingForTrace: enableSerilogRequestResponseLoggingForTrace, enableSerilogRequestResponseLoggingForConsole: enableSerilogRequestResponseLoggingForConsole, enableSerilogRequestResponseLoggingForRollingFile: enableSerilogRequestResponseLoggingForRollingFile, serviceRequestLoggingLocationForFile: serviceRequestLoggingLocationForFile);

            return advancedLogger;

        }
    }
}
