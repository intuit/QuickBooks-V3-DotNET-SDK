////********************************************************************
// <copyright file="QueryOperationType.cs" company="Intuit">
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
// <summary>Contains enumeration value for Query Operation Type.</summary>
////********************************************************************

namespace Intuit.Ipp.QueryFilter
{
    /// <summary>
    /// Contains enumeration value for Query Operation Type.
    /// </summary>
    public enum QueryOperationType
    {
        /// <summary>
        /// Entity query.
        /// </summary>
        query,
        
        /// <summary>
        /// Report query.
        /// </summary>
        report,

        /// <summary>
        /// Change Data query.
        /// </summary>
        changedata
    }
}
