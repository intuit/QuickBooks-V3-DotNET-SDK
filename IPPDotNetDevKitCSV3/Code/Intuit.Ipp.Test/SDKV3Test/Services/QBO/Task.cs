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
    public class TaskTest
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
        public void TaskAddTestUsingoAuth()
        {
            //Creating the Task for Add
            Task task = QBOHelper.CreateTask(qboContextoAuth);
            //Adding the Task
            Task added = Helper.Add<Task>(qboContextoAuth, task);
            //Verify the added Task
            QBOHelper.VerifyTask(task, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void TaskFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            TaskAddTestUsingoAuth();

            //Retrieving the Task using FindAll
            List<Task> tasks = Helper.FindAll<Task>(qboContextoAuth, new Task(), 1, 500);
            Assert.IsNotNull(tasks);
            Assert.IsTrue(tasks.Count<Task>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void TaskFindbyIdTestUsingoAuth()
        {
            //Creating the Task for Adding
            Task task = QBOHelper.CreateTask(qboContextoAuth);
            //Adding the Task
            Task added = Helper.Add<Task>(qboContextoAuth, task);
            Task found = Helper.FindById<Task>(qboContextoAuth, added);
            QBOHelper.VerifyTask(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void TaskUpdateTestUsingoAuth()
        {
            //Creating the Task for Adding
            Task task = QBOHelper.CreateTask(qboContextoAuth);
            //Adding the Task
            Task added = Helper.Add<Task>(qboContextoAuth, task);
            //Change the data of added entity
            Task changed = QBOHelper.UpdateTask(qboContextoAuth, added);
            //Update the returned entity data
            Task updated = Helper.Update<Task>(qboContextoAuth, changed);//Verify the updated Task
            QBOHelper.VerifyTask(changed, updated);
        }

        [TestMethod]
        public void TaskSparseUpdateTestUsingoAuth()
        {
            //Creating the Task for Adding
            Task task = QBOHelper.CreateTask(qboContextoAuth);
            //Adding the Task
            Task added = Helper.Add<Task>(qboContextoAuth, task);
            //Change the data of added entity
            Task changed = QBOHelper.UpdateTaskSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Task updated = Helper.Update<Task>(qboContextoAuth, changed);//Verify the updated Task
            QBOHelper.VerifyTaskSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void TaskDeleteTestUsingoAuth()
        {
            //Creating the Task for Adding
            Task task = QBOHelper.CreateTask(qboContextoAuth);
            //Adding the Task
            Task added = Helper.Add<Task>(qboContextoAuth, task);
            //Delete the returned entity
            try
            {
                Task deleted = Helper.Delete<Task>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void TaskVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            Task entity = QBOHelper.CreateTask(qboContextoAuth);
            //Adding the entity
            Task added = Helper.Add<Task>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                Task voided = Helper.Void<Task>(qboContextoAuth, added);
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
        public void TaskCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            TaskAddTestUsingoAuth();

            //Retrieving the Task using CDC
            List<Task> entities = Helper.CDC(qboContextoAuth, new Task(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<Task>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void TaskBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Task existing = Helper.FindOrAdd(qboContextoAuth, new Task());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateTask(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateTask(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Task");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Task>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Task).Id));
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
        public void TaskQueryUsingoAuth()
        {
            QueryService<Task> entityQuery = new QueryService<Task>(qboContextoAuth);
            Task existing = Helper.FindOrAdd<Task>(qboContextoAuth, new Task());
            List<Task> entities = entityQuery.ExecuteIdsQuery("Select * from Customer where Id =='"+ existing.Id+"'").ToList();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void TaskAddAsyncTestsUsingoAuth()
        {
            //Creating the Task for Add
            Task entity = QBOHelper.CreateTask(qboContextoAuth);

            Task added = Helper.AddAsync<Task>(qboContextoAuth, entity);
            QBOHelper.VerifyTask(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void TaskRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            TaskAddTestUsingoAuth();

            //Retrieving the Task using FindAll
            Helper.FindAllAsync<Task>(qboContextoAuth, new Task());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void TaskFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Task for Adding
            Task entity = QBOHelper.CreateTask(qboContextoAuth);
            //Adding the Task
            Task added = Helper.Add<Task>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Task>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void TaskUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Task for Adding
            Task entity = QBOHelper.CreateTask(qboContextoAuth);
            //Adding the Task
            Task added = Helper.Add<Task>(qboContextoAuth, entity);

            //Update the Task
            Task updated = QBOHelper.UpdateTask(qboContextoAuth, added);
            //Call the service
            Task updatedReturned = Helper.UpdateAsync<Task>(qboContextoAuth, updated);
            //Verify updated Task
            QBOHelper.VerifyTask(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void TaskDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Task for Adding
            Task entity = QBOHelper.CreateTask(qboContextoAuth);
            //Adding the Task
            Task added = Helper.Add<Task>(qboContextoAuth, entity);

            Helper.DeleteAsync<Task>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void TaskVoidAsyncTestsUsingoAuth()
        {
            //Creating the Task for Adding
            Task entity = QBOHelper.CreateTask(qboContextoAuth);
            //Adding the Task
            Task added = Helper.Add<Task>(qboContextoAuth, entity);

            Helper.VoidAsync<Task>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
