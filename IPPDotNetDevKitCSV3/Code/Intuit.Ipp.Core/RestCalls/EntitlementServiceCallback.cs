////********************************************************************
// <copyright file="EntitlementServiceCallBack.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2018 Intuit
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
// <summary>This file contains SdkException.</summary>
// <summary>This file contains logic for REST request handler.</summary>
////********************************************************************

namespace Intuit.Ipp.Core
{
    /// <summary>
    /// Contains event handlers for call back events.
    /// </summary>
    /// <typeparam name="T">Generic Constraint </typeparam>
    public static class EntitlementServiceCallback<T> where T : Intuit.Ipp.Data.EntitlementsResponse
    {
        /// <summary>
        /// Generic Event handler to handle Entitlements asynchronous call back.
        /// </summary>
        /// <param name="sender">Sender of this event.</param>
        /// <param name="entitlementCallCompletedEventArgs">Entitlements Call Completed Event Args.</param>
        public delegate void EntitlementCallCompletedEventHandler(object sender, EntitlementCallCompletedEventArgs<T> entitlementCallCompletedEventArgs);
    }
}
