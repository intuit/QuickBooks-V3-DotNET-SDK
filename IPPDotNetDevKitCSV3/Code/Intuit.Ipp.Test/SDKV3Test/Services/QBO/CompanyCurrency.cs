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
    [TestClass]
    [Ignore]
    public class CompanyCurrencyTest
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
        [Ignore]
        public void CompanyCurrencyAddTestUsingoAuth()
        {
            //Creating the CompanyCurrency for Add
            CompanyCurrency companyCurrency = QBOHelper.CreateCompanyCurrency(qboContextoAuth);
            //Adding the CompanyCurrency
            CompanyCurrency added = Helper.Add<CompanyCurrency>(qboContextoAuth, companyCurrency);
            //Verify the added CompanyCurrency
            QBOHelper.VerifyCompanyCurrency(companyCurrency, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        [Ignore]
        public void CompanyCurrencyFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CompanyCurrencyAddTestUsingoAuth();

            //Retrieving the CompanyCurrency using FindAll
            List<CompanyCurrency> currencys = Helper.FindAll<CompanyCurrency>(qboContextoAuth, new CompanyCurrency(), 1, 500);
            Assert.IsNotNull(currencys);
            Assert.IsTrue(currencys.Count<CompanyCurrency>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        [Ignore]
        public void CompanyCurrencyFindbyIdTestUsingoAuth()
        {
            //Creating the CompanyCurrency for Adding
            CompanyCurrency CompanyCurrency = QBOHelper.CreateCompanyCurrency(qboContextoAuth);
            //Adding the CompanyCurrency
            CompanyCurrency added = Helper.Add<CompanyCurrency>(qboContextoAuth, CompanyCurrency);
            CompanyCurrency found = Helper.FindById<CompanyCurrency>(qboContextoAuth, added);
            QBOHelper.VerifyCompanyCurrency(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        [Ignore]
        public void CompanyCurrencyUpdateTestUsingoAuth()
        {
            //Creating the CompanyCurrency for Adding
            CompanyCurrency companyCurrency = QBOHelper.CreateCompanyCurrency(qboContextoAuth);
            //Adding the CompanyCurrency
            CompanyCurrency added = Helper.Add<CompanyCurrency>(qboContextoAuth, companyCurrency);
            //Change the data of added entity
            CompanyCurrency changed = QBOHelper.UpdateCompanyCurrency(qboContextoAuth, added);
            //Update the returned entity data
            CompanyCurrency updated = Helper.Update<CompanyCurrency>(qboContextoAuth, changed);//Verify the updated CompanyCurrency
            QBOHelper.VerifyCompanyCurrency(changed, updated);
        }

        [TestMethod]
        [Ignore]
        public void CompanyCurrencySparseUpdateTestUsingoAuth()
        {
            //Creating the CompanyCurrency for Adding
            CompanyCurrency companyCurrency = QBOHelper.CreateCompanyCurrency(qboContextoAuth);
            //Adding the CompanyCurrency
            CompanyCurrency added = Helper.Add<CompanyCurrency>(qboContextoAuth, companyCurrency);
            //Change the data of added entity
            CompanyCurrency changed = QBOHelper.UpdateCompanyCurrencySparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            CompanyCurrency updated = Helper.Update<CompanyCurrency>(qboContextoAuth, changed);//Verify the updated CompanyCurrency
            QBOHelper.VerifyCompanyCurrencySparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        [Ignore] //Soft delete
        public void CompanyCurrencyDeleteTestUsingoAuth()
        {
            //Creating the CompanyCurrency for Adding
            CompanyCurrency companyCurrency = QBOHelper.CreateCompanyCurrency(qboContextoAuth);
            //Adding the CompanyCurrency
            CompanyCurrency added = Helper.Add<CompanyCurrency>(qboContextoAuth, companyCurrency);
            //Delete the returned entity
            try
            {
                CompanyCurrency deleted = Helper.Delete<CompanyCurrency>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [Ignore]
        public void CompanyCurrencyVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            CompanyCurrency entity = QBOHelper.CreateCompanyCurrency(qboContextoAuth);
            //Adding the entity
            CompanyCurrency added = Helper.Add<CompanyCurrency>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                CompanyCurrency voided = Helper.Void<CompanyCurrency>(qboContextoAuth, added);
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
        [Ignore]
        public void CompanyCurrencyCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CompanyCurrencyAddTestUsingoAuth();

            //Retrieving the CompanyCurrency using CDC
            List<CompanyCurrency> entities = Helper.CDC(qboContextoAuth, new CompanyCurrency(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<CompanyCurrency>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        [Ignore]
        public void CompanyCurrencyBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            CompanyCurrency existing = Helper.FindOrAdd(qboContextoAuth, new CompanyCurrency());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateCompanyCurrency(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateCompanyCurrency(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from CompanyCurrency");

            //batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<CompanyCurrency>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as CompanyCurrency).Id));
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
        public void CompanyCurrencyQueryUsingoAuth()
        {
            QueryService<CompanyCurrency> entityQuery = new QueryService<CompanyCurrency>(qboContextoAuth);
            CompanyCurrency existing = Helper.FindOrAdd<CompanyCurrency>(qboContextoAuth, new CompanyCurrency());
            //List<CompanyCurrency> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            int count = entityQuery.ExecuteIdsQuery("Select * from CompanyCurrency where Id='" + existing.Id + "'").Count;
            Assert.IsTrue(count> 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        [Ignore]
        public void CompanyCurrencyAddAsyncTestsUsingoAuth()
        {
            //Creating the CompanyCurrency for Add
            CompanyCurrency entity = QBOHelper.CreateCompanyCurrency(qboContextoAuth);

            CompanyCurrency added = Helper.AddAsync<CompanyCurrency>(qboContextoAuth, entity);
            QBOHelper.VerifyCompanyCurrency(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        [Ignore]
        public void CompanyCurrencyRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CompanyCurrencyAddTestUsingoAuth();

            //Retrieving the CompanyCurrency using FindAll
            Helper.FindAllAsync<CompanyCurrency>(qboContextoAuth, new CompanyCurrency());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        [Ignore]
        public void CompanyCurrencyFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the CompanyCurrency for Adding
            CompanyCurrency entity = QBOHelper.CreateCompanyCurrency(qboContextoAuth);
            //Adding the CompanyCurrency
            CompanyCurrency added = Helper.Add<CompanyCurrency>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<CompanyCurrency>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        [Ignore]
        public void CompanyCurrencyUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the CompanyCurrency for Adding
            CompanyCurrency entity = QBOHelper.CreateCompanyCurrency(qboContextoAuth);
            //Adding the CompanyCurrency
            CompanyCurrency added = Helper.Add<CompanyCurrency>(qboContextoAuth, entity);

            //Update the CompanyCurrency
            CompanyCurrency updated = QBOHelper.UpdateCompanyCurrency(qboContextoAuth, added);
            //Call the service
            CompanyCurrency updatedReturned = Helper.UpdateAsync<CompanyCurrency>(qboContextoAuth, updated);
            //Verify updated CompanyCurrency
            QBOHelper.VerifyCompanyCurrency(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        [Ignore]
        public void CompanyCurrencyDeleteAsyncTestsUsingoAuth()
        {
            //Creating the CompanyCurrency for Adding
            CompanyCurrency entity = QBOHelper.CreateCompanyCurrency(qboContextoAuth);
            //Adding the CompanyCurrency
            CompanyCurrency added = Helper.Add<CompanyCurrency>(qboContextoAuth, entity);

            Helper.DeleteAsync<CompanyCurrency>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        [Ignore]
        public void CompanyCurrencyVoidAsyncTestsUsingoAuth()
        {
            //Creating the CompanyCurrency for Adding
            CompanyCurrency entity = QBOHelper.CreateCompanyCurrency(qboContextoAuth);
            //Adding the CompanyCurrency
            CompanyCurrency added = Helper.Add<CompanyCurrency>(qboContextoAuth, entity);

            Helper.VoidAsync<CompanyCurrency>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
