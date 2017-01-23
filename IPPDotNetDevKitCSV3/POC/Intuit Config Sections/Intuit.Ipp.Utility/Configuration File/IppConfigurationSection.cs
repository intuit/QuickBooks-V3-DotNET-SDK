// -----------------------------------------------------------------------
// <copyright file="IppConfigurationSection.cs" company="">
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
    public class IppConfigurationSection : ConfigurationSection, IConfigurationSection
    {
       
        [ConfigurationProperty("logger")]
        public LoggerSection Logger
        {
            get
            {
                return (LoggerSection)this["logger"];
            }
            set
            {
                this["logger"] = value;
            }
        }

      }
   }
