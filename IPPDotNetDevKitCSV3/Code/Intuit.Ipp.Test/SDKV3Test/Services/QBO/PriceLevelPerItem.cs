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
    public class PriceLevelPerItemTest
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
		public void PriceLevelPerItemAddTestUsingoAuth()
		{
			//Creating the Bill for Add
			PriceLevelPerItem priceLevelPerItem = QBOHelper.CreatePriceLevelPerItem(qboContextoAuth);
			//Adding the PriceLevelPerItem
			PriceLevelPerItem added = Helper.Add<PriceLevelPerItem>(qboContextoAuth, priceLevelPerItem);
			//Verify the added PriceLevelPerItem
			QBOHelper.VerifyPriceLevelPerItem(priceLevelPerItem, added);
		}

		#endregion

		#region Test cases for FindAll Operations

		[TestMethod]
		public void PriceLevelPerItemFindAllTestUsingoAuth()
		{
			//Making sure that at least one entity is already present
			PriceLevelPerItemAddTestUsingoAuth();

			//Retrieving the Bill using FindAll
			List<PriceLevelPerItem> priceLevelPerItems = Helper.FindAll<PriceLevelPerItem>(qboContextoAuth, new PriceLevelPerItem(), 1, 500);
			Assert.IsNotNull(priceLevelPerItems);
			Assert.IsTrue(priceLevelPerItems.Count<PriceLevelPerItem>() > 0);
		}

		#endregion

		#region Test cases for FindbyId Operations

		[TestMethod]
		public void PriceLevelPerItemFindbyIdTestUsingoAuth()
		{
			//Creating the PriceLevelPerItem for Adding
			PriceLevelPerItem priceLevelPerItem = QBOHelper.CreatePriceLevelPerItem(qboContextoAuth);
			//Adding the PriceLevelPerItem
			PriceLevelPerItem added = Helper.Add<PriceLevelPerItem>(qboContextoAuth, priceLevelPerItem);
			PriceLevelPerItem found = Helper.FindById<PriceLevelPerItem>(qboContextoAuth, added);
			QBOHelper.VerifyPriceLevelPerItem(found, added);
		}

		#endregion

		#region Test cases for Update Operations

		[TestMethod]
		public void PriceLevelPerItemUpdateTestUsingoAuth()
		{
			//Creating the PriceLevelPerItem for Adding
			PriceLevelPerItem priceLevelPerItem = QBOHelper.CreatePriceLevelPerItem(qboContextoAuth);
			//Adding the PriceLevelPerItem
			PriceLevelPerItem added = Helper.Add<PriceLevelPerItem>(qboContextoAuth, priceLevelPerItem);
			//Change the data of added entity
			PriceLevelPerItem changed = QBOHelper.UpdatePriceLevelPerItem(qboContextoAuth, added);
			//Update the returned entity data
			PriceLevelPerItem updated = Helper.Update<PriceLevelPerItem>(qboContextoAuth, changed);//Verify the updated PriceLevelPerItem
			QBOHelper.VerifyPriceLevelPerItem(changed, updated);
		}

        [TestMethod]
        public void PriceLevelPerItemSparseUpdateTestUsingoAuth()
        {
            //Creating the PriceLevelPerItem for Adding
            PriceLevelPerItem priceLevelPerItem = QBOHelper.CreatePriceLevelPerItem(qboContextoAuth);
            //Adding the PriceLevelPerItem
            PriceLevelPerItem added = Helper.Add<PriceLevelPerItem>(qboContextoAuth, priceLevelPerItem);
            //Change the data of added entity
            PriceLevelPerItem changed = QBOHelper.UpdatePriceLevelPerItemSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            PriceLevelPerItem updated = Helper.Update<PriceLevelPerItem>(qboContextoAuth, changed);//Verify the updated PriceLevelPerItem
            QBOHelper.VerifyPriceLevelPerItemSparse(changed, updated);
        }

		#endregion

		#region Test cases for Delete Operations

		[TestMethod]
		public void PriceLevelPerItemDeleteTestUsingoAuth()
		{
			//Creating the PriceLevelPerItem for Adding
			PriceLevelPerItem priceLevelPerItem = QBOHelper.CreatePriceLevelPerItem(qboContextoAuth);
			//Adding the PriceLevelPerItem
			PriceLevelPerItem added = Helper.Add<PriceLevelPerItem>(qboContextoAuth, priceLevelPerItem);
            //Delete the returned entity
            try
            {
                PriceLevelPerItem deleted = Helper.Delete<PriceLevelPerItem>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
		}

        [TestMethod]
        public void PriceLevelPerItemVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            PriceLevelPerItem entity = QBOHelper.CreatePriceLevelPerItem(qboContextoAuth);
            //Adding the entity
            PriceLevelPerItem added = Helper.Add<PriceLevelPerItem>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                PriceLevelPerItem voided = Helper.Void<PriceLevelPerItem>(qboContextoAuth, added);
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
