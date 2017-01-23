using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intuit.Ipp.Utility
{
    public class LoggerSectionObj
    {
        private CustomLoggerElementsObj customLogger;
        private RequestConfigurationSectionObj requestSettings;

        public LoggerSectionObj()
        {
            this.customLogger = new CustomLoggerElementsObj();
            this.requestSettings = new RequestConfigurationSectionObj();
        }

        public CustomLoggerElementsObj CustomLogger
        {
            get { return customLogger; }
            set { customLogger = value; }
        }


        public RequestConfigurationSectionObj RequestSettings
        {
            get { return requestSettings; }
            set { requestSettings = value; }
        }
    }
}
