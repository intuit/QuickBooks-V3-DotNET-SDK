// -----------------------------------------------------------------------
// <copyright file="DataServiceTestCases.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Intuit.Ipp.DataService.Test
{
    using System.Collections.Generic;
    using Core;
    using Data;
    using System.Collections.ObjectModel;
    using System.Collections;
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class DataServiceTestCases
    {
        private ServiceContext serviceContext;
        private DataService dataService;

        public ServiceContext GetContext()
        {
            return serviceContext;
        }

        public DataServiceTestCases(ServiceContext serviceContext)
        {
            this.serviceContext = serviceContext;
            dataService = new DataService(serviceContext);
        }

        internal IEntity AddEntity(IEntity entity, Core.Configuration.SerializationFormat serializationFormat = Core.Configuration.SerializationFormat.Xml)
        {
            if (serializationFormat == Core.Configuration.SerializationFormat.Json)
            {
                serviceContext.IppConfiguration.Message.Request.SerializationFormat = Core.Configuration.SerializationFormat.Json;
            }

            IEntity addedEntity = null;
            try
            {
                addedEntity = dataService.Add(entity);
            }
            catch (Exception.IdsException ex)
            {
                serviceContext.IppConfiguration.Message.Request.SerializationFormat = Core.Configuration.SerializationFormat.Xml;
                throw;
            }

            serviceContext.IppConfiguration.Message.Request.SerializationFormat = Core.Configuration.SerializationFormat.Xml;
            return addedEntity;
        }

        internal IEnumerable<IEntity> FindAllEntities(IEntity entity, int startPosition, int maxResults)
        {
            return dataService.FindAll(entity, startPosition, maxResults);
        }

        internal IEnumerable<IEntity> FindAllEntities(IEntity entity)
        {
            return dataService.FindAll(entity);
        }

        internal IEnumerable<IEntity> FindByLevelEntities(IEntity entity)
        {
            return dataService.FindByLevel(entity);
        }

        internal IEnumerable<IEntity> FindByParentIdEntities(IEntity entity)
        {
            return dataService.FindByParentId(entity);
        }

        internal IEntity FindByIdEntity(IEntity entity)
        {
            return dataService.FindById(entity);
        }

        internal IEntity UpdateEntity(IEntity entity, Core.Configuration.SerializationFormat serializationFormat = Core.Configuration.SerializationFormat.Xml)
        {
            if (serializationFormat == Core.Configuration.SerializationFormat.Json)
            {
                serviceContext.IppConfiguration.Message.Request.SerializationFormat = Core.Configuration.SerializationFormat.Json;
            }

            IEntity updatedEntity = null;
            try
            {
                updatedEntity = dataService.Update(entity);
            }
            catch (Exception.IdsException)
            {
                serviceContext.IppConfiguration.Message.Request.SerializationFormat = Core.Configuration.SerializationFormat.Xml;
                throw;
            }

            serviceContext.IppConfiguration.Message.Request.SerializationFormat = Core.Configuration.SerializationFormat.Xml;
            return updatedEntity;
        }

        internal void VoidEntity(IEntity entity)
        {
            dataService.Void(entity);
        }

        internal void DeleteEntity(IEntity entity)
        {
            dataService.Delete(entity);
        }

        internal IntuitCDCResponse CDCEntity(List<IEntity> entity, System.DateTime changedSince)
        {
            return dataService.CDC(entity, changedSince);
        }
        
        internal Attachable Upload(Attachable entity, Stream stream)
        {
            return dataService.Upload(entity, stream);
        }

        internal byte[] Download(Attachable entity)
        {
            return dataService.Download(entity);
        }
    }
}
