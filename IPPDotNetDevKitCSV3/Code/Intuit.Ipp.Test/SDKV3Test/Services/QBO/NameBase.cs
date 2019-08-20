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
	public class NameBaseTest
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
		public void NameBaseAddTestUsingoAuth()
		{
			//Creating the Bill for Add
			NameBase nameBase = QBOHelper.CreateNameBase(qboContextoAuth);
			//Adding the NameBase
			NameBase added = Helper.Add<NameBase>(qboContextoAuth, nameBase);
			//Verify the added NameBase
			QBOHelper.VerifyNameBase(nameBase, added);
		}

		#endregion

		#region Test cases for FindAll Operations

		[TestMethod]
		public void NameBaseFindAllTestUsingoAuth()
		{
			//Making sure that at least one entity is already present
			NameBaseAddTestUsingoAuth();

			//Retrieving the Bill using FindAll
			List<NameBase> nameBases = Helper.FindAll<NameBase>(qboContextoAuth, new NameBase(), 1, 500);
			Assert.IsNotNull(nameBases);
			Assert.IsTrue(nameBases.Count<NameBase>() > 0);
		}

		#endregion

		#region Test cases for FindbyId Operations

		[TestMethod]
		public void NameBaseFindbyIdTestUsingoAuth()
		{
			//Creating the NameBase for Adding
			NameBase nameBase = QBOHelper.CreateNameBase(qboContextoAuth);
			//Adding the NameBase
			NameBase added = Helper.Add<NameBase>(qboContextoAuth, nameBase);
			NameBase found = Helper.FindById<NameBase>(qboContextoAuth, added);
			QBOHelper.VerifyNameBase(found, added);
		}

		#endregion

		#region Test cases for Update Operations

		[TestMethod]
		public void NameBaseUpdateTestUsingoAuth()
		{
			//Creating the NameBase for Adding
			NameBase nameBase = QBOHelper.CreateNameBase(qboContextoAuth);
			//Adding the NameBase
			NameBase added = Helper.Add<NameBase>(qboContextoAuth, nameBase);
			//Change the data of added entity
			QBOHelper.UpdateNameBase(qboContextoAuth, added);
			//Update the returned entity data
			NameBase updated = Helper.Update<NameBase>(qboContextoAuth, added);//Verify the updated NameBase
			QBOHelper.VerifyNameBase(added, updated);
		}

		#endregion

		#region Test cases for Delete Operations

		[TestMethod]
		public void NameBaseDeleteTestUsingoAuth()
		{
			//Creating the NameBase for Adding
			NameBase nameBase = QBOHelper.CreateNameBase(qboContextoAuth);
			//Adding the NameBase
			NameBase added = Helper.Add<NameBase>(qboContextoAuth, nameBase);
            //Delete the returned entity
            try
            {
                NameBase deleted = Helper.Delete<NameBase>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
		}

        [TestMethod]
        public void NameBaseVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            NameBase entity = QBOHelper.CreateNameBase(qboContextoAuth);
            //Adding the entity
            NameBase added = Helper.Add<NameBase>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                NameBase voided = Helper.Void<NameBase>(qboContextoAuth, added);
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
