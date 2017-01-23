using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intuit.Ipp.Utility
{
   public class RequestConfigurationSectionObj
    {
        private bool enableRequestResponseLogging;
        private string requestResponseLoggingDirectory;

        public RequestConfigurationSectionObj()
        {
            this.enableRequestResponseLogging = false;
            this.requestResponseLoggingDirectory = "From constructor";
        }
        public bool EnableRequestResponseLogging
        {
            get { return enableRequestResponseLogging; }
            set { enableRequestResponseLogging = value; }
        }
       

       public string RequestResponseLoggingDirectory
       {
           get { return requestResponseLoggingDirectory; }
           set { requestResponseLoggingDirectory = value; }
       }
    }
}
