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
    public class PriceLevelTest
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
        public void PriceLevelAddTestUsingoAuth()
        {
            //Creating the PriceLevel for Add
            PriceLevel priceLevel = QBOHelper.CreatePriceLevel(qboContextoAuth);
            //Adding the PriceLevel
            PriceLevel added = Helper.Add<PriceLevel>(qboContextoAuth, priceLevel);
            //Verify the added PriceLevel
            QBOHelper.VerifyPriceLevel(priceLevel, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void PriceLevelFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            PriceLevelAddTestUsingoAuth();

            //Retrieving the PriceLevel using FindAll
            List<PriceLevel> priceLevels = Helper.FindAll<PriceLevel>(qboContextoAuth, new PriceLevel(), 1, 500);
            Assert.IsNotNull(priceLevels);
            Assert.IsTrue(priceLevels.Count<PriceLevel>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void PriceLevelFindbyIdTestUsingoAuth()
        {
            //Creating the PriceLevel for Adding
            PriceLevel priceLevel = QBOHelper.CreatePriceLevel(qboContextoAuth);
            //Adding the PriceLevel
            PriceLevel added = Helper.Add<PriceLevel>(qboContextoAuth, priceLevel);
            PriceLevel found = Helper.FindById<PriceLevel>(qboContextoAuth, added);
            QBOHelper.VerifyPriceLevel(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void PriceLevelUpdateTestUsingoAuth()
        {
            //Creating the PriceLevel for Adding
            PriceLevel priceLevel = QBOHelper.CreatePriceLevel(qboContextoAuth);
            //Adding the PriceLevel
            PriceLevel added = Helper.Add<PriceLevel>(qboContextoAuth, priceLevel);
            //Change the data of added entity
            PriceLevel changed = QBOHelper.UpdatePriceLevel(qboContextoAuth, added);
            //Update the returned entity data
            PriceLevel updated = Helper.Update<PriceLevel>(qboContextoAuth, changed);//Verify the updated PriceLevel
            QBOHelper.VerifyPriceLevel(changed, updated);
        }

        [TestMethod]
        public void PriceLevelSparseUpdateTestUsingoAuth()
        {
            //Creating the PriceLevel for Adding
            PriceLevel priceLevel = QBOHelper.CreatePriceLevel(qboContextoAuth);
            //Adding the PriceLevel
            PriceLevel added = Helper.Add<PriceLevel>(qboContextoAuth, priceLevel);
            //Change the data of added entity
            PriceLevel changed = QBOHelper.UpdatePriceLevelSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            PriceLevel updated = Helper.Update<PriceLevel>(qboContextoAuth, changed);//Verify the updated PriceLevel
            QBOHelper.VerifyPriceLevelSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void PriceLevelDeleteTestUsingoAuth()
        {
            //Creating the PriceLevel for Adding
            PriceLevel priceLevel = QBOHelper.CreatePriceLevel(qboContextoAuth);
            //Adding the PriceLevel
            PriceLevel added = Helper.Add<PriceLevel>(qboContextoAuth, priceLevel);
            //Delete the returned entity
            try
            {
                PriceLevel deleted = Helper.Delete<PriceLevel>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void PriceLevelVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            PriceLevel entity = QBOHelper.CreatePriceLevel(qboContextoAuth);
            //Adding the entity
            PriceLevel added = Helper.Add<PriceLevel>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                PriceLevel voided = Helper.Void<PriceLevel>(qboContextoAuth, added);
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
        public void PriceLevelCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            PriceLevelAddTestUsingoAuth();

            //Retrieving the PriceLevel using CDC
            List<PriceLevel> entities = Helper.CDC(qboContextoAuth, new PriceLevel(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<PriceLevel>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void PriceLevelBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            PriceLevel existing = Helper.FindOrAdd(qboContextoAuth, new PriceLevel());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreatePriceLevel(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdatePriceLevel(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from PriceLevel");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<PriceLevel>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as PriceLevel).Id));
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
        public void PriceLevelQueryUsingoAuth()
        {
            QueryService<PriceLevel> entityQuery = new QueryService<PriceLevel>(qboContextoAuth);
            PriceLevel existing = Helper.FindOrAdd<PriceLevel>(qboContextoAuth, new PriceLevel());
            List<PriceLevel> entities = entityQuery.ExecuteIdsQuery("Select * from Customer where Id == '"+existing.Id+"'").ToList();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void PriceLevelAddAsyncTestsUsingoAuth()
        {
            //Creating the PriceLevel for Add
            PriceLevel entity = QBOHelper.CreatePriceLevel(qboContextoAuth);

            PriceLevel added = Helper.AddAsync<PriceLevel>(qboContextoAuth, entity);
            QBOHelper.VerifyPriceLevel(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void PriceLevelRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            PriceLevelAddTestUsingoAuth();

            //Retrieving the PriceLevel using FindAll
            Helper.FindAllAsync<PriceLevel>(qboContextoAuth, new PriceLevel());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void PriceLevelFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the PriceLevel for Adding
            PriceLevel entity = QBOHelper.CreatePriceLevel(qboContextoAuth);
            //Adding the PriceLevel
            PriceLevel added = Helper.Add<PriceLevel>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<PriceLevel>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void PriceLevelUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the PriceLevel for Adding
            PriceLevel entity = QBOHelper.CreatePriceLevel(qboContextoAuth);
            //Adding the PriceLevel
            PriceLevel added = Helper.Add<PriceLevel>(qboContextoAuth, entity);

            //Update the PriceLevel
            PriceLevel updated = QBOHelper.UpdatePriceLevel(qboContextoAuth, added);
            //Call the service
            PriceLevel updatedReturned = Helper.UpdateAsync<PriceLevel>(qboContextoAuth, updated);
            //Verify updated PriceLevel
            QBOHelper.VerifyPriceLevel(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void PriceLevelDeleteAsyncTestsUsingoAuth()
        {
            //Creating the PriceLevel for Adding
            PriceLevel entity = QBOHelper.CreatePriceLevel(qboContextoAuth);
            //Adding the PriceLevel
            PriceLevel added = Helper.Add<PriceLevel>(qboContextoAuth, entity);

            Helper.DeleteAsync<PriceLevel>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void PriceLevelVoidAsyncTestsUsingoAuth()
        {
            //Creating the PriceLevel for Adding
            PriceLevel entity = QBOHelper.CreatePriceLevel(qboContextoAuth);
            //Adding the PriceLevel
            PriceLevel added = Helper.Add<PriceLevel>(qboContextoAuth, entity);

            Helper.VoidAsync<PriceLevel>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
