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
	public class NumberTypeCustomFieldDefinitionTest
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
		public void NumberTypeCustomFieldDefinitionAddTestUsingoAuth()
		{
			//Creating the Bill for Add
			NumberTypeCustomFieldDefinition numberTypeCustomFieldDefinition = QBOHelper.CreateNumberTypeCustomFieldDefinition(qboContextoAuth);
			//Adding the NumberTypeCustomFieldDefinition
			NumberTypeCustomFieldDefinition added = Helper.Add<NumberTypeCustomFieldDefinition>(qboContextoAuth, numberTypeCustomFieldDefinition);
			//Verify the added NumberTypeCustomFieldDefinition
			QBOHelper.VerifyNumberTypeCustomFieldDefinition(numberTypeCustomFieldDefinition, added);
		}

		#endregion

		#region Test cases for FindAll Operations

		[TestMethod]
		public void NumberTypeCustomFieldDefinitionFindAllTestUsingoAuth()
		{
			//Making sure that at least one entity is already present
			NumberTypeCustomFieldDefinitionAddTestUsingoAuth();

			//Retrieving the Bill using FindAll
			List<NumberTypeCustomFieldDefinition> numberTypeCustomFieldDefinitions = Helper.FindAll<NumberTypeCustomFieldDefinition>(qboContextoAuth, new NumberTypeCustomFieldDefinition(), 1, 500);
			Assert.IsNotNull(numberTypeCustomFieldDefinitions);
			Assert.IsTrue(numberTypeCustomFieldDefinitions.Count<NumberTypeCustomFieldDefinition>() > 0);
		}

		#endregion

		#region Test cases for FindbyId Operations

		[TestMethod]
		public void NumberTypeCustomFieldDefinitionFindbyIdTestUsingoAuth()
		{
			//Creating the NumberTypeCustomFieldDefinition for Adding
			NumberTypeCustomFieldDefinition numberTypeCustomFieldDefinition = QBOHelper.CreateNumberTypeCustomFieldDefinition(qboContextoAuth);
			//Adding the NumberTypeCustomFieldDefinition
			NumberTypeCustomFieldDefinition added = Helper.Add<NumberTypeCustomFieldDefinition>(qboContextoAuth, numberTypeCustomFieldDefinition);
			NumberTypeCustomFieldDefinition found = Helper.FindById<NumberTypeCustomFieldDefinition>(qboContextoAuth, added);
			QBOHelper.VerifyNumberTypeCustomFieldDefinition(found, added);
		}

		#endregion

		#region Test cases for Update Operations

		[TestMethod]
		public void NumberTypeCustomFieldDefinitionUpdateTestUsingoAuth()
		{
			//Creating the NumberTypeCustomFieldDefinition for Adding
			NumberTypeCustomFieldDefinition numberTypeCustomFieldDefinition = QBOHelper.CreateNumberTypeCustomFieldDefinition(qboContextoAuth);
			//Adding the NumberTypeCustomFieldDefinition
			NumberTypeCustomFieldDefinition added = Helper.Add<NumberTypeCustomFieldDefinition>(qboContextoAuth, numberTypeCustomFieldDefinition);
			//Change the data of added entity
			QBOHelper.UpdateNumberTypeCustomFieldDefinition(qboContextoAuth, added);
			//Update the returned entity data
			NumberTypeCustomFieldDefinition updated = Helper.Update<NumberTypeCustomFieldDefinition>(qboContextoAuth, added);//Verify the updated NumberTypeCustomFieldDefinition
			QBOHelper.VerifyNumberTypeCustomFieldDefinition(added, updated);
		}

		#endregion

		#region Test cases for Delete Operations

		[TestMethod]
		public void NumberTypeCustomFieldDefinitionDeleteTestUsingoAuth()
		{
			//Creating the NumberTypeCustomFieldDefinition for Adding
			NumberTypeCustomFieldDefinition numberTypeCustomFieldDefinition = QBOHelper.CreateNumberTypeCustomFieldDefinition(qboContextoAuth);
			//Adding the NumberTypeCustomFieldDefinition
			NumberTypeCustomFieldDefinition added = Helper.Add<NumberTypeCustomFieldDefinition>(qboContextoAuth, numberTypeCustomFieldDefinition);
			//Delete the returned entity
			Helper.Delete<NumberTypeCustomFieldDefinition>(qboContextoAuth, added);
		}

		#endregion

		#endregion

		#endregion

	}
}
