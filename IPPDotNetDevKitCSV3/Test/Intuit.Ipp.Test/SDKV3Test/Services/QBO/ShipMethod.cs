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
    public class ShipMethodTest
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
        public void ShipMethodAddTestUsingoAuth()
        {
            //Creating the ShipMethod for Add
            ShipMethod shipMethod = QBOHelper.CreateShipMethod(qboContextoAuth);
            //Adding the ShipMethod
            ShipMethod added = Helper.Add<ShipMethod>(qboContextoAuth, shipMethod);
            //Verify the added ShipMethod
            QBOHelper.VerifyShipMethod(shipMethod, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void ShipMethodFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            ShipMethodAddTestUsingoAuth();

            //Retrieving the ShipMethod using FindAll
            List<ShipMethod> shipMethods = Helper.FindAll<ShipMethod>(qboContextoAuth, new ShipMethod(), 1, 500);
            Assert.IsNotNull(shipMethods);
            Assert.IsTrue(shipMethods.Count<ShipMethod>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void ShipMethodFindbyIdTestUsingoAuth()
        {
            //Creating the ShipMethod for Adding
            ShipMethod shipMethod = QBOHelper.CreateShipMethod(qboContextoAuth);
            //Adding the ShipMethod
            ShipMethod added = Helper.Add<ShipMethod>(qboContextoAuth, shipMethod);
            ShipMethod found = Helper.FindById<ShipMethod>(qboContextoAuth, added);
            QBOHelper.VerifyShipMethod(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void ShipMethodUpdateTestUsingoAuth()
        {
            //Creating the ShipMethod for Adding
            ShipMethod shipMethod = QBOHelper.CreateShipMethod(qboContextoAuth);
            //Adding the ShipMethod
            ShipMethod added = Helper.Add<ShipMethod>(qboContextoAuth, shipMethod);
            //Change the data of added entity
            ShipMethod changed = QBOHelper.UpdateShipMethod(qboContextoAuth, added);
            //Update the returned entity data
            ShipMethod updated = Helper.Update<ShipMethod>(qboContextoAuth, changed);//Verify the updated ShipMethod
            QBOHelper.VerifyShipMethod(changed, updated);
        }

        [TestMethod]
        public void ShipMethodSparseUpdateTestUsingoAuth()
        {
            //Creating the ShipMethod for Adding
            ShipMethod shipMethod = QBOHelper.CreateShipMethod(qboContextoAuth);
            //Adding the ShipMethod
            ShipMethod added = Helper.Add<ShipMethod>(qboContextoAuth, shipMethod);
            //Change the data of added entity
            ShipMethod changed = QBOHelper.UpdateShipMethodSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            ShipMethod updated = Helper.Update<ShipMethod>(qboContextoAuth, changed);//Verify the updated ShipMethod
            QBOHelper.VerifyShipMethodSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void ShipMethodDeleteTestUsingoAuth()
        {
            //Creating the ShipMethod for Adding
            ShipMethod shipMethod = QBOHelper.CreateShipMethod(qboContextoAuth);
            //Adding the ShipMethod
            ShipMethod added = Helper.Add<ShipMethod>(qboContextoAuth, shipMethod);
            //Delete the returned entity
            try
            {
                ShipMethod deleted = Helper.Delete<ShipMethod>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void ShipMethodVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            ShipMethod entity = QBOHelper.CreateShipMethod(qboContextoAuth);
            //Adding the entity
            ShipMethod added = Helper.Add<ShipMethod>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                ShipMethod voided = Helper.Void<ShipMethod>(qboContextoAuth, added);
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
        public void ShipMethodCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            ShipMethodAddTestUsingoAuth();

            //Retrieving the ShipMethod using CDC
            List<ShipMethod> entities = Helper.CDC(qboContextoAuth, new ShipMethod(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<ShipMethod>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void ShipMethodBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            ShipMethod existing = Helper.FindOrAdd(qboContextoAuth, new ShipMethod());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateShipMethod(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateShipMethod(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from ShipMethod");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<ShipMethod>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as ShipMethod).Id));
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
        public void ShipMethodQueryUsingoAuth()
        {
            QueryService<ShipMethod> entityQuery = new QueryService<ShipMethod>(qboContextoAuth);
            ShipMethod existing = Helper.FindOrAdd<ShipMethod>(qboContextoAuth, new ShipMethod());
            List<ShipMethod> entities = entityQuery.ExecuteIdsQuery("Select * from Customer where Id='"+existing.Id+"'").ToList();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void ShipMethodAddAsyncTestsUsingoAuth()
        {
            //Creating the ShipMethod for Add
            ShipMethod entity = QBOHelper.CreateShipMethod(qboContextoAuth);

            ShipMethod added = Helper.AddAsync<ShipMethod>(qboContextoAuth, entity);
            QBOHelper.VerifyShipMethod(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void ShipMethodRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            ShipMethodAddTestUsingoAuth();

            //Retrieving the ShipMethod using FindAll
            Helper.FindAllAsync<ShipMethod>(qboContextoAuth, new ShipMethod());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void ShipMethodFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the ShipMethod for Adding
            ShipMethod entity = QBOHelper.CreateShipMethod(qboContextoAuth);
            //Adding the ShipMethod
            ShipMethod added = Helper.Add<ShipMethod>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<ShipMethod>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void ShipMethodUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the ShipMethod for Adding
            ShipMethod entity = QBOHelper.CreateShipMethod(qboContextoAuth);
            //Adding the ShipMethod
            ShipMethod added = Helper.Add<ShipMethod>(qboContextoAuth, entity);

            //Update the ShipMethod
            ShipMethod updated = QBOHelper.UpdateShipMethod(qboContextoAuth, added);
            //Call the service
            ShipMethod updatedReturned = Helper.UpdateAsync<ShipMethod>(qboContextoAuth, updated);
            //Verify updated ShipMethod
            QBOHelper.VerifyShipMethod(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void ShipMethodDeleteAsyncTestsUsingoAuth()
        {
            //Creating the ShipMethod for Adding
            ShipMethod entity = QBOHelper.CreateShipMethod(qboContextoAuth);
            //Adding the ShipMethod
            ShipMethod added = Helper.Add<ShipMethod>(qboContextoAuth, entity);

            Helper.DeleteAsync<ShipMethod>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void ShipMethodVoidAsyncTestsUsingoAuth()
        {
            //Creating the ShipMethod for Adding
            ShipMethod entity = QBOHelper.CreateShipMethod(qboContextoAuth);
            //Adding the ShipMethod
            ShipMethod added = Helper.Add<ShipMethod>(qboContextoAuth, entity);

            Helper.VoidAsync<ShipMethod>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
