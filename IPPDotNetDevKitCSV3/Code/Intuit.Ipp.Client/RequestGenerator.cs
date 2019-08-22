using Intuit.Ipp.Core;
using Intuit.Ipp.Core.Rest;
using Intuit.Ipp.Diagnostics;
using Intuit.Ipp.Exception;
using Intuit.Ipp.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Intuit.Ipp.Client
{
    public class RequestGenerator
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
        public RequestGenerator(ServiceContext context)
        {
            this.serviceContext = context;
            this.RequestCompressor = CoreHelper.GetCompressor(this.serviceContext, true);
            this.ResponseCompressor = CoreHelper.GetCompressor(this.serviceContext, false);
            this.RequestSerializer = CoreHelper.GetSerializer(this.serviceContext, true);
            this.responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);
            this.RequestLogging = CoreHelper.GetRequestLogging(this.serviceContext);
        }


        public virtual HttpRequestMessage PrepareRequest(RequestParameters requestParameters, object requestBody, string oauthRequestUri = null, bool includeRequestId = true)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Intuit.Ipp.Diagnostics.TraceLevel.Info, "Called PrepareRequest method");

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
                    this.RequestId = this.RequestId.Substring(0, (int)Math.Min(this.RequestId.Length, 50 - maximumBatchIdLength));
                }

                if (includeRequestId)
                {
                    requestEndpoint = requestEndpoint.Contains("?") ? requestEndpoint += "&" : requestEndpoint += "?";
                    requestEndpoint += "requestid=";
                    requestEndpoint += this.RequestId;
                }
            }

            Uri requestUri = new Uri(requestEndpoint);
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = requestUri;
            if (requestParameters.Verb == HttpVerbType.POST) { httpRequest.Method = HttpMethod.Post; }
            else
            {
                httpRequest.Method = HttpMethod.Get;
            }


         
            // Set the accept header type to JSON.
            if (this.responseSerializer is JsonObjectSerializer)
            {
                httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(CoreConstants.CONTENTTYPE_APPLICATIONJSON));
            }

            // If the service type is IPS then set the Api name in header. 
            if (this.serviceContext.ServiceType == IntuitServicesType.IPS && (oauthRequestUri == null))
            {
                // Add the API name as header to the request.
                httpRequest.Headers.Add(CoreConstants.APIACTIONHEADER, requestParameters.ApiName);
            }

            if (this.RequestCompressor != null)
            {
                httpRequest.Headers.Add(CoreConstants.CONTENTENCODING, this.RequestCompressor.DataCompressionFormat.ToString().ToLowerInvariant());
            }


            if (requestParameters.Verb == HttpVerbType.POST)
            {
                byte[] content = null;

                MemoryStream streamRequestBody = requestBody as MemoryStream;
                StringBuilder requestData = new StringBuilder();
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
                            requestData.Append(stringRequestBody);
                        }
                        else
                        {
                            // If not, then serialize the requestBody using the Serializer and append to builder.
                            requestData.Append(this.RequestSerializer.Serialize(requestBody));
                        }

                        // Log Request Body to a file
                        this.RequestLogging.LogPlatformRequests(" RequestUrl: " + requestEndpoint + ", Request Payload:" + requestData.ToString(), true);


                        // Use of encoding to get bytes used to write to request stream.
                        UTF8Encoding encoding = new UTF8Encoding();
                        content = encoding.GetBytes(requestData.ToString());
                    }
                }
                else
                {
                    content = streamRequestBody.ToArray();
                }


                TraceSwitch traceSwitch = new TraceSwitch("IPPTraceSwitch", "IPP Trace Switch");

                // Set the request properties.
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Intuit.Ipp.Diagnostics.TraceLevel.Info, (int)traceSwitch.Level > (int)Intuit.Ipp.Diagnostics.TraceLevel.Info ? "Adding the payload to request.\n Start dump: \n" + requestData.ToString() : "Adding the payload to request.");

                if (content != null)
                {
                    if (this.RequestCompressor != null)
                    {
                        // Get the request stream.
                        using (var requestStream = new MemoryStream())
                        {
                            this.RequestCompressor.Compress(content, requestStream);
                        }
                    }
                    else
                    {
                        // Get the request stream.
                        using (var requestStream = new MemoryStream())
                        {
                            // Write the content to stream.
                            requestStream.Write(content, 0, content.Length);
                        }

                     
                    }
                }

                // Set the content type
                if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
                {
                    httpRequest.Content = new StringContent(requestData.ToString(), Encoding.UTF8, "application/json");
                }
                else
                {
                    httpRequest.Content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");
                }
            }

            // Authorize the request
            this.serviceContext.IppConfiguration.Security.Authorize(httpRequest, requestBody == null ? null : requestBody.ToString());

            // Add the Request Source header value.
          httpRequest.Headers.Add("UserAgent",CoreConstants.REQUESTSOURCEHEADER);
          
            // Return the created http web request.
            return httpRequest;
        }
        private string StripFirstSlash(string uri)
        {
            if (string.Compare(uri, 0, "/", 0, 1) == 0)
            {
                return uri.Substring(1, uri.Length - 1);
            }
            return uri;
        }

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


        public static Stream AddListener(string path)
        {
            string filename = path + "TraceLog-" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            Stream myFile = null;
            if (File.Exists(filename))
                myFile = new FileStream(filename, FileMode.Append);
            else
                myFile = new FileStream(filename, FileMode.Create);
            TextWriterTraceListener myTextListener = new
            TextWriterTraceListener(myFile);
            Trace.Listeners.Add(myTextListener);
            Trace.AutoFlush = true;
            return myFile;

        }
    }
}