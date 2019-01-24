////*********************************************************
// <copyright file="LocalConfigReader.cs" company="Intuit">
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
// <summary>This file contains SdkException.</summary>
// <summary>This file contains Local Config Reader.</summary>
////*********************************************************

namespace Intuit.Ipp.Core.Configuration
{
    using System;
    using System.IO;
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Exception;
    //using Intuit.Ipp.Retry;  
    using Intuit.Ipp.Security;
    using Intuit.Ipp.Utility;
#if net472
#else
    using Microsoft.Extensions.Configuration;
#endif

    /// <summary>
    /// Specifies the Default Configuration Reader implmentation used by the SDK.
    /// </summary>
    public class LocalConfigReader : IConfigReader
    {
        /// <summary>
        /// Reads the configuration from the config file and converts it to custom 
        /// config objects which the end developer will use to get or set the properties.
        /// </summary>
        /// <returns>The custom config object.</returns>
        public IppConfiguration ReadConfiguration()
        {
            bool sectionExists;



            //#if net472
            //            IppConfigurationSection ippConfigurationSection = IppConfigurationSection.Instance;
            //            IppConfiguration ippConfig = new IppConfiguration();
            //            if (ippConfigurationSection == null)
            //            {
            //               sectionExists=false;
            //            }
            //            else
            //            {
            //                sectionExists=true;
            //            }
            //#else
            //            ConfigurationSection configurationSection = new ConfigurationSection(new ConfigurationRoot);
            //            var section = ConfigurationSection.GetSection("testsection");
            //            sectionExists = section.Exists();
            //#endif

            IppConfigurationSection ippConfigurationSection = IppConfigurationSection.Instance;
            IppConfiguration ippConfig = new IppConfiguration();


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
                            SerializationFormat = SerializationFormat.Xml
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
                    Value=null
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
                //case SecurityMode.OAuth:
                //    OAuthRequestValidator validator = new OAuthRequestValidator(
                //        ippConfigurationSection.Security.OAuth.AccessToken,
                //        ippConfigurationSection.Security.OAuth.AccessTokenSecret,
                //        ippConfigurationSection.Security.OAuth.ConsumerKey,
                //        ippConfigurationSection.Security.OAuth.ConsumerSecret);
                //    ippConfig.Security = validator;
                //    break;
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
                    ippConfig.Message.Request.SerializationFormat = SerializationFormat.Xml;
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



            return ippConfig;
        }
    }
}
