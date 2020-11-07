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
//
using System.Collections.ObjectModel;
using Intuit.Ipp.DataService;

namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass] [Ignore]
    public class RecurringTransactionTest
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
        public void RecurringTransactionAddTestUsingoAuth()
        {
            //Creating the RecurringTransaction for Add
            RecurringTransaction recurringTransaction = QBOHelper.CreateRecurringTransaction(qboContextoAuth);
            //Adding the RecurringTransaction
            RecurringTransaction added = Helper.Add<RecurringTransaction>(qboContextoAuth, recurringTransaction);
            //Verify the added RecurringTransaction
            QBOHelper.VerifyRecurringTransaction(recurringTransaction, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void RecurringTransactionFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            RecurringTransactionAddTestUsingoAuth();

            //Retrieving the RecurringTransaction using FindAll
            List<RecurringTransaction> recurringTransactions = Helper.FindAll<RecurringTransaction>(qboContextoAuth, new RecurringTransaction(), 1, 500);
            Assert.IsNotNull(recurringTransactions);
            Assert.IsTrue(recurringTransactions.Count<RecurringTransaction>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void RecurringTransactionFindbyIdTestUsingoAuth()
        {
            //Creating the RecurringTransaction for Adding
            RecurringTransaction recurringTransaction = QBOHelper.CreateRecurringTransaction(qboContextoAuth);
            //Adding the RecurringTransaction
            RecurringTransaction added = Helper.Add<RecurringTransaction>(qboContextoAuth, recurringTransaction);
            RecurringTransaction found = Helper.FindById<RecurringTransaction>(qboContextoAuth, added);
            QBOHelper.VerifyRecurringTransaction(found, added);
        }

        #endregion

  

        #region Test cases for Delete Operations

        [TestMethod]
        public void RecurringTransactionDeleteTestUsingoAuth()
        //RecurringTransaction recur1 = new RecurringTransaction();
        //Invoice inv1 = new Invoice();
        //inv1.Id = r1.AnyIntuitObject.Id;
        //inv1.SyncToken = r1.AnyIntuitObject.SyncToken;
        //recur1.AnyIntuitObject = inv1;
        //var s = dataService.Delete(recur1);`
        {
            //Creating the RecurringTransaction for Adding
            RecurringTransaction recurringTransaction = QBOHelper.CreateRecurringTransaction(qboContextoAuth);
            //Adding the RecurringTransaction
            RecurringTransaction added = Helper.Add<RecurringTransaction>(qboContextoAuth, recurringTransaction);
            //Delete the returned entity
            try
            {
                RecurringTransaction deleted = Helper.Delete<RecurringTransaction>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [Ignore]
        public void RecurringTransactionVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            RecurringTransaction entity = QBOHelper.CreateRecurringTransaction(qboContextoAuth);
            //Adding the entity
            RecurringTransaction added = Helper.Add<RecurringTransaction>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                RecurringTransaction voided = Helper.Void<RecurringTransaction>(qboContextoAuth, added);
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
        public void RecurringTransactionCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            RecurringTransactionAddTestUsingoAuth();

            //Retrieving the RecurringTransaction using CDC
            List<RecurringTransaction> entities = Helper.CDC(qboContextoAuth, new RecurringTransaction(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<RecurringTransaction>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void RecurringTransactionBatchUsingoAuth()
        {
             Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            RecurringTransaction existing = Helper.FindOrAdd(qboContextoAuth, new RecurringTransaction());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateRecurringTransaction(qboContextoAuth));

            //batchEntries.Add(OperationEnum.update, QBOHelper.UpdateRecurringTransaction(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from RecurringTransaction");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<RecurringTransaction>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as RecurringTransaction).Id));
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
        public void RecurringTransactionQueryUsingoAuth()
        {
            QueryService<RecurringTransaction> entityQuery = new QueryService<RecurringTransaction>(qboContextoAuth);
            RecurringTransaction existing = Helper.FindOrAdd<RecurringTransaction>(qboContextoAuth, new RecurringTransaction());
            //List<RecurringTransaction> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            int count=entityQuery.ExecuteIdsQuery("Select * from RecurringTransaction where Id='"+existing.Id+"'").Count;
            Assert.IsTrue(count > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void RecurringTransactionAddAsyncTestsUsingoAuth()
        {
            //Creating the RecurringTransaction for Add
            RecurringTransaction entity = QBOHelper.CreateRecurringTransaction(qboContextoAuth);

            RecurringTransaction added = Helper.AddAsync<RecurringTransaction>(qboContextoAuth, entity);
            QBOHelper.VerifyRecurringTransaction(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void RecurringTransactionRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            RecurringTransactionAddTestUsingoAuth();

            //Retrieving the RecurringTransaction using FindAll
            Helper.FindAllAsync<RecurringTransaction>(qboContextoAuth, new RecurringTransaction());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void RecurringTransactionFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the RecurringTransaction for Adding
            RecurringTransaction entity = QBOHelper.CreateRecurringTransaction(qboContextoAuth);
            //Adding the RecurringTransaction
            RecurringTransaction added = Helper.Add<RecurringTransaction>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<RecurringTransaction>(qboContextoAuth, added);
        }

        #endregion


        #region Test Cases for Delete Operation

        [TestMethod]
        public void RecurringTransactionDeleteAsyncTestsUsingoAuth()
        {
            //Creating the RecurringTransaction for Adding
            RecurringTransaction entity = QBOHelper.CreateRecurringTransaction(qboContextoAuth);
            //Adding the RecurringTransaction
            RecurringTransaction added = Helper.Add<RecurringTransaction>(qboContextoAuth, entity);

            Helper.DeleteAsync<RecurringTransaction>(qboContextoAuth, added);
        }

        #endregion

     

        #endregion

        #endregion

    }
}
