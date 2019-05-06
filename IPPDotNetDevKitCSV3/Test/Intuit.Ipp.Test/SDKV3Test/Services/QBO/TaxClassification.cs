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
using System.Collections.ObjectModel;
using Intuit.Ipp.DataService;
using Intuit.Ipp.QueryFilter;


namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass]
    public class TaxClassification
    {
        ServiceContext qboContextoAuth = null;

        [TestInitialize]
        public void MyTestInitializer()
        {
            qboContextoAuth = Initializer.InitializeQBOServiceContextUsingoAuth();
            //qboContextoAuth.IppConfiguration.Logger.RequestLog.EnableRequestResponseLogging = true;
            //qboContextoAuth.IppConfiguration.Logger.RequestLog.ServiceRequestLoggingLocation = @"C:\IdsLogs";
        }

        #region Sync methods
        [TestMethod]
        public void TaxClassificationFindAllTestUsingOAuth()
        {
            try
            {
                List<Intuit.Ipp.Data.TaxClassification> taxClassifications = Helper.FindAll(qboContextoAuth, new Intuit.Ipp.Data.TaxClassification());
                Assert.IsNotNull(taxClassifications);
                Assert.IsTrue(taxClassifications.Count() > 0);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void TaxClassificationFindByLevelTestUsingOAuth()
        {
            List<Intuit.Ipp.Data.TaxClassification> taxClassList = Helper.FindAll(qboContextoAuth, new Intuit.Ipp.Data.TaxClassification());
            try
            {
                Intuit.Ipp.Data.TaxClassification taxClassification = new Intuit.Ipp.Data.TaxClassification
                {
                    Level = taxClassList[0].Level
                };
                List< Intuit.Ipp.Data.TaxClassification> taxClassifications = Helper.FindByLevel(qboContextoAuth, taxClassification);
                Assert.IsNotNull(taxClassifications);
                Assert.IsTrue(taxClassifications.Count() > 0);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void TaxClassificationFindByParentIdTestUsingOAuth()
        {
            List<Intuit.Ipp.Data.TaxClassification> taxClassList = Helper.FindAll(qboContextoAuth, new Intuit.Ipp.Data.TaxClassification());
            try
            {
                Intuit.Ipp.Data.TaxClassification taxClassification = new Intuit.Ipp.Data.TaxClassification
                {
                    ParentRef = taxClassList[0].ParentRef
                };
                List<Intuit.Ipp.Data.TaxClassification> taxClassifications = Helper.FindByParentId(qboContextoAuth, taxClassification);
                Assert.IsNotNull(taxClassifications);
                Assert.IsTrue(taxClassifications.Count() > 0);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
        #endregion

        #region Async Methods
        [TestMethod]
        public void TaxClassificationFindAllAsyncTestUsingOAuth()
        {
            Helper.FindAllAsync<Intuit.Ipp.Data.TaxClassification>(qboContextoAuth, new Intuit.Ipp.Data.TaxClassification());
        }

        [TestMethod]
        public void TaxClassificationFindByLevelAsyncTestUsingOAuth()
        {
            List<Intuit.Ipp.Data.TaxClassification> taxClassList = Helper.FindAll(qboContextoAuth, new Intuit.Ipp.Data.TaxClassification());
            Intuit.Ipp.Data.TaxClassification taxClassification = new Intuit.Ipp.Data.TaxClassification
            {
                Level = taxClassList[0].Level
            };
            Helper.FindByLevelAsync<Intuit.Ipp.Data.TaxClassification>(qboContextoAuth, taxClassification);
        }

        [TestMethod]
        public void TaxClassificationFindByParentIdAsyncTestUsingOAuth()
        {
            List<Intuit.Ipp.Data.TaxClassification> taxClassList = Helper.FindAll(qboContextoAuth, new Intuit.Ipp.Data.TaxClassification());
            Intuit.Ipp.Data.TaxClassification taxClassification = new Intuit.Ipp.Data.TaxClassification
            {
                ParentRef = taxClassList[0].ParentRef
            };
            Helper.FindByParentIdAsync<Intuit.Ipp.Data.TaxClassification>(qboContextoAuth, taxClassification);
        }
        #endregion
    }
}
