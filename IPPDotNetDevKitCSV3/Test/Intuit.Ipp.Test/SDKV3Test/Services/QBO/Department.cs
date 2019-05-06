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
    public class DepartmentTest
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
        public void DepartmentAddTestUsingoAuth()
        {
            //Creating the Department for Add
            Department department = QBOHelper.CreateDepartment(qboContextoAuth);
            //Adding the Department
            Department added = Helper.Add<Department>(qboContextoAuth, department);
            //Verify the added Department
            QBOHelper.VerifyDepartment(department, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void DepartmentFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            DepartmentAddTestUsingoAuth();

            //Retrieving the Department using FindAll
            List<Department> departments = Helper.FindAll<Department>(qboContextoAuth, new Department(), 1, 500);
            Assert.IsNotNull(departments);
            Assert.IsTrue(departments.Count<Department>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void DepartmentFindbyIdTestUsingoAuth()
        {
            //Creating the Department for Adding
            Department department = QBOHelper.CreateDepartment(qboContextoAuth);
            //Adding the Department
            Department added = Helper.Add<Department>(qboContextoAuth, department);
            Department found = Helper.FindById<Department>(qboContextoAuth, added);
            QBOHelper.VerifyDepartment(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void DepartmentUpdateTestUsingoAuth()
        {
            //Creating the Department for Adding
            Department department = QBOHelper.CreateDepartment(qboContextoAuth);
            //Adding the Department
            Department added = Helper.Add<Department>(qboContextoAuth, department);
            //Change the data of added entity
            Department changed = QBOHelper.UpdateDepartment(qboContextoAuth, added);
            //Update the returned entity data
            Department updated = Helper.Update<Department>(qboContextoAuth, changed);//Verify the updated Department
            QBOHelper.VerifyDepartment(changed, updated);
        }

        [TestMethod]
        public void DepartmentSparseUpdateTestUsingoAuth()
        {
            //Creating the Department for Adding
            Department department = QBOHelper.CreateDepartment(qboContextoAuth);
            //Adding the Department
            Department added = Helper.Add<Department>(qboContextoAuth, department);
            //Change the data of added entity
            Department changed = QBOHelper.UpdateDepartmentSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Department updated = Helper.Update<Department>(qboContextoAuth, changed);//Verify the updated Department
            QBOHelper.VerifyDepartmentSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod] [Ignore] //IgnoreReason:  Reported by Srini as not supported, with QBO team
        public void DepartmentDeleteTestUsingoAuth()
        {
            //Creating the Department for Adding
            Department department = QBOHelper.CreateDepartment(qboContextoAuth);
            //Adding the Department
            Department added = Helper.Add<Department>(qboContextoAuth, department);
            //Delete the returned entity
            try
            {
                Department deleted = Helper.Delete<Department>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod][Ignore]
        public void DepartmentVoidTestUsingoAuth()
        {
            //Creating the Department for Adding
            Department department = QBOHelper.CreateDepartment(qboContextoAuth);
            //Adding the Department
            Department added = Helper.Add<Department>(qboContextoAuth, department);
            //Delete the returned entity
            try
            {
                Department voided = Helper.Void<Department>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Voided , voided.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }


        #endregion

        #region Test cases for CDC Operations

        [TestMethod]
        public void DepartmentCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            DepartmentAddTestUsingoAuth();

            //Retrieving the Department using CDC
            List<Department> entities = Helper.CDC(qboContextoAuth, new Department(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<Department>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void DepartmentBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Department existing = Helper.FindOrAdd(qboContextoAuth, new Department());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateDepartment(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateDepartment(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Department");

            //batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Department>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Department).Id));
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
        public void DepartmentQueryUsingoAuth()
        {
            QueryService<Department> entityQuery = new QueryService<Department>(qboContextoAuth);
            Department existing = Helper.FindOrAdd<Department>(qboContextoAuth, new Department());
            //List<Department> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<Department> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Department where Id='" + existing.Id + "'").ToList<Department>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void DepartmentAddAsyncTestsUsingoAuth()
        {
            //Creating the Department for Add
            Department entity = QBOHelper.CreateDepartment(qboContextoAuth);

            Department added = Helper.AddAsync<Department>(qboContextoAuth, entity);
            QBOHelper.VerifyDepartment(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void DepartmentRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            DepartmentAddTestUsingoAuth();

            //Retrieving the Department using FindAll
            Helper.FindAllAsync<Department>(qboContextoAuth, new Department());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod] [Ignore] //IgnoreReason: http://jira.intuit.com/browse/QBO-11382 (Tracker #184)
        public void DepartmentFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Department for Adding
            Department entity = QBOHelper.CreateDepartment(qboContextoAuth);
            //Adding the Department
            Department added = Helper.Add<Department>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Department>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void DepartmentUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Department for Adding
            Department entity = QBOHelper.CreateDepartment(qboContextoAuth);
            //Adding the Department
            Department added = Helper.Add<Department>(qboContextoAuth, entity);

            //Update the Department
            Department updated = QBOHelper.UpdateDepartment(qboContextoAuth, added);
            //Call the service
            Department updatedReturned = Helper.UpdateAsync<Department>(qboContextoAuth, updated);
            //Verify updated Department
            QBOHelper.VerifyDepartment(updated, updatedReturned);
        }

        [TestMethod]
        public void DepartmentSparseUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Department for Adding
            Department entity = QBOHelper.CreateDepartment(qboContextoAuth);
            //Adding the Department
            Department added = Helper.Add<Department>(qboContextoAuth, entity);

            //Update the Department
            Department updated = QBOHelper.UpdateDepartmentSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Call the service
            Department updatedReturned = Helper.UpdateAsync<Department>(qboContextoAuth, updated);
            //Verify updated Department
            QBOHelper.VerifyDepartmentSparse(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod] [Ignore] //IgnoreReason:  Reported by Srini as not supported, with QBO team
        public void DepartmentDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Department for Adding
            Department entity = QBOHelper.CreateDepartment(qboContextoAuth);
            //Adding the Department
            Department added = Helper.Add<Department>(qboContextoAuth, entity);

            Helper.DeleteAsync<Department>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void DepartmentVoidAsyncTestsUsingoAuth()
        {
            //Creating the Department for Adding
            Department entity = QBOHelper.CreateDepartment(qboContextoAuth);
            //Adding the Department
            Department added = Helper.Add<Department>(qboContextoAuth, entity);

            Helper.VoidAsync<Department>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
