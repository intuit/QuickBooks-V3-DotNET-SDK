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
    public class StatusTest
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
        public void StatusAddTestUsingoAuth()
        {
            //Creating the Status for Add
            Status status = QBOHelper.CreateStatus(qboContextoAuth);
            //Adding the Status
            Status added = Helper.Add<Status>(qboContextoAuth, status);
            //Verify the added Status
            QBOHelper.VerifyStatus(status, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void StatusFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            StatusAddTestUsingoAuth();

            //Retrieving the Status using FindAll
            List<Status> statuss = Helper.FindAll<Status>(qboContextoAuth, new Status(), 1, 500);
            Assert.IsNotNull(statuss);
            Assert.IsTrue(statuss.Count<Status>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void StatusFindbyIdTestUsingoAuth()
        {
            //Creating the Status for Adding
            Status status = QBOHelper.CreateStatus(qboContextoAuth);
            //Adding the Status
            Status added = Helper.Add<Status>(qboContextoAuth, status);
            Status found = Helper.FindById<Status>(qboContextoAuth, added);
            QBOHelper.VerifyStatus(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void StatusUpdateTestUsingoAuth()
        {
            //Creating the Status for Adding
            Status status = QBOHelper.CreateStatus(qboContextoAuth);
            //Adding the Status
            Status added = Helper.Add<Status>(qboContextoAuth, status);
            //Change the data of added entity
            Status changed = QBOHelper.UpdateStatus(qboContextoAuth, added);
            //Update the returned entity data
            Status updated = Helper.Update<Status>(qboContextoAuth, changed);//Verify the updated Status
            QBOHelper.VerifyStatus(changed, updated);
        }

        [TestMethod]
        public void StatusSparseUpdateTestUsingoAuth()
        {
            //Creating the Status for Adding
            Status status = QBOHelper.CreateStatus(qboContextoAuth);
            //Adding the Status
            Status added = Helper.Add<Status>(qboContextoAuth, status);
            //Change the data of added entity
            Status changed = QBOHelper.UpdateStatusSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Status updated = Helper.Update<Status>(qboContextoAuth, changed);//Verify the updated Status
            QBOHelper.VerifyStatusSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void StatusDeleteTestUsingoAuth()
        {
            //Creating the Status for Adding
            Status status = QBOHelper.CreateStatus(qboContextoAuth);
            //Adding the Status
            Status added = Helper.Add<Status>(qboContextoAuth, status);
            //Delete the returned entity
            try
            {
                Status deleted = Helper.Delete<Status>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void StatusVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            Status entity = QBOHelper.CreateStatus(qboContextoAuth);
            //Adding the entity
            Status added = Helper.Add<Status>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                Status voided = Helper.Void<Status>(qboContextoAuth, added);
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
        public void StatusCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            StatusAddTestUsingoAuth();

            //Retrieving the Status using CDC
            List<Status> entities = Helper.CDC(qboContextoAuth, new Status(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<Status>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void StatusBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Status existing = Helper.FindOrAdd(qboContextoAuth, new Status());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateStatus(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateStatus(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Status");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Status>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Status).Id));
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
        public void StatusQueryUsingoAuth()
        {
            QueryService<Status> entityQuery = new QueryService<Status>(qboContextoAuth);
            Status existing = Helper.FindOrAdd<Status>(qboContextoAuth, new Status());
            List<Status> entities = entityQuery.ExecuteIdsQuery("Select * from Customer where Id == '"+existing.Id+"'").ToList();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void StatusAddAsyncTestsUsingoAuth()
        {
            //Creating the Status for Add
            Status entity = QBOHelper.CreateStatus(qboContextoAuth);

            Status added = Helper.AddAsync<Status>(qboContextoAuth, entity);
            QBOHelper.VerifyStatus(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void StatusRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            StatusAddTestUsingoAuth();

            //Retrieving the Status using FindAll
            Helper.FindAllAsync<Status>(qboContextoAuth, new Status());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void StatusFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Status for Adding
            Status entity = QBOHelper.CreateStatus(qboContextoAuth);
            //Adding the Status
            Status added = Helper.Add<Status>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Status>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void StatusUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Status for Adding
            Status entity = QBOHelper.CreateStatus(qboContextoAuth);
            //Adding the Status
            Status added = Helper.Add<Status>(qboContextoAuth, entity);

            //Update the Status
            Status updated = QBOHelper.UpdateStatus(qboContextoAuth, added);
            //Call the service
            Status updatedReturned = Helper.UpdateAsync<Status>(qboContextoAuth, updated);
            //Verify updated Status
            QBOHelper.VerifyStatus(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void StatusDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Status for Adding
            Status entity = QBOHelper.CreateStatus(qboContextoAuth);
            //Adding the Status
            Status added = Helper.Add<Status>(qboContextoAuth, entity);

            Helper.DeleteAsync<Status>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void StatusVoidAsyncTestsUsingoAuth()
        {
            //Creating the Status for Adding
            Status entity = QBOHelper.CreateStatus(qboContextoAuth);
            //Adding the Status
            Status added = Helper.Add<Status>(qboContextoAuth, entity);

            Helper.VoidAsync<Status>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
