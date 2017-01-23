////********************************************************************
// <copyright file="IntuitResponseStatus.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains enumerations related to CRUD Operations and batch processing.</summary>
////********************************************************************

namespace Intuit.Ipp.DataService
{
    /// <summary>
    /// status of response for delete and void operation 
    /// </summary>
    public enum IntuitResponseStatus
    {
        /// <summary>
        /// Entity has been made void. 
        /// </summary>
        Voided,

        /// <summary>
        /// entity has been deleted.
        /// </summary>
        Deleted
    }

    /// <summary>
    /// type of batch response
    /// </summary>
    public enum ResponseType
    {
        /// <summary>
        /// batch response has single entity 
        /// </summary>
        Entity,

        /// <summary>
        /// batch response has more than one enitity. 
        /// </summary>
        Query,

        /// <summary>
        /// batch response has exception.
        /// </summary>
        Exception,

        /// <summary>
        /// batch response has CDCQuery.
        /// </summary>
        CdcQuery
    }
}
