
////***************************************************************************************************
// <copyright file="IGlobalTaxService.cs" company="Intuit">
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
// <summary>This file contains interface for Tax service.</summary>
////***************************************************************************************************


namespace Intuit.Ipp.GlobalTaxService.Interface
{



    using System.Collections.ObjectModel;
    using Intuit.Ipp.Data;


    /// <summary>
    /// This interface specifies the Sync CRUD operations for IDS. 
    /// </summary>
    interface IGlobalTaxService
    {
        /// <summary>
        /// Adds TaxService entity under the specified realm. The realm must be set in the context.
        /// </summary>        
        /// <param name="taxCode">TaxService entity to Add.</param>
        /// <returns>Returns an updated version of the entity with updated identifier.</returns>
        
        TaxService AddTaxCode(TaxService taxcode);
    }
}


