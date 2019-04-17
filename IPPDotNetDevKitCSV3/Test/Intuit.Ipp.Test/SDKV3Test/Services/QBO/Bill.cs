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
    public class BillTest
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
        public void BillAddTestUsingoAuth()
        {
            //Creating the Bill for Add
            Bill bill = QBOHelper.CreateBill(qboContextoAuth);
            //Adding the Bill
            Bill added = Helper.Add<Bill>(qboContextoAuth, bill);
            //Verify the added Bill
            QBOHelper.VerifyBill(bill, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void BillFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            BillAddTestUsingoAuth();

            //Retrieving the Bill using FindAll
            List<Bill> bills = Helper.FindAll<Bill>(qboContextoAuth, new Bill(), 1, 500);
            Assert.IsNotNull(bills);
            Assert.IsTrue(bills.Count<Bill>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void BillFindbyIdTestUsingoAuth()
        {
            //Creating the Bill for Adding
            Bill bill = QBOHelper.CreateBill(qboContextoAuth);
            //Adding the Bill
            Bill added = Helper.Add<Bill>(qboContextoAuth, bill);
            Bill found = Helper.FindById<Bill>(qboContextoAuth, added);
            QBOHelper.VerifyBill(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void BillUpdateTestUsingoAuth()
        {
            //Creating the Bill for Adding
            Bill bill = QBOHelper.CreateBill(qboContextoAuth);
            //Adding the Bill
            Bill added = Helper.Add<Bill>(qboContextoAuth, bill);
            //Change the data of added entity
            Bill changed = QBOHelper.UpdateBill(qboContextoAuth, added);
            //Update the returned entity data
            Bill updated = Helper.Update<Bill>(qboContextoAuth, changed);//Verify the updated Bill
            QBOHelper.VerifyBill(changed, updated);
        }

        [TestMethod]
        public void BillSparseUpdateTestUsingoAuth()
        {
            //Creating the Bill for Adding
            Bill bill = QBOHelper.CreateBill(qboContextoAuth);
            //Adding the Bill
            Bill added = Helper.Add<Bill>(qboContextoAuth, bill);
            //Change the data of added entity
            Bill changed = QBOHelper.UpdateBillSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Bill updated = Helper.Update<Bill>(qboContextoAuth, changed);//Verify the updated Bill
            QBOHelper.VerifyBillSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void BillDeleteTestUsingoAuth()
        {
            //Creating the Bill for Adding
            Bill bill = QBOHelper.CreateBill(qboContextoAuth);
            //Adding the Bill
            Bill added = Helper.Add<Bill>(qboContextoAuth, bill);
            //Delete the returned entity
            try
            {
                Bill deleted = Helper.Delete<Bill>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod] [Ignore]
        public void BillVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            Bill entity = QBOHelper.CreateBill(qboContextoAuth);
            //Adding the entity
            Bill added = Helper.Add<Bill>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                Bill voided = Helper.Void<Bill>(qboContextoAuth, added);
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
        public void BillCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            BillAddTestUsingoAuth();

            //Retrieving the Bill using CDC
            List<Bill> entities = Helper.CDC(qboContextoAuth, new Bill(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<Bill>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void BillBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Bill existing = Helper.FindOrAdd(qboContextoAuth, new Bill());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateBill(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateBill(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Bill");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Bill>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Bill).Id));
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
        public void BillQueryUsingoAuth()
        {
            QueryService<Bill> entityQuery = new QueryService<Bill>(qboContextoAuth);
            Bill existing = Helper.FindOrAdd<Bill>(qboContextoAuth, new Bill());
            //List<Bill> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<Bill> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Bill where Id='" + existing.Id + "'").ToList<Bill>();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void BillAddAsyncTestsUsingoAuth()
        {
            //Creating the Bill for Add
            Bill entity = QBOHelper.CreateBill(qboContextoAuth);

            Bill added = Helper.AddAsync<Bill>(qboContextoAuth, entity);
            QBOHelper.VerifyBill(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void BillRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            BillAddTestUsingoAuth();

            //Retrieving the Bill using FindAll
            Helper.FindAllAsync<Bill>(qboContextoAuth, new Bill());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void BillFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Bill for Adding
            Bill entity = QBOHelper.CreateBill(qboContextoAuth);
            //Adding the Bill
            Bill added = Helper.Add<Bill>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Bill>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void BillUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Bill for Adding
            Bill entity = QBOHelper.CreateBill(qboContextoAuth);
            //Adding the Bill
            Bill added = Helper.Add<Bill>(qboContextoAuth, entity);

            //Update the Bill
            Bill updated = QBOHelper.UpdateBill(qboContextoAuth, added);
            //Call the service
            Bill updatedReturned = Helper.UpdateAsync<Bill>(qboContextoAuth, updated);
            //Verify updated Bill
            QBOHelper.VerifyBill(updated, updatedReturned);
        }

        [TestMethod]
        public void BillSparseUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Bill for Adding
            Bill entity = QBOHelper.CreateBill(qboContextoAuth);
            //Adding the Bill
            Bill added = Helper.Add<Bill>(qboContextoAuth, entity);

            //Update the Bill
            Bill updated = QBOHelper.UpdateBillSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Call the service
            Bill updatedReturned = Helper.UpdateAsync<Bill>(qboContextoAuth, updated);
            //Verify updated Bill
            QBOHelper.VerifyBillSparse(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void BillDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Bill for Adding
            Bill entity = QBOHelper.CreateBill(qboContextoAuth);
            //Adding the Bill
            Bill added = Helper.Add<Bill>(qboContextoAuth, entity);

            Helper.DeleteAsync<Bill>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void BillVoidAsyncTestsUsingoAuth()
        {
            //Creating the Bill for Adding
            Bill entity = QBOHelper.CreateBill(qboContextoAuth);
            //Adding the Bill
            Bill added = Helper.Add<Bill>(qboContextoAuth, entity);

            Helper.VoidAsync<Bill>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
