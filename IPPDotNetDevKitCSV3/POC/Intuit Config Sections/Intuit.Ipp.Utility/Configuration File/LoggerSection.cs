using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Intuit.Ipp.Utility
{
     public class LoggerSection : ConfigurationElement
    {
         [ConfigurationProperty("customLogger")]
         public CustomLoggerElements CustomLogger
         {
             get
             {
                 return (CustomLoggerElements)this["customLogger"];
             }
             set
             {
                 this["customLogger"] = value;
             }
         }

         [ConfigurationProperty("requestLog")]
         public RequestConfigurationSection RequestSettings
         {
             get
             {
                 return (RequestConfigurationSection)this["requestLog"];
             }
             set
             {
                 this["requestLog"] = value;
             }
         }


    }
}
