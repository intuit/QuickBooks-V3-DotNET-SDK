using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    public class JournalCodeTest
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
        public void AddExpensesJournalCodeTestUsingoAuth()
        {
            //Creating the JournalCode for Add
            JournalCode journalCode = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Expenses);

            //Adding the JournalCode
            JournalCode added = Helper.Add<JournalCode>(qboContextoAuth, journalCode);
            //Verify the added Account
            QBOHelper.VerifyJournalCode(journalCode, added);

        }


        [TestMethod]
        public void AddSalesJournalCodeTestUsingoAuth()
        {
            //Creating the JournalCode for Add
            JournalCode journalCode = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Sales);

            //Adding the JournalCode
            JournalCode added = Helper.Add<JournalCode>(qboContextoAuth, journalCode);
            //Verify the added Account
            QBOHelper.VerifyJournalCode(journalCode, added);

        }


        [TestMethod]
        public void AddBankJournalCodeTestUsingoAuth()
        {
            //Creating the JournalCode for Add
            JournalCode journalCode = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Bank);

            //Adding the JournalCode
            JournalCode added = Helper.Add<JournalCode>(qboContextoAuth, journalCode);
            //Verify the added Account
            QBOHelper.VerifyJournalCode(journalCode, added);
        }



        [TestMethod]
        [Ignore] //IgnoreReason: check "language issue"
        public void AddNouveauxJournalCodeTestUsingoAuth()
        {
            //Creating the JournalCode for Add
            JournalCode journalCode = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Nouveaux);

            //Adding the JournalCode
            JournalCode added = Helper.Add<JournalCode>(qboContextoAuth, journalCode);
            //Verify the added Account
            QBOHelper.VerifyJournalCode(journalCode, added);
        }



        [TestMethod]
        public void AddWagesJournalCodeTestUsingoAuth()
        {
            //Creating the JournalCode for Add
            JournalCode journalCode = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Wages);

            //Adding the JournalCode
            JournalCode added = Helper.Add<JournalCode>(qboContextoAuth, journalCode);
            //Verify the added Account
            QBOHelper.VerifyJournalCode(journalCode, added);
        }


        [TestMethod]
        public void AddCashJournalCodeTestUsingoAuth()
        {
            //Creating the JournalCode for Add
            JournalCode journalCode = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Cash);

            //Adding the JournalCode
            JournalCode added = Helper.Add<JournalCode>(qboContextoAuth, journalCode);
            //Verify the added Account
            QBOHelper.VerifyJournalCode(journalCode, added);
        }



        [TestMethod]
        public void AddOthersJournalCodeTestUsingoAuth()
        {
            //Creating the JournalCode for Add
            JournalCode journalCode = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Others);

            //Adding the JournalCode
            JournalCode added = Helper.Add<JournalCode>(qboContextoAuth, journalCode);
            //Verify the added Account
            QBOHelper.VerifyJournalCode(journalCode, added);
        }


        #endregion Test cases for Add Operations

        #region Test cases for FindAll Operations

        [TestMethod]
        public void JournalCodeFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            AddExpensesJournalCodeTestUsingoAuth();

            //Retrieving the JournalCode using FindAll
            List<JournalCode> journalCodes = Helper.FindAll<JournalCode>(qboContextoAuth, new JournalCode(), 1, 500);
            Assert.IsNotNull(journalCodes);
            Assert.IsTrue(journalCodes.Count<JournalCode>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void JournalCodeFindbyIdTestUsingoAuth()
        {
            //Creating the JournalCode for Adding
            JournalCode journalCode = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Expenses);
            //Adding the journalCode
            JournalCode added = Helper.Add<JournalCode>(qboContextoAuth, journalCode);
            JournalCode found = Helper.FindById<JournalCode>(qboContextoAuth, added);
            QBOHelper.VerifyJournalCode(found, added);
        }

        #endregion

        #region Test cases for Update Operations
        //check
        [TestMethod]
        public void JournalCodeUpdateTestUsingoAuth()
        {
            //Creating the JournalCode for Adding
            JournalCode journalCode = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Expenses);
            //Adding the JournalCode
            JournalCode added = Helper.Add<JournalCode>(qboContextoAuth, journalCode);
            //Change the data of added entity
            JournalCode changed = QBOHelper.UpdateJournalCode(qboContextoAuth, added);
            //Update the returned entity data
            JournalCode updated = Helper.Update<JournalCode>(qboContextoAuth, changed);//Verify the updated JournalCode
            QBOHelper.VerifyJournalCode(changed, updated);
        }

        [TestMethod]
        
        public void JournalCodeSparseUpdateTestUsingoAuth()
        {
            //Creating the JournalCode for Adding
            JournalCode journalCode = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Expenses);
            //Adding the JournalCode
            JournalCode added = Helper.Add<JournalCode>(qboContextoAuth, journalCode);
            //Change the data of added entity
            JournalCode changed = QBOHelper.UpdateJournalCodeSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            JournalCode updated = Helper.Update<JournalCode>(qboContextoAuth, changed);//Verify the updated JournalCode
            QBOHelper.VerifyJournalCodeSparse(changed, updated);
        }
        //check
        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        [Ignore] //IgnoreReason: Not supported
        public void JournalCodeDeleteTestUsingoAuth()
        {
            //Creating the JournalCode for Adding
            JournalCode journalCode = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Expenses);
            //Adding the JournalCode
            JournalCode added = Helper.Add<JournalCode>(qboContextoAuth, journalCode);
            //Delete the returned entity
            try
            {
                JournalCode deleted = Helper.Delete<JournalCode>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        #endregion

        #region Test cases for Void Operations

        [TestMethod]
        [Ignore] //IgnoreReason: Not supported
        public void JournalCodeVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            JournalCode entity = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Expenses);
            //Adding the entity
            JournalCode added = Helper.Add<JournalCode>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                JournalCode voided = Helper.Void<JournalCode>(qboContextoAuth, added);
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
        [Ignore] //IgnoreReason: Not supported
        public void JournalCodeCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            AddExpensesJournalCodeTestUsingoAuth();

            //Retrieving the JournalCode using CDC
            List<JournalCode> entities = Helper.CDC(qboContextoAuth, new JournalCode(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<JournalCode>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void JournalCodeBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            JournalCode existing = Helper.FindOrAdd(qboContextoAuth, new JournalCode());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Expenses));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateJournalCode(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from JournalCode");

            

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<JournalCode>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as JournalCode).Id));
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
        public void JournalCodeQueryUsingoAuth()
        {
            QueryService<JournalCode> entityQuery = new QueryService<JournalCode>(qboContextoAuth);
            JournalCode existing = Helper.FindOrAdd<JournalCode>(qboContextoAuth, new JournalCode());
            List<JournalCode> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM JournalCode where Id='" + existing.Id + "'").ToList<JournalCode>();

            //List<JournalCode> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion


        #endregion


        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void JournalCodeAddAsyncTestsUsingoAuth()
        {
            //Creating the JournalCode for Add
            JournalCode entity = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Expenses);

            JournalCode added = Helper.AddAsync<JournalCode>(qboContextoAuth, entity);
            QBOHelper.VerifyJournalCode(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void JournalCodeRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            JournalCodeAddAsyncTestsUsingoAuth();

            //Retrieving the JournalCode using FindAll
            Helper.FindAllAsync<JournalCode>(qboContextoAuth, new JournalCode());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void JournalCodeFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the JournalCode for Adding
            JournalCode entity = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Expenses);
            //Adding the JournalCode
            JournalCode added = Helper.Add<JournalCode>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<JournalCode>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void JournalCodeUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the JournalCode for Adding
            JournalCode entity = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Expenses);
            //Adding the JournalCode
            JournalCode added = Helper.Add<JournalCode>(qboContextoAuth, entity);

            //Update the JournalCode
            JournalCode updated = QBOHelper.UpdateJournalCode(qboContextoAuth, added);
            //Call the service
            JournalCode updatedReturned = Helper.UpdateAsync<JournalCode>(qboContextoAuth, updated);
            //Verify updated JournalCode
            QBOHelper.VerifyJournalCode(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        [Ignore] //IgnoreReason: Not supported
        public void JournalCodeDeleteAsyncTestsUsingoAuth()
        {
            //Creating the JournalCode for Adding
            JournalCode entity = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Expenses);
            //Adding the JournalCode
            JournalCode added = Helper.Add<JournalCode>(qboContextoAuth, entity);

            Helper.DeleteAsync<JournalCode>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        [Ignore] //IgnoreReason: Not supported
        public void JournalCodeVoidAsyncTestsUsingoAuth()
        {
            //Creating the JournalCode for Adding
            JournalCode entity = QBOHelper.CreateJournalCode(qboContextoAuth, JournalCodeTypeEnum.Expenses);
            //Adding the JournalCode
            JournalCode added = Helper.Add<JournalCode>(qboContextoAuth, entity);

            Helper.VoidAsync<JournalCode>(qboContextoAuth, added);
        }

        #endregion

        #endregion
      
        #endregion

    }
}
