// -----------------------------------------------------------------------
// <copyright file="RequestConfigurationSection.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Configuration;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RequestConfigurationSection : ConfigurationElement
    {
        [ConfigurationProperty("enableRequestResponseLogging", DefaultValue = "false", IsRequired = false)]
        public Boolean EnableRequestResponseLogging
        {
            get
            {
                return (Boolean)this["enableRequestResponseLogging"];
            }
            set
            {
                this["enableRequestResponseLogging"] = value;
            }
        }

        [ConfigurationProperty("requestResponseLoggingDirectory", DefaultValue = "C:\\Temp", IsRequired = false)]
        public string RequestResponseLoggingDirectory
        {
            get
            {
                return (string)this["requestResponseLoggingDirectory"];
            }
            set
            {
                this["requestResponseLoggingDirectory"] = value;
            }
        }
    }
}
