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
    [TestClass] [Ignore]
    public class SalesOrderTest
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
        public void SalesOrderAddTestUsingoAuth()
        {
            //Creating the SalesOrder for Add
            SalesOrder salesOrder = QBOHelper.CreateSalesOrder(qboContextoAuth);
            //Adding the SalesOrder
            SalesOrder added = Helper.Add<SalesOrder>(qboContextoAuth, salesOrder);
            //Verify the added SalesOrder
            QBOHelper.VerifySalesOrder(salesOrder, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void SalesOrderFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            SalesOrderAddTestUsingoAuth();

            //Retrieving the SalesOrder using FindAll
            List<SalesOrder> salesOrders = Helper.FindAll<SalesOrder>(qboContextoAuth, new SalesOrder(), 1, 500);
            Assert.IsNotNull(salesOrders);
            Assert.IsTrue(salesOrders.Count<SalesOrder>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void SalesOrderFindbyIdTestUsingoAuth()
        {
            //Creating the SalesOrder for Adding
            SalesOrder salesOrder = QBOHelper.CreateSalesOrder(qboContextoAuth);
            //Adding the SalesOrder
            SalesOrder added = Helper.Add<SalesOrder>(qboContextoAuth, salesOrder);
            SalesOrder found = Helper.FindById<SalesOrder>(qboContextoAuth, added);
            QBOHelper.VerifySalesOrder(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void SalesOrderUpdateTestUsingoAuth()
        {
            //Creating the SalesOrder for Adding
            SalesOrder salesOrder = QBOHelper.CreateSalesOrder(qboContextoAuth);
            //Adding the SalesOrder
            SalesOrder added = Helper.Add<SalesOrder>(qboContextoAuth, salesOrder);
            //Change the data of added entity
            SalesOrder changed = QBOHelper.UpdateSalesOrder(qboContextoAuth, added);
            //Update the returned entity data
            SalesOrder updated = Helper.Update<SalesOrder>(qboContextoAuth, changed);//Verify the updated SalesOrder
            QBOHelper.VerifySalesOrder(changed, updated);
        }

        [TestMethod]
        public void SalesOrderSparseUpdateTestUsingoAuth()
        {
            //Creating the SalesOrder for Adding
            SalesOrder salesOrder = QBOHelper.CreateSalesOrder(qboContextoAuth);
            //Adding the SalesOrder
            SalesOrder added = Helper.Add<SalesOrder>(qboContextoAuth, salesOrder);
            //Change the data of added entity
            SalesOrder changed = QBOHelper.UpdateSalesOrderSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            SalesOrder updated = Helper.Update<SalesOrder>(qboContextoAuth, changed);//Verify the updated SalesOrder
            QBOHelper.VerifySalesOrderSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void SalesOrderDeleteTestUsingoAuth()
        {
            //Creating the SalesOrder for Adding
            SalesOrder salesOrder = QBOHelper.CreateSalesOrder(qboContextoAuth);
            //Adding the SalesOrder
            SalesOrder added = Helper.Add<SalesOrder>(qboContextoAuth, salesOrder);
            //Delete the returned entity
            try
            {
                SalesOrder deleted = Helper.Delete<SalesOrder>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SalesOrderVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            SalesOrder entity = QBOHelper.CreateSalesOrder(qboContextoAuth);
            //Adding the entity
            SalesOrder added = Helper.Add<SalesOrder>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                SalesOrder voided = Helper.Void<SalesOrder>(qboContextoAuth, added);
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
        public void SalesOrderCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            SalesOrderAddTestUsingoAuth();

            //Retrieving the SalesOrder using CDC
            List<SalesOrder> entities = Helper.CDC(qboContextoAuth, new SalesOrder(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<SalesOrder>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void SalesOrderBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            SalesOrder existing = Helper.FindOrAdd(qboContextoAuth, new SalesOrder());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateSalesOrder(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateSalesOrder(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from SalesOrder");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<SalesOrder>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as SalesOrder).Id));
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
        public void SalesOrderQueryUsingoAuth()
        {
            QueryService<SalesOrder> entityQuery = new QueryService<SalesOrder>(qboContextoAuth);
            SalesOrder existing = Helper.FindOrAdd<SalesOrder>(qboContextoAuth, new SalesOrder());
            List<SalesOrder> entities = entityQuery.ExecuteIdsQuery("Select * from Customer where Id == "+existing.Id+"'").ToList();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void SalesOrderAddAsyncTestsUsingoAuth()
        {
            //Creating the SalesOrder for Add
            SalesOrder entity = QBOHelper.CreateSalesOrder(qboContextoAuth);

            SalesOrder added = Helper.AddAsync<SalesOrder>(qboContextoAuth, entity);
            QBOHelper.VerifySalesOrder(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void SalesOrderRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            SalesOrderAddTestUsingoAuth();

            //Retrieving the SalesOrder using FindAll
            Helper.FindAllAsync<SalesOrder>(qboContextoAuth, new SalesOrder());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void SalesOrderFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the SalesOrder for Adding
            SalesOrder entity = QBOHelper.CreateSalesOrder(qboContextoAuth);
            //Adding the SalesOrder
            SalesOrder added = Helper.Add<SalesOrder>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<SalesOrder>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void SalesOrderUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the SalesOrder for Adding
            SalesOrder entity = QBOHelper.CreateSalesOrder(qboContextoAuth);
            //Adding the SalesOrder
            SalesOrder added = Helper.Add<SalesOrder>(qboContextoAuth, entity);

            //Update the SalesOrder
            SalesOrder updated = QBOHelper.UpdateSalesOrder(qboContextoAuth, added);
            //Call the service
            SalesOrder updatedReturned = Helper.UpdateAsync<SalesOrder>(qboContextoAuth, updated);
            //Verify updated SalesOrder
            QBOHelper.VerifySalesOrder(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void SalesOrderDeleteAsyncTestsUsingoAuth()
        {
            //Creating the SalesOrder for Adding
            SalesOrder entity = QBOHelper.CreateSalesOrder(qboContextoAuth);
            //Adding the SalesOrder
            SalesOrder added = Helper.Add<SalesOrder>(qboContextoAuth, entity);

            Helper.DeleteAsync<SalesOrder>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void SalesOrderVoidAsyncTestsUsingoAuth()
        {
            //Creating the SalesOrder for Adding
            SalesOrder entity = QBOHelper.CreateSalesOrder(qboContextoAuth);
            //Adding the SalesOrder
            SalesOrder added = Helper.Add<SalesOrder>(qboContextoAuth, entity);

            Helper.VoidAsync<SalesOrder>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
