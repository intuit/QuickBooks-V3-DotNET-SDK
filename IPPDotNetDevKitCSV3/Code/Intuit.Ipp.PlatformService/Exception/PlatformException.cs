using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intuit.Ipp.PlatformService
{
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
