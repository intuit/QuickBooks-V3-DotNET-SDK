////*********************************************************
// <copyright file="CoreHelper.cs" company="Intuit">
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
// <summary>This file contains Helper methods.</summary>
////*********************************************************

namespace Intuit.Ipp.Core
{
    using System;
    using System.Text;
    using System.Xml;
    //using Intuit.Ipp.Core.Compression; 
    using Intuit.Ipp.Core.Properties;
    using Intuit.Ipp.Core.Rest;
    using Intuit.Ipp.Exception;
    using Intuit.Ipp.Utility;
   

    /// <summary>
    /// Helper class.
    /// </summary>
    public static class CoreHelper
    { 


        /// <summary>
        /// Gets or sets Serilog Request Logging.
        /// </summary>
        internal static AdvancedLogging AdvancedLogging;
    
        /// <summary>
        /// Gets the serializer mechanism using the service context and the depending on the request and response.
        /// </summary>
        /// <param name="serviceContext">The service context object.</param>
        /// <param name="isRequest">Specifies whether to return serializer mechanism for reqeust or response.</param>
        /// <returns>The Serializer mechanism.</returns>
        public static IEntitySerializer GetSerializer(ServiceContext serviceContext, bool isRequest)
        {
            IEntitySerializer serializer = null;
            if (isRequest)
            {
                switch (serviceContext.IppConfiguration.Message.Request.SerializationFormat)
                {
                    case Configuration.SerializationFormat.Xml:
                        serializer = new XmlObjectSerializer();
                        break;
                    case Configuration.SerializationFormat.Json:
                        serializer = new JsonObjectSerializer();
                        break;
                    case Configuration.SerializationFormat.Custom:
                        // TODO: check whtether this is possible
                        // this.serializer = this.serviceContext.IppConfiguration.Message.Request.CustomSerializer;
                        break;
                }
            }
            else
            {
                switch (serviceContext.IppConfiguration.Message.Response.SerializationFormat)
                {
                    case Configuration.SerializationFormat.Xml:
                        serializer = new XmlObjectSerializer();
                        break;
                    case Configuration.SerializationFormat.Json:
                        serializer = new JsonObjectSerializer();
                        break;
                    case Configuration.SerializationFormat.Custom:
                        // TODO: check whtether this is possible
                        // this.serializer = this.serviceContext.IppConfiguration.Message.Request.CustomSerializer;
                        break;
                }
            }

            return serializer;
        }

        /// <summary>
        /// Parses the response string to an XmlDocument object.
        /// </summary>
        /// <param name="response">The response string.</param>
        /// <returns>The XmlDocument object.</returns>
        public static XmlDocument ParseResponseIntoXml(string response)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            XmlDocument respXML = new XmlDocument();
            byte[] responseBytes = encoder.GetBytes(response);
            string quickBaseResponse = EncodingFixer.FixQuickBaseEncoding(responseBytes);
            respXML.LoadXml(quickBaseResponse);
            return respXML;
        }

        /// <summary>
        /// Checks whether the reponse is null or empty and throws communication exception.
        /// </summary>
        /// <param name="response">The response from the query service.</param>
        public static void CheckNullResponseAndThrowException(string response)
        {
            if (string.IsNullOrWhiteSpace(response))
            {
                IdsException exception = new IdsException(Resources.CommunicationErrorMessage, new CommunicationException(Resources.ResponseStreamNullOrEmptyMessage));
                throw exception;
            }
        }

        /// <summary>
        /// Gets the compression mechanism using the service context and the depending on the request and response.
        /// </summary>
        /// <param name="serviceContext">The service context object.</param>
        /// <param name="isRequest">Specifies whether to return compression mechanism for reqeust or response.</param>
        /// <returns>The Compression mechanism.</returns>
        public static ICompressor GetCompressor(ServiceContext serviceContext, bool isRequest)
        {
            ICompressor compressor = null;
            if (isRequest)
            {
                switch (serviceContext.IppConfiguration.Message.Request.CompressionFormat)
                {
                    case Configuration.CompressionFormat.GZip:
                        compressor = new GZipCompressor();
                        break;
                    case Configuration.CompressionFormat.Deflate:
                        compressor = new DeflateCompressor();
                        break;
                }
            }
            else
            {
                switch (serviceContext.IppConfiguration.Message.Response.CompressionFormat)
                {
                    case Configuration.CompressionFormat.GZip:
                        compressor = new GZipCompressor();
                        break;
                    case Configuration.CompressionFormat.Deflate:
                        compressor = new DeflateCompressor();
                        break;
                }
            }

            return compressor;
        }

