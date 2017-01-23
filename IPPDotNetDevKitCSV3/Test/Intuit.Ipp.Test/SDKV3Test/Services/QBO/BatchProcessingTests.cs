using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.LinqExtender;
using System.Configuration;

namespace Intuit.Ipp.Test.QBO
{
    [TestClass]
    public class BatchProcessingTests
    {

        [TestMethod]
        public void BatchIncludeTest()
        {

            ServiceContext context = Initializer.InitializeQBOServiceContextUsingoAuth();
            DataService.DataService service = new DataService.DataService(context);

            DataService.Batch batch = service.CreateNewBatch();
            List<String> optionsData = new List<string>();
            optionsData.Add("firsttxndate");

            batch.Add("Select * From CompanyInfo", "QueryCo", optionsData);
            batch.Execute();

            bool receivedIncludeParameter = false;
            IntuitBatchResponse queryCompanyResponse = batch["QueryCo"];
            if (queryCompanyResponse.ResponseType == ResponseType.Query)
            {
                CompanyInfo companyInfo = queryCompanyResponse.Entities[0] as CompanyInfo;
                foreach (NameValue nameValue in companyInfo.NameValue)
                {
                    receivedIncludeParameter = nameValue.Name == "firsttxndate";
                    if (receivedIncludeParameter) { break; }
                }

            }
            if (!receivedIncludeParameter) { Assert.Fail("CompanyInfo not returned"); }
        }

        [TestMethod]
        public void BatchInvoiceTest()
        {
            ServiceContext context = Initializer.InitializeQBOServiceContextUsingoAuth();
            DataService.DataService service = new DataService.DataService(context);
            List<Invoice> addedInvoiceList = new List<Invoice>();
            List<Invoice> newInvoiceList = new List<Invoice>();
            for (int i = 0; i < 5; i++)
            {
                Invoice invoice = QBOHelper.CreateInvoice(context);
                addedInvoiceList.Add(service.Add<Invoice>(invoice));
            }

            for (int i = 0; i < 5; i++)
            {
                newInvoiceList.Add(QBOHelper.CreateInvoice(context));
            }

            QueryService<Invoice> invoiceContext = new QueryService<Invoice>(context);

            DataService.Batch batch = service.CreateNewBatch();

            int count = 1;
            foreach (Invoice invoice in newInvoiceList)
            {
                batch.Add(invoice, "AddInvoice" + count, OperationEnum.create);
                count++;
            }

            count = 0;

            List<string> docNumbers = new List<string>();
            foreach (Invoice invoice in addedInvoiceList)
            {
                invoice.DocNumber = "SUDoc" + Guid.NewGuid().ToString().Substring(0, 6);
                docNumbers.Add(invoice.DocNumber);
                invoice.sparse = true;
                invoice.sparseSpecified = true;
                batch.Add(invoice, "UpdateInvoice" + count, OperationEnum.update);
                count++;
            }

            string[] values = docNumbers.ToArray();
            batch.Add(invoiceContext.Where(c => c.DocNumber.In(values)).ToIdsQuery(), "QueryInvoice1");
            batch.Execute();

            int position = 0;
            foreach (IntuitBatchResponse resp in batch.IntuitBatchItemResponses)
            {
                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (position <= 4)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Invoice).Id));
                }

                if (position > 4 && position < 10)
                {
                    Assert.IsTrue(((Invoice)resp.Entity).DocNumber.Contains("SUDoc"));
                }

                if (position == 10)
                {
                    Assert.IsTrue(resp.Entities.Count == 5);
                }

                position++;
            }
        }

        [TestMethod]
        public void BatchEntityTest()
        {
            ServiceContext context = Initializer.InitializeQBOServiceContextUsingoAuth();
            DataService.DataService service = new DataService.DataService(context);

            Customer customer = CreateCustomer();
            Customer addedCustomer = service.Add(customer);

            Invoice invoice = QBOHelper.CreateInvoice(context);

            QueryService<Term> termContext = new QueryService<Term>(context);

            QueryService<TaxRate> taxRateContext = new QueryService<TaxRate>(context);
            QueryService<TaxCode> taxCodeContext = new QueryService<TaxCode>(context);
            QueryService<Item> itemContext = new QueryService<Item>(context);

            DataService.Batch batch = service.CreateNewBatch();
            batch.Add(addedCustomer, "UpdateCustomer", OperationEnum.update);
            batch.Add(invoice, "AddInvoice", OperationEnum.create);
            batch.Add(termContext.Take(5).ToIdsQuery(), "QueryTerm");
            batch.Add(taxRateContext.Take(5).ToIdsQuery(), "QueryTaxRate");
            batch.Add(taxCodeContext.Take(5).ToIdsQuery(), "QueryTaxCode");
            batch.Add(itemContext.Take(5).ToIdsQuery(), "QueryItem");

            batch.Execute();
            foreach (IntuitBatchResponse resp in batch.IntuitBatchItemResponses)
            {
                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }
            }
        }


        #region Helper methods

        private Customer CreateCustomer()
        {
            Customer newCustomer = new Customer();
            string guid = Guid.NewGuid().ToString("N");

            //Mandatory Fields

            newCustomer.DisplayName = "Disp" + guid.Substring(0, 25);
            newCustomer.Title = "Title" + guid.Substring(0, 7);
            newCustomer.DisplayName = "DN" + guid.Substring(0, 20);

            newCustomer.MiddleName = "M" + guid.Substring(0, 4);
            newCustomer.FamilyName = "FN" + guid.Substring(0, 20);

            return newCustomer;
        }


        //internal static Invoice CreateInvoice(ServiceContext context)
        //{
        //    List<Customer> customers = FindAllHelper<Customer>(context, new Customer(), 1, 10);
        //    Assert.IsTrue(customers.Count > 0);
        //    List<TaxCode> taxCodes = FindAllHelper<TaxCode>(context, new TaxCode(), 1, 10);


        //    Assert.IsTrue(taxCodes.Count > 0);
        //    Invoice newInvoice = new Invoice();
        //    newInvoice.DocNumber = Guid.NewGuid().ToString("N").Substring(0, 10);
        //    newInvoice.TxnDate = DateTime.Today.Date;
        //    newInvoice.TxnDateSpecified = true;
        //    newInvoice.CustomerRef = new ReferenceType() { type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Customer), name = customers[0].GivenName, Value = customers[0].Id/*, typeSpecified=true */ };
        //    newInvoice.ARAccountRef = new ReferenceType() { type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Account), name = "Account Receivable", Value = "QB:37"/*, typeSpecified=true */ };
        //    newInvoice.TxnTaxDetail = new TxnTaxDetail() { TotalTax = 0, DefaultTaxCodeRef = new ReferenceType() { Value = taxCodes[0].Id, type = Enum.GetName(typeof(objectNameEnumType), objectNameEnumType.Customer),/* typeSpecified=true,*/ name = taxCodes[0].Name } };
        //    Line invLine = new Line();

        //    invLine.Amount = 10000;
        //    invLine.DetailType = LineDetailTypeEnum.DescriptionOnly;
        //    invLine.AmountSpecified = true;
        //    invLine.Description = "Desc Invoice";
        //    newInvoice.Line = new Line[] { invLine };
        //    return newInvoice;
        //}
        #endregion
    }
}
