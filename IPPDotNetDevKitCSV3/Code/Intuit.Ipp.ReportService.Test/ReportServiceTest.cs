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

namespace Intuit.Ipp.ReportService.Test
{
    /// <summary>
    ///This is a test class for ReportService and is intended
    ///to contain all ReportService Unit Tests
    ///</summary>
    [TestClass()]
    public class ReportServiceTest
    {
        
        private static ReportService reportServiceTestCases;

            [ClassInitialize()]
            public static void MyClassInitialize(TestContext testContext)
            {
                string accessTokenQBO = ConfigurationManager.AppSettings["AccessTokenQBO"];
                //string accessTokenSecretQBO = ConfigurationManager.AppSettings["AccessTokenSecretQBO"];
                //string consumerKeyQBO = ConfigurationManager.AppSettings["ConsumerKeyQBO"];
                //string ConsumerSecretQBO = ConfigurationManager.AppSettings["ConsumerSecretQBO"];
                string realmIAQBO = ConfigurationManager.AppSettings["RealmIAQBO"];
                OAuth2RequestValidator oAuthRequestValidator = new OAuth2RequestValidator(accessTokenQBO);
                ServiceContext context = new ServiceContext(realmIAQBO, IntuitServicesType.QBO, oAuthRequestValidator);
                context.IppConfiguration.Message.Response.SerializationFormat = Intuit.Ipp.Core.Configuration.SerializationFormat.Json;
                reportServiceTestCases = new ReportService(context);
                
            }

            [TestMethod()]
            public void ExecuteGetBalanceSheetReportTest()
            {
                try
                {
                    reportServiceTestCases.start_date = "2014-01-01";
                    reportServiceTestCases.end_date = "2014-12-31";
                    reportServiceTestCases.accounting_method = "Accrual";
                    Report report = reportServiceTestCases.ExecuteReport("BalanceSheet");
                    Assert.IsNotNull(report);
                    Assert.AreEqual("BalanceSheet", report.Header.ReportName);
                    Assert.AreEqual(report.Header.ReportBasis.ToString(), reportServiceTestCases.accounting_method);
                    Assert.AreEqual(report.Header.StartPeriod, reportServiceTestCases.start_date);
                }
                catch (System.Exception ex)
                {
                    Assert.Fail(ex.ToString());
                }

            }

    }

}