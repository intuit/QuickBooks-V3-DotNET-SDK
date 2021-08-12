using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Serilog;
using Serilog.Sinks.File;
using Serilog.Core;
using Serilog.Events;

using System.IO;

namespace TestSDK
{

    /// <summary>
    /// Summary description for CLogger2
    /// </summary>

        public class CLogger // : ILogger
        {
            public Logger customLogger;
            public Logger GetCustomLogger()
            {

            //Log file path for widows n ios
             string filePath = Path.Combine("C:\\Users\\nshrivastava\\Documents\\Serilog_log", "QBOApiLogs-" + DateTime.Now.Ticks.ToString() + ".txt");

        

        //Setting logger config for Serilog
        var loggerConfig = new LoggerConfiguration()
                     .MinimumLevel.Verbose()
                     .WriteTo.File(filePath);

                customLogger = loggerConfig.CreateLogger();
                return customLogger;


            }
        }
}