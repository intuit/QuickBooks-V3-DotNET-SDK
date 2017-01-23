using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Intuit.Ipp.Utility
{
    public class CustomLoggerElements : ConfigurationElement
    {
        [ConfigurationProperty("name", DefaultValue = "asda", IsRequired = false)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("type", DefaultValue = "", IsRequired = false)]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
            set
            {
                this["type"] = value;
            }
        }

        [ConfigurationProperty("enable", DefaultValue = "false", IsRequired = false)]
        public Boolean Enable
        {
            get
            {
                return (Boolean)this["enable"];
            }
            set
            {
                this["enable"] = value;
            }
        }
    }
}
