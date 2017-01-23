using System;

namespace Intuit.Ipp.LinqExtender
{
  
    /// <summary>
    /// Custom extender expection class.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class ProviderException : Exception
    {
        /// <summary>
        /// Parametrized constructor for the expection.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public ProviderException(string message, Exception ex) : base(message, ex) { }
        /// <summary>
        /// Defautl constructor for the expection.
        /// </summary>
        /// <param name="message"></param>
        public ProviderException(string message) : base(message) { }      
    }
}
