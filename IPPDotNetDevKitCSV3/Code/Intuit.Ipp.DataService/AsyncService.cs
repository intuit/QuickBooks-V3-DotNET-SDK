////***************************************************************************************************
// <copyright file="AsyncService.cs" company="Intuit">
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
// <summary>This file contains methods for CRUD operations that supports asynchronous call.</summary>
////***************************************************************************************************
namespace Intuit.Ipp.DataService
{
    using System;
    using System.Collections.Generic;
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

    /// <summary>
    /// Intuit Partner Platform Services for QBO.
    /// </summary>
    internal class AsyncService
    {
        /// <summary>
        /// The Service context object.
        /// </summary>
        private ServiceContext serviceContext;

        /// <summary>
        /// The Requested Entity.
        /// </summary>
        private IEntity requestedEntity;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsyncService"/> class.
        /// </summary>
        /// <param name="serviceContext">IPP Service Context</param>
        public AsyncService(ServiceContext serviceContext)
        {
            if (serviceContext == null)
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException("serviceContext", "The Service Context cannot be null."));
                IdsExceptionManager.HandleException(exception);
            }

            if (serviceContext.IppConfiguration.Logger == null)
            {
                IdsException exception = new IdsException("The Logger cannot be null.");
                IdsExceptionManager.HandleException(exception);
            }

            if (string.IsNullOrWhiteSpace(serviceContext.RealmId))
            {
                InvalidRealmException exception = new InvalidRealmException();
                IdsExceptionManager.HandleException(exception);
            }

