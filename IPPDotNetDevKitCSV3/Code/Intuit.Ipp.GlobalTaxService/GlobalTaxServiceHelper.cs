
////*********************************************************
// <copyright file="GlobalTaxServiceHelper.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains helper methods for GlobalTaxService.</summary>
////*********************************************************


namespace Intuit.Ipp.GlobalTaxService
{
    using System;
    using Intuit.Ipp.Data;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Class contains Helper Methods for Services.
    /// </summary>
    internal sealed class GlobalTaxServiceHelper
    {
        /// <summary>
        /// Prevents a default instance of the ServicesHelper class from being created.
        /// </summary>
        private GlobalTaxServiceHelper()
        {
        }

        /// <summary>
        /// Checks whether the entity passed has a type or not.
        /// </summary>
        /// <param name="entity">CDM Entity.</param>
        /// <returns>True if the type exists or else false.</returns>
        internal static bool IsTypeNull(Intuit.Ipp.Data.TaxService taxCodeName)
        {
            if (taxCodeName == null || taxCodeName.GetType() == null || string.IsNullOrWhiteSpace(taxCodeName.GetType().Name))
            {
                return false;
            }

            return true;
        }
    }
}
