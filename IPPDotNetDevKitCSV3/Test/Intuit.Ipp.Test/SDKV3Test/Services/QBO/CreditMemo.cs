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
    public class CreditMemoTest
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
        public void CreditMemoAddTestUsingoAuth()
        {
            //Creating the CreditMemo for Add
            CreditMemo creditMemo = QBOHelper.CreateCreditMemo(qboContextoAuth);
            //Adding the CreditMemo
            CreditMemo added = Helper.Add<CreditMemo>(qboContextoAuth, creditMemo);
            //Verify the added CreditMemo
            QBOHelper.VerifyCreditMemo(creditMemo, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void CreditMemoFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CreditMemoAddTestUsingoAuth();

            //Retrieving the CreditMemo using FindAll
            List<CreditMemo> creditMemos = Helper.FindAll<CreditMemo>(qboContextoAuth, new CreditMemo(), 1, 500);
            Assert.IsNotNull(creditMemos);
            Assert.IsTrue(creditMemos.Count<CreditMemo>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void CreditMemoFindbyIdTestUsingoAuth()
        {
            //Creating the CreditMemo for Adding
            CreditMemo creditMemo = QBOHelper.CreateCreditMemo(qboContextoAuth);
            //Adding the CreditMemo
            CreditMemo added = Helper.Add<CreditMemo>(qboContextoAuth, creditMemo);
            CreditMemo found = Helper.FindById<CreditMemo>(qboContextoAuth, added);
            QBOHelper.VerifyCreditMemo(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void CreditMemoUpdateTestUsingoAuth()
        {
            //Creating the CreditMemo for Adding
            CreditMemo creditMemo = QBOHelper.CreateCreditMemo(qboContextoAuth);
            //Adding the CreditMemo
            CreditMemo added = Helper.Add<CreditMemo>(qboContextoAuth, creditMemo);
            //Change the data of added entity
            CreditMemo changed = QBOHelper.UpdateCreditMemo(qboContextoAuth, added);
            //Update the returned entity data
            CreditMemo updated = Helper.Update<CreditMemo>(qboContextoAuth, changed);
            //Verify the updated CreditMemo
            QBOHelper.VerifyCreditMemo(changed, updated);
        }

        [TestMethod]
        public void CreditMemoSparseUpdateTestUsingoAuth()
        {
            //Creating the CreditMemo for Adding
            CreditMemo creditMemo = QBOHelper.CreateCreditMemo(qboContextoAuth);
            //Adding the CreditMemo
            CreditMemo added = Helper.Add<CreditMemo>(qboContextoAuth, creditMemo);
            //Change the data of added entity
            CreditMemo changed = QBOHelper.UpdateCreditMemoSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            CreditMemo updated = Helper.Update<CreditMemo>(qboContextoAuth, changed);
            //Verify the updated CreditMemo
            QBOHelper.VerifyCreditMemoSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void CreditMemoDeleteTestUsingoAuth()
        {
            //Creating the CreditMemo for Adding
            CreditMemo creditMemo = QBOHelper.CreateCreditMemo(qboContextoAuth);
            //Adding the CreditMemo
            CreditMemo added = Helper.Add<CreditMemo>(qboContextoAuth, creditMemo);
            //Delete the returned entity
            try
            {
                CreditMemo deleted = Helper.Delete<CreditMemo>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod][Ignore]
        public void CreditMemoVoidTestUsingoAuth()
        {
            //Creating the CreditMemo for Adding
            CreditMemo creditMemo = QBOHelper.CreateCreditMemo(qboContextoAuth);
            //Adding the CreditMemo
            CreditMemo added = Helper.Add<CreditMemo>(qboContextoAuth, creditMemo);
            //Delete the returned entity
            try
            {
                CreditMemo voided = Helper.Void<CreditMemo>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Voided , voided.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        #endregion

        #region Test cases for CDC Operations

        [TestMethod]
        public void CreditMemoCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CreditMemoAddTestUsingoAuth();

            //Retrieving the CreditMemo using CDC
            List<CreditMemo> entities = Helper.CDC(qboContextoAuth, new CreditMemo(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<CreditMemo>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void CreditMemoBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            CreditMemo existing = Helper.FindOrAdd(qboContextoAuth, new CreditMemo());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateCreditMemo(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateCreditMemo(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from CreditMemo");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<CreditMemo>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as CreditMemo).Id));
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
        public void CreditMemoQueryUsingoAuth()
        {
            QueryService<CreditMemo> entityQuery = new QueryService<CreditMemo>(qboContextoAuth);
            CreditMemo existing = Helper.FindOrAdd<CreditMemo>(qboContextoAuth, new CreditMemo());
            //List<CreditMemo> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<CreditMemo> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM CreditMemo where Id='" + existing.Id + "'").ToList<CreditMemo>();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void CreditMemoAddAsyncTestsUsingoAuth()
        {
            //Creating the CreditMemo for Add
            CreditMemo entity = QBOHelper.CreateCreditMemo(qboContextoAuth);

            CreditMemo added = Helper.AddAsync<CreditMemo>(qboContextoAuth, entity);
            QBOHelper.VerifyCreditMemo(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void CreditMemoRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CreditMemoAddTestUsingoAuth();

            //Retrieving the CreditMemo using FindAll
            Helper.FindAllAsync<CreditMemo>(qboContextoAuth, new CreditMemo());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void CreditMemoFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the CreditMemo for Adding
            CreditMemo entity = QBOHelper.CreateCreditMemo(qboContextoAuth);
            //Adding the CreditMemo
            CreditMemo added = Helper.Add<CreditMemo>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<CreditMemo>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void CreditMemoUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the CreditMemo for Adding
            CreditMemo entity = QBOHelper.CreateCreditMemo(qboContextoAuth);
            //Adding the CreditMemo
            CreditMemo added = Helper.Add<CreditMemo>(qboContextoAuth, entity);

            //Update the CreditMemo
            CreditMemo updated = QBOHelper.UpdateCreditMemo(qboContextoAuth, added);
            //Call the service
            CreditMemo updatedReturned = Helper.UpdateAsync<CreditMemo>(qboContextoAuth, updated);
            //Verify updated CreditMemo
            QBOHelper.VerifyCreditMemo(updated, updatedReturned);
        }

        [TestMethod]
        public void CreditMemoSparseUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the CreditMemo for Adding
            CreditMemo entity = QBOHelper.CreateCreditMemo(qboContextoAuth);
            //Adding the CreditMemo
            CreditMemo added = Helper.Add<CreditMemo>(qboContextoAuth, entity);

            //Update the CreditMemo
            CreditMemo updated = QBOHelper.UpdateCreditMemoSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Call the service
            CreditMemo updatedReturned = Helper.UpdateAsync<CreditMemo>(qboContextoAuth, updated);
            //Verify updated CreditMemo
            QBOHelper.VerifyCreditMemoSparse(updated, updatedReturned);
        }
        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void CreditMemoDeleteAsyncTestsUsingoAuth()
        {
            //Creating the CreditMemo for Adding
            CreditMemo entity = QBOHelper.CreateCreditMemo(qboContextoAuth);
            //Adding the CreditMemo
            CreditMemo added = Helper.Add<CreditMemo>(qboContextoAuth, entity);

            Helper.DeleteAsync<CreditMemo>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void CreditMemoVoidAsyncTestsUsingoAuth()
        {
            //Creating the CreditMemo for Adding
            CreditMemo entity = QBOHelper.CreateCreditMemo(qboContextoAuth);
            //Adding the CreditMemo
            CreditMemo added = Helper.Add<CreditMemo>(qboContextoAuth, entity);

            Helper.VoidAsync<CreditMemo>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
