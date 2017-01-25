////*********************************************************
// <copyright file="TraceLevel.cs" company="Intuit">
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
// <summary>This file contains SdkException.</summary>
// <summary>This file defines the trace levels..</summary>
////*********************************************************

namespace Intuit.Ipp.Diagnostics
{
    /// <summary>
    /// Specifies what level of messages to output.
    /// </summary>
    public enum TraceLevel
    {
        /// <summary>
        /// Output no tracing and debugging messages.
        /// </summary>
        Off = 0,
        
        /// <summary>
        /// Output error-handling messages.
        /// </summary>
        Error = 1,
        
        /// <summary>
        /// Output warnings and error-handling messages.
        /// </summary>
        Warning = 2,
        
        /// <summary>
        /// Output informational messages, warnings, and error-handling messages.
        /// </summary>
        Info = 3,
        
        /// <summary>
        /// Output all debugging and tracing messages.
        /// </summary>
        Verbose = 4,
    }
}
