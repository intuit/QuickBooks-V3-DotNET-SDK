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
	public class BooleanTypeCustomFieldDefinitionTest
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
		public void BooleanTypeCustomFieldDefinitionAddTestUsingoAuth()
		{
			//Creating the Bill for Add
			BooleanTypeCustomFieldDefinition booleanTypeCustomFieldDefinition = QBOHelper.CreateBooleanTypeCustomFieldDefinition(qboContextoAuth);
			//Adding the BooleanTypeCustomFieldDefinition
			BooleanTypeCustomFieldDefinition added = Helper.Add<BooleanTypeCustomFieldDefinition>(qboContextoAuth, booleanTypeCustomFieldDefinition);
			//Verify the added BooleanTypeCustomFieldDefinition
			QBOHelper.VerifyBooleanTypeCustomFieldDefinition(booleanTypeCustomFieldDefinition, added);
		}

		#endregion

		#region Test cases for FindAll Operations

		[TestMethod]
		public void BooleanTypeCustomFieldDefinitionFindAllTestUsingoAuth()
		{
			//Making sure that at least one entity is already present
			BooleanTypeCustomFieldDefinitionAddTestUsingoAuth();

			//Retrieving the Bill using FindAll
			List<BooleanTypeCustomFieldDefinition> booleanTypeCustomFieldDefinitions = Helper.FindAll<BooleanTypeCustomFieldDefinition>(qboContextoAuth, new BooleanTypeCustomFieldDefinition(), 1, 500);
			Assert.IsNotNull(booleanTypeCustomFieldDefinitions);
			Assert.IsTrue(booleanTypeCustomFieldDefinitions.Count<BooleanTypeCustomFieldDefinition>() > 0);
		}

		#endregion

		#region Test cases for FindbyId Operations

		[TestMethod]
		public void BooleanTypeCustomFieldDefinitionFindbyIdTestUsingoAuth()
		{
			//Creating the BooleanTypeCustomFieldDefinition for Adding
			BooleanTypeCustomFieldDefinition booleanTypeCustomFieldDefinition = QBOHelper.CreateBooleanTypeCustomFieldDefinition(qboContextoAuth);
			//Adding the BooleanTypeCustomFieldDefinition
			BooleanTypeCustomFieldDefinition added = Helper.Add<BooleanTypeCustomFieldDefinition>(qboContextoAuth, booleanTypeCustomFieldDefinition);
			BooleanTypeCustomFieldDefinition found = Helper.FindById<BooleanTypeCustomFieldDefinition>(qboContextoAuth, added);
			QBOHelper.VerifyBooleanTypeCustomFieldDefinition(found, added);
		}

		#endregion

		#region Test cases for Update Operations

		[TestMethod]
		public void BooleanTypeCustomFieldDefinitionUpdateTestUsingoAuth()
		{
			//Creating the BooleanTypeCustomFieldDefinition for Adding
			BooleanTypeCustomFieldDefinition booleanTypeCustomFieldDefinition = QBOHelper.CreateBooleanTypeCustomFieldDefinition(qboContextoAuth);
			//Adding the BooleanTypeCustomFieldDefinition
			BooleanTypeCustomFieldDefinition added = Helper.Add<BooleanTypeCustomFieldDefinition>(qboContextoAuth, booleanTypeCustomFieldDefinition);
			//Change the data of added entity
			QBOHelper.UpdateBooleanTypeCustomFieldDefinition(qboContextoAuth, added);
			//Update the returned entity data
			BooleanTypeCustomFieldDefinition updated = Helper.Update<BooleanTypeCustomFieldDefinition>(qboContextoAuth, added);//Verify the updated BooleanTypeCustomFieldDefinition
			QBOHelper.VerifyBooleanTypeCustomFieldDefinition(added, updated);
		}

		#endregion

		#region Test cases for Delete Operations

		[TestMethod]
		public void BooleanTypeCustomFieldDefinitionDeleteTestUsingoAuth()
		{
			//Creating the BooleanTypeCustomFieldDefinition for Adding
			BooleanTypeCustomFieldDefinition booleanTypeCustomFieldDefinition = QBOHelper.CreateBooleanTypeCustomFieldDefinition(qboContextoAuth);
			//Adding the BooleanTypeCustomFieldDefinition
			BooleanTypeCustomFieldDefinition added = Helper.Add<BooleanTypeCustomFieldDefinition>(qboContextoAuth, booleanTypeCustomFieldDefinition);
			//Delete the returned entity
			Helper.Delete<BooleanTypeCustomFieldDefinition>(qboContextoAuth, added);
		}

		#endregion

		#endregion

		#endregion

	}
}
