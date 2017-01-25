////********************************************************************
// <copyright file="BatchProcessingCallback.cs" company="Intuit">
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
// <summary>This file contains Contains event handler for batch processing call back event.</summary>
////********************************************************************

namespace Intuit.Ipp.DataService
{
    /// <summary>
    /// Contains event handler for call back event.
    /// </summary>

    public static class BatchProcessingCallback 
    {
        /// <summary>
        /// event handler to handle call back for batch exceution completion
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="eventArgs">The <see cref="Intuit.Ipp.DataService.BatchExecutionCompletedEventArgs"/> instance containing the event data.</param>
        public delegate void BatchExecutionCompletedEventHandler(object sender, BatchExecutionCompletedEventArgs eventArgs);
    }
}
