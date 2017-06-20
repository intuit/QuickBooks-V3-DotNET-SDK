////********************************************************************
// <copyright file="SyncRestHandler.cs" company="Intuit">
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
// <summary>This file contains logic for REST request handler.</summary>
////********************************************************************

using System.Diagnostics;

namespace Intuit.Ipp.Core.Rest
{
    using System;
    using System.IO;
    using System.Net;
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Exception;

    /// <summary>
    /// SyncRestHandler contains the logic for preparing the REST request, calls REST services and returns the response.
    /// </summary>
    public class SyncRestHandler : RestHandler
    {
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        private ServiceContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncRestHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public SyncRestHandler(ServiceContext context)
            : base(context)
        {
            this.context = context;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="SyncRestHandler"/> class from being created.
        /// </summary>
        private SyncRestHandler()
        {
        }

        /// <summary>
        /// Gets the value which indicates to set the request body of the http web request.
        /// </summary>
        internal override bool IsSyncRequestStream
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Prepares the HttpWebRequest along with authentication header added to the request.
        /// </summary>
        /// <param name="requestParameters">The parameters.</param>
        /// <param name="requestBody">The request entity.</param>
        /// <param name="oauthRequestUri">The OAuth reqeust uri.</param>
        /// <returns>
        /// Http web request object.
        /// </returns>
        public override HttpWebRequest PrepareRequest(RequestParameters requestParameters, object requestBody, string oauthRequestUri = null, bool includeRequestId = true)
        {
            HttpWebRequest request = base.PrepareRequest(requestParameters, requestBody, oauthRequestUri, includeRequestId);
            // set the timeout for the request if it has a value.
            Nullable<int> timeout = this.context.Timeout;
            if ( timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            return request;
        }

        /// <summary>
        /// Returns the response by calling REST service.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Response from REST service.</returns>
        public override string GetResponse(HttpWebRequest request)
        {
            FaultHandler handler = new FaultHandler(this.context);

            // Create a variable for storing the response.
            string response = string.Empty;
            try
            {
                // Check whether the retryPolicy is null.
                if (this.context.IppConfiguration.RetryPolicy == null)
                {
                    // If yes then call the rest service without retry framework enabled.
                    response = this.CallRestService(request);
                }
                else
                {
                    // If no then call the rest service using the execute action of retry framework.
                    this.context.IppConfiguration.RetryPolicy.ExecuteAction(() =>
                    {
                        response = this.CallRestService(request);
                    });
                }
                if (request != null && request.RequestUri != null && request.RequestUri.Segments != null)
                {
                    if (System.Array.IndexOf(request.RequestUri.Segments, "reports/") >= 0)
                    {
                        if (!response.StartsWith("{\"Report\":")) { response = "{\"Report\":" + response + "}"; }
                    }
                }
         
                if (request != null && request.RequestUri != null && request.RequestUri.Segments != null)
                {
                    if (System.Array.IndexOf(request.RequestUri.Segments, "taxservice/") >= 0)
                    {
                        //This if condition was added as Json serialization was failing for the FaultResponse bcoz of missing TaxService seriliazation tag on AnyIntuitObject in Fms.cs class

                        if (!response.Contains("Fault"))
                        {

                            if (!response.StartsWith("{\"TaxService\":")) { response = "{\"TaxService\":" + response + "}"; }
                        }
                    }
                }
            }
            catch (RetryExceededException retryExceededException)
            {
                // System.Net.HttpWebRequest.Abort() was previously called.-or- The time-out
                // period for the request expired.-or- An error occurred while processing the request.
                bool isIps = false;
                if (this.context.ServiceType == IntuitServicesType.IPS)
                {
                    isIps = true;
                }


                this.context.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, retryExceededException.ToString());
                throw retryExceededException;

            }
            catch (WebException webException)
            {
                // System.Net.HttpWebRequest.Abort() was previously called.-or- The time-out
                // period for the request expired.-or- An error occurred while processing the request.
                bool isIps = false;
                if (this.context.ServiceType == IntuitServicesType.IPS)
                {
                    isIps = true;
                }

                IdsException idsException = handler.ParseResponseAndThrowException(webException, isIps);
                if (idsException != null)
                {
                    this.context.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    throw idsException;
                }
            }
            finally
            {
                this.context.RequestId = null;
            }

            if (this.context.ServiceType == IntuitServicesType.IPS)
            {
                // Handle errors here
                Utility.IntuitErrorHandler.HandleErrors(response);
            }
            else
            {
                // Check the response if there are any fault tags and throw appropriate exceptions.
                IdsException exception = handler.ParseErrorResponseAndPrepareException(response);
                if (exception != null)
                {
                    throw exception;
                }
            }

            // Return the response.
            return response;
        }

        /// <summary>
        /// Returns the response stream by calling REST service.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Response from REST service.</returns>
        public override byte[] GetResponseStream(HttpWebRequest request)
        {
            FaultHandler handler = new FaultHandler(this.context);
            byte[] receivedBytes = new byte[0];
            
            try
            {
                // Check whether the retryPolicy is null.
                if (this.context.IppConfiguration.RetryPolicy == null)
                {
                    // If yes then call the rest service without retry framework enabled.
                    receivedBytes = GetRestServiceCallResponseStream(request);
                }
                else
                {
                    // If no then call the rest service using the execute action of retry framework.
                    this.context.IppConfiguration.RetryPolicy.ExecuteAction(() =>
                    {
                       receivedBytes = GetRestServiceCallResponseStream(request);
                    });
                }
            }
            catch (WebException webException)
            {
                // System.Net.HttpWebRequest.Abort() was previously called.-or- The time-out
                // period for the request expired.-or- An error occurred while processing the request.
                bool isIps = false;
                if (this.context.ServiceType == IntuitServicesType.IPS)
                {
                    isIps = true;
                }

                IdsException idsException = handler.ParseResponseAndThrowException(webException, isIps);
                if (idsException != null)
                {
                    this.context.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    throw idsException;
                }
            }
            finally
            {
                this.context.RequestId = null;
            }

            return receivedBytes;
        }

        /// <summary>
        /// Calls the rest service.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Returns the response.</returns>
        private string CallRestService(HttpWebRequest request)
        {
            this.context.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, "Getting the response from service.");

            // Call the service and get response.
            using (HttpWebResponse httpWebResponse = request.GetResponse() as HttpWebResponse)
            {
                string parsedResponse = this.ParseResponse(httpWebResponse);
                TraceSwitch traceSwitch = new TraceSwitch("IPPTraceSwitch", "IPP Trace Switch");
                this.context.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, (int)traceSwitch.Level > (int)TraceLevel.Info ? "Got the response from service.\n Start dump: \n " + parsedResponse : "Got the response from service.");

                // Parse the response from the call and return.
                return parsedResponse;
            }
        }

