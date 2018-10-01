

namespace Intuit.Ipp.Core    
{
    using Intuit.Ipp.Exception;

    public class EntitlementCallCompletedEventArgs<T> where T : Intuit.Ipp.Data.EntitlementsResponse
    {
        public EntitlementCallCompletedEventArgs()
        {
        }

        public T EntitlementsResponse { get; set; }

        public IdsException Error { get; set; }
    }
}
