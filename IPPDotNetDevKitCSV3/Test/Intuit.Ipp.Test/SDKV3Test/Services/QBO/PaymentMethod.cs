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
    public class PaymentMethodTest
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
        public void PaymentMethodAddTestUsingoAuth()
        {
            //Creating the PaymentMethod for Add
            PaymentMethod paymentMethod = QBOHelper.CreatePaymentMethod(qboContextoAuth);
            //Adding the PaymentMethod
            PaymentMethod added = Helper.Add<PaymentMethod>(qboContextoAuth, paymentMethod);
            //Verify the added PaymentMethod
            QBOHelper.VerifyPaymentMethod(paymentMethod, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void PaymentMethodFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            PaymentMethodAddTestUsingoAuth();

            //Retrieving the PaymentMethod using FindAll
            List<PaymentMethod> paymentMethods = Helper.FindAll<PaymentMethod>(qboContextoAuth, new PaymentMethod(), 1, 500);
            Assert.IsNotNull(paymentMethods);
            Assert.IsTrue(paymentMethods.Count<PaymentMethod>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void PaymentMethodFindbyIdTestUsingoAuth()
        {
            //Creating the PaymentMethod for Adding
            PaymentMethod paymentMethod = QBOHelper.CreatePaymentMethod(qboContextoAuth);
            //Adding the PaymentMethod
            PaymentMethod added = Helper.Add<PaymentMethod>(qboContextoAuth, paymentMethod);
            PaymentMethod found = Helper.FindById<PaymentMethod>(qboContextoAuth, added);
            QBOHelper.VerifyPaymentMethod(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void PaymentMethodUpdateTestUsingoAuth()
        {
            //Creating the PaymentMethod for Adding
            PaymentMethod paymentMethod = QBOHelper.CreatePaymentMethod(qboContextoAuth);
            //Adding the PaymentMethod
            PaymentMethod added = Helper.Add<PaymentMethod>(qboContextoAuth, paymentMethod);
            //Change the data of added entity
            PaymentMethod changed = QBOHelper.UpdatePaymentMethod(qboContextoAuth, added);
            //Update the returned entity data
            PaymentMethod updated = Helper.Update<PaymentMethod>(qboContextoAuth, changed); //Verify the updated PaymentMethod
            QBOHelper.VerifyPaymentMethod(changed, updated);
        }

        [TestMethod]
        public void PaymentMethodSparseUpdateTestUsingoAuth()
        {
            //Creating the PaymentMethod for Adding
            PaymentMethod paymentMethod = QBOHelper.CreatePaymentMethod(qboContextoAuth);
            //Adding the PaymentMethod
            PaymentMethod added = Helper.Add<PaymentMethod>(qboContextoAuth, paymentMethod);
            //Change the data of added entity
            PaymentMethod changed = QBOHelper.SparseUpdatePaymentMethod(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            PaymentMethod updated = Helper.Update<PaymentMethod>(qboContextoAuth, changed); //Verify the updated PaymentMethod
            QBOHelper.VerifyPaymentMethodSparseUpdate(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod] [Ignore]  //TestComment: Returns Operation Not Supported:  DevKit tracker Item #150
        public void PaymentMethodDeleteTestUsingoAuth()
        {
            //Creating the PaymentMethod for Adding
            PaymentMethod paymentMethod = QBOHelper.CreatePaymentMethod(qboContextoAuth);
            //Adding the PaymentMethod
            PaymentMethod added = Helper.Add<PaymentMethod>(qboContextoAuth, paymentMethod);
            //Delete the returned entity
            try
            {
                PaymentMethod deleted = Helper.Delete<PaymentMethod>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }

        }

        [TestMethod] [Ignore]
        public void PaymentMethodVoidTestUsingoAuth()
        {
            //Creating the PaymentMethod for Adding
            PaymentMethod paymentMethod = QBOHelper.CreatePaymentMethod(qboContextoAuth);
            //Adding the PaymentMethod
            PaymentMethod added = Helper.Add<PaymentMethod>(qboContextoAuth, paymentMethod);
            //Void the returned entity
            try
            {
                PaymentMethod voided = Helper.Void<PaymentMethod>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, voided.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }

        }

        #endregion

        #region Test cases for CDC Operations

        [TestMethod]
        public void PaymentMethodCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            PaymentMethodAddTestUsingoAuth();

            //Retrieving the PaymentMethod using CDC
            List<PaymentMethod> entities = Helper.CDC(qboContextoAuth, new PaymentMethod(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<PaymentMethod>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void PaymentMethodBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            PaymentMethod existing = Helper.FindOrAddPaymentMethod(qboContextoAuth, "CREDIT_CARD");

            batchEntries.Add(OperationEnum.create, QBOHelper.CreatePaymentMethod(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdatePaymentMethod(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from PaymentMethod");

          //  batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<PaymentMethod>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as PaymentMethod).Id));
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
        public void PaymentMethodQueryUsingoAuth()
        {
            QueryService<PaymentMethod> entityQuery = new QueryService<PaymentMethod>(qboContextoAuth);
            PaymentMethod existing = Helper.FindOrAdd<PaymentMethod>(qboContextoAuth, new PaymentMethod());
            List<PaymentMethod> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM PaymentMethod where Id='" + existing.Id + "'").ToList<PaymentMethod>();

            //List<PaymentMethod> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void PaymentMethodAddAsyncTestsUsingoAuth()
        {
            //Creating the PaymentMethod for Add
            PaymentMethod entity = QBOHelper.CreatePaymentMethod(qboContextoAuth);

            PaymentMethod added = Helper.AddAsync<PaymentMethod>(qboContextoAuth, entity);
            QBOHelper.VerifyPaymentMethod(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void PaymentMethodRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            PaymentMethodAddTestUsingoAuth();

            //Retrieving the PaymentMethod using FindAll
            Helper.FindAllAsync<PaymentMethod>(qboContextoAuth, new PaymentMethod());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void PaymentMethodFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the PaymentMethod for Adding
            PaymentMethod entity = QBOHelper.CreatePaymentMethod(qboContextoAuth);
            //Adding the PaymentMethod
            PaymentMethod added = Helper.Add<PaymentMethod>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<PaymentMethod>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void PaymentMethodUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the PaymentMethod for Adding
            PaymentMethod entity = QBOHelper.CreatePaymentMethod(qboContextoAuth);
            //Adding the PaymentMethod
            PaymentMethod added = Helper.Add<PaymentMethod>(qboContextoAuth, entity);

            //Update the PaymentMethod
            PaymentMethod updated = QBOHelper.UpdatePaymentMethod(qboContextoAuth, added);
            //Call the service
            PaymentMethod updatedReturned = Helper.UpdateAsync<PaymentMethod>(qboContextoAuth, updated);
            //Verify updated PaymentMethod
            QBOHelper.VerifyPaymentMethod(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod] [Ignore]  //TestComment: Returns Operation Not Supported:  DevKit tracker Item #150
        public void PaymentMethodDeleteAsyncTestsUsingoAuth()
        {
            //Creating the PaymentMethod for Adding
            PaymentMethod entity = QBOHelper.CreatePaymentMethod(qboContextoAuth);
            //Adding the PaymentMethod
            PaymentMethod added = Helper.Add<PaymentMethod>(qboContextoAuth, entity);

            Helper.DeleteAsync<PaymentMethod>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void PaymentMethodVoidAsyncTestsUsingoAuth()
        {
            //Creating the PaymentMethod for Adding
            PaymentMethod entity = QBOHelper.CreatePaymentMethod(qboContextoAuth);
            //Adding the PaymentMethod
            PaymentMethod added = Helper.Add<PaymentMethod>(qboContextoAuth, entity);

            Helper.VoidAsync<PaymentMethod>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
