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
    [TestClass]
    public class VendorCreditTest
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
        public void VendorCreditAddTestUsingoAuth()
        {
            //Creating the VendorCredit for Add
            VendorCredit vendorCredit = QBOHelper.CreateVendorCredit(qboContextoAuth);
            //Adding the VendorCredit
            VendorCredit added = Helper.Add<VendorCredit>(qboContextoAuth, vendorCredit);
            //Verify the added VendorCredit
            QBOHelper.VerifyVendorCredit(vendorCredit, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]// [Ignore]  //IgnoreReason: Not Supported
        public void VendorCreditFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            VendorCreditAddTestUsingoAuth();

            //Retrieving the VendorCredit using FindAll
            List<VendorCredit> vendorCredits = Helper.FindAll<VendorCredit>(qboContextoAuth, new VendorCredit(), 1, 500);
            Assert.IsNotNull(vendorCredits);
            Assert.IsTrue(vendorCredits.Count<VendorCredit>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void VendorCreditFindbyIdTestUsingoAuth()
        {
            //Creating the VendorCredit for Adding
            VendorCredit vendorCredit = QBOHelper.CreateVendorCredit(qboContextoAuth);
            //Adding the VendorCredit
            VendorCredit added = Helper.Add<VendorCredit>(qboContextoAuth, vendorCredit);
            VendorCredit found = Helper.FindById<VendorCredit>(qboContextoAuth, added);
            QBOHelper.VerifyVendorCredit(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void VendorCreditUpdateTestUsingoAuth()
        {
            //Creating the VendorCredit for Adding
            VendorCredit vendorCredit = QBOHelper.CreateVendorCredit(qboContextoAuth);
            //Adding the VendorCredit
            VendorCredit added = Helper.Add<VendorCredit>(qboContextoAuth, vendorCredit);
            //Change the data of added entity
            VendorCredit changed = QBOHelper.UpdateVendorCredit(qboContextoAuth, added);
            //Update the returned entity data
            VendorCredit updated = Helper.Update<VendorCredit>(qboContextoAuth, changed);//Verify the updated VendorCredit
            QBOHelper.VerifyVendorCredit(changed, updated);
        }

        [TestMethod]
        public void VendorCreditSparseUpdateTestUsingoAuth()
        {
            //Creating the VendorCredit for Adding
            VendorCredit vendorCredit = QBOHelper.CreateVendorCredit(qboContextoAuth);
            //Adding the VendorCredit
            VendorCredit added = Helper.Add<VendorCredit>(qboContextoAuth, vendorCredit);
            //Change the data of added entity
            VendorCredit changed = QBOHelper.UpdateVendorCreditSparse(qboContextoAuth, added.Id, added.SyncToken, added.VendorRef);
            //Update the returned entity data
            VendorCredit updated = Helper.Update<VendorCredit>(qboContextoAuth, changed);//Verify the updated VendorCredit
            QBOHelper.VerifyVendorCreditSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void VendorCreditDeleteTestUsingoAuth()
        {
            //Creating the VendorCredit for Adding
            VendorCredit vendorCredit = QBOHelper.CreateVendorCredit(qboContextoAuth);
            //Adding the VendorCredit
            VendorCredit added = Helper.Add<VendorCredit>(qboContextoAuth, vendorCredit);
            //Delete the returned entity
            try
            {
                VendorCredit deleted = Helper.Delete<VendorCredit>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod] [Ignore] //IgnoreReason: Not supported
        public void VendorCreditVoidTestUsingoAuth()
        {
            //Creating the VendorCredit for Adding
            VendorCredit vendorCredit = QBOHelper.CreateVendorCredit(qboContextoAuth);
            //Adding the VendorCredit
            VendorCredit added = Helper.Add<VendorCredit>(qboContextoAuth, vendorCredit);
            try
            {
                VendorCredit voided = Helper.Void<VendorCredit>(qboContextoAuth, added);
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
        public void VendorCreditCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            VendorCreditAddTestUsingoAuth();

            //Retrieving the VendorCredit using CDC
            List<VendorCredit> entities = Helper.CDC(qboContextoAuth, new VendorCredit(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<VendorCredit>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void VendorCreditBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            //VendorCredit existing = Helper.FindOrAdd(qboContextoAuth, new VendorCredit());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateVendorCredit(qboContextoAuth));

            //batchEntries.Add(OperationEnum.update, QBOHelper.UpdateVendorCredit(qboContextoAuth, existing));

            //batchEntries.Add(OperationEnum.query, "select * from VendorCredit");

            //batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<VendorCredit>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as VendorCredit).Id));
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
        [TestMethod] //[Ignore]  //IgnoreReason: Not Supported
        public void VendorCreditQueryUsingoAuth()
        {
            QueryService<VendorCredit> entityQuery = new QueryService<VendorCredit>(qboContextoAuth);
            VendorCredit existing = Helper.FindOrAdd<VendorCredit>(qboContextoAuth, new VendorCredit());
            //List<VendorCredit> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<VendorCredit> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM VendorCredit where Id='" + existing.Id + "'").ToList<VendorCredit>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void VendorCreditAddAsyncTestsUsingoAuth()
        {
            //Creating the VendorCredit for Add
            VendorCredit entity = QBOHelper.CreateVendorCredit(qboContextoAuth);

            VendorCredit added = Helper.AddAsync<VendorCredit>(qboContextoAuth, entity);
            QBOHelper.VerifyVendorCredit(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod] //[Ignore]  //IgnoreReason:  Not Supported
        public void VendorCreditRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            VendorCreditAddTestUsingoAuth();

            //Retrieving the VendorCredit using FindAll
            Helper.FindAllAsync<VendorCredit>(qboContextoAuth, new VendorCredit());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void VendorCreditFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the VendorCredit for Adding
            VendorCredit entity = QBOHelper.CreateVendorCredit(qboContextoAuth);
            //Adding the VendorCredit
            VendorCredit added = Helper.Add<VendorCredit>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<VendorCredit>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void VendorCreditUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the VendorCredit for Adding
            VendorCredit entity = QBOHelper.CreateVendorCredit(qboContextoAuth);
            //Adding the VendorCredit
            VendorCredit added = Helper.Add<VendorCredit>(qboContextoAuth, entity);

            //Update the VendorCredit
            VendorCredit updated = QBOHelper.UpdateVendorCredit(qboContextoAuth, added);
            //Call the service
            VendorCredit updatedReturned = Helper.UpdateAsync<VendorCredit>(qboContextoAuth, updated);
            //Verify updated VendorCredit
            QBOHelper.VerifyVendorCredit(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void VendorCreditDeleteAsyncTestsUsingoAuth()
        {
            //Creating the VendorCredit for Adding
            VendorCredit entity = QBOHelper.CreateVendorCredit(qboContextoAuth);
            //Adding the VendorCredit
            VendorCredit added = Helper.Add<VendorCredit>(qboContextoAuth, entity);

            Helper.DeleteAsync<VendorCredit>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore] //IgnoreReason: Not supported
        public void VendorCreditVoidAsyncTestsUsingoAuth()
        {
            //Creating the VendorCredit for Adding
            VendorCredit entity = QBOHelper.CreateVendorCredit(qboContextoAuth);
            //Adding the VendorCredit
            VendorCredit added = Helper.Add<VendorCredit>(qboContextoAuth, entity);

            Helper.VoidAsync<VendorCredit>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
