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
	public class DateTypeCustomFieldDefinitionTest
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
		public void DateTypeCustomFieldDefinitionAddTestUsingoAuth()
		{
			//Creating the Bill for Add
			DateTypeCustomFieldDefinition dateTypeCustomFieldDefinition = QBOHelper.CreateDateTypeCustomFieldDefinition(qboContextoAuth);
			//Adding the DateTypeCustomFieldDefinition
			DateTypeCustomFieldDefinition added = Helper.Add<DateTypeCustomFieldDefinition>(qboContextoAuth, dateTypeCustomFieldDefinition);
			//Verify the added DateTypeCustomFieldDefinition
			QBOHelper.VerifyDateTypeCustomFieldDefinition(dateTypeCustomFieldDefinition, added);
		}

		#endregion

		#region Test cases for FindAll Operations

		[TestMethod]
		public void DateTypeCustomFieldDefinitionFindAllTestUsingoAuth()
		{
			//Making sure that at least one entity is already present
			DateTypeCustomFieldDefinitionAddTestUsingoAuth();

			//Retrieving the Bill using FindAll
			List<DateTypeCustomFieldDefinition> dateTypeCustomFieldDefinitions = Helper.FindAll<DateTypeCustomFieldDefinition>(qboContextoAuth, new DateTypeCustomFieldDefinition(), 1, 500);
			Assert.IsNotNull(dateTypeCustomFieldDefinitions);
			Assert.IsTrue(dateTypeCustomFieldDefinitions.Count<DateTypeCustomFieldDefinition>() > 0);
		}

		#endregion

		#region Test cases for FindbyId Operations

		[TestMethod]
		public void DateTypeCustomFieldDefinitionFindbyIdTestUsingoAuth()
		{
			//Creating the DateTypeCustomFieldDefinition for Adding
			DateTypeCustomFieldDefinition dateTypeCustomFieldDefinition = QBOHelper.CreateDateTypeCustomFieldDefinition(qboContextoAuth);
			//Adding the DateTypeCustomFieldDefinition
			DateTypeCustomFieldDefinition added = Helper.Add<DateTypeCustomFieldDefinition>(qboContextoAuth, dateTypeCustomFieldDefinition);
			DateTypeCustomFieldDefinition found = Helper.FindById<DateTypeCustomFieldDefinition>(qboContextoAuth, added);
			QBOHelper.VerifyDateTypeCustomFieldDefinition(found, added);
		}

		#endregion

		#region Test cases for Update Operations

		[TestMethod]
		public void DateTypeCustomFieldDefinitionUpdateTestUsingoAuth()
		{
			//Creating the DateTypeCustomFieldDefinition for Adding
			DateTypeCustomFieldDefinition dateTypeCustomFieldDefinition = QBOHelper.CreateDateTypeCustomFieldDefinition(qboContextoAuth);
			//Adding the DateTypeCustomFieldDefinition
			DateTypeCustomFieldDefinition added = Helper.Add<DateTypeCustomFieldDefinition>(qboContextoAuth, dateTypeCustomFieldDefinition);
			//Change the data of added entity
			QBOHelper.UpdateDateTypeCustomFieldDefinition(qboContextoAuth, added);
			//Update the returned entity data
			DateTypeCustomFieldDefinition updated = Helper.Update<DateTypeCustomFieldDefinition>(qboContextoAuth, added);//Verify the updated DateTypeCustomFieldDefinition
			QBOHelper.VerifyDateTypeCustomFieldDefinition(added, updated);
		}

		#endregion

		#region Test cases for Delete Operations

		[TestMethod]
		public void DateTypeCustomFieldDefinitionDeleteTestUsingoAuth()
		{
			//Creating the DateTypeCustomFieldDefinition for Adding
			DateTypeCustomFieldDefinition dateTypeCustomFieldDefinition = QBOHelper.CreateDateTypeCustomFieldDefinition(qboContextoAuth);
			//Adding the DateTypeCustomFieldDefinition
			DateTypeCustomFieldDefinition added = Helper.Add<DateTypeCustomFieldDefinition>(qboContextoAuth, dateTypeCustomFieldDefinition);
			//Delete the returned entity
			Helper.Delete<DateTypeCustomFieldDefinition>(qboContextoAuth, added);
		}

		#endregion

		#endregion

		#endregion

	}
}
