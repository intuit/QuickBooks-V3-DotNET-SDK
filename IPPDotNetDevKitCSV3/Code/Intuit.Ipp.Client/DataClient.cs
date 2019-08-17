using Intuit.Ipp.Client.Properties;
using Intuit.Ipp.Core;
using Intuit.Ipp.Core.Rest;
using Intuit.Ipp.Data;
using Intuit.Ipp.Exception;
using Intuit.Ipp.Security;
using Intuit.Ipp.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
namespace Intuit.Ipp.Client
{
    public class DataClient
    {
        /// <summary>
        /// The Service context object.
        /// </summary>
        private ServiceContext serviceContext;

        protected HttpClient client;

        public DataClient(ServiceContext serviceContext)
        {
            client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            ServiceContextValidation(serviceContext);
            this.serviceContext = serviceContext;
           // Set the Service type to QBO by calling a method.
            this.serviceContext.UseDataServices();
        }

        /// <summary>
        /// Adds an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Add.</param>
        /// <returns>Returns an updated version of the entity with updated identifier and sync token.</returns>
        public async Task<T> AddAsync<T>(T entity) where T : IEntity
        {
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}{1}/company/{2}/{3}", this.serviceContext.BaseUrl, CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

            HttpRequestMessage request = PrepareBody(entity,uri);
            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
            
            string content = "";
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
            {
                 content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                
            }
            CoreHelper.CheckNullResponseAndThrowException(content);
            // de serialize object
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(content);
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Add.");
            return (T)(restResponse.AnyIntuitObject as IEntity);
        }

        /// <summary>
        ///  Updates an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> UpdateAsync<T>(T entity) where T : IEntity
        {
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}{1}/company/{2}/{3}", this.serviceContext.BaseUrl, CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

            HttpRequestMessage request = PrepareBody(entity, uri);
            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

            string content = "";
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
            {
                content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            }
            CoreHelper.CheckNullResponseAndThrowException(content);
            // de serialize object
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(content);
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Add.");
            return (T)(restResponse.AnyIntuitObject as IEntity);
        }
        /// <summary>
        ///  Deletes an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> DeleteAsync<T>(T entity) where T : IEntity
        {
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}{1}/company/{2}/{3}?operation=delete", this.serviceContext.BaseUrl, CoreConstants.VERSION, this.serviceContext.RealmId, resourceString);

            HttpRequestMessage request = PrepareBody(entity, uri);
            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);

            string content = "";
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
            {
                content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            }
            CoreHelper.CheckNullResponseAndThrowException(content);
            // de serialize object
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(content);
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Add.");
            return (T)(restResponse.AnyIntuitObject as IEntity);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindAllAsync<T>(T entity, int startPosition = 1, int maxResults = 500) where T : IEntity
        {
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            string query = string.Format(CultureInfo.InvariantCulture, "select * from {0} startPosition {1} maxResults {2}", resourceString, startPosition, maxResults);
            string uri = string.Format(CultureInfo.InvariantCulture, "{0}{1}/company/{2}/query?query={3}", this.serviceContext.BaseUrl, CoreConstants.VERSION, this.serviceContext.RealmId, query);

            List<T> entities = new List<T>();
            
            HttpRequestMessage request = PrepareBody(entity, uri);
            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
            string content = "";
            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.BadRequest)
            {
                content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);//errorDetail can be added here if required for BadRequest.

            }
            CoreHelper.CheckNullResponseAndThrowException(content);
            // de serialize object
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(content);
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Add.");
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
            ReadOnlyCollection<T> readOnlyCollection = new ReadOnlyCollection<T>(entities);

            //JObject jObject = JObject.Parse(content);
            return readOnlyCollection;
        }

         private HttpRequestMessage PrepareBody<T>(T entity,string uri) where T : IEntity
        {
            string resourceString = entity.GetType().Name.ToLower(CultureInfo.InvariantCulture);
            string accessToken = "";
            if (this.serviceContext.IppConfiguration.Security != null)
            {
                OAuth2RequestValidator tokens = (OAuth2RequestValidator)this.serviceContext.IppConfiguration.Security;
                accessToken = tokens.AccessToken;

            }
         
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
          //  request.Content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            return request;
        }


 
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

    }
}
