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
    public class ItemTest
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

 

        #region Test cases for Add Operations France

        [TestMethod]
        public void ItemAddTestUsingoAuthFrance()
        {
            //Creating the Item for Add
            //Item item = QBOHelper.CreateItem(qboContextoAuth);

            Item item = QBOHelper.CreateItemFrance(qboContextoAuth);
            //Adding the Item
            Item added = Helper.Add<Item>(qboContextoAuth, item);
            //Verify the added Item
            // QBOHelper.VerifyItem(item, added);
            QBOHelper.VerifyItemFrance(item, added);
        }

        #endregion

        #region Test cases for Add Operations

        [TestMethod]
        public void ItemAddTestUsingoAuth()
        {
            //Creating the Item for Add
            Item item = QBOHelper.CreateItem(qboContextoAuth);
            //Adding the Item
            Item added = Helper.Add<Item>(qboContextoAuth, item);
            //Verify the added Item
            QBOHelper.VerifyItem(item, added);

        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void ItemFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            ItemAddTestUsingoAuth();

            //Retrieving the Item using FindAll
            List<Item> items = Helper.FindAll<Item>(qboContextoAuth, new Item(), 1, 500);
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count<Item>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void ItemFindbyIdTestUsingoAuth()
        {
            //Creating the Item for Adding
            Item item = QBOHelper.CreateItem(qboContextoAuth);
            //Adding the Item
            Item added = Helper.Add<Item>(qboContextoAuth, item);
            Item found = Helper.FindById<Item>(qboContextoAuth, added);
            QBOHelper.VerifyItem(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void ItemUpdateTestUsingoAuth()
        {
            //Creating the Item for Adding
            Item item = QBOHelper.CreateItem(qboContextoAuth);
         
            //Adding the Item
            Item added = Helper.Add<Item>(qboContextoAuth, item);
            //Change the data of added entity
            Item changed = QBOHelper.UpdateItem(qboContextoAuth, added);
            //Update the returned entity data
            Item updated = Helper.Update<Item>(qboContextoAuth, changed);//Verify the updated Item
            QBOHelper.VerifyItem(changed, updated);
        }

        #region Test cases for Update,donotupdate Operations France
        [TestMethod]
        public void ItemUpdateTestUsingoAuthFrance()
        {
            //Creating the Item for Adding
            //Item item = QBOHelper.CreateItem(qboContextoAuth);
              Item item = QBOHelper.CreateItemFrance(qboContextoAuth);
            //Adding the Item
            Item added = Helper.Add<Item>(qboContextoAuth, item);
            //Change the data of added entity
            Item changed = QBOHelper.UpdateItemFrance(qboContextoAuth, added);
            //Update the returned entity data
            Item updated = Helper.Update<Item>(qboContextoAuth, changed);//Verify the updated Item
            QBOHelper.VerifyItemFrance(changed, updated);
        }


        [TestMethod]
        public void ItemdDonotUpdateAccTestUsingoAuthFrance()
        {
            //Creating the Item for Adding
            Item item = QBOHelper.CreateItemFrance(qboContextoAuth);
            //Adding the Item
            Item added = Helper.Add<Item>(qboContextoAuth, item);
            //Change the data of added entity
            Item changed = QBOHelper.UpdateItemFrance(qboContextoAuth, added);
            //Update the returned entity data
            Item updated = Helper.DonotUpdateAccountOnTxnsFrance<Item>(qboContextoAuth, changed);//Verify the updated Item
            QBOHelper.VerifyItemFrance(changed, updated);
        }

        #endregion


        [TestMethod]
        public void ItemSparseUpdateTestUsingoAuth()
        {
            //Creating the Item for Adding
            Item item = QBOHelper.CreateItem(qboContextoAuth);
            //Adding the Item
            Item added = Helper.Add<Item>(qboContextoAuth, item);
            //Change the data of added entity
            Item changed = QBOHelper.SparseUpdateItem(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Item updated = Helper.Update<Item>(qboContextoAuth, changed);//Verify the updated Item
            QBOHelper.VerifyItemSparseUpdate(changed, updated);
        }

        #endregion
    
        #region Test cases for Delete Operations

        [TestMethod] [Ignore]  //TestComment: Returns Operation Not Supported
        public void ItemDeleteTestUsingoAuth()
        {
            //Creating the Item for Adding
            Item item = QBOHelper.CreateItem(qboContextoAuth);
            //Adding the Item
            Item added = Helper.Add<Item>(qboContextoAuth, item);
            //Delete the returned entity
            try
            {
                Item deleted = Helper.Delete<Item>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod] [Ignore]
        public void ItemVoidTestUsingoAuth()
        {
            //Creating the Item for Adding
            Item item = QBOHelper.CreateItem(qboContextoAuth);
            //Adding the Item
            Item added = Helper.Add<Item>(qboContextoAuth, item);
            //Void the returned entity
            try
            {
                Item voided = Helper.Void<Item>(qboContextoAuth, added);
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
        public void ItemCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            ItemAddTestUsingoAuth();

            //Retrieving the Item using FindAll
            List<Item> items = Helper.CDC(qboContextoAuth, new Item(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(items);
            Assert.IsTrue(items.Count<Item>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void ItemBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Item existing = Helper.FindOrAdd(qboContextoAuth, new Item());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateItem(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateItem(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Item");

            //batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Item>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Item).Id));
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
        public void ItemQueryUsingoAuth()
        {
            QueryService<Item> entityQuery = new QueryService<Item>(qboContextoAuth);
            Item existing = Helper.FindOrAdd<Item>(qboContextoAuth, new Item());
            //List<Item> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<Item> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Item where Id='" + existing.Id + "'").ToList<Item>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void ItemAddAsyncTestsUsingoAuth()
        {
            //Creating the Item for Add
            Item entity = QBOHelper.CreateItem(qboContextoAuth);

            Item added = Helper.AddAsync<Item>(qboContextoAuth, entity);
            QBOHelper.VerifyItem(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void ItemRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            ItemAddTestUsingoAuth();

            //Retrieving the Item using FindAll
            Helper.FindAllAsync<Item>(qboContextoAuth, new Item());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void ItemFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Item for Adding
            Item entity = QBOHelper.CreateItem(qboContextoAuth);
            //Adding the Item
            Item added = Helper.Add<Item>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Item>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void ItemUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Item for Adding
            Item entity = QBOHelper.CreateItem(qboContextoAuth);
            //Adding the Item
            Item added = Helper.Add<Item>(qboContextoAuth, entity);

            //Update the Item
            Item updated = QBOHelper.UpdateItem(qboContextoAuth, added);
            //Call the service
            Item updatedReturned = Helper.UpdateAsync<Item>(qboContextoAuth, updated);
            //Verify updated Item
            QBOHelper.VerifyItem(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod] [Ignore]  //TestComment: Returns Operation Not Supported
        public void ItemDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Item for Adding
            Item entity = QBOHelper.CreateItem(qboContextoAuth);
            //Adding the Item
            Item added = Helper.Add<Item>(qboContextoAuth, entity);

            Helper.DeleteAsync<Item>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void ItemVoidAsyncTestsUsingoAuth()
        {
            //Creating the Item for Adding
            Item entity = QBOHelper.CreateItem(qboContextoAuth);
            //Adding the Item
            Item added = Helper.Add<Item>(qboContextoAuth, entity);

            Helper.VoidAsync<Item>(qboContextoAuth, added);
        }

        #endregion

        //newly added
        #region Test Cases for Do Not Update Account Operation

        [TestMethod][Ignore]
        public void ItemDonotUpdatedaccAsyncTestsUsingoAuthFrance()
        {
            //Creating the Item for Adding
            Item entity = QBOHelper.CreateItemFrance(qboContextoAuth);
            //Adding the Item
            Item added = Helper.Add<Item>(qboContextoAuth, entity);

            //Update the Item
            // Item updated = QBOHelper.UpdateItem(qboContextoAuth, added);
            Item updated = QBOHelper.UpdateItemFrance(qboContextoAuth, added);
            //Call the service
            Item updatedReturned = Helper.DonotUpdateAccountOnTxnsAsyncFrance<Item>(qboContextoAuth, updated);
            //Verify updated Item
            QBOHelper.VerifyItemFrance(updated, updatedReturned);
        }

        #endregion

        #endregion

        #endregion

    }
}
