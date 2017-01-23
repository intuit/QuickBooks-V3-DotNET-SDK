
////***************************************************************************************************
// <copyright file="IGlobalTaxService.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
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
        /// <param name="entity">TaxService entity to Add.</param>
        /// <returns>Returns an updated version of the entity with updated identifier.</returns>
        
        TaxService AddTaxCode(TaxService taxcode);
    }
}


