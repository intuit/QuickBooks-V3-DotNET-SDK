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
using Intuit.Ipp.LinqExtender;

namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass]
    public class AccountTest
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
        public void AddBankAccountTestUsingoAuth()
        {
            //Creating the Account for Add
            Account account = QBOHelper.CreateAccount(qboContextoAuth, AccountTypeEnum.Bank, AccountClassificationEnum.Asset);
            //Adding the Account
            Account added = Helper.Add<Account>(qboContextoAuth, account);
            //Verify the added Account
            QBOHelper.VerifyAccount(account, added);
        }


        [TestMethod]
        public void AddCreditCardAccountTestUsingoAuth()
        {
            //Creating the Account for Add
            Account account = QBOHelper.CreateAccount(qboContextoAuth, AccountTypeEnum.CreditCard, AccountClassificationEnum.Liability);
            //Adding the Account
            Account added = Helper.Add<Account>(qboContextoAuth, account);
            //Verify the added Account
            QBOHelper.VerifyAccount(account, added);
        }

        [TestMethod] [Ignore] //TestComment: Test if entity type is not supported
        public void AddEquityBankAccountTestUsingoAuth()
        {
            //Creating the Account for Add
            Account account = QBOHelper.CreateAccount(qboContextoAuth, AccountTypeEnum.Equity, AccountClassificationEnum.Equity);
            //Adding the Account
            Account added = Helper.Add<Account>(qboContextoAuth, account);
            //Verify the added Account
            QBOHelper.VerifyAccount(account, added);
        }

        [TestMethod]
        public void AddExpenseAccountTestUsingoAuth()
        {
            //Creating the Account for Add
            Account account = QBOHelper.CreateAccount(qboContextoAuth, AccountTypeEnum.Expense, AccountClassificationEnum.Expense);
            //Adding the Account
            Account added = Helper.Add<Account>(qboContextoAuth, account);
            //Verify the added Account
            QBOHelper.VerifyAccount(account, added);
        }


        [TestMethod]
        public void AddAccountWithAccAliasTestUsingoAuthFrance()
        {
            //Creating the Account for Add
           // Account account = QBOHelper.CreateAccount(qboContextoAuth, AccountTypeEnum.Expense, AccountClassificationEnum.Expense);
            Account account = QBOHelper.CreateAccountFrance(qboContextoAuth, AccountTypeEnum.Expense, AccountClassificationEnum.Expense);
            //Adding the Account
            Account added = Helper.Add<Account>(qboContextoAuth, account);
            //Verify the added Account
          //  QBOHelper.VerifyAccount(account, added);
             QBOHelper.VerifyAccountFrance(account, added);
        }
        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void AccountFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            AddBankAccountTestUsingoAuth();

            //Retrieving the Account using FindAll
            List<Account> accounts = Helper.FindAll<Account>(qboContextoAuth, new Account(), 1, 500);
            Assert.IsNotNull(accounts);
            Assert.IsTrue(accounts.Count<Account>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void AccountFindbyIdTestUsingoAuth()
        {
            //Creating the Account for Adding
            Account account = QBOHelper.CreateAccount(qboContextoAuth, AccountTypeEnum.Bank, AccountClassificationEnum.Asset);
            //Adding the Account
            Account added = Helper.Add<Account>(qboContextoAuth, account);
            Account found = Helper.FindById<Account>(qboContextoAuth, added);
            QBOHelper.VerifyAccount(found, added);
        }

        [TestMethod]
        [ExpectedException(typeof(IdsException))]
        public void AccountFindbyInvalidIdTestUsingoAuth()
        {
            Account added = new Account() { Id = "100000" };
            Account found = Helper.FindById<Account>(qboContextoAuth, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void AccountUpdateTestUsingoAuth()
        {
            //Creating the Account for Adding
            Account account = QBOHelper.CreateAccount(qboContextoAuth, AccountTypeEnum.Bank, AccountClassificationEnum.Asset);
            //Adding the Account
            Account added = Helper.Add<Account>(qboContextoAuth, account);
            //Change the data of added entity
            Account changed = QBOHelper.UpdateAccount(qboContextoAuth, added);
            //Update the returned entity data
            Account updated = Helper.Update<Account>(qboContextoAuth, changed);//Verify the updated Account
            QBOHelper.VerifyAccount(changed, updated);
        }

        [TestMethod]
        public void AccountSparseUpdateTestUsingoAuth()
        {
            //Creating the Account for Adding
            Account account = QBOHelper.CreateAccount(qboContextoAuth, AccountTypeEnum.Bank, AccountClassificationEnum.Asset);
            //Adding the Account
            Account added = Helper.Add<Account>(qboContextoAuth, account);
            //Change the data of added entity
            Account changed = QBOHelper.SparseUpdateAccount(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Account updated = Helper.Update<Account>(qboContextoAuth, changed);//Verify the updated Account
            QBOHelper.VerifyAccountSparseUpdate(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod] [Ignore]  //TestComment: Returns Operation Not Supported:  DevKit tracker Item #150
        public void AccountDeleteTestUsingoAuth()
        {
            //Creating the Account for Adding
            Account account = QBOHelper.CreateAccount(qboContextoAuth, AccountTypeEnum.Bank, AccountClassificationEnum.Asset);
            //Adding the Account
            Account added = Helper.Add<Account>(qboContextoAuth, account);
            //Delete the returned entity
            try
            {
                Account deleted = Helper.Delete<Account>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod] [Ignore]
        public void AccountVoidTestUsingoAuth()
        {
            //Creating the Account for Adding
            Account account = QBOHelper.CreateAccount(qboContextoAuth, AccountTypeEnum.Bank, AccountClassificationEnum.Asset);
            //Adding the Account
            Account added = Helper.Add<Account>(qboContextoAuth, account);
            //Void the returned entity
            try
            {
                Account voided = Helper.Void<Account>(qboContextoAuth, added);
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
        public void AccountCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            AddBankAccountTestUsingoAuth();

            //Retrieving the Entity using FindAll
            List<Account> entities = Helper.CDC(qboContextoAuth, new Account(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<Account>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void AccountBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Account existing = Helper.FindOrAdd(qboContextoAuth, new Account());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateAccount(qboContextoAuth, AccountTypeEnum.Bank, AccountClassificationEnum.Asset));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateAccount(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Account");

            //IgnoreReason: Not Supported in v67
            //batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Account>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Account).Id));
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
        public void AccountQueryUsingoAuth()
        {
            QueryService<Account> entityQuery = new QueryService<Account>(qboContextoAuth);
            Account existing = Helper.FindOrAddAccount(qboContextoAuth, AccountTypeEnum.Bank, AccountClassificationEnum.Expense);
            //List<Account> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<Account> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Account where Id='" + existing.Id + "'").ToList<Account>();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void AccountAddAsyncTestsUsingoAuth()
        {
            //Creating the Account for Add
            Account entity = QBOHelper.CreateAccount(qboContextoAuth, AccountTypeEnum.Bank, AccountClassificationEnum.Expense);

            Account added = Helper.AddAsync<Account>(qboContextoAuth, entity);
            QBOHelper.VerifyAccount(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void AccountRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            AddBankAccountTestUsingoAuth();

            //Retrieving the Account using FindAll
            Helper.FindAllAsync<Account>(qboContextoAuth, new Account());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void AccountFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Account for Adding
            Account entity = QBOHelper.CreateAccount(qboContextoAuth, AccountTypeEnum.Bank, AccountClassificationEnum.Expense);
            //Adding the Account
            Account added = Helper.Add<Account>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Account>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void AccountUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Account for Adding
            Account entity = QBOHelper.CreateAccount(qboContextoAuth, AccountTypeEnum.Bank, AccountClassificationEnum.Expense);
            //Adding the Account
            Account added = Helper.Add<Account>(qboContextoAuth, entity);

            //Update the Account
            Account updated = QBOHelper.UpdateAccount(qboContextoAuth, added);
            //Call the service
            Account updatedReturned = Helper.UpdateAsync<Account>(qboContextoAuth, updated);
            //Verify updated Account
            QBOHelper.VerifyAccount(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod] [Ignore] //TestComment: Returns Operation Not Supported:  DevKit tracker Item #150
        public void AccountDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Account for Adding
            Account entity = QBOHelper.CreateAccount(qboContextoAuth, AccountTypeEnum.Bank, AccountClassificationEnum.Expense);
            //Adding the Account
            Account added = Helper.Add<Account>(qboContextoAuth, entity);

            Helper.DeleteAsync<Account>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
