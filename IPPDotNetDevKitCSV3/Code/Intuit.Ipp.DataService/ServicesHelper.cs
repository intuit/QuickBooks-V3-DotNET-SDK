////*********************************************************
// <copyright file="ServicesHelper.cs" company="Intuit">
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
// <summary>This file contains helper methods for Services.</summary>
////*********************************************************

namespace Intuit.Ipp.DataService
{
    using System;
    using System.Globalization;
    using System.Reflection;
    using Intuit.Ipp.Core;
    using Intuit.Ipp.Data;
    using Intuit.Ipp.DataService.Properties;
    using Intuit.Ipp.Exception;

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

        /// <summary>
        /// Validates if entity is null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="serviceContext"></param>
        internal static void ValidateEntity<T>(T entity, ServiceContext serviceContext) where T : IEntity
        {
            if (!IsTypeNull(entity))
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }
        }

        /// <summary>
        /// Validate if passed object is null
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="serviceContext"></param>
        internal static void ValidateObject(Object obj, ServiceContext serviceContext)
        {
            if (obj == null)
            {
                IdsException exception = new IdsException(Resources.ParameterNotNullMessage, new ArgumentNullException(Resources.EntityString));
                serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }
        }

        /// <summary>
        /// Validate if IntuitEntity was converted successfully
        /// </summary>
        /// <param name="intuitEntity"></param>
        /// <param name="serviceContext"></param>
        internal static void ValidateIntuitEntity(IntuitEntity intuitEntity, ServiceContext serviceContext)
        {
            if (intuitEntity == null)
            {
                IdsException exception = new IdsException(Resources.EntityConversionFailedMessage);
                serviceContext.IppConfiguration.Logger.CustomLogger.Log(Diagnostics.TraceLevel.Error, string.Format(CultureInfo.InvariantCulture, Resources.ExceptionGeneratedMessage, exception.ToString()));
                IdsExceptionManager.HandleException(exception);
            }
        }

        /// <summary>
        /// Get ParentRef field of TaxClassification object
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="serviceContext"></param>
        /// <returns></returns>
        internal static ReferenceType PrepareTaxClassificationByParentId(IEntity entity, ServiceContext serviceContext)
        {
            string parentId = string.Empty;
            PropertyInfo parentRefProp = entity.GetType().GetProperty("ParentRef");
            ReferenceType parentRef = (ReferenceType)parentRefProp.GetValue(entity);
            return parentRef;
            //ValidateObject(parentRef, serviceContext);
            //parentId = parentRef.Value;
            //return parentId;
        }

        /// <summary>
        /// Get Level field of TaxClassification object
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="serviceContext"></param>
        /// <returns></returns>
        internal static string PrepareTaxClassificationByLevel(IEntity entity, ServiceContext serviceContext)
        {
            string level = string.Empty;
            PropertyInfo levelProp = entity.GetType().GetProperty("Level");
            level = (string)levelProp.GetValue(entity);
            return level;
        }
    }
}
