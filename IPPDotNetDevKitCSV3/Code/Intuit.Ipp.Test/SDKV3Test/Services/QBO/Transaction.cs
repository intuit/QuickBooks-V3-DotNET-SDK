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

namespace Intuit.Ipp.Test.Services.QBO
{
	[TestClass] [Ignore]
	public class TransactionTest
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
		public void TransactionAddTestUsingoAuth()
		{
			//Creating the Bill for Add
			Transaction transaction = QBOHelper.CreateTransaction(qboContextoAuth);
			//Adding the Transaction
			Transaction added = Helper.Add<Transaction>(qboContextoAuth, transaction);
			//Verify the added Transaction
			QBOHelper.VerifyTransaction(transaction, added);
		}

		#endregion

		#region Test cases for FindAll Operations

		[TestMethod]
		public void TransactionFindAllTestUsingoAuth()
		{
			//Making sure that at least one entity is already present
			TransactionAddTestUsingoAuth();

			//Retrieving the Bill using FindAll
			List<Transaction> transactions = Helper.FindAll<Transaction>(qboContextoAuth, new Transaction(), 1, 500);
			Assert.IsNotNull(transactions);
			Assert.IsTrue(transactions.Count<Transaction>() > 0);
		}

		#endregion

		#region Test cases for FindbyId Operations

		[TestMethod]
		public void TransactionFindbyIdTestUsingoAuth()
		{
			//Creating the Transaction for Adding
			Transaction transaction = QBOHelper.CreateTransaction(qboContextoAuth);
			//Adding the Transaction
			Transaction added = Helper.Add<Transaction>(qboContextoAuth, transaction);
			Transaction found = Helper.FindById<Transaction>(qboContextoAuth, added);
			QBOHelper.VerifyTransaction(found, added);
		}

		#endregion

		#region Test cases for Update Operations

		[TestMethod]
		public void TransactionUpdateTestUsingoAuth()
		{
			//Creating the Transaction for Adding
			Transaction transaction = QBOHelper.CreateTransaction(qboContextoAuth);
			//Adding the Transaction
			Transaction added = Helper.Add<Transaction>(qboContextoAuth, transaction);
			//Change the data of added entity
			Transaction changed = QBOHelper.UpdateTransaction(qboContextoAuth, added);
			//Update the returned entity data
			Transaction updated = Helper.Update<Transaction>(qboContextoAuth, changed);//Verify the updated Transaction
			QBOHelper.VerifyTransaction(changed, updated);
		}

        [TestMethod]
        public void TransactionSparseUpdateTestUsingoAuth()
        {
            //Creating the Transaction for Adding
            Transaction transaction = QBOHelper.CreateTransaction(qboContextoAuth);
            //Adding the Transaction
            Transaction added = Helper.Add<Transaction>(qboContextoAuth, transaction);
            //Change the data of added entity
            Transaction changed = QBOHelper.UpdateTransactionSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Transaction updated = Helper.Update<Transaction>(qboContextoAuth, changed);//Verify the updated Transaction
            QBOHelper.VerifyTransactionSparse(changed, updated);
        }

		#endregion

		#region Test cases for Delete Operations

		[TestMethod]
		public void TransactionDeleteTestUsingoAuth()
		{
			//Creating the Transaction for Adding
			Transaction transaction = QBOHelper.CreateTransaction(qboContextoAuth);
			//Adding the Transaction
			Transaction added = Helper.Add<Transaction>(qboContextoAuth, transaction);
            //Delete the returned entity
            try
            {
                Transaction deleted = Helper.Delete<Transaction>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
		}

        [TestMethod]
        public void TransactionVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            Transaction entity = QBOHelper.CreateTransaction(qboContextoAuth);
            //Adding the entity
            Transaction added = Helper.Add<Transaction>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                Transaction voided = Helper.Void<Transaction>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Voided, voided.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

		#endregion

		#endregion

		#endregion

	}
}
