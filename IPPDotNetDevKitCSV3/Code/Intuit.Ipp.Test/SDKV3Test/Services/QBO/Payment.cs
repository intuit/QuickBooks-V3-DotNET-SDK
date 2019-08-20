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
    public class PaymentTest
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
        public void PaymentAddTestUsingCheck()
        {
            //Creating the Payment for Add
            Payment payment = QBOHelper.CreatePaymentCheck(qboContextoAuth);
            //Adding the Payment
            Payment added = Helper.Add<Payment>(qboContextoAuth, payment);
            //Verify the added Payment
            QBOHelper.VerifyPayment(payment, added);
        }

        [TestMethod]
        public void PaymentAddTestUsingCreditCard()
        {
            //Creating the Payment for Add
            Payment payment = QBOHelper.CreatePaymentCreditCard(qboContextoAuth);
            //Adding the Payment
            Payment added = Helper.Add<Payment>(qboContextoAuth, payment);
            //Verify the added Payment
            QBOHelper.VerifyPayment(payment, added);
        }

        [TestMethod]
        public void PaymentAddTestUsingoAuth()
        {
            //Creating the Payment for Add
            Payment payment = QBOHelper.CreatePayment(qboContextoAuth);
            //Adding the Payment
            Payment added = Helper.Add<Payment>(qboContextoAuth, payment);
            //Verify the added Payment
            QBOHelper.VerifyPayment(payment, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void PaymentFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            PaymentAddTestUsingoAuth();

            //Retrieving the Payment using FindAll
            List<Payment> payments = Helper.FindAll<Payment>(qboContextoAuth, new Payment(), 1, 500);
            Assert.IsNotNull(payments);
            Assert.IsTrue(payments.Count<Payment>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void PaymentFindbyIdTestUsingoAuth()
        {
            //Creating the Payment for Adding
            Payment payment = QBOHelper.CreatePayment(qboContextoAuth);
            //Adding the Payment
            Payment added = Helper.Add<Payment>(qboContextoAuth, payment);
            Payment found = Helper.FindById<Payment>(qboContextoAuth, added);
            QBOHelper.VerifyPayment(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void PaymentUpdateTestUsingoAuth()
        {
            //Creating the Payment for Adding
            Payment payment = QBOHelper.CreatePayment(qboContextoAuth);
            //Adding the Payment
            Payment added = Helper.Add<Payment>(qboContextoAuth, payment);
            //Change the data of added entity
            Payment changed = QBOHelper.UpdatePayment(qboContextoAuth, added);
            //Update the returned entity data
            Payment updated = Helper.Update<Payment>(qboContextoAuth, changed);//Verify the updated Payment
            QBOHelper.VerifyPayment(changed, updated);
        }

        [TestMethod]
        public void PaymentSparseUpdateTestUsingoAuth()
        {
            //Creating the Payment for Adding
            Payment payment = QBOHelper.CreatePayment(qboContextoAuth);
            //Adding the Payment
            Payment added = Helper.Add<Payment>(qboContextoAuth, payment);
            //Change the data of added entity
            Payment changed = QBOHelper.SparseUpdatePayment(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Payment updated = Helper.Update<Payment>(qboContextoAuth, changed);//Verify the updated Payment
            QBOHelper.VerifyPaymentSparseUpdate(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void PaymentDeleteTestUsingoAuth()
        {
            //Creating the Payment for Adding
            Payment payment = QBOHelper.CreatePayment(qboContextoAuth);
            //Adding the Payment
            Payment added = Helper.Add<Payment>(qboContextoAuth, payment);
            //Delete the returned entity
            try
            {
                Payment deleted = Helper.Delete<Payment>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        [Ignore]
        public void PaymentVoidTestUsingoAuth()
        {
            //Creating the Payment for Adding
            Payment payment = QBOHelper.CreatePayment(qboContextoAuth);
            //Adding the Payment
            Payment added = Helper.Add<Payment>(qboContextoAuth, payment);
            try
            {
                Payment voided = Helper.Void<Payment>(qboContextoAuth, added);
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
        public void PaymentCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            PaymentAddTestUsingoAuth();

            //Retrieving the Payment using CDC
            List<Payment> entities = Helper.CDC(qboContextoAuth, new Payment(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<Payment>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void PaymentBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Payment existing = Helper.FindOrAdd(qboContextoAuth, new Payment());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreatePayment(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdatePayment(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Payment");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Payment>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Payment).Id));
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
        public void PaymentQueryUsingoAuth()
        {
            QueryService<Payment> entityQuery = new QueryService<Payment>(qboContextoAuth);
            Payment existing = Helper.FindOrAdd<Payment>(qboContextoAuth, new Payment());
            List<Payment> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Payment where Id='" + existing.Id + "'").ToList<Payment>();

            //List<Payment> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void PaymentAddAsyncTestsUsingoAuth()
        {
            //Creating the Payment for Add
            Payment entity = QBOHelper.CreatePayment(qboContextoAuth);

            Payment added = Helper.AddAsync<Payment>(qboContextoAuth, entity);
            QBOHelper.VerifyPayment(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void PaymentRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            PaymentAddTestUsingoAuth();

            //Retrieving the Payment using FindAll
            Helper.FindAllAsync<Payment>(qboContextoAuth, new Payment());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void PaymentFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Payment for Adding
            Payment entity = QBOHelper.CreatePayment(qboContextoAuth);
            //Adding the Payment
            Payment added = Helper.Add<Payment>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Payment>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void PaymentUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Payment for Adding
            Payment entity = QBOHelper.CreatePayment(qboContextoAuth);
            //Adding the Payment
            Payment added = Helper.Add<Payment>(qboContextoAuth, entity);

            //Update the Payment
            Payment updated = QBOHelper.UpdatePayment(qboContextoAuth, added);
            //Call the service
            Payment updatedReturned = Helper.UpdateAsync<Payment>(qboContextoAuth, updated);
            //Verify updated Payment
            QBOHelper.VerifyPayment(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void PaymentDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Payment for Adding
            Payment entity = QBOHelper.CreatePayment(qboContextoAuth);
            //Adding the Payment
            Payment added = Helper.Add<Payment>(qboContextoAuth, entity);

            Helper.DeleteAsync<Payment>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod][Ignore]
        public void PaymentVoidAsyncTestsUsingoAuth()
        {
            //Creating the Payment for Adding
            Payment entity = QBOHelper.CreatePayment(qboContextoAuth);
            //Adding the Payment
            Payment added = Helper.Add<Payment>(qboContextoAuth, entity);

            Helper.VoidAsync<Payment>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
