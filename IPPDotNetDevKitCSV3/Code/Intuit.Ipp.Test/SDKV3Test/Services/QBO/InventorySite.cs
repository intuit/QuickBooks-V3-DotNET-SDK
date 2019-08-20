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
    public class InventorySiteTest
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
        public void InventorySiteAddTestUsingoAuth()
        {
            //Creating the InventorySite for Add
            InventorySite inventorySite = QBOHelper.CreateInventorySite(qboContextoAuth);
            //Adding the InventorySite
            InventorySite added = Helper.Add<InventorySite>(qboContextoAuth, inventorySite);
            //Verify the added InventorySite
            QBOHelper.VerifyInventorySite(inventorySite, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void InventorySiteFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            InventorySiteAddTestUsingoAuth();

            //Retrieving the InventorySite using FindAll
            List<InventorySite> inventorySites = Helper.FindAll<InventorySite>(qboContextoAuth, new InventorySite(), 1, 500);
            Assert.IsNotNull(inventorySites);
            Assert.IsTrue(inventorySites.Count<InventorySite>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void InventorySiteFindbyIdTestUsingoAuth()
        {
            //Creating the InventorySite for Adding
            InventorySite inventorySite = QBOHelper.CreateInventorySite(qboContextoAuth);
            //Adding the InventorySite
            InventorySite added = Helper.Add<InventorySite>(qboContextoAuth, inventorySite);
            InventorySite found = Helper.FindById<InventorySite>(qboContextoAuth, added);
            QBOHelper.VerifyInventorySite(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void InventorySiteUpdateTestUsingoAuth()
        {
            //Creating the InventorySite for Adding
            InventorySite inventorySite = QBOHelper.CreateInventorySite(qboContextoAuth);
            //Adding the InventorySite
            InventorySite added = Helper.Add<InventorySite>(qboContextoAuth, inventorySite);
            //Change the data of added entity
            InventorySite changed = QBOHelper.UpdateInventorySite(qboContextoAuth, added);
            //Update the returned entity data
            InventorySite updated = Helper.Update<InventorySite>(qboContextoAuth, changed);//Verify the updated InventorySite
            QBOHelper.VerifyInventorySite(changed, updated);
        }

        [TestMethod]
        public void InventorySiteSparseUpdateTestUsingoAuth()
        {
            //Creating the InventorySite for Adding
            InventorySite inventorySite = QBOHelper.CreateInventorySite(qboContextoAuth);
            //Adding the InventorySite
            InventorySite added = Helper.Add<InventorySite>(qboContextoAuth, inventorySite);
            //Change the data of added entity
            InventorySite changed = QBOHelper.UpdateInventorySiteSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            InventorySite updated = Helper.Update<InventorySite>(qboContextoAuth, changed);//Verify the updated InventorySite
            QBOHelper.VerifyInventorySiteSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void InventorySiteDeleteTestUsingoAuth()
        {
            //Creating the InventorySite for Adding
            InventorySite inventorySite = QBOHelper.CreateInventorySite(qboContextoAuth);
            //Adding the InventorySite
            InventorySite added = Helper.Add<InventorySite>(qboContextoAuth, inventorySite);
            //Delete the returned entity
            try
            {
                InventorySite deleted = Helper.Delete<InventorySite>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void InventorySiteVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            InventorySite entity = QBOHelper.CreateInventorySite(qboContextoAuth);
            //Adding the entity
            InventorySite added = Helper.Add<InventorySite>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                InventorySite voided = Helper.Void<InventorySite>(qboContextoAuth, added);
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
        public void InventorySiteCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            InventorySiteAddTestUsingoAuth();

            //Retrieving the InventorySite using CDC
            List<InventorySite> entities = Helper.CDC(qboContextoAuth, new InventorySite(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<InventorySite>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void InventorySiteBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            InventorySite existing = Helper.FindOrAdd(qboContextoAuth, new InventorySite());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateInventorySite(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateInventorySite(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from InventorySite");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<InventorySite>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as InventorySite).Id));
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
        public void InventorySiteQueryUsingoAuth()
        {
            QueryService<InventorySite> entityQuery = new QueryService<InventorySite>(qboContextoAuth);
            InventorySite existing = Helper.FindOrAdd<InventorySite>(qboContextoAuth, new InventorySite());
            //List<InventorySite> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            int count = entityQuery.ExecuteIdsQuery("Select * from InventorySite where Id='" + existing.Id + "'").Count;
            Assert.IsTrue(count > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void InventorySiteAddAsyncTestsUsingoAuth()
        {
            //Creating the InventorySite for Add
            InventorySite entity = QBOHelper.CreateInventorySite(qboContextoAuth);

            InventorySite added = Helper.AddAsync<InventorySite>(qboContextoAuth, entity);
            QBOHelper.VerifyInventorySite(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void InventorySiteRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            InventorySiteAddTestUsingoAuth();

            //Retrieving the InventorySite using FindAll
            Helper.FindAllAsync<InventorySite>(qboContextoAuth, new InventorySite());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void InventorySiteFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the InventorySite for Adding
            InventorySite entity = QBOHelper.CreateInventorySite(qboContextoAuth);
            //Adding the InventorySite
            InventorySite added = Helper.Add<InventorySite>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<InventorySite>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void InventorySiteUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the InventorySite for Adding
            InventorySite entity = QBOHelper.CreateInventorySite(qboContextoAuth);
            //Adding the InventorySite
            InventorySite added = Helper.Add<InventorySite>(qboContextoAuth, entity);

            //Update the InventorySite
            InventorySite updated = QBOHelper.UpdateInventorySite(qboContextoAuth, added);
            //Call the service
            InventorySite updatedReturned = Helper.UpdateAsync<InventorySite>(qboContextoAuth, updated);
            //Verify updated InventorySite
            QBOHelper.VerifyInventorySite(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void InventorySiteDeleteAsyncTestsUsingoAuth()
        {
            //Creating the InventorySite for Adding
            InventorySite entity = QBOHelper.CreateInventorySite(qboContextoAuth);
            //Adding the InventorySite
            InventorySite added = Helper.Add<InventorySite>(qboContextoAuth, entity);

            Helper.DeleteAsync<InventorySite>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void InventorySiteVoidAsyncTestsUsingoAuth()
        {
            //Creating the InventorySite for Adding
            InventorySite entity = QBOHelper.CreateInventorySite(qboContextoAuth);
            //Adding the InventorySite
            InventorySite added = Helper.Add<InventorySite>(qboContextoAuth, entity);

            Helper.VoidAsync<InventorySite>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
