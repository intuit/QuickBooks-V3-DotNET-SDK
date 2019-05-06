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
    public class TransferTest
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
        [Ignore]
        public void TransferAddTestUsingoAuth()
        {
            //Creating the Transfer for Add
            Transfer transfer = QBOHelper.CreateTransfer(qboContextoAuth);
            //Adding the Transfer
            Transfer added = Helper.Add<Transfer>(qboContextoAuth, transfer);
            //Verify the added Transfer
            QBOHelper.VerifyTransfer(transfer, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void TransferFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            TransferAddTestUsingoAuth();

            //Retrieving the Transfer using FindAll
            List<Transfer> transfers = Helper.FindAll<Transfer>(qboContextoAuth, new Transfer(), 1, 500);
            Assert.IsNotNull(transfers);
            Assert.IsTrue(transfers.Count<Transfer>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void TransferFindbyIdTestUsingoAuth()
        {
            //Creating the Transfer for Adding
            Transfer transfer = QBOHelper.CreateTransfer(qboContextoAuth);
            //Adding the Transfer
            Transfer added = Helper.Add<Transfer>(qboContextoAuth, transfer);
            Transfer found = Helper.FindById<Transfer>(qboContextoAuth, added);
            QBOHelper.VerifyTransfer(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        [Ignore]
        public void TransferUpdateTestUsingoAuth()
        {
            //Creating the Transfer for Adding
            Transfer transfer = QBOHelper.CreateTransfer(qboContextoAuth);
            //Adding the Transfer
            Transfer added = Helper.Add<Transfer>(qboContextoAuth, transfer);
            //Change the data of added entity
            Transfer changed = QBOHelper.UpdateTransfer(qboContextoAuth, added);
            //Update the returned entity data
            Transfer updated = Helper.Update<Transfer>(qboContextoAuth, changed);
            QBOHelper.VerifyTransfer(changed, updated);
        }


        [TestMethod]
        [Ignore]
        public void TransferSparseUpdateTestUsingoAuth()
        {
            //Creating the Transfer for Adding
            Transfer transfer = QBOHelper.CreateTransfer(qboContextoAuth);
            //Adding the Transfer
            Transfer added = Helper.Add<Transfer>(qboContextoAuth, transfer);
            //Change the data of added entity
            Transfer changed = QBOHelper.UpdateTransferSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Transfer updated = Helper.Update<Transfer>(qboContextoAuth, changed);
            QBOHelper.VerifyTransferSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        [Ignore]
        public void TransferDeleteTestUsingoAuth()
        {
            //Creating the Transfer for Adding
            Transfer transfer = QBOHelper.CreateTransfer(qboContextoAuth);
            //Adding the Transfer
            Transfer added = Helper.Add<Transfer>(qboContextoAuth, transfer);
            //Delete the returned entity
            try
            {
                Transfer deleted = Helper.Delete<Transfer>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [Ignore]
        public void TransferVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            Transfer entity = QBOHelper.CreateTransfer(qboContextoAuth);
            //Adding the entity
            Transfer added = Helper.Add<Transfer>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                Transfer voided = Helper.Void<Transfer>(qboContextoAuth, added);
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
        [Ignore]
        public void TransferCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            TransferAddTestUsingoAuth();

            //Retrieving the Transfer using CDC
            List<Transfer> entities = Helper.CDC(qboContextoAuth, new Transfer(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<Transfer>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        [Ignore]
        public void TransferBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Transfer existing = Helper.FindOrAdd(qboContextoAuth, new Transfer());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateTransfer(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateTransfer(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Transfer");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Transfer>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Transfer).Id));
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
        public void TransferQueryUsingoAuth()
        {
            QueryService<Transfer> entityQuery = new QueryService<Transfer>(qboContextoAuth);
            Transfer existing = Helper.FindOrAdd<Transfer>(qboContextoAuth, new Transfer());
            //List<Transfer> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<Transfer> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Transfer where Id='" + existing.Id + "'").ToList<Transfer>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        [Ignore]
        public void TransferAddAsyncTestsUsingoAuth()
        {
            //Creating the Transfer for Add
            Transfer entity = QBOHelper.CreateTransfer(qboContextoAuth);

            Transfer added = Helper.AddAsync<Transfer>(qboContextoAuth, entity);
            QBOHelper.VerifyTransfer(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void TransferRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            TransferAddTestUsingoAuth();

            //Retrieving the Transfer using FindAll
            Helper.FindAllAsync<Transfer>(qboContextoAuth, new Transfer());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void TransferFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Transfer for Adding
            Transfer entity = QBOHelper.CreateTransfer(qboContextoAuth);
            //Adding the Transfer
            Transfer added = Helper.Add<Transfer>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Transfer>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        [Ignore]
        public void TransferUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Transfer for Adding
            Transfer entity = QBOHelper.CreateTransfer(qboContextoAuth);
            //Adding the Transfer
            Transfer added = Helper.Add<Transfer>(qboContextoAuth, entity);

            //Update the Transfer
            Transfer updated = QBOHelper.UpdateTransfer(qboContextoAuth, added);
            //Call the service
            Transfer updatedReturned = Helper.UpdateAsync<Transfer>(qboContextoAuth, updated);
            //Verify updated Transfer
            QBOHelper.VerifyTransfer(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        [Ignore]
        public void TransferDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Transfer for Adding
            Transfer entity = QBOHelper.CreateTransfer(qboContextoAuth);
            //Adding the Transfer
            Transfer added = Helper.Add<Transfer>(qboContextoAuth, entity);

            Helper.DeleteAsync<Transfer>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        [Ignore]
        public void TransferVoidAsyncTestsUsingoAuth()
        {
            //Creating the Transfer for Adding
            Transfer entity = QBOHelper.CreateTransfer(qboContextoAuth);
            //Adding the Transfer
            Transfer added = Helper.Add<Transfer>(qboContextoAuth, entity);

            Helper.VoidAsync<Transfer>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
