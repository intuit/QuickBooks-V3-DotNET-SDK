////*********************************************************
// <copyright file="JsonFileConfigurationProvider.cs" company="Intuit">
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
// <summary>This file contains Json file config Reader.</summary>
////*********************************************************



namespace Intuit.Ipp.Core.Configuration
{
    using System;
    using System.Globalization;
    using System.IO;
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Exception;
    //using Intuit.Ipp.Retry;
    using Intuit.Ipp.Security;
    using Intuit.Ipp.Utility;

    using System.Configuration;
#if NETSTANDARD2_0
    using Microsoft.Extensions.Configuration;
#endif



    /// <summary>
    /// Specifies the Default Json file configuration provider implementation used by the SDK.
    /// By default reads "appsettings.json" file.
    /// </summary>
    public class JsonFileConfigurationProvider : Core.IConfigurationProvider
    {

#if NETSTANDARD2_0
        public string logPath { get; set; }
        IConfigurationRoot builder;
        public JsonFileConfigurationProvider(string path)
        {
            builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path, optional: true)
                .Build();
           


        }
        public JsonFileConfigurationProvider() : this("appsettings.json")
        {
        }


#endif

        /// <summary>
        /// Reads the configuration from the config file and converts it to custom
        /// config objects which the end developer will use to get or set the properties.
        /// </summary>
        /// <returns>The custom config object.</returns>
        public IppConfiguration GetConfiguration()
        {


            IppConfiguration ippConfig = new IppConfiguration();

#if !NETSTANDARD2_0
            //initializing the Advanced Serilogger
            IppConfigurationSection ippConfigurationSection = IppConfigurationSection.Instance;
            
            ippConfig.AdvancedLogger = new AdvancedLogger
            {
             
                RequestAdvancedLog = new RequestAdvancedLog()
                {
                    EnableSerilogRequestResponseLoggingForDebug = false,
                    EnableSerilogRequestResponseLoggingForTrace = false,
                    EnableSerilogRequestResponseLoggingForConsole = false,
                    EnableSerilogRequestResponseLoggingForRollingFile = false,
                   // EnableSerilogRequestResponseLoggingForAzureDocumentDB = false,
                    ServiceRequestLoggingLocationForFile = System.IO.Path.GetTempPath()
                }
            };

            if (ippConfigurationSection == null)
            {



                ippConfig.Logger = new Logger
                {
                    CustomLogger = new TraceLogger(),
                    RequestLog = new RequestLog
                    {
                        EnableRequestResponseLogging = false,
                        ServiceRequestLoggingLocation = System.IO.Path.GetTempPath()
                    }
                };

                ippConfig.Message = new Message
                {
                    Request = new Request
                    {
                        CompressionFormat = CompressionFormat.GZip,
                        SerializationFormat = SerializationFormat.Xml//do not change this as it will break code for devs
                    },
                    Response = new Response
                    {
                        CompressionFormat = CompressionFormat.GZip,
                        SerializationFormat = SerializationFormat.Json
                    }
                };

                ippConfig.BaseUrl = new BaseUrl
                {

                    Qbo = null,
                    Ips = null,
                    OAuthAccessTokenUrl = null,
                    UserNameAuthentication = null
                };

                ippConfig.MinorVersion = new MinorVersion
                {

                    Qbo = null
                };

                ippConfig.VerifierToken = new VerifierToken
                {
                    Value = null
                };

                return ippConfig;
            }

            ippConfig.Logger = new Logger();
            ippConfig.Logger.RequestLog = new RequestLog();
            ippConfig.Logger.RequestLog.EnableRequestResponseLogging = ippConfigurationSection.Logger.RequestLog.EnableRequestResponseLogging;
            if (string.IsNullOrEmpty(ippConfigurationSection.Logger.RequestLog.RequestResponseLoggingDirectory))
            {
                ippConfig.Logger.RequestLog.ServiceRequestLoggingLocation = Path.GetTempPath();
            }
            else
            {
                string location = ippConfigurationSection.Logger.RequestLog.RequestResponseLoggingDirectory;
                if (!Directory.Exists(location))
                {
                    IdsException exception = new IdsException(Properties.Resources.ValidDirectoryPathMessage, new DirectoryNotFoundException());
                    IdsExceptionManager.HandleException(exception);
                }

                ippConfig.Logger.RequestLog.ServiceRequestLoggingLocation = ippConfigurationSection.Logger.RequestLog.RequestResponseLoggingDirectory;
            }

            if (!string.IsNullOrEmpty(ippConfigurationSection.Logger.CustomLogger.Name) && !string.IsNullOrEmpty(ippConfigurationSection.Logger.CustomLogger.Type) && ippConfigurationSection.Logger.CustomLogger.Enable)
            {
                Type customerLoggerType = Type.GetType(ippConfigurationSection.Logger.CustomLogger.Type);
                ippConfig.Logger.CustomLogger = Activator.CreateInstance(customerLoggerType) as ILogger;
            }
            else
            {
                ippConfig.Logger.CustomLogger = new TraceLogger();
            }

            switch (ippConfigurationSection.Security.Mode)
            {
                case SecurityMode.OAuth:
                    OAuth2RequestValidator validator = new OAuth2RequestValidator(
                        ippConfigurationSection.Security.OAuth.AccessToken);
                    ippConfig.Security = validator;
                    break;
                case SecurityMode.Custom:
                    if (!string.IsNullOrEmpty(ippConfigurationSection.Security.CustomSecurity.Name) && !string.IsNullOrEmpty(ippConfigurationSection.Security.CustomSecurity.Type) && ippConfigurationSection.Security.CustomSecurity.Enable)
                    {
                        Type customSecurityType = Type.GetType(ippConfigurationSection.Security.CustomSecurity.Type);
                        string[] paramateres = ippConfigurationSection.Security.CustomSecurity.Params.Split(',');
                        ippConfig.Security = Activator.CreateInstance(customSecurityType, paramateres) as IRequestValidator;
                    }

                    break;
            }

            //// TODO : This will not be used now.
            ////if (!string.IsNullOrEmpty(ippConfigurationSection.Message.CustomSerializer.Name) && !string.IsNullOrEmpty(ippConfigurationSection.Message.CustomSerializer.Type) && ippConfigurationSection.Message.CustomSerializer.Enable)
            ////{
            ////    Type customSerializerType = Type.GetType(ippConfigurationSection.Message.CustomSerializer.Type);
            ////    IEntitySerializer entitySerializer = Activator.CreateInstance(customSerializerType) as IEntitySerializer;
            ////    if (ippConfigurationSection.Message.Request.SerializationFormat == SerializationFormat.Custom)
            ////    {
            ////        ippConfig.Message.Request.Serializer = entitySerializer;
            ////    }

            ////    if (ippConfigurationSection.Message.Response.SerializationFormat == SerializationFormat.Custom)
            ////    {

            ////        ippConfig.Message.Response.Serializer = entitySerializer;
            ////    }
            ////}

            ippConfig.Message = new Message();
            ippConfig.Message.Request = new Request();
            ippConfig.Message.Response = new Response();

            switch (ippConfigurationSection.Message.Request.CompressionFormat)
            {
                case Intuit.Ipp.Utility.CompressionFormat.None:
                    ippConfig.Message.Request.CompressionFormat = CompressionFormat.None;
                    break;
                case Intuit.Ipp.Utility.CompressionFormat.DEFAULT:
                case Intuit.Ipp.Utility.CompressionFormat.GZip:
                    ippConfig.Message.Request.CompressionFormat = CompressionFormat.GZip;
                    break;
                case Intuit.Ipp.Utility.CompressionFormat.Deflate:
                    ippConfig.Message.Request.CompressionFormat = CompressionFormat.Deflate;
                    break;
                default:
                    break;
            }



            switch (ippConfigurationSection.Message.Response.CompressionFormat)
            {
                case Intuit.Ipp.Utility.CompressionFormat.None:
                    ippConfig.Message.Response.CompressionFormat = CompressionFormat.None;
                    break;
                case Intuit.Ipp.Utility.CompressionFormat.DEFAULT:
                case Intuit.Ipp.Utility.CompressionFormat.GZip:
                    ippConfig.Message.Response.CompressionFormat = CompressionFormat.GZip;
                    break;
                case Intuit.Ipp.Utility.CompressionFormat.Deflate:
                    ippConfig.Message.Response.CompressionFormat = CompressionFormat.Deflate;
                    break;
            }

            switch (ippConfigurationSection.Message.Request.SerializationFormat)
            {
                case Intuit.Ipp.Utility.SerializationFormat.DEFAULT:
                case Intuit.Ipp.Utility.SerializationFormat.Xml:
                    ippConfig.Message.Request.SerializationFormat = SerializationFormat.Xml;//do not change this as it will break code for devs
                    break;
                case Intuit.Ipp.Utility.SerializationFormat.Json:
                    ippConfig.Message.Request.SerializationFormat = SerializationFormat.Json;
                    break;
                case Intuit.Ipp.Utility.SerializationFormat.Custom:
                    ippConfig.Message.Request.SerializationFormat = SerializationFormat.Custom;
                    break;
            }

            switch (ippConfigurationSection.Message.Response.SerializationFormat)
            {
                case Intuit.Ipp.Utility.SerializationFormat.Xml:
                    ippConfig.Message.Response.SerializationFormat = SerializationFormat.Xml;
                    break;
                case Intuit.Ipp.Utility.SerializationFormat.DEFAULT:
                case Intuit.Ipp.Utility.SerializationFormat.Json:
                    ippConfig.Message.Response.SerializationFormat = SerializationFormat.Json;
                    break;
                case Intuit.Ipp.Utility.SerializationFormat.Custom:
                    ippConfig.Message.Response.SerializationFormat = SerializationFormat.Custom;
                    break;
            }


            switch (ippConfigurationSection.Retry.Mode)
            {
                case RetryMode.Linear:
                    if (!CoreHelper.IsInvalidaLinearRetryMode(
                        ippConfigurationSection.Retry.LinearRetry.RetryCount,
                        ippConfigurationSection.Retry.LinearRetry.RetryInterval))
                    {
                        ippConfig.RetryPolicy = new IntuitRetryPolicy(
                            ippConfigurationSection.Retry.LinearRetry.RetryCount,
                            ippConfigurationSection.Retry.LinearRetry.RetryInterval);
                    }

                    break;
                case RetryMode.Incremental:
                    if (!CoreHelper.IsInvalidaIncrementalRetryMode(
                        ippConfigurationSection.Retry.IncrementatlRetry.RetryCount,
                        ippConfigurationSection.Retry.IncrementatlRetry.InitialInterval,
                        ippConfigurationSection.Retry.IncrementatlRetry.Increment))
                    {
                        ippConfig.RetryPolicy = new IntuitRetryPolicy(
                            ippConfigurationSection.Retry.IncrementatlRetry.RetryCount,
                            ippConfigurationSection.Retry.IncrementatlRetry.InitialInterval,
                            ippConfigurationSection.Retry.IncrementatlRetry.Increment);
                    }

                    break;
                case RetryMode.Exponential:
                    if (!CoreHelper.IsInvalidaExponentialRetryMode(
                        ippConfigurationSection.Retry.ExponentialRetry.RetryCount,
                            ippConfigurationSection.Retry.ExponentialRetry.MinBackoff,
                            ippConfigurationSection.Retry.ExponentialRetry.MaxBackoff,
                            ippConfigurationSection.Retry.ExponentialRetry.DeltaBackoff))
                    {
                        ippConfig.RetryPolicy = new IntuitRetryPolicy(
                            ippConfigurationSection.Retry.ExponentialRetry.RetryCount,
                            ippConfigurationSection.Retry.ExponentialRetry.MinBackoff,
                            ippConfigurationSection.Retry.ExponentialRetry.MaxBackoff,
                            ippConfigurationSection.Retry.ExponentialRetry.DeltaBackoff);
                    }

                    break;
            }

            ippConfig.BaseUrl = new BaseUrl();

            ippConfig.BaseUrl.Qbo = ippConfigurationSection.Service.BaseUrl.Qbo;
            ippConfig.BaseUrl.Ips = ippConfigurationSection.Service.BaseUrl.Ips;
            ippConfig.BaseUrl.OAuthAccessTokenUrl = ippConfigurationSection.Service.BaseUrl.OAuthAccessTokenUrl;
            ippConfig.BaseUrl.UserNameAuthentication = ippConfigurationSection.Service.BaseUrl.UserNameAuthentication;

            ippConfig.MinorVersion = new MinorVersion();

            ippConfig.MinorVersion.Qbo = ippConfigurationSection.Service.MinorVersion.Qbo;



            ippConfig.VerifierToken = new VerifierToken();
            ippConfig.VerifierToken.Value = ippConfigurationSection.WebhooksService.WebhooksVerifier.Value;

#endif
#if NETSTANDARD2_0

            //Setting defaults for configurations
            #region defaults

            ippConfig.AdvancedLogger = new AdvancedLogger
            {
             
                RequestAdvancedLog = new RequestAdvancedLog()
                {
                    EnableSerilogRequestResponseLoggingForDebug = false,
                    EnableSerilogRequestResponseLoggingForTrace = false,
                    EnableSerilogRequestResponseLoggingForConsole = false,
                    EnableSerilogRequestResponseLoggingForRollingFile = false,
                   // EnableSerilogRequestResponseLoggingForAzureDocumentDB = false,
                    ServiceRequestLoggingLocationForFile = System.IO.Path.GetTempPath()
                }
            };


            ippConfig.Logger = new Logger
            {
                CustomLogger = new TraceLogger(),
                RequestLog = new RequestLog
                {
                    EnableRequestResponseLogging = false,
                    ServiceRequestLoggingLocation = System.IO.Path.GetTempPath()
                }
            };

            ippConfig.Message = new Message
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

            ippConfig.BaseUrl = new BaseUrl
            {

                Qbo = null,
                Ips = null,
                OAuthAccessTokenUrl = null,
                UserNameAuthentication = null
            };

            ippConfig.MinorVersion = new MinorVersion
            {

                Qbo = null
            };

            ippConfig.VerifierToken = new VerifierToken
            {
                Value = null
            };


            #endregion

         

            //Read all appsettings.json sections
            var loggerSettings = builder.GetSection("Logger").GetSection("RequestLog");
            var customLoggerSettings = builder.GetSection("CustomLogger").GetSection("RequestLog");
            var securitySettings = builder.GetSection("Security").GetSection("Mode");
            var securityOauthSettings = builder.GetSection("Security").GetSection("Mode").GetSection("OAuth");
            var securityCustomSettings = builder.GetSection("Security").GetSection("Mode").GetSection("Custom");
            var messageRequestSettings = builder.GetSection("Message").GetSection("Request");
            var messageResponseSettings = builder.GetSection("Message").GetSection("Response");
            var retrySettings = builder.GetSection("Retry").GetSection("Mode");
            var retrySettingsLinear = builder.GetSection("Retry").GetSection("Mode").GetSection("LinearRetry");
            var retrySettingsIncremental = builder.GetSection("Retry").GetSection("Mode").GetSection("IncrementalRetry");
            var retrySettingsExponential = builder.GetSection("Retry").GetSection("Mode").GetSection("ExponentialRetry");
            var serviceSettings = builder.GetSection("Service");
            var serviceBaseUrlSettings = builder.GetSection("Service").GetSection("BaseUrl");
            var serviceMinorversionSettings = builder.GetSection("Service").GetSection("Minorversion");
            var webhooksVerifierTokenSettings = builder.GetSection("WebhooksService").GetSection("VerifierToken");
            var serilogLoggerSettings = builder.GetSection("SeriLogger").GetSection("RequestSerilog");
            var serilogLoggerSettingsDebug = builder.GetSection("SeriLogger").GetSection("RequestSerilog").GetSection("Debug");
            var serilogLoggerSettingsConsole = builder.GetSection("SeriLogger").GetSection("RequestSerilog").GetSection("Console");
            var serilogLoggerSettingsRollingFile = builder.GetSection("SeriLogger").GetSection("RequestSerilog").GetSection("RollingFile");
            //var serilogLoggerSettingsFile= builder.GetSection("SeriLogger").GetSection("RequestSerilog").GetSection("File");
            //var serilogLoggerSettingsAzureDocumentDB = builder.GetSection("SeriLogger").GetSection("RequestSerilog").GetSection("AzureDocumentDB");


            if (!string.IsNullOrEmpty(serilogLoggerSettingsRollingFile["LogDirectory"]) && Convert.ToBoolean(serilogLoggerSettingsRollingFile["EnableLogs"]) == true)
            {
             


                ippConfig.AdvancedLogger.RequestAdvancedLog.EnableSerilogRequestResponseLoggingForRollingFile = Convert.ToBoolean(serilogLoggerSettingsRollingFile["EnableLogs"]);


                string location = loggerSettings["LogDirectory"];
                if (!Directory.Exists(location))
                {
                    IdsException exception = new IdsException(Properties.Resources.ValidDirectoryPathMessage, new DirectoryNotFoundException());
                    IdsExceptionManager.HandleException(exception);
                }

                ippConfig.AdvancedLogger.RequestAdvancedLog.ServiceRequestLoggingLocationForFile = serilogLoggerSettingsRollingFile["LogDirectory"];

            }

            #region support later

            //if (!string.IsNullOrEmpty(serilogLoggerSettingsAzureDocumentDB["LogDirectory"]) && Convert.ToBoolean(serilogLoggerSettingsAzureDocumentDB["EnableLogs"]) == true)
            //{


            //    ippConfig.AdvancedLogger.RequestAdvancedLog.EnableSerilogRequestResponseLoggingForAzureDocumentDB = Convert.ToBoolean(serilogLoggerSettingsAzureDocumentDB["EnableLogs"]);


            //    string url = serilogLoggerSettingsAzureDocumentDB["Url"];
            //    Uri parsedUrl = new Uri(url);
            //    string secureKey = serilogLoggerSettingsAzureDocumentDB["SecureKey"];

            //    if (string.IsNullOrEmpty(url))
            //    {
            //        IdsException exception = new IdsException(Properties.Resources.AzureDocumentDBSecureKeyNullEmptyMessage, new ArgumentNullException());
            //        IdsExceptionManager.HandleException(exception);
            //    }
            //    if (string.IsNullOrEmpty(secureKey))
            //    {
            //        IdsException exception = new IdsException(Properties.Resources.AzureDocumentDBSecureKeyNullEmptyMessage, new ArgumentNullException());
            //        IdsExceptionManager.HandleException(exception);
            //    }


            //    if (string.IsNullOrEmpty(serilogLoggerSettingsAzureDocumentDB["TimeToLive"]))
            //    {
            //         ippConfig.AdvancedLogger.RequestAdvancedLog.ServiceRequestAzureDocumentDBTTL = 7;//Defaulted to last 7 days logs
            //    }
            //    else
            // {
            //         ippConfig.AdvancedLogger.RequestAdvancedLog.ServiceRequestAzureDocumentDBTTL = Convert.ToInt32(serilogLoggerSettingsAzureDocumentDB["TimeToLive"]);
            // }

            //    ippConfig.AdvancedLogger.RequestAdvancedLog.ServiceRequestAzureDocumentDBUrl = parsedUrl;
            //     ippConfig.AdvancedLogger.RequestAdvancedLog.ServiceRequestAzureDocumentDBSecureKey = secureKey;


            //}

            #endregion

            if (!string.IsNullOrEmpty(loggerSettings["LogDirectory"]) && Convert.ToBoolean(loggerSettings["EnableLogs"]) == true)
            {

                ippConfig.Logger.RequestLog.EnableRequestResponseLogging = Convert.ToBoolean(loggerSettings["EnableLogs"]);


                string location = loggerSettings["LogDirectory"];
                if (!Directory.Exists(location))
                {
                    IdsException exception = new IdsException(Properties.Resources.ValidDirectoryPathMessage, new DirectoryNotFoundException());
                    IdsExceptionManager.HandleException(exception);
                }

                ippConfig.Logger.RequestLog.ServiceRequestLoggingLocation = loggerSettings["LogDirectory"];

            }

            if (!string.IsNullOrEmpty(customLoggerSettings["Name"]) && !string.IsNullOrEmpty(customLoggerSettings["Type"]) && Convert.ToBoolean(customLoggerSettings["Enable"]) == true)
            {
                Type customerLoggerType = Type.GetType(customLoggerSettings["Type"]);
                ippConfig.Logger.CustomLogger = Activator.CreateInstance(customerLoggerType) as ILogger;
            }
            else
            {
                ippConfig.Logger.CustomLogger = new TraceLogger();
            }



            if (!string.IsNullOrEmpty(securityOauthSettings["AccessToken"]) && Convert.ToBoolean(securityOauthSettings["Enable"]) == true)
            {
                OAuth2RequestValidator validator = new OAuth2RequestValidator(
                       securityOauthSettings["AccessToken"]);
                ippConfig.Security = validator;
            }
            else if (securityCustomSettings["Enable"] == "true")
            {
                if (!string.IsNullOrEmpty(securityCustomSettings["Name"]) && !string.IsNullOrEmpty(securityCustomSettings["Type"]))
                {
                    Type customSecurityType = Type.GetType(securityCustomSettings["Type"]);
                    if (!string.IsNullOrEmpty(securityCustomSettings["Params"]))
                    {
                        string[] paramateres = securityCustomSettings["Params"].Split(',');
                        ippConfig.Security = Activator.CreateInstance(customSecurityType, paramateres) as IRequestValidator;
                    }
                }

            }

            ippConfig.Message = new Message();
            ippConfig.Message.Request = new Request();
            ippConfig.Message.Response = new Response();

            if (!string.IsNullOrEmpty(messageRequestSettings["CompressionFormat"]))
            {
                switch (Enum.Parse(typeof(Utility.CompressionFormat), messageRequestSettings["CompressionFormat"]))
                {
                    case Intuit.Ipp.Utility.CompressionFormat.None:
                        ippConfig.Message.Request.CompressionFormat = CompressionFormat.None;
                        break;
                    case Intuit.Ipp.Utility.CompressionFormat.DEFAULT:
                    case Intuit.Ipp.Utility.CompressionFormat.GZip:
                        ippConfig.Message.Request.CompressionFormat = CompressionFormat.GZip;
                        break;
                    case Intuit.Ipp.Utility.CompressionFormat.Deflate:
                        ippConfig.Message.Request.CompressionFormat = CompressionFormat.Deflate;
                        break;
                    default:
                        break;
                }
            }


            if (!string.IsNullOrEmpty(messageResponseSettings["CompressionFormat"]))
            {
                switch (Enum.Parse(typeof(Utility.CompressionFormat), messageResponseSettings["CompressionFormat"]))
                {
                    case Intuit.Ipp.Utility.CompressionFormat.None:
                        ippConfig.Message.Response.CompressionFormat = CompressionFormat.None;
                        break;
                    case Intuit.Ipp.Utility.CompressionFormat.DEFAULT:
                    case Intuit.Ipp.Utility.CompressionFormat.GZip:
                        ippConfig.Message.Response.CompressionFormat = CompressionFormat.GZip;
                        break;
                    case Intuit.Ipp.Utility.CompressionFormat.Deflate:
                        ippConfig.Message.Response.CompressionFormat = CompressionFormat.Deflate;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(messageRequestSettings["SerializationFormat"]))
            {
                switch (Enum.Parse(typeof(Utility.SerializationFormat), messageRequestSettings["SerializationFormat"]))
                {
                    case Intuit.Ipp.Utility.SerializationFormat.DEFAULT:
                    case Intuit.Ipp.Utility.SerializationFormat.Xml:
                        ippConfig.Message.Request.SerializationFormat = SerializationFormat.Xml;
                        break;
                    case Intuit.Ipp.Utility.SerializationFormat.Json:
                        ippConfig.Message.Request.SerializationFormat = SerializationFormat.Json;
                        break;
                    case Intuit.Ipp.Utility.SerializationFormat.Custom:
                        ippConfig.Message.Request.SerializationFormat = SerializationFormat.Custom;
                        break;
                }
            }


            if (!string.IsNullOrEmpty(messageResponseSettings["SerializationFormat"]))
            {
                switch (Enum.Parse(typeof(Utility.SerializationFormat), messageResponseSettings["SerializationFormat"]))
                {
                    case Intuit.Ipp.Utility.SerializationFormat.Xml:
                        ippConfig.Message.Response.SerializationFormat = SerializationFormat.Xml;
                        break;
                    case Intuit.Ipp.Utility.SerializationFormat.DEFAULT:
                    case Intuit.Ipp.Utility.SerializationFormat.Json:
                        ippConfig.Message.Response.SerializationFormat = SerializationFormat.Json;
                        break;
                    case Intuit.Ipp.Utility.SerializationFormat.Custom:
                        ippConfig.Message.Response.SerializationFormat = SerializationFormat.Custom;
                        break;
                }
            }


            if ((!string.IsNullOrEmpty(retrySettingsLinear["Enable"])) && Convert.ToBoolean(retrySettingsLinear["Enable"]) == true)
            {
                if (!string.IsNullOrEmpty(retrySettingsLinear["RetryCount"]) && !string.IsNullOrEmpty(retrySettingsLinear["RetryInterval"]))
                {
                    if (!CoreHelper.IsInvalidaLinearRetryMode(
                                                  Convert.ToInt32(retrySettingsLinear["RetryCount"]),
                                                   TimeSpan.Parse(retrySettingsLinear["RetryInterval"])))
                    {
                        ippConfig.RetryPolicy = new IntuitRetryPolicy(
                            Convert.ToInt32(retrySettingsLinear["RetryCount"]),
                             TimeSpan.Parse(retrySettingsLinear["RetryInterval"]));
                    }
                }
            }
            else if ((!string.IsNullOrEmpty(retrySettingsIncremental["Enable"])) && Convert.ToBoolean(retrySettingsIncremental["Enable"]) == true)
            {
                if (!string.IsNullOrEmpty(retrySettingsIncremental["RetryCount"]) && !string.IsNullOrEmpty(retrySettingsIncremental["InitialInterval"]) && !string.IsNullOrEmpty(retrySettingsIncremental["Increment"]))
                {
                    if (!CoreHelper.IsInvalidaIncrementalRetryMode(
                         Convert.ToInt32(retrySettingsIncremental["RetryCount"]),
                         TimeSpan.Parse(retrySettingsIncremental["InitialInterval"]),
                         TimeSpan.Parse(retrySettingsIncremental["Increment"])))
                    {
                        ippConfig.RetryPolicy = new IntuitRetryPolicy(
                            Convert.ToInt32(retrySettingsIncremental["RetryCount"]),
                            TimeSpan.Parse(retrySettingsIncremental["InitialInterval"]),
                            TimeSpan.Parse(retrySettingsIncremental["Increment"]));
                    }
                }
            }
            else if ((!string.IsNullOrEmpty(retrySettingsExponential["Enable"])) && Convert.ToBoolean(retrySettingsExponential["Enable"]) == true)
            {
                if (!string.IsNullOrEmpty(retrySettingsExponential["RetryCount"]) && !string.IsNullOrEmpty(retrySettingsExponential["MinBackoff"]) && !string.IsNullOrEmpty(retrySettingsExponential["MaxBackoff"]) && !string.IsNullOrEmpty(retrySettingsExponential["DeltaBackoff"]))
                {

                    if (!CoreHelper.IsInvalidaExponentialRetryMode(
                          Convert.ToInt32(retrySettingsExponential["RetryCount"]),
                          TimeSpan.Parse(retrySettingsExponential["MinBackoff"]),
                          TimeSpan.Parse(retrySettingsExponential["MaxBackoff"]),
                          TimeSpan.Parse(retrySettingsExponential["DeltaBackoff"])))
                    {
                        ippConfig.RetryPolicy = new IntuitRetryPolicy(
                            Convert.ToInt32(retrySettingsExponential["RetryCount"]),
                            TimeSpan.Parse(retrySettingsExponential["MinBackoff"]),
                            TimeSpan.Parse(retrySettingsExponential["MaxBackoff"]),
                            TimeSpan.Parse(retrySettingsExponential["DeltaBackoff"]));
                    }
                }
            }


            //ippConfig.BaseUrl = new BaseUrl();

            ippConfig.BaseUrl.Qbo = serviceBaseUrlSettings["Qbo"];
            ippConfig.BaseUrl.Ips = serviceBaseUrlSettings["Ips"];
            ippConfig.BaseUrl.OAuthAccessTokenUrl = serviceBaseUrlSettings["OAuthAccessTokenUrl"];
            ippConfig.BaseUrl.UserNameAuthentication = serviceBaseUrlSettings["UserNameAuthentication"];

            //ippConfig.MinorVersion = new MinorVersion();

            ippConfig.MinorVersion.Qbo = serviceMinorversionSettings["Qbo"];



            //ippConfig.VerifierToken = new VerifierToken();
            ippConfig.VerifierToken.Value = webhooksVerifierTokenSettings["Value"];

#endif
           

            return ippConfig;

        }

    }
}

