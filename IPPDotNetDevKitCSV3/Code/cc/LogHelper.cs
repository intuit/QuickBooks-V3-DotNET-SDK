////*********************************************************
// <copyright file="ILogger.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2016 Intuit
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/
// <summary>This file contains helper for Logging.</summary>
////*********************************************************
///
namespace Intuit.Ipp.OAuth2PlatformClient.Diagnostics
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
        public static OAuthAdvancedLogging GetAdvancedLoggingCustom(Serilog.ILogger customLogger)
        {
            OAuthAdvancedLogging advancedLogger;
            advancedLogger = new OAuthAdvancedLogging(customLogger);

            return advancedLogger;

        }

        /// <summary>
        /// Gets the Request Response Logging mechanism for advanced logging using serilog.
        /// </summary>
        /// <returns>Returns value which specifies the request response logging mechanism.</returns>
        public static OAuthAdvancedLogging GetAdvancedLogging(bool enableSerilogRequestResponseLoggingForDebug,bool enableSerilogRequestResponseLoggingForTrace, bool enableSerilogRequestResponseLoggingForConsole,bool enableSerilogRequestResponseLoggingForFile, string serviceRequestLoggingLocationForFile)
        {
            OAuthAdvancedLogging advancedLogger;
            advancedLogger = new OAuthAdvancedLogging(enableSerilogRequestResponseLoggingForDebug: enableSerilogRequestResponseLoggingForDebug, enableSerilogRequestResponseLoggingForTrace: enableSerilogRequestResponseLoggingForTrace, enableSerilogRequestResponseLoggingForConsole: enableSerilogRequestResponseLoggingForConsole, enableSerilogRequestResponseLoggingForFile: enableSerilogRequestResponseLoggingForFile, serviceRequestLoggingLocationForFile: serviceRequestLoggingLocationForFile);

            return advancedLogger;

        }
    }
}
