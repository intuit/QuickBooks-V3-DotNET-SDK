////********************************************************************
// <copyright file="RestHandler.cs" company="Intuit">
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
// <summary>This file contains logic for REST Handler.</summary>
////********************************************************************

using System.Diagnostics;

namespace Intuit.Ipp.Core.Rest
{
    using System;
    using System.Net;
    using System.Text;
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Utility;
    using System.IO;
    using System.Collections.Generic;

    /// <summary>
    /// Rest Handler class.
    /// </summary>
    /// <seealso cref="RestHandler"/>
    public abstract class RestHandler : IRestHandler
    {
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        private ServiceContext serviceContext;

        /// <summary>
        /// Response serializer.
        /// </summary>
        private IEntitySerializer responseSerializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestHandler"/> class.
        /// </summary>
        /// <param name="context">The Service Context.</param>
        protected RestHandler(ServiceContext context)
            : this()
        {
            this.serviceContext = context;
            this.RequestCompressor = CoreHelper.GetCompressor(this.serviceContext, true);
            this.ResponseCompressor = CoreHelper.GetCompressor(this.serviceContext, false);
            this.RequestSerializer = CoreHelper.GetSerializer(this.serviceContext, true);
            this.responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);
            this.RequestLogging = CoreHelper.GetRequestLogging(this.serviceContext);
           // this.AdvancedLogging =  CoreHelper.GetAdvancedLogging(this.serviceContext);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestHandler"/> class.
        /// </summary>
        protected RestHandler()
        {
        }

        /// <summary>
        /// Gets a value indicating whether to write to request stream or not.
        /// For async requests GetRequestStream is an async operation.
        /// </summary>
        internal abstract bool IsSyncRequestStream { get; }

        /// <summary>
        /// Gets or sets Request compressor.
        /// </summary>
        internal ICompressor RequestCompressor { get; set; }

        /// <summary>
        /// Gets or sets Response compressor.
        /// </summary>
        internal ICompressor ResponseCompressor { get; set; }

        /// <summary>
        /// Gets or sets Request serializer.
        /// </summary>
        internal IEntitySerializer RequestSerializer { get; set; }

        /// <summary>
        /// Gets or sets Request Logging.
        /// </summary>
        internal LogRequestsToDisk RequestLogging { get; set; }

        ///// <summary>
        ///// Gets or sets Serilog Request Logging.
        ///// </summary>
        //internal static AdvancedLogging AdvancedLogging { get; set; }

        /// <summary>
        /// Gets or sets the minorVersion.
        /// </summary>
        internal string MinorVersion { get; set; }

        /// <summary>
        /// Gets or sets the Include param.
        /// </summary>
        internal List<String> Include { get; set; }

        /// <summary>
        /// Gets or sets the requestId param.
        /// </summary>
        internal string RequestId { get; set; }

        /// <summary>
        /// Prepares the HttpWebRequest along with authentication header added to the request.
        /// </summary>
        /// <param name="requestParameters">The parameters.</param>
        /// <param name="requestBody">The request entity.</param>
        /// <param name="oauthRequestUri">The OAuth request uri.</param>
        /// <returns>
        /// Http web request object.
        /// </returns>
        public virtual System.Net.HttpWebRequest PrepareRequest(RequestParameters requestParameters, object requestBody, string oauthRequestUri, bool includeRequestId = true)
        {
            //initialize the Advanced logger
            CoreHelper.AdvancedLogging = CoreHelper.GetAdvancedLogging(this.serviceContext);
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, "Called PrepareRequest method");

            // This step is required since the configuration settings might have been changed.
            this.RequestCompressor = CoreHelper.GetCompressor(this.serviceContext, true);
            this.ResponseCompressor = CoreHelper.GetCompressor(this.serviceContext, false);
            this.RequestSerializer = CoreHelper.GetSerializer(this.serviceContext, true);
            this.responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);

            this.Include = this.serviceContext.Include;
            this.MinorVersion = this.serviceContext.MinorVersion;
            this.RequestId = this.serviceContext.RequestId;

