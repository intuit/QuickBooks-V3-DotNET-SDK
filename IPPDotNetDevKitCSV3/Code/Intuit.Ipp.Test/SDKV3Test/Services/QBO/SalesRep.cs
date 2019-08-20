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
    public class SalesRepTest
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
        public void SalesRepAddTestUsingoAuth()
        {
            //Creating the SalesRep for Add
            SalesRep salesRep = QBOHelper.CreateSalesRep(qboContextoAuth);
            //Adding the SalesRep
            SalesRep added = Helper.Add<SalesRep>(qboContextoAuth, salesRep);
            //Verify the added SalesRep
            QBOHelper.VerifySalesRep(salesRep, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void SalesRepFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            SalesRepAddTestUsingoAuth();

            //Retrieving the SalesRep using FindAll
            List<SalesRep> salesReps = Helper.FindAll<SalesRep>(qboContextoAuth, new SalesRep(), 1, 500);
            Assert.IsNotNull(salesReps);
            Assert.IsTrue(salesReps.Count<SalesRep>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void SalesRepFindbyIdTestUsingoAuth()
        {
            //Creating the SalesRep for Adding
            SalesRep salesRep = QBOHelper.CreateSalesRep(qboContextoAuth);
            //Adding the SalesRep
            SalesRep added = Helper.Add<SalesRep>(qboContextoAuth, salesRep);
            SalesRep found = Helper.FindById<SalesRep>(qboContextoAuth, added);
            QBOHelper.VerifySalesRep(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void SalesRepUpdateTestUsingoAuth()
        {
            //Creating the SalesRep for Adding
            SalesRep salesRep = QBOHelper.CreateSalesRep(qboContextoAuth);
            //Adding the SalesRep
            SalesRep added = Helper.Add<SalesRep>(qboContextoAuth, salesRep);
            //Change the data of added entity
            SalesRep changed = QBOHelper.UpdateSalesRep(qboContextoAuth, added);
            //Update the returned entity data
            SalesRep updated = Helper.Update<SalesRep>(qboContextoAuth, changed);//Verify the updated SalesRep
            QBOHelper.VerifySalesRep(changed, updated);
        }


        [TestMethod]
        public void SalesRepSparseUpdateTestUsingoAuth()
        {
            //Creating the SalesRep for Adding
            SalesRep salesRep = QBOHelper.CreateSalesRep(qboContextoAuth);
            //Adding the SalesRep
            SalesRep added = Helper.Add<SalesRep>(qboContextoAuth, salesRep);
            //Change the data of added entity
            SalesRep changed = QBOHelper.UpdateSalesRepSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            SalesRep updated = Helper.Update<SalesRep>(qboContextoAuth, changed);//Verify the updated SalesRep
            QBOHelper.VerifySalesRepSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void SalesRepDeleteTestUsingoAuth()
        {
            //Creating the SalesRep for Adding
            SalesRep salesRep = QBOHelper.CreateSalesRep(qboContextoAuth);
            //Adding the SalesRep
            SalesRep added = Helper.Add<SalesRep>(qboContextoAuth, salesRep);
            //Delete the returned entity
            try
            {
                SalesRep deleted = Helper.Delete<SalesRep>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SalesRepVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            SalesRep entity = QBOHelper.CreateSalesRep(qboContextoAuth);
            //Adding the entity
            SalesRep added = Helper.Add<SalesRep>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                SalesRep voided = Helper.Void<SalesRep>(qboContextoAuth, added);
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
        public void SalesRepCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            SalesRepAddTestUsingoAuth();

            //Retrieving the SalesRep using CDC
            List<SalesRep> entities = Helper.CDC(qboContextoAuth, new SalesRep(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<SalesRep>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void SalesRepBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            SalesRep existing = Helper.FindOrAdd(qboContextoAuth, new SalesRep());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateSalesRep(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateSalesRep(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from SalesRep");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<SalesRep>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as SalesRep).Id));
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
        public void SalesRepQueryUsingoAuth()
        {
            QueryService<SalesRep> entityQuery = new QueryService<SalesRep>(qboContextoAuth);
            SalesRep existing = Helper.FindOrAdd<SalesRep>(qboContextoAuth, new SalesRep());
            List<SalesRep> entities = entityQuery.ExecuteIdsQuery("Select * from Customer where Id == "+existing.Id+"'").ToList();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void SalesRepAddAsyncTestsUsingoAuth()
        {
            //Creating the SalesRep for Add
            SalesRep entity = QBOHelper.CreateSalesRep(qboContextoAuth);

            SalesRep added = Helper.AddAsync<SalesRep>(qboContextoAuth, entity);
            QBOHelper.VerifySalesRep(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void SalesRepRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            SalesRepAddTestUsingoAuth();

            //Retrieving the SalesRep using FindAll
            Helper.FindAllAsync<SalesRep>(qboContextoAuth, new SalesRep());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void SalesRepFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the SalesRep for Adding
            SalesRep entity = QBOHelper.CreateSalesRep(qboContextoAuth);
            //Adding the SalesRep
            SalesRep added = Helper.Add<SalesRep>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<SalesRep>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void SalesRepUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the SalesRep for Adding
            SalesRep entity = QBOHelper.CreateSalesRep(qboContextoAuth);
            //Adding the SalesRep
            SalesRep added = Helper.Add<SalesRep>(qboContextoAuth, entity);

            //Update the SalesRep
            SalesRep updated = QBOHelper.UpdateSalesRep(qboContextoAuth, added);
            //Call the service
            SalesRep updatedReturned = Helper.UpdateAsync<SalesRep>(qboContextoAuth, updated);
            //Verify updated SalesRep
            QBOHelper.VerifySalesRep(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void SalesRepDeleteAsyncTestsUsingoAuth()
        {
            //Creating the SalesRep for Adding
            SalesRep entity = QBOHelper.CreateSalesRep(qboContextoAuth);
            //Adding the SalesRep
            SalesRep added = Helper.Add<SalesRep>(qboContextoAuth, entity);

            Helper.DeleteAsync<SalesRep>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void SalesRepVoidAsyncTestsUsingoAuth()
        {
            //Creating the SalesRep for Adding
            SalesRep entity = QBOHelper.CreateSalesRep(qboContextoAuth);
            //Adding the SalesRep
            SalesRep added = Helper.Add<SalesRep>(qboContextoAuth, entity);

            Helper.VoidAsync<SalesRep>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
