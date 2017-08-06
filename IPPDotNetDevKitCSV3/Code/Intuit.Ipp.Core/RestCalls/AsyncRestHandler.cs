////********************************************************************
// <copyright file="ASyncRestHandler.cs" company="Intuit">
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
// <summary>This file contains logic for Async REST request handler.</summary>
////********************************************************************

using System.Diagnostics;

namespace Intuit.Ipp.Core.Rest
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Xml;
    using Intuit.Ipp.Core.Properties;
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Exception;

    /// <summary>
    /// RestRequestHandler contains the logic for preparing the REST request, calls REST services and returns the response.
    /// </summary>
    public class AsyncRestHandler : RestHandler
    {
        /// <summary>
        /// Request Body.
        /// </summary>
        private string requestBody;

        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        private ServiceContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncRestHandler"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public AsyncRestHandler(ServiceContext context)
            : base(context)
        {
            this.context = context;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="AsyncRestHandler"/> class from being created.
        /// </summary>
        private AsyncRestHandler()
        {
        }

        /// <summary>
        /// Callback event.
        /// </summary>
        public event EventHandler<AsyncCallCompletedEventArgs> OnCallCompleted;

        /// <summary>
        /// Gets the value which indicates not to set the request body of the http web request.
        /// </summary>
        internal override bool IsSyncRequestStream
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Prepares the HttpWebRequest along with authentication header added to the request.
        /// </summary>
        /// <param name="requestParameters">The parameters.</param>
        /// <param name="requestBody">The request entity.</param>
        /// <param name="oauthRequestUri">The OAtuth request uri.</param>
        /// <returns>
        /// Http web request object.
        /// </returns>
        public override HttpWebRequest PrepareRequest(RequestParameters requestParameters, object requestBody, string oauthRequestUri = null, bool includeRequestId = true)
        {
            // When the Verb is POST, we need to serialize the request xml to body.
            if (requestParameters.Verb == HttpVerbType.POST)
            {
                StringBuilder requestXML = new StringBuilder();

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

                    this.requestBody = requestXML.ToString();
                }
            }

            return base.PrepareRequest(requestParameters, requestBody, oauthRequestUri, includeRequestId);
        }

        /// <summary>
        /// Returns the response by calling REST service.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Response from REST service.</returns>
        public override string GetResponse(HttpWebRequest request)
        {
            try
            {
                // Check whether the retryPolicy is null.
                if (this.context.IppConfiguration.RetryPolicy == null)
                {
                    // If yes then call the rest service without retry framework enabled.
                    this.ExecAsyncRequest(request);
                }
                else
                {
                    // If no then call the rest service using the execute action of retry framework.
                    this.ExecAsyncRequestWithRetryPolicy(request);
                }

                return null;
            }
            finally
            {
                this.context.RequestId = null;
            }
        }

        /// <summary>
        /// Returns the response stream by calling REST service.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Response from REST service.</returns>
        public override byte[] GetResponseStream(HttpWebRequest request)
        {
            try
            {
                // Check whether the retryPolicy is null.
                if (this.context.IppConfiguration.RetryPolicy == null)
                {
                    // If yes then call the rest service without retry framework enabled.
                    this.ExecAsyncRequest(request);
                }
                else
                {
                    // If no then call the rest service using the execute action of retry framework.
                    this.ExecAsyncRequestWithRetryPolicy(request);
                }

                //return null;
                byte[] pdf = new byte[0];
                return pdf;
            }
            catch (Exception)
            {
                    
                throw;
            }
            
            
        }

        /// <summary>
        /// Executes the Asynchronous Request.
        /// </summary>
        /// <param name="asyncRequest">Asynchronous web request.</param>
        private void ExecAsyncRequestWithRetryPolicy(HttpWebRequest asyncRequest)
        {
            AsyncCallCompletedEventArgs resultArguments = null;

            // Check whether the Method is post.
            if (asyncRequest.Method == "POST")
            {
                // If true then ExecuteAction which calls the service using retry framework.
                this.context.IppConfiguration.RetryPolicy.ExecuteAction(
                           ac =>
                           {
                               // Invoke the begin method of the asynchronous call.
                               asyncRequest.BeginGetRequestStream(ac, asyncRequest);
                           },
                           ar =>
                           {
                               // Invoke the end method of the asynchronous call.
                               HttpWebRequest request = (HttpWebRequest)ar.AsyncState;

                               // Log Request Body to a file
                               this.RequestLogging.LogPlatformRequests(this.requestBody, true);

                               // Using encoding get the byte value of the requestBody.
                               UTF8Encoding encoding = new UTF8Encoding();
                               byte[] content = encoding.GetBytes(this.requestBody);

                               TraceSwitch traceSwitch = new TraceSwitch("IPPTraceSwitch", "IPP Trace Switch");
                               this.context.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, (int)traceSwitch.Level > (int)TraceLevel.Info ? "Adding the payload to request. \n Start dump: " + this.requestBody : "Adding the payload to request.");

                               // Check whether compression is enabled and compress the stream accordingly.
                               if (this.RequestCompressor != null)
                               {
                                   // get the request stream.
                                   using (var requeststream = request.EndGetRequestStream(ar))
                                   {
                                       this.RequestCompressor.Compress(content, requeststream);
                                   }
                               }
                               else
                               {
                                   // Get the Request stream.
                                   using (System.IO.Stream postStream = request.EndGetRequestStream(ar))
                                   {
                                       // Write the byte content to the stream.
                                       postStream.Write(content, 0, content.Length);
                                   }
                               }
                           },
                           () =>
                           {
                               // Action to perform if the asynchronous operation
                               // succeeded.
                               this.ExecuteServiceCallAsync(asyncRequest);
                           },
                           e =>
                           {
                               // Action to perform if the asynchronous operation
                               // failed after all the retries.
                               IdsException idsException = e as IdsException;
                               if (idsException != null)
                               {
                                   resultArguments = new AsyncCallCompletedEventArgs(null, idsException);
                               }
                               else
                               {
                                   resultArguments = new AsyncCallCompletedEventArgs(null, new IdsException("Exception has been generated.", e));
                               }

                               this.OnCallCompleted(this, resultArguments);
                           });
            }
            else
            {
                // Call the service without writing the request body to request stream.
                this.ExecuteServiceCallAsync(asyncRequest);
            }
        }

        /// <summary>
        /// Executes the Service Call using the Retry Policy.
        /// </summary>
        /// <param name="myRequest">Http Request.</param>
        private void ExecuteServiceCallAsync(HttpWebRequest myRequest)
        {
            AsyncCallCompletedEventArgs resultArguments = null;

            // ExecuteAction which calls the service using retry framework.
            this.context.IppConfiguration.RetryPolicy.ExecuteAction(
                      ac =>
                      {
                          // Invoke the begin method of the asynchronous call.
                          myRequest.BeginGetResponse(ac, myRequest);
                      },
                      ar =>
                      {
                          resultArguments = CreateEventArgsForRequest(ar);
                      },
                      () =>
                      {
                          // Action to perform if the asynchronous operation
                          // succeeded.
                          if (this.OnCallCompleted != null)
                          {
                              this.OnCallCompleted(this, resultArguments);
                          }
                      },
                      e =>
                      {
                          // Action to perform if the asynchronous operation
                          // failed after all the retries.
                          resultArguments = CreateEventArgsForException(e);
                          this.OnCallCompleted(this, resultArguments);
                      });
        }

        /// <summary>
        /// Creates the Event Args for Exception/Fault responses from server.
        /// </summary>
        /// <param name="exception">The exception class.</param>
        /// <returns>Async CallCompletedEvent Arguments.</returns>
        private AsyncCallCompletedEventArgs CreateEventArgsForException(Exception exception)
        {
            AsyncCallCompletedEventArgs resultArguments = null;
            WebException webException = exception as WebException;
            if (webException != null)
            {
                bool isIps = false;
                if (this.context.ServiceType == IntuitServicesType.IPS)
                {
                    isIps = true;
                }

                FaultHandler handler = new FaultHandler(this.context);
                IdsException idsException = handler.ParseResponseAndThrowException(webException, isIps);
                if (idsException != null)
                {
                    this.context.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    resultArguments = new AsyncCallCompletedEventArgs(null, idsException);
                }
            }
            else
            {
                IdsException idsException = exception as IdsException;
                if (idsException != null)
                {
                    this.context.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    resultArguments = new AsyncCallCompletedEventArgs(null, idsException);
                }
                else
                {
                    this.context.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    resultArguments = new AsyncCallCompletedEventArgs(null, new IdsException("Exception has been generated.", exception));
                }
            }

            return resultArguments;
        }

        /// <summary>
        /// Creates the Event Args for success responses from server.
        /// </summary>
        /// <param name="asyncResult">The IAsynResult.</param>
        /// <returns>Async CallCompletedEvent Arguments.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "Require the method not to be static.")]
        private AsyncCallCompletedEventArgs CreateEventArgsForRequest(IAsyncResult asyncResult)
        {
            IdsException exception = null;
            AsyncCallCompletedEventArgs resultArguments = null;
            byte[] receiveBytes = new byte[0];
            bool isResponsePdf = false;

            // Get the async state of the web request.
            HttpWebRequest request = (HttpWebRequest)asyncResult.AsyncState;

            // Invoke the end method of the asynchronous call. 
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asyncResult);
            string resultString = string.Empty;

            if (!string.IsNullOrWhiteSpace(response.ContentEncoding) && this.ResponseCompressor != null)
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var decompressedStream = this.ResponseCompressor.Decompress(responseStream))
                    {

                        if (response.ContentType.ToLower().Contains(CoreConstants.CONTENTTYPE_APPLICATIONPDF.ToLower()))
                        {
                            receiveBytes = ConvertResponseStreamToBytes(decompressedStream);
                            isResponsePdf = true;
                        }
                        else
                        {

                            // Get the response stream.
                            StreamReader reader = new StreamReader(decompressedStream);

                            // Read the Stream
                            resultString = reader.ReadToEnd();
                            // Close reader
                            reader.Close();
                        }
                    }
                }
            }
            else
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    

                    //get the response in bytes if the conten-type is application/pdf
                    if (response.ContentType.ToLower().Contains(CoreConstants.CONTENTTYPE_APPLICATIONPDF.ToLower()))
                    {
                        receiveBytes = ConvertResponseStreamToBytes(responseStream);
                        isResponsePdf = true;
                    }
                    else
                    {
                        // Get the response stream.
                        StreamReader reader = new StreamReader(responseStream);
                        
                        // Read the Stream
                        resultString = reader.ReadToEnd();
                        // Close reader
                        reader.Close();
                    }
                }
            }

            // Log response to disk.
            this.RequestLogging.LogPlatformRequests(resultString, false);

            //log response to logs
            TraceSwitch traceSwitch = new TraceSwitch("IPPTraceSwitch", "IPP Trace Switch");
            this.context.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, (int)traceSwitch.Level > (int)TraceLevel.Info ? "Got the response from service.\n Start Dump: \n" + resultString : "Got the response from service.");

            //if response is of not type pdf do as usual
            if (!isResponsePdf)
            {
                // If the response string is null then there was a communication mismatch with the server. So throw exception.
                if (string.IsNullOrWhiteSpace(resultString))
                {
                    exception = new IdsException(Resources.CommunicationErrorMessage, new CommunicationException(Resources.ResponseStreamNullOrEmptyMessage));
                    resultArguments = new AsyncCallCompletedEventArgs(null, exception);
                }
                else
                {
                    if (this.context.ServiceType == IntuitServicesType.IPS)
                    {
                        // Handle errors here
                        resultArguments = this.HandleErrors(resultString);
                    }
                    else
                    {
                        FaultHandler handler = new FaultHandler(this.context);
                        IdsException idsException = handler.ParseErrorResponseAndPrepareException(resultString);
                        if (idsException != null)
                        {
                            this.context.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                            resultArguments = new AsyncCallCompletedEventArgs(null, idsException);
                        }
                        else
                        {
                            resultArguments = new AsyncCallCompletedEventArgs(resultString, null);
                        }
                    }
                }
            }
            else //if response is of type pdf act accordingly
            {
                if (receiveBytes.Length <= 0)
                {
                    //response equivalent to nullorwhitespace above so act in similar way
                    exception = new IdsException(Resources.CommunicationErrorMessage, new CommunicationException(Resources.ResponseStreamNullOrEmptyMessage));
                    resultArguments = new AsyncCallCompletedEventArgs(null, exception);
                }
                
                //faults not applicable here since we are expecting only pdf in binary
                resultArguments = new AsyncCallCompletedEventArgs(null, null, receiveBytes);
            }

            return resultArguments;
        }

        /// <summary>
        /// Gets a response stream and returns them as byte array.
        /// </summary>
        /// <param name="responseStream">Stream from web response</param>
        private byte[] ConvertResponseStreamToBytes(Stream responseStream)
        {
            byte[] bytes = new byte[0];
            
            MemoryStream mem = new MemoryStream();

            if (responseStream != null)
                responseStream.CopyTo(mem);

            bytes = mem.ToArray();

            return bytes;
        }
        
        /// <summary>
        /// Executes the Asynchronous Request.
        /// </summary>
        /// <param name="asyncRequest">Asynchronous web request.</param>
        private void ExecAsyncRequest(HttpWebRequest asyncRequest)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // Check whether the method is Post
            if (asyncRequest.Method == "POST")
            {
                // If true then get the request steam to write the content.
                asyncRequest.BeginGetRequestStream(new AsyncCallback(this.GetRequestStreamCallback), asyncRequest);
            }
            else
            {
                // else execute the service call.
                asyncRequest.BeginGetResponse(new AsyncCallback(this.AsyncExecutionCompleted), asyncRequest);
            }
        }

        /// <summary>
        /// Call back method for Find all entities  Asynchronously 
        /// </summary>
        /// <param name="result">Asynchronous Result.</param>
        private void AsyncExecutionCompleted(IAsyncResult result)
        {
            AsyncCallCompletedEventArgs resultArguments = null;
            try
            {
                resultArguments = this.CreateEventArgsForRequest(result);
                if (this.OnCallCompleted != null)
                {
                    this.OnCallCompleted(this, resultArguments);
                }
            }
            catch (WebException webException)
            {
                resultArguments = this.CreateEventArgsForException(webException);
                this.OnCallCompleted(this, resultArguments);
            }
        }

        /// <summary>
        /// Callback for GetRequestStream.
        /// </summary>
        /// <param name="asynchronousResult">Asynchronous Result.</param>
        private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;

            // Log Request Body to a file
            this.RequestLogging.LogPlatformRequests(this.requestBody, true);

            UTF8Encoding encoding = new UTF8Encoding();
            byte[] content = encoding.GetBytes(this.requestBody);


            TraceSwitch traceSwitch = new TraceSwitch("IPPTraceSwitch", "IPP Trace Switch");
            this.context.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Info, (int)traceSwitch.Level > (int)TraceLevel.Info ? "Adding the payload to request.\n Start dump of request: \n" + this.requestBody : "Adding the payload to request.");

            // Check whether compression is enabled and compress the stream accordingly.
            if (this.RequestCompressor != null)
            {
                // get the request stream.
                using (var requeststream = request.EndGetRequestStream(asynchronousResult))
                {
                    this.RequestCompressor.Compress(content, requeststream);
                }
            }
            else
            {
                // get the request stream.
                using (System.IO.Stream postStream = request.EndGetRequestStream(asynchronousResult))
                {
                    postStream.Write(content, 0, content.Length);
                }
            }

            // Start the asynchronous operation to get the response
            request.BeginGetResponse(new AsyncCallback(this.AsyncExecutionCompleted), request);
        }

        /// <summary>
        /// Check the response for any errors it might indicate. Will throw an exception if API response indicates an error.
        /// Will throw an exception if it has a problem determining success or error.
        /// </summary>
        /// <param name="responseXml">the QuickBase response to examine</param>
        /// <returns>Asyn Call Completed Arguments.</returns>
        private AsyncCallCompletedEventArgs HandleErrors(string responseXml)
        {
            XmlDocument document = new System.Xml.XmlDocument();
            document.LoadXml(responseXml);
            AsyncCallCompletedEventArgs resultArguments = null;
            XmlNode errCodeNode = document.SelectSingleNode(Utility.UtilityConstants.ERRCODEXPATH);
            IdsException exception = null;
            if (errCodeNode == null)
            {
                exception = new IdsException(Resources.ErrorCodeMissing);
                resultArguments = new AsyncCallCompletedEventArgs(null, exception);
            }

            int errorCode;
            if (!int.TryParse(errCodeNode.InnerText, out errorCode))
            {
                exception = new IdsException(string.Format(CultureInfo.InvariantCulture, Resources.ErrorCodeNonNemeric, errorCode));
                resultArguments = new AsyncCallCompletedEventArgs(null, exception);
            }

            if (errorCode == 0)
            {
                // 0 indicates success
                resultArguments = new AsyncCallCompletedEventArgs(responseXml, null);
                return resultArguments;
            }

            XmlNode errTextNode = document.SelectSingleNode(Utility.UtilityConstants.ERRTEXTXPATH);
            if (errTextNode == null)
            {
                exception = new IdsException(string.Format(CultureInfo.InvariantCulture, Resources.ErrorWithNoText, errorCode));
                resultArguments = new AsyncCallCompletedEventArgs(null, exception);
            }

            string errorText = errTextNode.InnerText;
            XmlNode errDetailNode = document.SelectSingleNode(Utility.UtilityConstants.ERRDETAILXPATH);
            string errorDetail = errDetailNode != null ? errDetailNode.InnerText : null;

            if (!string.IsNullOrEmpty(errorDetail))
            {
                exception = new IdsException(string.Format(CultureInfo.InvariantCulture, Resources.ErrorDetails0, errorText, errorCode, errorDetail));
                resultArguments = new AsyncCallCompletedEventArgs(null, exception);
            }

            exception = new IdsException(string.Format(CultureInfo.InvariantCulture, Resources.ErrorDetails1, errorText, errorCode));
            resultArguments = new AsyncCallCompletedEventArgs(null, exception);
            return resultArguments;
        }
    }
}
