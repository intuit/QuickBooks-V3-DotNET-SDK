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
    public class TaxCodeTest
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
        public void TaxCodeAddTestUsingoAuth()
        {
            //Creating the TaxCode for Add
            TaxCode taxCode = QBOHelper.CreateTaxCode(qboContextoAuth);
            //Adding the TaxCode
            TaxCode added = Helper.Add<TaxCode>(qboContextoAuth, taxCode);
            //Verify the added TaxCode
            QBOHelper.VerifyTaxCode(taxCode, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void TaxCodeFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            //TaxCodeAddTestUsingoAuth();

            //Retrieving the TaxCode using FindAll
            List<TaxCode> taxCodes = Helper.FindAll<TaxCode>(qboContextoAuth, new TaxCode(), 1, 500);
            Assert.IsNotNull(taxCodes);
            Assert.IsTrue(taxCodes.Count<TaxCode>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void TaxCodeFindbyIdTestUsingoAuth()
        {
            //Creating the TaxCode for Adding
            //TaxCode taxCode = QBOHelper.CreateTaxCode(qboContextoAuth);
            //Adding the TaxCode
            TaxCode added = Helper.FindOrAdd<TaxCode>(qboContextoAuth, new TaxCode());
            TaxCode taxCode = Helper.FindAll<TaxCode>(qboContextoAuth, new TaxCode())[3];

            TaxCode found = Helper.FindById<TaxCode>(qboContextoAuth, taxCode);
            QBOHelper.VerifyTaxCode(found, taxCode);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod][Ignore]
        public void TaxCodeUpdateTestUsingoAuth()
        {
            //Creating the TaxCode for Adding
            //TaxCode taxCode = QBOHelper.CreateTaxCode(qboContextoAuth);


            //Adding the TaxCode
            TaxCode added = Helper.FindOrAdd<TaxCode>(qboContextoAuth, new TaxCode());
            if (added != null)
            {
                //Change the data of added entity
                TaxCode changed = QBOHelper.UpdateTaxCode(qboContextoAuth, added);
                //Update the returned entity data
                TaxCode updated = Helper.Update<TaxCode>(qboContextoAuth, changed);//Verify the updated TaxCode
                QBOHelper.VerifyTaxCode(changed, updated);
            }
        }


        [TestMethod][Ignore]
        public void TaxCodeSparseUpdateTestUsingoAuth()
        {
            //Creating the TaxCode for Adding
            //TaxCode taxCode = QBOHelper.CreateTaxCode(qboContextoAuth);
            //Adding the TaxCode
            TaxCode added = Helper.FindOrAdd<TaxCode>(qboContextoAuth, new TaxCode());
            if (added != null)
            {
                //Change the data of added entity
                TaxCode changed = QBOHelper.SparseUpdateTaxCode(qboContextoAuth, added.Id, added.SyncToken);
                //Update the returned entity data
                TaxCode updated = Helper.Update<TaxCode>(qboContextoAuth, changed);//Verify the updated TaxCode
                QBOHelper.VerifyTaxCodeSparseUpdate(changed, updated);
            }
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod][Ignore]
        public void TaxCodeDeleteTestUsingoAuth()
        {
            //Creating the TaxCode for Adding
            TaxCode taxCode = QBOHelper.CreateTaxCode(qboContextoAuth);
            //Adding the TaxCode
            TaxCode added = Helper.Add<TaxCode>(qboContextoAuth, taxCode);
            //Delete the returned entity
            try
            {
                TaxCode deleted = Helper.Delete<TaxCode>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }


        [TestMethod][Ignore]
        public void TaxCodeVoidTestUsingoAuth()
        {
            //Creating the TaxCode for Adding
            TaxCode taxCode = QBOHelper.CreateTaxCode(qboContextoAuth);
            //Adding the TaxCode
            TaxCode added = Helper.Add<TaxCode>(qboContextoAuth, taxCode);
            //Void the returned entity
            try
            {
                TaxCode voided = Helper.Void<TaxCode>(qboContextoAuth, added);
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
        public void TaxCodeCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            //TaxCodeAddTestUsingoAuth();

            //Retrieving the TaxCode using FindAll
            List<TaxCode> taxCodes = Helper.CDC(qboContextoAuth, new TaxCode(), DateTime.Today.AddDays(-100));
            if(taxCodes==null)
            {
                Assert.Inconclusive("No tax codes returned in CDC response");
            }
            Assert.IsNotNull(taxCodes);
            Assert.IsTrue(taxCodes.Count<TaxCode>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void TaxCodeBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            //TaxCode existing = Helper.FindOrAdd(qboContextoAuth, new TaxCode());

            //batchEntries.Add(OperationEnum.create, QBOHelper.CreateTaxCode(qboContextoAuth));

            //batchEntries.Add(OperationEnum.update, QBOHelper.UpdateTaxCode(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from TaxCode");

            //batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<TaxCode>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {
                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as TaxCode).Id));
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
        public void TaxCodeQueryUsingoAuth()
        {
            QueryService<TaxCode> entityQuery = new QueryService<TaxCode>(qboContextoAuth);
            TaxCode added = Helper.FindOrAdd<TaxCode>(qboContextoAuth, new TaxCode());
            TaxCode taxCode = Helper.FindAll<TaxCode>(qboContextoAuth, new TaxCode())[3];
            //List<TaxCode> entities = entityQuery.Where(c => c.Id == taxCode.Id).ToList();
            List<TaxCode> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM TaxCode").ToList<TaxCode>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod] [Ignore]
        public void TaxCodeAddAsyncTestsUsingoAuth()
        {
            //Creating the TaxCode for Add
            TaxCode entity = QBOHelper.CreateTaxCode(qboContextoAuth);

            TaxCode added = Helper.AddAsync<TaxCode>(qboContextoAuth, entity);
            QBOHelper.VerifyTaxCode(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void TaxCodeRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            //TaxCodeAddTestUsingoAuth();

            //Retrieving the TaxCode using FindAll
            Helper.FindAllAsync<TaxCode>(qboContextoAuth, new TaxCode());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void TaxCodeFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the TaxCode for Adding
            //TaxCode entity = QBOHelper.CreateTaxCode(qboContextoAuth);
            //Adding the TaxCode
            TaxCode added = Helper.FindOrAdd<TaxCode>(qboContextoAuth, new TaxCode());
            TaxCode taxCode = Helper.FindAll<TaxCode>(qboContextoAuth, new TaxCode())[3];

            //FindById and verify
            Helper.FindByIdAsync<TaxCode>(qboContextoAuth, taxCode);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod][Ignore]
        public void TaxCodeUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the TaxCode for Adding
            //TaxCode entity = QBOHelper.CreateTaxCode(qboContextoAuth);
            //Adding the TaxCode
            TaxCode added = Helper.FindOrAdd<TaxCode>(qboContextoAuth, new TaxCode());

            if (added != null)
            {
                //Update the TaxCode
                TaxCode updated = QBOHelper.UpdateTaxCode(qboContextoAuth, added);
                //Call the service
                TaxCode updatedReturned = Helper.UpdateAsync<TaxCode>(qboContextoAuth, updated);
                //Verify updated TaxCode
                QBOHelper.VerifyTaxCode(updated, updatedReturned);
            }
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod][Ignore]
        public void TaxCodeDeleteAsyncTestsUsingoAuth()
        {
            //Creating the TaxCode for Adding
            //TaxCode entity = QBOHelper.CreateTaxCode(qboContextoAuth);
            //Adding the TaxCode
            TaxCode added = Helper.FindOrAdd<TaxCode>(qboContextoAuth, new TaxCode());

            Helper.DeleteAsync<TaxCode>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod][Ignore]
        public void TaxCodeVoidAsyncTestsUsingoAuth()
        {
            //Creating the TaxCode for Adding
            //TaxCode entity = QBOHelper.CreateTaxCode(qboContextoAuth);
            //Adding the TaxCode
            TaxCode added = Helper.FindOrAdd<TaxCode>(qboContextoAuth, new TaxCode());

            Helper.VoidAsync<TaxCode>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
