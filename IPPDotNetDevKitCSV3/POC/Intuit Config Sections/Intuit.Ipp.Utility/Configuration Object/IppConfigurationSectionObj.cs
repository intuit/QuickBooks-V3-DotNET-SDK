using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intuit.Ipp.Utility
{
    public class IppConfigurationSectionObj : IConfigurationSection
    {
        private LoggerSectionObj logger;


        public IppConfigurationSectionObj()
        {
            this.logger = new LoggerSectionObj();
        }
        public LoggerSectionObj Logger
        {
            get { return logger; }
            set { logger = value; }
        }

    }
}
