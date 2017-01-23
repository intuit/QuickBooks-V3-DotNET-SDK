using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IppDotNetDevKit_MvcWebRole.Models
{
    public class UpdateModel : OperationStatus
    {
        public string Id { get; set; }
        public string SyncToken { get; set; }
        public string DisplayName { get; set; }
        public string GivenName { get; set; }
        public string MiddleName { get; set; }
        public string FamilyName { get; set; }
        public string Title { get; set; }
        public bool Sparse { get; set; }
    }
}