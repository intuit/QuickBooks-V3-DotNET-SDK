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
    public class SyncActivityTest
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
        public void SyncActivityAddTestUsingoAuth()
        {
            //Creating the SyncActivity for Add
            SyncActivity syncActivity = QBOHelper.CreateSyncActivity(qboContextoAuth);
            //Adding the SyncActivity
            SyncActivity added = Helper.Add<SyncActivity>(qboContextoAuth, syncActivity);
            //Verify the added SyncActivity
            QBOHelper.VerifySyncActivity(syncActivity, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void SyncActivityFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            SyncActivityAddTestUsingoAuth();

            //Retrieving the SyncActivity using FindAll
            List<SyncActivity> syncActivitys = Helper.FindAll<SyncActivity>(qboContextoAuth, new SyncActivity(), 1, 500);
            Assert.IsNotNull(syncActivitys);
            Assert.IsTrue(syncActivitys.Count<SyncActivity>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void SyncActivityFindbyIdTestUsingoAuth()
        {
            //Creating the SyncActivity for Adding
            SyncActivity syncActivity = QBOHelper.CreateSyncActivity(qboContextoAuth);
            //Adding the SyncActivity
            SyncActivity added = Helper.Add<SyncActivity>(qboContextoAuth, syncActivity);
            SyncActivity found = Helper.FindById<SyncActivity>(qboContextoAuth, added);
            QBOHelper.VerifySyncActivity(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void SyncActivityUpdateTestUsingoAuth()
        {
            //Creating the SyncActivity for Adding
            SyncActivity syncActivity = QBOHelper.CreateSyncActivity(qboContextoAuth);
            //Adding the SyncActivity
            SyncActivity added = Helper.Add<SyncActivity>(qboContextoAuth, syncActivity);
            //Change the data of added entity
            SyncActivity changed = QBOHelper.UpdateSyncActivity(qboContextoAuth, added);
            //Update the returned entity data
            SyncActivity updated = Helper.Update<SyncActivity>(qboContextoAuth, changed);//Verify the updated SyncActivity
            QBOHelper.VerifySyncActivity(changed, updated);
        }

        [TestMethod]
        public void SyncActivitySparseUpdateTestUsingoAuth()
        {
            //Creating the SyncActivity for Adding
            SyncActivity syncActivity = QBOHelper.CreateSyncActivity(qboContextoAuth);
            //Adding the SyncActivity
            SyncActivity added = Helper.Add<SyncActivity>(qboContextoAuth, syncActivity);
            //Change the data of added entity
            SyncActivity changed = QBOHelper.UpdateSyncActivitySparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            SyncActivity updated = Helper.Update<SyncActivity>(qboContextoAuth, changed);//Verify the updated SyncActivity
            QBOHelper.VerifySyncActivitySparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void SyncActivityDeleteTestUsingoAuth()
        {
            //Creating the SyncActivity for Adding
            SyncActivity syncActivity = QBOHelper.CreateSyncActivity(qboContextoAuth);
            //Adding the SyncActivity
            SyncActivity added = Helper.Add<SyncActivity>(qboContextoAuth, syncActivity);
            //Delete the returned entity
            try
            {
                SyncActivity deleted = Helper.Delete<SyncActivity>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SyncActivityVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            SyncActivity entity = QBOHelper.CreateSyncActivity(qboContextoAuth);
            //Adding the entity
            SyncActivity added = Helper.Add<SyncActivity>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                SyncActivity voided = Helper.Void<SyncActivity>(qboContextoAuth, added);
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
        public void SyncActivityCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            SyncActivityAddTestUsingoAuth();

            //Retrieving the SyncActivity using CDC
            List<SyncActivity> entities = Helper.CDC(qboContextoAuth, new SyncActivity(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<SyncActivity>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void SyncActivityBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            SyncActivity existing = Helper.FindOrAdd(qboContextoAuth, new SyncActivity());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateSyncActivity(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateSyncActivity(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from SyncActivity");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<SyncActivity>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as SyncActivity).Id));
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
        public void SyncActivityQueryUsingoAuth()
        {
            QueryService<SyncActivity> entityQuery = new QueryService<SyncActivity>(qboContextoAuth);
            SyncActivity existing = Helper.FindOrAdd<SyncActivity>(qboContextoAuth, new SyncActivity());
            List<SyncActivity> entities = entityQuery.ExecuteIdsQuery("Select * from Customer where Id='"+ existing.Id+"'").ToList();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void SyncActivityAddAsyncTestsUsingoAuth()
        {
            //Creating the SyncActivity for Add
            SyncActivity entity = QBOHelper.CreateSyncActivity(qboContextoAuth);

            SyncActivity added = Helper.AddAsync<SyncActivity>(qboContextoAuth, entity);
            QBOHelper.VerifySyncActivity(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void SyncActivityRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            SyncActivityAddTestUsingoAuth();

            //Retrieving the SyncActivity using FindAll
            Helper.FindAllAsync<SyncActivity>(qboContextoAuth, new SyncActivity());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void SyncActivityFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the SyncActivity for Adding
            SyncActivity entity = QBOHelper.CreateSyncActivity(qboContextoAuth);
            //Adding the SyncActivity
            SyncActivity added = Helper.Add<SyncActivity>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<SyncActivity>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void SyncActivityUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the SyncActivity for Adding
            SyncActivity entity = QBOHelper.CreateSyncActivity(qboContextoAuth);
            //Adding the SyncActivity
            SyncActivity added = Helper.Add<SyncActivity>(qboContextoAuth, entity);

            //Update the SyncActivity
            SyncActivity updated = QBOHelper.UpdateSyncActivity(qboContextoAuth, added);
            //Call the service
            SyncActivity updatedReturned = Helper.UpdateAsync<SyncActivity>(qboContextoAuth, updated);
            //Verify updated SyncActivity
            QBOHelper.VerifySyncActivity(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void SyncActivityDeleteAsyncTestsUsingoAuth()
        {
            //Creating the SyncActivity for Adding
            SyncActivity entity = QBOHelper.CreateSyncActivity(qboContextoAuth);
            //Adding the SyncActivity
            SyncActivity added = Helper.Add<SyncActivity>(qboContextoAuth, entity);

            Helper.DeleteAsync<SyncActivity>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void SyncActivityVoidAsyncTestsUsingoAuth()
        {
            //Creating the SyncActivity for Adding
            SyncActivity entity = QBOHelper.CreateSyncActivity(qboContextoAuth);
            //Adding the SyncActivity
            SyncActivity added = Helper.Add<SyncActivity>(qboContextoAuth, entity);

            Helper.VoidAsync<SyncActivity>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
