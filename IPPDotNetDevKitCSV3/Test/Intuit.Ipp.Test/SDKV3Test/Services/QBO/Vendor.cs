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
using System.Collections.ObjectModel;
using Intuit.Ipp.DataService;
using Intuit.Ipp.QueryFilter;


namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass]
    public class VendorTest
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
        public void VendorAddTestUsingoAuth()
        {
            //Creating the Vendor for Add
            Vendor vendor = QBOHelper.CreateVendor(qboContextoAuth);
            //Adding the Vendor
            Vendor added = Helper.Add<Vendor>(qboContextoAuth, vendor);
            //Verify the added Vendor
            QBOHelper.VerifyVendor(vendor, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void VendorFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            VendorAddTestUsingoAuth();

            //Retrieving the Vendor using FindAll
            List<Vendor> vendors = Helper.FindAll<Vendor>(qboContextoAuth, new Vendor(), 1, 500);
            Assert.IsNotNull(vendors);
            Assert.IsTrue(vendors.Count<Vendor>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void VendorFindbyIdTestUsingoAuth()
        {
            //Creating the Vendor for Adding
            Vendor vendor = QBOHelper.CreateVendor(qboContextoAuth);
            //Adding the Vendor
            Vendor added = Helper.Add<Vendor>(qboContextoAuth, vendor);
            Vendor found = Helper.FindById<Vendor>(qboContextoAuth, added);
            QBOHelper.VerifyVendor(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void VendorUpdateTestUsingoAuth()
        {
            //Creating the Vendor for Adding
            Vendor vendor = QBOHelper.CreateVendor(qboContextoAuth);
            //Adding the Vendor
            Vendor added = Helper.Add<Vendor>(qboContextoAuth, vendor);
            //Change the data of added entity
            Vendor changed = QBOHelper.UpdateVendor(qboContextoAuth, added);
            //Update the returned entity data
            Vendor updated = Helper.Update<Vendor>(qboContextoAuth, changed);//Verify the updated Vendor
            QBOHelper.VerifyVendor(changed, updated);
        }

        [TestMethod]
        public void VendorSparseUpdateTestUsingoAuth()
        {
            //Creating the Vendor for Adding
            Vendor vendor = QBOHelper.CreateVendor(qboContextoAuth);
            //Adding the Vendor
            Vendor added = Helper.Add<Vendor>(qboContextoAuth, vendor);
            //Change the data of added entity
            Vendor changed = QBOHelper.SparseUpdateVendor(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Vendor updated = Helper.Update<Vendor>(qboContextoAuth, changed);//Verify the updated Vendor
            QBOHelper.VerifyVendorSparseUpdate(changed, updated);
        }

        #endregion

        
        #region  Test cases for Updateaccountontxns Operations

        [TestMethod]
        public void VendorUpdateAccountOnTxnsTestUsingoAuthFrance()
        {
            
            //Creating the Vendor for Adding
            Vendor vendor = QBOHelper.CreateVendorFrance(qboContextoAuth);
            //Adding the Vendor
            Vendor added = Helper.Add<Vendor>(qboContextoAuth, vendor);
            //Change the data of added entity
            Vendor changed = QBOHelper.UpdateVendorFrance(qboContextoAuth, added);
            //Update the returned entity data
            Vendor updated = Helper.UpdateAccountOnTxnsFrance<Vendor>(qboContextoAuth, changed);//Verify the updated Vendor
            QBOHelper.VerifyVendorFrance(changed, updated);
        }


        #endregion
        

        #region Test cases for Delete Operations

        [TestMethod] [Ignore]  //TestComment: Returns Operation Not Supported
        public void VendorDeleteTestUsingoAuth()
        {
            //Creating the Vendor for Adding
            Vendor vendor = QBOHelper.CreateVendor(qboContextoAuth);
            //Adding the Vendor
            Vendor added = Helper.Add<Vendor>(qboContextoAuth, vendor);
            //Delete the returned entity
            try
            {
                Vendor deleted = Helper.Delete<Vendor>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }


        [TestMethod] [Ignore]
        public void VendorVoidTestUsingoAuth()
        {
            //Creating the Vendor for Adding
            Vendor vendor = QBOHelper.CreateVendor(qboContextoAuth);
            //Adding the Vendor
            Vendor added = Helper.Add<Vendor>(qboContextoAuth, vendor);
            //Void the returned entity
            try
            {
                Vendor voided = Helper.Void<Vendor>(qboContextoAuth, added);
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
        public void VendorCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            VendorAddTestUsingoAuth();

            //Retrieving the Vendor using FindAll
            List<Vendor> vendors = Helper.CDC(qboContextoAuth, new Vendor(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(vendors);
            Assert.IsTrue(vendors.Count<Vendor>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void VendorBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Vendor existing = Helper.FindOrAdd(qboContextoAuth, new Vendor());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateVendor(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateVendor(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Vendor");

         //   batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Vendor>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Vendor).Id));
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
            QueryService<Vendor> entityQuery = new QueryService<Vendor>(qboContextoAuth);
            Vendor existing = Helper.FindOrAdd<Vendor>(qboContextoAuth, new Vendor());
            //List<Vendor> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<Vendor> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Vendor where Id='" + existing.Id + "'").ToList<Vendor>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void VendorAddAsyncTestsUsingoAuth()
        {
            //Creating the Vendor for Add
            Vendor entity = QBOHelper.CreateVendor(qboContextoAuth);

            Vendor added = Helper.AddAsync<Vendor>(qboContextoAuth, entity);
            QBOHelper.VerifyVendor(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void VendorRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            VendorAddTestUsingoAuth();

            //Retrieving the Vendor using FindAll
            Helper.FindAllAsync<Vendor>(qboContextoAuth, new Vendor());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void VendorFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Vendor for Adding
            Vendor entity = QBOHelper.CreateVendor(qboContextoAuth);
            //Adding the Vendor
            Vendor added = Helper.Add<Vendor>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Vendor>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void VendorUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Vendor for Adding
            Vendor entity = QBOHelper.CreateVendor(qboContextoAuth);
            //Adding the Vendor
            Vendor added = Helper.Add<Vendor>(qboContextoAuth, entity);

            //Update the Vendor
            Vendor updated = QBOHelper.UpdateVendor(qboContextoAuth, added);
            //Call the service
            Vendor updatedReturned = Helper.UpdateAsync<Vendor>(qboContextoAuth, updated);
            //Verify updated Vendor
            QBOHelper.VerifyVendor(updated, updatedReturned);
        }

        #endregion

       
        #region  Test cases for Updateaccountontxns Operations

        [TestMethod]
        public void VendorUpdateAccountOnTxnsAsyncTestUsingoAuthFrance()
        {

            //Creating the Vendor for Adding
            Vendor vendor = QBOHelper.CreateVendorFrance(qboContextoAuth);
            //Adding the Vendor
            Vendor added = Helper.Add<Vendor>(qboContextoAuth, vendor);
            //Change the data of added entity
            Vendor changed = QBOHelper.UpdateVendorFrance(qboContextoAuth, added);
            //Update the returned entity data
        
            Vendor updated = Helper.UpdateAccountOnTxnsAsyncFrance<Vendor>(qboContextoAuth, changed);//Verify the updated Vendor
            QBOHelper.VerifyVendorFrance(changed, updated);
        }


        #endregion
       


        #region Test Cases for Delete Operation

        [TestMethod] [Ignore]  //TestComment: Returns Operation Not Supported
        public void VendorDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Vendor for Adding
            Vendor entity = QBOHelper.CreateVendor(qboContextoAuth);
            //Adding the Vendor
            Vendor added = Helper.Add<Vendor>(qboContextoAuth, entity);

            Helper.DeleteAsync<Vendor>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void VendorVoidAsyncTestsUsingoAuth()
        {
            //Creating the Vendor for Adding
            Vendor entity = QBOHelper.CreateVendor(qboContextoAuth);
            //Adding the Vendor
            Vendor added = Helper.Add<Vendor>(qboContextoAuth, entity);

            Helper.VoidAsync<Vendor>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
