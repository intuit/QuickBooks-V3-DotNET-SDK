
////*********************************************************
// <copyright file="GlobalTaxServiceHelper.cs" company="Intuit">
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
// <summary>This file contains helper methods for GlobalTaxService.</summary>
////*********************************************************


namespace Intuit.Ipp.GlobalTaxService
{
    using Data;

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
        internal static bool IsTypeNull(TaxService taxCodeName)
        {
            if (taxCodeName == null || taxCodeName.GetType() == null || string.IsNullOrWhiteSpace(taxCodeName.GetType().Name))
            {
                return false;
            }

            return true;
        }
    }
}
