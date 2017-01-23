
namespace IppDotNetDevKit_MvcWebRole
{
    public class SampleLogging : Intuit.Ipp.Diagnostics.ILogger
    {
        public static string LogMsg;

        public void Log(Intuit.Ipp.Diagnostics.TraceLevel idsTraceLevel, string messageToWrite)
        {
            LogMsg = LogMsg + messageToWrite + "\n";
        }
    }
}