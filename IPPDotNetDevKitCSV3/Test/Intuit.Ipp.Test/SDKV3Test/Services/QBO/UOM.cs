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
    public class UOMTest
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
        public void UOMAddTestUsingoAuth()
        {
            //Creating the UOM for Add
            UOM uOM = QBOHelper.CreateUOM(qboContextoAuth);
            //Adding the UOM
            UOM added = Helper.Add<UOM>(qboContextoAuth, uOM);
            //Verify the added UOM
            QBOHelper.VerifyUOM(uOM, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void UOMFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            UOMAddTestUsingoAuth();

            //Retrieving the UOM using FindAll
            List<UOM> uOMs = Helper.FindAll<UOM>(qboContextoAuth, new UOM(), 1, 500);
            Assert.IsNotNull(uOMs);
            Assert.IsTrue(uOMs.Count<UOM>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void UOMFindbyIdTestUsingoAuth()
        {
            //Creating the UOM for Adding
            UOM uOM = QBOHelper.CreateUOM(qboContextoAuth);
            //Adding the UOM
            UOM added = Helper.Add<UOM>(qboContextoAuth, uOM);
            UOM found = Helper.FindById<UOM>(qboContextoAuth, added);
            QBOHelper.VerifyUOM(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void UOMUpdateTestUsingoAuth()
        {
            //Creating the UOM for Adding
            UOM uOM = QBOHelper.CreateUOM(qboContextoAuth);
            //Adding the UOM
            UOM added = Helper.Add<UOM>(qboContextoAuth, uOM);
            //Change the data of added entity
            UOM changed = QBOHelper.UpdateUOM(qboContextoAuth, added);
            //Update the returned entity data
            UOM updated = Helper.Update<UOM>(qboContextoAuth, changed);//Verify the updated UOM
            QBOHelper.VerifyUOM(changed, updated);
        }

        [TestMethod]
        public void UOMSparseUpdateTestUsingoAuth()
        {
            //Creating the UOM for Adding
            UOM uOM = QBOHelper.CreateUOM(qboContextoAuth);
            //Adding the UOM
            UOM added = Helper.Add<UOM>(qboContextoAuth, uOM);
            //Change the data of added entity
            UOM changed = QBOHelper.UpdateUOMSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            UOM updated = Helper.Update<UOM>(qboContextoAuth, changed);//Verify the updated UOM
            QBOHelper.VerifyUOMSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void UOMDeleteTestUsingoAuth()
        {
            //Creating the UOM for Adding
            UOM uOM = QBOHelper.CreateUOM(qboContextoAuth);
            //Adding the UOM
            UOM added = Helper.Add<UOM>(qboContextoAuth, uOM);
            //Delete the returned entity
            try
            {
                UOM deleted = Helper.Delete<UOM>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UserVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            User entity = QBOHelper.CreateUser(qboContextoAuth);
            //Adding the entity
            User added = Helper.Add<User>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                User voided = Helper.Void<User>(qboContextoAuth, added);
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
        public void UOMCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            UOMAddTestUsingoAuth();

            //Retrieving the UOM using CDC
            List<UOM> entities = Helper.CDC(qboContextoAuth, new UOM(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<UOM>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void UOMBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            UOM existing = Helper.FindOrAdd(qboContextoAuth, new UOM());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateUOM(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateUOM(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from UOM");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<UOM>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as UOM).Id));
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
        public void UOMQueryUsingoAuth()
        {
            QueryService<UOM> entityQuery = new QueryService<UOM>(qboContextoAuth);
            UOM existing = Helper.FindOrAdd<UOM>(qboContextoAuth, new UOM());
            //List<UOM> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            int count = entityQuery.ExecuteIdsQuery("Select * from UOM where Id='" + existing.Id + "'").Count;
            Assert.IsTrue(count > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void UOMAddAsyncTestsUsingoAuth()
        {
            //Creating the UOM for Add
            UOM entity = QBOHelper.CreateUOM(qboContextoAuth);

            UOM added = Helper.AddAsync<UOM>(qboContextoAuth, entity);
            QBOHelper.VerifyUOM(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void UOMRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            UOMAddTestUsingoAuth();

            //Retrieving the UOM using FindAll
            Helper.FindAllAsync<UOM>(qboContextoAuth, new UOM());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void UOMFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the UOM for Adding
            UOM entity = QBOHelper.CreateUOM(qboContextoAuth);
            //Adding the UOM
            UOM added = Helper.Add<UOM>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<UOM>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void UOMUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the UOM for Adding
            UOM entity = QBOHelper.CreateUOM(qboContextoAuth);
            //Adding the UOM
            UOM added = Helper.Add<UOM>(qboContextoAuth, entity);

            //Update the UOM
            UOM updated = QBOHelper.UpdateUOM(qboContextoAuth, added);
            //Call the service
            UOM updatedReturned = Helper.UpdateAsync<UOM>(qboContextoAuth, updated);
            //Verify updated UOM
            QBOHelper.VerifyUOM(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void UOMDeleteAsyncTestsUsingoAuth()
        {
            //Creating the UOM for Adding
            UOM entity = QBOHelper.CreateUOM(qboContextoAuth);
            //Adding the UOM
            UOM added = Helper.Add<UOM>(qboContextoAuth, entity);

            Helper.DeleteAsync<UOM>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void UOMVoidAsyncTestsUsingoAuth()
        {
            //Creating the UOM for Adding
            UOM entity = QBOHelper.CreateUOM(qboContextoAuth);
            //Adding the UOM
            UOM added = Helper.Add<UOM>(qboContextoAuth, entity);

            Helper.VoidAsync<UOM>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
