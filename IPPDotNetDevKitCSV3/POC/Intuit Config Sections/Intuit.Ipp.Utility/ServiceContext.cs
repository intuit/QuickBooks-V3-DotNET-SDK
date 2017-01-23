using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intuit.Ipp.Utility
{
    public class ServiceContext
    {
        private IppConfigurationSectionObj sdkConfiguration;

        public ServiceContext()
        {
            if (this.SdkConfiguration == null)
            {
                sdkConfiguration = Translator(SDKConfiguration.GetConfigurationValue("intuit/ipp"));
            }
           
        }
        public IppConfigurationSectionObj SdkConfiguration
        {
            get
            {
                return sdkConfiguration;
                
            }
            set { sdkConfiguration = value; }
        }

        //public IppConfigurationSectionObj GetSdkConfiguration()
        //{   
        //    return SDKConfiguration.GetConfigurationValue("intuit/ipp");            
        //}

        public void SetConfigurationPublisher(Func<string,IppConfigurationSectionObj> configurationPublisher)
        {
            SDKConfiguration.configurationPublisher = configurationPublisher;
        }

        private static IppConfigurationSectionObj Translator(IppConfigurationSection ippConfigSection)
        {
            // use reflection to translate

            IppConfigurationSectionObj ippConfig = new IppConfigurationSectionObj();

            // converting configuration class to object model
            ippConfig.Logger.RequestSettings.RequestResponseLoggingDirectory = ippConfigSection.Logger.RequestSettings.RequestResponseLoggingDirectory;
            return ippConfig;
        }
       
    }
}
