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
    [TestClass] [Ignore]
    public class CompanyTest
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
        public void CompanyAddTestUsingoAuth()
        {
            //Creating the Company for Add
            Company company = QBOHelper.CreateCompany(qboContextoAuth);
            //Adding the Company
            Company added = Helper.Add<Company>(qboContextoAuth, company);
            //Verify the added Company
            QBOHelper.VerifyCompany(company, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void CompanyFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CompanyAddTestUsingoAuth();

            //Retrieving the Company using FindAll
            List<Company> companys = Helper.FindAll<Company>(qboContextoAuth, new Company(), 1, 500);
            Assert.IsNotNull(companys);
            Assert.IsTrue(companys.Count<Company>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void CompanyFindbyIdTestUsingoAuth()
        {
            //Creating the Company for Adding
            Company company = QBOHelper.CreateCompany(qboContextoAuth);
            //Adding the Company
            Company added = Helper.Add<Company>(qboContextoAuth, company);
            Company found = Helper.FindById<Company>(qboContextoAuth, added);
            QBOHelper.VerifyCompany(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void CompanyUpdateTestUsingoAuth()
        {
            //Creating the Company for Adding
            Company company = QBOHelper.CreateCompany(qboContextoAuth);
            //Adding the Company
            Company added = Helper.Add<Company>(qboContextoAuth, company);
            //Change the data of added entity
            Company changed = QBOHelper.UpdateCompany(qboContextoAuth, added);
            //Update the returned entity data
            Company updated = Helper.Update<Company>(qboContextoAuth, changed);//Verify the updated Company
            QBOHelper.VerifyCompany(changed, updated);
        }

        [TestMethod]
        public void CompanySparseUpdateTestUsingoAuth()
        {
            //Creating the Company for Adding
            Company company = QBOHelper.CreateCompany(qboContextoAuth);
            //Adding the Company
            Company added = Helper.Add<Company>(qboContextoAuth, company);
            //Change the data of added entity
            Company changed = QBOHelper.UpdateCompanySparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Company updated = Helper.Update<Company>(qboContextoAuth, changed);//Verify the updated Company
            QBOHelper.VerifyCompanySparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void CompanyDeleteTestUsingoAuth()
        {
            //Creating the Company for Adding
            Company company = QBOHelper.CreateCompany(qboContextoAuth);
            //Adding the Company
            Company added = Helper.Add<Company>(qboContextoAuth, company);
            //Delete the returned entity
            try
            {
                Company deleted = Helper.Delete<Company>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void CompanyVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            Company entity = QBOHelper.CreateCompany(qboContextoAuth);
            //Adding the entity
            Company added = Helper.Add<Company>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                Company voided = Helper.Void<Company>(qboContextoAuth, added);
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
        public void CompanyCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CompanyAddTestUsingoAuth();

            //Retrieving the Company using CDC
            List<Company> entities = Helper.CDC(qboContextoAuth, new Company(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<Company>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void CompanyBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Company existing = Helper.FindOrAdd(qboContextoAuth, new Company());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateCompany(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateCompany(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Company");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Company>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Company).Id));
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
        public void CompanyQueryUsingoAuth()
        {
            QueryService<Company> entityQuery = new QueryService<Company>(qboContextoAuth);
            Company existing = Helper.FindOrAdd<Company>(qboContextoAuth, new Company());
            // List<Company> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            int count = entityQuery.ExecuteIdsQuery("Select * from Company where Id='" + existing.Id + "'").Count;
                Assert.IsTrue(count > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void CompanyAddAsyncTestsUsingoAuth()
        {
            //Creating the Company for Add
            Company entity = QBOHelper.CreateCompany(qboContextoAuth);

            Company added = Helper.AddAsync<Company>(qboContextoAuth, entity);
            QBOHelper.VerifyCompany(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void CompanyRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CompanyAddTestUsingoAuth();

            //Retrieving the Company using FindAll
            Helper.FindAllAsync<Company>(qboContextoAuth, new Company());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void CompanyFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Company for Adding
            Company entity = QBOHelper.CreateCompany(qboContextoAuth);
            //Adding the Company
            Company added = Helper.Add<Company>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Company>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void CompanyUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Company for Adding
            Company entity = QBOHelper.CreateCompany(qboContextoAuth);
            //Adding the Company
            Company added = Helper.Add<Company>(qboContextoAuth, entity);

            //Update the Company
            Company updated = QBOHelper.UpdateCompany(qboContextoAuth, added);
            //Call the service
            Company updatedReturned = Helper.UpdateAsync<Company>(qboContextoAuth, updated);
            //Verify updated Company
            QBOHelper.VerifyCompany(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void CompanyDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Company for Adding
            Company entity = QBOHelper.CreateCompany(qboContextoAuth);
            //Adding the Company
            Company added = Helper.Add<Company>(qboContextoAuth, entity);

            Helper.DeleteAsync<Company>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void CompanyVoidAsyncTestsUsingoAuth()
        {
            //Creating the Company for Adding
            Company entity = QBOHelper.CreateCompany(qboContextoAuth);
            //Adding the Company
            Company added = Helper.Add<Company>(qboContextoAuth, entity);

            Helper.VoidAsync<Company>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