            string requestEndpoint;
            // Prepare the request Uri from base Uri and resource Uri.
            if (oauthRequestUri == null)
            {
                requestEndpoint = this.serviceContext.BaseUrl + StripFirstSlash(requestParameters.ResourceUri);
            }
            else
            {
                requestEndpoint = oauthRequestUri;
            }

            if (this.Include != null && this.Include.Count > 0)
            {
                requestEndpoint = this.AppendQueryParameters(requestEndpoint, "include", string.Join(",", this.Include.ToArray()));
            }

            requestEndpoint = !String.IsNullOrWhiteSpace(this.MinorVersion) ? this.AppendQueryParameters(requestEndpoint, "minorversion", this.MinorVersion) : this.AppendQueryParameters(requestEndpoint, "minorversion", Properties.Resources.DefaultMinorVersionValue);

            if (!String.IsNullOrWhiteSpace(this.RequestId))
            {
                if (requestBody != null && requestBody is Intuit.Ipp.Data.IntuitBatchRequest)
                {
                    //The maximum request ID + batch ID length is 50 characters.
                    //For backwards compatibility, truncate the request ID if necessary.
                    Intuit.Ipp.Data.IntuitBatchRequest batchRequest = (Intuit.Ipp.Data.IntuitBatchRequest)requestBody;
                    int maximumBatchIdLength = 0;
                    for (int batchIndex = 0; batchIndex < batchRequest.BatchItemRequest.Length; batchIndex++)
                    {
                        maximumBatchIdLength = Math.Max(maximumBatchIdLength, batchRequest.BatchItemRequest[batchIndex].bId.Length);
                    }
                    if (maximumBatchIdLength < 50)
                    {
                        this.RequestId = this.RequestId.Substring(0, (int)Math.Min(this.RequestId.Length, 50 - maximumBatchIdLength));
                    }
                }

                if (includeRequestId && !String.IsNullOrWhiteSpace(this.RequestId))
                {
                    requestEndpoint = requestEndpoint.Contains("?") ? requestEndpoint += "&" : requestEndpoint += "?";
                    requestEndpoint += "requestid=";
                    requestEndpoint += this.RequestId;
                }
            }

            Uri requestUri = new Uri(requestEndpoint);

            // Create the HttpWebRequest using the requestUri created above.
            HttpWebRequest httpWebRequest = WebRequest.Create(requestUri) as HttpWebRequest;

            // Set the Method 
            httpWebRequest.Method = requestParameters.Verb.ToString();

            // Set the content type
            httpWebRequest.ContentType = requestParameters.ContentType;
            
            // Set the accept header type to JSON.
            if (this.responseSerializer is JsonObjectSerializer)
            {
                httpWebRequest.Accept = CoreConstants.CONTENTTYPE_APPLICATIONJSON;
            }

            // If the service type is IPS then set the Api name in header. 
            if (this.serviceContext.ServiceType == IntuitServicesType.IPS && (oauthRequestUri == null))
            {
                // Add the API name as header to the request.
                httpWebRequest.Headers.Add(CoreConstants.APIACTIONHEADER, requestParameters.ApiName);
            }

            if (this.RequestCompressor != null)
            {
                httpWebRequest.Headers.Add(CoreConstants.CONTENTENCODING, this.RequestCompressor.DataCompressionFormat.ToString().ToLowerInvariant());
            }

            if (this.ResponseCompressor != null)
            {
                httpWebRequest.Headers.Add(CoreConstants.ACCEPTENCODING, this.ResponseCompressor.DataCompressionFormat.ToString().ToLowerInvariant());
            }

