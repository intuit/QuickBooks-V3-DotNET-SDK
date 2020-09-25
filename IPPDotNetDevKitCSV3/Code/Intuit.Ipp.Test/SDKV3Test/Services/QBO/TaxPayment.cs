using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;

namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass]
    public class TaxPaymentTest
    {
        ServiceContext qboContextoAuth;
        [TestInitialize]
        public void MyTestInitializer()
        {
            qboContextoAuth = Initializer.InitializeQBOServiceContextUsingoAuth();
        }


      #region Sync Methods


        [TestMethod]
        public void TaxPaymentFindByIdTest()
        {
            QueryService<TaxPayment> entityQuery = new QueryService<TaxPayment>(qboContextoAuth);
            List<TaxPayment> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM TaxPayment").ToList<TaxPayment>();
            if (entities.Count > 0)
            {
                TaxPayment added = entities[0];
                TaxPayment found = Helper.FindById<TaxPayment>(qboContextoAuth, added);
                QBOHelper.VerifyTaxPayment(found, added);
            }
        }

        [TestMethod]
        public void TaxPaymentQueryTest()
        {
            QueryService<TaxPayment> entityQuery = new QueryService<TaxPayment>(qboContextoAuth);
            List<TaxPayment> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM TaxPayment").ToList<TaxPayment>();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion



        #region ASync Methods

      
        [TestMethod]
        public void TaxPaymentFindByIdAsyncTest()
        {
            QueryService<TaxPayment> entityQuery = new QueryService<TaxPayment>(qboContextoAuth);
            List<TaxPayment> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM TaxPayment").ToList<TaxPayment>();
            if (entities.Count > 0)
            {
                TaxPayment added = entities[0];
                TaxPayment found = Helper.FindByIdAsync<TaxPayment>(qboContextoAuth, added);
                QBOHelper.VerifyTaxPayment(found, added);
            }
        }

        [TestMethod]
        public void TaxPaymentQueryAsyncTest()
        {
            List<TaxPayment> entities = Helper.FindAllAsync<TaxPayment>(qboContextoAuth, new TaxPayment());
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

    }
}