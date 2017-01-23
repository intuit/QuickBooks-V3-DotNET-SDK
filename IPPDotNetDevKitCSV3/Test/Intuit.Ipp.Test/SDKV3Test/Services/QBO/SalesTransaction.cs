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
	public class SalesTransactionTest
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
		public void SalesTransactionAddTestUsingoAuth()
		{
			//Creating the Bill for Add
			SalesTransaction salesTransaction = QBOHelper.CreateSalesTransaction(qboContextoAuth);
			//Adding the SalesTransaction
			SalesTransaction added = Helper.Add<SalesTransaction>(qboContextoAuth, salesTransaction);
			//Verify the added SalesTransaction
			QBOHelper.VerifySalesTransaction(salesTransaction, added);
		}

		#endregion

		#region Test cases for FindAll Operations

		[TestMethod]
		public void SalesTransactionFindAllTestUsingoAuth()
		{
			//Making sure that at least one entity is already present
			SalesTransactionAddTestUsingoAuth();

			//Retrieving the Bill using FindAll
			List<SalesTransaction> salesTransactions = Helper.FindAll<SalesTransaction>(qboContextoAuth, new SalesTransaction(), 1, 500);
			Assert.IsNotNull(salesTransactions);
			Assert.IsTrue(salesTransactions.Count<SalesTransaction>() > 0);
		}

		#endregion

		#region Test cases for FindbyId Operations

		[TestMethod]
		public void SalesTransactionFindbyIdTestUsingoAuth()
		{
			//Creating the SalesTransaction for Adding
			SalesTransaction salesTransaction = QBOHelper.CreateSalesTransaction(qboContextoAuth);
			//Adding the SalesTransaction
			SalesTransaction added = Helper.Add<SalesTransaction>(qboContextoAuth, salesTransaction);
			SalesTransaction found = Helper.FindById<SalesTransaction>(qboContextoAuth, added);
			QBOHelper.VerifySalesTransaction(found, added);
		}

		#endregion

		#region Test cases for Update Operations

		[TestMethod]
		public void SalesTransactionUpdateTestUsingoAuth()
		{
			//Creating the SalesTransaction for Adding
			SalesTransaction salesTransaction = QBOHelper.CreateSalesTransaction(qboContextoAuth);
			//Adding the SalesTransaction
			SalesTransaction added = Helper.Add<SalesTransaction>(qboContextoAuth, salesTransaction);
			//Change the data of added entity
			SalesTransaction changed = QBOHelper.UpdateSalesTransaction(qboContextoAuth, added);
			//Update the returned entity data
			SalesTransaction updated = Helper.Update<SalesTransaction>(qboContextoAuth, changed);//Verify the updated SalesTransaction
			QBOHelper.VerifySalesTransaction(changed, updated);
		}

        [TestMethod]
        public void SalesTransactionSparseUpdateTestUsingoAuth()
        {
            //Creating the SalesTransaction for Adding
            SalesTransaction salesTransaction = QBOHelper.CreateSalesTransaction(qboContextoAuth);
            //Adding the SalesTransaction
            SalesTransaction added = Helper.Add<SalesTransaction>(qboContextoAuth, salesTransaction);
            //Change the data of added entity
            SalesTransaction changed = QBOHelper.UpdateSalesTransactionSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            SalesTransaction updated = Helper.Update<SalesTransaction>(qboContextoAuth, changed);//Verify the updated SalesTransaction
            QBOHelper.VerifySalesTransactionSparse(changed, updated);
        }

		#endregion

		#region Test cases for Delete Operations

		[TestMethod]
		public void SalesTransactionDeleteTestUsingoAuth()
		{
			//Creating the SalesTransaction for Adding
			SalesTransaction salesTransaction = QBOHelper.CreateSalesTransaction(qboContextoAuth);
			//Adding the SalesTransaction
			SalesTransaction added = Helper.Add<SalesTransaction>(qboContextoAuth, salesTransaction);
            //Delete the returned entity
            try
            {
                SalesTransaction deleted = Helper.Delete<SalesTransaction>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
		}

        [TestMethod]
        public void SalesTransactionVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            SalesTransaction entity = QBOHelper.CreateSalesTransaction(qboContextoAuth);
            //Adding the entity
            SalesTransaction added = Helper.Add<SalesTransaction>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                SalesTransaction voided = Helper.Void<SalesTransaction>(qboContextoAuth, added);
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
