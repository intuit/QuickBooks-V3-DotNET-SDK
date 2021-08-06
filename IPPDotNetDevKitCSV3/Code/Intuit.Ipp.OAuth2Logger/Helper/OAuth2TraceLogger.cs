using Intuit.Ipp.OAuth2Logger.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Intuit.Ipp.OAuth2Logger.Helper
{
    public class OAuth2TraceLogger : IOAuth2Logger
    {
        /// <summary>
        /// Provides a multilevel switch to control tracing.
        /// </summary>
        private TraceSwitch traceSwitch;

        /// <summary>
        /// Initializes a new instance of the TraceLogger class.
        /// </summary>
        public OAuth2TraceLogger()
        {
            // Searches for the switch name "IPPTraceSwitch" in the config file of the client.
            // If not found then default trace switch is OFF.
            this.traceSwitch = new TraceSwitch("IPPTraceSwitch", "IPP Trace Switch");
        }

        /// <summary>
        /// Logs messages depending on the ids trace level.
        /// </summary>
        /// <param name="idsTraceLevel">IDS Trace Level.</param>
        /// <param name="messageToWrite">The message to write.</param>
        public void Log(OAuth2TraceLevel idsTraceLevel, string messageToWrite)
        {
            if ((int)this.traceSwitch.Level < (int)idsTraceLevel)
            {
                return;
            }

            StackTrace st = new StackTrace(1, true);
            StackFrame sf = new StackFrame();
            sf = st.GetFrame(0);

            StringBuilder logMessage = new StringBuilder();
            logMessage.Append(string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0} -- ", idsTraceLevel.ToString()));
            logMessage.Append(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture) + " -- ");
            string fileName = Path.GetFileName(sf.GetFileName());
            logMessage.Append(fileName + " - " + sf.GetFileLineNumber() + " - " + sf.GetMethod() + " - " + messageToWrite);
            switch (idsTraceLevel)
            {
                case OAuth2TraceLevel.Info:
                    Trace.TraceInformation(logMessage.ToString());
                    break;
                case OAuth2TraceLevel.Verbose:
                    Trace.WriteLine(logMessage.ToString());
                    break;
                case OAuth2TraceLevel.Warning:
                    Trace.TraceWarning(logMessage.ToString());
                    break;
                case OAuth2TraceLevel.Error:
                    Trace.TraceError(logMessage.ToString());
                    break;
                case OAuth2TraceLevel.Off:
                    break;
            }
        }
    }
}
