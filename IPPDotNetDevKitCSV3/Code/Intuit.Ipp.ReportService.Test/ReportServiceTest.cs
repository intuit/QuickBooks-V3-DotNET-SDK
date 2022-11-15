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

namespace Intuit.Ipp.ReportService.Test.Common
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
            ServiceContext context = Initializer.InitializeServiceContextQbo();
            context.IppConfiguration.Message.Response.SerializationFormat = Intuit.Ipp.Core.Configuration.SerializationFormat.Json;
            reportServiceTestCases = new ReportService(context);

        }

        /// <summary>
        /// BL report test
        /// </summary>
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

        /// <summary>
        /// Fec report test
        /// </summary>
        [TestMethod()]
        public void ExecuteGetFecReportTest()
        {
            try
            {
                reportServiceTestCases.start_date = "2014-01-01";
                reportServiceTestCases.end_date = "2014-12-31";
                reportServiceTestCases.accounting_method = "Accrual";
                reportServiceTestCases.attachmentType = "TEMPORARY_LINKS";
                reportServiceTestCases.add_due_date = "true";
                Report report = reportServiceTestCases.ExecuteReport("FECReport");
                Assert.IsNotNull(report);
                Assert.AreEqual("FECReport", report.Header.ReportName);
                Assert.AreEqual(report.Header.ReportBasis.ToString(), reportServiceTestCases.accounting_method);
                Assert.AreEqual(report.Header.StartPeriod, reportServiceTestCases.start_date);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }

        }

        /// <summary>
        /// Profit And Loss report test
        /// </summary>
        [TestMethod()]
        public void ExecuteProfitLossReportTest()
        {
            try
            {
                reportServiceTestCases.accounting_method = "Accrual";
                reportServiceTestCases.date_macro = "Last Fiscal Year";
                Report report = reportServiceTestCases.ExecuteReport("ProfitAndLoss");
                Assert.IsNotNull(report);
                Assert.AreEqual("ProfitAndLoss", report.Header.ReportName);
                Assert.AreEqual(report.Header.ReportBasis.ToString(), reportServiceTestCases.accounting_method);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }

        }

        /// <summary>
        /// Journal report test
        /// </summary>
        [TestMethod()]
        public void ExecuteJournalReportTest()
        {
            try
            {
                reportServiceTestCases.date_macro = "Last Fiscal Year";
                Report report = reportServiceTestCases.ExecuteReport("JournalReport");
                Assert.IsNotNull(report);
                Assert.AreEqual("JournalReport", report.Header.ReportName);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }

        }

        /// <summary>
        /// Transaction Detail By Account test
        /// </summary>
        [TestMethod()]
        public void ExecuteTransactionDetailByAccountReportTest()
        {
            try
            {
                reportServiceTestCases.columns = "post";
                reportServiceTestCases.transaction_type = "post";
                reportServiceTestCases.accounting_method = "Cash";
                reportServiceTestCases.sort_by = "create_date";
                reportServiceTestCases.sort_order = "descend";
                reportServiceTestCases.start_date = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
                reportServiceTestCases.end_date = DateTime.Today.ToString("yyyy-MM-dd");
                Report report = reportServiceTestCases.ExecuteReport("TransactionDetailByAccount");
                Assert.IsNotNull(report);
                Assert.AreEqual("TransactionDetailByAccount", report.Header.ReportName);
                Assert.AreEqual(report.Header.ReportBasis.ToString(), reportServiceTestCases.accounting_method);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }

        }
    }

}