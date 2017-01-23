using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intuit.Ipp.Utility;

namespace Intuit.Configuration.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            
            ServiceContext sc = new ServiceContext();  
            
            Console.WriteLine("Getting config from local config file.");
            Console.WriteLine(sc.SdkConfiguration.Logger.RequestSettings.RequestResponseLoggingDirectory);


            sc.SdkConfiguration.Logger.RequestSettings.RequestResponseLoggingDirectory = "Set by me";
            Console.WriteLine(sc.SdkConfiguration.Logger.RequestSettings.RequestResponseLoggingDirectory);

            Console.WriteLine(sc.SdkConfiguration.Logger.RequestSettings.RequestResponseLoggingDirectory);
            
            Console.ReadLine();
        }

        static IConfigurationSection GetConfigFromMyOwnResource(string configName)
        {
           // Call hosted service and return

            IppConfigurationSectionObj ippconfig = new IppConfigurationSectionObj();
            ippconfig.Logger.RequestSettings.RequestResponseLoggingDirectory = "From hosted service";
            return ippconfig;
        }
    }
}
