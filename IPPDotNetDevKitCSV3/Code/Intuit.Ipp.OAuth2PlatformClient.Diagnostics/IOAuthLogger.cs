////*********************************************************
// <copyright file="IOAuthLogger.cs" company="Intuit">
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

namespace Intuit.Ipp.OAuth2PlatformClient.Diagnostics
{
    /// <summary>
    /// Logger for OAuth requests and responses.
    /// </summary>
    public interface IOAuthLogger
    {
        /// <summary>
        /// Log a simple message.
        /// </summary>
        /// <param name="message">The message.</param>
        void Log(string message);
    }

    /// <summary>
    /// Null logger.
    /// </summary>
    public class NullOAuthLogger : IOAuthLogger
    {
        /// <summary>
        /// Singleton instance of <see cref="NullOAuthLogger"/>.
        /// </summary>
        public static readonly IOAuthLogger Instance = new NullOAuthLogger();

        private NullOAuthLogger()
        {
        }

        void IOAuthLogger.Log(string messageToWrite)
        {
        }
    }
}
