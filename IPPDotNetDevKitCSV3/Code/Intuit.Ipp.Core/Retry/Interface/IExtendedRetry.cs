////*********************************************************
// <copyright file="IExtendedRetry.cs" company="Intuit">
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
// <summary>This file contains IExtendedRetryExceptions contracts.</summary>
////***************************************************
//namespace Intuit.Ipp.Retry  
namespace Intuit.Ipp.Core
{
    using System;

    /// <summary>
    /// Custom exception retry strategy contracts.
    /// </summary>
    public interface IExtendedRetry
    {
        /// <summary>
        /// Determines whether [is retry exception] [the specified ex].
        /// </summary>
        /// <param name="ex">The exception object.</param>
        /// <returns>
        ///   <c>true</c> if [is parameter (ex) is retry exception]; otherwise, <c>false</c>.
        /// </returns>
        bool IsRetryException(Exception ex);
    }
}
