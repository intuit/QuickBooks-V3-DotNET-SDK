using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IDSLinq
{
    public class ReportProfitAndLoss
    {
        private offeringId offeringId;

        public offeringId OfferingId
        {
            get { return offeringId; }
            set { offeringId = value; }
        }
        private string chunkSize;

        public string ChunkSize
        {
            get { return chunkSize; }
            set { chunkSize = value; }
        }
        private ReportBasisEnum reportBasis;

        public ReportBasisEnum ReportBasis
        {
            get { return reportBasis; }
            set { reportBasis = value; }
        }

    }

    public enum offeringId
    {

        /// <remarks/>
        ipp,

        /// <remarks/>
        cmo,

        /// <remarks/>
        esbsync,
    }

    public enum ReportBasisEnum
    {

        /// <remarks/>
        Cash,

        /// <remarks/>
        Accrual,
    }
}
