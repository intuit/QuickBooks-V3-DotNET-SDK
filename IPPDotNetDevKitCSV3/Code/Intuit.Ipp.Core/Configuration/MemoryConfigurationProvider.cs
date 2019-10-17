////*********************************************************
// <copyright file="MemoryConfigurationProvider.cs" company="Intuit">
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
//
// <summary>This file contains config provider, that returns user IppConfiguration.</summary>
////*********************************************************



namespace Intuit.Ipp.Core.Configuration
{
    using Intuit.Ipp.Diagnostics;


    /// <summary>
    /// Specifies the configuration provider, that returns user provided <see cref="IppConfiguration"/>
    /// </summary>
    public class MemoryConfigurationProvider : IConfigurationProvider
    {
        private readonly IppConfiguration configuration;

        /// <summary>
        /// Check given new <see cref="IppConfiguration"/> and fill some properties if they isn't setted.
        /// </summary>
        /// <param name="cfg"></param>
        public MemoryConfigurationProvider(IppConfiguration cfg)
        {
            if (cfg.Logger == null)
            {
                cfg.Logger = new Logger
                {
                    CustomLogger = new TraceLogger(),
                    RequestLog = new RequestLog
                    {
                        EnableRequestResponseLogging = false,
                        ServiceRequestLoggingLocation = System.IO.Path.GetTempPath()
                    }
                };
            }

            if (cfg.AdvancedLogger == null)
            {
                cfg.AdvancedLogger = new AdvancedLogger
                {
          
                    RequestAdvancedLog = new RequestAdvancedLog()
                    {
                        EnableSerilogRequestResponseLoggingForDebug = false,
                        EnableSerilogRequestResponseLoggingForTrace = false,
                        EnableSerilogRequestResponseLoggingForConsole = false,
                        EnableSerilogRequestResponseLoggingForRollingFile = false,
                        EnableSerilogRequestResponseLoggingForAzureDocumentDB = false,
                        ServiceRequestLoggingLocationForFile = System.IO.Path.GetTempPath()
                    }
                };
            }

            if (cfg.Message == null)
            {
                cfg.Message = new Message
                {
                    Request = new Request
                    {
                        CompressionFormat = CompressionFormat.GZip,
                        SerializationFormat = SerializationFormat.Json
                    },
                    Response = new Response
                    {
                        CompressionFormat = CompressionFormat.GZip,
                        SerializationFormat = SerializationFormat.Json
                    }
                };
            }
            if (cfg.BaseUrl == null)
            {
                cfg.BaseUrl = new BaseUrl()
;
            }
            if (cfg.MinorVersion == null)
            {
                cfg.MinorVersion = new MinorVersion();
            }
;
            if (cfg.VerifierToken == null)
            {
                cfg.VerifierToken = new VerifierToken();
            }
            configuration = cfg;
        }

        /// <summary>
        /// Returns given configuration
        /// </summary>
        public IppConfiguration GetConfiguration() => configuration;

    }
}

