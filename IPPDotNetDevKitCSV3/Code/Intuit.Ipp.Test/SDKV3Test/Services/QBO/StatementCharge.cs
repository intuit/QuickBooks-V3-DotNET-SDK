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
    public class StatementChargeTest
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
        public void StatementChargeAddTestUsingoAuth()
        {
            //Creating the StatementCharge for Add
            StatementCharge statementCharge = QBOHelper.CreateStatementCharge(qboContextoAuth);
            //Adding the StatementCharge
            StatementCharge added = Helper.Add<StatementCharge>(qboContextoAuth, statementCharge);
            //Verify the added StatementCharge
            QBOHelper.VerifyStatementCharge(statementCharge, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void StatementChargeFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            StatementChargeAddTestUsingoAuth();

            //Retrieving the StatementCharge using FindAll
            List<StatementCharge> statementCharges = Helper.FindAll<StatementCharge>(qboContextoAuth, new StatementCharge(), 1, 500);
            Assert.IsNotNull(statementCharges);
            Assert.IsTrue(statementCharges.Count<StatementCharge>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void StatementChargeFindbyIdTestUsingoAuth()
        {
            //Creating the StatementCharge for Adding
            StatementCharge statementCharge = QBOHelper.CreateStatementCharge(qboContextoAuth);
            //Adding the StatementCharge
            StatementCharge added = Helper.Add<StatementCharge>(qboContextoAuth, statementCharge);
            StatementCharge found = Helper.FindById<StatementCharge>(qboContextoAuth, added);
            QBOHelper.VerifyStatementCharge(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void StatementChargeUpdateTestUsingoAuth()
        {
            //Creating the StatementCharge for Adding
            StatementCharge statementCharge = QBOHelper.CreateStatementCharge(qboContextoAuth);
            //Adding the StatementCharge
            StatementCharge added = Helper.Add<StatementCharge>(qboContextoAuth, statementCharge);
            //Change the data of added entity
            StatementCharge changed = QBOHelper.UpdateStatementCharge(qboContextoAuth, added);
            //Update the returned entity data
            StatementCharge updated = Helper.Update<StatementCharge>(qboContextoAuth, changed);//Verify the updated StatementCharge
            QBOHelper.VerifyStatementCharge(changed, updated);
        }

        [TestMethod]
        public void StatementChargeSparseUpdateTestUsingoAuth()
        {
            //Creating the StatementCharge for Adding
            StatementCharge statementCharge = QBOHelper.CreateStatementCharge(qboContextoAuth);
            //Adding the StatementCharge
            StatementCharge added = Helper.Add<StatementCharge>(qboContextoAuth, statementCharge);
            //Change the data of added entity
            StatementCharge changed = QBOHelper.UpdateStatementChargeSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            StatementCharge updated = Helper.Update<StatementCharge>(qboContextoAuth, changed);//Verify the updated StatementCharge
            QBOHelper.VerifyStatementChargeSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void StatementChargeDeleteTestUsingoAuth()
        {
            //Creating the StatementCharge for Adding
            StatementCharge statementCharge = QBOHelper.CreateStatementCharge(qboContextoAuth);
            //Adding the StatementCharge
            StatementCharge added = Helper.Add<StatementCharge>(qboContextoAuth, statementCharge);
            //Delete the returned entity
            try
            {
                StatementCharge deleted = Helper.Delete<StatementCharge>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void StatementChargeVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            StatementCharge entity = QBOHelper.CreateStatementCharge(qboContextoAuth);
            //Adding the entity
            StatementCharge added = Helper.Add<StatementCharge>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                StatementCharge voided = Helper.Void<StatementCharge>(qboContextoAuth, added);
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
        public void StatementChargeCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            StatementChargeAddTestUsingoAuth();

            //Retrieving the StatementCharge using CDC
            List<StatementCharge> entities = Helper.CDC(qboContextoAuth, new StatementCharge(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<StatementCharge>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void StatementChargeBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            StatementCharge existing = Helper.FindOrAdd(qboContextoAuth, new StatementCharge());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateStatementCharge(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateStatementCharge(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from StatementCharge");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<StatementCharge>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as StatementCharge).Id));
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
        public void StatementChargeQueryUsingoAuth()
        {
            QueryService<StatementCharge> entityQuery = new QueryService<StatementCharge>(qboContextoAuth);
            StatementCharge existing = Helper.FindOrAdd<StatementCharge>(qboContextoAuth, new StatementCharge());
            List<StatementCharge> entities = entityQuery.ExecuteIdsQuery("Select * from StatementCharge where Id =='"+ existing.Id+"'").ToList();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void StatementChargeAddAsyncTestsUsingoAuth()
        {
            //Creating the StatementCharge for Add
            StatementCharge entity = QBOHelper.CreateStatementCharge(qboContextoAuth);

            StatementCharge added = Helper.AddAsync<StatementCharge>(qboContextoAuth, entity);
            QBOHelper.VerifyStatementCharge(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void StatementChargeRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            StatementChargeAddTestUsingoAuth();

            //Retrieving the StatementCharge using FindAll
            Helper.FindAllAsync<StatementCharge>(qboContextoAuth, new StatementCharge());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void StatementChargeFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the StatementCharge for Adding
            StatementCharge entity = QBOHelper.CreateStatementCharge(qboContextoAuth);
            //Adding the StatementCharge
            StatementCharge added = Helper.Add<StatementCharge>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<StatementCharge>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void StatementChargeUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the StatementCharge for Adding
            StatementCharge entity = QBOHelper.CreateStatementCharge(qboContextoAuth);
            //Adding the StatementCharge
            StatementCharge added = Helper.Add<StatementCharge>(qboContextoAuth, entity);

            //Update the StatementCharge
            StatementCharge updated = QBOHelper.UpdateStatementCharge(qboContextoAuth, added);
            //Call the service
            StatementCharge updatedReturned = Helper.UpdateAsync<StatementCharge>(qboContextoAuth, updated);
            //Verify updated StatementCharge
            QBOHelper.VerifyStatementCharge(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void StatementChargeDeleteAsyncTestsUsingoAuth()
        {
            //Creating the StatementCharge for Adding
            StatementCharge entity = QBOHelper.CreateStatementCharge(qboContextoAuth);
            //Adding the StatementCharge
            StatementCharge added = Helper.Add<StatementCharge>(qboContextoAuth, entity);

            Helper.DeleteAsync<StatementCharge>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void StatementChargeVoidAsyncTestsUsingoAuth()
        {
            //Creating the StatementCharge for Adding
            StatementCharge entity = QBOHelper.CreateStatementCharge(qboContextoAuth);
            //Adding the StatementCharge
            StatementCharge added = Helper.Add<StatementCharge>(qboContextoAuth, entity);

            Helper.VoidAsync<StatementCharge>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
