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
    public class PurchaseOrderTest
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
        public void PurchaseOrderAddTestUsingoAuth()
        {
            //Creating the PurchaseOrder for Add
            PurchaseOrder purchaseOrder = QBOHelper.CreatePurchaseOrder(qboContextoAuth);
            //Adding the PurchaseOrder
            PurchaseOrder added = Helper.Add<PurchaseOrder>(qboContextoAuth, purchaseOrder);
            //Verify the added PurchaseOrder
            QBOHelper.VerifyPurchaseOrder(purchaseOrder, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void PurchaseOrderFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            PurchaseOrderAddTestUsingoAuth();

            //Retrieving the PurchaseOrder using FindAll
            List<PurchaseOrder> purchaseOrders = Helper.FindAll<PurchaseOrder>(qboContextoAuth, new PurchaseOrder(), 1, 500);
            Assert.IsNotNull(purchaseOrders);
            Assert.IsTrue(purchaseOrders.Count<PurchaseOrder>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void PurchaseOrderFindbyIdTestUsingoAuth()
        {
            //Creating the PurchaseOrder for Adding
            PurchaseOrder purchaseOrder = QBOHelper.CreatePurchaseOrder(qboContextoAuth);
            //Adding the PurchaseOrder
            PurchaseOrder added = Helper.Add<PurchaseOrder>(qboContextoAuth, purchaseOrder);
            PurchaseOrder found = Helper.FindById<PurchaseOrder>(qboContextoAuth, added);
            QBOHelper.VerifyPurchaseOrder(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void PurchaseOrderUpdateTestUsingoAuth()
        {
            //Creating the PurchaseOrder for Adding
            PurchaseOrder purchaseOrder = QBOHelper.CreatePurchaseOrder(qboContextoAuth);
            //Adding the PurchaseOrder
            PurchaseOrder added = Helper.Add<PurchaseOrder>(qboContextoAuth, purchaseOrder);
            //Change the data of added entity
            PurchaseOrder changed = QBOHelper.UpdatePurchaseOrder(qboContextoAuth, added);
            //Update the returned entity data
            PurchaseOrder updated = Helper.Update<PurchaseOrder>(qboContextoAuth, changed);
            //Verify the updated PurchaseOrder
            QBOHelper.VerifyPurchaseOrder(changed, updated);
        }

        [TestMethod]
        public void PurchaseOrderSparseUpdateTestUsingoAuth()
        {
            //Creating the PurchaseOrder for Adding
            PurchaseOrder purchaseOrder = QBOHelper.CreatePurchaseOrder(qboContextoAuth);
            //Adding the PurchaseOrder
            PurchaseOrder added = Helper.Add<PurchaseOrder>(qboContextoAuth, purchaseOrder);
            //Change the data of added entity
            PurchaseOrder changed = QBOHelper.UpdatePurchaseOrderSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            PurchaseOrder updated = Helper.Update<PurchaseOrder>(qboContextoAuth, changed);
            //Verify the updated PurchaseOrder
            QBOHelper.VerifyPurchaseOrderSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void PurchaseOrderDeleteTestUsingoAuth()
        {
            //Creating the PurchaseOrder for Adding
            PurchaseOrder purchaseOrder = QBOHelper.CreatePurchaseOrder(qboContextoAuth);
            //Adding the PurchaseOrder
            PurchaseOrder added = Helper.Add<PurchaseOrder>(qboContextoAuth, purchaseOrder);
            //Delete the returned entity
            try
            {
                PurchaseOrder deleted = Helper.Delete<PurchaseOrder>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod][Ignore]
        public void PurchaseOrderVoidTestUsingoAuth()
        {
            //Creating the PurchaseOrder for Adding
            PurchaseOrder purchaseOrder = QBOHelper.CreatePurchaseOrder(qboContextoAuth);
            //Adding the PurchaseOrder
            PurchaseOrder added = Helper.Add<PurchaseOrder>(qboContextoAuth, purchaseOrder);
            //Delete the returned entity
            try
            {
                PurchaseOrder voided = Helper.Void<PurchaseOrder>(qboContextoAuth, added);
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
        public void PurchaseOrderCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            PurchaseOrderAddTestUsingoAuth();

            //Retrieving the PurchaseOrder using CDC
            List<PurchaseOrder> entities = Helper.CDC(qboContextoAuth, new PurchaseOrder(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<PurchaseOrder>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void PurchaseOrderBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            PurchaseOrder existing = Helper.FindOrAdd(qboContextoAuth, new PurchaseOrder());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreatePurchaseOrder(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdatePurchaseOrder(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from PurchaseOrder");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<PurchaseOrder>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as PurchaseOrder).Id));
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
        public void PurchaseOrderQueryUsingoAuth()
        {
            QueryService<PurchaseOrder> entityQuery = new QueryService<PurchaseOrder>(qboContextoAuth);
            PurchaseOrder existing = Helper.FindOrAdd<PurchaseOrder>(qboContextoAuth, new PurchaseOrder());
            //List<PurchaseOrder> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<PurchaseOrder> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM PurchaseOrder where Id='" + existing.Id + "'").ToList<PurchaseOrder>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void PurchaseOrderAddAsyncTestsUsingoAuth()
        {
            //Creating the PurchaseOrder for Add
            PurchaseOrder entity = QBOHelper.CreatePurchaseOrder(qboContextoAuth);

            PurchaseOrder added = Helper.AddAsync<PurchaseOrder>(qboContextoAuth, entity);
            QBOHelper.VerifyPurchaseOrder(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void PurchaseOrderRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            PurchaseOrderAddTestUsingoAuth();

            //Retrieving the PurchaseOrder using FindAll
            Helper.FindAllAsync<PurchaseOrder>(qboContextoAuth, new PurchaseOrder());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void PurchaseOrderFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the PurchaseOrder for Adding
            PurchaseOrder entity = QBOHelper.CreatePurchaseOrder(qboContextoAuth);
            //Adding the PurchaseOrder
            PurchaseOrder added = Helper.Add<PurchaseOrder>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<PurchaseOrder>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void PurchaseOrderUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the PurchaseOrder for Adding
            PurchaseOrder entity = QBOHelper.CreatePurchaseOrder(qboContextoAuth);
            //Adding the PurchaseOrder
            PurchaseOrder added = Helper.Add<PurchaseOrder>(qboContextoAuth, entity);

            //Update the PurchaseOrder
            PurchaseOrder updated = QBOHelper.UpdatePurchaseOrder(qboContextoAuth, added);
            //Call the service
            PurchaseOrder updatedReturned = Helper.UpdateAsync<PurchaseOrder>(qboContextoAuth, updated);
            //Verify updated PurchaseOrder
            QBOHelper.VerifyPurchaseOrder(updated, updatedReturned);
        }

        [TestMethod]
        public void PurchaseOrderSparseUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the PurchaseOrder for Adding
            PurchaseOrder entity = QBOHelper.CreatePurchaseOrder(qboContextoAuth);
            //Adding the PurchaseOrder
            PurchaseOrder added = Helper.Add<PurchaseOrder>(qboContextoAuth, entity);

            //Update the PurchaseOrder
            PurchaseOrder updated = QBOHelper.UpdatePurchaseOrderSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Call the service
            PurchaseOrder updatedReturned = Helper.UpdateAsync<PurchaseOrder>(qboContextoAuth, updated);
            //Verify updated PurchaseOrder
            QBOHelper.VerifyPurchaseOrderSparse(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void PurchaseOrderDeleteAsyncTestsUsingoAuth()
        {
            //Creating the PurchaseOrder for Adding
            PurchaseOrder entity = QBOHelper.CreatePurchaseOrder(qboContextoAuth);
            //Adding the PurchaseOrder
            PurchaseOrder added = Helper.Add<PurchaseOrder>(qboContextoAuth, entity);

            Helper.DeleteAsync<PurchaseOrder>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void PurchaseOrderVoidAsyncTestsUsingoAuth()
        {
            //Creating the PurchaseOrder for Adding
            PurchaseOrder entity = QBOHelper.CreatePurchaseOrder(qboContextoAuth);
            //Adding the PurchaseOrder
            PurchaseOrder added = Helper.Add<PurchaseOrder>(qboContextoAuth, entity);

            Helper.VoidAsync<PurchaseOrder>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
