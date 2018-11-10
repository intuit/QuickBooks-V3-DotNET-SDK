////*********************************************************
// <copyright file="ServiceElement.cs" company="Intuit">
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
// <summary>This file contains Service element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System.Configuration;

    /// <summary>
    /// Service element.
    /// </summary>
    public class ServiceElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the BaseUrl Element.
        /// </summary>
        [ConfigurationProperty("baseUrl")]
        public BaseUrlElement BaseUrl
        {
            get
            {
                return (BaseUrlElement)this["baseUrl"];
            }
        }

        /// <summary>
        /// Gets the MinorVersion Element.
        /// </summary>
        [ConfigurationProperty("minorVersion")]
        public MinorVersionElement MinorVersion
        {
            get
            {
                return (MinorVersionElement)this["minorVersion"];
            }
        }


        
    }
}
