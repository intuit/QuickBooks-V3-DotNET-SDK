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
    public class JournalEntryTest
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
        public void JournalEntryAddTestUsingoAuth()
        {
            //Creating the JournalEntry for Add
            JournalEntry journalEntry = QBOHelper.CreateJournalEntry(qboContextoAuth);
            //Adding the JournalEntry
            JournalEntry added = Helper.Add<JournalEntry>(qboContextoAuth, journalEntry);
            //Verify the added JournalEntry
            QBOHelper.VerifyJournalEntry(journalEntry, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void JournalEntryFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            JournalEntryAddTestUsingoAuth();

            //Retrieving the JournalEntry using FindAll
            List<JournalEntry> journalEntrys = Helper.FindAll<JournalEntry>(qboContextoAuth, new JournalEntry(), 1, 500);
            Assert.IsNotNull(journalEntrys);
            Assert.IsTrue(journalEntrys.Count<JournalEntry>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void JournalEntryFindbyIdTestUsingoAuth()
        {
            //Creating the JournalEntry for Adding
            JournalEntry journalEntry = QBOHelper.CreateJournalEntry(qboContextoAuth);
            //Adding the JournalEntry
            JournalEntry added = Helper.Add<JournalEntry>(qboContextoAuth, journalEntry);
            JournalEntry found = Helper.FindById<JournalEntry>(qboContextoAuth, added);
            QBOHelper.VerifyJournalEntry(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void JournalEntryUpdateTestUsingoAuth()
        {
            //Creating the JournalEntry for Adding
            JournalEntry journalEntry = QBOHelper.CreateJournalEntry(qboContextoAuth);
            //Adding the JournalEntry
            JournalEntry added = Helper.Add<JournalEntry>(qboContextoAuth, journalEntry);
            //Change the data of added entity
            JournalEntry changed = QBOHelper.UpdateJournalEntry(qboContextoAuth, added);
            //Update the returned entity data
            JournalEntry updated = Helper.Update<JournalEntry>(qboContextoAuth, changed);//Verify the updated JournalEntry
            QBOHelper.VerifyJournalEntry(changed, updated);
        }

        [TestMethod]
        public void JournalEntrySparseUpdateTestUsingoAuth()
        {
            //Creating the JournalEntry for Adding
            JournalEntry journalEntry = QBOHelper.CreateJournalEntry(qboContextoAuth);
            //Adding the JournalEntry
            JournalEntry added = Helper.Add<JournalEntry>(qboContextoAuth, journalEntry);
            //Change the data of added entity
            JournalEntry changed = QBOHelper.UpdateJournalEntrySparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            JournalEntry updated = Helper.Update<JournalEntry>(qboContextoAuth, changed);//Verify the updated JournalEntry
            QBOHelper.VerifyJournalEntrySparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void JournalEntryDeleteTestUsingoAuth()
        {
            //Creating the JournalEntry for Adding
            JournalEntry journalEntry = QBOHelper.CreateJournalEntry(qboContextoAuth);
            //Adding the JournalEntry
            JournalEntry added = Helper.Add<JournalEntry>(qboContextoAuth, journalEntry);
            //Delete the returned entity
            try
            {
                JournalEntry deleted = Helper.Delete<JournalEntry>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod] [Ignore] //IgnoreReason: Not supported
        public void JournalEntryVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            JournalEntry entity = QBOHelper.CreateJournalEntry(qboContextoAuth);
            //Adding the entity
            JournalEntry added = Helper.Add<JournalEntry>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                JournalEntry voided = Helper.Void<JournalEntry>(qboContextoAuth, added);
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
        public void JournalEntryCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            JournalEntryAddTestUsingoAuth();

            //Retrieving the JournalEntry using CDC
            List<JournalEntry> entities = Helper.CDC(qboContextoAuth, new JournalEntry(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<JournalEntry>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void JournalEntryBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            JournalEntry existing = Helper.FindOrAdd(qboContextoAuth, new JournalEntry());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateJournalEntry(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateJournalEntry(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from JournalEntry");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<JournalEntry>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as JournalEntry).Id));
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
        public void JournalEntryQueryUsingoAuth()
        {
            QueryService<JournalEntry> entityQuery = new QueryService<JournalEntry>(qboContextoAuth);
            JournalEntry existing = Helper.FindOrAdd<JournalEntry>(qboContextoAuth, new JournalEntry());
            //List<JournalEntry> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<JournalEntry> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM JournalEntry where Id='" + existing.Id + "'").ToList<JournalEntry>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion
        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void JournalEntryAddAsyncTestsUsingoAuth()
        {
            //Creating the JournalEntry for Add
            JournalEntry entity = QBOHelper.CreateJournalEntry(qboContextoAuth);

            JournalEntry added = Helper.AddAsync<JournalEntry>(qboContextoAuth, entity);
            QBOHelper.VerifyJournalEntry(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void JournalEntryRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            JournalEntryAddTestUsingoAuth();

            //Retrieving the JournalEntry using FindAll
            Helper.FindAllAsync<JournalEntry>(qboContextoAuth, new JournalEntry());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void JournalEntryFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the JournalEntry for Adding
            JournalEntry entity = QBOHelper.CreateJournalEntry(qboContextoAuth);
            //Adding the JournalEntry
            JournalEntry added = Helper.Add<JournalEntry>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<JournalEntry>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void JournalEntryUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the JournalEntry for Adding
            JournalEntry entity = QBOHelper.CreateJournalEntry(qboContextoAuth);
            //Adding the JournalEntry
            JournalEntry added = Helper.Add<JournalEntry>(qboContextoAuth, entity);

            //Update the JournalEntry
            JournalEntry updated = QBOHelper.UpdateJournalEntry(qboContextoAuth, added);
            //Call the service
            JournalEntry updatedReturned = Helper.UpdateAsync<JournalEntry>(qboContextoAuth, updated);
            //Verify updated JournalEntry
            QBOHelper.VerifyJournalEntry(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void JournalEntryDeleteAsyncTestsUsingoAuth()
        {
            //Creating the JournalEntry for Adding
            JournalEntry entity = QBOHelper.CreateJournalEntry(qboContextoAuth);
            //Adding the JournalEntry
            JournalEntry added = Helper.Add<JournalEntry>(qboContextoAuth, entity);

            Helper.DeleteAsync<JournalEntry>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore] //IgnoreReason: Not supported
        public void JournalEntryVoidAsyncTestsUsingoAuth()
        {
            //Creating the JournalEntry for Adding
            JournalEntry entity = QBOHelper.CreateJournalEntry(qboContextoAuth);
            //Adding the JournalEntry
            JournalEntry added = Helper.Add<JournalEntry>(qboContextoAuth, entity);

            Helper.VoidAsync<JournalEntry>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
