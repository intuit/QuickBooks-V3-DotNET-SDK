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
    public class JobTypeTest
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
        public void JobTypeAddTestUsingoAuth()
        {
            //Creating the JobType for Add
            JobType jobType = QBOHelper.CreateJobType(qboContextoAuth);
            //Adding the JobType
            JobType added = Helper.Add<JobType>(qboContextoAuth, jobType);
            //Verify the added JobType
            QBOHelper.VerifyJobType(jobType, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void JobTypeFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            JobTypeAddTestUsingoAuth();

            //Retrieving the JobType using FindAll
            List<JobType> jobTypes = Helper.FindAll<JobType>(qboContextoAuth, new JobType(), 1, 500);
            Assert.IsNotNull(jobTypes);
            Assert.IsTrue(jobTypes.Count<JobType>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void JobTypeFindbyIdTestUsingoAuth()
        {
            //Creating the JobType for Adding
            JobType jobType = QBOHelper.CreateJobType(qboContextoAuth);
            //Adding the JobType
            JobType added = Helper.Add<JobType>(qboContextoAuth, jobType);
            JobType found = Helper.FindById<JobType>(qboContextoAuth, added);
            QBOHelper.VerifyJobType(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void JobTypeUpdateTestUsingoAuth()
        {
            //Creating the JobType for Adding
            JobType jobType = QBOHelper.CreateJobType(qboContextoAuth);
            //Adding the JobType
            JobType added = Helper.Add<JobType>(qboContextoAuth, jobType);
            //Change the data of added entity
            JobType changed = QBOHelper.UpdateJobType(qboContextoAuth, added);
            //Update the returned entity data
            JobType updated = Helper.Update<JobType>(qboContextoAuth, changed);//Verify the updated JobType
            QBOHelper.VerifyJobType(changed, updated);
        }

        [TestMethod]
        public void JobTypeSparseUpdateTestUsingoAuth()
        {
            //Creating the JobType for Adding
            JobType jobType = QBOHelper.CreateJobType(qboContextoAuth);
            //Adding the JobType
            JobType added = Helper.Add<JobType>(qboContextoAuth, jobType);
            //Change the data of added entity
            JobType changed = QBOHelper.UpdateJobTypeSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            JobType updated = Helper.Update<JobType>(qboContextoAuth, changed);//Verify the updated JobType
            QBOHelper.VerifyJobTypeSparse(changed, updated);
        }


        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void JobTypeDeleteTestUsingoAuth()
        {
            //Creating the JobType for Adding
            JobType jobType = QBOHelper.CreateJobType(qboContextoAuth);
            //Adding the JobType
            JobType added = Helper.Add<JobType>(qboContextoAuth, jobType);
            //Delete the returned entity
            try
            {
                JobType deleted = Helper.Delete<JobType>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void JobTypeVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            JobType entity = QBOHelper.CreateJobType(qboContextoAuth);
            //Adding the entity
            JobType added = Helper.Add<JobType>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                JobType voided = Helper.Void<JobType>(qboContextoAuth, added);
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
        public void JobTypeCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            JobTypeAddTestUsingoAuth();

            //Retrieving the JobType using CDC
            List<JobType> entities = Helper.CDC(qboContextoAuth, new JobType(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<JobType>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void JobTypeBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            JobType existing = Helper.FindOrAdd(qboContextoAuth, new JobType());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateJobType(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateJobType(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from JobType");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<JobType>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as JobType).Id));
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
        public void JobTypeQueryUsingoAuth()
        {
            QueryService<JobType> entityQuery = new QueryService<JobType>(qboContextoAuth);
            JobType existing = Helper.FindOrAdd<JobType>(qboContextoAuth, new JobType());
            //List<JobType> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            int count = entityQuery.ExecuteIdsQuery("Select * from JobType where Id='" + existing.Id + "'").Count;
            Assert.IsTrue(count > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void JobTypeAddAsyncTestsUsingoAuth()
        {
            //Creating the JobType for Add
            JobType entity = QBOHelper.CreateJobType(qboContextoAuth);

            JobType added = Helper.AddAsync<JobType>(qboContextoAuth, entity);
            QBOHelper.VerifyJobType(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void JobTypeRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            JobTypeAddTestUsingoAuth();

            //Retrieving the JobType using FindAll
            Helper.FindAllAsync<JobType>(qboContextoAuth, new JobType());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void JobTypeFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the JobType for Adding
            JobType entity = QBOHelper.CreateJobType(qboContextoAuth);
            //Adding the JobType
            JobType added = Helper.Add<JobType>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<JobType>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void JobTypeUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the JobType for Adding
            JobType entity = QBOHelper.CreateJobType(qboContextoAuth);
            //Adding the JobType
            JobType added = Helper.Add<JobType>(qboContextoAuth, entity);

            //Update the JobType
            JobType updated = QBOHelper.UpdateJobType(qboContextoAuth, added);
            //Call the service
            JobType updatedReturned = Helper.UpdateAsync<JobType>(qboContextoAuth, updated);
            //Verify updated JobType
            QBOHelper.VerifyJobType(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void JobTypeDeleteAsyncTestsUsingoAuth()
        {
            //Creating the JobType for Adding
            JobType entity = QBOHelper.CreateJobType(qboContextoAuth);
            //Adding the JobType
            JobType added = Helper.Add<JobType>(qboContextoAuth, entity);

            Helper.DeleteAsync<JobType>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void JobTypeVoidAsyncTestsUsingoAuth()
        {
            //Creating the JobType for Adding
            JobType entity = QBOHelper.CreateJobType(qboContextoAuth);
            //Adding the JobType
            JobType added = Helper.Add<JobType>(qboContextoAuth, entity);

            Helper.VoidAsync<JobType>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
