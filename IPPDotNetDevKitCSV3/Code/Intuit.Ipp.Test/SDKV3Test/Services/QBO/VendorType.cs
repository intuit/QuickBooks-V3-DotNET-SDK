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
//
using System.Collections.ObjectModel;
using Intuit.Ipp.DataService;

namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass] [Ignore]
    public class VendorTypeTest
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
        public void VendorTypeAddTestUsingoAuth()
        {
            //Creating the VendorType for Add
            VendorType vendorType = QBOHelper.CreateVendorType(qboContextoAuth);
            //Adding the VendorType
            VendorType added = Helper.Add<VendorType>(qboContextoAuth, vendorType);
            //Verify the added VendorType
            QBOHelper.VerifyVendorType(vendorType, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void VendorTypeFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            VendorTypeAddTestUsingoAuth();

            //Retrieving the VendorType using FindAll
            List<VendorType> vendorTypes = Helper.FindAll<VendorType>(qboContextoAuth, new VendorType(), 1, 500);
            Assert.IsNotNull(vendorTypes);
            Assert.IsTrue(vendorTypes.Count<VendorType>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void VendorTypeFindbyIdTestUsingoAuth()
        {
            //Creating the VendorType for Adding
            VendorType vendorType = QBOHelper.CreateVendorType(qboContextoAuth);
            //Adding the VendorType
            VendorType added = Helper.Add<VendorType>(qboContextoAuth, vendorType);
            VendorType found = Helper.FindById<VendorType>(qboContextoAuth, added);
            QBOHelper.VerifyVendorType(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void VendorTypeUpdateTestUsingoAuth()
        {
            //Creating the VendorType for Adding
            VendorType vendorType = QBOHelper.CreateVendorType(qboContextoAuth);
            //Adding the VendorType
            VendorType added = Helper.Add<VendorType>(qboContextoAuth, vendorType);
            //Change the data of added entity
            VendorType changed = QBOHelper.UpdateVendorType(qboContextoAuth, added);
            //Update the returned entity data
            VendorType updated = Helper.Update<VendorType>(qboContextoAuth, changed);//Verify the updated VendorType
            QBOHelper.VerifyVendorType(changed, updated);
        }


        [TestMethod]
        public void VendorTypeSparseUpdateTestUsingoAuth()
        {
            //Creating the VendorType for Adding
            VendorType vendorType = QBOHelper.CreateVendorType(qboContextoAuth);
            //Adding the VendorType
            VendorType added = Helper.Add<VendorType>(qboContextoAuth, vendorType);
            //Change the data of added entity
            VendorType changed = QBOHelper.UpdateVendorTypeSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            VendorType updated = Helper.Update<VendorType>(qboContextoAuth, changed);//Verify the updated VendorType
            QBOHelper.VerifyVendorTypeSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void VendorTypeDeleteTestUsingoAuth()
        {
            //Creating the VendorType for Adding
            VendorType vendorType = QBOHelper.CreateVendorType(qboContextoAuth);
            //Adding the VendorType
            VendorType added = Helper.Add<VendorType>(qboContextoAuth, vendorType);
            //Delete the returned entity
            try
            {
                VendorType deleted = Helper.Delete<VendorType>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void VendorTypeVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            VendorType entity = QBOHelper.CreateVendorType(qboContextoAuth);
            //Adding the entity
            VendorType added = Helper.Add<VendorType>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                VendorType voided = Helper.Void<VendorType>(qboContextoAuth, added);
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
        public void VendorTypeCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            VendorTypeAddTestUsingoAuth();

            //Retrieving the VendorType using CDC
            List<VendorType> entities = Helper.CDC(qboContextoAuth, new VendorType(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<VendorType>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void VendorTypeBatchUsingoAuth()
        {
             Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            VendorType existing = Helper.FindOrAdd(qboContextoAuth, new VendorType());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateVendorType(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateVendorType(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from VendorType");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<VendorType>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as VendorType).Id));
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
        public void VendorQueryUsingoAuth()
        {
            QueryService<VendorType> entityQuery = new QueryService<VendorType>(qboContextoAuth);
            VendorType existing = Helper.FindOrAdd<VendorType>(qboContextoAuth, new VendorType());
            //List<VendorType> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            int count=entityQuery.ExecuteIdsQuery("Select * from VendorType where Id='"+existing.Id+"'").Count;
            Assert.IsTrue(count > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void VendorTypeAddAsyncTestsUsingoAuth()
        {
            //Creating the VendorType for Add
            VendorType entity = QBOHelper.CreateVendorType(qboContextoAuth);

            VendorType added = Helper.AddAsync<VendorType>(qboContextoAuth, entity);
            QBOHelper.VerifyVendorType(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void VendorTypeRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            VendorTypeAddTestUsingoAuth();

            //Retrieving the VendorType using FindAll
            Helper.FindAllAsync<VendorType>(qboContextoAuth, new VendorType());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void VendorTypeFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the VendorType for Adding
            VendorType entity = QBOHelper.CreateVendorType(qboContextoAuth);
            //Adding the VendorType
            VendorType added = Helper.Add<VendorType>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<VendorType>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void VendorTypeUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the VendorType for Adding
            VendorType entity = QBOHelper.CreateVendorType(qboContextoAuth);
            //Adding the VendorType
            VendorType added = Helper.Add<VendorType>(qboContextoAuth, entity);

            //Update the VendorType
            VendorType updated = QBOHelper.UpdateVendorType(qboContextoAuth, added);
            //Call the service
            VendorType updatedReturned = Helper.UpdateAsync<VendorType>(qboContextoAuth, updated);
            //Verify updated VendorType
            QBOHelper.VerifyVendorType(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void VendorTypeDeleteAsyncTestsUsingoAuth()
        {
            //Creating the VendorType for Adding
            VendorType entity = QBOHelper.CreateVendorType(qboContextoAuth);
            //Adding the VendorType
            VendorType added = Helper.Add<VendorType>(qboContextoAuth, entity);

            Helper.DeleteAsync<VendorType>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void VendorTypeVoidAsyncTestsUsingoAuth()
        {
            //Creating the VendorType for Adding
            VendorType entity = QBOHelper.CreateVendorType(qboContextoAuth);
            //Adding the VendorType
            VendorType added = Helper.Add<VendorType>(qboContextoAuth, entity);

            Helper.VoidAsync<VendorType>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
