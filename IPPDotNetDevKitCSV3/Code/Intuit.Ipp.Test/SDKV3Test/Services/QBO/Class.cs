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
    public class ClassTest
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
        public void ClassAddTestUsingoAuth()
        {
            //Creating the Class for Add
            Class class1 = QBOHelper.CreateClass(qboContextoAuth);
            //Adding the Class
            Class added = Helper.Add<Class>(qboContextoAuth, class1);
            //Verify the added Class
            QBOHelper.VerifyClass(class1, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void ClassFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            ClassAddTestUsingoAuth();

            //Retrieving the Class using FindAll
            List<Class> classes = Helper.FindAll<Class>(qboContextoAuth, new Class(), 1, 500);
            Assert.IsNotNull(classes);
            Assert.IsTrue(classes.Count<Class>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void ClassFindbyIdTestUsingoAuth()
        {
            //Creating the Class for Adding
            Class class1 = QBOHelper.CreateClass(qboContextoAuth);
            //Adding the Class
            Class added = Helper.Add<Class>(qboContextoAuth, class1);
            Class found = Helper.FindById<Class>(qboContextoAuth, added);
            QBOHelper.VerifyClass(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void ClassUpdateTestUsingoAuth()
        {
            //Creating the Class for Adding
            Class class1 = QBOHelper.CreateClass(qboContextoAuth);
            //Adding the Class
            Class added = Helper.Add<Class>(qboContextoAuth, class1);
            //Change the data of added entity
            Class changed = QBOHelper.UpdateClass(qboContextoAuth, added);
            //Update the returned entity data
            Class updated = Helper.Update<Class>(qboContextoAuth, changed);//Verify the updated Class
            QBOHelper.VerifyClass(changed, updated);
        }

        [TestMethod]
        public void ClassSparseUpdateTestUsingoAuth()
        {
            //Creating the Class for Adding
            Class class1 = QBOHelper.CreateClass(qboContextoAuth);
            //Adding the Class
            Class added = Helper.Add<Class>(qboContextoAuth, class1);
            //Change the data of added entity
            Class changed = QBOHelper.SparseUpdateClass(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Class updated = Helper.Update<Class>(qboContextoAuth, changed);//Verify the updated Class
            QBOHelper.VerifyClassSparseUpdate(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod] [Ignore] //IgnoreReason: http://jira.intuit.com/browse/QBO-11387
        public void ClassDeleteTestUsingoAuth()
        {
            //Creating the Class for Adding
            Class class1 = QBOHelper.CreateClass(qboContextoAuth);
            //Adding the Class
            Class added = Helper.Add<Class>(qboContextoAuth, class1);
            //Delete the returned entity
            try
            {
                Class deleted = Helper.Delete<Class>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod] [Ignore]
        public void ClassVoidTestUsingoAuth()
        {
            //Creating the Class for Adding
            Class class1 = QBOHelper.CreateClass(qboContextoAuth);
            //Adding the Class
            Class added = Helper.Add<Class>(qboContextoAuth, class1);
            //Void the returned entity
            try
            {
                Class voided = Helper.Void<Class>(qboContextoAuth, added);
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
        public void ClassCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            ClassAddTestUsingoAuth();

            //Retrieving the Class using FindAll
            List<Class> classes = Helper.CDC(qboContextoAuth, new Class(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(classes);
            Assert.IsTrue(classes.Count<Class>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void ClassBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Class existing = Helper.FindOrAdd(qboContextoAuth, new Class());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateClass(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateClass(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Class");

           // batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Class>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Class).Id));
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
        public void ClassQueryUsingoAuth()
        {
            QueryService<Class> entityQuery = new QueryService<Class>(qboContextoAuth);
            Class existing = Helper.FindOrAdd<Class>(qboContextoAuth, new Class());
            List<Class> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Class where Id='" + existing.Id + "'").ToList<Class>();

            //List<Class> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void ClassAddAsyncTestsUsingoAuth()
        {
            //Creating the Class for Add
            Class entity = QBOHelper.CreateClass(qboContextoAuth);

            Class added = Helper.AddAsync<Class>(qboContextoAuth, entity);
            QBOHelper.VerifyClass(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void ClassRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            ClassAddTestUsingoAuth();

            //Retrieving the Class using FindAll
            Helper.FindAllAsync<Class>(qboContextoAuth, new Class());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void ClassFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Class for Adding
            Class entity = QBOHelper.CreateClass(qboContextoAuth);
            //Adding the Class
            Class added = Helper.Add<Class>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Class>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void ClassUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Class for Adding
            Class entity = QBOHelper.CreateClass(qboContextoAuth);
            //Adding the Class
            Class added = Helper.Add<Class>(qboContextoAuth, entity);

            //Update the Class
            Class updated = QBOHelper.UpdateClass(qboContextoAuth, added);
            //Call the service
            Class updatedReturned = Helper.UpdateAsync<Class>(qboContextoAuth, updated);
            //Verify updated Class
            QBOHelper.VerifyClass(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod] [Ignore] //IgnoreReason: http://jira.intuit.com/browse/QBO-11387
        public void ClassDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Class for Adding
            Class entity = QBOHelper.CreateClass(qboContextoAuth);
            //Adding the Class
            Class added = Helper.Add<Class>(qboContextoAuth, entity);

            Helper.DeleteAsync<Class>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void ClassVoidAsyncTestsUsingoAuth()
        {
            //Creating the Class for Adding
            Class entity = QBOHelper.CreateClass(qboContextoAuth);
            //Adding the Class
            Class added = Helper.Add<Class>(qboContextoAuth, entity);

            Helper.VoidAsync<Class>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
