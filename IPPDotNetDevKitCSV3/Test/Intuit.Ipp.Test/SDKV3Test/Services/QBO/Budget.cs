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
    public class BudgetTest
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
        public void BudgetAddTestUsingoAuth()
        {
            //Creating the Budget for Add
            Budget Budget = QBOHelper.CreateBudget(qboContextoAuth);
            //Adding the Budget
            Budget added = Helper.Add<Budget>(qboContextoAuth, Budget);
            //Verify the added Budget
            QBOHelper.VerifyBudget(Budget, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        [Ignore]
        public void BudgetFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            BudgetAddTestUsingoAuth();

            //Retrieving the Budget using FindAll
            List<Budget> Budgets = Helper.FindAll<Budget>(qboContextoAuth, new Budget(), 1, 500);
            Assert.IsNotNull(Budgets);
            Assert.IsTrue(Budgets.Count<Budget>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        [Ignore]
        public void BudgetFindbyIdTestUsingoAuth()
        {
            //Creating the Budget for Adding
            Budget Budget = QBOHelper.CreateBudget(qboContextoAuth);
            //Adding the Budget
            Budget added = Helper.Add<Budget>(qboContextoAuth, Budget);
            Budget found = Helper.FindById<Budget>(qboContextoAuth, added);
            QBOHelper.VerifyBudget(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        [Ignore]
        public void BudgetUpdateTestUsingoAuth()
        {
            //Creating the Budget for Adding
            Budget Budget = QBOHelper.CreateBudget(qboContextoAuth);
            //Adding the Budget
            Budget added = Helper.Add<Budget>(qboContextoAuth, Budget);
            //Change the data of added entity
            Budget changed = QBOHelper.UpdateBudget(qboContextoAuth, added);
            //Update the returned entity data
            Budget updated = Helper.Update<Budget>(qboContextoAuth, changed);//Verify the updated Budget
            QBOHelper.VerifyBudget(changed, updated);
        }

        [TestMethod]
        [Ignore]
        public void BudgetSparseUpdateTestUsingoAuth()
        {
            //Creating the Budget for Adding
            Budget Budget = QBOHelper.CreateBudget(qboContextoAuth);
            //Adding the Budget
            Budget added = Helper.Add<Budget>(qboContextoAuth, Budget);
            //Change the data of added entity
            Budget changed = QBOHelper.UpdateBudgetSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Budget updated = Helper.Update<Budget>(qboContextoAuth, changed);//Verify the updated Budget
            QBOHelper.VerifyBudgetSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        [Ignore]
        public void BudgetDeleteTestUsingoAuth()
        {
            //Creating the Budget for Adding
            Budget Budget = QBOHelper.CreateBudget(qboContextoAuth);
            //Adding the Budget
            Budget added = Helper.Add<Budget>(qboContextoAuth, Budget);
            //Delete the returned entity
            try
            {
                Budget deleted = Helper.Delete<Budget>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [Ignore]
        public void BudgetVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            Budget entity = QBOHelper.CreateBudget(qboContextoAuth);
            //Adding the entity
            Budget added = Helper.Add<Budget>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                Budget voided = Helper.Void<Budget>(qboContextoAuth, added);
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
        public void BudgetCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            BudgetAddTestUsingoAuth();

            //Retrieving the Budget using CDC
            List<Budget> entities = Helper.CDC(qboContextoAuth, new Budget(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<Budget>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        [Ignore]
        public void BudgetBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Budget existing = Helper.FindOrAdd(qboContextoAuth, new Budget());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateBudget(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateBudget(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Budget");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Budget>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Budget).Id));
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
        public void BudgetQueryUsingoAuth()
        {
            QueryService<Budget> entityQuery = new QueryService<Budget>(qboContextoAuth);
            //Budget existing = Helper.FindOrAdd<Budget>(qboContextoAuth, new Budget());
            List<Budget> entities = entityQuery.ExecuteIdsQuery("select * from Budget").ToList();
            Assert.IsTrue(entities.Count() >= 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod][Ignore]
        public void BudgetAddAsyncTestsUsingoAuth()
        {
            //Creating the Budget for Add
            Budget entity = QBOHelper.CreateBudget(qboContextoAuth);

            Budget added = Helper.AddAsync<Budget>(qboContextoAuth, entity);
            QBOHelper.VerifyBudget(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        [Ignore]
        public void BudgetRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            BudgetAddTestUsingoAuth();

            //Retrieving the Budget using FindAll
            Helper.FindAllAsync<Budget>(qboContextoAuth, new Budget());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        [Ignore]
        public void BudgetFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Budget for Adding
            Budget entity = QBOHelper.CreateBudget(qboContextoAuth);
            //Adding the Budget
            Budget added = Helper.Add<Budget>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Budget>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        [Ignore]
        public void BudgetUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Budget for Adding
            Budget entity = QBOHelper.CreateBudget(qboContextoAuth);
            //Adding the Budget
            Budget added = Helper.Add<Budget>(qboContextoAuth, entity);

            //Update the Budget
            Budget updated = QBOHelper.UpdateBudget(qboContextoAuth, added);
            //Call the service
            Budget updatedReturned = Helper.UpdateAsync<Budget>(qboContextoAuth, updated);
            //Verify updated Budget
            QBOHelper.VerifyBudget(updated, updatedReturned);
        }

        [TestMethod]
        [Ignore]
        public void BudgetSparseUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Budget for Adding
            Budget entity = QBOHelper.CreateBudget(qboContextoAuth);
            //Adding the Budget
            Budget added = Helper.Add<Budget>(qboContextoAuth, entity);

            //Update the Budget
            Budget updated = QBOHelper.UpdateBudgetSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Call the service
            Budget updatedReturned = Helper.UpdateAsync<Budget>(qboContextoAuth, updated);
            //Verify updated Budget
            QBOHelper.VerifyBudgetSparse(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        [Ignore]
        public void BudgetDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Budget for Adding
            Budget entity = QBOHelper.CreateBudget(qboContextoAuth);
            //Adding the Budget
            Budget added = Helper.Add<Budget>(qboContextoAuth, entity);

            Helper.DeleteAsync<Budget>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        [Ignore]
        public void BudgetVoidAsyncTestsUsingoAuth()
        {
            //Creating the Budget for Adding
            Budget entity = QBOHelper.CreateBudget(qboContextoAuth);
            //Adding the Budget
            Budget added = Helper.Add<Budget>(qboContextoAuth, entity);

            Helper.VoidAsync<Budget>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
