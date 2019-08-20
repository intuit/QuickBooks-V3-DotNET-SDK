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
    public class SalesReceiptTest
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
        public void SalesReceiptAddTestUsingoAuth()
        {
            //Creating the SalesReceipt for Add
            SalesReceipt salesReceipt = QBOHelper.CreateSalesReceipt(qboContextoAuth);
            //Adding the SalesReceipt
            SalesReceipt added = Helper.Add<SalesReceipt>(qboContextoAuth, salesReceipt);
            //Verify the added SalesReceipt
            QBOHelper.VerifySalesReceipt(salesReceipt, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void SalesReceiptFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            SalesReceiptAddTestUsingoAuth();

            //Retrieving the SalesReceipt using FindAll
            List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(qboContextoAuth, new SalesReceipt(), 1, 500);
            Assert.IsNotNull(salesReceipts);
            Assert.IsTrue(salesReceipts.Count<SalesReceipt>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void SalesReceiptFindbyIdTestUsingoAuth()
        {
            //Creating the SalesReceipt for Adding
            SalesReceipt salesReceipt = QBOHelper.CreateSalesReceipt(qboContextoAuth);
            //Adding the SalesReceipt
            SalesReceipt added = Helper.Add<SalesReceipt>(qboContextoAuth, salesReceipt);
            SalesReceipt found = Helper.FindById<SalesReceipt>(qboContextoAuth, added);
            QBOHelper.VerifySalesReceipt(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void SalesReceiptUpdateTestUsingoAuth()
        {
            
            //Creating the SalesReceipt for Adding
            SalesReceipt salesReceipt = QBOHelper.CreateSalesReceipt(qboContextoAuth);
            //Adding the SalesReceipt
            SalesReceipt added = Helper.Add<SalesReceipt>(qboContextoAuth, salesReceipt);
            //Change the data of added entity
            SalesReceipt changed = QBOHelper.UpdateSalesReceipt(qboContextoAuth, added);
            //Update the returned entity data
            SalesReceipt updated = Helper.Update<SalesReceipt>(qboContextoAuth, changed);//Verify the updated SalesReceipt
            QBOHelper.VerifySalesReceipt(changed, updated);
        }

        [TestMethod]
        public void SalesReceiptSparseUpdateTestUsingoAuth()
        {
            //Creating the SalesReceipt for Adding
            SalesReceipt salesReceipt = QBOHelper.CreateSalesReceipt(qboContextoAuth);
            //Adding the SalesReceipt
            SalesReceipt added = Helper.Add<SalesReceipt>(qboContextoAuth, salesReceipt);
            //Change the data of added entity
            SalesReceipt changed = QBOHelper.SparseUpdateSalesReceipt(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            SalesReceipt updated = Helper.Update<SalesReceipt>(qboContextoAuth, changed);//Verify the updated SalesReceipt
            QBOHelper.VerifySalesReceiptSparseUpdate(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void SalesReceiptDeleteTestUsingoAuth()
        {
            //Creating the SalesReceipt for Adding
            SalesReceipt salesReceipt = QBOHelper.CreateSalesReceipt(qboContextoAuth);
            //Adding the SalesReceipt
            SalesReceipt added = Helper.Add<SalesReceipt>(qboContextoAuth, salesReceipt);
            //Delete the returned entity
            try
            {
                SalesReceipt deleted = Helper.Delete<SalesReceipt>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [Ignore]
        public void SalesReceiptVoidTestUsingoAuth()
        {
            //Creating the SalesReceipt for Adding
            SalesReceipt salesReceipt = QBOHelper.CreateSalesReceipt(qboContextoAuth);
            //Adding the SalesReceipt
            SalesReceipt added = Helper.Add<SalesReceipt>(qboContextoAuth, salesReceipt);
            //Void the returned entity
            try
            {
                SalesReceipt voided = Helper.Void<SalesReceipt>(qboContextoAuth, added);
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
        public void SalesReceiptCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            SalesReceiptAddTestUsingoAuth();

            //Retrieving the SalesReceipt using FindAll
            List<SalesReceipt> salesReceipts = Helper.CDC(qboContextoAuth, new SalesReceipt(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(salesReceipts);
            Assert.IsTrue(salesReceipts.Count<SalesReceipt>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void SalesReceiptBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            SalesReceipt existing = Helper.FindOrAdd(qboContextoAuth, new SalesReceipt());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateSalesReceipt(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateSalesReceipt(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from SalesReceipt");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<SalesReceipt>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as SalesReceipt).Id));
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
        public void SalesReceiptQueryUsingoAuth()
        {
            QueryService<SalesReceipt> entityQuery = new QueryService<SalesReceipt>(qboContextoAuth);
            SalesReceipt existing = Helper.FindOrAdd<SalesReceipt>(qboContextoAuth, new SalesReceipt());
            //List<SalesReceipt> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<SalesReceipt> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM SalesReceipt where Id='" + existing.Id + "'").ToList<SalesReceipt>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void SalesReceiptAddAsyncTestsUsingoAuth()
        {
            //Creating the SalesReceipt for Add
            SalesReceipt entity = QBOHelper.CreateSalesReceipt(qboContextoAuth);

            SalesReceipt added = Helper.AddAsync<SalesReceipt>(qboContextoAuth, entity);
            QBOHelper.VerifySalesReceipt(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void SalesReceiptRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            SalesReceiptAddTestUsingoAuth();

            //Retrieving the SalesReceipt using FindAll
            Helper.FindAllAsync<SalesReceipt>(qboContextoAuth, new SalesReceipt());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void SalesReceiptFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the SalesReceipt for Adding
            SalesReceipt entity = QBOHelper.CreateSalesReceipt(qboContextoAuth);
            //Adding the SalesReceipt
            SalesReceipt added = Helper.Add<SalesReceipt>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<SalesReceipt>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void SalesReceiptUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the SalesReceipt for Adding
            SalesReceipt entity = QBOHelper.CreateSalesReceipt(qboContextoAuth);
            //Adding the SalesReceipt
            SalesReceipt added = Helper.Add<SalesReceipt>(qboContextoAuth, entity);

            //Update the SalesReceipt
            SalesReceipt updated = QBOHelper.UpdateSalesReceipt(qboContextoAuth, added);
            //Call the service
            SalesReceipt updatedReturned = Helper.UpdateAsync<SalesReceipt>(qboContextoAuth, updated);
            //Verify updated SalesReceipt
            QBOHelper.VerifySalesReceipt(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void SalesReceiptDeleteAsyncTestsUsingoAuth()
        {
            //Creating the SalesReceipt for Adding
            SalesReceipt entity = QBOHelper.CreateSalesReceipt(qboContextoAuth);
            //Adding the SalesReceipt
            SalesReceipt added = Helper.Add<SalesReceipt>(qboContextoAuth, entity);

            Helper.DeleteAsync<SalesReceipt>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        [Ignore]
        public void SalesReceiptVoidAsyncTestsUsingoAuth()
        {
            //Creating the SalesReceipt for Adding
            SalesReceipt entity = QBOHelper.CreateSalesReceipt(qboContextoAuth);
            //Adding the SalesReceipt
            SalesReceipt added = Helper.Add<SalesReceipt>(qboContextoAuth, entity);

            Helper.VoidAsync<SalesReceipt>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
