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
    public class CompanyInfoTest
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

        [TestMethod] [Ignore]
        public void CompanyInfoAddTestUsingoAuth()
        {
            //Creating the Bill for Add
            CompanyInfo companyInfo = QBOHelper.CreateCompanyInfo(qboContextoAuth);
            //Adding the CompanyInfo
            CompanyInfo added = Helper.Add<CompanyInfo>(qboContextoAuth, companyInfo);
            //Verify the added CompanyInfo
            QBOHelper.VerifyCompanyInfo(companyInfo, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void CompanyInfoFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
           // CompanyInfoAddTestUsingoAuth();

            //Retrieving the Bill using FindAll
            List<CompanyInfo> companyInfos = Helper.FindAll<CompanyInfo>(qboContextoAuth, new CompanyInfo(), 1, 500);
            Assert.IsNotNull(companyInfos);
            Assert.IsTrue(companyInfos.Count<CompanyInfo>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void CompanyInfoFindbyIdTestUsingoAuth()
        {
            //Creating the CompanyInfo for Adding
           // CompanyInfo companyInfo = QBOHelper.CreateCompanyInfo(qboContextoAuth);
            //Adding the CompanyInfo
           // CompanyInfo added = Helper.Add<CompanyInfo>(qboContextoAuth, companyInfo);
            CompanyInfo added = new CompanyInfo();
            added.Id = "1";
            CompanyInfo found = Helper.FindById<CompanyInfo>(qboContextoAuth, added);
            Assert.AreEqual(added.Id, found.Id);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod][Ignore]
        public void CompanyInfoUpdateTestUsingoAuth()
        {
            ////Creating the CompanyInfo for Adding
            //CompanyInfo companyInfo = QBOHelper.CreateCompanyInfo(qboContextoAuth);
            ////Adding the CompanyInfo
            //CompanyInfo added = Helper.Add<CompanyInfo>(qboContextoAuth, companyInfo);
            
            List<CompanyInfo> found = Helper.FindAll<CompanyInfo>(qboContextoAuth, new CompanyInfo());

            //Change the data of added entity
            CompanyInfo changed = QBOHelper.UpdateCompanyInfo(qboContextoAuth, found[0]);

            //Update the returned entity data
            CompanyInfo updated = Helper.Update<CompanyInfo>(qboContextoAuth, changed);//Verify the updated CompanyInfo
            QBOHelper.VerifyCompanyInfo(changed, updated);
        }

        [TestMethod] [Ignore]
        public void CompanyInfoSparseUpdateTestUsingoAuth()
        {
            ////Creating the CompanyInfo for Adding
            //CompanyInfo companyInfo = QBOHelper.CreateCompanyInfo(qboContextoAuth);
            ////Adding the CompanyInfo
            //CompanyInfo added = Helper.Add<CompanyInfo>(qboContextoAuth, companyInfo);

            List<CompanyInfo> found = Helper.FindAll<CompanyInfo>(qboContextoAuth, new CompanyInfo());

            //Change the data of added entity
            CompanyInfo changed = QBOHelper.SparseUpdateCompanyInfo(qboContextoAuth, found[0].Id, found[0].SyncToken, found[0].CompanyName);
            //Update the returned entity data
            CompanyInfo updated = Helper.Update<CompanyInfo>(qboContextoAuth, changed);//Verify the updated CompanyInfo
            QBOHelper.VerifyCompanyInfoSparseUpdate(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod] [Ignore]
        public void CompanyInfoDeleteTestUsingoAuth()
        {
            //Creating the CompanyInfo for Adding
            CompanyInfo companyInfo = QBOHelper.CreateCompanyInfo(qboContextoAuth);
            //Adding the CompanyInfo
            CompanyInfo added = Helper.Add<CompanyInfo>(qboContextoAuth, companyInfo);
            //Delete the returned entity
            try
            {
                CompanyInfo deleted = Helper.Delete<CompanyInfo>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod] [Ignore] //IgnoreReason:  Not Supported
        public void CompanyInfoVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            CompanyInfo entity = QBOHelper.CreateCompanyInfo(qboContextoAuth);
            //Adding the entity
            CompanyInfo added = Helper.Add<CompanyInfo>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                CompanyInfo voided = Helper.Void<CompanyInfo>(qboContextoAuth, added);
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
        public void CompanyInfoCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            //CompanyInfoAddTestUsingoAuth();

            //Retrieving the Bill using FindAll
            List<CompanyInfo> companyInfos = Helper.CDC(qboContextoAuth, new CompanyInfo(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(companyInfos);
            Assert.IsTrue(companyInfos.Count<CompanyInfo>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void CompanyInfoBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            List<CompanyInfo> existing = Helper.FindAll<CompanyInfo>(qboContextoAuth, new CompanyInfo());



            Assert.IsNotNull(existing);
           

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateCompanyInfo(qboContextoAuth, existing[0]));

            batchEntries.Add(OperationEnum.query, "select * from CompanyInfo");

            
            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<CompanyInfo>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as CompanyInfo).Id));
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
        public void CompanyInfoQueryUsingoAuth()
        {
            QueryService<CompanyInfo> entityQuery = new QueryService<CompanyInfo>(qboContextoAuth);
            CompanyInfo existing = Helper.FindOrAdd<CompanyInfo>(qboContextoAuth, new CompanyInfo());
            //List<CompanyInfo> entities = entityQuery.Where(c => c.MetaData.CreateTime == existing.MetaData.CreateTime).ToList();
            List<CompanyInfo> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM CompanyInfo").ToList<CompanyInfo>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod] [Ignore]
        public void CompanyInfoAddAsyncTestsUsingoAuth()
        {
            //Creating the CompanyInfo for Add
            CompanyInfo entity = QBOHelper.CreateCompanyInfo(qboContextoAuth);

            CompanyInfo added = Helper.AddAsync<CompanyInfo>(qboContextoAuth, entity);
            QBOHelper.VerifyCompanyInfo(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void CompanyInfoRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            //CompanyInfoAddTestUsingoAuth();

            //Retrieving the CompanyInfo using FindAll
            Helper.FindAllAsync<CompanyInfo>(qboContextoAuth, new CompanyInfo());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void CompanyInfoFindByIdAsyncTestsUsingoAuth()
        {
            ////Creating the CompanyInfo for Adding
            //CompanyInfo entity = QBOHelper.CreateCompanyInfo(qboContextoAuth);
            ////Adding the CompanyInfo
            //CompanyInfo added = Helper.Add<CompanyInfo>(qboContextoAuth, entity);

            CompanyInfo added = new CompanyInfo();
            added.Id = "1";
            //FindById and verify
            Helper.FindByIdAsync<CompanyInfo>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void CompanyInfoUpdatedAsyncTestsUsingoAuth()
        {
            ////Creating the CompanyInfo for Adding
            //CompanyInfo companyInfo = QBOHelper.CreateCompanyInfo(qboContextoAuth);
            ////Adding the CompanyInfo
            //CompanyInfo added = Helper.Add<CompanyInfo>(qboContextoAuth, companyInfo);

            List<CompanyInfo> found = Helper.FindAll<CompanyInfo>(qboContextoAuth, new CompanyInfo());

            //Change the data of added entity
            CompanyInfo changed = QBOHelper.UpdateCompanyInfo(qboContextoAuth, found[0]);

            //Update the returned entity data
            CompanyInfo updated = Helper.UpdateAsync<CompanyInfo>(qboContextoAuth, changed);//Verify the updated CompanyInfo
            QBOHelper.VerifyCompanyInfo(changed, updated);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod] [Ignore]
        public void CompanyInfoDeleteAsyncTestsUsingoAuth()
        {
            //Creating the CompanyInfo for Adding
            CompanyInfo entity = QBOHelper.CreateCompanyInfo(qboContextoAuth);
            //Adding the CompanyInfo
            CompanyInfo added = Helper.Add<CompanyInfo>(qboContextoAuth, entity);

            Helper.DeleteAsync<CompanyInfo>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void CompanyInfoVoidAsyncTestsUsingoAuth()
        {
            //Creating the CompanyInfo for Adding
            CompanyInfo entity = QBOHelper.CreateCompanyInfo(qboContextoAuth);
            //Adding the CompanyInfo
            CompanyInfo added = Helper.Add<CompanyInfo>(qboContextoAuth, entity);

            Helper.VoidAsync<CompanyInfo>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
