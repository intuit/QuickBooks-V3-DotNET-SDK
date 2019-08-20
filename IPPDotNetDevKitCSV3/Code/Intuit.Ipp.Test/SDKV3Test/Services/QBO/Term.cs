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
    public class TermTest
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
        public void TermAddTestUsingoAuth()
        {
            //Creating the Bill for Add
            Term term = QBOHelper.CreateTerm(qboContextoAuth);
            //Adding the Term
            Term added = Helper.Add<Term>(qboContextoAuth, term);
            //Verify the added Term
            QBOHelper.VerifyTerm(term, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void TermFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            TermAddTestUsingoAuth();

            //Retrieving the Term using FindAll
            List<Term> terms = Helper.FindAll<Term>(qboContextoAuth, new Term(), 1, 500);
            Assert.IsNotNull(terms);
            Assert.IsTrue(terms.Count<Term>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void TermFindbyIdTestUsingoAuth()
        {
            //Creating the Term for Adding
            Term term = QBOHelper.CreateTerm(qboContextoAuth);
            //Adding the Term
            Term added = Helper.Add<Term>(qboContextoAuth, term);
            Term found = Helper.FindById<Term>(qboContextoAuth, added);
            QBOHelper.VerifyTerm(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void TermUpdateTestUsingoAuth()
        {
            //Creating the Term for Adding
            Term term = QBOHelper.CreateTerm(qboContextoAuth);
            //Adding the Term
            Term added = Helper.Add<Term>(qboContextoAuth, term);
            //Change the data of added entity
            Term changed = QBOHelper.UpdateTerm(qboContextoAuth, added);
            //Update the returned entity data
            Term updated = Helper.Update<Term>(qboContextoAuth, changed);//Verify the updated Term
            QBOHelper.VerifyTerm(changed, updated);
        }

        [TestMethod]
        public void TermSparseUpdateTestUsingoAuth()
        {
            //Creating the Term for Adding
            Term term = QBOHelper.CreateTerm(qboContextoAuth);
            //Adding the Term
            Term added = Helper.Add<Term>(qboContextoAuth, term);
            //Change the data of added entity
            Term changed = QBOHelper.SparseUpdateTerm(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Term updated = Helper.Update<Term>(qboContextoAuth, changed);//Verify the updated Term
            QBOHelper.VerifyTermSparseUpdate(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod] [Ignore]   //TestComment: Returns Operation Not Supported
        public void TermDeleteTestUsingoAuth()
        {
            //Creating the Term for Adding
            Term term = QBOHelper.CreateTerm(qboContextoAuth);
            //Adding the Term
            Term added = Helper.Add<Term>(qboContextoAuth, term);
            //Delete the returned entity
            try
            {
                Term deleted = Helper.Delete<Term>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }


        [TestMethod] [Ignore]
        public void TermVoidTestUsingoAuth()
        {
            //Creating the Term for Adding
            Term term = QBOHelper.CreateTerm(qboContextoAuth);
            //Adding the Term
            Term added = Helper.Add<Term>(qboContextoAuth, term);
            //Void the returned entity
            try
            {
                Term voided = Helper.Void<Term>(qboContextoAuth, added);
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
        public void TermCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            TermAddTestUsingoAuth();

            //Retrieving the Term using CDC
            List<Term> entities = Helper.CDC(qboContextoAuth, new Term(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<Term>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void TermBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Term existing = Helper.FindOrAdd(qboContextoAuth, new Term());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateTerm(qboContextoAuth));

            //batchEntries.Add(OperationEnum.update, QBOHelper.UpdateTerm(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Term");

          //  batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Term>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Term).Id));
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
        public void TermQueryUsingoAuth()
        {
            QueryService<Term> entityQuery = new QueryService<Term>(qboContextoAuth);
            Term existing = Helper.FindOrAdd<Term>(qboContextoAuth, new Term());
            //List<Term> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<Term> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Term where Id='" + existing.Id + "'").ToList<Term>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void TermAddAsyncTestsUsingoAuth()
        {
            //Creating the Term for Add
            Term entity = QBOHelper.CreateTerm(qboContextoAuth);

            Term added = Helper.AddAsync<Term>(qboContextoAuth, entity);
            QBOHelper.VerifyTerm(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void TermRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            TermAddTestUsingoAuth();

            //Retrieving the Term using FindAll
            Helper.FindAllAsync<Term>(qboContextoAuth, new Term());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void TermFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Term for Adding
            Term entity = QBOHelper.CreateTerm(qboContextoAuth);
            //Adding the Term
            Term added = Helper.Add<Term>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Term>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void TermUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Term for Adding
            Term entity = QBOHelper.CreateTerm(qboContextoAuth);
            //Adding the Term
            Term added = Helper.Add<Term>(qboContextoAuth, entity);

            //Update the Term
            Term updated = QBOHelper.UpdateTerm(qboContextoAuth, added);
            //Call the service
            Term updatedReturned = Helper.UpdateAsync<Term>(qboContextoAuth, updated);
            //Verify updated Term
            QBOHelper.VerifyTerm(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod] [Ignore] //TestComment: Returns Operation Not Supported
        public void TermDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Term for Adding
            Term entity = QBOHelper.CreateTerm(qboContextoAuth);
            //Adding the Term
            Term added = Helper.Add<Term>(qboContextoAuth, entity);

            Helper.DeleteAsync<Term>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void TermVoidAsyncTestsUsingoAuth()
        {
            //Creating the Term for Adding
            Term entity = QBOHelper.CreateTerm(qboContextoAuth);
            //Adding the Term
            Term added = Helper.Add<Term>(qboContextoAuth, entity);

            Helper.VoidAsync<Term>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
