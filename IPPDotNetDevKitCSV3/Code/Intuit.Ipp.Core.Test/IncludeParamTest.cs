using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using Intuit.Ipp.Utility;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Exception;
using Intuit.Ipp.DataService;

/// <summary>
///This test case covers Include parameter, minorversion, requestId tests.
///</summary>

namespace Intuit.Ipp.Core.Test
{
    [TestClass]
    public class IncludeParamTest
    {
        private TestContext testContextInstance;
        ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();

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

        /// <summary>
        /// Test for Include Parameter- allowduplicatedocnum for Purchase- check
        /// </summary>
        [TestMethod]
        public void PurchaseDuplicateCheckNoTest()
        {
            try
            {
                serviceContext.IppConfiguration.MinorVersion.Qbo = "8";
                QueryService<Purchase> purchaseQueryService = new QueryService<Purchase>(serviceContext);
                Purchase purchaseExisting = purchaseQueryService.ExecuteIdsQuery("Select * From Purchase where PaymentType='Check'").FirstOrDefault<Purchase>();
                
                serviceContext.Include.Add("allowduplicatedocnum");

                Intuit.Ipp.DataService.DataService commonServiceQBO = new Intuit.Ipp.DataService.DataService(serviceContext);
                commonServiceQBO.Add<Purchase>(AddCheckPayment(purchaseExisting));

            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                serviceContext.Include.Clear();
            }




        }

        private Purchase AddCheckPayment(Purchase purchaseExisting)
        {
            QueryService<Customer> customerQueryService = new QueryService<Customer>(serviceContext);
            Customer customer = customerQueryService.ExecuteIdsQuery("Select * From Customer StartPosition 1 MaxResults 1").FirstOrDefault<Customer>();


            //Find Bank Account 
            QueryService<Account> accountQueryService = new QueryService<Account>(serviceContext);
            Account account = accountQueryService.ExecuteIdsQuery("Select * From Account Where AccountType='Bank' StartPosition 1 MaxResults 1").FirstOrDefault<Account>();
            //Expense Account
            Account expenseaccount = accountQueryService.ExecuteIdsQuery("Select * From Account Where AccountType='Expense' StartPosition 1 MaxResults 1").FirstOrDefault<Account>();

            Purchase purchase = new Purchase();

            //Assign the doc number from previously fetched check purchase
            purchase.DocNumber = purchaseExisting.DocNumber;

            purchase.AccountRef = new ReferenceType()
            {

                name = account.Name,
                Value = account.Id
            };

            purchase.PaymentType = PaymentTypeEnum.Check;
            purchase.PaymentTypeSpecified = true;

            purchase.EntityRef = new ReferenceType()
            {
                name = customer.DisplayName,
                Value = customer.Id
            };


            purchase.TotalAmt = new Decimal(1000.00);
            purchase.TotalAmtSpecified = true;



            purchase.TxnDate = DateTime.UtcNow.Date;
            purchase.TxnDateSpecified = true;


            List<Line> lineList = new List<Line>();
            Line line = new Line();
            //line.LineNum = "LineNum";
            line.Description = "Description for Line";
            line.Amount = new Decimal(1000.00);
            line.AmountSpecified = true;


            line.DetailType = LineDetailTypeEnum.AccountBasedExpenseLineDetail;
            line.DetailTypeSpecified = true;
            AccountBasedExpenseLineDetail lineDetail = new AccountBasedExpenseLineDetail();

            lineDetail.AccountRef = new ReferenceType { name = expenseaccount.Name, Value = expenseaccount.Id };
            lineDetail.BillableStatus = BillableStatusEnum.NotBillable;
            lineDetail.TaxAmount = new Decimal(10.00);
            lineDetail.TaxAmountSpecified = true;
            line.AnyIntuitObject = lineDetail;



            lineList.Add(line);
            purchase.Line = lineList.ToArray();

            return purchase;
        }


        /// <summary>
        /// Test for Include Parameter- firsttxndate for CompanyInfo
        /// </summary>
        [TestMethod]
        public void CompanyInfoFirstTxnDateTest()
        {
            serviceContext.IppConfiguration.MinorVersion.Qbo = "8";
            serviceContext.Include.Add("firsttxndate");

            try
            {
                QueryService<CompanyInfo> companyInfoQueryService = new QueryService<CompanyInfo>(serviceContext);
                CompanyInfo companyInfo = companyInfoQueryService.ExecuteIdsQuery("Select * From CompanyInfo StartPosition 1 MaxResults 1").FirstOrDefault<CompanyInfo>();

            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                serviceContext.Include.Clear();
            }
        }

    }
}
