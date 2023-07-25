////*********************************************************
// <copyright file="DataService.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2019 Intuit
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
// <summary>This file contains DataService performs CRUD operations on V3 QuickBooks APIs.</summary>
////*********************************************************

using System.Text.RegularExpressions;

namespace Intuit.Ipp.DataService
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using Intuit.Ipp.Core;
    using Intuit.Ipp.Core.Rest;
    using Intuit.Ipp.Data;
    using Intuit.Ipp.DataService.Properties;
    using Intuit.Ipp.Diagnostics;
    using Intuit.Ipp.Exception;
    using Intuit.Ipp.Utility;
    using System.Text;
    using System.IO;
    //using Intuit.Ipp.QueryFilter;
    //using Intuit.Ipp.LinqExtender;


    /// <summary>
    /// This class file contains DataService performs CRUD operations on V3 QuickBooks APIs.
    /// </summary>
    public class DataService : IDataService
    {
        /// <summary>
        /// The Service context object.
        /// </summary>
        private ServiceContext serviceContext;

        /// <summary>
        /// Rest Request Handler.
        /// </summary>
        private IRestHandler restHandler;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataService"/> class.
        /// </summary>
        /// <param name="serviceContext">IPP Service Context</param>
        public DataService(ServiceContext serviceContext)
        {
            ServiceContextValidation(serviceContext);
            this.serviceContext = serviceContext;
            this.restHandler = new SyncRestHandler(this.serviceContext);

            // Set the Service type to QBO by calling a method.
            this.serviceContext.UseDataServices();
        }

        #region Async handlers
        /// <summary>
        /// Gets or sets the call back event for find all method in asynchronous call.
        /// </summary>
        /// <value>
        /// The OnFindAllCompleted call back.
        /// </value>
        public DataServiceCallback<IEntity>.FindAllCallCompletedEventHandler OnFindAllAsyncCompleted { get; set; }

        /// <summary>
        /// Gets or sets the call back event for Add method in asynchronous call.
        /// </summary>
        /// <value>
        /// The OnAddAsyncCompleted call back.
        /// </value>
        public DataServiceCallback<IEntity>.CallCompletedEventHandler OnAddAsyncCompleted { get; set; }

        /// <summary>
        /// Gets or sets the call back event for FindByid method in asynchronous call.
        /// </summary>
        /// <value>
        /// The OnFindByIdAsyncCompleted call back.
        /// </value>
        public DataServiceCallback<IEntity>.CallCompletedEventHandler OnFindByIdAsyncCompleted { get; set; }

        /// <summary>
        /// Gets or sets the call back event for FindByLevel method in asynchronous call.
        /// </summary>
        /// <value>
        /// The OnFindByLevelAsyncCompleted call back.
        /// </value>
        public DataServiceCallback<IEntity>.FindAllCallCompletedEventHandler OnFindByLevelAsyncCompleted { get; set; }

        /// <summary>
        /// Gets or sets the call back event for FindByParentId method in asynchronous call.
        /// </summary>
        /// /// <value>
        /// The OnFindByParentIdAsyncCompleted call back.
        /// </value>
        public DataServiceCallback<IEntity>.FindAllCallCompletedEventHandler OnFindByParentIdAsyncCompleted { get; set; }

        /// <summary>
        /// Gets or sets the call back event for GetPdf method in asynchronous call.
        /// </summary>
        /// <value>
        /// The OnGetPdfAsyncCompleted call back.
        /// </value>
        public DataServiceCallback<IEntity>.PdfCallCompletedEventHandler OnGetPdfAsyncCompleted { get; set; }

        /// <summary>
        /// Gets or sets the call back event for SendEmail method in asynchronous call.
        /// </summary>
        /// <value>
        /// The OnSendEmailAsyncCompleted call back.
        /// </value>
        public DataServiceCallback<IEntity>.CallCompletedEventHandler OnSendEmailAsyncCompleted { get; set; }

        /// <summary>
        /// Gets or sets the call back event for Update method in asynchronous call.
        /// </summary>
        /// <value>
        /// The OnUpdateAsyncCompleted call back.
        /// </value>
        public DataServiceCallback<IEntity>.CallCompletedEventHandler OnUpdateAsyncCompleted { get; set; }

        /// <summary>
        /// Gets or sets the call back event for Update account method in asynchronous call.
        /// </summary>
        /// <value>
        /// The OnUpdateAccAsyncCompleted call back.
        /// </value>
        public DataServiceCallback<IEntity>.CallCompletedEventHandler OnUpdateAccAsyncCompleted { get; set; }

        /// <summary>
        /// Gets or sets the call back event for Update account method in asynchronous call.
        /// </summary>
        /// <value>
        /// The OnDoNotUpdateAccAsyncCompleted call back.
        /// </value>
        public DataServiceCallback<IEntity>.CallCompletedEventHandler OnDoNotUpdateAccAsyncCompleted { get; set; }

        /// <summary>
        /// Gets or sets the call back event for Delete method in asynchronous call.
        /// </summary>
        /// <value>
        /// The OnDeleteAsyncCompleted call back.
        /// </value>
        public DataServiceCallback<IEntity>.CallCompletedEventHandler OnDeleteAsyncCompleted { get; set; }

        /// <summary>
        /// Gets or sets the call back event for Delete method in asynchronous call.
        /// </summary>
        /// <value>
        /// The OnDeleteAsyncCompleted call back.
        /// </value>
        public DataServiceCallback<IEntity>.CallCompletedEventHandler OnVoidAsyncCompleted { get; set; }

        /// <summary>
        /// Gets or sets the call back event for Revert method in asynchronous call.
        /// </summary>
        /// <value>
        /// The OnRevertAsyncCompleted call back.
        /// </value>
        public DataServiceCallback<IEntity>.CallCompletedEventHandler OnRevertAsyncCompleted { get; set; }

        /// <summary>
        /// Gets or sets the call back event for CDC method in asynchronous call.
        /// </summary>
        /// <value>
        /// The OnCDCCompleted call back.
        /// </value>
        public DataServiceCallback<IEntity>.CDCCallCompletedEventHandler OnCDCAsyncCompleted { get; set; }


        #endregion

        #region Add

        /// <summary>
        /// Adds an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Add.</param>
        /// <returns>Returns an updated version of the entity with updated identifier and sync token.</returns>
        public T Add<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Add.");

            // Validate parameter
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            if (resourceString == "creditcardpaymenttxn")
            {
                resourceString = "creditcardpayment";
            }

            // Builds resource Uri
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

            // Creates request parameters
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }

            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, entity);

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
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(response);
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Add.");
            return (T)(restResponse.AnyIntuitObject as IEntity);
        }

        #endregion

        #region Delete, Void
        /// <summary>
        /// Deletes an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Delete.</param>
        public T Delete<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Delete.");

            // Validate parameter
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            if (resourceString == "creditcardpaymenttxn")
            {
                resourceString = "creditcardpayment";
            }
            // Builds resource Uri
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}?operation=delete", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

            // Creates request parameters
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }

            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, entity);

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
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(response);
            IntuitEntity intuitEntity = restResponse.AnyIntuitObject as IntuitEntity;

            if (intuitEntity != null && intuitEntity.status != EntityStatusEnum.Deleted)
            {
                IdsException exception = new IdsException(Resources.CommunicationErrorMessage, new CommunicationException(Resources.StatusNotDeleted));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Void.");
            return (T)(restResponse.AnyIntuitObject as IEntity);
        }

        /// <summary>
        /// Voids an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Void (only entities of type Sales Receipt and Payment are supported to be voided)</param>
        /// <returns name="T">Returns the voided entity</returns>
        public T Void<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Void.");

            // Validate parameter
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            CheckForVoidAllowedEntities(entity);
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);

            // Builds resource Uri
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}?operation=void", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

            // Creates request parameters
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }

            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, entity);

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
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(response);

            if (restResponse.AnyIntuitObjects != null)
            {
                IntuitEntity intuitEntity = restResponse.AnyIntuitObject as IntuitEntity;

                if (restResponse != null && restResponse.status != IntuitResponseStatus.Deleted.ToString())
                {
                    IdsException exception = new IdsException(Resources.CommunicationErrorMessage, new CommunicationException(Resources.StatusNotVoided));
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                    IdsExceptionManager.HandleException(exception);
                }
            }

            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Void.");
            return (T)(restResponse.AnyIntuitObject as IEntity);
        }
        #endregion

        #region Update

        /// <summary>
        /// Updates an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Update.</param>
        /// <returns>Returns an updated version of the entity with updated identifier and sync token.</returns>
        public T Update<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Update.");

            // Validate parameter
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            if (resourceString == "creditcardpaymenttxn")
            {
                resourceString = "creditcardpayment";
            }
            // Builds resource Uri
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

            // Creates request parameters
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }

            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, entity);

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
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(response);
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Add.");
            return (T)(restResponse.AnyIntuitObject as IEntity);
        }

        #endregion

        #region updateaccountontxns

        /// <summary>
        /// updateaccountontxns an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Update.</param>
        /// <returns>Returns an updated version of the entity with updated identifier and sync token.</returns>
        public T UpdateAccountOnTxns<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Add.");

            // Validate parameter
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);

            // Builds resource Uri
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}?include=updateaccountontxns", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

            // Creates request parameters
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }

            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, entity);

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
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(response);
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Add.");
            return (T)(restResponse.AnyIntuitObject as IEntity);
        }

        #endregion

        #region donotupdateaccountontxns

        /// <summary>
        /// donotupdateaccountontxns an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Update.</param>
        /// <returns>Returns an updated version of the entity with updated identifier and sync token.</returns>
        public T DoNotUpdateAccountOnTxns<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Add.");

            // Validate parameter
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);

            // Builds resource Uri
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}?include=donotupdateaccountontxns", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

            // Creates request parameters
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }

            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, entity);

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
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(response);
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Add.");
            return (T)(restResponse.AnyIntuitObject as IEntity);
        }

        #endregion

        #region Read

        #region PDF

        /// <summary>
        /// Returns an entity as pdf bytes.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to be returned as pdf bytes (entities of type Sales Receipt, Invoice and Estimate are supported to be returned as pdf).</param>
        /// <returns type="byte[]">Returns pdf as bytes</returns>
        public byte[] GetPdf<T>(T entity) where T : IEntity
        {
            // Validate parameter
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method GetPdf by " + entity.GetType().FullName);

            string id = string.Empty;
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);

            // Convert to role base to get the Id property which is required to Find the entity.
            IntuitEntity intuitEntity = entity as IntuitEntity;
            if (intuitEntity == null)
            {
                IdsException exception = new IdsException(Resources.EntityConversionFailedMessage);
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            // Check whether the Id is null and throw an exception if it is null.
            if (string.IsNullOrWhiteSpace(intuitEntity.Id) && (entity.GetType().Name != "Preferences"))
            {
                IdsException exception = new IdsException(Resources.EntityIdNotNullMessage, new ArgumentNullException(Resources.IdString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            //check if the operation is allowed on the entity
            CheckForPdfAllowedEntities(entity);

            id = intuitEntity.Id;

            //build the url to be called
            string uri = string.Empty;
            uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}/{3}/pdf", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString, id);

            // Creates request parameters
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat ==
                Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }

            // Prepare request
            HttpWebRequest request = this.restHandler.PrepareRequest(requestParameters: parameters, requestBody: null, includeRequestId: false);
            request.Accept = CoreConstants.CONTENTTYPE_APPLICATIONPDF;

            //Exception for Download, it does not accept "Accept" header
            //request.Accept = null;
            byte[] response = new byte[0];
            try
            {
                // Gets response
                response = this.restHandler.GetResponseStream(request);
            }
            catch (IdsException ex)
            {
                IdsExceptionManager.HandleException(ex);
            }

            return response;
        }


        #endregion

        #region Email
        /// <summary>
        /// Call the synchronous methods to send entity of type T in an email.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Instance of entity to be  sent in an email. This is not the actual entity that will be emailed. This entity needs to be present on server and the entity on the server will be sent in an email.Any changes in the passed in entity must be committed to the server in order for it to reflect in the email. Entities of type Sales Receipt, Invoice and Estimate are supported to be sent in an email as pdf</param>
        /// <param name="sendToEmail">Optional parameter to specify an email address</param>
        /// <returns name="T">Retruns the entity sent in email</returns>
        public T SendEmail<T>(T entity, string sendToEmail = null) where T : IEntity
        {
            // Validate parameter
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method SendEmail by " + entity.GetType().FullName);

            string id = string.Empty;
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);

            // Convert to role base to get the Id property which is required to Find the entity.
            IntuitEntity intuitEntity = entity as IntuitEntity;
            if (intuitEntity == null)
            {
                IdsException exception = new IdsException(Resources.EntityConversionFailedMessage);
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            // Check whether the Id is null and throw an exception if it is null.
            if (string.IsNullOrWhiteSpace(intuitEntity.Id) && (entity.GetType().Name != "Preferences"))
            {
                IdsException exception = new IdsException(Resources.EntityIdNotNullMessage, new ArgumentNullException(Resources.IdString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            //check if the operation is allowed on the entity
            CheckForPdfAllowedEntities(entity);

            //check if email address is valid
            ProcessSendToEmail(sendToEmail);
            id = intuitEntity.Id;

            //build the url to be called
            string uri = string.Empty;

            //IF sendtoemail is specidfied that takes priority and is used to send the email to, if not specified it uses the email from BillEmail.Address from the entity saved on the server and not from the passes in entity
            uri = String.IsNullOrWhiteSpace(sendToEmail) ? string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}/{3}/send", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString, id) : string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}/{3}/send?sendTo={4}", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString, id, sendToEmail);

            // Creates request parameters
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat ==
                Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }

            // Prepare request
            HttpWebRequest request = this.restHandler.PrepareRequest(requestParameters: parameters, requestBody: null, includeRequestId: false);
            request.ContentType = CoreConstants.CONTENTTYPE_APPLICATIONOCTETSTREAM;

            string response = string.Empty;
            try
            {
                // Gets response
                response = this.restHandler.GetResponse(request);
            }
            catch (IdsException ex)
            {
                IdsExceptionManager.HandleException(ex);
            }

            CoreHelper.CheckNullResponseAndThrowException(response);

            // De serialize object
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(response);

            object value = restResponse.AnyIntuitObject;
            if (value != null)
            {
                return (T)(value as IEntity);
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Checks if email address is valid
        /// </summary>
        /// <param name="sendToEmail">email address</param>
        private void ProcessSendToEmail(String sendToEmail)
        {
            //if email address is null return no need to check
            if (String.IsNullOrWhiteSpace(sendToEmail)) return;

            //check if the email address is empty or null
            if (IsValidEmailAddress(sendToEmail)) return;

            IdsException exception = new IdsException(Resources.EmailAddressNotValid,
                    new ArgumentNullException(Resources.IdString));
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error,
                string.Format(
                    CultureInfo.InvariantCulture,
                    Resources.EmailAddressNotValidExceptionMessage,
                    exception.ToString()));
            IdsExceptionManager.HandleException(exception);
        }

        /// <summary>
        /// Validates email address through Regex 
        /// </summary>
        /// <param name="sendToEmail">email address</param>
        private bool IsValidEmailAddress(String emailAddress)
        {
            return !String.IsNullOrWhiteSpace(emailAddress) && Regex.IsMatch(emailAddress,
                @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))"
                + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                RegexOptions.IgnoreCase);
        }

        #endregion

        /// <summary>
        /// Returns an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity"> Entity type to Find.</param>    
        /// <returns> Returns an entity of specified Id.</returns> 
        public T FindById<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method FindById.");

            // Validate parameter
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            string id = string.Empty;
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            if (resourceString == "creditcardpaymenttxn")
            {
                resourceString = "creditcardpayment";
            }


            // Convert to role base to get the Id property which is required to Find the entity.
            IntuitEntity intuitEntity = entity as IntuitEntity;
            if (intuitEntity == null)
            {
                IdsException exception = new IdsException(Resources.EntityConversionFailedMessage);
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            // Check whether the Id is null and throw an exception if it is null.
            if (string.IsNullOrWhiteSpace(intuitEntity.Id) && (entity.GetType().Name != "Preferences"))
            {
                IdsException exception = new IdsException(Resources.EntityIdNotNullMessage, new ArgumentNullException(Resources.IdString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            id = intuitEntity.Id;




            string uri = string.Empty;

            if (resourceString.Equals("preferences"))

            {
                uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);
            }
            else
            {
                uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}/{3}", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString, id);
            }

            // Creates request parameters
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat ==
                Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }

            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, null);
            string response = string.Empty;
            try
            {
                // Gets response
                response = this.restHandler.GetResponse(request);
            }
            catch (IdsException ex)
            {
                IdsExceptionManager.HandleException(ex);
            }

            CoreHelper.CheckNullResponseAndThrowException(response);

            // De serialize object
            IntuitResponse restResponse =
                (IntuitResponse)
                CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(response);

            object value = restResponse.AnyIntuitObject;
            if (value != null)
            {
                return (T)(value as IEntity);
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Returns entities by the Parent Id specified, supported for TaxClassification only.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ReadOnlyCollection<T> FindByParentId<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method FindByParentId.");

            ServicesHelper.ValidateEntity(entity, serviceContext);
            ServicesHelper.ValidateEntityType(entity, "TaxClassification", serviceContext);

            string parentId = string.Empty;
            ReferenceType parentRef = ServicesHelper.PrepareByParentId(entity, serviceContext);
            ServicesHelper.ValidateObject(parentRef, serviceContext);
            parentId = parentRef.Value;

            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);

            // Convert to role base to get the Id property which is required to Find the entity.
            IntuitEntity intuitEntity = entity as IntuitEntity;
            ServicesHelper.ValidateIntuitEntity(intuitEntity, serviceContext);

            // Check whether the Id is null and throw an exception if it is null.
            ServicesHelper.ValidateId(parentId, serviceContext);

            string uri = string.Empty;
            uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}/{3}/children", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString, parentId);

            List<T> entities = PrepareAndExecuteHttpRequest<T>(uri);

            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method FindByParentId.");

            ReadOnlyCollection<T> readOnlyCollection = new ReadOnlyCollection<T>(entities);
            return readOnlyCollection;
        }

        /// <summary>
        /// Returns entities by the Level specified, supported for TaxClassification only.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ReadOnlyCollection<T> FindByLevel<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method FindByLevel.");

            // Validate parameter
            ServicesHelper.ValidateEntity(entity, serviceContext);
            ServicesHelper.ValidateEntityType(entity, "TaxClassification", serviceContext);

            string level = string.Empty;
            level = ServicesHelper.PrepareByLevel(entity, serviceContext);

            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);

            // Convert to role base to get the Id property which is required to Find the entity.
            IntuitEntity intuitEntity = entity as IntuitEntity;
            ServicesHelper.ValidateIntuitEntity(intuitEntity, serviceContext);

            // Check whether the Level is null and throw an exception if it is null.
            ServicesHelper.ValidateId(level, serviceContext);

            string uri = string.Empty;
            uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}?level={3}", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString, level);

            List<T> entities = PrepareAndExecuteHttpRequest<T>(uri);

            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method FindByLevel.");

            ReadOnlyCollection<T> readOnlyCollection = new ReadOnlyCollection<T>(entities);
            return readOnlyCollection;
        }

        /// <summary>
        /// Returns a list of all entities of type T under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">The entity for which the data is required.</param>
        /// <param name="startPosition">The start position to retrieve.</param>
        /// <param name="maxResults">Maximum no. of results to retrieve</param>
        /// <returns> Returns the list of entities.</returns>
        public ReadOnlyCollection<T> FindAll<T>(T entity, int startPosition = 1, int maxResults = 500) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method FindAll.");

            ServicesHelper.ValidateEntity(entity, serviceContext);
            string resourceString = entity.GetType().Name;

            if (resourceString.ToLower(CultureInfo.InvariantCulture) == "creditcardpaymenttxn")
            {
                resourceString = "creditcardpayment";
            }
            List<T> entities = new List<T>();

            if (resourceString == "TaxClassification")
            {
                string uri = string.Empty;
                uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString.ToLower(CultureInfo.InvariantCulture));

                entities = PrepareAndExecuteHttpRequest<T>(uri);
            }
            else
            {
                if (startPosition <= 0)
                {
                    IdsException exception = new IdsException(Resources.ParameterZeroNegativeValueMessage, new ArgumentException(Resources.PageNumberString));
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                    IdsExceptionManager.HandleException(exception);
                }

                if (maxResults <= 0)
                {
                    IdsException exception = new IdsException(Resources.ParameterZeroNegativeValueMessage, new ArgumentException(Resources.PageSizeString));
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                    IdsExceptionManager.HandleException(exception);
                }

                // Gets the resource name to be added to the resource Uri
                string query = string.Format(CultureInfo.InvariantCulture, "select * from {0} startPosition {1} maxResults {2}", resourceString, startPosition, maxResults);
                string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/query", CoreConstants.VERSION, this.serviceContext.RealmId);

                // Creates request parameters
                RequestParameters parameters = null;

                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONTEXT);


                // Prepares request
                HttpWebRequest request = this.restHandler.PrepareRequest(parameters, query);
                string response = string.Empty;
                try
                {
                    // Gets response
                    response = this.restHandler.GetResponse(request);
                }
                catch (IdsException ex)
                {
                    IdsExceptionManager.HandleException(ex);
                }

                CoreHelper.CheckNullResponseAndThrowException(response);

                // Deserialize object
                IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(response);
                QueryResponse queryResponse = restResponse.AnyIntuitObject as QueryResponse;

                if (queryResponse.maxResults > 0)
                {
                    object tempEntities = queryResponse.AnyIntuitObjects;
                    object[] tempEntityArray = (object[])tempEntities;

                    if (tempEntityArray.Length > 0)
                    {
                        foreach (object item in tempEntityArray)
                        {
                            entities.Add((T)item);
                        }
                    }
                }

                /*            Type type = queryResponse.GetType();
                            List<T> entities = new List<T>();
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
                                            foreach (object item in tempEntityArray)
                                            {
                                                entities.Add((T)item);
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                            */
                //this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method FindAll.");

                //System.Collections.ObjectModel.ReadOnlyCollection<T> readOnlyCollection = new System.Collections.ObjectModel.ReadOnlyCollection<T>(entities);
                //return readOnlyCollection;
            }

            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method FindAll.");

            ReadOnlyCollection<T> readOnlyCollection = new ReadOnlyCollection<T>(entities);
            return readOnlyCollection;
        }

        #endregion

        #region Batch

        /// <summary>
        /// Creates new batch
        /// </summary>
        /// <returns> returns the batch object</returns>
        public Batch CreateNewBatch()
        {
            Batch batch = new Batch(this.serviceContext, this.restHandler);
            return batch;
        }

        #endregion

        #region CDC

        /// <summary>
        /// Returns List of entities changed after certain time.
        /// </summary>
        /// <param name="entityList"> List of entity.</param>    
        /// <param name="changedSince"> DateTime of timespan after which entities were changed.</param>    
        /// <returns> Returns an IntuitCDCResponse.</returns> 
        public IntuitCDCResponse CDC(List<IEntity> entityList, DateTime changedSince)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method CDC.");

            // Validate parameter
            if (entityList.Count <= 0)
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            string entityString = string.Empty;
            foreach (IEntity entity in entityList)
            {
                if (entityString.Equals(string.Empty))
                {
                    entityString = entity.GetType().Name;
                }
                else
                {
                    entityString = entityString + "," + entity.GetType().Name;
                }
            }
            string query = string.Empty;
            string uri = string.Empty;

            {
                query = "entities=" + entityString + "&changedSince=" + changedSince.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");
                uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/cdc?{2}", CoreConstants.VERSION, this.serviceContext.RealmId, query);
            }

            // Creates request parameters
            RequestParameters parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);

            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, null);
            string response = string.Empty;
            try
            {
                // Gets response
                response = this.restHandler.GetResponse(request);
            }
            catch (IdsException ex)
            {
                IdsExceptionManager.HandleException(ex);
            }

            CoreHelper.CheckNullResponseAndThrowException(response);

            // De serialize object
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(response);
            object[] cdcResponses = restResponse.AnyIntuitObjects;

            IntuitCDCResponse returnValue = new IntuitCDCResponse();

            foreach (CDCResponse cdcResponse in cdcResponses)
            {
                object[] queryResponses = cdcResponse.AnyIntuitObjects;

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
                                        }
                                        returnValue.entities.Add(entityName, entities);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method CDC.");
            return returnValue;
        }

        #endregion

        #region Async methods

        #region Async Read
        /// <summary>
        /// Retrieves specified entity based passed page number and page size
        /// </summary>
        /// <typeparam name="T">Entity to be retrieved</typeparam>
        /// <param name="entity">Instance of entity to be retrieved</param>
        /// <param name="startPosition">The start position.</param>
        /// <param name="maxResults">The max results.</param>
        public void FindAllAsync<T>(T entity, int startPosition = 1, int maxResults = 500) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method FindAll Asynchronously.");
            FindAllCallCompletedEventArgs findAllCompletedEventArgs = new FindAllCallCompletedEventArgs();
            //// Validate parameter
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                findAllCompletedEventArgs.Error = exception;
                this.OnFindAllAsyncCompleted(this, findAllCompletedEventArgs);
                return;
            }

            if (entity.GetType().Name == "TaxClassification")
            {
                try
                {
                    AsyncService asyncService = new AsyncService(this.serviceContext);
                    asyncService.OnFindAllAsynCompleted += new DataServiceCallback<IEntity>.FindAllCallCompletedEventHandler(this.FindAllAsyncCompleted);
                    asyncService.FindAllAsync<T>(entity, startPosition, maxResults);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);
                    findAllCompletedEventArgs.Error = idsException;
                    this.OnFindAllAsyncCompleted(this, findAllCompletedEventArgs);
                }
            }
            else
            {
                if (startPosition <= 0)
                {
                    IdsException exception = new IdsException(Resources.ParameterZeroNegativeValueMessage, new ArgumentException(Resources.PageNumberString));
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                    findAllCompletedEventArgs.Error = exception;
                    this.OnFindAllAsyncCompleted(this, findAllCompletedEventArgs);
                    return;
                }

                if (maxResults <= 0)
                {
                    IdsException exception = new IdsException(Resources.ParameterZeroNegativeValueMessage, new ArgumentException(Resources.PageSizeString));
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                    findAllCompletedEventArgs.Error = exception;
                    this.OnFindAllAsyncCompleted(this, findAllCompletedEventArgs);
                    return;
                }

                try
                {
                    AsyncService asyncService = new AsyncService(this.serviceContext);
                    asyncService.OnFindAllAsynCompleted += new DataServiceCallback<IEntity>.FindAllCallCompletedEventHandler(this.FindAllAsyncCompleted);
                    asyncService.FindAllAsync<T>(entity, startPosition, maxResults);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);
                    findAllCompletedEventArgs.Error = idsException;
                    this.OnFindAllAsyncCompleted(this, findAllCompletedEventArgs);
                }
            }
        }

        /// <summary>
        /// Retrieves specified entities based on passed Level, supported for TaxClassification only.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void FindByLevelAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method FindByLevelAsync.");
            FindAllCallCompletedEventArgs callCompletedEventArgs = new FindAllCallCompletedEventArgs();

            try
            {
                AsyncService asyncService = new AsyncService(this.serviceContext);
                asyncService.OnFindByLevelAsyncCompleted += new DataServiceCallback<IEntity>.FindAllCallCompletedEventHandler(this.FindByLevelAsyncCompleted);
                asyncService.FindByLevelAsync<T>(entity);
            }
            catch (SystemException systemException)
            {
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                callCompletedEventArgs.Error = idsException;
                this.OnFindByLevelAsyncCompleted(this, callCompletedEventArgs);
            }
        }

        /// <summary>
        /// Retrieves specified entities based on passed ParentId, supported for TaxClassification only.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public void FindByParentIdAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method FindByParentIdAsync.");
            FindAllCallCompletedEventArgs callCompletedEventArgs = new FindAllCallCompletedEventArgs();

            try
            {
                AsyncService asyncService = new AsyncService(this.serviceContext);
                asyncService.OnFindByParentIdAsyncCompleted += new DataServiceCallback<IEntity>.FindAllCallCompletedEventHandler(this.FindByParentIdAsyncCompleted);
                asyncService.FindByParentIdAsync<T>(entity);
            }
            catch (SystemException systemException)
            {
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                callCompletedEventArgs.Error = idsException;
                this.OnFindByParentIdAsyncCompleted(this, callCompletedEventArgs);
            }
        }

        /// <summary>
        /// Call the Asynchronous methods to get a particular entity details.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Instance of entity to find</param>
        public void FindByIdAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method FindById Asynchronously.");
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                callCompletedEventArgs.Error = exception;
                this.OnFindByIdAsyncCompleted(this, callCompletedEventArgs);
            }
            else
            {
                try
                {
                    AsyncService asyncService = new AsyncService(this.serviceContext);
                    asyncService.OnFindByIdAsynCompleted += new DataServiceCallback<IEntity>.CallCompletedEventHandler(this.FindByIdAsynCompleted);
                    asyncService.FindByIdAsync<T>(entity);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);
                    callCompletedEventArgs.Error = idsException;
                    this.OnFindByIdAsyncCompleted(this, callCompletedEventArgs);
                }
            }
        }

        /// <summary>
        /// Call the Asynchronous methods to get a particular entity as pdf.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to be returned as pdf bytes (entities of type Sales Receipt, Invoice and Estimate are supported to be returned as pdf).</param>
        public void GetPdfAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method GetPdf Asynchronously.");
            PdfCallCompletedEventArgs pdfCallCompletedEventArgs = new PdfCallCompletedEventArgs();
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                pdfCallCompletedEventArgs.Error = exception;
                this.OnGetPdfAsyncCompleted(this, pdfCallCompletedEventArgs);
            }
            else
            {
                //check if the operation is allowed on the entity
                CheckForPdfAllowedEntities(entity);

                try
                {
                    AsyncService asyncService = new AsyncService(this.serviceContext);
                    asyncService.OnGetPdfAsynCompleted += new DataServiceCallback<IEntity>.PdfCallCompletedEventHandler(this.GetPdfAsynCompleted);
                    asyncService.GetPdfAsync<T>(entity);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);
                    pdfCallCompletedEventArgs.Error = idsException;
                    this.OnGetPdfAsyncCompleted(this, pdfCallCompletedEventArgs);
                }
            }
        }

        /// <summary>
        /// Call the Asynchronous methods to send entity of type T in an email.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Instance of entity to be  sent in an email. This is not the actual entity that will be emailed. This entity needs to be present on server and the entity on the server will be sent in an email.Any changes in the passed in entity must be committed to the server in order for it to reflect in the email. Entities of type Sales Receipt, Invoice and Estimate are supported to be sent in an email as pdf</param>
        /// <param name="sendToEmail">Optional parameter to specify an email address</param>
        public void SendEmailAsync<T>(T entity, String sendToEmail = null) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method SendEmail Asynchronously.");
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                callCompletedEventArgs.Error = exception;
                this.OnSendEmailAsyncCompleted(this, callCompletedEventArgs);
            }
            else
            {
                //check if the operation is allowed on the entity
                CheckForPdfAllowedEntities(entity);

                //check if email address is valid
                ProcessSendToEmail(sendToEmail);

                try
                {
                    AsyncService asyncService = new AsyncService(this.serviceContext);
                    asyncService.OnSendEmailAsynCompleted += new DataServiceCallback<IEntity>.CallCompletedEventHandler(this.SendEmailAsynCompleted);
                    asyncService.SendEmailAsync<T>(entity, sendToEmail);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);
                    callCompletedEventArgs.Error = idsException;
                    this.OnSendEmailAsyncCompleted(this, callCompletedEventArgs);
                }
            }
        }

        /// <summary>
        /// Checks for PDF allowed entities
        /// </summary>
        /// <param name="entity">Entity to check</param>
        private void CheckForPdfAllowedEntities(IEntity entity)
        {
            Type givenType = entity.GetType();

            if (!(givenType == typeof(SalesReceipt) || givenType == typeof(Invoice) || givenType == typeof(Estimate) || (givenType == typeof(CreditMemo) || givenType == typeof(RefundReceipt) || givenType == typeof(PurchaseOrder) || givenType == typeof(Payment))))
            {
                IdsException exception = new IdsException(Resources.PdfOperationNotSupportedOnEntity);
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));

                IdsExceptionManager.HandleException(exception);
            }
        }

        /// <summary>
        /// Checks for void allowed entities
        /// </summary>
        /// <param name="entity">Entity to check</param>
        private void CheckForVoidAllowedEntities(IEntity entity)
        {
            Type givenType = entity.GetType();

            if (!(givenType == typeof(SalesReceipt) || givenType == typeof(Payment) || givenType == typeof(Invoice)))
            {
                IdsException exception = new IdsException(Resources.VoidOperationNotSupportedOnEntity);
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));

                IdsExceptionManager.HandleException(exception);
            }
        }

        /// <summary>
        /// Retrieves entities changed since particular Date
        /// </summary>
        /// <param name="entityList">List of entities to be retrieved</param>
        /// <param name="changedSince">date after which entities changed.</param>
        public void CDCAsync(List<IEntity> entityList, DateTime changedSince)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method CDC Asynchronously.");
            CDCCallCompletedEventArgs cdcCompletedEventArgs = new CDCCallCompletedEventArgs();
            //// Validate parameter
            if (entityList.Count <= 0)
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                cdcCompletedEventArgs.Error = exception;
                this.OnCDCAsyncCompleted(this, cdcCompletedEventArgs);
                return;
            }

            try
            {
                AsyncService asyncService = new AsyncService(this.serviceContext);
                asyncService.OnCDCAsynCompleted += new DataServiceCallback<IEntity>.CDCCallCompletedEventHandler(this.CDCAsyncCompleted);
                asyncService.CDCAsync(entityList, changedSince);
            }
            catch (SystemException systemException)
            {
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                cdcCompletedEventArgs.Error = idsException;
                this.OnCDCAsyncCompleted(this, cdcCompletedEventArgs);
            }
        }

        #endregion

        #region Async add
        /// <summary>
        /// Adds specified new entity
        /// </summary>
        /// <typeparam name="T">Entity to be added</typeparam>
        /// <param name="entity">Instance of entity to be added</param>
        public void AddAsync<T>(T entity) where T : IEntity
        {
            Console.Write("AddAsync started \n");
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Add Asynchronously.");
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            Console.Write("callCompletedEventArgs instantiated \n");
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                Console.Write("IdsException instantiated \n");
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                callCompletedEventArgs.Error = exception;
                this.OnAddAsyncCompleted(this, callCompletedEventArgs);
            }
            else
            {
                try
                {
                    AsyncService asyncService = new AsyncService(this.serviceContext);
                    asyncService.OnAddAsyncCompleted += new DataServiceCallback<IEntity>.CallCompletedEventHandler(this.AddAsyncCompleted);
                    asyncService.AddAsync<IEntity>(entity as IEntity);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);
                    callCompletedEventArgs.Error = idsException;
                    this.OnAddAsyncCompleted(this, callCompletedEventArgs);
                }
            }
        }

        #endregion

        #region Async Delete Void
        /// <summary>
        /// Deletes an entity under the specified realm. The realm must be set in the context
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Instance of entity to Delete</param>
        public void DeleteAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method delete Asynchronously.");
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                callCompletedEventArgs.Error = exception;
                this.OnDeleteAsyncCompleted(this, callCompletedEventArgs);
            }
            else
            {
                try
                {
                    AsyncService asyncService = new AsyncService(this.serviceContext);
                    asyncService.OnDeleteAsynCompleted += new DataServiceCallback<IEntity>.CallCompletedEventHandler(this.DeleteAsyncCompleted);
                    asyncService.DeleteAsync<IEntity>(entity as IEntity);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);
                    callCompletedEventArgs.Error = idsException;
                    this.OnDeleteAsyncCompleted(this, callCompletedEventArgs);
                }
            }
        }

        /// <summary>
        /// Async function that Voids an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Void (only entities of type Sales Receipt and Payment are supported to be voided)</param>
        public void VoidAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Void Asynchronously.");
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                callCompletedEventArgs.Error = exception;
                this.OnVoidAsyncCompleted(this, callCompletedEventArgs);
            }
            else
            {
                CheckForVoidAllowedEntities(entity);
                try
                {
                    AsyncService asyncService = new AsyncService(this.serviceContext);
                    asyncService.OnVoidAsynCompleted += new DataServiceCallback<IEntity>.CallCompletedEventHandler(this.VoidAsyncCompleted);
                    asyncService.VoidAsync<IEntity>(entity as IEntity);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);
                    callCompletedEventArgs.Error = idsException;
                    this.OnVoidAsyncCompleted(this, callCompletedEventArgs);
                }
            }
        }

        #endregion

        #region Async update
        /// <summary>
        /// Updates an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Returns an updated version of the entity with updated identifier and sync token</param>
        public void UpdateAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Update Asynchronously.");
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                callCompletedEventArgs.Error = exception;
                this.OnUpdateAsyncCompleted(this, callCompletedEventArgs);
            }
            else
            {
                try
                {
                    AsyncService asyncService = new AsyncService(this.serviceContext);
                    asyncService.OnUpdateAsynCompleted += new DataServiceCallback<IEntity>.CallCompletedEventHandler(this.UpdateAsyncCompleted);
                    asyncService.UpdateAsync<IEntity>(entity as IEntity);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);
                    callCompletedEventArgs.Error = idsException;
                    this.OnUpdateAsyncCompleted(this, callCompletedEventArgs);
                }
            }
        }

        #endregion

        #region Async updateacc
        /// <summary>
        /// Updates an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Returns an updated version of the entity with updated identifier and sync token</param>
        public void UpdateAccAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Update account Asynchronously.");
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                callCompletedEventArgs.Error = exception;
                this.OnUpdateAccAsyncCompleted(this, callCompletedEventArgs);
            }
            else
            {
                try
                {
                    AsyncService asyncService = new AsyncService(this.serviceContext);

                    asyncService.OnUpdateAccAsynCompleted += new DataServiceCallback<IEntity>.CallCompletedEventHandler(this.UpdateAccAsyncCompleted);
                    //check
                    asyncService.UpdateAccAsync<IEntity>(entity as IEntity);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);
                    callCompletedEventArgs.Error = idsException;
                    this.OnUpdateAccAsyncCompleted(this, callCompletedEventArgs);
                }
            }
        }

        #endregion


        #region Async donotupdateacc
        /// <summary>
        /// Updates an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Returns an updated version of the entity with updated identifier and sync token</param>
        public void DoNotUpdateAccAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Not Update account Asynchronously.");
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                callCompletedEventArgs.Error = exception;
                this.OnDoNotUpdateAccAsyncCompleted(this, callCompletedEventArgs);
            }
            else
            {
                try
                {
                    AsyncService asyncService = new AsyncService(this.serviceContext);

                    asyncService.OnDoNotUpdateAccAsyncCompleted += new DataServiceCallback<IEntity>.CallCompletedEventHandler(this.DoNotUpdateAccAsyncCompleted);
                    //check
                    asyncService.DoNotUpdateAccAsync<IEntity>(entity as IEntity);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);
                    callCompletedEventArgs.Error = idsException;
                    this.OnDoNotUpdateAccAsyncCompleted(this, callCompletedEventArgs);
                }
            }
        }

        #endregion

        #endregion

        #region Static Methods

        /// <summary>
        /// Validates the Service context.
        /// </summary>
        /// <param name="serviceContext">Service Context.</param>
        internal static void ServiceContextValidation(ServiceContext serviceContext)
        {
            if (serviceContext == null)
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.ServiceContextParameterName, Resources.ServiceContextNotNullMessage));
                IdsExceptionManager.HandleException(exception);
            }
        }

        #endregion

        #region Async Completed methods

        /// <summary>
        /// Find Asynchronous Call back method
        /// </summary>
        /// <param name="sender">Rest handler class</param>
        /// <param name="eventArgs">callback event arguments</param>
        private void FindAllAsyncCompleted(object sender, FindAllCallCompletedEventArgs eventArgs)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method FindAllAsync.");
            this.OnFindAllAsyncCompleted(sender, eventArgs);
        }

        /// <summary>
        /// Callback method for FindBy Id
        /// </summary>
        /// <param name="sender">Rest handler class</param>
        /// <param name="eventArgs">callback event arguments</param>
        private void FindByIdAsynCompleted(object sender, CallCompletedEventArgs<IEntity> eventArgs)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method FindByIdAsync.");
            this.OnFindByIdAsyncCompleted(sender, eventArgs);
        }

        /// <summary>
        /// CallBack method for FindByLevelAsync method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void FindByLevelAsyncCompleted(object sender, FindAllCallCompletedEventArgs eventArgs)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method FindByLevelAsyncCompleted.");
            this.OnFindByLevelAsyncCompleted(sender, eventArgs);
        }

        /// <summary>
        /// CallBack method for FindByParentIdAsync method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void FindByParentIdAsyncCompleted(object sender, FindAllCallCompletedEventArgs eventArgs)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method FindByParentIdAsyncCompleted.");
            this.OnFindByParentIdAsyncCompleted(sender, eventArgs);
        }

        /// <summary>
        /// Callback method for GetPdf
        /// </summary>
        /// <param name="sender">Rest handler class</param>
        /// <param name="eventArgs">callback event arguments</param>
        private void GetPdfAsynCompleted(object sender, PdfCallCompletedEventArgs eventArgs)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method GetPdfAsync.");
            this.OnGetPdfAsyncCompleted(sender, eventArgs);
        }

        /// <summary>
        /// Callback method for SendEmail
        /// </summary>
        /// <param name="sender">Rest handler class</param>
        /// <param name="eventArgs">callback event arguments</param>
        private void SendEmailAsynCompleted(object sender, CallCompletedEventArgs<IEntity> eventArgs)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method SendEmail.");
            this.OnSendEmailAsyncCompleted(sender, eventArgs);
        }

        /// <summary>
        /// Add Asynchronous call back method
        /// </summary>
        /// <param name="sender">Rest handler class</param>
        /// <param name="eventArgs">callback event arguments</param>
        private void AddAsyncCompleted(object sender, CallCompletedEventArgs<IEntity> eventArgs)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method add Async.");
            this.OnAddAsyncCompleted(sender, eventArgs);
        }

        /// <summary>
        /// Delete Asynchronously completed
        /// </summary>
        /// <param name="sender">Rest handler class</param>
        /// <param name="eventArgs">callback event arguments</param>
        private void DeleteAsyncCompleted(object sender, CallCompletedEventArgs<IEntity> eventArgs)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Delete Asyn.");
            this.OnDeleteAsyncCompleted(sender, eventArgs);
        }

        /// <summary>
        /// Voids the Asynchronously completed.
        /// </summary>
        /// <param name="sender">Rest handler class</param>
        /// <param name="eventArgs">callback event arguments</param>
        private void VoidAsyncCompleted(object sender, CallCompletedEventArgs<IEntity> eventArgs)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method VoidAsync.");
            this.OnVoidAsyncCompleted(sender, eventArgs);
        }

        /// <summary>
        /// Update asynchronously completed
        /// </summary>
        /// <param name="sender">Rest handler class</param>
        /// <param name="eventArgs">callback event arguments</param>
        private void UpdateAsyncCompleted(object sender, CallCompletedEventArgs<IEntity> eventArgs)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method UpdateAsync.");
            this.OnUpdateAsyncCompleted(sender, eventArgs);
        }

        /// <summary>
        /// Update account asynchronously completed
        /// </summary>
        /// <param name="sender">Rest handler class</param>
        /// <param name="eventArgs">callback event arguments</param>
        private void UpdateAccAsyncCompleted(object sender, CallCompletedEventArgs<IEntity> eventArgs)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method UpdateAccAsync.");
            this.OnUpdateAccAsyncCompleted(sender, eventArgs);
        }

        /// <summary>
        /// Do Not Update account asynchronously completed
        /// </summary>
        /// <param name="sender">Rest handler class</param>
        /// <param name="eventArgs">callback event arguments</param>
        private void DoNotUpdateAccAsyncCompleted(object sender, CallCompletedEventArgs<IEntity> eventArgs)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method DoNotUpdateaccAsync.");
            this.OnDoNotUpdateAccAsyncCompleted(sender, eventArgs);
        }

        /// <summary>
        /// CDC Asynchronous Call back method
        /// </summary>
        /// <param name="sender">Rest handler class</param>
        /// <param name="eventArgs">callback event arguments</param>
        private void CDCAsyncCompleted(object sender, CDCCallCompletedEventArgs eventArgs)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method CDCAsync.");
            this.OnCDCAsyncCompleted(sender, eventArgs);
        }

        #endregion

        #region Attachment

        /// <summary>
        /// Upload a stream with metadata defined in Attachable under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <param name="entity">Attachment Metadata of Stream to be Uploaded.</param>
        /// <param name="stream">Stream to be uploaded</param>
        /// <returns>Returns an uploaded attachment with updated identifier and sync token.</returns>
        public Attachable Upload(Attachable entity, System.IO.Stream stream)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Upload.");

            // Validate parameter
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            // Builds resource Uri
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/upload", CoreConstants.VERSION, this.serviceContext.RealmId);

            // Creates request parameters

            //guid will be the boundary string
            RequestParameters parameters;
            string guid = Guid.NewGuid().ToString();

            parameters = new RequestParameters(uri, HttpVerbType.POST, string.Format(CultureInfo.InvariantCulture, CoreConstants.CONTENTTYPE_MULTIPARTFORMDATAFORMAT, guid));

            //Construct attachement multipart request body
            IEntitySerializer serializer = CoreHelper.GetSerializer(this.serviceContext, true);
            string entityboundaryHeader = string.Empty;

            string contentTypeHeader = string.Empty;

            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                contentTypeHeader = string.Format(CoreConstants.CONTENTDISPOSITION_CONTENTTYPE_FORMAT, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                contentTypeHeader = string.Format(CoreConstants.CONTENTDISPOSITION_CONTENTTYPE_FORMAT, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }

            entityboundaryHeader = String.Format(CoreConstants.CONTENTDISPOSITION_FORMAT, guid, "file_metadata_0", null, contentTypeHeader, null);

            System.IO.Stream body = new System.IO.MemoryStream();
            //adding serialized attachement entity to request body
            UTF8Encoding enc = new UTF8Encoding();

            byte[] bytes = enc.GetBytes(entityboundaryHeader);
            body.Write(bytes, 0, bytes.Length);

            bytes = enc.GetBytes(serializer.Serialize(entity) + "\r\n");
            body.Write(bytes, 0, bytes.Length);

            //adding file to request body
            //string fileboundaryHeader = String.Format(CoreConstants.CONTENTDISPOSITION_BINARY_FORMAT, guid, "file_content_0", file.Name, fileMime);
            string fileNameHeader = string.Empty;
            if (!string.IsNullOrEmpty(entity.FileName))
            {
                fileNameHeader = string.Format(CoreConstants.CONTENTDISPOSITION_FILENAME_FORMAT, entity.FileName);
            }

            if (!string.IsNullOrEmpty(entity.ContentType))
            {
                contentTypeHeader = string.Format(CoreConstants.CONTENTDISPOSITION_CONTENTTYPE_FORMAT, entity.ContentType);
            }
            else
            {
                contentTypeHeader = null;
            }

            string contentTransferEncoding = string.Format(CoreConstants.CONTENTDISPOSITION_CONTENTTRANSFERENCODING_FORMAT, "binary");

            string fileboundaryHeader = String.Format(CoreConstants.CONTENTDISPOSITION_FORMAT, guid, "file_content_0", fileNameHeader, contentTypeHeader, contentTransferEncoding);
            bytes = enc.GetBytes(fileboundaryHeader);
            body.Write(bytes, 0, bytes.Length);

            using (BinaryReader br = new BinaryReader(stream))
            {
                byte[] buffer = br.ReadBytes((int)stream.Length);
                body.Write(buffer, 0, buffer.Count());
            }

            bytes = enc.GetBytes("\r\n--" + guid);
            body.Write(bytes, 0, bytes.Length);

            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, body);

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
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(response);
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Add.");
            foreach (AttachableResponse returnEntity in restResponse.AnyIntuitObjects)
            {
                return returnEntity.AnyIntuitObject as Attachable;
            }
            return null;
        }

        /// <summary>
        /// Returns an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <param name="entity"> Entity type to Find.</param>    
        /// <returns> Returns an entity of specified Id.</returns> 
        public byte[] Download(Attachable entity)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Download.");

            // Validate parameter
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            string uri = string.Empty;

            // Check whether the Id is null and throw an exception if it is null.
            if (string.IsNullOrWhiteSpace(entity.FileAccessUri))
            {
                IdsException exception = new IdsException(Resources.EntityIdNotNullMessage, new ArgumentNullException(Resources.IdString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }

            uri = entity.FileAccessUri;

            // Creates request parameters
            RequestParameters parameters = new RequestParameters(uri, HttpVerbType.GET, null);

            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, null);

            //Exception for Download, it does not accept "Accept" header
            request.Accept = null;

            string response = string.Empty;
            try
            {
                // Gets response
                response = this.restHandler.GetResponse(request);
            }
            catch (IdsException ex)
            {
                IdsExceptionManager.HandleException(ex);
            }

            CoreHelper.CheckNullResponseAndThrowException(response);

            Uri uriResponse = new Uri(response);

            if (uriResponse != null)
            {
                HttpWebRequest downloadRequest = WebRequest.Create(uriResponse) as HttpWebRequest;
                downloadRequest.Method = HttpVerbType.GET.ToString();

                using (WebResponse downloadResponse = downloadRequest.GetResponse())
                using (BinaryReader streamReader = new BinaryReader(downloadResponse.GetResponseStream()))
                {

                    return streamReader.ReadBytes(int.Parse(downloadResponse.Headers["Content-Length"]));
                }

                //  return response;
            }

            return null;
        }

        #endregion

        /// <summary>
        /// Prepare Http request for Reading Tax Cassification methods
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        private List<T> PrepareAndExecuteHttpRequest<T>(string uri)
        {
            // Creates request parameters
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat ==
                Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }

            // Prepares request
            HttpWebRequest request = this.restHandler.PrepareRequest(parameters, null);
            string response = string.Empty;
            try
            {
                // Gets response
                response = this.restHandler.GetResponse(request);
            }
            catch (IdsException ex)
            {
                IdsExceptionManager.HandleException(ex);
            }

            CoreHelper.CheckNullResponseAndThrowException(response);

            // Deserialize object
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(response);
            QueryResponse queryResponse = restResponse.AnyIntuitObject as QueryResponse;

            List<T> entities = new List<T>();

            if (queryResponse.AnyIntuitObjects == null)
            {
                IdsException exception = new IdsException(Resources.CommunicationErrorMessage, new CommunicationException(Resources.ResponseStreamNullOrEmptyMessage));
                throw exception;
            }
            else
            {
                queryResponse.maxResults = queryResponse.AnyIntuitObjects.Length;

                queryResponse.maxResultsSpecified = true;

                if (queryResponse.maxResults > 0)
                {
                    object tempEntities = queryResponse.AnyIntuitObjects;
                    object[] tempEntityArray = (object[])tempEntities;

                    if (tempEntityArray.Length > 0)
                    {
                        foreach (object item in tempEntityArray)
                        {
                            entities.Add((T)item);
                        }
                    }
                }
            }
            return entities;
        }

    }
}
