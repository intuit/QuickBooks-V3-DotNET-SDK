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
	public class NameValueTest
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
		public void NameValueAddTestUsingoAuth()
		{
			//Creating the Bill for Add
			NameValue nameValue = QBOHelper.CreateNameValue(qboContextoAuth);
			//Adding the NameValue
			NameValue added = Helper.Add<NameValue>(qboContextoAuth, nameValue);
			//Verify the added NameValue
			QBOHelper.VerifyNameValue(nameValue, added);
		}

		#endregion

		#region Test cases for FindAll Operations

		[TestMethod]
		public void NameValueFindAllTestUsingoAuth()
		{
			//Making sure that at least one entity is already present
			NameValueAddTestUsingoAuth();

			//Retrieving the Bill using FindAll
			List<NameValue> nameValues = Helper.FindAll<NameValue>(qboContextoAuth, new NameValue(), 1, 500);
			Assert.IsNotNull(nameValues);
			Assert.IsTrue(nameValues.Count<NameValue>() > 0);
		}

		#endregion

		#region Test cases for FindbyId Operations

		[TestMethod]
		public void NameValueFindbyIdTestUsingoAuth()
		{
			//Creating the NameValue for Adding
			NameValue nameValue = QBOHelper.CreateNameValue(qboContextoAuth);
			//Adding the NameValue
			NameValue added = Helper.Add<NameValue>(qboContextoAuth, nameValue);
			NameValue found = Helper.FindById<NameValue>(qboContextoAuth, added);
			QBOHelper.VerifyNameValue(found, added);
		}

		#endregion

		#region Test cases for Update Operations

		[TestMethod]
		public void NameValueUpdateTestUsingoAuth()
		{
			//Creating the NameValue for Adding
			NameValue nameValue = QBOHelper.CreateNameValue(qboContextoAuth);
			//Adding the NameValue
			NameValue added = Helper.Add<NameValue>(qboContextoAuth, nameValue);
			//Change the data of added entity
			QBOHelper.UpdateNameValue(qboContextoAuth, added);
			//Update the returned entity data
			NameValue updated = Helper.Update<NameValue>(qboContextoAuth, added);//Verify the updated NameValue
			QBOHelper.VerifyNameValue(added, updated);
		}

		#endregion

		#region Test cases for Delete Operations

		[TestMethod]
		public void NameValueDeleteTestUsingoAuth()
		{
			//Creating the NameValue for Adding
			NameValue nameValue = QBOHelper.CreateNameValue(qboContextoAuth);
			//Adding the NameValue
			NameValue added = Helper.Add<NameValue>(qboContextoAuth, nameValue);
            //Delete the returned entity
            try
            {
                NameValue deleted = Helper.Delete<NameValue>(qboContextoAuth, added);
                //Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
		}

        [TestMethod]
        public void NameValueVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            NameValue entity = QBOHelper.CreateNameValue(qboContextoAuth);
            //Adding the entity
            NameValue added = Helper.Add<NameValue>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                NameValue voided = Helper.Void<NameValue>(qboContextoAuth, added);
                //Assert.AreEqual(EntityStatusEnum.Voided, voided.status);
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
