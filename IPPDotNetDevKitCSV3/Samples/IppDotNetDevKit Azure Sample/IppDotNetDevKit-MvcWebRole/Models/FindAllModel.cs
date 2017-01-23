using System.Collections.Generic;
using Intuit.Ipp.Data;

namespace IppDotNetDevKit_MvcWebRole.Models
{
    public class FindAllModel
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Invoice> Invoices { get; set; }
    }
}