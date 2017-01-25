////********************************************************************
// <copyright file="IRequestValidator.cs" company="Intuit">
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
// <summary>This file contains Request validate contract.</summary>
////********************************************************************

namespace Intuit.Ipp.Security
{
    using System.Net;

    /// <summary>
    /// Interface for request validate
    /// </summary>
    public interface IRequestValidator
    {
        /// <summary>
        /// Authorizes the web request.
        /// </summary>
        /// <param name="webRequest">The web request.</param>
        /// <param name="requestBody">The request body.</param>
        void Authorize(WebRequest webRequest, string requestBody);
    }
}