        // <summary>
        /// Calls the rest service.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Returns the response.</returns>
        private byte[] GetRestServiceCallResponseStream(HttpWebRequest request)
        {
            this.context.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, "Getting the response from service as response stream.");

            Stream receiveStream = new MemoryStream();
            byte[] receiveBytes = new byte[0];
            MemoryStream mem = new MemoryStream();
            
            // Call the service and get response.
            using (HttpWebResponse httpWebResponse = request.GetResponse() as HttpWebResponse)
            {

                if (!string.IsNullOrWhiteSpace(httpWebResponse.ContentEncoding) && this.ResponseCompressor != null)
                {
                    using (var responseStream = httpWebResponse.GetResponseStream())
                    {
                        using (var decompressedStream = this.ResponseCompressor.Decompress(responseStream))
                        {
                            decompressedStream.CopyTo(mem);
                            receiveBytes = mem.ToArray();
                        }
                    }
                }
                else
                {

                    using (var responseStream = httpWebResponse.GetResponseStream())
                    {
                        responseStream.CopyTo(mem);
                        receiveBytes = mem.ToArray();
                    }
                }
                
                TraceSwitch traceSwitch = new TraceSwitch("IPPTraceSwitch", "IPP Trace Switch");
                this.context.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, "Got the response from service.");
            }

            // Return the response stream
            return receiveBytes;
        }

        /// <summary>
        /// Parses the response object.
        /// </summary>
        /// <param name="httpWebResponse">The Http Web Response object.</param>
        /// <returns>Returns the response by parsing httpWebResponse object.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Require the method not to be static.")]
        private string ParseResponse(HttpWebResponse httpWebResponse)
        {
            // Create a variable for storing the response.
            string response = string.Empty;

            // Check whether the Status Code is OK which denotes that successful response.
            // TODO: This check might be rhetorical since any response from Ids will be 200OK with error response.
            if (httpWebResponse.StatusCode == HttpStatusCode.OK)
            {
                if (!string.IsNullOrWhiteSpace(httpWebResponse.ContentEncoding) && this.ResponseCompressor != null)
                {
                    using (var responseStream = httpWebResponse.GetResponseStream())
                    {
                        using (var decompressedStream = this.ResponseCompressor.Decompress(responseStream))
                        {
                            StreamReader reader = new StreamReader(decompressedStream);

                            // Read the Stream
                            response = reader.ReadToEnd();
                            // Close reader
                            reader.Close();
                        }
                    }
                }
                else
                {
                    using (Stream responseStream = httpWebResponse.GetResponseStream())
                    {
                        // Get the response stream.
                        StreamReader reader = new StreamReader(responseStream);

                        // Read the Stream
                        response = reader.ReadToEnd();
                        // Close reader
                        reader.Close();
                    }
                }

                // Log the response to Disk.
                this.RequestLogging.LogPlatformRequests(response, false);
                
                
            }

            // Return the response.
            return response;
        }
    }
}
