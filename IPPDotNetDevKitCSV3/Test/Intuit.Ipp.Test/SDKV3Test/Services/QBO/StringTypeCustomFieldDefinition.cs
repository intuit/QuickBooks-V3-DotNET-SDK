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
	public class StringTypeCustomFieldDefinitionTest
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
		public void StringTypeCustomFieldDefinitionAddTestUsingoAuth()
		{
			//Creating the Bill for Add
			StringTypeCustomFieldDefinition stringTypeCustomFieldDefinition = QBOHelper.CreateStringTypeCustomFieldDefinition(qboContextoAuth);
			//Adding the StringTypeCustomFieldDefinition
			StringTypeCustomFieldDefinition added = Helper.Add<StringTypeCustomFieldDefinition>(qboContextoAuth, stringTypeCustomFieldDefinition);
			//Verify the added StringTypeCustomFieldDefinition
			QBOHelper.VerifyStringTypeCustomFieldDefinition(stringTypeCustomFieldDefinition, added);
		}

		#endregion

		#region Test cases for FindAll Operations

		[TestMethod]
		public void StringTypeCustomFieldDefinitionFindAllTestUsingoAuth()
		{
			//Making sure that at least one entity is already present
			StringTypeCustomFieldDefinitionAddTestUsingoAuth();

			//Retrieving the Bill using FindAll
			List<StringTypeCustomFieldDefinition> stringTypeCustomFieldDefinitions = Helper.FindAll<StringTypeCustomFieldDefinition>(qboContextoAuth, new StringTypeCustomFieldDefinition(), 1, 500);
			Assert.IsNotNull(stringTypeCustomFieldDefinitions);
			Assert.IsTrue(stringTypeCustomFieldDefinitions.Count<StringTypeCustomFieldDefinition>() > 0);
		}

		#endregion

		#region Test cases for FindbyId Operations

		[TestMethod]
		public void StringTypeCustomFieldDefinitionFindbyIdTestUsingoAuth()
		{
			//Creating the StringTypeCustomFieldDefinition for Adding
			StringTypeCustomFieldDefinition stringTypeCustomFieldDefinition = QBOHelper.CreateStringTypeCustomFieldDefinition(qboContextoAuth);
			//Adding the StringTypeCustomFieldDefinition
			StringTypeCustomFieldDefinition added = Helper.Add<StringTypeCustomFieldDefinition>(qboContextoAuth, stringTypeCustomFieldDefinition);
			StringTypeCustomFieldDefinition found = Helper.FindById<StringTypeCustomFieldDefinition>(qboContextoAuth, added);
			QBOHelper.VerifyStringTypeCustomFieldDefinition(found, added);
		}

		#endregion

		#region Test cases for Update Operations

		[TestMethod]
		public void StringTypeCustomFieldDefinitionUpdateTestUsingoAuth()
		{
			//Creating the StringTypeCustomFieldDefinition for Adding
			StringTypeCustomFieldDefinition stringTypeCustomFieldDefinition = QBOHelper.CreateStringTypeCustomFieldDefinition(qboContextoAuth);
			//Adding the StringTypeCustomFieldDefinition
			StringTypeCustomFieldDefinition added = Helper.Add<StringTypeCustomFieldDefinition>(qboContextoAuth, stringTypeCustomFieldDefinition);
			//Change the data of added entity
			QBOHelper.UpdateStringTypeCustomFieldDefinition(qboContextoAuth, added);
			//Update the returned entity data
			StringTypeCustomFieldDefinition updated = Helper.Update<StringTypeCustomFieldDefinition>(qboContextoAuth, added);//Verify the updated StringTypeCustomFieldDefinition
			QBOHelper.VerifyStringTypeCustomFieldDefinition(added, updated);
		}

		#endregion

		#region Test cases for Delete Operations

		[TestMethod]
		public void StringTypeCustomFieldDefinitionDeleteTestUsingoAuth()
		{
			//Creating the StringTypeCustomFieldDefinition for Adding
			StringTypeCustomFieldDefinition stringTypeCustomFieldDefinition = QBOHelper.CreateStringTypeCustomFieldDefinition(qboContextoAuth);
			//Adding the StringTypeCustomFieldDefinition
			StringTypeCustomFieldDefinition added = Helper.Add<StringTypeCustomFieldDefinition>(qboContextoAuth, stringTypeCustomFieldDefinition);
			//Delete the returned entity
			Helper.Delete<StringTypeCustomFieldDefinition>(qboContextoAuth, added);
		}

		#endregion

		#endregion

		#endregion

	}
}
