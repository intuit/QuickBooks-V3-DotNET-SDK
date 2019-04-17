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


namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass]
    public class InvoiceTest
    {
        ServiceContext qboContextoAuth = null;
        [TestInitialize]
        public void MyTestInitializer()
        {
            qboContextoAuth = Initializer.InitializeQBOServiceContextUsingoAuth();
            //qboContextoAuth.IppConfiguration.Logger.RequestLog.EnableRequestResponseLogging = true;
            //qboContextoAuth.IppConfiguration.Logger.RequestLog.ServiceRequestLoggingLocation = @"C:\IdsLogs";
        }


        #region TestCases for QBOContextOAuth

        #region Sync Methods

        #region Test cases for Add Operations

        [TestMethod]
        public void InvoiceAddTestUsingoAuth()
        {
            //Creating the Invoice for Add
            Invoice invoice = QBOHelper.CreateInvoice(qboContextoAuth);
            //Adding the Invoice
            Invoice added = Helper.Add<Invoice>(qboContextoAuth, invoice);
            //Verify the added Invoice
            QBOHelper.VerifyInvoice(invoice, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void InvoiceFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            InvoiceAddTestUsingoAuth();

            //Retrieving the Invoice using FindAll
            List<Invoice> invoices = Helper.FindAll<Invoice>(qboContextoAuth, new Invoice(), 1, 500);
            Assert.IsNotNull(invoices);
            Assert.IsTrue(invoices.Count<Invoice>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void InvoiceFindbyIdTestUsingoAuth()
        {
            //Creating the Invoice for Adding
            Invoice invoice = QBOHelper.CreateInvoice(qboContextoAuth);
            //Adding the Invoice
            Invoice added = Helper.Add<Invoice>(qboContextoAuth, invoice);
            Invoice found = Helper.FindById<Invoice>(qboContextoAuth, added);
            QBOHelper.VerifyInvoice(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void InvoiceUpdateTestUsingoAuth()
        {
            //Creating the Invoice for Adding
            Invoice invoice = QBOHelper.CreateInvoice(qboContextoAuth);
            //Adding the Invoice
            Invoice added = Helper.Add<Invoice>(qboContextoAuth, invoice);
            //Change the data of added entity
            Invoice changed = QBOHelper.UpdateInvoice(qboContextoAuth, added);
            //Update the returned entity data
            Invoice updated = Helper.Update<Invoice>(qboContextoAuth, changed);//Verify the updated Invoice
            QBOHelper.VerifyInvoice(changed, updated);
        }


        [TestMethod]
        public void InvoiceSparseUpdateTestUsingoAuth()
        {
            //Creating the Invoice for Adding
            Invoice invoice = QBOHelper.CreateInvoice(qboContextoAuth);
            //Adding the Invoice
            Invoice added = Helper.Add<Invoice>(qboContextoAuth, invoice);
            //Change the data of added entity
            Invoice changed = QBOHelper.SparseUpdateInvoice(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Invoice updated = Helper.Update<Invoice>(qboContextoAuth, changed);//Verify the updated Invoice
            QBOHelper.VerifyInvoiceSparseUpdate(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void InvoiceDeleteTestUsingoAuth()
        {
            //Creating the Invoice for Adding
            Invoice invoice = QBOHelper.CreateInvoice(qboContextoAuth);
            //Adding the Invoice
            Invoice added = Helper.Add<Invoice>(qboContextoAuth, invoice);
            //Delete the returned entity
            try
            {
                Invoice deleted = Helper.Delete<Invoice>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }


        [TestMethod] [Ignore]
        public void InvoiceVoidTestUsingoAuth()
        {
            //Creating the Invoice for Adding
            Invoice invoice = QBOHelper.CreateInvoice(qboContextoAuth);
            //Adding the Invoice
            Invoice added = Helper.Add<Invoice>(qboContextoAuth, invoice);
            //Void the returned entity
            try
            {
                Invoice voided = Helper.Void<Invoice>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Voided, voided.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        #endregion

        #region Test cases for CDC Operations

        [TestMethod]
        public void InvoiceCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            //InvoiceAddTestUsingoAuth();

            //Retrieving the Invoice using FindAll
            List<Invoice> invoices = Helper.CDC(qboContextoAuth, new Invoice(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(invoices);
            Assert.IsTrue(invoices.Count<Invoice>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void InvoiceBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Invoice existing = Helper.FindOrAdd(qboContextoAuth, new Invoice());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateInvoice(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateInvoice(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Invoice");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Invoice>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Invoice).Id));
                }
                else if (resp.ResponseType == ResponseType.Query)
                {
                    Assert.IsTrue(resp.Entities != null && resp.Entities.Count > 0);
                }
                else if (resp.ResponseType == ResponseType.CdcQuery)
                {
                    Assert.IsTrue(resp.CDCResponse != null && resp.CDCResponse.entities != null && resp.CDCResponse.entities.Count > 0);
                }

                position++;
            }
        }

        #endregion

        #region Test cases for Query
        [TestMethod]
        public void InvoiceQueryUsingoAuth()
        {
            QueryService<Invoice> entityQuery = new QueryService<Invoice>(qboContextoAuth);
            Invoice existing = Helper.FindOrAdd<Invoice>(qboContextoAuth, new Invoice());
            //List<Invoice> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<Invoice> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Invoice where Id='" + existing.Id + "'").ToList<Invoice>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void InvoiceAddAsyncTestsUsingoAuth()
        {
            //Creating the Invoice for Add
            Invoice entity = QBOHelper.CreateInvoice(qboContextoAuth);

            Invoice added = Helper.AddAsync<Invoice>(qboContextoAuth, entity);
            QBOHelper.VerifyInvoice(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void InvoiceRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            InvoiceAddTestUsingoAuth();

            //Retrieving the Invoice using FindAll
            Helper.FindAllAsync<Invoice>(qboContextoAuth, new Invoice());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void InvoiceFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Invoice for Adding
            Invoice entity = QBOHelper.CreateInvoice(qboContextoAuth);
            //Adding the Invoice
            Invoice added = Helper.Add<Invoice>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Invoice>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void InvoiceUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Invoice for Adding
            Invoice entity = QBOHelper.CreateInvoice(qboContextoAuth);
            //Adding the Invoice
            Invoice added = Helper.Add<Invoice>(qboContextoAuth, entity);

            //Update the Invoice
            Invoice updated = QBOHelper.UpdateInvoice(qboContextoAuth, added);
            //Call the service
            Invoice updatedReturned = Helper.UpdateAsync<Invoice>(qboContextoAuth, updated);
            //Verify updated Invoice
            QBOHelper.VerifyInvoice(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void InvoiceDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Invoice for Adding
            Invoice entity = QBOHelper.CreateInvoice(qboContextoAuth);
            //Adding the Invoice
            Invoice added = Helper.Add<Invoice>(qboContextoAuth, entity);

            Helper.DeleteAsync<Invoice>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void InvoiceVoidAsyncTestsUsingoAuth()
        {
            //Creating the Invoice for Adding
            Invoice entity = QBOHelper.CreateInvoice(qboContextoAuth);
            //Adding the Invoice
            Invoice added = Helper.Add<Invoice>(qboContextoAuth, entity);

            Helper.VoidAsync<Invoice>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
