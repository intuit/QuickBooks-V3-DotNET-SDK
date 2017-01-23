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
	public class PurchaseByVendorTest
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
		public void PurchaseByVendorAddTestUsingoAuth()
		{
			//Creating the Bill for Add
			PurchaseByVendor purchaseByVendor = QBOHelper.CreatePurchaseByVendor(qboContextoAuth);
			//Adding the PurchaseByVendor
			PurchaseByVendor added = Helper.Add<PurchaseByVendor>(qboContextoAuth, purchaseByVendor);
			//Verify the added PurchaseByVendor
			QBOHelper.VerifyPurchaseByVendor(purchaseByVendor, added);
		}

		#endregion

		#region Test cases for FindAll Operations

		[TestMethod]
		public void PurchaseByVendorFindAllTestUsingoAuth()
		{
			//Making sure that at least one entity is already present
			PurchaseByVendorAddTestUsingoAuth();

			//Retrieving the Bill using FindAll
			List<PurchaseByVendor> purchaseByVendors = Helper.FindAll<PurchaseByVendor>(qboContextoAuth, new PurchaseByVendor(), 1, 500);
			Assert.IsNotNull(purchaseByVendors);
			Assert.IsTrue(purchaseByVendors.Count<PurchaseByVendor>() > 0);
		}

		#endregion

		#region Test cases for FindbyId Operations

		[TestMethod]
		public void PurchaseByVendorFindbyIdTestUsingoAuth()
		{
			//Creating the PurchaseByVendor for Adding
			PurchaseByVendor purchaseByVendor = QBOHelper.CreatePurchaseByVendor(qboContextoAuth);
			//Adding the PurchaseByVendor
			PurchaseByVendor added = Helper.Add<PurchaseByVendor>(qboContextoAuth, purchaseByVendor);
			PurchaseByVendor found = Helper.FindById<PurchaseByVendor>(qboContextoAuth, added);
			QBOHelper.VerifyPurchaseByVendor(found, added);
		}

		#endregion

		#region Test cases for Update Operations

		[TestMethod]
		public void PurchaseByVendorUpdateTestUsingoAuth()
		{
			//Creating the PurchaseByVendor for Adding
			PurchaseByVendor purchaseByVendor = QBOHelper.CreatePurchaseByVendor(qboContextoAuth);
			//Adding the PurchaseByVendor
			PurchaseByVendor added = Helper.Add<PurchaseByVendor>(qboContextoAuth, purchaseByVendor);
			//Change the data of added entity
			PurchaseByVendor changed = QBOHelper.UpdatePurchaseByVendor(qboContextoAuth, added);
			//Update the returned entity data
			PurchaseByVendor updated = Helper.Update<PurchaseByVendor>(qboContextoAuth, changed);//Verify the updated PurchaseByVendor
			QBOHelper.VerifyPurchaseByVendor(changed, updated);
		}

        [TestMethod]
        public void PurchaseByVendorSparseUpdateTestUsingoAuth()
        {
            //Creating the PurchaseByVendor for Adding
            PurchaseByVendor purchaseByVendor = QBOHelper.CreatePurchaseByVendor(qboContextoAuth);
            //Adding the PurchaseByVendor
            PurchaseByVendor added = Helper.Add<PurchaseByVendor>(qboContextoAuth, purchaseByVendor);
            //Change the data of added entity
            PurchaseByVendor changed = QBOHelper.UpdatePurchaseByVendorSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            PurchaseByVendor updated = Helper.Update<PurchaseByVendor>(qboContextoAuth, changed);//Verify the updated PurchaseByVendor
            QBOHelper.VerifyPurchaseByVendorSparse(changed, updated);
        }

		#endregion

		#region Test cases for Delete Operations

		[TestMethod]
		public void PurchaseByVendorDeleteTestUsingoAuth()
		{
			//Creating the PurchaseByVendor for Adding
			PurchaseByVendor purchaseByVendor = QBOHelper.CreatePurchaseByVendor(qboContextoAuth);
			//Adding the PurchaseByVendor
			PurchaseByVendor added = Helper.Add<PurchaseByVendor>(qboContextoAuth, purchaseByVendor);
            //Delete the returned entity
            try
            {
                PurchaseByVendor deleted = Helper.Delete<PurchaseByVendor>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
		}

        [TestMethod]
        public void PurchaseByVendorVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            PurchaseByVendor entity = QBOHelper.CreatePurchaseByVendor(qboContextoAuth);
            //Adding the entity
            PurchaseByVendor added = Helper.Add<PurchaseByVendor>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                PurchaseByVendor voided = Helper.Void<PurchaseByVendor>(qboContextoAuth, added);
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
