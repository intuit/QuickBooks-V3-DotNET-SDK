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
using Intuit.Ipp.QueryFilter;

using System.Collections.ObjectModel;
using Intuit.Ipp.DataService;

namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass] [Ignore]
    public class OtherNameTest
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
        public void OtherNameAddTestUsingoAuth()
        {
            //Creating the OtherName for Add
            OtherName otherName = QBOHelper.CreateOtherName(qboContextoAuth);
            //Adding the OtherName
            OtherName added = Helper.Add<OtherName>(qboContextoAuth, otherName);
            //Verify the added OtherName
            QBOHelper.VerifyOtherName(otherName, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void OtherNameFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            OtherNameAddTestUsingoAuth();

            //Retrieving the OtherName using FindAll
            List<OtherName> otherNames = Helper.FindAll<OtherName>(qboContextoAuth, new OtherName(), 1, 500);
            Assert.IsNotNull(otherNames);
            Assert.IsTrue(otherNames.Count<OtherName>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void OtherNameFindbyIdTestUsingoAuth()
        {
            //Creating the OtherName for Adding
            OtherName otherName = QBOHelper.CreateOtherName(qboContextoAuth);
            //Adding the OtherName
            OtherName added = Helper.Add<OtherName>(qboContextoAuth, otherName);
            OtherName found = Helper.FindById<OtherName>(qboContextoAuth, added);
            QBOHelper.VerifyOtherName(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void OtherNameUpdateTestUsingoAuth()
        {
            //Creating the OtherName for Adding
            OtherName otherName = QBOHelper.CreateOtherName(qboContextoAuth);
            //Adding the OtherName
            OtherName added = Helper.Add<OtherName>(qboContextoAuth, otherName);
            //Change the data of added entity
            OtherName changed = QBOHelper.UpdateOtherName(qboContextoAuth, added);
            //Update the returned entity data
            OtherName updated = Helper.Update<OtherName>(qboContextoAuth, changed);//Verify the updated OtherName
            QBOHelper.VerifyOtherName(changed, updated);
        }

        [TestMethod]
        public void OtherNameSparseUpdateTestUsingoAuth()
        {
            //Creating the OtherName for Adding
            OtherName otherName = QBOHelper.CreateOtherName(qboContextoAuth);
            //Adding the OtherName
            OtherName added = Helper.Add<OtherName>(qboContextoAuth, otherName);
            //Change the data of added entity
            OtherName changed = QBOHelper.UpdateOtherNameSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            OtherName updated = Helper.Update<OtherName>(qboContextoAuth, changed);//Verify the updated OtherName
            QBOHelper.VerifyOtherNameSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void OtherNameDeleteTestUsingoAuth()
        {
            //Creating the OtherName for Adding
            OtherName otherName = QBOHelper.CreateOtherName(qboContextoAuth);
            //Adding the OtherName
            OtherName added = Helper.Add<OtherName>(qboContextoAuth, otherName);
            //Delete the returned entity
            try
            {
                OtherName deleted = Helper.Delete<OtherName>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void OtherNameVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            OtherName entity = QBOHelper.CreateOtherName(qboContextoAuth);
            //Adding the entity
            OtherName added = Helper.Add<OtherName>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                OtherName voided = Helper.Void<OtherName>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Voided, voided.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        #endregion

        #region Test cases for CDC Operations

        [TestMethod]
        public void OtherNameCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            OtherNameAddTestUsingoAuth();

            //Retrieving the OtherName using CDC
            List<OtherName> entities = Helper.CDC(qboContextoAuth, new OtherName(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<OtherName>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void OtherNameBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            OtherName existing = Helper.FindOrAdd(qboContextoAuth, new OtherName());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateOtherName(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateOtherName(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from OtherName");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<OtherName>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as OtherName).Id));
                }
                else if (resp.ResponseType == ResponseType.Query)
                {
                    Assert.IsTrue(resp.Entities != null && resp.Entities.Count > 0);
                }
                else if (resp.ResponseType == ResponseType.CdcQuery)
                {
                    Assert.IsTrue(resp.CDCResponse != null && resp.CDCResponse.entities != null && resp.CDCResponse.entities.Count > 0);
                }

                position++;
            }
        }

        #endregion

        #region Test cases for Query
        [TestMethod]
        public void OtherNameQueryUsingoAuth()
        {
            QueryService<OtherName> entityQuery = new QueryService<OtherName>(qboContextoAuth);
            OtherName existing = Helper.FindOrAdd<OtherName>(qboContextoAuth, new OtherName());
            int count = entityQuery.ExecuteIdsQuery("Select * from OtherName where Id='" + existing.Id + "'").Count;
            Assert.IsTrue(count > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void OtherNameAddAsyncTestsUsingoAuth()
        {
            //Creating the OtherName for Add
            OtherName entity = QBOHelper.CreateOtherName(qboContextoAuth);

            OtherName added = Helper.AddAsync<OtherName>(qboContextoAuth, entity);
            QBOHelper.VerifyOtherName(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void OtherNameRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            OtherNameAddTestUsingoAuth();

            //Retrieving the OtherName using FindAll
            Helper.FindAllAsync<OtherName>(qboContextoAuth, new OtherName());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void OtherNameFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the OtherName for Adding
            OtherName entity = QBOHelper.CreateOtherName(qboContextoAuth);
            //Adding the OtherName
            OtherName added = Helper.Add<OtherName>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<OtherName>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void OtherNameUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the OtherName for Adding
            OtherName entity = QBOHelper.CreateOtherName(qboContextoAuth);
            //Adding the OtherName
            OtherName added = Helper.Add<OtherName>(qboContextoAuth, entity);

            //Update the OtherName
            OtherName updated = QBOHelper.UpdateOtherName(qboContextoAuth, added);
            //Call the service
            OtherName updatedReturned = Helper.UpdateAsync<OtherName>(qboContextoAuth, updated);
            //Verify updated OtherName
            QBOHelper.VerifyOtherName(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void OtherNameDeleteAsyncTestsUsingoAuth()
        {
            //Creating the OtherName for Adding
            OtherName entity = QBOHelper.CreateOtherName(qboContextoAuth);
            //Adding the OtherName
            OtherName added = Helper.Add<OtherName>(qboContextoAuth, entity);

            Helper.DeleteAsync<OtherName>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void OtherNameVoidAsyncTestsUsingoAuth()
        {
            //Creating the OtherName for Adding
            OtherName entity = QBOHelper.CreateOtherName(qboContextoAuth);
            //Adding the OtherName
            OtherName added = Helper.Add<OtherName>(qboContextoAuth, entity);

            Helper.VoidAsync<OtherName>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
