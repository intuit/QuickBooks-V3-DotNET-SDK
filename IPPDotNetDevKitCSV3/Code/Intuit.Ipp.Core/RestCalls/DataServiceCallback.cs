////********************************************************************
// <copyright file="DataServiceCallBack.cs" company="Intuit">
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
// <summary>This file contains logic for REST request handler.</summary>
////********************************************************************

namespace Intuit.Ipp.Core
{
    /// <summary>
    /// Contains event handlers for call back events.
    /// </summary>
    /// <typeparam name="T">Generic Constraint </typeparam>
    public static class DataServiceCallback<T> where T : Intuit.Ipp.Data.IEntity
    {
        /// <summary>
        /// Generic event handler to handle multiple call backs.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="callCompletedEventArgs">Call Completed Event Args.</param>
        public delegate void CallCompletedEventHandler(object sender, CallCompletedEventArgs<T> callCompletedEventArgs);

        /// <summary>
        /// Generic event handler to handle multiple pdf call backs.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="pdfCallCompletedEventArgs">Pdf Call Completed Event Args.</param>
        public delegate void PdfCallCompletedEventHandler(object sender, PdfCallCompletedEventArgs pdfCallCompletedEventArgs);

        /// <summary>
        /// Event handler to handle FindAll asynchronous call back.
        /// </summary>
        /// <param name="sender">Sender of this event.</param>
        /// <param name="findAllCallCompletedEventArgs">FindAll Call Completed Event Args.</param>
        public delegate void FindAllCallCompletedEventHandler(object sender, FindAllCallCompletedEventArgs findAllCallCompletedEventArgs);

        /// <summary>
        /// Event handler to handle CDC asynchronous call back.
        /// </summary>
        /// <param name="sender">Sender of this event.</param>
        /// <param name="cdcCallCompletedEventArgs">CDC Call Completed Event Args.</param>
        public delegate void CDCCallCompletedEventHandler(object sender, CDCCallCompletedEventArgs cdcCallCompletedEventArgs);

        /// <summary>
        /// Generic Event handler to handle asynchronous call back.
        /// </summary>
        /// <param name="sender">Sender of this event.</param>
        /// <param name="asyncCallCompletedEventArgs">Async Call Completed Event Args.</param>
        public delegate void AsyncCallCompletedEventHandler(object sender, AsyncCallCompletedEventArgs asyncCallCompletedEventArgs);
    }
}
