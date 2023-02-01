﻿////*********************************************************
// <copyright file="ILogger.cs" company="Intuit">
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
// <summary>This file contains an interface for Logging.</summary>
////*********************************************************

namespace Intuit.Ipp.Diagnostics
{
    /// <summary>
    /// Interface used to log messages. 
    /// </summary>
    public interface IAdvancedLogger
    {
        /// <summary>
        /// Logs messages
        /// </summary>
        /// <param name="messageToWrite">The message to write.</param>
        void Log(string messageToWrite);
    }

    /// <summary>
    /// Null logger.
    /// </summary>
    public class NullAdvancedLogger : IAdvancedLogger
    {
        /// <summary>
        /// Singleton instance of <see cref="NullAdvancedLogger"/>.
        /// </summary>
        public static readonly IAdvancedLogger Instance = new NullAdvancedLogger();

        private NullAdvancedLogger()
        {
        }

        void IAdvancedLogger.Log(string messageToWrite)
        {
        }
    }
}
