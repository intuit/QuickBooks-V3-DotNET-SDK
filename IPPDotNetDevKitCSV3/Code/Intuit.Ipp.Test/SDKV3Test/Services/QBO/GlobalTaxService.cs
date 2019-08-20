using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.Security;
using Intuit.Ipp.Exception;
using System.Threading;
using System.Collections.ObjectModel;
using Intuit.Ipp.DataService;
using Intuit.Ipp.QueryFilter;

using System.Configuration;
using Intuit.Ipp.GlobalTaxService;


namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass] //Works only for US realms, not yet supported for global
    public class GlobalTaxServiceTest
    {
        ServiceContext qboContextoAuth = null;
        [TestInitialize]
        public void MyTestInitializer()
        {
            qboContextoAuth = Initializer.InitializeQBOServiceContextUsingoAuth();
            //qboContextoAuth.IppConfiguration.Logger.RequestLog.EnableRequestResponseLogging = true;
            //qboContextoAuth.IppConfiguration.Logger.RequestLog.ServiceRequestLoggingLocation = @"C:\IdsLogs";
        }


      

        #region Sync Methods

        #region Test cases for Add Operations

  

        [TestMethod]
        public void TaxCodeAddTestUsingoAuth()
        {
            String guid = Helper.GetGuid();
            GlobalTaxService.GlobalTaxService taxSvc = new GlobalTaxService.GlobalTaxService(qboContextoAuth);
            Intuit.Ipp.Data.TaxService taxCodetobeAdded = new Data.TaxService();
            taxCodetobeAdded.TaxCode = "taxC_"+guid;

            TaxAgency taxagency = Helper.FindOrAdd<TaxAgency>(qboContextoAuth, new TaxAgency());
           

            List<TaxRateDetails> lstTaxRate = new List<TaxRateDetails>();
            TaxRateDetails taxdetail1 = new TaxRateDetails();
            taxdetail1.TaxRateName = "taxR1_"+guid;
            taxdetail1.RateValue = 3m;
            taxdetail1.RateValueSpecified = true;
            taxdetail1.TaxAgencyId = taxagency.Id.ToString();
            taxdetail1.TaxApplicableOn = TaxRateApplicableOnEnum.Sales;
            taxdetail1.TaxApplicableOnSpecified = true;
            lstTaxRate.Add(taxdetail1);

            TaxRateDetails taxdetail2 = new TaxRateDetails();
            taxdetail2.TaxRateName = "taxR2_" + guid;
            taxdetail2.RateValue = 2m;
            taxdetail2.RateValueSpecified = true;
            taxdetail2.TaxAgencyId = taxagency.Id.ToString();
            taxdetail2.TaxApplicableOn = TaxRateApplicableOnEnum.Sales;
            taxdetail2.TaxApplicableOnSpecified = true;
            lstTaxRate.Add(taxdetail2);

            //TaxRateDetails taxdetail3 = new TaxRateDetails();
            //taxdetail3.TaxRateName = "rate298";
            //taxdetail3.TaxRateId = "2";

            //lstTaxRate.Add(taxdetail3);

            taxCodetobeAdded.TaxRateDetails = lstTaxRate.ToArray();

            Intuit.Ipp.Data.TaxService taxCodeAdded = taxSvc.AddTaxCode(taxCodetobeAdded);
            Assert.IsNotNull(taxCodeAdded.TaxCodeId);
        }

        #endregion

        #endregion

       

    }
}
