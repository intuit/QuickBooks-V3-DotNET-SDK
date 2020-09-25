using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.Exception;
//using Intuit.Ipp.LinqExtender;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using Intuit.Ipp.DataService;
using Intuit.Ipp.Security;
using System.Threading;
using Intuit.Ipp.QueryFilter;
using System.Collections.Specialized;
using System.Text;
using System.Net;
using Intuit.Ipp.GlobalTaxService.Test.Common;

namespace Intuit.Ipp.GlobalTaxService.Test
{
    [TestClass]
    public class GlobalTaxServiceTest
    {


        private TestContext testContextInstance;
        private static ServiceContext context;


        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            context = Initializer.InitializeServiceContextQbo();
            context.IppConfiguration.Logger.RequestLog.EnableRequestResponseLogging = true;
            context.IppConfiguration.Logger.RequestLog.ServiceRequestLoggingLocation = @"c:\\Logs";

        }


        [TestMethod()]
        public void AddTaxCodeSyncTest()
        {
            try
            {
                //GlobalTaxService taxSvc = new GlobalTaxService(context);
                //Intuit.Ipp.Data.TaxService  taxCodetobeAdded =  new Data.TaxService();
                //taxCodetobeAdded.TaxCode = "taxservic294";

                //List<TaxRateDetails> lstTaxRate = new List<TaxRateDetails>();
                //TaxRateDetails taxdetail1 = new TaxRateDetails();
                //taxdetail1.TaxRateName = "rat296";
                //taxdetail1.RateValue = 3m;
                //taxdetail1.RateValueSpecified = true;
                //taxdetail1.TaxAgencyId = "1";
                //taxdetail1.TaxApplicableOn = TaxRateApplicableOnEnum.Sales;
                //taxdetail1.TaxApplicableOnSpecified = true;
                //lstTaxRate.Add(taxdetail1);

                //TaxRateDetails taxdetail2 = new TaxRateDetails();
                //taxdetail2.TaxRateName = "rate297";
                //taxdetail2.RateValue = 2m;
                //taxdetail2.RateValueSpecified = true;
                //taxdetail2.TaxAgencyId = "1";
                //taxdetail2.TaxApplicableOn = TaxRateApplicableOnEnum.Sales;
                //taxdetail2.TaxApplicableOnSpecified = true;
                //lstTaxRate.Add(taxdetail2);

                //TaxRateDetails taxdetail3 = new TaxRateDetails();
                //taxdetail3.TaxRateName = "rate298";
                //taxdetail3.TaxRateId = "2";

                //lstTaxRate.Add(taxdetail3);

                //taxCodetobeAdded.TaxRateDetails = lstTaxRate.ToArray();

                //Intuit.Ipp.Data.TaxService taxCodeAdded = taxSvc.AddTaxCode(taxCodetobeAdded);
                //Assert.IsNotNull(taxCodeAdded.TaxCodeId);


                //GlobalTaxService taxSvc = new GlobalTaxService(context);
                //Intuit.Ipp.Data.TaxService taxCodetobeAdded = new Data.TaxService();
                //taxCodetobeAdded.TaxCode = "taxservic296";//change name everytime

                //List<TaxRateDetails> lstTaxRate = new List<TaxRateDetails>();
                //TaxRateDetails taxdetail1 = new TaxRateDetails();

                //taxdetail1.TaxRateId = "30";

                //lstTaxRate.Add(taxdetail1);



                //taxCodetobeAdded.TaxRateDetails = lstTaxRate.ToArray();

                //Intuit.Ipp.Data.TaxService taxCodeAdded = taxSvc.AddTaxCode(taxCodetobeAdded);



                GlobalTaxService taxSvc = new GlobalTaxService(context);
                TaxService taxCodetobeAdded = new TaxService();
                taxCodetobeAdded.TaxCode = "taxC_" + Guid.NewGuid().ToString("N");

                QueryService<TaxAgency> taxagency = new QueryService<TaxAgency>(context);
                TaxAgency taxagencyResult = taxagency.ExecuteIdsQuery("select * from TaxAgency").FirstOrDefault<TaxAgency>();



                List<TaxRateDetails> lstTaxRate = new List<TaxRateDetails>();
                TaxRateDetails taxdetail1 = new TaxRateDetails();
                taxdetail1.TaxRateName = "taxR1_" + Guid.NewGuid().ToString("N");
                taxdetail1.RateValue = 3m;
                taxdetail1.RateValueSpecified = true;
                taxdetail1.TaxAgencyId = taxagencyResult.Id.ToString();
                taxdetail1.TaxApplicableOn = TaxRateApplicableOnEnum.Sales;
                taxdetail1.TaxApplicableOnSpecified = true;
                lstTaxRate.Add(taxdetail1);

                TaxRateDetails taxdetail2 = new TaxRateDetails();
                taxdetail2.TaxRateName = "taxR2_" + Guid.NewGuid().ToString("N");
                taxdetail2.RateValue = 2m;
                taxdetail2.RateValueSpecified = true;
                taxdetail2.TaxAgencyId = taxagencyResult.Id.ToString();
                taxdetail2.TaxApplicableOn = TaxRateApplicableOnEnum.Sales;
                taxdetail2.TaxApplicableOnSpecified = true;
                lstTaxRate.Add(taxdetail2);

                //TaxRateDetails taxdetail3 = new TaxRateDetails();
                //taxdetail3.TaxRateName = "rate298";
                //taxdetail3.TaxRateId = "2";

                //lstTaxRate.Add(taxdetail3);

                taxCodetobeAdded.TaxRateDetails = lstTaxRate.ToArray();

                TaxService taxCodeAdded = taxSvc.AddTaxCode(taxCodetobeAdded);
                Assert.IsNotNull(taxCodeAdded.TaxCodeId);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void AddTaxCodeAsyncTest()
        {
            //GlobalTaxService taxSvc = new GlobalTaxService(context);
            //Intuit.Ipp.Data.TaxService taxCodetobeAdded = new Data.TaxService();

            //taxCodetobeAdded.TaxCode = "taxservicetax428";

            //List<TaxRateDetails> lstTaxRate = new List<TaxRateDetails>();
            //TaxRateDetails taxdetail1 = new TaxRateDetails();
            //taxdetail1.TaxRateName = "rate2419";
            //taxdetail1.RateValue = 3m;
            //taxdetail1.RateValueSpecified = true;
            //taxdetail1.TaxAgencyId = "1";
            //taxdetail1.TaxApplicableOn = TaxRateApplicableOnEnum.Sales;
            //taxdetail1.TaxApplicableOnSpecified = true;
            //lstTaxRate.Add(taxdetail1);

            //TaxRateDetails taxdetail2 = new TaxRateDetails();
            //taxdetail2.TaxRateName = "rate2429";
            //taxdetail2.RateValue = 2m;
            //taxdetail2.RateValueSpecified = true;
            //taxdetail2.TaxAgencyId = "1";
            //taxdetail2.TaxApplicableOn = TaxRateApplicableOnEnum.Sales;
            //taxdetail2.TaxApplicableOnSpecified = true;
            //lstTaxRate.Add(taxdetail2);

            ////TaxRateDetails taxdetail3 = new TaxRateDetails();
            ////taxdetail3.TaxRateName = "rate3";
            ////taxdetail3.TaxRateId = "2";

            ////lstTaxRate.Add(taxdetail3);

            //taxCodetobeAdded.TaxRateDetails = lstTaxRate.ToArray();


            GlobalTaxService taxSvc = new GlobalTaxService(context);
            TaxService taxCodetobeAdded = new TaxService();
            taxCodetobeAdded.TaxCode = "taxC_" + Guid.NewGuid().ToString("N");

            QueryService<TaxAgency> taxagency = new QueryService<TaxAgency>(context);
            TaxAgency taxagencyResult = taxagency.ExecuteIdsQuery("select * from TaxAgency").FirstOrDefault<TaxAgency>();



            List<TaxRateDetails> lstTaxRate = new List<TaxRateDetails>();
            TaxRateDetails taxdetail1 = new TaxRateDetails();
            taxdetail1.TaxRateName = "taxR1_" + Guid.NewGuid().ToString("N");
            taxdetail1.RateValue = 3m;
            taxdetail1.RateValueSpecified = true;
            taxdetail1.TaxAgencyId = taxagencyResult.Id.ToString();
            taxdetail1.TaxApplicableOn = TaxRateApplicableOnEnum.Sales;
            taxdetail1.TaxApplicableOnSpecified = true;
            lstTaxRate.Add(taxdetail1);

            TaxRateDetails taxdetail2 = new TaxRateDetails();
            taxdetail2.TaxRateName = "taxR2_" + Guid.NewGuid().ToString("N");
            taxdetail2.RateValue = 2m;
            taxdetail2.RateValueSpecified = true;
            taxdetail2.TaxAgencyId = taxagencyResult.Id.ToString();
            taxdetail2.TaxApplicableOn = TaxRateApplicableOnEnum.Sales;
            taxdetail2.TaxApplicableOnSpecified = true;
            lstTaxRate.Add(taxdetail2);

            //TaxRateDetails taxdetail3 = new TaxRateDetails();
            //taxdetail3.TaxRateName = "rate298";
            //taxdetail3.TaxRateId = "2";

            //lstTaxRate.Add(taxdetail3);

            taxCodetobeAdded.TaxRateDetails = lstTaxRate.ToArray();



            try
            {
                ManualResetEvent manualEvent = new ManualResetEvent(false);
                taxSvc.OnAddTaxCodeAsyncCompleted = (sender, e) =>
                {
                    Assert.IsNotNull(e);
                    TaxService addedTaxCode = e.TaxService;
                    Assert.IsTrue(!string.IsNullOrEmpty(addedTaxCode.TaxCodeId));
                    manualEvent.Set();
                };
                taxSvc.AddTaxCodeAsync(taxCodetobeAdded);
                manualEvent.WaitOne(30000);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }

        }
    }
}
