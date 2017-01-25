////********************************************************************
// <copyright file="IntuitResponseStatus.cs" company="Intuit">
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
