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

using System.Collections.ObjectModel;
using Intuit.Ipp.DataService;

namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass] 
    public class RefundReceiptTest
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
        public void RefundReceiptAddTestUsingoAuth()
        {
            //Creating the RefundReceipt for Add
            RefundReceipt refundReceipt = QBOHelper.CreateRefundReceipt(qboContextoAuth);
            //Adding the RefundReceipt
            RefundReceipt added = Helper.Add<RefundReceipt>(qboContextoAuth, refundReceipt);
            //Verify the added RefundReceipt
            QBOHelper.VerifyRefundReceipt(refundReceipt, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void RefundReceiptFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            RefundReceiptAddTestUsingoAuth();

            //Retrieving the RefundReceipt using FindAll
            List<RefundReceipt> refundReceipts = Helper.FindAll<RefundReceipt>(qboContextoAuth, new RefundReceipt(), 1, 500);
            Assert.IsNotNull(refundReceipts);
            Assert.IsTrue(refundReceipts.Count<RefundReceipt>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void RefundReceiptFindbyIdTestUsingoAuth()
        {
            //Creating the RefundReceipt for Adding
            RefundReceipt refundReceipt = QBOHelper.CreateRefundReceipt(qboContextoAuth);
            //Adding the RefundReceipt
            RefundReceipt added = Helper.Add<RefundReceipt>(qboContextoAuth, refundReceipt);
            RefundReceipt found = Helper.FindById<RefundReceipt>(qboContextoAuth, added);
            QBOHelper.VerifyRefundReceipt(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void RefundReceiptUpdateTestUsingoAuth()
        {
            //Creating the RefundReceipt for Adding
            RefundReceipt refundReceipt = QBOHelper.CreateRefundReceipt(qboContextoAuth);
            //Adding the RefundReceipt
            RefundReceipt added = Helper.Add<RefundReceipt>(qboContextoAuth, refundReceipt);
            //Change the data of added entity
            RefundReceipt changed = QBOHelper.UpdateRefundReceipt(qboContextoAuth, added);
            //Update the returned entity data
            RefundReceipt updated = Helper.Update<RefundReceipt>(qboContextoAuth, changed);//Verify the updated RefundReceipt
            QBOHelper.VerifyRefundReceipt(changed, updated);
        }

        [TestMethod]
        public void RefundReceiptSparseUpdateTestUsingoAuth()
        {
            //Creating the RefundReceipt for Adding
            RefundReceipt refundReceipt = QBOHelper.CreateRefundReceipt(qboContextoAuth);
            //Adding the RefundReceipt
            RefundReceipt added = Helper.Add<RefundReceipt>(qboContextoAuth, refundReceipt);
            //Change the data of added entity
            RefundReceipt changed = QBOHelper.UpdateRefundReceiptSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            RefundReceipt updated = Helper.Update<RefundReceipt>(qboContextoAuth, changed);//Verify the updated RefundReceipt
            QBOHelper.VerifyRefundReceiptSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void RefundReceiptDeleteTestUsingoAuth()
        {
            //Creating the RefundReceipt for Adding
            RefundReceipt refundReceipt = QBOHelper.CreateRefundReceipt(qboContextoAuth);
            //Adding the RefundReceipt
            RefundReceipt added = Helper.Add<RefundReceipt>(qboContextoAuth, refundReceipt);
            //Delete the returned entity
            try
            {
                RefundReceipt deleted = Helper.Delete<RefundReceipt>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod][Ignore]
        public void RefundReceiptVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            RefundReceipt entity = QBOHelper.CreateRefundReceipt(qboContextoAuth);
            //Adding the entity
            RefundReceipt added = Helper.Add<RefundReceipt>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                RefundReceipt voided = Helper.Void<RefundReceipt>(qboContextoAuth, added);
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
        public void RefundReceiptCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            RefundReceiptAddTestUsingoAuth();

            //Retrieving the RefundReceipt using CDC
            List<RefundReceipt> entities = Helper.CDC(qboContextoAuth, new RefundReceipt(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<RefundReceipt>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void RefundReceiptBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            RefundReceipt existing = Helper.FindOrAdd(qboContextoAuth, new RefundReceipt());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateRefundReceipt(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateRefundReceipt(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from RefundReceipt");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<RefundReceipt>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as RefundReceipt).Id));
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
        public void RefundReceiptQueryUsingoAuth()
        {
            QueryService<RefundReceipt> entityQuery = new QueryService<RefundReceipt>(qboContextoAuth);
            RefundReceipt existing = Helper.FindOrAdd<RefundReceipt>(qboContextoAuth, new RefundReceipt());
            //List<RefundReceipt> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<RefundReceipt> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM RefundReceipt where Id='" + existing.Id + "'").ToList<RefundReceipt>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void RefundReceiptAddAsyncTestsUsingoAuth()
        {
            //Creating the RefundReceipt for Add
            RefundReceipt entity = QBOHelper.CreateRefundReceipt(qboContextoAuth);

            RefundReceipt added = Helper.AddAsync<RefundReceipt>(qboContextoAuth, entity);
            QBOHelper.VerifyRefundReceipt(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void RefundReceiptRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            RefundReceiptAddTestUsingoAuth();

            //Retrieving the RefundReceipt using FindAll
            Helper.FindAllAsync<RefundReceipt>(qboContextoAuth, new RefundReceipt());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void RefundReceiptFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the RefundReceipt for Adding
            RefundReceipt entity = QBOHelper.CreateRefundReceipt(qboContextoAuth);
            //Adding the RefundReceipt
            RefundReceipt added = Helper.Add<RefundReceipt>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<RefundReceipt>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void RefundReceiptUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the RefundReceipt for Adding
            RefundReceipt entity = QBOHelper.CreateRefundReceipt(qboContextoAuth);
            //Adding the RefundReceipt
            RefundReceipt added = Helper.Add<RefundReceipt>(qboContextoAuth, entity);

            //Update the RefundReceipt
            RefundReceipt updated = QBOHelper.UpdateRefundReceipt(qboContextoAuth, added);
            //Call the service
            RefundReceipt updatedReturned = Helper.UpdateAsync<RefundReceipt>(qboContextoAuth, updated);
            //Verify updated RefundReceipt
            QBOHelper.VerifyRefundReceipt(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void RefundReceiptDeleteAsyncTestsUsingoAuth()
        {
            //Creating the RefundReceipt for Adding
            RefundReceipt entity = QBOHelper.CreateRefundReceipt(qboContextoAuth);
            //Adding the RefundReceipt
            RefundReceipt added = Helper.Add<RefundReceipt>(qboContextoAuth, entity);

            Helper.DeleteAsync<RefundReceipt>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        [Ignore]
        public void RefundReceiptVoidAsyncTestsUsingoAuth()
        {
            //Creating the RefundReceipt for Adding
            RefundReceipt entity = QBOHelper.CreateRefundReceipt(qboContextoAuth);
            //Adding the RefundReceipt
            RefundReceipt added = Helper.Add<RefundReceipt>(qboContextoAuth, entity);

            Helper.VoidAsync<RefundReceipt>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