            this.serviceContext = serviceContext;
        }

        /// <summary>
        /// call back event for find all
        /// </summary>
        public event DataServiceCallback<IEntity>.FindAllCallCompletedEventHandler OnFindAllAsynCompleted;

        /// <summary>
        /// Call back event for add.
        /// </summary>
        public event DataServiceCallback<IEntity>.CallCompletedEventHandler OnAddAsyncCompleted;

        /// <summary>
        /// call back event for update.
        /// </summary>
        public event DataServiceCallback<IEntity>.CallCompletedEventHandler OnUpdateAsynCompleted;

        
        /// <summary>
        /// call back event for update account.
        /// </summary>
        public event DataServiceCallback<IEntity>.CallCompletedEventHandler OnUpdateAccAsynCompleted;
        

        /// <summary>
        /// call back event for update account.
        /// </summary>
        public event DataServiceCallback<IEntity>.CallCompletedEventHandler OnDoNotUpdateAccAsyncCompleted;
        

        /// <summary>
        /// Call Back event for Delete.
        /// </summary>
        public event DataServiceCallback<IEntity>.CallCompletedEventHandler OnDeleteAsynCompleted;

        /// <summary>
        /// Call Back event for Void.
        /// </summary>
        public event DataServiceCallback<IEntity>.CallCompletedEventHandler OnVoidAsynCompleted;

        /// <summary>
        /// Call back event for FindById method.
        /// </summary>
        public event DataServiceCallback<IEntity>.CallCompletedEventHandler OnFindByIdAsynCompleted;

        /// <summary>
        /// Call back event for GetPdf method.
        /// </summary>
        public event DataServiceCallback<IEntity>.PdfCallCompletedEventHandler OnGetPdfAsynCompleted;

        /// <summary>
        /// Call back event for SendEmail method.
        /// </summary>
        public event DataServiceCallback<IEntity>.CallCompletedEventHandler OnSendEmailAsynCompleted;

        /// <summary>
        /// Call back event for CDC method.
        /// </summary>
        public event DataServiceCallback<IEntity>.CDCCallCompletedEventHandler OnCDCAsynCompleted;

        #region FindAll

        /// <summary>
        /// Gets a list of all entities  of type T under the specified realm (asynchronously). The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type</typeparam>
        /// <param name="entity">The entity for which the data is required.</param>
        /// <param name="startPosition">The start position.</param>
        /// <param name="maxResults">The max results.</param>
        public void FindAllAsync<T>(T entity, int startPosition, int maxResults) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method FindAll Asynchronously.");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(this.serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(this.FindAllAsyncCompleted<IEntity>);
            FindAllCallCompletedEventArgs findAllCompletedEventArgs = new FindAllCallCompletedEventArgs();

            // string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            string resourceString = entity.GetType().Name;
            this.requestedEntity = entity;
            try
            {
                // Builds resource Uri
                string query = string.Format(CultureInfo.InvariantCulture, "select * from {0} startPosition {1} maxResults {2}", resourceString, startPosition, maxResults);
                string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/query", CoreConstants.VERSION, this.serviceContext.RealmId);

                // Create request parameters
                RequestParameters parameters = null;
            
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONTEXT);
       

                //// Prepare request
                HttpWebRequest request = asyncRestHandler.PrepareRequest(parameters, query);

                //// get response
                asyncRestHandler.GetResponse(request);
            }
            catch (SystemException systemException)
            {
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                findAllCompletedEventArgs.Error = idsException;
                this.OnFindAllAsynCompleted(this, findAllCompletedEventArgs);
            }
        }

        #endregion

        #region add

        /// <summary>
        /// Adds an entity (asynchronously) under the specified realm in an asynchronous manner. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Add</param>
        public void AddAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Add Asynchronously.");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(this.serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(this.AddAsyncompleted);
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            this.requestedEntity = entity;
            try
            {
                // Builds resource Uri
                string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

                // Create request parameters
                RequestParameters parameters;
                if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
                {
                    parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
                }
                else
                {
                    parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
                }

                // Prepare request
                HttpWebRequest request = asyncRestHandler.PrepareRequest(parameters, entity);

                //// get response
                asyncRestHandler.GetResponse(request);
            }
            catch (SystemException systemException)
            {
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                callCompletedEventArgs.Error = idsException;
                this.OnAddAsyncCompleted(this, callCompletedEventArgs);
            }
        }

        #endregion

        #region update

        /// <summary>
        /// Updates(asynchronously) an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Update</param>
        public void UpdateAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Update Asynchronously.");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(this.serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(this.UpdateAsyncCompleted);
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            this.requestedEntity = entity;
            try
            {
                // Builds resource Uri
                string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

                //// Create request parameters
                RequestParameters parameters;
                if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
                {
                    parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
                }
                else
                {
                    parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
                }

                //// Prepare request
                HttpWebRequest request = asyncRestHandler.PrepareRequest(parameters, entity);

                //// get response
                asyncRestHandler.GetResponse(request);
            }
            catch (SystemException systemException)
            {
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                callCompletedEventArgs.Error = idsException;
                this.OnUpdateAsynCompleted(this, callCompletedEventArgs);
            }
        }

        #endregion


        #region updateacc

        /// <summary>
        /// Updates(asynchronously) an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Update</param>
        public void UpdateAccAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Update account Asynchronously.");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(this.serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(this.UpdateAccAsyncCompleted);
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            this.requestedEntity = entity;
            try
            {
                // Builds resource Uri
               
                string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}?include=updateaccountontxns", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

                //// Create request parameters
                RequestParameters parameters;
                if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
                {
                    parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
                }
                else
                {
                    parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
                }

                //// Prepare request
                HttpWebRequest request = asyncRestHandler.PrepareRequest(parameters, entity);

                //// get response
                asyncRestHandler.GetResponse(request);
            }
            catch (SystemException systemException)
            {
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                callCompletedEventArgs.Error = idsException;
                this.OnUpdateAccAsynCompleted(this, callCompletedEventArgs);
            }
        }

        #endregion

        #region donotupdateacc

        /// <summary>
        /// do not Updates(asynchronously) an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Update</param>
        public void DoNotUpdateAccAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Update account Asynchronously.");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(this.serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(this.DoNotUpdateAccAsyncCompleted);
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            this.requestedEntity = entity;
            try
            {
                // Builds resource Uri
                //string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);
               
                string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}?include=donotupdateaccountontxns", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

                //// Create request parameters
                RequestParameters parameters;
                if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
                {
                    parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
                }
                else
                {
                    parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
                }

                //// Prepare request
                HttpWebRequest request = asyncRestHandler.PrepareRequest(parameters, entity);

                //// get response
                asyncRestHandler.GetResponse(request);
            }
            catch (SystemException systemException)
            {
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                callCompletedEventArgs.Error = idsException;
                this.OnDoNotUpdateAccAsyncCompleted(this, callCompletedEventArgs);
            }
        }

        #endregion

        #region Delete

        /// <summary>
        /// Deletes an entity (asynchronously) under the specified realm. The realm must be set in the context
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Delete</param>
        public void DeleteAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method delete Asynchronously.");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(this.serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(this.DeleteAsyncCompleted);
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            this.requestedEntity = entity;
            try
            {
                // Builds resource Uri
                string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}?operation=delete", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

                //// Create request parameters
                RequestParameters parameters;
                if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
                {
                    parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
                }
                else
                {
                    parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
                }

                //// Prepare request
                HttpWebRequest request = asyncRestHandler.PrepareRequest(parameters, entity);

                //// get response
                asyncRestHandler.GetResponse(request);
            }
            catch (SystemException systemException)
            {
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                callCompletedEventArgs.Error = idsException;
                this.OnDeleteAsynCompleted(this, callCompletedEventArgs);
            }
        }

        #endregion

        #region void
        /// <summary>
        /// Deletes an entity (asynchronously) under the specified realm. The realm must be set in the context
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Delete</param>
        public void VoidAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Void Asynchronously.");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(this.serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(this.VoidAsyncCompleted);
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            this.requestedEntity = entity;
            try
            {
                // Builds resource Uri
                string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}?include=void", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

                //// Create request parameters
                RequestParameters parameters;
                if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
                {
                    parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
                }
                else
                {
                    parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
                }

                //// Prepare request
                HttpWebRequest request = asyncRestHandler.PrepareRequest(parameters, entity);

                //// get response
                asyncRestHandler.GetResponse(request);
            }
            catch (SystemException systemException)
            {
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                callCompletedEventArgs.Error = idsException;
                this.OnVoidAsynCompleted(this, callCompletedEventArgs);
            }
        }

        #endregion

        #region GetPdf

        /// <summary>
        /// Calls the asynchronous method to get the required entity as pdf.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity"> Entity type to get as pdf</param>    
        public void GetPdfAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method GetPdf Asynchronously.");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(this.serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(this.GetPdfCompleted);
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            PdfCallCompletedEventArgs pdfCallCompletedEventArgs = new PdfCallCompletedEventArgs();
            IntuitEntity intuitEntity = entity as IntuitEntity;
            string id = string.Empty;
            if (intuitEntity == null)
            {
                IdsException exception = new IdsException(Resources.EntityConversionFailedMessage);
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                pdfCallCompletedEventArgs.Error = exception;
                this.OnGetPdfAsynCompleted(this, pdfCallCompletedEventArgs);
                return;
            }

            // Check whether the Id is null and throw an exception if it is null.
            if (string.IsNullOrWhiteSpace(intuitEntity.Id))
            {
                IdsException exception = new IdsException(Resources.EntityIdNotNullMessage, new ArgumentNullException(Resources.IdString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                pdfCallCompletedEventArgs.Error = exception;
                this.OnGetPdfAsynCompleted(this, pdfCallCompletedEventArgs);
                return;
            }

            id = intuitEntity.Id;
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            this.requestedEntity = entity;
            try
            {
                // Builds resource Uri
                string uri = string.Empty;
                
                if (resourceString.Equals("preferences"))
                {
                    uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}/pdf", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);
                }
                else
                {
                    uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}/{3}/pdf", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString, id);
                }

                //// Create request parameters
                RequestParameters parameters;
                if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
                {
                    parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
                }
                else
                {
                    parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);
                }

                //// Prepare request
                HttpWebRequest request = asyncRestHandler.PrepareRequest(requestParameters: parameters, requestBody: null, includeRequestId: false);

                //set accept header to application/pdf
                request.Accept = CoreConstants.CONTENTTYPE_APPLICATIONPDF;

                //// get response
                asyncRestHandler.GetResponse(request);
            }
            catch (SystemException systemException)
            {
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                callCompletedEventArgs.Error = idsException;
                this.OnFindByIdAsynCompleted(this, callCompletedEventArgs);
            }
        }

        /// <summary>
        /// GetPdf call back method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Intuit.Ipp.Core.AsyncCallCompletedEventArgs"/> instance containing the event data.</param>
        public void GetPdfCompleted(object sender, AsyncCallCompletedEventArgs eventArgs)
        {
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            PdfCallCompletedEventArgs pdfCallCompletedEventArgs = new PdfCallCompletedEventArgs();
            if (eventArgs.Error == null)
            {
                try
                {
                   
                    pdfCallCompletedEventArgs.PdfBytes = eventArgs.ByteResult;


                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing event GetPdfCompleted in AsyncService object.");
                    this.OnGetPdfAsynCompleted(this, pdfCallCompletedEventArgs);
                }
                catch (SystemException systemException)
                {
                    IdsException idsException = CreateIdsException(systemException);
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    callCompletedEventArgs.Error = idsException;
                    this.OnGetPdfAsynCompleted(this, pdfCallCompletedEventArgs);
                }
            }
            else
            {
                callCompletedEventArgs.Error = eventArgs.Error;
                this.OnGetPdfAsynCompleted(this, pdfCallCompletedEventArgs);
            }
        }

        #endregion

        /// <summary>
        /// Calls the asynchronous method to email the required entity.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity"> Entity type to email</param>    
        /// <param name="sendToEmail">Email address to send email to.</param>    
        public void SendEmailAsync<T>(T entity, String sendToEmail) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Add Asynchronously.");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(this.serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(this.SendEmailCompleted);
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            IntuitEntity intuitEntity = entity as IntuitEntity;
            string id = string.Empty;

            id = intuitEntity.Id;
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            this.requestedEntity = entity;
            try
            {
                // Builds resource Uri
                string uri = string.Empty;

                //IF sendtoemail is specidfied that takes priority and is used to send the email to, if not specified it uses the email from BillEmail.Address from the entity saved on the server and not from the passes in entity
                uri = String.IsNullOrWhiteSpace(sendToEmail) ? string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}/{3}/send", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString, id)
                    : string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}/{3}/send?sendTo={4}", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString, id, sendToEmail);

                //// Create request parameters
                RequestParameters parameters;
                if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
                {
                    parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
                }
                else
                {
                    parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
                }

                //// Prepare request
                HttpWebRequest request = asyncRestHandler.PrepareRequest(requestParameters: parameters, requestBody: string.Empty, includeRequestId: false);
                request.ContentType = CoreConstants.CONTENTTYPE_APPLICATIONOCTETSTREAM;

                //// get response
                asyncRestHandler.GetResponse(request);
            }
            catch (SystemException systemException)
            {
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                callCompletedEventArgs.Error = idsException;
                this.OnSendEmailAsynCompleted(this, callCompletedEventArgs);
            }
        }

        /// <summary>
        /// SendEmail call back method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Intuit.Ipp.Core.AsyncCallCompletedEventArgs"/> instance containing the event data.</param>
        public void SendEmailCompleted(object sender, AsyncCallCompletedEventArgs eventArgs)
        {
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            if (eventArgs.Error == null)
            {
                try
                {
                    IEntitySerializer responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);
                    IntuitResponse restResponse = (IntuitResponse)responseSerializer.Deserialize<IntuitResponse>(eventArgs.Result);
                    IEntity response = restResponse.AnyIntuitObject as IEntity;
                    callCompletedEventArgs.Entity = null;
                    if (response != null)
                    {
                        callCompletedEventArgs.Entity = response;
                    }

                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing event SednEmailCompleted in AsyncService object.");
                    this.OnSendEmailAsynCompleted(this, callCompletedEventArgs);
                }
                catch (SystemException systemException)
                {
                    IdsException idsException = CreateIdsException(systemException);
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    callCompletedEventArgs.Error = idsException;
                    this.OnSendEmailAsynCompleted(this, callCompletedEventArgs);
                }
            }
            else
            {
                callCompletedEventArgs.Error = eventArgs.Error;
                this.OnSendEmailAsynCompleted(this, callCompletedEventArgs);
            }
        }

        #region FindById

        /// <summary>
        /// Calls the asynchronous method to get the required entity.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity"> Entity type to Find</param>    
        public void FindByIdAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method Add Asynchronously.");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(this.serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(this.FindByIdCompleted);
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            IntuitEntity intuitEntity = entity as IntuitEntity;
            string id = string.Empty;
            if (intuitEntity == null)
            {
                IdsException exception = new IdsException(Resources.EntityConversionFailedMessage);
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                callCompletedEventArgs.Error = exception;
                this.OnFindByIdAsynCompleted(this, callCompletedEventArgs);
                return;
            }

            // Check whether the Id is null and throw an exception if it is null.
            if (string.IsNullOrWhiteSpace(intuitEntity.Id))
            {
                IdsException exception = new IdsException(Resources.EntityIdNotNullMessage, new ArgumentNullException(Resources.IdString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                callCompletedEventArgs.Error = exception;
                this.OnFindByIdAsynCompleted(this, callCompletedEventArgs);
                return;
            }

            id = intuitEntity.Id;
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            this.requestedEntity = entity;
            try
            {
                // Builds resource Uri
                string uri = string.Empty;
                
                if (resourceString.Equals("preferences"))
                {
                    uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);
                }
                else
                {
                    uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}/{3}", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString, id);
                }

                //// Create request parameters
                RequestParameters parameters;
                if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
                {
                    parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
                }
                else
                {
                    parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);
                }

                //// Prepare request
                HttpWebRequest request = asyncRestHandler.PrepareRequest(parameters, null);

                //// get response
                asyncRestHandler.GetResponse(request);
            }
            catch (SystemException systemException)
            {
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                callCompletedEventArgs.Error = idsException;
                this.OnFindByIdAsynCompleted(this, callCompletedEventArgs);
            }
        }

        /// <summary>
        /// FindByid call back method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Intuit.Ipp.Core.AsyncCallCompletedEventArgs"/> instance containing the event data.</param>
        public void FindByIdCompleted(object sender, AsyncCallCompletedEventArgs eventArgs)
        {
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            if (eventArgs.Error == null)
            {
                try
                {
                    IEntitySerializer responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);
                    IntuitResponse restResponse = (IntuitResponse)responseSerializer.Deserialize<IntuitResponse>(eventArgs.Result);
                    IEntity response = restResponse.AnyIntuitObject as IEntity;
                    if (response != null)
                    {
                        callCompletedEventArgs.Entity = response;
                    }
                    else
                    {
                        QueryResponse queryResponse = restResponse.AnyIntuitObject as QueryResponse;
                        if (queryResponse != null && queryResponse.AnyIntuitObjects.Count() > 0)
                        {
                            callCompletedEventArgs.Entity = queryResponse.AnyIntuitObjects[0] as IEntity;
                        }
                        else
                        {
                            callCompletedEventArgs.Entity = null;
                        }
                    }

                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing event FindByIdCompleted in AsyncService object.");
                    this.OnFindByIdAsynCompleted(this, callCompletedEventArgs);
                }
                catch (SystemException systemException)
                {
                    IdsException idsException = CreateIdsException(systemException);
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    callCompletedEventArgs.Error = idsException;
                    this.OnFindByIdAsynCompleted(this, callCompletedEventArgs);
                }
            }
            else
            {
                callCompletedEventArgs.Error = eventArgs.Error;
                this.OnFindByIdAsynCompleted(this, callCompletedEventArgs);
            }
        }

        #endregion

        #region CDC

        /// <summary>
        /// Calls the asynchronous method to get the required CDC.
        /// </summary>
        public void CDCAsync(List<IEntity> entityList, DateTime changedSince)
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method CDC Asynchronously.");
            AsyncRestHandler asyncRestHandler = new AsyncRestHandler(this.serviceContext);
            asyncRestHandler.OnCallCompleted += new EventHandler<AsyncCallCompletedEventArgs>(this.CDCAsyncCompleted);
            CDCCallCompletedEventArgs cdcCompletedEventArgs = new CDCCallCompletedEventArgs();
            List<IntuitEntity> intuitEntityList = entityList.Cast<IntuitEntity>().ToList();
            string id = string.Empty;
            if (intuitEntityList.Count <= 0)
            {
                IdsException exception = new IdsException(Resources.EntityConversionFailedMessage);
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                cdcCompletedEventArgs.Error = exception;
                this.OnCDCAsynCompleted(this, cdcCompletedEventArgs);
                return;
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

            string query = "entities=" + entityString + "&changedSince=" + changedSince.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'");

            try
            {
                // Builds resource Uri
                string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/cdc?{2}", CoreConstants.VERSION, this.serviceContext.RealmId, query);

                //// Create request parameters
                RequestParameters parameters;
                if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
                {
                    parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
                }
                else
                {
                    parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);
                }

                //// Prepare request
                HttpWebRequest request = asyncRestHandler.PrepareRequest(parameters, null);

                //// get response
                asyncRestHandler.GetResponse(request);
            }
            catch (SystemException systemException)
            {
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                IdsException idsException = new IdsException(systemException.Message);
                cdcCompletedEventArgs.Error = idsException;
                this.OnCDCAsynCompleted(this, cdcCompletedEventArgs);
            }
        }

        #endregion

        #region IdsException
        /// <summary>
        /// Creates the ids exception.
        /// </summary>
        /// <param name="applicationException">The application exception.</param>
        /// <returns>Returns the IdsException.</returns>
        private static IdsException CreateIdsException(Exception applicationException)
        {
            IdsException idsException = new IdsException(applicationException.Message);
            return idsException;
        }

        #endregion

        #region Async Completed methods

        /// <summary>
        /// FindAll a sync completed call back.
        /// </summary>
        /// <typeparam name="T">The Entity type.</typeparam>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Intuit.Ipp.Core.AsyncCallCompletedEventArgs"/> instance containing the event data.</param>
        private void FindAllAsyncCompleted<T>(object sender, AsyncCallCompletedEventArgs eventArgs) where T : IEntity
        {
            FindAllCallCompletedEventArgs findAllCallCompletedEventArgs = new FindAllCallCompletedEventArgs();
            if (eventArgs.Error == null)
            {
                try
                {
                    IEntitySerializer responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);

                    // Deserialize object
                    IntuitResponse restResponse = (IntuitResponse)responseSerializer.Deserialize<IntuitResponse>(eventArgs.Result);
                    QueryResponse queryResponse = restResponse.AnyIntuitObject as QueryResponse;
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
                                    foreach (object item in tempEntityArray)
                                    {
                                        entities.Add((IEntity)item);
                                    }
                                }
                            }

                            //break;
                        }
                    }

                    findAllCallCompletedEventArgs.Entities = entities;
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing event AddAsyncompleted in AsyncService object.");
                    this.OnFindAllAsynCompleted(this, findAllCallCompletedEventArgs);
                }
                catch (SystemException systemException)
                {
                    IdsException idsException = CreateIdsException(systemException);
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    findAllCallCompletedEventArgs.Error = idsException;
                    this.OnFindAllAsynCompleted(this, findAllCallCompletedEventArgs);
                }
            }
            else
            {
                findAllCallCompletedEventArgs.Error = eventArgs.Error;
                this.OnFindAllAsynCompleted(this, findAllCallCompletedEventArgs);
            }
        }

        /// <summary>
        /// call back method for asynchronously Adding entity
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Intuit.Ipp.Core.AsyncCallCompletedEventArgs"/> instance containing the event data.</param>
        private void AddAsyncompleted(object sender, AsyncCallCompletedEventArgs eventArgs)
        {
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            if (eventArgs.Error == null)
            {
                try
                {
                    IEntitySerializer responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);
                    IntuitResponse restResponse = (IntuitResponse)responseSerializer.Deserialize<IntuitResponse>(eventArgs.Result);
                    callCompletedEventArgs.Entity = restResponse.AnyIntuitObject as IEntity;
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing event AddAsyncompleted in AsyncService object.");
                    this.OnAddAsyncCompleted(this, callCompletedEventArgs);
                }
                catch (SystemException systemException)
                {
                    IdsException idsException = CreateIdsException(systemException);
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    callCompletedEventArgs.Error = idsException;
                    this.OnAddAsyncCompleted(this, callCompletedEventArgs);
                }
            }
            else
            {
                callCompletedEventArgs.Error = eventArgs.Error;
                this.OnAddAsyncCompleted(this, callCompletedEventArgs);
            }
        }

        /// <summary>
        /// Callback event
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Intuit.Ipp.Core.AsyncCallCompletedEventArgs"/> instance containing the event data.</param>
        private void UpdateAsyncCompleted(object sender, AsyncCallCompletedEventArgs eventArgs)
        {
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            if (eventArgs.Error == null)
            {
                try
                {
                    IEntitySerializer responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);
                    IntuitResponse restResponse = (IntuitResponse)responseSerializer.Deserialize<IntuitResponse>(eventArgs.Result);
                    callCompletedEventArgs.Entity = restResponse.AnyIntuitObject as IEntity;
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing event AddAsyncompleted in AsyncService object.");
                    this.OnUpdateAsynCompleted(this, callCompletedEventArgs);
                }
                catch (SystemException systemException)
                {
                    IdsException idsException = CreateIdsException(systemException);
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    callCompletedEventArgs.Error = idsException;
                    this.OnUpdateAsynCompleted(this, callCompletedEventArgs);
                }
            }
            else
            {
                callCompletedEventArgs.Error = eventArgs.Error;
                this.OnUpdateAsynCompleted(this, callCompletedEventArgs);
            }
        }
       

        /// <summary>
        /// Callback event
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Intuit.Ipp.Core.AsyncCallCompletedEventArgs"/> instance containing the event data.</param>
        private void UpdateAccAsyncCompleted(object sender, AsyncCallCompletedEventArgs eventArgs)
        {
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            if (eventArgs.Error == null)
            {
                try
                {
                    IEntitySerializer responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);
                    IntuitResponse restResponse = (IntuitResponse)responseSerializer.Deserialize<IntuitResponse>(eventArgs.Result);
                    callCompletedEventArgs.Entity = restResponse.AnyIntuitObject as IEntity;
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing event AddAsyncompleted in AsyncService object.");
                    this.OnUpdateAccAsynCompleted(this, callCompletedEventArgs);
                }
                catch (SystemException systemException)
                {
                    IdsException idsException = CreateIdsException(systemException);
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    callCompletedEventArgs.Error = idsException;
                    this.OnUpdateAccAsynCompleted(this, callCompletedEventArgs);
                }
            }
            else
            {
                callCompletedEventArgs.Error = eventArgs.Error;
                this.OnUpdateAccAsynCompleted(this, callCompletedEventArgs);
            }
        }

        private void DoNotUpdateAccAsyncCompleted(object sender, AsyncCallCompletedEventArgs eventArgs)
        {
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            if (eventArgs.Error == null)
            {
                try
                {
                    IEntitySerializer responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);
                    IntuitResponse restResponse = (IntuitResponse)responseSerializer.Deserialize<IntuitResponse>(eventArgs.Result);
                    callCompletedEventArgs.Entity = restResponse.AnyIntuitObject as IEntity;
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing event AddAsyncompleted in AsyncService object.");
                    this.OnDoNotUpdateAccAsyncCompleted(this, callCompletedEventArgs);
                }
                catch (SystemException systemException)
                {
                    IdsException idsException = CreateIdsException(systemException);
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    callCompletedEventArgs.Error = idsException;
                    this.OnDoNotUpdateAccAsyncCompleted(this, callCompletedEventArgs);
                }
            }
            else
            {
                callCompletedEventArgs.Error = eventArgs.Error;
                this.OnDoNotUpdateAccAsyncCompleted(this, callCompletedEventArgs);
            }
        }




        /// <summary>
        /// Delete operation call back method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Intuit.Ipp.Core.AsyncCallCompletedEventArgs"/> instance containing the event data.</param>
        private void DeleteAsyncCompleted(object sender, AsyncCallCompletedEventArgs eventArgs)
        {
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            if (eventArgs.Error == null)
            {
                try
                {
                    IEntitySerializer responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);
                    IntuitResponse restResponse = (IntuitResponse)responseSerializer.Deserialize<IntuitResponse>(eventArgs.Result);
                    IntuitEntity intuitEntity = restResponse.AnyIntuitObject as IntuitEntity;

                    Type type = this.requestedEntity.GetType();
                    PropertyInfo[] propertyInfoArray = type.GetProperties();
                    PropertyInfo statusPropInfo = propertyInfoArray.FirstOrDefault(pi => pi.Name == CoreConstants.STATUS);
                    if (statusPropInfo != null)
                    {
                        statusPropInfo.SetValue(this.requestedEntity, intuitEntity.status, null);
                    }

                    
                    if (intuitEntity.status != EntityStatusEnum.Deleted)
                    {
                        IdsException exception = new IdsException(Resources.CommunicationErrorMessage, new CommunicationException(Resources.StatusNotDeleted));
                        this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                        callCompletedEventArgs.Error = exception;
                        this.OnDeleteAsynCompleted(this, callCompletedEventArgs);
                    }

                    callCompletedEventArgs.Entity = this.requestedEntity;
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing event DeleteAsyncCompleted in AsyncService object.");
                    this.OnDeleteAsynCompleted(this, callCompletedEventArgs);
                }
                catch (SystemException systemException)
                {
                    IdsException idsException = CreateIdsException(systemException);
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    callCompletedEventArgs.Error = idsException;
                    this.OnDeleteAsynCompleted(this, callCompletedEventArgs);
                }
            }
            else
            {
                callCompletedEventArgs.Error = eventArgs.Error;
                this.OnDeleteAsynCompleted(this, callCompletedEventArgs);
            }
        }

        /// <summary>
        /// Delete operation call back method.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Intuit.Ipp.Core.AsyncCallCompletedEventArgs"/> instance containing the event data.</param>
        private void VoidAsyncCompleted(object sender, AsyncCallCompletedEventArgs eventArgs)
        {
            CallCompletedEventArgs<IEntity> callCompletedEventArgs = new CallCompletedEventArgs<IEntity>();
            if (eventArgs.Error == null)
            {
                try
                {
                    IEntitySerializer responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);
                    IntuitResponse restResponse = (IntuitResponse)responseSerializer.Deserialize<IntuitResponse>(eventArgs.Result);

                    if (restResponse.AnyIntuitObject != null)
                    {
                        IntuitEntity intuitEntity = restResponse.AnyIntuitObject as IntuitEntity;

                        Type type = this.requestedEntity.GetType();
                        PropertyInfo[] propertyInfoArray = type.GetProperties();
                        PropertyInfo statusPropInfo = propertyInfoArray.FirstOrDefault(pi => pi.Name == CoreConstants.STATUS);
                        if (statusPropInfo != null)
                        {
                            statusPropInfo.SetValue(this.requestedEntity, intuitEntity.status, null);
                        }

                        if (intuitEntity == null)
                        {
                            IdsException exception = new IdsException(Resources.CommunicationErrorMessage, new CommunicationException(Resources.StatusNotVoided));
                            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                            callCompletedEventArgs.Error = exception;
                            this.OnVoidAsynCompleted(this, callCompletedEventArgs);
                        }
                    }

                    callCompletedEventArgs.Entity = restResponse.AnyIntuitObject as IEntity;
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing event VoidAsyncCompleted in AsyncService object.");
                    this.OnVoidAsynCompleted(this, callCompletedEventArgs);
                }
                catch (SystemException systemException)
                {
                    IdsException idsException = CreateIdsException(systemException);
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    callCompletedEventArgs.Error = idsException;
                    this.OnVoidAsynCompleted(this, callCompletedEventArgs);
                }
            }
            else
            {
                callCompletedEventArgs.Error = eventArgs.Error;
                this.OnVoidAsynCompleted(this, callCompletedEventArgs);
            }
        }


        /// <summary>
        /// CDC Async completed call back.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The <see cref="Intuit.Ipp.Core.AsyncCallCompletedEventArgs"/> instance containing the event data.</param>
        private void CDCAsyncCompleted(object sender, AsyncCallCompletedEventArgs eventArgs)
        {
            CDCCallCompletedEventArgs cdcCallCompletedEventArgs = new CDCCallCompletedEventArgs();
            if (eventArgs.Error == null)
            {
                try
                {
                    IEntitySerializer responseSerializer = CoreHelper.GetSerializer(this.serviceContext, false);

                    // Deserialize object
                    IntuitResponse restResponse = (IntuitResponse)responseSerializer.Deserialize<IntuitResponse>(eventArgs.Result);
                    object[] cdcResponses = restResponse.AnyIntuitObjects;
                    int count = 1;

                    Dictionary<string, List<IEntity>> returnValue = new Dictionary<string, List<IEntity>>();

                    foreach (CDCResponse cdcResponse in cdcResponses)
                    {
                        object[] queryResponses = cdcResponse.AnyIntuitObjects;

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
                                            foreach (object item in tempEntityArray)
                                            {
                                                entities.Add((IEntity)item);
                                                returnValue.Add(item.GetType().Name, entities);
                                                count++;
                                            }
                                        }
                                        //break;
                                    }
                                }
                            }
                        }
                    }

                    cdcCallCompletedEventArgs.Entities = returnValue;
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing event AddAsyncompleted in AsyncService object.");
                    this.OnCDCAsynCompleted(this, cdcCallCompletedEventArgs);
                }
                catch (SystemException systemException)
                {
                    IdsException idsException = CreateIdsException(systemException);
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, idsException.ToString());
                    cdcCallCompletedEventArgs.Error = idsException;
                    this.OnCDCAsynCompleted(this, cdcCallCompletedEventArgs);
                }
            }
            else
            {
                cdcCallCompletedEventArgs.Error = eventArgs.Error;
                this.OnCDCAsynCompleted(this, cdcCallCompletedEventArgs);
            }
        }

        #endregion 
    }
}