            // This indicates whether a sync call or an async call is to be made. For an async call
            // the GetRequestStream is an async call so do not call it here.
            if (this.IsSyncRequestStream)
            {
                // When the Verb is POST, we need to serialize the request xml to body.
                if (requestParameters.Verb == HttpVerbType.POST)
                {
                    byte[] content = null;

                    MemoryStream streamRequestBody = requestBody as MemoryStream;
                    StringBuilder requestXML = new StringBuilder();
                    if (streamRequestBody == null)
                    {

                        // Check whether the requestBody is null or not.
                        if (requestBody != null)
                        {
                            // Check whether the requestBody is string type.
                            string stringRequestBody = requestBody as string;
                            if (!string.IsNullOrWhiteSpace(stringRequestBody))
                            {
                                // If yes then append the string to the builder.
                                requestXML.Append(stringRequestBody);
                            }
                            else
                            {
                                // If not, then serialize the requestBody using the Serializer and append to builder.
                                requestXML.Append(this.RequestSerializer.Serialize(requestBody));
                            }


                            //enabling header logging in Serilogger
                            WebHeaderCollection allHeaders = httpWebRequest.Headers;

                            CoreHelper.AdvancedLogging.Log(" RequestUrl: " + httpWebRequest.RequestUri);
                            CoreHelper.AdvancedLogging.Log("Logging all headers in the request:");

                            for (int i = 0; i < allHeaders.Count; i++)
                            {
                                CoreHelper.AdvancedLogging.Log(allHeaders.GetKey(i) + "-" + allHeaders[i]);
                            }


                            // Log Request Body to a file
                            this.RequestLogging.LogPlatformRequests(" RequestUrl: " + requestEndpoint + ", Request Payload:" + requestXML.ToString(), true);
                            //Log to Serilog
                            CoreHelper.AdvancedLogging.Log( "Request Payload:" + requestXML.ToString());

                            // Use of encoding to get bytes used to write to request stream.
                            UTF8Encoding encoding = new UTF8Encoding();
                            content = encoding.GetBytes(requestXML.ToString());
                        }
                    }
                    else
                    {
                        content = streamRequestBody.ToArray();
                    }


                    TraceSwitch traceSwitch = new TraceSwitch("IPPTraceSwitch", "IPP Trace Switch");
                    
                    // Set the request properties.
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, (int)traceSwitch.Level > (int)TraceLevel.Info ? "Adding the payload to request.\n Start dump: \n" + requestXML.ToString() : "Adding the payload to request.");

                    if (content != null)
                    {
                        if (this.RequestCompressor != null)
                        {
                            // Get the request stream.
                            using (var requestStream = httpWebRequest.GetRequestStream())
                            {
                                this.RequestCompressor.Compress(content, requestStream);
                            }
                        }
                        else
                        {
                            // Get the request stream.
                            using (var stream = httpWebRequest.GetRequestStream())
                            {
                                // Write the content to stream.
                                stream.Write(content, 0, content.Length);
                            }
                        }
                    }
                }
            }

            // Authorize the request
            this.serviceContext.IppConfiguration.Security.Authorize(httpWebRequest, requestBody == null ? null : requestBody.ToString());

            // Add the Request Source header value.
            httpWebRequest.UserAgent = CoreConstants.REQUESTSOURCEHEADER;

            // Return the created http web request.
            return httpWebRequest;
        }

        /// <summary>
        /// Strip first slash
        /// </summary>
        private string StripFirstSlash(string uri)
        {
            if (string.Compare(uri, 0, "/", 0, 1) == 0)
            {
                return uri.Substring(1, uri.Length - 1);
            }
            return uri;
        }

        /// <summary>
        /// Append Query Parameters
        /// </summary>
        private string AppendQueryParameters(string requestEndpoint, string name, string value)
        {
            System.Text.StringBuilder requestEndpointBuilder = new System.Text.StringBuilder(requestEndpoint);
            requestEndpointBuilder.Append(requestEndpoint.Contains("?") ? "&" : "?");
            requestEndpointBuilder.Append(name);
            requestEndpointBuilder.Append("=");
            requestEndpointBuilder.Append(value);
            return requestEndpointBuilder.ToString();
        }

        /// <summary>
        /// Returns the response by calling REST service.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Response from REST service.</returns>
        public abstract string GetResponse(System.Net.HttpWebRequest request);

        /// <summary>
        /// Returns the response stream by calling REST service.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Response from REST service.</returns>
        public abstract byte[] GetResponseStream(System.Net.HttpWebRequest request);
    }
}
