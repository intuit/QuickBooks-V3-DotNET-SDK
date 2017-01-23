// -----------------------------------------------------------------------
// <copyright file="IntuitConfigurationReader.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SDKConfiguration
    {
        //public static IppConfigurationSection IppConfig = (IppConfigurationSection)System.Configuration.ConfigurationManager.GetSection("intuit/ipp");

        public static Func<string, IConfigurationSection> configurationPublisher;            

        static SDKConfiguration()
        {
            configurationPublisher = GetConfigValuesFromLocal;
        }

        public static void SetConfigurationPublisher(Func<string, IConfigurationSection> configurationPublisher)
        {
            SDKConfiguration.configurationPublisher = configurationPublisher;
        }

        public static IppConfigurationSection GetConfigurationValue(string configName)
        {
            IppConfigurationSection configValue = null;
            if (configurationPublisher == null)
            {
                throw new InvalidOperationException("Configuration publisher has not been set");
            }
            // Here we can add a piece of code to handle a situation where the user provided delegate does not return a config value. In which case we read from our default config provider
            try
            {
                configValue = (IppConfigurationSection)configurationPublisher(configName);
            }
            catch (ConfigNotAvailableException)
            {
                configValue = GetConfigValuesFromLocal(configName);
            }

            return configValue;
        }

        internal static IppConfigurationSection GetConfigValuesFromLocal(string configName)
        {
            return (IppConfigurationSection)System.Configuration.ConfigurationManager.GetSection("intuit/ipp");
        }
    }
    
}
