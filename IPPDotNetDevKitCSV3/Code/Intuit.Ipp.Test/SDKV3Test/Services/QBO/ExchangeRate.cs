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
    public class ExchangeRateTest
    {
        ServiceContext qboContextoAuth = null;
        [TestInitialize]
        public void MyTestInitializer()
        {
            qboContextoAuth = Initializer.InitializeQBOServiceContextUsingoAuth();
            //qboContextoAuth.IppConfiguration.Logger.RequestLog.EnableRequestResponseLogging = true;
            //qboContextoAuth.IppConfiguration.Logger.RequestLog.ServiceRequestLoggingLocation = @"C:\IdsLogs";
        }


        #region Test cases for FindbyId Operations

        [TestMethod]
        [Ignore]//Id is not supported for ExchangeRate
        public void ExchangeRateFindbyIdTestUsingoAuth()
        {
            var existing = Helper.FindAll<ExchangeRate>(qboContextoAuth, new ExchangeRate(), 1, 100).FirstOrDefault();

            QueryService<ExchangeRate> entityQuery = new QueryService<ExchangeRate>(qboContextoAuth);

            var found = entityQuery.ExecuteIdsQuery("Select * from ExchangeRate where Id='"+existing.Id+"'").FirstOrDefault();


            QBOHelper.VerifyExchangeRate(found, existing);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        [Ignore]//Fetch any other currency other than home currency else rate will not be updated
        public void ExchangeRateUpdateTestUsingoAuth()
        {
            var existing = Helper.FindAll<ExchangeRate>(qboContextoAuth, new ExchangeRate(), 1, 100).FirstOrDefault();


            //Change the data of added entity
            ExchangeRate changed = QBOHelper.UpdateExchangeRate(qboContextoAuth, existing);
            //Update the returned entity data
            ExchangeRate updated = Helper.Update<ExchangeRate>(qboContextoAuth, changed);//Verify the updated ExchangeRate
            QBOHelper.VerifyExchangeRate(changed, updated);
        }

        [TestMethod]
        [Ignore]//Fetch any other currency other than home currency else rate will not be updated
        public void ExchangeRateSparseUpdateTestUsingoAuth()
        {

            var existing = Helper.FindAll<ExchangeRate>(qboContextoAuth, new ExchangeRate(), 1, 100).FirstOrDefault();


            //Change the data of added entity
            ExchangeRate changed = QBOHelper.UpdateExchangeRateSparse(qboContextoAuth, existing.Id, existing.SyncToken);
            //Update the returned entity data
            ExchangeRate updated = Helper.Update<ExchangeRate>(qboContextoAuth, changed);//Verify the updated ExchangeRate
            QBOHelper.VerifyExchangeRateSparse(changed, updated);
        }
        #endregion

        #region Test cases for Query
        [TestMethod]
        public void ExchangeRateQueryUsingoAuth()
        {
            QueryService<ExchangeRate> entityQuery = new QueryService<ExchangeRate>(qboContextoAuth);
            var existing = Helper.FindAll<ExchangeRate>(qboContextoAuth, new ExchangeRate(), 1, 100);

            Assert.IsTrue(existing.Count() > 0);
        }

        #endregion
    }
}
