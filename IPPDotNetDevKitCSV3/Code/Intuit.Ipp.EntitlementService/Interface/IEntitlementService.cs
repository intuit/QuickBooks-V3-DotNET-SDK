﻿////***************************************************************************************************
// <copyright file="IEntitlementService.cs" company="Intuit">
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
// <summary>This file contains interface for Entitlements service.</summary>
////***************************************************************************************************

namespace Intuit.Ipp.EntitlementService
{
    using Data;

    /// <summary>
    /// This interface specifies the EntitlementService Read operation
    /// </summary>
    public interface IEntitlementService
    {
        /// <summary>
        /// Gets Entitlements for a specified realm. The realm must be set in the context.
        /// </summary>
        /// <param name="entitlementBaseUrl"></param>
        /// <returns></returns>
        EntitlementsResponse GetEntitlements(string entitlementBaseUrl);

        /// <summary>
        /// Gets Entitlements for a specified realm. The realm must be set in the context.
        /// </summary>
        /// <param name="entitlementBaseUrl"></param>
        void GetEntitlementsAsync(string entitlementBaseUrl);
    }
}
