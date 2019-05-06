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
    public class TimeActivityTest
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
        public void TimeActivityAddTestUsingoAuth()
        {
            //Creating the TimeActivity for Add
            TimeActivity timeActivity = QBOHelper.CreateTimeActivity(qboContextoAuth);
            //Adding the TimeActivity
            TimeActivity added = Helper.Add<TimeActivity>(qboContextoAuth, timeActivity);
            //Verify the added TimeActivity
            QBOHelper.VerifyTimeActivity(timeActivity, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void TimeActivityFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            TimeActivityAddTestUsingoAuth();

            //Retrieving the TimeActivity using FindAll
            List<TimeActivity> timeActivitys = Helper.FindAll<TimeActivity>(qboContextoAuth, new TimeActivity(), 1, 500);
            Assert.IsNotNull(timeActivitys);
            Assert.IsTrue(timeActivitys.Count<TimeActivity>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void TimeActivityFindbyIdTestUsingoAuth()
        {
            //Creating the TimeActivity for Adding
            TimeActivity timeActivity = QBOHelper.CreateTimeActivity(qboContextoAuth);
            //Adding the TimeActivity
            TimeActivity added = Helper.FindOrAdd(qboContextoAuth, new TimeActivity());
            TimeActivity found = Helper.FindById<TimeActivity>(qboContextoAuth, added);
            QBOHelper.VerifyTimeActivity(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void TimeActivityUpdateTestUsingoAuth()
        {
            /*
            //Creating the TimeActivity for Adding
            TimeActivity timeActivity = QBOHelper.CreateTimeActivity(qboContextoAuth);
            //Adding the TimeActivity
            TimeActivity added = Helper.Add<TimeActivity>(qboContextoAuth, timeActivity);
            */

            TimeActivity added = Helper.FindOrAdd(qboContextoAuth, new TimeActivity());
            //Change the data of added entity
            TimeActivity changed = QBOHelper.UpdateTimeActivity(qboContextoAuth, added);
            //Update the returned entity data
            TimeActivity updated = Helper.Update<TimeActivity>(qboContextoAuth, changed);//Verify the updated TimeActivity
            QBOHelper.VerifyTimeActivity(changed, updated);
        }

        [TestMethod]
        public void TimeActivitySparseUpdateTestUsingoAuth()
        {
            /*
            //Creating the TimeActivity for Adding
            TimeActivity timeActivity = QBOHelper.CreateTimeActivity(qboContextoAuth);
            //Adding the TimeActivity
            TimeActivity added = Helper.Add<TimeActivity>(qboContextoAuth, timeActivity);
            */

            TimeActivity added = Helper.FindOrAdd(qboContextoAuth, new TimeActivity());
            //Change the data of added entity
            TimeActivity changed = QBOHelper.UpdateTimeActivitySparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            TimeActivity updated = Helper.Update<TimeActivity>(qboContextoAuth, changed);//Verify the updated TimeActivity
            QBOHelper.VerifyTimeActivitySparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void TimeActivityDeleteTestUsingoAuth()
        {
            //Creating the TimeActivity for Adding
            //TimeActivity timeActivity = QBOHelper.CreateTimeActivity(qboContextoAuth);
            //Adding the TimeActivity
            //TimeActivity added = Helper.Add<TimeActivity>(qboContextoAuth, timeActivity);
            TimeActivity added = Helper.FindOrAdd(qboContextoAuth, new TimeActivity());

            //Delete the returned entity
            try
            {
                TimeActivity deleted = Helper.Delete<TimeActivity>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod][Ignore]
        public void TimeActivityVoidTestUsingoAuth()
        {
            TimeActivity added = Helper.FindOrAdd(qboContextoAuth, new TimeActivity());

            //Delete the returned entity
            try
            {
                TimeActivity voided = Helper.Void<TimeActivity>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Voided, voided.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        #endregion

        #region Test cases for CDC Operations

        [TestMethod][Ignore]//Jira- https://jira.intuit.com/browse/ipp-3295
        public void TimeActivityCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            //TimeActivityAddTestUsingoAuth();

            //Retrieving the TimeActivity using CDC
            List<TimeActivity> entities = Helper.CDC(qboContextoAuth, new TimeActivity(), DateTime.Today.AddDays(-100));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<TimeActivity>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void TimeActivityBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            TimeActivity existing = Helper.FindOrAdd(qboContextoAuth, new TimeActivity());

          //  batchEntries.Add(OperationEnum.create, QBOHelper.CreateTimeActivity(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateTimeActivity(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from TimeActivity");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<TimeActivity>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as TimeActivity).Id));
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
        public void TimeActivityQueryUsingoAuth()
        {
            QueryService<TimeActivity> entityQuery = new QueryService<TimeActivity>(qboContextoAuth);
            TimeActivity existing = Helper.FindOrAdd<TimeActivity>(qboContextoAuth, new TimeActivity());
            //List<TimeActivity> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<TimeActivity> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM TimeActivity where Id='" + existing.Id + "'").ToList<TimeActivity>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]  
        public void TimeActivityAddAsyncTestsUsingoAuth()
        {
            //Creating the TimeActivity for Add
            TimeActivity entity = QBOHelper.CreateTimeActivity(qboContextoAuth);

            TimeActivity added = Helper.AddAsync<TimeActivity>(qboContextoAuth, entity);
            QBOHelper.VerifyTimeActivity(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void TimeActivityRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            //TimeActivityAddTestUsingoAuth();

            //Retrieving the TimeActivity using FindAll
            Helper.FindAllAsync<TimeActivity>(qboContextoAuth, new TimeActivity());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void TimeActivityFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the TimeActivity for Adding
            //TimeActivity entity = QBOHelper.CreateTimeActivity(qboContextoAuth);
            //Adding the TimeActivity
            //TimeActivity added = Helper.Add<TimeActivity>(qboContextoAuth, entity);

            TimeActivity added = Helper.FindOrAdd(qboContextoAuth, new TimeActivity());
            //FindById and verify
            Helper.FindByIdAsync<TimeActivity>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void TimeActivityUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the TimeActivity for Adding
            //TimeActivity entity = QBOHelper.CreateTimeActivity(qboContextoAuth);
            //Adding the TimeActivity
            //TimeActivity added = Helper.Add<TimeActivity>(qboContextoAuth, entity);

            TimeActivity added = Helper.FindOrAdd(qboContextoAuth, new TimeActivity());
            //Update the TimeActivity
            TimeActivity updated = QBOHelper.UpdateTimeActivity(qboContextoAuth, added);
            //Call the service
            TimeActivity updatedReturned = Helper.UpdateAsync<TimeActivity>(qboContextoAuth, updated);
            //Verify updated TimeActivity
            QBOHelper.VerifyTimeActivity(updated, updatedReturned);
        }

        [TestMethod]
        public void TimeActivitySparseUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the TimeActivity for Adding
            //TimeActivity entity = QBOHelper.CreateTimeActivity(qboContextoAuth);
            //Adding the TimeActivity
            //TimeActivity added = Helper.Add<TimeActivity>(qboContextoAuth, entity);

            TimeActivity added = Helper.FindOrAdd(qboContextoAuth, new TimeActivity());
            //Update the TimeActivity
            TimeActivity updated = QBOHelper.UpdateTimeActivitySparse(qboContextoAuth, added.Id, added.SyncToken);
            //Call the service
            TimeActivity updatedReturned = Helper.UpdateAsync<TimeActivity>(qboContextoAuth, updated);
            //Verify updated TimeActivity
            QBOHelper.VerifyTimeActivitySparse(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void TimeActivityDeleteAsyncTestsUsingoAuth()
        {
            //Creating the TimeActivity for Adding
            //TimeActivity entity = QBOHelper.CreateTimeActivity(qboContextoAuth);
            //Adding the TimeActivity
            //TimeActivity added = Helper.Add<TimeActivity>(qboContextoAuth, entity);

            TimeActivity added = Helper.FindOrAdd(qboContextoAuth, new TimeActivity());

            Helper.DeleteAsync<TimeActivity>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod][Ignore]
        public void TimeActivityVoidAsyncTestsUsingoAuth()
        {
            //Creating the TimeActivity for Adding
            //TimeActivity entity = QBOHelper.CreateTimeActivity(qboContextoAuth);
            //Adding the TimeActivity
            //TimeActivity added = Helper.Add<TimeActivity>(qboContextoAuth, entity);

            TimeActivity added = Helper.FindOrAdd(qboContextoAuth, new TimeActivity());

            Helper.VoidAsync<TimeActivity>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
