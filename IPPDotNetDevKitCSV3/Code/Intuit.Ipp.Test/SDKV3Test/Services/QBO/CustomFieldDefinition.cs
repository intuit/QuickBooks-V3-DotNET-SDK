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
	public class CustomFieldDefinitionTest
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
		public void CustomFieldDefinitionAddTestUsingoAuth()
		{
            ////Creating the Bill for Add
            //CustomFieldDefinition customFieldDefinition = QBOHelper.CreateCustomFieldDefinition(qboContextoAuth);
            ////Adding the CustomFieldDefinition
            //CustomFieldDefinition added = Helper.Add<CustomFieldDefinition>(qboContextoAuth, customFieldDefinition);
            ////Verify the added CustomFieldDefinition
			//QBOHelper.VerifyCustomFieldDefinition(customFieldDefinition, added);
		}

		#endregion

		#region Test cases for FindAll Operations

		[TestMethod]
		public void CustomFieldDefinitionFindAllTestUsingoAuth()
		{
            ////Making sure that at least one entity is already present
            //CustomFieldDefinitionAddTestUsingoAuth();

            ////Retrieving the Bill using FindAll
            //List<CustomFieldDefinition> customFieldDefinitions = Helper.FindAll<CustomFieldDefinition>(qboContextoAuth, new CustomFieldDefinition(), 1, 500);
            //Assert.IsNotNull(customFieldDefinitions);
            //Assert.IsTrue(customFieldDefinitions.Count<CustomFieldDefinition>() > 0);
		}

		#endregion

		#region Test cases for FindbyId Operations

		[TestMethod]
		public void CustomFieldDefinitionFindbyIdTestUsingoAuth()
		{
            ////Creating the CustomFieldDefinition for Adding
            //CustomFieldDefinition customFieldDefinition = QBOHelper.CreateCustomFieldDefinition(qboContextoAuth);
            ////Adding the CustomFieldDefinition
            //CustomFieldDefinition added = Helper.Add<CustomFieldDefinition>(qboContextoAuth, customFieldDefinition);
            //CustomFieldDefinition found = Helper.FindById<CustomFieldDefinition>(qboContextoAuth, added);
            //QBOHelper.VerifyCustomFieldDefinition(found, added);
		}

		#endregion

		#region Test cases for Update Operations

		[TestMethod]
		public void CustomFieldDefinitionUpdateTestUsingoAuth()
		{
            ////Creating the CustomFieldDefinition for Adding
            //CustomFieldDefinition customFieldDefinition = QBOHelper.CreateCustomFieldDefinition(qboContextoAuth);
            ////Adding the CustomFieldDefinition
            //CustomFieldDefinition added = Helper.Add<CustomFieldDefinition>(qboContextoAuth, customFieldDefinition);
            ////Change the data of added entity
            //QBOHelper.UpdateCustomFieldDefinition(qboContextoAuth, added);
            ////Update the returned entity data
            //CustomFieldDefinition updated = Helper.Update<CustomFieldDefinition>(qboContextoAuth, added);//Verify the updated CustomFieldDefinition
            //QBOHelper.VerifyCustomFieldDefinition(added, updated);
		}

		#endregion

		#region Test cases for Delete Operations

		[TestMethod]
		public void CustomFieldDefinitionDeleteTestUsingoAuth()
		{
            ////Creating the CustomFieldDefinition for Adding
            //CustomFieldDefinition customFieldDefinition = QBOHelper.CreateCustomFieldDefinition(qboContextoAuth);
            ////Adding the CustomFieldDefinition
            //CustomFieldDefinition added = Helper.Add<CustomFieldDefinition>(qboContextoAuth, customFieldDefinition);
            ////Delete the returned entity
            //Helper.Delete<CustomFieldDefinition>(qboContextoAuth, added);
		}

		#endregion

		#endregion

		#endregion

	}
}
