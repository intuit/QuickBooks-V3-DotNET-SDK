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
    public class EmployeeTest
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
        public void EmployeeAddTestUsingoAuth()
        {
            //Creating the Employee for Add
            Employee employee = QBOHelper.CreateEmployee(qboContextoAuth);
            //Adding the Employee
            Employee added = Helper.Add<Employee>(qboContextoAuth, employee);
            //Verify the added Employee
            QBOHelper.VerifyEmployee(employee, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod] 
        public void EmployeeFindAllTestUsingoAuth()
        {
            Employee employee = Helper.FindOrAdd(qboContextoAuth, new Employee());

            //Retrieving the Employee using FindAll
            List<Employee> employees = Helper.FindAll<Employee>(qboContextoAuth, new Employee(), 1, 500);
            Assert.IsNotNull(employees);
            Assert.IsTrue(employees.Count<Employee>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod] 
        public void EmployeeFindbyIdTestUsingoAuth()
        {
            //Creating the Employee for Adding
            Employee employee = Helper.FindOrAdd(qboContextoAuth, new Employee());
            Employee found = Helper.FindById<Employee>(qboContextoAuth, employee);
            QBOHelper.VerifyEmployee(found, employee);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void EmployeeUpdateTestUsingoAuth()
        {
            Employee employee = Helper.FindOrAdd(qboContextoAuth, new Employee());
            //Change the data of added entity
            Employee changed = QBOHelper.UpdateEmployee(qboContextoAuth, employee);
            //Update the returned entity data
            Employee updated = Helper.Update<Employee>(qboContextoAuth, changed);//Verify the updated Employee
            QBOHelper.VerifyEmployee(changed, updated);
        }

        [TestMethod]
        public void EmployeeSparseUpdateTestUsingoAuth()
        {
            Employee employee = Helper.FindOrAdd(qboContextoAuth, new Employee());
            //Change the data of added entity
            Employee changed = QBOHelper.SparseUpdateEmployee(qboContextoAuth, employee.Id, employee.SyncToken);
            //Update the returned entity data
            Employee updated = Helper.Update<Employee>(qboContextoAuth, changed);//Verify the updated Employee
            QBOHelper.VerifyEmployeeSparseUpdate(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod] [Ignore]//Not supported
        public void EmployeeDeleteTestUsingoAuth()
        {
            //Creating the Employee for Adding
            Employee employee = QBOHelper.CreateEmployee(qboContextoAuth);
            //Adding the Employee
            Employee added = Helper.Add<Employee>(qboContextoAuth, employee);
            //Delete the returned entity
            try
            {
                Employee deleted = Helper.Delete<Employee>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod] [Ignore]
        public void EmployeeVoidTestUsingoAuth()
        {
            //Creating the Employee for Adding
            Employee employee = QBOHelper.CreateEmployee(qboContextoAuth);
            //Adding the Employee
            Employee added = Helper.Add<Employee>(qboContextoAuth, employee);
            //Void the returned entity
            try
            {
                Employee voided = Helper.Void<Employee>(qboContextoAuth, added);
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
        public void EmployeeCDCTestUsingoAuth()
        {
            Employee employee = Helper.FindOrAdd(qboContextoAuth, new Employee());

            //Retrieving the Employee using CDC
            List<Employee> entities = Helper.CDC(qboContextoAuth, new Employee(), DateTime.Now.AddDays(-100));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<Employee>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void EmployeeBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Employee existing = Helper.FindOrAdd(qboContextoAuth, new Employee());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateEmployee(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateEmployee(qboContextoAuth, existing));

            //batchEntries.Add(OperationEnum.query, "select * from Employee");

            //batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Employee>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Employee).Id));
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
        public void EmployeeQueryUsingoAuth()
        {
            QueryService<Employee> entityQuery = new QueryService<Employee>(qboContextoAuth);
            Employee existing = Helper.FindOrAdd<Employee>(qboContextoAuth, new Employee());

            List<Employee> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Employee where Id='" + existing.Id + "'").ToList<Employee>();

            Assert.IsTrue(entities.Count() > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void EmployeeAddAsyncTestsUsingoAuth()
        {
            //Creating the Employee for Add
            Employee entity = QBOHelper.CreateEmployee(qboContextoAuth);

            Employee added = Helper.AddAsync<Employee>(qboContextoAuth, entity);
            QBOHelper.VerifyEmployee(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod] 
        public void EmployeeRetrieveAsyncTestsUsingoAuth()
        {
            Employee employee = Helper.FindOrAdd(qboContextoAuth, new Employee());

            //Retrieving the Employee using FindAll
            Helper.FindAllAsync<Employee>(qboContextoAuth, new Employee());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod] 
        public void EmployeeFindByIdAsyncTestsUsingoAuth()
        {
            Employee employee = Helper.FindOrAdd(qboContextoAuth, new Employee());

            //FindById and verify
            Helper.FindByIdAsync<Employee>(qboContextoAuth, employee);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void EmployeeUpdatedAsyncTestsUsingoAuth()
        {
            Employee employee = Helper.FindOrAdd(qboContextoAuth, new Employee());

            //Update the Employee
            Employee updated = QBOHelper.UpdateEmployee(qboContextoAuth, employee);
            //Call the service
            Employee updatedReturned = Helper.UpdateAsync<Employee>(qboContextoAuth, updated);
            //Verify updated Employee
            QBOHelper.VerifyEmployee(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod] [Ignore] //Not supported
        public void EmployeeDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Employee for Adding
            Employee entity = QBOHelper.CreateEmployee(qboContextoAuth);
            //Adding the Employee
            Employee added = Helper.Add<Employee>(qboContextoAuth, entity);

            Helper.DeleteAsync<Employee>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void EmployeeVoidAsyncTestsUsingoAuth()
        {
            //Creating the Employee for Adding
            Employee entity = QBOHelper.CreateEmployee(qboContextoAuth);
            //Adding the Employee
            Employee added = Helper.Add<Employee>(qboContextoAuth, entity);

            Helper.VoidAsync<Employee>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
