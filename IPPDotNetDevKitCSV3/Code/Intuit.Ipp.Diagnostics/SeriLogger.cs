using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Serilog;
using Serilog.Sinks;
using Serilog.Core;
using Serilog.Events;
using System.IO;
using System.Diagnostics;
using System.Text;
using Serilog.Configuration;
using SerilogTraceListener;
using Serilog.Enrichers;
using Microsoft.Extensions.Configuration;

namespace Intuit.Ipp.Diagnostics
{
    public class SeriLogger:ILogger
    {
        //public string applog { get; set; }
        public static Logger log { get; set; }
        // <summary>
        /// Provides a multilevel switch to control tracing.
        /// </summary>
        //private TraceSwitch traceSwitch;

        public SeriLogger()
        {
            // Searches for the switch name "IPPTraceSwitch" in the config file of the client.
            // If not found then default trace switch is OFF.
            //this.traceSwitch = new TraceSwitch("IPPTraceSwitch", "IPP Trace Switch");
            //var configuration = new ConfigurationBuilder()
            //.AddJsonFile("appsettings.json")
            //.Build();

            //log = new LoggerConfiguration()
            //    .MinimumLevel.Verbose()
            //    .Enrich.FromLogContext()
            //    .ReadFrom.Configuration(configuration)
            //    .CreateLogger();


            log = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.FromLogContext()
                .WriteTo.Trace()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.LiterateConsole()
                .WriteTo.RollingFile(@"C:\Documents\Serilog_log\Log-{Date}.txt")
                //.WriteTo.AzureDocumentDB()
                //.WriteTo.AzureAnalytics
                .CreateLogger();

            var listener = new global::SerilogTraceListener.SerilogTraceListener(log);
            //listener.
            Trace.Listeners.Add(listener);

            log.Information("Logger is initialized");
            
        }
        //private static Stream AddListener(string path)
        //{
        //    string filename = path + "TraceLog-" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
        //    Stream myFile = null;
        //    if (File.Exists(filename))
        //        myFile = new FileStream(filename, FileMode.Append);
        //    else
        //        myFile = new FileStream(filename, FileMode.Create);
        //    TextWriterTraceListener myTextListener = new TextWriterTraceListener(myFile);
        //    Trace.Listeners.Add(myTextListener);
        //    Trace.AutoFlush = true;
        //    return myFile;
        //}

        
       

        /// <summary>
        /// Logs messages depending on the ids trace level.
        /// </summary>
        /// <param name="idsTraceLevel">IDS Trace Level.</param>
        /// <param name="messageToWrite">The message to write.</param>
        public void Log(TraceLevel idsTraceLevel, string messageToWrite)
        {
            log.Write(LogEventLevel.Verbose, messageToWrite);
        }


        //public static void LogSeriLogMessage()
        //{
        //    log = new LoggerConfiguration()
        //        .WriteTo.Trace()
        //        .CreateLogger();
        //    log.Write(LogEventLevel.Verbose, applog);
        //}
    }
}