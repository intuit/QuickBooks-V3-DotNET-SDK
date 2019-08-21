using Intuit.Ipp.Client.Properties;
using Intuit.Ipp.Core;
using Intuit.Ipp.Core.Rest;
using Intuit.Ipp.Data;
using Intuit.Ipp.Exception;
using Intuit.Ipp.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Intuit.Ipp.Client
{
    public class AsynchronousService
    {
        /// <summary>
        /// The Service context object.
        /// </summary>
        private ServiceContext serviceContext;

        /// <summary>
        /// 
        /// </summary>
        protected HttpClient client;

        /// <summary>
        /// The Requested Entity.
        /// </summary>
        private IEntity requestedEntity;

        /// <summary>
        /// Initializes a new instance of the <see cref="AsynchronousService"/> class.
        /// </summary>
        /// <param name="serviceContext">IPP Service Context</param>
        public AsynchronousService(ServiceContext serviceContext)
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
            client = new HttpClient();
            this.serviceContext = serviceContext;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<string> FindByIdAsync<T>(T entity) where T : IEntity
        {
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            string uri = string.Empty;
            IntuitEntity intuitEntity = entity as IntuitEntity;
            string id = intuitEntity.Id;
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

            RequestGenerator helper = new RequestGenerator(serviceContext);
            HttpRequestMessage request = helper.PrepareRequest(parameters, null);
            string content = await GetResponse(request);
            return content;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="startPosition"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public async Task<string> FindAllAsync<T>(T entity, int startPosition = 1, int maxResults = 500)
        {
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            string query = string.Format(CultureInfo.InvariantCulture, "select * from {0} startPosition {1} maxResults {2}", resourceString, startPosition, maxResults);
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/query?query={2}", CoreConstants.VERSION, this.serviceContext.RealmId, query);
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.GET, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }

            RequestGenerator helper = new RequestGenerator(serviceContext);
            HttpRequestMessage request = helper.PrepareRequest(parameters, query);
            string content = await GetResponse(request);
            return content;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<string> AddAsync<T>(T entity)
        {
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

         
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }
            RequestGenerator requestGenerator = new RequestGenerator(serviceContext);
            HttpRequestMessage request = requestGenerator.PrepareRequest(parameters, entity);
            string content = await GetResponse(request);
            return content;
        }

        private async Task<string> GetResponse(HttpRequestMessage request)
        {
           
            string content = "";
            try
            {
              
                if (this.serviceContext.IppConfiguration.RetryPolicy == null)
                {
                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        CoreHelper.CheckNullResponseAndThrowException(content);
                    }
                }
                else
                {
                    //this.ExecAsyncRequestWithRetryPolicy(request);
                }
              
            }
            finally
            {
                this.serviceContext.RequestId = null;
            }
            return content;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<string> UpdateAsync<T>(T entity) where T : IEntity
        {
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }
            RequestGenerator requestGenerator = new RequestGenerator(serviceContext);
            HttpRequestMessage request = requestGenerator.PrepareRequest(parameters, entity); ;
            string content = await GetResponse(request);
            return content;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async void DeleteAsync<T>(T entity) where T : IEntity
        {
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}/company/{1}/{2}?operation=delete", CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);
            this.requestedEntity = entity;
            RequestParameters parameters;
            if (this.serviceContext.IppConfiguration.Message.Request.SerializationFormat == Intuit.Ipp.Core.Configuration.SerializationFormat.Json)
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONJSON);
            }
            else
            {
                parameters = new RequestParameters(uri, HttpVerbType.POST, CoreConstants.CONTENTTYPE_APPLICATIONXML);
            }
            RequestGenerator requestGenerator = new RequestGenerator(serviceContext);
            HttpRequestMessage request = requestGenerator.PrepareRequest(parameters, entity); ;
            string content = await GetResponse(request);
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(content);
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Add.");
            IntuitEntity intuitEntity = restResponse.AnyIntuitObject as IntuitEntity;
            if (intuitEntity.status != EntityStatusEnum.Deleted)
            {
                IdsException exception = new IdsException(Resources.CommunicationErrorMessage, new CommunicationException(Resources.StatusNotDeleted));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));

            }
            else
            {
                Type type = this.requestedEntity.GetType();
                PropertyInfo[] propertyInfoArray = type.GetProperties();
                PropertyInfo statusPropInfo = propertyInfoArray.FirstOrDefault(pi => pi.Name == CoreConstants.STATUS);
                if (statusPropInfo != null)
                {
                    statusPropInfo.SetValue(this.requestedEntity, intuitEntity.status, null);
                }

            }
        }
    }
}
