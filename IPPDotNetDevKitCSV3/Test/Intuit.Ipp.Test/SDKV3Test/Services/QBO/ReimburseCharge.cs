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
    public class ReimburseChargeTest
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


        #region Test cases for FindAll Operations

        [TestMethod]
        public void ReimburseChargeFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present


            //Retrieving the ReimburseCharge using FindAll
            List<ReimburseCharge> transfers = Helper.FindAll<ReimburseCharge>(qboContextoAuth, new ReimburseCharge(), 1, 500);
            Assert.IsNotNull(transfers);
            Assert.IsTrue(transfers.Count<ReimburseCharge>() > 0);
        }

        #endregion



        #region Test cases for Query
        [TestMethod]        
        public void ReimburseChargeQueryUsingoAuth()
        {
            QueryService<ReimburseCharge> entityQuery = new QueryService<ReimburseCharge>(qboContextoAuth);

            List<ReimburseCharge> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM ReimburseCharge").ToList();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion


        #endregion

    }
}
