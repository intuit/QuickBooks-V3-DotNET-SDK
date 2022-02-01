////***************************************************************************************************
// <copyright file="Batch.cs" company="Intuit">
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
// <summary>This file contains code for Batch Processing.</summary>
////***************************************************************************************************

using System.Reflection;

namespace Intuit.Ipp.DataService
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using Intuit.Ipp.Core;
    using Intuit.Ipp.Core.Rest;
    using Intuit.Ipp.Data;
    using Intuit.Ipp.DataService.Properties;
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Exception;
    using Intuit.Ipp.Utility;

    /// <summary>
    /// This class contains code for Batch Processing.
    /// </summary>
    public class Batch
    {
        /// <summary>
        /// batch requests
        /// </summary>
        private List<BatchItemRequest> batchRequests;

        /// <summary>
        /// batch responses
        /// </summary>
        private List<BatchItemResponse> batchResponses;

        /// <summary>
        /// inuit batch item responses list.
        /// </summary>
        private List<IntuitBatchResponse> intuitBatchItemResponses;

        /// <summary>
        /// service context object.
        /// </summary>
        private ServiceContext serviceContext;

        /// <summary>
        /// rest handler object.
        /// </summary>
        private IRestHandler restHandler;

        /// <summary>
        /// serializer to be used. 
        /// </summary>
        private IEntitySerializer responseSerializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Batch"/> class.
        /// </summary>
        /// <param name="serviceContext">The service context.</param>
        /// <param name="restHandler">The rest handler.</param>
        internal Batch(ServiceContext serviceContext, IRestHandler restHandler)
        {
            this.serviceContext = serviceContext;
            this.restHandler = restHandler;
            this.responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);
            this.batchRequests = new List<BatchItemRequest>();
            this.batchResponses = new List<BatchItemResponse>();
            this.intuitBatchItemResponses = new List<IntuitBatchResponse>();
        }

        #region Execute Async handler
        /// <summary>
        /// Gets or sets the call back event for ExecuteAsync method in asynchronous call.
        /// </summary>
        /// <value>
        /// on batch execute async completed.
        /// </value>
        public BatchProcessingCallback.BatchExecutionCompletedEventHandler OnBatchExecuteAsyncCompleted { get; set; }

        #endregion

        #region properties

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get
            {
                return this.batchRequests.Count;
            }
        }

        /// <summary>
        /// Gets list of entites in case ResponseType is Report.
        /// </summary>
        public ReadOnlyCollection<IntuitBatchResponse> IntuitBatchItemResponses
        {
            get { return new ReadOnlyCollection<IntuitBatchResponse>(this.intuitBatchItemResponses); }
        }

        /// <summary>
        /// Gets the <see cref="Intuit.Ipp.DataService.IntuitBatchResponse"/> with the specified id.
        /// </summary>
        /// <param name="id"> unique batchitem id. </param>
        public IntuitBatchResponse this[string id]
        {
            get
            {
                BatchItemResponse batchresponse = this.batchResponses.Find(item => item.bId == id);
                // if (batchresponse == null)
                // {
                //    throw new IdsException(string.Format("Could not find the batch item response with the specified id: {0}", id)); 
                // }

                IntuitBatchResponse result = ProcessBatchItemResponse(batchresponse);
                return result;
            }
        }

        /// <summary>
        /// Tries to get the <see cref="Intuit.Ipp.DataService.IntuitBatchResponse"/> with the specified id
        /// </summary>
        /// <param name="id"> unique batchitem id. </param>
        /// <returns>True if the item was found, otherwise false</returns>
        public bool TryGetValue(string id, out IntuitBatchResponse intuitBatchResponse)
        {
            BatchItemResponse batchresponse = this.batchResponses.FirstOrDefault(item => item.bId == id);
            if (batchresponse == null)
            {
                intuitBatchResponse = null;
                return false;
            }

            intuitBatchResponse = ProcessBatchItemResponse(batchresponse);
            return true;
        }

        #endregion

        #region methods

        /// <summary>
        /// Adds the specified query.
        /// </summary>
        /// <param name="query"> IDS query. </param>
        /// <param name="id"> unique batchitem id.</param>
        /// <example> 
        /// query parameter : This parameter takes IDS query string.
        /// Developers can write LINQ query and convert it the IDS query using QueryService class.
        /// Please see the example below. In this example LINQ query is converted to it's equivalent IDS query.
        /// Please refer online documentation more details.
        /// </example>
        /// <code>
        /// QueryService<Customer> customerContext=new QueryService<Customer>(serviceContext);
        /// string query = this.customerContext.Where(c => c.MiddleName.StartsWith("a") && c.FamilyName.EndsWith("z")).ToIdsQuery();
        /// </code>
        public void Add(string query, string id)
        {
            Add(query, id, null);
        }

        /// <summary>
        /// Adds the specified query.
        /// </summary>
        /// <param name="query"> IDS query. </param>
        /// <param name="id"> unique batchitem id.</param>
        /// <example> 
        /// query parameter : This parameter takes IDS query string.
        /// Developers can write LINQ query and convert it the IDS query using QueryService class.
        /// Please see the example below. In this example LINQ query is converted to it's equivalent IDS query.
        /// Please refer online documentation more details.
        /// </example>
        /// <code>
        /// QueryService<Customer> customerContext=new QueryService<Customer>(serviceContext);
        /// string query = this.customerContext.Where(c => c.MiddleName.StartsWith("a") && c.FamilyName.EndsWith("z")).ToIdsQuery();
        /// </code>
        public void Add(string query, string id, List<String> optionsData)
        {
            // Create and Add a new BatchItem
            if (string.IsNullOrEmpty(query))
            {
                IdsException exception = new IdsException(Resources.StringParameterNullOrEmpty, new ArgumentException(Resources.IdString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            if (string.IsNullOrEmpty(id))
            {
                IdsException exception = new IdsException(Resources.StringParameterNullOrEmpty, new ArgumentException(Resources.IdString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            if (this.batchRequests.Count > 25)
            {
                IdsException exception = new IdsException(Resources.batchItemsExceededMessage, new BatchItemsExceededException(Resources.batchItemsExceededMessage));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            if (this.batchRequests.Count > 0 && this.batchRequests.Find(item => item.bId == id) != null)
            {
                IdsException exception = new IdsException(Resources.BatchIdAlreadyUsed, new ArgumentException(Resources.IdString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            BatchItemRequest batchItem = new BatchItemRequest();
            batchItem.AnyIntuitObject = query;
            batchItem.bId = id;
            //batchItem.operation = OperationEnum.query;
            batchItem.operationSpecified = false;
            if (optionsData != null && optionsData.Count > 0) { batchItem.optionsData = string.Join(",", optionsData.ToArray()).ToLower(); }
            batchItem.ItemElementName = ItemChoiceType8.Query;
            this.batchRequests.Add(batchItem);
        }

        /// <summary>
        /// Adds the specified CDCQuery.
        /// </summary>
        /// <param name="query"> IDS query. </param>
        /// <param name="id"> unique batchitem id.</param>
        /// <example> 
        /// query parameter : This parameter takes IDS query string.
        /// Developers can write LINQ query and convert it the IDS query using QueryService class.
        /// Please see the example below. In this example LINQ query is converted to it's equivalent IDS query.
        /// Please refer online documentation more details.
        /// </example>
        /// <code>
        /// QueryService<Customer> customerContext=new QueryService<Customer>(serviceContext);
        /// string query = this.customerContext.Where(c => c.MiddleName.StartsWith("a") && c.FamilyName.EndsWith("z")).ToIdsQuery();
        /// </code>
        public void Add(CDCQuery query, string id)
        {
            Add(query, id, null);
        }

        /// <summary>
        /// Adds the specified CDCQuery.
        /// </summary>
        /// <param name="query"> IDS query. </param>
        /// <param name="id"> unique batchitem id.</param>
        /// <example> 
        /// query parameter : This parameter takes IDS query string.
        /// Developers can write LINQ query and convert it the IDS query using QueryService class.
        /// Please see the example below. In this example LINQ query is converted to it's equivalent IDS query.
        /// Please refer online documentation more details.
        /// </example>
        /// <code>
        /// QueryService<Customer> customerContext=new QueryService<Customer>(serviceContext);
        /// string query = this.customerContext.Where(c => c.MiddleName.StartsWith("a") && c.FamilyName.EndsWith("z")).ToIdsQuery();
        /// </code>
        public void Add(CDCQuery query, string id, List<string> optionsData)
        {
            // Create and Add a new BatchItem
            if (query == null)
            {
                IdsException exception = new IdsException(Resources.StringParameterNullOrEmpty, new ArgumentException(Resources.IdString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            if (string.IsNullOrEmpty(id))
            {
                IdsException exception = new IdsException(Resources.StringParameterNullOrEmpty, new ArgumentException(Resources.IdString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            if (this.batchRequests.Count > 25)
            {
                IdsException exception = new IdsException(Resources.batchItemsExceededMessage, new BatchItemsExceededException(Resources.batchItemsExceededMessage));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            if (this.batchRequests.Count > 0 && this.batchRequests.Find(item => item.bId == id) != null)
            {
                IdsException exception = new IdsException(Resources.BatchIdAlreadyUsed, new ArgumentException(Resources.IdString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            BatchItemRequest batchItem = new BatchItemRequest();
            batchItem.AnyIntuitObject = query;
            batchItem.ItemElementName = ItemChoiceType8.CDCQuery;
            batchItem.bId = id;
            batchItem.operationSpecified = false;
            if (optionsData != null && optionsData.Count > 0) { batchItem.optionsData = string.Join(",", optionsData.ToArray()).ToLower(); }
            this.batchRequests.Add(batchItem);
        }

        /// <summary>
        /// Adds the specified entity operation.
        /// </summary>
        /// <param name="entity">entitiy for the batch operation.</param>
        /// <param name="id">Unique batchitem id</param>
        /// <param name="operation">operation to be performed for the entity.</param>
        public void Add(IEntity entity, string id, OperationEnum operation)
        {
            Add(entity, id, operation, null);
        }

        /// <summary>
        /// Adds the specified entity operation.
        /// </summary>
        /// <param name="entity">entitiy for the batch operation.</param>
        /// <param name="id">Unique batchitem id</param>
        /// <param name="operation">operation to be performed for the entity.</param>
        public void Add(IEntity entity, string id, OperationEnum operation, List<String> optionsData)
        {
            // Create and Add a new BatchItem
            if (entity == null)
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            if (string.IsNullOrEmpty(id))
            {
                IdsException exception = new IdsException(Resources.StringParameterNullOrEmpty, new ArgumentException(Resources.IdString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            if (this.batchRequests.Count > 25)
            {
                IdsException exception = new IdsException(Resources.batchItemsExceededMessage, new BatchItemsExceededException(Resources.batchItemsExceededMessage));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            if (this.batchRequests.Count > 0 && this.batchRequests.Find(item => item.bId == id) != null)
            {
                IdsException exception = new IdsException(Resources.BatchIdAlreadyUsed, new ArgumentException(Resources.IdString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            BatchItemRequest batchItem = new BatchItemRequest();
            batchItem.AnyIntuitObject = entity;
            batchItem.bId = id;
            batchItem.operation = operation;
            batchItem.operationSpecified = true;
            if (optionsData != null && optionsData.Count > 0) { batchItem.optionsData = string.Join(",", optionsData.ToArray()).ToLower(); }
            ItemChoiceType8 result;
            if (Enum.TryParse(entity.GetType().Name, out result))
            {
                batchItem.ItemElementName = result;
            }

            this.batchRequests.Add(batchItem);
        }


        /// <summary>
        ///  Removes batchitem with the specified batchitem id.
        /// </summary>
        /// <param name="id"> unique batchitem id</param>
        public void Remove(string id)
        {
            BatchItemRequest removeItem = this.batchRequests.FirstOrDefault(bid => bid.bId == id);
            if (removeItem == null)
            {
                IdsException exception = new IdsException(string.Format(CultureInfo.InvariantCulture, Resources.BatchItemIdNotFound, id));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }
            else
            {
                this.batchRequests.Remove(removeItem);
            }
        }

        /// <summary>
        /// Remove all the batchitem requests.
        /// </summary>
        public void RemoveAll()
        {
            this.batchRequests.Clear();
        }

        /// <summary>
        /// This method executes the batch request.
        /// </summary>
        public void Execute()
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Started Executing Method Execute for Batch");

            // Create Intuit Batch Request
            IntuitBatchRequest intuitBatchRequest = new IntuitBatchRequest();
            intuitBatchRequest.BatchItemRequest = this.batchRequests.ToArray<BatchItemRequest>();

            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/batch", Utility.CoreConstants.VERSION, this.serviceContext.RealmId);

            // Creates request parameters
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, Utility.CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, Utility.CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }
            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, intuitBatchRequest);

            string response = string.Empty;
            try
            {
                // gets response
                response = this.restHandler.GetResponse(request);
            }
            catch (IdsException ex)
            {
                IdsExceptionManager.HandleException(ex);
            }

            CoreHelper.CheckNullResponseAndThrowException(response);

            // de serialize object
            IntuitResponse restResponse = (IntuitResponse)this.responseSerializer.Deserialize<IntuitResponse>(response);
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Execute method for batch.");
            foreach (object obj in restResponse.AnyIntuitObjects)
            {
                BatchItemResponse batchItemResponse = obj as BatchItemResponse;
                this.batchResponses.Add(batchItemResponse);

                // process batch item
                this.intuitBatchItemResponses.Add(ProcessBatchItemResponse(batchItemResponse));
            }
        }

        /// <summary>
        /// This method executes the batch request Asynchronously. 
        /// </summary>
        public void ExecuteAsync()
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Started Executing Method ExecuteAsync for Batch");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(this.serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(this.BatchAsyncompleted);
            BatchExecutionCompletedEventArgs batchCompletedEventArgs = new BatchExecutionCompletedEventArgs();

            // Create Intuit Batch Request
            IntuitBatchRequest intuitBatchRequest = new IntuitBatchRequest();
            intuitBatchRequest.BatchItemRequest = this.batchRequests.ToArray<BatchItemRequest>();
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/batch", Utility.CoreConstants.VERSION, this.serviceContext.RealmId);

            // Creates request parameters
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, Utility.CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, Utility.CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }

            // Prepares request
            HttpWebRequest request = asyncRestHandler.PrepareRequest(parameters, intuitBatchRequest);

            try
            {
                // gets response
                asyncRestHandler.GetResponse(request);
            }
            catch (SystemException systemException)
            {
                IdsException idsException = new IdsException(systemException.Message);
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                batchCompletedEventArgs.Error = idsException;
                this.OnBatchExecuteAsyncCompleted(this, batchCompletedEventArgs);
            }
        }

        /// <summary>
        /// Prepare IdsException out of Fault object.
        /// </summary>
        /// <param name="fault">Fault object.</param>
        /// <returns> returns IdsException object.</returns>
        private static IdsException IterateFaultAndPrepareException(Fault fault)
        {
            if (fault == null)
            {
                return null;
            }

            IdsException idsException = null;

            // Create a list of exceptions.
            List<IdsError> aggregateExceptions = new List<IdsError>();

            // Check whether the fault is null or not.
            if (fault != null)
            {
                // Fault types can be of Validation, Service, Authentication and Authorization. Run them through the switch case.
                switch (fault.type)
                {
                    // If Validation errors iterate the Errors and add them to the list of exceptions.
                    case "Validation":
                    case "ValidationFault":
                        if (fault.Error != null && fault.Error.Count() > 0)
                        {
                            foreach (var item in fault.Error)
                            {
                                // Add commonException to aggregateExceptions
                                // CommonException defines four properties: Message, Code, Element, Detail.
                                aggregateExceptions.Add(new IdsError(item.Message, item.code, item.element, item.Detail));
                            }

                            // Throw specific exception like ValidationException.
                            idsException = new ValidationException(aggregateExceptions);
                        }

                        break;

                    // If Validation errors iterate the Errors and add them to the list of exceptions.
                    case "Service":
                    case "ServiceFault":
                        if (fault.Error != null && fault.Error.Count() > 0)
                        {
                            foreach (var item in fault.Error)
                            {
                                // Add commonException to aggregateExceptions
                                // CommonException defines four properties: Message, Code, Element, Detail.
                                aggregateExceptions.Add(new IdsError(item.Message, item.code, item.element, item.Detail));
                            }

                            // Throw specific exception like ServiceException.
                            idsException = new ServiceException(aggregateExceptions);
                        }

                        break;

                    // If Validation errors iterate the Errors and add them to the list of exceptions.
                    case "Authentication":
                    case "AuthenticationFault":
                    case "Authorization":
                    case "AuthorizationFault":
                        if (fault.Error != null && fault.Error.Count() > 0)
                        {
                            foreach (var item in fault.Error)
                            {
                                // Add commonException to aggregateExceptions
                                // CommonException defines four properties: Message, Code, Element, Detail.
                                aggregateExceptions.Add(new IdsError(item.Message, item.code, item.element, item.Detail));
                            }

                            // Throw specific exception like AuthenticationException which is wrapped in SecurityException.
                            idsException = new SecurityException(aggregateExceptions);
                        }

                        break;

                    // Use this as default if there was some other type of Fault
                    default:
                        if (fault.Error != null && fault.Error.Count() > 0)
                        {
                            foreach (var item in fault.Error)
                            {
                                // Add commonException to aggregateExceptions
                                // CommonException defines four properties: Message, Code, Element, Detail.
                                aggregateExceptions.Add(new IdsError(item.Message, item.code, item.element, item.Detail));
                            }

                            // Throw generic exception like IdsException.
                            idsException = new IdsException(string.Format(CultureInfo.InvariantCulture, "Fault Exception of type: {0} has been generated.", fault.type), aggregateExceptions);
                        }

                        break;
                }
            }

            // Return idsException which will be of type Validation, Service or Security.
            return idsException;
        }

        /// <summary>
        /// process batch item response
        /// </summary>
        /// <param name="batchitemResponse">The batchitem response.</param>
        /// <returns> returns IntuitBatchResponse object.</returns>
        private static IntuitBatchResponse ProcessBatchItemResponse(BatchItemResponse batchitemResponse)
        {
            IntuitBatchResponse result = new IntuitBatchResponse();
            result.Id = batchitemResponse.bId;
            Fault fault = batchitemResponse.AnyIntuitObject as Fault;
            if (fault == null)
            {
                CDCResponse cdcResponses = batchitemResponse.AnyIntuitObject as CDCResponse;
                if (cdcResponses != null)
                {
                    result.ResponseType = ResponseType.CdcQuery;
                    int count = 1;
                    IntuitCDCResponse returnValue = new IntuitCDCResponse();
                    object[] queryResponses = cdcResponses.AnyIntuitObjects;
                    if (queryResponses != null)
                    {

                        foreach (QueryResponse queryResponse in queryResponses)
                        {
                            Type type = queryResponse.GetType();
                            List<IEntity> entities = new List<IEntity>();

                            PropertyInfo[] propertyInfoArray = type.GetProperties();

                            foreach (PropertyInfo propertyInfo in propertyInfoArray)
                            {
                                if (true == propertyInfo.PropertyType.IsArray)
                                {
                                    object tempEntities = propertyInfo.GetValue(queryResponse, null);
                                    if (tempEntities != null)
                                    {
                                        object[] tempEntityArray = (object[])tempEntities;

                                        if (tempEntityArray.Length > 0)
                                        {
                                            List<IEntity> arr = new List<IEntity>();
                                            string entityName = string.Empty;
                                            foreach (object item in tempEntityArray)
                                            {
                                                entities.Add((IEntity)item);
                                                entityName = item.GetType().Name;
                                                count++;
                                            }
                                            returnValue.entities.Add(entityName, entities);
                                        }
                                        break;
                                    }
                                }
                            }
                            result.CDCResponse = returnValue;
                        }
                    }
                }
                else
                {
                    IEntity entity = batchitemResponse.AnyIntuitObject as IEntity;
                    if (entity == null)
                    {
                        QueryResponse queryResponse = batchitemResponse.AnyIntuitObject as QueryResponse;
                        if (queryResponse != null)
                        {
                            result.ResponseType = ResponseType.Query;
                            QueryResponse returnValue = new QueryResponse();
                            returnValue.totalCount = queryResponse.totalCount;
                            returnValue.totalCountSpecified = queryResponse.totalCountSpecified;
                            result.QueryResponse = returnValue;

                            if (queryResponse.AnyIntuitObjects != null && queryResponse.AnyIntuitObjects.Count() > 0)
                            {
                                foreach (object obj in queryResponse.AnyIntuitObjects)
                                {
                                    result.AddEntities(obj as IEntity);
                                }
                            }
                        }
                        else
                        {
                            //Not sure how we end up here
                        }
                    }
                    else
                    {
                        result.ResponseType = ResponseType.Entity;
                        result.Entity = entity;
                    }
                }
            }
            else
            {
                result.ResponseType = ResponseType.Exception;
                IdsException idsException = IterateFaultAndPrepareException(fault);
                result.Exception = idsException;
            }

            return result;
        }

        /// <summary>
        /// call back method for asynchronous batch execution. 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Intuit.Ipp.Core.AsyncCallCompletedEventArgs"/> instance containing the event data.</param>
        private void BatchAsyncompleted(object sender, AsyncCallCompletedEventArgs eventArgs)
        {
            BatchExecutionCompletedEventArgs batchCompletedEventArgs = new BatchExecutionCompletedEventArgs();

            try
            {
                if (eventArgs.Error == null)
                {
                    try
                    {
                        IEntitySerializer responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);
                        IntuitResponse restResponse = (IntuitResponse)responseSerializer.Deserialize<IntuitResponse>(eventArgs.Result);
                        foreach (object obj in restResponse.AnyIntuitObjects)
                        {
                            BatchItemResponse batchItemResponse = obj as BatchItemResponse;
                            this.batchResponses.Add(batchItemResponse);

                            // process batch item
                            this.intuitBatchItemResponses.Add(ProcessBatchItemResponse(batchItemResponse));
                        }

                        batchCompletedEventArgs.Batch = this;
                        this.OnBatchExecuteAsyncCompleted(this, batchCompletedEventArgs);
                    }
                    catch (SystemException systemException)
                    {
                        IdsException idsException = new IdsException("Batch execution failed", systemException);
                        this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                        batchCompletedEventArgs.Error = idsException;
                        this.OnBatchExecuteAsyncCompleted(this, batchCompletedEventArgs);
                    }
                }
                else
                {
                    batchCompletedEventArgs.Error = eventArgs.Error;
                    this.OnBatchExecuteAsyncCompleted(this, batchCompletedEventArgs);
                }
            }
            catch (Exception e)
            {
                IdsException idsException = new IdsException("Batch execution failed", e);
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                batchCompletedEventArgs.Error = idsException;
                this.OnBatchExecuteAsyncCompleted(this, batchCompletedEventArgs);
            }
        }

        #endregion
    }
}