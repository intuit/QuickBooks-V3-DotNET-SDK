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
    public class FixedAssetTest
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
        public void FixedAssetAddTestUsingoAuth()
        {
            //Creating the FixedAsset for Add
            FixedAsset fixedAsset = QBOHelper.CreateFixedAsset(qboContextoAuth);
            //Adding the FixedAsset
            FixedAsset added = Helper.Add<FixedAsset>(qboContextoAuth, fixedAsset);
            //Verify the added FixedAsset
            QBOHelper.VerifyFixedAsset(fixedAsset, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void FixedAssetFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            FixedAssetAddTestUsingoAuth();

            //Retrieving the FixedAsset using FindAll
            List<FixedAsset> fixedAssets = Helper.FindAll<FixedAsset>(qboContextoAuth, new FixedAsset(), 1, 500);
            Assert.IsNotNull(fixedAssets);
            Assert.IsTrue(fixedAssets.Count<FixedAsset>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void FixedAssetFindbyIdTestUsingoAuth()
        {
            //Creating the FixedAsset for Adding
            FixedAsset fixedAsset = QBOHelper.CreateFixedAsset(qboContextoAuth);
            //Adding the FixedAsset
            FixedAsset added = Helper.Add<FixedAsset>(qboContextoAuth, fixedAsset);
            FixedAsset found = Helper.FindById<FixedAsset>(qboContextoAuth, added);
            QBOHelper.VerifyFixedAsset(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void FixedAssetUpdateTestUsingoAuth()
        {
            //Creating the FixedAsset for Adding
            FixedAsset fixedAsset = QBOHelper.CreateFixedAsset(qboContextoAuth);
            //Adding the FixedAsset
            FixedAsset added = Helper.Add<FixedAsset>(qboContextoAuth, fixedAsset);
            //Change the data of added entity
            FixedAsset changed = QBOHelper.UpdateFixedAsset(qboContextoAuth, added);
            //Update the returned entity data
            FixedAsset updated = Helper.Update<FixedAsset>(qboContextoAuth, changed);//Verify the updated FixedAsset
            QBOHelper.VerifyFixedAsset(changed, updated);
        }

        [TestMethod]
        public void FixedAssetSparseUpdateTestUsingoAuth()
        {
            //Creating the FixedAsset for Adding
            FixedAsset fixedAsset = QBOHelper.CreateFixedAsset(qboContextoAuth);
            //Adding the FixedAsset
            FixedAsset added = Helper.Add<FixedAsset>(qboContextoAuth, fixedAsset);
            //Change the data of added entity
            FixedAsset changed = QBOHelper.UpdateFixedAssetSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            FixedAsset updated = Helper.Update<FixedAsset>(qboContextoAuth, changed);//Verify the updated FixedAsset
            QBOHelper.VerifyFixedAssetSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void FixedAssetDeleteTestUsingoAuth()
        {
            //Creating the FixedAsset for Adding
            FixedAsset fixedAsset = QBOHelper.CreateFixedAsset(qboContextoAuth);
            //Adding the FixedAsset
            FixedAsset added = Helper.Add<FixedAsset>(qboContextoAuth, fixedAsset);
            //Delete the returned entity
            try
            {
                FixedAsset deleted = Helper.Delete<FixedAsset>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void FixedAssetVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            FixedAsset entity = QBOHelper.CreateFixedAsset(qboContextoAuth);
            //Adding the entity
            FixedAsset added = Helper.Add<FixedAsset>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                FixedAsset voided = Helper.Void<FixedAsset>(qboContextoAuth, added);
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
        public void FixedAssetCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            FixedAssetAddTestUsingoAuth();

            //Retrieving the FixedAsset using CDC
            List<FixedAsset> entities = Helper.CDC(qboContextoAuth, new FixedAsset(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<FixedAsset>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void FixedAssetBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            FixedAsset existing = Helper.FindOrAdd(qboContextoAuth, new FixedAsset());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateFixedAsset(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateFixedAsset(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from FixedAsset");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<FixedAsset>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as FixedAsset).Id));
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
        public void FixedAssetQueryUsingoAuth()
        {
            QueryService<FixedAsset> entityQuery = new QueryService<FixedAsset>(qboContextoAuth);
            FixedAsset existing = Helper.FindOrAdd<FixedAsset>(qboContextoAuth, new FixedAsset());
            int count = entityQuery.ExecuteIdsQuery("Select * from FixedAsset where Id='" + existing.Id + "'").Count;
            Assert.IsTrue(count > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void FixedAssetAddAsyncTestsUsingoAuth()
        {
            //Creating the FixedAsset for Add
            FixedAsset entity = QBOHelper.CreateFixedAsset(qboContextoAuth);

            FixedAsset added = Helper.AddAsync<FixedAsset>(qboContextoAuth, entity);
            QBOHelper.VerifyFixedAsset(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void FixedAssetRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            FixedAssetAddTestUsingoAuth();

            //Retrieving the FixedAsset using FindAll
            Helper.FindAllAsync<FixedAsset>(qboContextoAuth, new FixedAsset());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void FixedAssetFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the FixedAsset for Adding
            FixedAsset entity = QBOHelper.CreateFixedAsset(qboContextoAuth);
            //Adding the FixedAsset
            FixedAsset added = Helper.Add<FixedAsset>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<FixedAsset>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void FixedAssetUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the FixedAsset for Adding
            FixedAsset entity = QBOHelper.CreateFixedAsset(qboContextoAuth);
            //Adding the FixedAsset
            FixedAsset added = Helper.Add<FixedAsset>(qboContextoAuth, entity);

            //Update the FixedAsset
            FixedAsset updated = QBOHelper.UpdateFixedAsset(qboContextoAuth, added);
            //Call the service
            FixedAsset updatedReturned = Helper.UpdateAsync<FixedAsset>(qboContextoAuth, updated);
            //Verify updated FixedAsset
            QBOHelper.VerifyFixedAsset(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void FixedAssetDeleteAsyncTestsUsingoAuth()
        {
            //Creating the FixedAsset for Adding
            FixedAsset entity = QBOHelper.CreateFixedAsset(qboContextoAuth);
            //Adding the FixedAsset
            FixedAsset added = Helper.Add<FixedAsset>(qboContextoAuth, entity);

            Helper.DeleteAsync<FixedAsset>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void FixedAssetVoidAsyncTestsUsingoAuth()
        {
            //Creating the FixedAsset for Adding
            FixedAsset entity = QBOHelper.CreateFixedAsset(qboContextoAuth);
            //Adding the FixedAsset
            FixedAsset added = Helper.Add<FixedAsset>(qboContextoAuth, entity);

            Helper.VoidAsync<FixedAsset>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
