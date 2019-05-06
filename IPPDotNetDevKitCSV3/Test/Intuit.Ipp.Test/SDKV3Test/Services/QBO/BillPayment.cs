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
    public class BillPaymentTest
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
        public void BillPaymentCheckAddTestUsingoAuth()
        {
            //Creating the BillPayment for Add
            BillPayment billPayment = QBOHelper.CreateBillPaymentCheck(qboContextoAuth);
            //Adding the BillPayment
            BillPayment added = Helper.Add<BillPayment>(qboContextoAuth, billPayment);
            //Verify the added BillPayment
            QBOHelper.VerifyBillPayment(billPayment, added);
        }

        [TestMethod]
        public void BillPaymentCreditCardkAddTestUsingoAuth()
        {
            //Creating the BillPayment for Add
            BillPayment billPayment = QBOHelper.CreateBillPaymentCreditCard(qboContextoAuth);
            //Adding the BillPayment
            BillPayment added = Helper.Add<BillPayment>(qboContextoAuth, billPayment);
            //Verify the added BillPayment
            QBOHelper.VerifyBillPayment(billPayment, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod] //[Ignore]  //IgnoreReason:  Not Supported
        public void BillPaymentFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            BillPaymentCheckAddTestUsingoAuth();

            //Retrieving the BillPayment using FindAll
            List<BillPayment> billPayments = Helper.FindAll<BillPayment>(qboContextoAuth, new BillPayment(), 1, 500);
            Assert.IsNotNull(billPayments);
            Assert.IsTrue(billPayments.Count<BillPayment>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void BillPaymentFindbyIdTestUsingoAuth()
        {
            //Creating the BillPayment for Adding
            BillPayment billPayment = QBOHelper.CreateBillPaymentCheck(qboContextoAuth);
            //Adding the BillPayment
            BillPayment added = Helper.Add<BillPayment>(qboContextoAuth, billPayment);
            BillPayment found = Helper.FindById<BillPayment>(qboContextoAuth, added);
            QBOHelper.VerifyBillPayment(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void BillPaymentUpdateTestUsingoAuth()
        {
            //Creating the BillPayment for Adding
            BillPayment billPayment = QBOHelper.CreateBillPaymentCheck(qboContextoAuth);
            //Adding the BillPayment
            BillPayment added = Helper.Add<BillPayment>(qboContextoAuth, billPayment);
            //Change the data of added entity
            BillPayment changed =  QBOHelper.UpdateBillPayment(qboContextoAuth, added);
            //Update the returned entity data
            BillPayment updated = Helper.Update<BillPayment>(qboContextoAuth, changed);//Verify the updated BillPayment
            QBOHelper.VerifyBillPayment(changed, updated);
        }

        [TestMethod] [Ignore] //IgnoreReason:  Not Supported
        public void BillPaymentSparseUpdateTestUsingoAuth()
        {
            //Creating the BillPayment for Adding
            BillPayment BillPayment = QBOHelper.CreateBillPaymentCheck(qboContextoAuth);
            //Adding the BillPayment
            BillPayment added = Helper.Add<BillPayment>(qboContextoAuth, BillPayment);
            //Change the data of added entity
            BillPayment changed = QBOHelper.UpdateBillPaymentSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            BillPayment updated = Helper.Update<BillPayment>(qboContextoAuth, changed);//Verify the updated BillPayment
            QBOHelper.VerifyBillPaymentSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]// [Ignore]  //IgnoreReason: https://jira.intuit.com/browse/IPP-3289
        public void BillPaymentDeleteTestUsingoAuth()
        {
            //Creating the BillPayment for Adding
            BillPayment billPayment = QBOHelper.CreateBillPaymentCheck(qboContextoAuth);
            //Adding the BillPayment
            BillPayment added = Helper.Add<BillPayment>(qboContextoAuth, billPayment);
            //Delete the returned entity
            try
            {
                BillPayment deleted = Helper.Delete<BillPayment>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod] [Ignore] //IgnoreReason: Not supported
        public void BillPaymentVoidTestUsingoAuth()
        {
            //Creating the BillPayment for Adding
            BillPayment BillPayment = QBOHelper.CreateBillPaymentCheck(qboContextoAuth);
            //Adding the BillPayment
            BillPayment added = Helper.Add<BillPayment>(qboContextoAuth, BillPayment);
            //Void the returned entity
            try
            {
                BillPayment voided = Helper.Void<BillPayment>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Voided, voided.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        #endregion

        #region Test cases for CDC Operations

        [TestMethod] //[Ignore]  //IgnoreReason:  Not Supported
        public void BillPaymentCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            BillPaymentCheckAddTestUsingoAuth();

            //Retrieving the BillPayment using CDC
            List<BillPayment> entities = Helper.CDC(qboContextoAuth, new BillPayment(), DateTime.Today.AddDays(-100));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<BillPayment>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void BillPaymentBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            BillPayment entity = QBOHelper.CreateBillPaymentCheck(qboContextoAuth);
            BillPayment existing = Helper.Add<BillPayment>(qboContextoAuth, entity);

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateBillPaymentCheck(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateBillPayment(qboContextoAuth, existing));

            //batchEntries.Add(OperationEnum.query, "select * from BillPayment");

            //batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<BillPayment>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as BillPayment).Id));
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
        public void BillPaymentQueryUsingoAuth()
        {
            QueryService<BillPayment> entityQuery = new QueryService<BillPayment>(qboContextoAuth);
            BillPayment existing = Helper.FindOrAdd<BillPayment>(qboContextoAuth, new BillPayment());
            //List<BillPayment> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<BillPayment> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM BillPayment where Id='" + existing.Id + "'").ToList<BillPayment>();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void BillPaymentAddAsyncTestsUsingoAuth()
        {
            //Creating the BillPayment for Add
            BillPayment entity = QBOHelper.CreateBillPaymentCheck(qboContextoAuth);

            BillPayment added = Helper.AddAsync<BillPayment>(qboContextoAuth, entity);
            QBOHelper.VerifyBillPayment(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod] //[Ignore] //IgnoreReason:  Not supported
        public void BillPaymentRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            BillPaymentCheckAddTestUsingoAuth();

            //Retrieving the BillPayment using FindAll
            Helper.FindAllAsync<BillPayment>(qboContextoAuth, new BillPayment());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void BillPaymentFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the BillPayment for Adding
            BillPayment entity = QBOHelper.CreateBillPaymentCheck(qboContextoAuth);
            //Adding the BillPayment
            BillPayment added = Helper.Add<BillPayment>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<BillPayment>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void BillPaymentUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the BillPayment for Adding
            BillPayment entity = QBOHelper.CreateBillPaymentCheck(qboContextoAuth);
            //Adding the BillPayment
            BillPayment added = Helper.Add<BillPayment>(qboContextoAuth, entity);

            //Update the BillPayment
            BillPayment updated = QBOHelper.UpdateBillPayment(qboContextoAuth, added);
            //Call the service
            BillPayment updatedReturned = Helper.UpdateAsync<BillPayment>(qboContextoAuth, updated);
            //Verify updated BillPayment
            QBOHelper.VerifyBillPayment(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod] //[Ignore]  //IgnoreReason: https://jira.intuit.com/browse/IPP-3289
        public void BillPaymentDeleteAsyncTestsUsingoAuth()
        {
            //Creating the BillPayment for Adding
            BillPayment entity = QBOHelper.CreateBillPaymentCheck(qboContextoAuth);
            //Adding the BillPayment
            BillPayment added = Helper.Add<BillPayment>(qboContextoAuth, entity);

            Helper.DeleteAsync<BillPayment>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]  //IgnoreReason: Not Supported
        public void BillPaymentVoidAsyncTestsUsingoAuth()
        {
            //Creating the BillPayment for Adding
            BillPayment entity = QBOHelper.CreateBillPaymentCheck(qboContextoAuth);
            //Adding the BillPayment
            BillPayment added = Helper.Add<BillPayment>(qboContextoAuth, entity);

            Helper.VoidAsync<BillPayment>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
