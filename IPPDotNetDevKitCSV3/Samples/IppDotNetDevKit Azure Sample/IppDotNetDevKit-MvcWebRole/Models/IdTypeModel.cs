
using Intuit.Ipp.Data;
namespace IppDotNetDevKit_MvcWebRole.Models
{
    public class IdTypeModel : OperationStatus
    {
        public bool Active { get; set; }
        public string Status { get; set; }
        public string IdValue { get; set; }
        public string DisplayName { get; set; }
        public string SyncToken { get; set; }
    }
}