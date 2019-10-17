////*********************************************************
// <copyright file="IppConfiguration.cs" company="Intuit">
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

// <summary>This file contains Ipp Configuration.</summary>
////*********************************************************

namespace Intuit.Ipp.Core.Configuration
{
   
    //using Intuit.Ipp.Retry;  
    using Intuit.Ipp.Security;
    


    /// <summary>
    /// Ipp configuration.
    /// </summary>
    public class IppConfiguration
    {
        /// <summary>
        /// Gets or sets the Serilog Logger mechanism.
        /// </summary>
        public AdvancedLogger AdvancedLogger { get; set; }


        /// <summary>
        /// Gets or sets the Logger mechanism.
        /// </summary>
        public Logger Logger { get; set; }

        /// <summary>
        /// Gets or sets the Security mechanism like OAuth.
        /// </summary>
        public IRequestValidator Security { get; set; }

        /// <summary>
        /// Gets or sets the Message settings like Compression, Serialization.
        /// </summary>
        public Message Message { get; set; }

        /// <summary>
        /// Gets or sets the Retry Policy used to retry service calls when Retry-able Exceptions are generated.
        /// </summary>
        //public IntuitRetryPolicy RetryPolicy { get; set; }//Nemo
        public IntuitRetryPolicy RetryPolicy { get; set; }

        /// <summary>
        /// Gets or sets the Base Urls like Pre-Production url's.
        /// </summary>
        public BaseUrl BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the Webhooks Verifier token.
        /// </summary>
        public VerifierToken VerifierToken { get; set; }

        /// <summary>
        /// Gets or sets the minorVersion
        /// </summary>
        public MinorVersion MinorVersion { get; set; }

       
    }
}
