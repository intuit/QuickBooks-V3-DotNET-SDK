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
using Intuit.Ipp.QueryFilter;

using Intuit.Ipp.DataService;
using System.Collections.ObjectModel;
using Intuit.Ipp.ReportService;

namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass]
    public class ReportsTest
    {
        ServiceContext qboContextoAuth = null;
        [TestInitialize]
        public void MyTestInitializer()
        {
            qboContextoAuth = Initializer.InitializeQBOServiceContextUsingoAuth();
            //qboContextoAuth.IppConfiguration.Logger.RequestLog.EnableRequestResponseLogging = true;
            //qboContextoAuth.IppConfiguration.Logger.RequestLog.ServiceRequestLoggingLocation = @"C:\IdsLogs";
            qboContextoAuth.IppConfiguration.Message.Response.SerializationFormat = Intuit.Ipp.Core.Configuration.SerializationFormat.Json;
        }

        #region " Sync Methods "

        [TestMethod]
        public void ReportsBalanceSheetTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            reportsService.qzurl = "true"; //should return href tag
            reportsService.subcol_py_chg = "true";
            reportsService.accounting_method = "Accrual";
            Report report = reportsService.ExecuteReport("BalanceSheet");
            Assert.IsNotNull(report);
            Assert.AreEqual("BalanceSheet", report.Header.ReportName);
            Assert.AreEqual(report.Header.ReportBasis.ToString(), reportsService.accounting_method);
            Assert.AreEqual(report.Header.StartPeriod, reportsService.start_date);
        }


        [TestMethod]
        public void ReportsProfitAndLossTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.date_macro = "This Fiscal Year-to-date";
            reportsService.accounting_method = "Accrual";
            reportsService.qzurl = "true";
            reportsService.subcol_py_chg = "true";
            Report report = reportsService.ExecuteReport("ProfitAndLoss");
            Assert.IsNotNull(report);
            Assert.AreEqual("ProfitAndLoss", report.Header.ReportName);
            Assert.AreEqual(report.Header.ReportBasis.ToString(), reportsService.accounting_method);

        }

        [TestMethod]
        public void ReportsCashFlowTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            reportsService.qzurl = "true";
            Report report = reportsService.ExecuteReport("CashFlow");
            Assert.IsNotNull(report);
            Assert.AreEqual("CashFlow", report.Header.ReportName);
            Assert.AreEqual(report.Header.StartPeriod, reportsService.start_date);

        }

        [TestMethod]
        public void ReportsAPAgingSummaryTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            reportsService.qzurl = "true";
            Report report = reportsService.ExecuteReport("AgedPayables");
            Assert.IsNotNull(report);
            Assert.AreEqual("AgedPayables", report.Header.ReportName);

        }



        [TestMethod]
        public void ReportsARAgingSummaryTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date ="2014-12-31";
            reportsService.qzurl = "true";
            Report report = reportsService.ExecuteReport("AgedReceivables");
            Assert.IsNotNull(report);
            Assert.AreEqual("AgedReceivables", report.Header.ReportName);

        }

        [TestMethod]
        public void ReportsCustomerIncomeTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            reportsService.accounting_method = "Accrual";
            reportsService.subcol_py_chg = "true";
            Report report = reportsService.ExecuteReport("CustomerIncome");
            Assert.IsNotNull(report);
            Assert.AreEqual("CustomerIncome", report.Header.ReportName);
            Assert.AreEqual(report.Header.ReportBasis.ToString(), reportsService.accounting_method);
            Assert.AreEqual(report.Header.StartPeriod, reportsService.start_date);

        }

        [TestMethod]
        public void ReportsCustomerBalanceTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            Report report = reportsService.ExecuteReport("CustomerBalance");
            Assert.IsNotNull(report);
            Assert.AreEqual("CustomerBalance", report.Header.ReportName);

        }

        [TestMethod]
        public void ReportsCustomerSalesTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            reportsService.accounting_method = "Accrual";
            reportsService.subcol_py_chg = "true";
            Report report = reportsService.ExecuteReport("CustomerSales");
            Assert.IsNotNull(report);
            Assert.AreEqual("CustomerSales", report.Header.ReportName);
            Assert.AreEqual(report.Header.ReportBasis.ToString(), reportsService.accounting_method);
            Assert.AreEqual(report.Header.StartPeriod, reportsService.start_date);

        }

        [TestMethod]
        public void ReportsItemSalesTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            reportsService.accounting_method = "Accrual";
            Report report = reportsService.ExecuteReport("ItemSales");
            Assert.IsNotNull(report);
            Assert.AreEqual("ItemSales", report.Header.ReportName);
            Assert.AreEqual(report.Header.ReportBasis.ToString(), reportsService.accounting_method);
            Assert.AreEqual(report.Header.StartPeriod, reportsService.start_date);

        }

        [TestMethod]
        public void ReportsDepartmentSalesTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            reportsService.accounting_method = "Accrual";
            reportsService.subcol_py_chg = "true";
            Report report = reportsService.ExecuteReport("DepartmentSales");
            Assert.IsNotNull(report);
            Assert.AreEqual("DepartmentSales", report.Header.ReportName);
            Assert.AreEqual(report.Header.ReportBasis.ToString(), reportsService.accounting_method);
            Assert.AreEqual(report.Header.StartPeriod, reportsService.start_date);

        }

        [TestMethod] [Ignore]
        public void ReportsBASTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            reportsService.accounting_method = "cashbasis";
            Report report = reportsService.ExecuteReport("BAS");
            Assert.IsNotNull(report);
            Assert.AreEqual("BAS", report.Header.ReportName);
            Assert.AreEqual(report.Header.ReportBasis.ToString(), reportsService.accounting_method);
            Assert.AreEqual(report.Header.StartPeriod, reportsService.start_date);

        }

        [TestMethod]
        [Ignore]//QBO-47289 
        public void ReportsInventoryTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.report_date = "2014-12-31";
            Report report = reportsService.ExecuteReport("InventoryValuationSummary");
            Assert.IsNotNull(report);
            Assert.AreEqual("InventoryValuationSummary", report.Header.ReportName);
        }

        [TestMethod]
        public void ReportsSalesByProductTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2013-01-01";
            reportsService.end_date = "2014-12-31";
            Report report = reportsService.ExecuteReport("ItemSales");
            Assert.IsNotNull(report);
            Assert.AreEqual("ItemSales", report.Header.ReportName);
        }

        [TestMethod]
        public void ReportsVendorBalanceTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth); ;
            reportsService.start_date = "2013-01-01";
            reportsService.end_date = "2014-12-31";
            Report report = reportsService.ExecuteReport("VendorBalance");
            Assert.IsNotNull(report);
            Assert.AreEqual("VendorBalance", report.Header.ReportName);
        }

        [TestMethod]
        public void ReportsVendorExpenseTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            reportsService.accounting_method = "Accrual";
            reportsService.subcol_py_chg = "true";
            Report report = reportsService.ExecuteReport("VendorExpenses");
            Assert.IsNotNull(report);
            Assert.AreEqual("VendorExpenses", report.Header.ReportName);
            Assert.AreEqual(report.Header.ReportBasis.ToString(), reportsService.accounting_method);
            Assert.AreEqual(report.Header.StartPeriod, reportsService.start_date);

        }

        [TestMethod]
        public void ReportsTrialBalanceTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            Report report = reportsService.ExecuteReport("TrialBalance");
            Assert.IsNotNull(report);
            Assert.AreEqual("TrialBalance", report.Header.ReportName);
            Assert.AreEqual(report.Header.StartPeriod, reportsService.start_date);

        }

        [TestMethod]
        public void ReportsClassSalesTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            reportsService.subcol_py_chg = "true";
            reportsService.accounting_method = "Accrual";
            Report report = reportsService.ExecuteReport("ClassSales");
            Assert.IsNotNull(report);
            Assert.AreEqual("ClassSales", report.Header.ReportName);
            Assert.AreEqual(report.Header.ReportBasis.ToString(), reportsService.accounting_method);
            Assert.AreEqual(report.Header.StartPeriod, reportsService.start_date);

        }

        //new ones

        [TestMethod]
        public void ReportsAPAgingDetailTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";            
            Report report = reportsService.ExecuteReport("AgedPayableDetail");
            Assert.IsNotNull(report);
            Assert.AreEqual("AgedPayableDetail", report.Header.ReportName);

        }

        [TestMethod]
        public void ReportsARAgingDetailTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            //reportsService.columns = "tx_date";
            Report report = reportsService.ExecuteReport("AgedReceivableDetail");
            Assert.IsNotNull(report);
            Assert.AreEqual("AgedReceivableDetail", report.Header.ReportName);

        }

        [TestMethod]
        public void ReportsCustomerBalanceDetailTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            Report report = reportsService.ExecuteReport("CustomerBalanceDetail");
            Assert.IsNotNull(report);
            Assert.AreEqual("CustomerBalanceDetail", report.Header.ReportName);

        }

        [TestMethod]
        public void ReportsProfitAndLossDetailTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            Report report = reportsService.ExecuteReport("ProfitAndLossDetail");
            Assert.IsNotNull(report);
            Assert.AreEqual("ProfitAndLossDetail", report.Header.ReportName);

        }

        [TestMethod]
        public void ReportsVendorBalanceDetailTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth); ;
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            reportsService.qzurl = "true";
            Report report = reportsService.ExecuteReport("VendorBalanceDetail");
            Assert.IsNotNull(report);
            Assert.AreEqual("VendorBalanceDetail", report.Header.ReportName);
        }

        [TestMethod]
        public void ReportsGeneralLedgerDetailTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth); ;
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            Report report = reportsService.ExecuteReport("GeneralLedger");
            Assert.IsNotNull(report);
            Assert.AreEqual("GeneralLedger", report.Header.ReportName);
        }


      

        [TestMethod]
        public void ReportsAccountListTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth); ;
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            reportsService.qzurl = "true";//not yet supported
            Report report = reportsService.ExecuteReport("AccountList");
            Assert.IsNotNull(report);
            Assert.AreEqual("AccountList", report.Header.ReportName);
        }

        [TestMethod]
        public void ReportsTransactionListTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth); ;
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            reportsService.qzurl = "true";//not yet supported
            Report report = reportsService.ExecuteReport("TransactionList");
            Assert.IsNotNull(report);
            Assert.AreEqual("TransactionList", report.Header.ReportName);
        }



        [TestMethod]
        public void ReportsPrintTestUsingoAuth()
        {
            ReportService.ReportService reportsService = new ReportService.ReportService(qboContextoAuth);
            reportsService.start_date = "2014-01-01";
            reportsService.end_date = "2014-12-31";
            reportsService.accounting_method = "Accrual";
            Report report = reportsService.ExecuteReport("BalanceSheet");
            StringBuilder reportText = new StringBuilder();
            //Append Report Header
            printHeader(reportText, report);
            //Determine Maxmimum Text Lengths to format Report
            int[] maximumColumnTextSize = getMaximumColumnTextSize(report);
            //Append Column Headers
            printColumnData(reportText, report.Columns, maximumColumnTextSize, 0);
            //Append Rows
            printRows(reportText, report.Rows, maximumColumnTextSize, 1);
            //Print Formatted Report to Console
            Console.Write(reportText.ToString());
        }

        #endregion

        #region "Async Methods "

        [TestMethod]
        public void ReportsBalanceSheetAsyncTestUsingoAuth()
        {
            Report report = Helper.GetReportAsync(qboContextoAuth, "BalanceSheet");
            Assert.AreEqual("BalanceSheet", report.Header.ReportName);
            Assert.IsNotNull(report.Header.ReportBasis);
            Assert.IsNotNull(report.Header.StartPeriod);
        }


        #endregion

        #region " Helper Methods "

        #region " Determine Maximum Column Text Length "

        static int[] getMaximumColumnTextSize(Report report)
        {
            if (report.Columns == null) { return null; }
            int[] maximumColumnSize = new int[report.Columns.Count()];
            for (int columnIndex = 0; columnIndex < report.Columns.Count(); columnIndex++)
            {
                maximumColumnSize[columnIndex] = Math.Max(maximumColumnSize[columnIndex], report.Columns[columnIndex].ColTitle.Length);
            }
            return getMaximumRowColumnTextSize(report.Rows, maximumColumnSize, 1);
        }

        static int[] getMaximumRowColumnTextSize(Row[] rows, int[] maximumColumnSize, int level)
        {
            for (int rowIndex = 0; rowIndex < rows.Length; rowIndex++)
            {
                Row row = rows[rowIndex];
                Header rowHeader = getRowProperty<Header>(row, ItemsChoiceType1.Header);
                if (rowHeader != null) { getMaximumColDataTextSize(rowHeader.ColData, maximumColumnSize, level); }
                ColData[] colData = getRowProperty<ColData[]>(row, ItemsChoiceType1.ColData);
                if (colData != null) { getMaximumColDataTextSize(colData, maximumColumnSize, level); }
                Rows nestedRows = getRowProperty<Rows>(row, ItemsChoiceType1.Rows);
                if (nestedRows != null) { getMaximumRowColumnTextSize(nestedRows.Row, maximumColumnSize, level + 1); }
                Summary rowSummary = getRowProperty<Summary>(row, ItemsChoiceType1.Summary);
                if (rowSummary != null) { getMaximumColDataTextSize(rowSummary.ColData, maximumColumnSize, level); }
            }
            return maximumColumnSize;
        }

        static int[] getMaximumColDataTextSize(ColData[] colData, int[] maximumColumnSize, int level)
        {
            for (int colDataIndex = 0; colDataIndex < colData.Length; colDataIndex++)
            {
                maximumColumnSize[colDataIndex] = Math.Max(maximumColumnSize[colDataIndex], (new String(' ', level * 3) + colData[colDataIndex].value).Length);
            }
            return maximumColumnSize;
        }

        #endregion

        #region " Print Report Sections "

        static void printHeader(StringBuilder reportText, Report report)
        {
            const string lineDelimiter = "-----------------------------------------------------";
            reportText.AppendLine(report.Header.ReportName);
            reportText.AppendLine(lineDelimiter);
            reportText.AppendLine("As of " + report.Header.StartPeriod);
            reportText.AppendLine(lineDelimiter);
            reportText.AppendLine(lineDelimiter);
        }

        static void printRows(StringBuilder reportText, Row[] rows, int[] maxColumnSize, int level)
        {
            for (int rowIndex = 0; rowIndex < rows.Length; rowIndex++)
            {
                Row row = rows[rowIndex];

                //Get Row Header
                Header rowHeader = getRowProperty<Header>(row, ItemsChoiceType1.Header);

                //Append Row Header
                if (rowHeader != null && rowHeader.ColData != null) { printColData(reportText, rowHeader.ColData, maxColumnSize, level); }

                //Get Row ColData
                ColData[] colData = getRowProperty<ColData[]>(row, ItemsChoiceType1.ColData);

                //Append ColData
                if (colData != null) { printColData(reportText, colData, maxColumnSize, level); }

                //Get Child Rows
                Rows childRows = getRowProperty<Rows>(row, ItemsChoiceType1.Rows);

                //Append Child Rows
                if (childRows != null) { printRows(reportText, childRows.Row, maxColumnSize, level + 1); }

                //Get Row Summary
                Summary rowSummary = getRowProperty<Summary>(row, ItemsChoiceType1.Summary);

                //Append Row Summary
                if (rowSummary != null && rowSummary.ColData != null) { printColData(reportText, rowSummary.ColData, maxColumnSize, level); }
            }
        }

        static void printColData(StringBuilder reportText, ColData[] colData, int[] maxColumnSize, int level)
        {
            for (int colDataIndex = 0; colDataIndex < colData.Length; colDataIndex++)
            {
                if (colDataIndex > 0) { reportText.Append("     "); }
                StringBuilder rowText = new StringBuilder();
                if (colDataIndex == 0) { rowText.Append(new String(' ', level * 3)); };
                rowText.Append(colData[colDataIndex].value);
                if (rowText.Length < maxColumnSize[colDataIndex])
                {
                    rowText.Append(new String(' ', maxColumnSize[colDataIndex] - rowText.Length));
                }
                reportText.Append(rowText.ToString());
            }
            reportText.AppendLine();
        }

        static void printColumnData(StringBuilder reportText, Column[] columns, int[] maxColumnSize, int level)
        {
            for (int colDataIndex = 0; colDataIndex < columns.Length; colDataIndex++)
            {
                if (colDataIndex > 0) { reportText.Append("     "); }
                StringBuilder rowText = new StringBuilder();
                if (colDataIndex == 0) { rowText.Append(new String(' ', level * 3)); };
                rowText.Append(columns[colDataIndex].ColTitle);
                if (rowText.Length < maxColumnSize[colDataIndex])
                {
                    rowText.Append(new String(' ', maxColumnSize[colDataIndex] - rowText.Length));
                }
                reportText.Append(rowText.ToString());
            }
            reportText.AppendLine();
        }

        #endregion

        #region " Get Row Property Helper Methods - Header, ColData, Rows (children), Summary "

        static T getRowProperty<T>(Row row, ItemsChoiceType1 itemsChoiceType)
        {
            int choiceElementIndex = getChoiceElementIndex(row, itemsChoiceType);
            if (choiceElementIndex == -1) { return default(T); } else { return (T)row.AnyIntuitObjects[choiceElementIndex]; }
        }

        static int getChoiceElementIndex(Row row, ItemsChoiceType1 itemsChoiceType)
        {
            if (row.ItemsElementName != null)
            {
                for (int itemsChoiceTypeIndex = 0; itemsChoiceTypeIndex < row.ItemsElementName.Count(); itemsChoiceTypeIndex++)
                {
                    if (row.ItemsElementName[itemsChoiceTypeIndex] == itemsChoiceType) { return itemsChoiceTypeIndex; }
                }
            }
            return -1;
        }

        #endregion

        #endregion
    }
}
