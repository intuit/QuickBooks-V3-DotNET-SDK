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
    public class CreditCardPaymentTest
    {
        ServiceContext qboContextoAuth;
        [TestInitialize]
        public void MyTestInitializer()
        {
            qboContextoAuth = Initializer.InitializeQBOServiceContextUsingoAuth();
        }


        #region TestCases for QBOContextOAuth

        #region Sync Methods


        [TestMethod]
        public void CreditCardPaymentAddTest()
        {
            //Creating the credit card Payment for Add
            CreditCardPaymentTxn creditCardPayment = QBOHelper.CreateCreditCardPayment(qboContextoAuth);
            //Adding the  credit card payment
            CreditCardPaymentTxn added = Helper.Add<CreditCardPaymentTxn>(qboContextoAuth, creditCardPayment);
            //Verify the added Payment
            QBOHelper.VerifyCreditCardPayment(creditCardPayment, added);


        }


        [TestMethod]
        public void CreditCardPaymentUpdateTest()
        {
            //Creating the credit card Payment for Add
            CreditCardPaymentTxn creditCardPayment = QBOHelper.CreateCreditCardPayment(qboContextoAuth);
            //Adding the  credit card payment
            CreditCardPaymentTxn added = Helper.Add<CreditCardPaymentTxn>(qboContextoAuth, creditCardPayment);
            //Verify the added Payment
            CreditCardPaymentTxn creditCardPaymentUpdated = QBOHelper.UpdateCreditCardPayment(qboContextoAuth, added);
            //updating the  credit card payment
            CreditCardPaymentTxn updated = Helper.Update<CreditCardPaymentTxn>(qboContextoAuth, creditCardPaymentUpdated);
            //Verify the added Payment
            QBOHelper.VerifyCreditCardPayment(creditCardPaymentUpdated, updated);
        }

        [TestMethod]
        public void CreditCardPaymentDeleteTest()
        {
            //Creating the credit card Payment for Add
            CreditCardPaymentTxn creditCardPayment = QBOHelper.CreateCreditCardPayment(qboContextoAuth);
            //Adding the  credit card payment
            CreditCardPaymentTxn added = Helper.Add<CreditCardPaymentTxn>(qboContextoAuth, creditCardPayment);
            //updating the  credit card payment
            CreditCardPaymentTxn deleted = Helper.Delete<CreditCardPaymentTxn>(qboContextoAuth, added);
            Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
        }

        [TestMethod]
        public void CreditCardPaymentFindByIdTest()
        {
            //Creating the Payment for Adding
            CreditCardPaymentTxn creditCardPaymentTxn = QBOHelper.CreateCreditCardPayment(qboContextoAuth);
            //Adding the Payment
            CreditCardPaymentTxn added = Helper.Add<CreditCardPaymentTxn>(qboContextoAuth, creditCardPaymentTxn);
            CreditCardPaymentTxn found = Helper.FindById<CreditCardPaymentTxn>(qboContextoAuth, added);
            QBOHelper.VerifyCreditCardPayment(found, added);
        }

        [TestMethod]
        public void CreditCardPaymentQueryTest()
        {
            QueryService<CreditCardPaymentTxn> entityQuery = new QueryService<CreditCardPaymentTxn>(qboContextoAuth);
            CreditCardPaymentTxn existing = Helper.FindOrAdd<CreditCardPaymentTxn>(qboContextoAuth, new CreditCardPaymentTxn());
            List<CreditCardPaymentTxn> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM CreditCardPayment where Id='" + existing.Id + "'").ToList<CreditCardPaymentTxn>();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion



        #region ASync Methods

        [TestMethod]
        public void CreditCardPaymentAddAsyncTest()
        {
            CreditCardPaymentTxn entity = QBOHelper.CreateCreditCardPayment(qboContextoAuth);
            CreditCardPaymentTxn added = Helper.AddAsync<CreditCardPaymentTxn>(qboContextoAuth, entity);
            QBOHelper.VerifyCreditCardPayment(entity, added);
        }

        [TestMethod]
        public void CreditCardPaymentUpdateAsyncTest()
        {
            CreditCardPaymentTxn entity = QBOHelper.CreateCreditCardPayment(qboContextoAuth);
            CreditCardPaymentTxn added = Helper.AddAsync<CreditCardPaymentTxn>(qboContextoAuth, entity);
            CreditCardPaymentTxn creditCardPaymentUpdated = QBOHelper.UpdateCreditCardPayment(qboContextoAuth, added);
            CreditCardPaymentTxn updated = Helper.UpdateAsync<CreditCardPaymentTxn>(qboContextoAuth, creditCardPaymentUpdated);
            QBOHelper.VerifyCreditCardPayment(creditCardPaymentUpdated, updated);
        }

        [TestMethod]
        public void CreditCardPaymentDeleteAsyncTest()
        {
            CreditCardPaymentTxn entity = QBOHelper.CreateCreditCardPayment(qboContextoAuth);
            CreditCardPaymentTxn added = Helper.AddAsync<CreditCardPaymentTxn>(qboContextoAuth, entity);
            CreditCardPaymentTxn deleted = Helper.DeleteAsync<CreditCardPaymentTxn>(qboContextoAuth, added);
            Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
        }

        [TestMethod]
        public void CreditCardPaymentFindByIdAsyncTest()
        {
            CreditCardPaymentTxn entity = QBOHelper.CreateCreditCardPayment(qboContextoAuth);
            CreditCardPaymentTxn added = Helper.AddAsync<CreditCardPaymentTxn>(qboContextoAuth, entity);
            CreditCardPaymentTxn found = Helper.FindByIdAsync<CreditCardPaymentTxn>(qboContextoAuth, added);
            QBOHelper.VerifyCreditCardPayment(found, added);
        }

        [TestMethod]
        public void CreditCardPaymentQueryAsyncTest()
        {
            List<CreditCardPaymentTxn> entities = Helper.FindAllAsync<CreditCardPaymentTxn>(qboContextoAuth, new CreditCardPaymentTxn());
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion
        #endregion
    }
}