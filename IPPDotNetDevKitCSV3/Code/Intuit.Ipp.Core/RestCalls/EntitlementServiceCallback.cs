using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intuit.Ipp.Core
{
    public static class EntitlementServiceCallback<T> where T : Intuit.Ipp.Data.EntitlementsResponse
    {
        public delegate void EntitlementCallCompletedEventHandler(object sender, EntitlementCallCompletedEventArgs<T> entitlementCallCompletedEventArgs);
    }
}
