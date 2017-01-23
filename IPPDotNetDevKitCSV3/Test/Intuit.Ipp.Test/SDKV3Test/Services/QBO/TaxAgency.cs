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
    [TestClass] 
	public class TaxAgencyTest
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
		public void TaxAgencyAddTestUsingoAuth()
		{
			//Creating the Bill for Add
			TaxAgency taxAgency = QBOHelper.CreateTaxAgency(qboContextoAuth);
			//Adding the TaxAgency
			TaxAgency added = Helper.Add<TaxAgency>(qboContextoAuth, taxAgency);
			//Verify the added TaxAgency
			QBOHelper.VerifyTaxAgency(taxAgency, added);
		}

		#endregion

		#region Test cases for FindAll Operations

		[TestMethod]
		public void TaxAgencyFindAllTestUsingoAuth()
		{
			//Making sure that at least one entity is already present
			TaxAgencyAddTestUsingoAuth();

			//Retrieving the Bill using FindAll
			List<TaxAgency> taxAgencys = Helper.FindAll<TaxAgency>(qboContextoAuth, new TaxAgency(), 1, 500);
			Assert.IsNotNull(taxAgencys);
			Assert.IsTrue(taxAgencys.Count<TaxAgency>() > 0);
		}

		#endregion

		#region Test cases for FindbyId Operations

		[TestMethod]
		public void TaxAgencyFindbyIdTestUsingoAuth()
		{
			//Creating the TaxAgency for Adding
			TaxAgency taxAgency = QBOHelper.CreateTaxAgency(qboContextoAuth);
			//Adding the TaxAgency
			TaxAgency added = Helper.Add<TaxAgency>(qboContextoAuth, taxAgency);
			TaxAgency found = Helper.FindById<TaxAgency>(qboContextoAuth, added);
			QBOHelper.VerifyTaxAgency(found, added);
		}

		#endregion

		#region Test cases for Update Operations

		[TestMethod][Ignore]
		public void TaxAgencyUpdateTestUsingoAuth()
		{
			//Creating the TaxAgency for Adding
			TaxAgency taxAgency = QBOHelper.CreateTaxAgency(qboContextoAuth);
			//Adding the TaxAgency
			TaxAgency added = Helper.Add<TaxAgency>(qboContextoAuth, taxAgency);
			//Change the data of added entity
			TaxAgency changed = QBOHelper.UpdateTaxAgency(qboContextoAuth, added);
			//Update the returned entity data
			TaxAgency updated = Helper.Update<TaxAgency>(qboContextoAuth, changed);//Verify the updated TaxAgency
			QBOHelper.VerifyTaxAgency(changed, updated);
		}

        [TestMethod]
        [Ignore]
        public void TaxAgencySparseUpdateTestUsingoAuth()
        {
            //Creating the TaxAgency for Adding
            TaxAgency taxAgency = QBOHelper.CreateTaxAgency(qboContextoAuth);
            //Adding the TaxAgency
            TaxAgency added = Helper.Add<TaxAgency>(qboContextoAuth, taxAgency);
            //Change the data of added entity
            TaxAgency changed = QBOHelper.UpdateTaxAgencySparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            TaxAgency updated = Helper.Update<TaxAgency>(qboContextoAuth, changed);//Verify the updated TaxAgency
            QBOHelper.VerifyTaxAgencySparse(changed, updated);
        }

		#endregion

		#region Test cases for Delete Operations

        [TestMethod]
        [Ignore]
		public void TaxAgencyDeleteTestUsingoAuth()
		{
			//Creating the TaxAgency for Adding
			TaxAgency taxAgency = QBOHelper.CreateTaxAgency(qboContextoAuth);
			//Adding the TaxAgency
			TaxAgency added = Helper.Add<TaxAgency>(qboContextoAuth, taxAgency);
            //Delete the returned entity
            try
            {
                TaxAgency deleted = Helper.Delete<TaxAgency>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
		}

        [TestMethod]
        [Ignore]
        public void TaxAgencyVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            TaxAgency entity = QBOHelper.CreateTaxAgency(qboContextoAuth);
            //Adding the entity
            TaxAgency added = Helper.Add<TaxAgency>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                TaxAgency voided = Helper.Void<TaxAgency>(qboContextoAuth, added);
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
