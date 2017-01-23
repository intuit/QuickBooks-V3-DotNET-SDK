using System.Collections.ObjectModel;
using Intuit.Ipp.DataService;
using Intuit.Ipp.Exception;

namespace IppDotNetDevKit_MvcWebRole.Models.Qbd
{
    public class BatchModel
    {
        public bool CustomerAdd { get; set; }
        public bool CustomerFindById { get; set; }
        public bool CustomerFindAll { get; set; }
        public bool InvoiceFindById { get; set; }
        public bool InvoiceFindAll { get; set; }

        public string DisplayName_Customer { get; set; }
        public string Id_Customer { get; set; }
        public string Id_Invoice { get; set; }

        public ReadOnlyCollection<IntuitBatchResponse> IntuitBatchResponse { get; set; }
        public IdsException Exception { get; set; }
        public ResponseModel Response { get; set; }
    }

    public class RequestModel
    {
        public bool UseCompression { get; set; }
        public bool UseJson { get; set; }
    }

    public class ResponseModel : RequestModel
    {
    }
}