////***************************************************************************************************
// <copyright file="IDataService.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains interfaces for data service.</summary>
////***************************************************************************************************

namespace Intuit.Ipp.DataService
{
    using System.Collections.ObjectModel;
    using Intuit.Ipp.Data;

    /// <summary>
    /// This interface specifies the Sync CRUD operations for IDS. 
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Adds an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Add.</param>
        /// <returns>Returns an updated version of the entity with updated identifier and sync token.</returns>
        T Add<T>(T entity) where T : IEntity;

        /// <summary>
        /// Deletes an entity under the specified realm. The realm must be set in the context
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Delete.</param>
        T Delete<T>(T entity) where T : IEntity;

        /// <summary>
        /// Voids an entity under the specified realm. The realm must be set in the context
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Delete.</param>
        T Void<T>(T entity) where T : IEntity;

        /// <summary>
        /// Updates an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">Entity to Update.</param>
        /// <returns>Returns an updated version of the entity with updated identifier and sync token.</returns>
        T Update<T>(T entity) where T : IEntity;

        /// <summary>
        /// Gets an entity under the specified realm. The realm must be set in the context.
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity"> Entity type to Find.</param>    
        /// <returns> Returns an entity of specified Id.</returns> 
        T FindById<T>(T entity) where T : IEntity;

        /// <summary>
        /// Gets a list of all entities of type T under the specified realm. The realm must be set in the context
        /// </summary>
        /// <typeparam name="T">Generic Type T.</typeparam>
        /// <param name="entity">The entity for which the data is required.</param>
        /// <param name="pageNumber">The Page number to retrieve.</param>
        /// <param name="pageSize">The number of record fetch in one call. Max size supported by QBO is 1000.</param>
        /// <returns> Returns the list of entities.</returns>"
        ReadOnlyCollection<T> FindAll<T>(T entity, int pageNumber, int pageSize) where T : IEntity;
    }
}
