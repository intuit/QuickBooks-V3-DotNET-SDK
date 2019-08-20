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
    public class TaxRateTest
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

        [TestMethod] [Ignore]
        public void TaxRateAddTestUsingoAuth()
        {
            //Creating the TaxRate for Add
            TaxRate taxRate = QBOHelper.CreateTaxRate(qboContextoAuth);
            //Adding the TaxRate
            TaxRate added = Helper.Add<TaxRate>(qboContextoAuth, taxRate);
            //Verify the added TaxRate
            QBOHelper.VerifyTaxRate(taxRate, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void TaxRateFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            //TaxRateAddTestUsingoAuth();

            //Retrieving the TaxRate using FindAll
            List<TaxRate> taxRates = Helper.FindAll<TaxRate>(qboContextoAuth, new TaxRate(), 1, 500);
            Assert.IsNotNull(taxRates);
            Assert.IsTrue(taxRates.Count<TaxRate>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void TaxRateFindbyIdTestUsingoAuth()
        {
            //Creating the TaxRate for Adding
            //TaxRate taxRate = QBOHelper.CreateTaxRate(qboContextoAuth);
            //Adding the TaxRate
            TaxRate added = Helper.FindOrAdd<TaxRate>(qboContextoAuth, new TaxRate());
            TaxRate found = Helper.FindById<TaxRate>(qboContextoAuth, added);
            QBOHelper.VerifyTaxRate(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod] [Ignore]
        public void TaxRateUpdateTestUsingoAuth()
        {
            //Creating the TaxRate for Adding
            //TaxRate taxRate = QBOHelper.CreateTaxRate(qboContextoAuth);
            //Adding the TaxRate
            TaxRate added = Helper.FindOrAdd<TaxRate>(qboContextoAuth, new TaxRate());
            //Change the data of added entity
            TaxRate changed = QBOHelper.UpdateTaxRate(qboContextoAuth, added);
            //Update the returned entity data
            TaxRate updated = Helper.Update<TaxRate>(qboContextoAuth, changed);//Verify the updated TaxRate
            QBOHelper.VerifyTaxRate(changed, updated);
        }

        [TestMethod] [Ignore]
        public void TaxRateSparseUpdateTestUsingoAuth()
        {
            //Creating the TaxRate for Adding
            TaxRate taxRate = QBOHelper.CreateTaxRate(qboContextoAuth);
            //Adding the TaxRate
            TaxRate added = Helper.Add<TaxRate>(qboContextoAuth, taxRate);
            //Change the data of added entity
            TaxRate changed = QBOHelper.SparseUpdateTaxRate(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            TaxRate updated = Helper.Update<TaxRate>(qboContextoAuth, changed);//Verify the updated TaxRate
            QBOHelper.VerifyTaxRateSparseUpdate(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod] [Ignore]
        public void TaxRateDeleteTestUsingoAuth()
        {
            //Creating the TaxRate for Adding
            //TaxRate taxRate = QBOHelper.CreateTaxRate(qboContextoAuth);
            //Adding the TaxRate
            TaxRate added = Helper.FindOrAdd<TaxRate>(qboContextoAuth, new TaxRate());
            //Delete the returned entity
            try
            {
                TaxRate deleted = Helper.Delete<TaxRate>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }


        [TestMethod] [Ignore]
        public void TaxRateVoidTestUsingoAuth()
        {
            //Creating the TaxRate for Adding
            TaxRate taxRate = QBOHelper.CreateTaxRate(qboContextoAuth);
            //Adding the TaxRate
            TaxRate added = Helper.Add<TaxRate>(qboContextoAuth, taxRate);
            //Void the returned entity
            try
            {
                TaxRate voided = Helper.Void<TaxRate>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Voided, voided.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        #endregion

        #region Test cases for CDC Operations

        [TestMethod] [Ignore]  //IgnoreReason:  CDC operations where Create is not supported removed for build
        public void TaxRateCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            //TaxRateAddTestUsingoAuth(); //TestComment:  This must be added manually as create is not supported

            //Retrieving the TaxRate using FindAll
            List<TaxRate> taxRates = Helper.CDC(qboContextoAuth, new TaxRate(), DateTime.Today.AddDays(-100));
            if(taxRates==null)
            {
                Assert.Inconclusive("No tax rates returned in response");
            }
            Assert.IsNotNull(taxRates);
            Assert.IsTrue(taxRates.Count<TaxRate>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void TaxRateBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            TaxRate existing = Helper.FindOrAdd(qboContextoAuth, new TaxRate());

         //   batchEntries.Add(OperationEnum.create, QBOHelper.CreateTaxRate(qboContextoAuth));

         //   batchEntries.Add(OperationEnum.update, QBOHelper.UpdateTaxRate(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from TaxRate");

        //    batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<TaxRate>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as TaxRate).Id));
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
        public void TaxRateQueryUsingoAuth()
        {
            QueryService<TaxRate> entityQuery = new QueryService<TaxRate>(qboContextoAuth);
            TaxRate existing = Helper.FindOrAdd<TaxRate>(qboContextoAuth, new TaxRate());
            //List<TaxRate> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<TaxRate> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM TaxRate where Id='" + existing.Id + "'").ToList<TaxRate>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod] [Ignore]
        public void TaxRateAddAsyncTestsUsingoAuth()
        {
            //Creating the TaxRate for Add
            TaxRate entity = QBOHelper.CreateTaxRate(qboContextoAuth);

            TaxRate added = Helper.AddAsync<TaxRate>(qboContextoAuth, entity);
            QBOHelper.VerifyTaxRate(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void TaxRateRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            //TaxRateAddTestUsingoAuth();

            //Retrieving the TaxRate using FindAll
            Helper.FindAllAsync<TaxRate>(qboContextoAuth, new TaxRate());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void TaxRateFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the TaxRate for Adding
            //TaxRate entity = QBOHelper.CreateTaxRate(qboContextoAuth);
            //Adding the TaxRate
            TaxRate added = Helper.FindOrAdd<TaxRate>(qboContextoAuth, new TaxRate());

            //FindById and verify
            Helper.FindByIdAsync<TaxRate>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod] [Ignore]
        public void TaxRateUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the TaxRate for Adding
            //TaxRate entity = QBOHelper.CreateTaxRate(qboContextoAuth);
            //Adding the TaxRate
            TaxRate added = Helper.FindOrAdd<TaxRate>(qboContextoAuth, new TaxRate());

            //Update the TaxRate
            TaxRate updated = QBOHelper.UpdateTaxRate(qboContextoAuth, added);
            //Call the service
            TaxRate updatedReturned = Helper.UpdateAsync<TaxRate>(qboContextoAuth, updated);
            //Verify updated TaxRate
            QBOHelper.VerifyTaxRate(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod] [Ignore]
        public void TaxRateDeleteAsyncTestsUsingoAuth()
        {
            //Creating the TaxRate for Adding
            //TaxRate entity = QBOHelper.CreateTaxRate(qboContextoAuth);
            //Adding the TaxRate
            TaxRate added = Helper.FindOrAdd<TaxRate>(qboContextoAuth, new TaxRate());

            Helper.DeleteAsync<TaxRate>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void TaxRateVoidAsyncTestsUsingoAuth()
        {
            //Creating the TaxRate for Adding
            //TaxRate entity = QBOHelper.CreateTaxRate(qboContextoAuth);
            //Adding the TaxRate
            TaxRate added = Helper.FindOrAdd<TaxRate>(qboContextoAuth, new TaxRate());

            Helper.VoidAsync<TaxRate>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
