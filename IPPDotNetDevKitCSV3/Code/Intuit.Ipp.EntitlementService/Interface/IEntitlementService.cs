

namespace Intuit.Ipp.EntitlementService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Intuit.Ipp.Data;

    public interface IEntitlementService
    {
        EntitlementsResponse GetEntitlements(string entitlementBaseUrl);
        void GetEntitlementsAsync(string entitlementBaseUrl);
    }
}