        /// <summary>
        /// Gets the Request Response Logging mechanism.
        /// </summary>
        /// <param name="serviceContext">The serivce context object.</param>
        /// <returns>Returns value which specifies the request response logging mechanism.</returns>
        public static Rest.LogRequestsToDisk GetRequestLogging(ServiceContext serviceContext)
        {
            Rest.LogRequestsToDisk requestLogger;
            if (serviceContext.IppConfiguration != null &&
                serviceContext.IppConfiguration.Logger != null &&
                serviceContext.IppConfiguration.Logger.RequestLog != null)
            {
                requestLogger = new Rest.LogRequestsToDisk(
                    serviceContext.IppConfiguration.Logger.RequestLog.EnableRequestResponseLogging,
                    serviceContext.IppConfiguration.Logger.RequestLog.ServiceRequestLoggingLocation);
            }
            else
            {
                requestLogger = new Rest.LogRequestsToDisk(false, null);
            }

            return requestLogger;
        }


        /// <summary>
        /// Gets the Request Response Logging mechanism for advanced logging using serilog.
        /// </summary>
        /// <param name="serviceContext">The serivce context object.</param>
        /// <returns>Returns value which specifies the request response logging mechanism.</returns>
        public static Rest.AdvancedLogging GetAdvancedLogging(ServiceContext serviceContext)
        {
            Rest.AdvancedLogging requestLogger;
            if (serviceContext.IppConfiguration != null &&
                serviceContext.IppConfiguration.AdvancedLogger != null &&
                serviceContext.IppConfiguration.AdvancedLogger.RequestAdvancedLog != null)
            {
                requestLogger = new Rest.AdvancedLogging(
                    serviceContext.IppConfiguration.AdvancedLogger.RequestAdvancedLog.EnableSerilogRequestResponseLoggingForDebug,
                    serviceContext.IppConfiguration.AdvancedLogger.RequestAdvancedLog.EnableSerilogRequestResponseLoggingForTrace,
                    serviceContext.IppConfiguration.AdvancedLogger.RequestAdvancedLog.EnableSerilogRequestResponseLoggingForConsole,
                    serviceContext.IppConfiguration.AdvancedLogger.RequestAdvancedLog.EnableSerilogRequestResponseLoggingForRollingFile,
                    serviceContext.IppConfiguration.AdvancedLogger.RequestAdvancedLog.ServiceRequestLoggingLocationForFile);
              

            }
            else
            {
                requestLogger = new Rest.AdvancedLogging(enableSerilogRequestResponseLoggingForDebug: true, enableSerilogRequestResponseLoggingForTrace: true, enableSerilogRequestResponseLoggingForConsole: true, enableSerilogRequestResponseLoggingForRollingFile: false, serviceRequestLoggingLocationForFile: null);
            }

            return requestLogger;
        }



        /// <summary>
        /// Checks whether the retry count is less than or equal to zero.
        /// Checks whether the timespan values are zero.
        /// </summary>
        /// <param name="retryCount">Ther retry count.</param>
        /// <param name="minBackoff">The Min interval value.</param>
        /// <param name="maxBackoff">The Max value.</param>
        /// <param name="deltaBackoff">The Delta backoff value.</param>
        /// <returns>Returns true if parameter values are correct else false.</returns>
        internal static bool IsInvalidaExponentialRetryMode(int retryCount, TimeSpan minBackoff, TimeSpan maxBackoff, TimeSpan deltaBackoff)
        {
            bool invalid = false;
            if (!IsInvalidaIncrementalRetryMode(retryCount, minBackoff, maxBackoff))
            {
                if (deltaBackoff == TimeSpan.Zero)
                {
                    invalid = true;
                }
            }

            return invalid;
        }

        /// <summary>
        /// Checks whether the retry count is less than or equal to zero.
        /// Checks whether the timespan values are zero.
        /// </summary>
        /// <param name="retryCount">Ther retry count.</param>
        /// <param name="initialInterval">The initial interval value.</param>
        /// <param name="increment">The increment value.</param>
        /// <returns>Returns true if parameter values are correct else false.</returns>
        internal static bool IsInvalidaIncrementalRetryMode(int retryCount, TimeSpan initialInterval, TimeSpan increment)
        {
            bool invalid = false;
            if (!IsInvalidaLinearRetryMode(retryCount, initialInterval))
            {
                if (increment == TimeSpan.Zero)
                {
                    invalid = true;
                }
            }

            return invalid;
        }

        /// <summary>
        /// Checks whether the retry count is less than or equal to zero.
        /// Checks whether the timespan value is zero.
        /// </summary>
        /// <param name="retryCount">Ther retry count.</param>
        /// <param name="interval">The interval value.</param>
        /// <returns>Returns true if parameter values are correct else false.</returns>
        internal static bool IsInvalidaLinearRetryMode(int retryCount, TimeSpan interval)
        {
            bool invalid = false;
            if (interval == TimeSpan.Zero)
            {
                invalid = true;
            }
            else
            {
                if (retryCount <= 0)
                {
                    invalid = true;
                }
            }

            return invalid;
        }
    }
}
