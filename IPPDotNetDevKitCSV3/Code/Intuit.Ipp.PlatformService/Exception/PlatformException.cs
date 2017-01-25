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

namespace Intuit.Ipp.PlatformService
{


    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// PlatformException class
    /// </summary>
    public class PlatformException : System.Exception
    {
        /// <summary>
        /// ErrorCode
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// ErrorMessage
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// ServerTime
        /// </summary>
        public string ServerTime { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PlatformException( string errCode, string errMsg, string serverTime = null) {
            this.ErrorCode = errCode;
            this.ErrorMessage = errMsg;
            this.ServerTime = serverTime;
        }
    }
}
