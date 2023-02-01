////********************************************************************
// <copyright file="FaultExtensions.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2023 Intuit
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
// <summary>Contains extension methods for the Fault data type.</summary>
////********************************************************************

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Intuit.Ipp.Data;
using Intuit.Ipp.Exception;

namespace Intuit.Ipp.DataService
{
    /// <summary>
    /// Fault extensions class
    /// </summary>
    internal static class FaultExtensions
    {
        /// <summary>
        /// Prepare IdsException out of Fault object.
        /// </summary>
        /// <param name="fault">Fault object.</param>
        /// <returns> returns IdsException object.</returns>
        internal static IdsException IterateFaultAndPrepareException(this Fault fault)
        {
            if (fault == null)
            {
                return null;
            }

            IdsException idsException = null;

            // Create a list of exceptions.
            List<IdsError> aggregateExceptions = new List<IdsError>();

            // Check whether the fault is null or not.
            if (fault != null)
            {
                // Fault types can be of Validation, Service, Authentication and Authorization. Run them through the switch case.
                switch (fault.type)
                {
                    // If Validation errors iterate the Errors and add them to the list of exceptions.
                    case "Validation":
                    case "ValidationFault":
                        if (fault.Error != null && fault.Error.Count() > 0)
                        {
                            foreach (var item in fault.Error)
                            {
                                // Add commonException to aggregateExceptions
                                // CommonException defines four properties: Message, Code, Element, Detail.
                                aggregateExceptions.Add(new IdsError(item.Message, item.code, item.element, item.Detail));
                            }

                            // Throw specific exception like ValidationException.
                            idsException = new ValidationException(aggregateExceptions);
                        }

                        break;

                    // If Validation errors iterate the Errors and add them to the list of exceptions.
                    case "Service":
                    case "ServiceFault":
                        if (fault.Error != null && fault.Error.Count() > 0)
                        {
                            foreach (var item in fault.Error)
                            {
                                // Add commonException to aggregateExceptions
                                // CommonException defines four properties: Message, Code, Element, Detail.
                                aggregateExceptions.Add(new IdsError(item.Message, item.code, item.element, item.Detail));
                            }

                            // Throw specific exception like ServiceException.
                            idsException = new ServiceException(aggregateExceptions);
                        }

                        break;

                    // If Validation errors iterate the Errors and add them to the list of exceptions.
                    case "Authentication":
                    case "AuthenticationFault":
                    case "Authorization":
                    case "AuthorizationFault":
                        if (fault.Error != null && fault.Error.Count() > 0)
                        {
                            foreach (var item in fault.Error)
                            {
                                // Add commonException to aggregateExceptions
                                // CommonException defines four properties: Message, Code, Element, Detail.
                                aggregateExceptions.Add(new IdsError(item.Message, item.code, item.element, item.Detail));
                            }

                            // Throw specific exception like AuthenticationException which is wrapped in SecurityException.
                            idsException = new SecurityException(aggregateExceptions);
                        }

                        break;

                    // Use this as default if there was some other type of Fault
                    default:
                        if (fault.Error != null && fault.Error.Count() > 0)
                        {
                            foreach (var item in fault.Error)
                            {
                                // Add commonException to aggregateExceptions
                                // CommonException defines four properties: Message, Code, Element, Detail.
                                aggregateExceptions.Add(new IdsError(item.Message, item.code, item.element, item.Detail));
                            }

                            // Throw generic exception like IdsException.
                            idsException =
                                new IdsException(
                                    string.Format(CultureInfo.InvariantCulture,
                                        "Fault Exception of type: {0} has been generated.", fault.type),
                                    aggregateExceptions);
                        }

                        break;
                }
            }

            // Return idsException which will be of type Validation, Service or Security.
            return idsException;
        }
    }
}
