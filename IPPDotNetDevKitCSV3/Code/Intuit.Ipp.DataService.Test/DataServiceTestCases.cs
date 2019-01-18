// -----------------------------------------------------------------------
// <copyright file="DataServiceTestCases.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Intuit.Ipp.DataService.Test
{
    using System.Collections.Generic;
    using Intuit.Ipp.Core;
    using Intuit.Ipp.Data;
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
            return this.serviceContext;
        }

        public DataServiceTestCases(ServiceContext serviceContext)
        {
            this.serviceContext = serviceContext;
            this.dataService = new DataService(serviceContext);
        }

        internal IEntity AddEntity(IEntity entity, Core.Configuration.SerializationFormat serializationFormat = Core.Configuration.SerializationFormat.Xml)
        {
            if (serializationFormat == Core.Configuration.SerializationFormat.Json)
            {
                this.serviceContext.IppConfiguration.Message.Request.SerializationFormat = Core.Configuration.SerializationFormat.Json;
            }

            IEntity addedEntity = null;
            try
            {
                addedEntity = this.dataService.Add(entity);
            }
            catch (Intuit.Ipp.Exception.IdsException ex)
            {
                this.serviceContext.IppConfiguration.Message.Request.SerializationFormat = Core.Configuration.SerializationFormat.Xml;
                throw ex;
            }

            this.serviceContext.IppConfiguration.Message.Request.SerializationFormat = Core.Configuration.SerializationFormat.Xml;
            return addedEntity;
        }

        internal IEnumerable<IEntity> FindAllEntities(IEntity entity, int startPosition, int maxResults)
        {
            return this.dataService.FindAll(entity, startPosition, maxResults);
        }

        internal IEnumerable<IEntity> FindAllEntities(IEntity entity)
        {
            return this.dataService.FindAll(entity);
        }

        internal IEnumerable<IEntity> FindByLevelEntities(IEntity entity)
        {
            return this.dataService.FindByLevel(entity);
        }

        internal IEnumerable<IEntity> FindByParentIdEntities(IEntity entity)
        {
            return this.dataService.FindByParentId(entity);
        }

        internal IEntity FindByIdEntity(IEntity entity)
        {
            return this.dataService.FindById(entity);
        }

        internal IEntity UpdateEntity(IEntity entity, Core.Configuration.SerializationFormat serializationFormat = Core.Configuration.SerializationFormat.Xml)
        {
            if (serializationFormat == Core.Configuration.SerializationFormat.Json)
            {
                this.serviceContext.IppConfiguration.Message.Request.SerializationFormat = Core.Configuration.SerializationFormat.Json;
            }

            IEntity updatedEntity = null;
            try
            {
                updatedEntity = this.dataService.Update(entity);
            }
            catch (Intuit.Ipp.Exception.IdsException)
            {
                this.serviceContext.IppConfiguration.Message.Request.SerializationFormat = Core.Configuration.SerializationFormat.Xml;
                throw;
            }

            this.serviceContext.IppConfiguration.Message.Request.SerializationFormat = Core.Configuration.SerializationFormat.Xml;
            return updatedEntity;
        }

        internal void VoidEntity(IEntity entity)
        {
            this.dataService.Void(entity);
        }

        internal void DeleteEntity(IEntity entity)
        {
            this.dataService.Delete(entity);
        }

        internal IntuitCDCResponse CDCEntity(List<IEntity> entity, System.DateTime changedSince)
        {
            return this.dataService.CDC(entity, changedSince);
        }
        
        internal Attachable Upload(Attachable entity, System.IO.Stream stream)
        {
            return this.dataService.Upload(entity, stream);
        }

        internal byte[] Download(Attachable entity)
        {
            return this.dataService.Download(entity);
        }
    }
}
