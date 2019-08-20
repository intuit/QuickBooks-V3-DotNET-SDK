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
    public class EstimateTest
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
        public void EstimateAddTestUsingoAuth()
        {
            //Creating the Estimate for Add
            Estimate estimate = QBOHelper.CreateEstimate(qboContextoAuth);
            //Adding the Estimate
            Estimate added = Helper.Add<Estimate>(qboContextoAuth, estimate);
            //Verify the added Estimate
            QBOHelper.VerifyEstimate(estimate, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void EstimateFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            EstimateAddTestUsingoAuth();

            //Retrieving the Estimate using FindAll
            List<Estimate> estimates = Helper.FindAll<Estimate>(qboContextoAuth, new Estimate(), 1, 500);
            Assert.IsNotNull(estimates);
            Assert.IsTrue(estimates.Count<Estimate>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void EstimateFindbyIdTestUsingoAuth()
        {
            //Creating the Estimate for Adding
            Estimate estimate = QBOHelper.CreateEstimate(qboContextoAuth);
            //Adding the Estimate
            Estimate added = Helper.Add<Estimate>(qboContextoAuth, estimate);
            Estimate found = Helper.FindById<Estimate>(qboContextoAuth, added);
            QBOHelper.VerifyEstimate(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void EstimateUpdateTestUsingoAuth()
        {
            //Creating the Estimate for Adding
            Estimate estimate = QBOHelper.CreateEstimate(qboContextoAuth);
            //Adding the Estimate
            Estimate added = Helper.Add<Estimate>(qboContextoAuth, estimate);
            //Change the data of added entity
            Estimate changed = QBOHelper.UpdateEstimate(qboContextoAuth, added);
            //Update the returned entity data
            Estimate updated = Helper.Update<Estimate>(qboContextoAuth, changed);//Verify the updated Estimate
            QBOHelper.VerifyEstimate(changed, updated);
        }

        [TestMethod]
        public void EstimateSparseUpdateTestUsingoAuth()
        {
            //Creating the Estimate for Adding
            Estimate estimate = QBOHelper.CreateEstimate(qboContextoAuth);
            //Adding the Estimate
            Estimate added = Helper.Add<Estimate>(qboContextoAuth, estimate);
            //Change the data of added entity
            Estimate changed = QBOHelper.SparseUpdateEstimate(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Estimate updated = Helper.Update<Estimate>(qboContextoAuth, changed);//Verify the updated Estimate
            QBOHelper.VerifyEstimateSparseUpdate(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void EstimateDeleteTestUsingoAuth()
        {
            //Creating the Estimate for Adding
            Estimate estimate = QBOHelper.CreateEstimate(qboContextoAuth);
            //Adding the Estimate
            Estimate added = Helper.Add<Estimate>(qboContextoAuth, estimate);
            //Delete the returned entity
            try
            {
                Estimate deleted = Helper.Delete<Estimate>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod] [Ignore]
        public void EstimateVoidTestUsingoAuth()
        {
            //Creating the Estimate for Adding
            Estimate estimate = QBOHelper.CreateEstimate(qboContextoAuth);
            //Adding the Estimate
            Estimate added = Helper.Add<Estimate>(qboContextoAuth, estimate);
            //Void the returned entity
            try
            {
                Estimate voided = Helper.Void<Estimate>(qboContextoAuth, added);
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
        public void EstimateCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            EstimateAddTestUsingoAuth();

            //Retrieving the Estimate using FindAll
            List<Estimate> estimates = Helper.CDC(qboContextoAuth, new Estimate(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(estimates);
            Assert.IsTrue(estimates.Count<Estimate>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void EstimateBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Estimate existing = Helper.FindOrAdd(qboContextoAuth, new Estimate());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateEstimate(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateEstimate(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Estimate");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Estimate>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Estimate).Id));
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
        public void EstimateQueryUsingoAuth()
        {
            QueryService<Estimate> entityQuery = new QueryService<Estimate>(qboContextoAuth);
            Estimate existing = Helper.FindOrAdd<Estimate>(qboContextoAuth, new Estimate());
            //List<Estimate> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<Estimate> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Estimate where Id='" + existing.Id + "'").ToList<Estimate>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void EstimateAddAsyncTestsUsingoAuth()
        {
            //Creating the Estimate for Add
            Estimate entity = QBOHelper.CreateEstimate(qboContextoAuth);

            Estimate added = Helper.AddAsync<Estimate>(qboContextoAuth, entity);
            QBOHelper.VerifyEstimate(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void EstimateRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            EstimateAddTestUsingoAuth();

            //Retrieving the Estimate using FindAll
            Helper.FindAllAsync<Estimate>(qboContextoAuth, new Estimate());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void EstimateFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Estimate for Adding
            Estimate entity = QBOHelper.CreateEstimate(qboContextoAuth);
            //Adding the Estimate
            Estimate added = Helper.Add<Estimate>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Estimate>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void EstimateUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Estimate for Adding
            Estimate entity = QBOHelper.CreateEstimate(qboContextoAuth);
            //Adding the Estimate
            Estimate added = Helper.Add<Estimate>(qboContextoAuth, entity);

            //Update the Estimate
            Estimate updated = QBOHelper.UpdateEstimate(qboContextoAuth, added);
            //Call the service
            Estimate updatedReturned = Helper.UpdateAsync<Estimate>(qboContextoAuth, updated);
            //Verify updated Estimate
            QBOHelper.VerifyEstimate(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void EstimateDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Estimate for Adding
            Estimate entity = QBOHelper.CreateEstimate(qboContextoAuth);
            //Adding the Estimate
            Estimate added = Helper.Add<Estimate>(qboContextoAuth, entity);

            Helper.DeleteAsync<Estimate>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void EstimateVoidAsyncTestsUsingoAuth()
        {
            //Creating the Estimate for Adding
            Estimate entity = QBOHelper.CreateEstimate(qboContextoAuth);
            //Adding the Estimate
            Estimate added = Helper.Add<Estimate>(qboContextoAuth, entity);

            Helper.VoidAsync<Estimate>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
