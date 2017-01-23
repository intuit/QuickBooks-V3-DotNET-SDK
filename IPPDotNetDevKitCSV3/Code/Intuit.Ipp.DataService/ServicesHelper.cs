////*********************************************************
// <copyright file="ServicesHelper.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains helper methods for Services.</summary>
////*********************************************************

namespace Intuit.Ipp.DataService
{
    using System;
    using Intuit.Ipp.Data;

    /// <summary>
    /// Class contains Helper Methods for Services.
    /// </summary>
    internal sealed class ServicesHelper
    {
        /// <summary>
        /// Prevents a default instance of the ServicesHelper class from being created.
        /// </summary>
        private ServicesHelper()
        {
        }

        /// <summary>
        /// Checks whether the entity passed has a type or not.
        /// </summary>
        /// <param name="entity">CDM Entity.</param>
        /// <returns>True if the type exists or else false.</returns>
        internal static bool IsTypeNull(IEntity entity)
        {
            if (entity == null || entity.GetType() == null || string.IsNullOrWhiteSpace(entity.GetType().Name))
            {
                return false;
            }

            return true;
        }
    }
}
