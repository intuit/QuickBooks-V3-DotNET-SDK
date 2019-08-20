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

using System.Configuration;

namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass]
    public class PurchaseTest
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

        #region Cash Purchase Methods

        #region Test cases for Add Operations

        [TestMethod]
        public void CashPurchaseAddTestUsingoAuth()
        {
            //Creating the Purchase for Add
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.Cash);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            //Verify the added Purchase
            QBOHelper.VerifyPurchase(purchase, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void CashPurchaseFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CashPurchaseAddTestUsingoAuth();

            //Retrieving the Purchase using FindAll
            List<Purchase> purchases = Helper.FindAll<Purchase>(qboContextoAuth, new Purchase(), 1, 500);
            Assert.IsNotNull(purchases);
            Assert.IsTrue(purchases.Count<Purchase>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void CashPurchaseFindbyIdTestUsingoAuth()
        {
            //Creating the Purchase for Adding
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.Cash);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            Purchase found = Helper.FindById<Purchase>(qboContextoAuth, added);
            QBOHelper.VerifyPurchase(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void CashPurchaseUpdateTestUsingoAuth()
        {
            //Creating the Purchase for Adding
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.Cash);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            //Change the data of added entity
            Purchase changed = QBOHelper.UpdatePurchase(qboContextoAuth, added);
            //Update the returned entity data
            Purchase updated = Helper.Update<Purchase>(qboContextoAuth, changed);//Verify the updated Purchase
            QBOHelper.VerifyPurchase(changed, updated);
        }


        [TestMethod]
        public void CashPurchaseSparseUpdateTestUsingoAuth()
        {
            //Creating the Purchase for Adding
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.Cash);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            //Change the data of added entity
            Purchase changed = QBOHelper.SparseUpdatePurchase(qboContextoAuth, added.Id, added.PaymentType, added.SyncToken);
            //Update the returned entity data
            Purchase updated = Helper.Update<Purchase>(qboContextoAuth, changed);//Verify the updated Purchase
            QBOHelper.VerifyPurchaseSparseUpdate(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void CashPurchaseDeleteTestUsingoAuth()
        {
            //Creating the Purchase for Adding
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.Cash);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            //Delete the returned entity
            try
            {
                Purchase deleted = Helper.Delete<Purchase>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [Ignore]
        public void CashPurchaseVoidTestUsingoAuth()
        {
            //Creating the Purchase for Adding
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.Cash);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            //Void the returned entity
            try
            {
                Purchase voided = Helper.Void<Purchase>(qboContextoAuth, added);
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
        public void CashPurchaseCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CashPurchaseAddTestUsingoAuth();

            //Retrieving the Purchase using FindAll
            List<Purchase> cashPurchases = Helper.CDC(qboContextoAuth, new Purchase(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(cashPurchases);
            Assert.IsTrue(cashPurchases.Count<Purchase>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void PurchaseBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Purchase existing = Helper.FindOrAdd(qboContextoAuth, new Purchase());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.Cash));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdatePurchase(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Purchase");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Purchase>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Purchase).Id));
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
        public void CashPurchaseQueryUsingoAuth()
        {
            QueryService<Purchase> entityQuery = new QueryService<Purchase>(qboContextoAuth);
            Purchase existing = Helper.FindOrAddPurchase(qboContextoAuth, PaymentTypeEnum.Cash);

            List<Purchase> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Purchase where Id='" + existing.Id + "'").ToList<Purchase>();

            //List<Purchase> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region Check Purchase Methods

        #region Test cases for Add Operations

        [TestMethod]
        public void CheckPurchaseAddTestUsingoAuth()
        {
            //Creating the Purchase for Add
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.Check);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            //Verify the added Purchase
            QBOHelper.VerifyPurchase(purchase, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void CheckPurchaseFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CheckPurchaseAddTestUsingoAuth();

            //Retrieving the Purchase using FindAll
            List<Purchase> purchases = Helper.FindAll<Purchase>(qboContextoAuth, new Purchase(), 1, 500);
            Assert.IsNotNull(purchases);
            Assert.IsTrue(purchases.Count<Purchase>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void CheckPurchaseFindbyIdTestUsingoAuth()
        {
            //Creating the Purchase for Adding
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.Check);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            Purchase found = Helper.FindById<Purchase>(qboContextoAuth, added);
            QBOHelper.VerifyPurchase(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void CheckPurchaseUpdateTestUsingoAuth()
        {
            //Creating the Purchase for Adding
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.Check);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            //Change the data of added entity
            Purchase changed = QBOHelper.UpdatePurchase(qboContextoAuth, added);
            //Update the returned entity data
            Purchase updated = Helper.Update<Purchase>(qboContextoAuth, changed);//Verify the updated Purchase
            QBOHelper.VerifyPurchase(changed, updated);
        }


        [TestMethod]
        public void CheckPurchaseSparseUpdateTestUsingoAuth()
        {
            //Creating the Purchase for Adding
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.Check);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            //Change the data of added entity
            Purchase changed = QBOHelper.SparseUpdatePurchase(qboContextoAuth, added.Id, added.PaymentType, added.SyncToken);
            //Update the returned entity data
            Purchase updated = Helper.Update<Purchase>(qboContextoAuth, changed);//Verify the updated Purchase
            QBOHelper.VerifyPurchaseSparseUpdate(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void CheckPurchaseDeleteTestUsingoAuth()
        {
            //Creating the Purchase for Adding
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.Check);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            //Delete the returned entity
            try
            {
                Purchase deleted = Helper.Delete<Purchase>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [Ignore]
        public void CheckPurchaseVoidTestUsingoAuth()
        {
            //Creating the Purchase for Adding
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.Check);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            //Void the returned entity
            try
            {
                Purchase voided = Helper.Void<Purchase>(qboContextoAuth, added);
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
        public void CheckPurchaseCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CheckPurchaseAddTestUsingoAuth();

            //Retrieving the Purchase using FindAll
            List<Purchase> checkPurchases = Helper.CDC(qboContextoAuth, new Purchase(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(checkPurchases);
            Assert.IsTrue(checkPurchases.Count<Purchase>() > 0);
        }

        #endregion

        #region Test cases for Query
        [TestMethod]
        public void CheckPurchaseQueryUsingoAuth()
        {
            QueryService<Purchase> entityQuery = new QueryService<Purchase>(qboContextoAuth);
            Purchase existing = Helper.FindOrAddPurchase(qboContextoAuth, PaymentTypeEnum.Check);
            //List<Purchase> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<Purchase> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Purchase where Id='" + existing.Id + "'").ToList<Purchase>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region CreditCard Purchase Methods

        #region Test cases for Add Operations

        [TestMethod]
        public void CreditCardPurchaseAddTestUsingoAuth()
        {
            //Creating the Purchase for Add
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.CreditCard);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            //Verify the added Purchase
            QBOHelper.VerifyPurchase(purchase, added);
        }


        [TestMethod]
        public void CheckPurchaseAddDuplicateDocNumberGlobalTestUsingoAuth()
        {
            try
            {
                //Creating the Purchase for Add
                Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.Check);
                purchase.DocNumber = "DUPLICATE";
                //Pass parameter to allow duplicate doc numbers
                qboContextoAuth.Include.Add("allowduplicatedocnum");
                //Adding the Purchase
                Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
                Purchase addedDuplicate = Helper.Add<Purchase>(qboContextoAuth, purchase);
                //Verify the added Purchase
                QBOHelper.VerifyPurchase(purchase, added);
            }
            finally { qboContextoAuth.Include.Clear(); }
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void CreditCardPurchaseFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CreditCardPurchaseAddTestUsingoAuth();

            //Retrieving the Purchase using FindAll
            List<Purchase> purchases = Helper.FindAll<Purchase>(qboContextoAuth, new Purchase(), 1, 500);
            Assert.IsNotNull(purchases);
            Assert.IsTrue(purchases.Count<Purchase>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void CreditCardPurchaseFindbyIdTestUsingoAuth()
        {
            //Creating the Purchase for Adding
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.CreditCard);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            Purchase found = Helper.FindById<Purchase>(qboContextoAuth, added);
            QBOHelper.VerifyPurchase(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void CreditCardPurchaseUpdateTestUsingoAuth()
        {
            //Creating the Purchase for Adding
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.CreditCard);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            //Change the data of added entity
            Purchase changed = QBOHelper.UpdatePurchase(qboContextoAuth, added);
            //Update the returned entity data
            Purchase updated = Helper.Update<Purchase>(qboContextoAuth, changed);//Verify the updated Purchase
            QBOHelper.VerifyPurchase(changed, updated);
        }


        [TestMethod]
        public void CreditCardPurchaseSparseUpdateTestUsingoAuth()
        {
            //Creating the Purchase for Adding
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.CreditCard);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            //Change the data of added entity
            Purchase changed = QBOHelper.SparseUpdatePurchase(qboContextoAuth, added.Id, added.PaymentType, added.SyncToken);
            //Update the returned entity data
            Purchase updated = Helper.Update<Purchase>(qboContextoAuth, changed);//Verify the updated Purchase
            QBOHelper.VerifyPurchaseSparseUpdate(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void CreditCardPurchaseDeleteTestUsingoAuth()
        {
            //Creating the Purchase for Adding
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.CreditCard);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            //Delete the returned entity
            try
            {
                Purchase deleted = Helper.Delete<Purchase>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }

        }

        [TestMethod]
        [Ignore]
        public void CreditCardPurchaseVoidTestUsingoAuth()
        {
            //Creating the Purchase for Adding
            Purchase purchase = QBOHelper.CreatePurchase(qboContextoAuth, PaymentTypeEnum.CreditCard);
            //Adding the Purchase
            Purchase added = Helper.Add<Purchase>(qboContextoAuth, purchase);
            //Void the returned entity
            try
            {
                Purchase voided = Helper.Void<Purchase>(qboContextoAuth, added);
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
        public void CreditCardPurchaseCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CreditCardPurchaseAddTestUsingoAuth();

            //Retrieving the Purchase using FindAll
            List<Purchase> creditCardPurchases = Helper.CDC(qboContextoAuth, new Purchase(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(creditCardPurchases);
            Assert.IsTrue(creditCardPurchases.Count<Purchase>() > 0);
        }

        #endregion

        #region Test cases for Query
        [TestMethod]
        public void CreditCardPurchaseQueryUsingoAuth()
        {
            QueryService<Purchase> entityQuery = new QueryService<Purchase>(qboContextoAuth);
            Purchase existing = Helper.FindOrAddPurchase(qboContextoAuth, PaymentTypeEnum.CreditCard);
            //List<Purchase> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<Purchase> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Purchase where Id='" + existing.Id + "'").ToList<Purchase>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #endregion

    }
}
