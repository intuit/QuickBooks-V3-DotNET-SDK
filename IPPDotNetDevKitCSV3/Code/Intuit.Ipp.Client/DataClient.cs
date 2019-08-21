using Intuit.Ipp.Client.Properties;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.Diagnostics;
using Intuit.Ipp.Exception;
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

        /// <summary>
        /// 
        /// </summary>
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
            string content = "";
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method FindById Asynchronously.");
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
            }
            else
            {
                try
                {
                    AsynchronousService asyncService = new AsynchronousService(this.serviceContext);
                    content = await asyncService.AddAsync<T>(entity);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);

                }
            }
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(content);
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Add.");
            IEntity entityResponse = restResponse.AnyIntuitObject as IEntity;
            return (T)entityResponse;
        }

        /// <summary>
        ///  Updates an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> UpdateAsync<T>(T entity) where T : IEntity
        {
            string content = "";
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method FindById Asynchronously.");
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
            }
            else
            {
                try
                {
                    AsynchronousService asyncService = new AsynchronousService(this.serviceContext);
                    content = await asyncService.UpdateAsync<T>(entity);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);

                }
            }
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(content);
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Add.");
            IEntity entityResponse = restResponse.AnyIntuitObject as IEntity;
            return (T)entityResponse;
        }

        /// <summary>
        ///  Deletes an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async void DeleteAsync<T>(T entity) where T : IEntity
        {
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method FindById Asynchronously.");
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
            }
            else
            {
                try
                {
                    AsynchronousService asyncService = new AsynchronousService(this.serviceContext);
                    asyncService.DeleteAsync<T>(entity);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);

                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> FindAllAsync<T>(T entity, int startPosition = 1, int maxResults = 500) where T : IEntity
        {
            string content = "";
            List<T> entities = new List<T>();
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method FindById Asynchronously.");
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
            }
            else
            {
                if (entity.GetType().Name == "TaxClassification")
                {
                    try
                    {
                        AsynchronousService asyncService = new AsynchronousService(this.serviceContext);

                        content = await asyncService.FindAllAsync<T>(entity, startPosition, maxResults);
                    }
                    catch (SystemException systemException)
                    {
                        this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                        IdsException idsException = new IdsException(systemException.Message);

                    }
                }
                else
                {
                    if (startPosition <= 0)
                    {
                        IdsException exception = new IdsException(Resources.ParameterZeroNegativeValueMessage, new ArgumentException(Resources.PageNumberString));
                        this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                        return null;
                    }

                    if (maxResults <= 0)
                    {
                        IdsException exception = new IdsException(Resources.ParameterZeroNegativeValueMessage, new ArgumentException(Resources.PageSizeString));
                        this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                        return null;
                    }

                    try
                    {
                        AsynchronousService asyncService = new AsynchronousService(this.serviceContext);
                        content = await asyncService.FindAllAsync<T>(entity, startPosition, maxResults);
                    }
                    catch (SystemException systemException)
                    {
                        this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                        IdsException idsException = new IdsException(systemException.Message);

                    }
                }

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
              
            }
            ReadOnlyCollection<T> readOnlyCollection = new ReadOnlyCollection<T>(entities);

             return readOnlyCollection;
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<T> FindByIdAsync<T>(T entity) where T : IEntity
        {
            string content = "";
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Called Method FindById Asynchronously.");
            if (!ServicesHelper.IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
            }
            else
            {
                try
                {
                    AsynchronousService asyncService = new AsynchronousService(this.serviceContext);
                    content = await asyncService.FindByIdAsync<T>(entity);
                }
                catch (SystemException systemException)
                {
                    this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(TraceLevel.Error, systemException.Message);
                    IdsException idsException = new IdsException(systemException.Message);

                }
            }
            IntuitResponse restResponse = (IntuitResponse)CoreHelper.GetSerializer(this.serviceContext, false).Deserialize<IntuitResponse>(content);
            this.serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Info, "Finished Executing Method Add.");
            IEntity entityResponse = restResponse.AnyIntuitObject as IEntity;
            return (T)entityResponse;
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