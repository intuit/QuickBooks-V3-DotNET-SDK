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
using Intuit.Ipp.LinqExtender;
using System.Collections.ObjectModel;
using Intuit.Ipp.DataService;

namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass] [Ignore]
    public class CurrencyTest
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
        public void CurrencyAddTestUsingoAuth()
        {
            //Creating the Currency for Add
            Currency currency = QBOHelper.CreateCurrency(qboContextoAuth);
            //Adding the Currency
            Currency added = Helper.Add<Currency>(qboContextoAuth, currency);
            //Verify the added Currency
            QBOHelper.VerifyCurrency(currency, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void CurrencyFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CurrencyAddTestUsingoAuth();

            //Retrieving the Currency using FindAll
            List<Currency> currencys = Helper.FindAll<Currency>(qboContextoAuth, new Currency(), 1, 500);
            Assert.IsNotNull(currencys);
            Assert.IsTrue(currencys.Count<Currency>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void CurrencyFindbyIdTestUsingoAuth()
        {
            //Creating the Currency for Adding
            Currency currency = QBOHelper.CreateCurrency(qboContextoAuth);
            //Adding the Currency
            Currency added = Helper.Add<Currency>(qboContextoAuth, currency);
            Currency found = Helper.FindById<Currency>(qboContextoAuth, added);
            QBOHelper.VerifyCurrency(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void CurrencyUpdateTestUsingoAuth()
        {
            //Creating the Currency for Adding
            Currency currency = QBOHelper.CreateCurrency(qboContextoAuth);
            //Adding the Currency
            Currency added = Helper.Add<Currency>(qboContextoAuth, currency);
            //Change the data of added entity
            Currency changed = QBOHelper.UpdateCurrency(qboContextoAuth, added);
            //Update the returned entity data
            Currency updated = Helper.Update<Currency>(qboContextoAuth, changed);//Verify the updated Currency
            QBOHelper.VerifyCurrency(changed, updated);
        }

        [TestMethod]
        public void CurrencySparseUpdateTestUsingoAuth()
        {
            //Creating the Currency for Adding
            Currency currency = QBOHelper.CreateCurrency(qboContextoAuth);
            //Adding the Currency
            Currency added = Helper.Add<Currency>(qboContextoAuth, currency);
            //Change the data of added entity
            Currency changed = QBOHelper.UpdateCurrencySparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Currency updated = Helper.Update<Currency>(qboContextoAuth, changed);//Verify the updated Currency
            QBOHelper.VerifyCurrencySparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void CurrencyDeleteTestUsingoAuth()
        {
            //Creating the Currency for Adding
            Currency currency = QBOHelper.CreateCurrency(qboContextoAuth);
            //Adding the Currency
            Currency added = Helper.Add<Currency>(qboContextoAuth, currency);
            //Delete the returned entity
            try
            {
                Currency deleted = Helper.Delete<Currency>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void CurrencyVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            Currency entity = QBOHelper.CreateCurrency(qboContextoAuth);
            //Adding the entity
            Currency added = Helper.Add<Currency>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                Currency voided = Helper.Void<Currency>(qboContextoAuth, added);
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
        public void CurrencyCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CurrencyAddTestUsingoAuth();

            //Retrieving the Currency using CDC
            List<Currency> entities = Helper.CDC(qboContextoAuth, new Currency(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<Currency>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void CurrencyBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Currency existing = Helper.FindOrAdd(qboContextoAuth, new Currency());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateCurrency(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateCurrency(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Currency");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Currency>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Currency).Id));
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
        public void CurrencyQueryUsingoAuth()
        {
            QueryService<Currency> entityQuery = new QueryService<Currency>(qboContextoAuth);
            Currency existing = Helper.FindOrAdd<Currency>(qboContextoAuth, new Currency());
            List<Currency> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void CurrencyAddAsyncTestsUsingoAuth()
        {
            //Creating the Currency for Add
            Currency entity = QBOHelper.CreateCurrency(qboContextoAuth);

            Currency added = Helper.AddAsync<Currency>(qboContextoAuth, entity);
            QBOHelper.VerifyCurrency(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void CurrencyRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CurrencyAddTestUsingoAuth();

            //Retrieving the Currency using FindAll
            Helper.FindAllAsync<Currency>(qboContextoAuth, new Currency());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void CurrencyFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Currency for Adding
            Currency entity = QBOHelper.CreateCurrency(qboContextoAuth);
            //Adding the Currency
            Currency added = Helper.Add<Currency>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Currency>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void CurrencyUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Currency for Adding
            Currency entity = QBOHelper.CreateCurrency(qboContextoAuth);
            //Adding the Currency
            Currency added = Helper.Add<Currency>(qboContextoAuth, entity);

            //Update the Currency
            Currency updated = QBOHelper.UpdateCurrency(qboContextoAuth, added);
            //Call the service
            Currency updatedReturned = Helper.UpdateAsync<Currency>(qboContextoAuth, updated);
            //Verify updated Currency
            QBOHelper.VerifyCurrency(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void CurrencyDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Currency for Adding
            Currency entity = QBOHelper.CreateCurrency(qboContextoAuth);
            //Adding the Currency
            Currency added = Helper.Add<Currency>(qboContextoAuth, entity);

            Helper.DeleteAsync<Currency>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void CurrencyVoidAsyncTestsUsingoAuth()
        {
            //Creating the Currency for Adding
            Currency entity = QBOHelper.CreateCurrency(qboContextoAuth);
            //Adding the Currency
            Currency added = Helper.Add<Currency>(qboContextoAuth, entity);

            Helper.VoidAsync<Currency>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
