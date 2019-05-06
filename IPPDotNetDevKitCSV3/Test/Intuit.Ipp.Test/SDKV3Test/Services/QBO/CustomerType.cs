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
    public class CustomerTypeTest
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
        public void CustomerTypeAddTestUsingoAuth()
        {
            //Creating the CustomerType for Add
            CustomerType customerType = QBOHelper.CreateCustomerType(qboContextoAuth);
            //Adding the CustomerType
            CustomerType added = Helper.Add<CustomerType>(qboContextoAuth, customerType);
            //Verify the added CustomerType
            QBOHelper.VerifyCustomerType(customerType, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void CustomerTypeFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CustomerTypeAddTestUsingoAuth();

            //Retrieving the CustomerType using FindAll
            List<CustomerType> customerTypes = Helper.FindAll<CustomerType>(qboContextoAuth, new CustomerType(), 1, 500);
            Assert.IsNotNull(customerTypes);
            Assert.IsTrue(customerTypes.Count<CustomerType>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void CustomerTypeFindbyIdTestUsingoAuth()
        {
            //Creating the CustomerType for Adding
            CustomerType customerType = QBOHelper.CreateCustomerType(qboContextoAuth);
            //Adding the CustomerType
            CustomerType added = Helper.Add<CustomerType>(qboContextoAuth, customerType);
            CustomerType found = Helper.FindById<CustomerType>(qboContextoAuth, added);
            QBOHelper.VerifyCustomerType(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void CustomerTypeUpdateTestUsingoAuth()
        {
            //Creating the CustomerType for Adding
            CustomerType customerType = QBOHelper.CreateCustomerType(qboContextoAuth);
            //Adding the CustomerType
            CustomerType added = Helper.Add<CustomerType>(qboContextoAuth, customerType);
            //Change the data of added entity
            CustomerType changed = QBOHelper.UpdateCustomerType(qboContextoAuth, added);
            //Update the returned entity data
            CustomerType updated = Helper.Update<CustomerType>(qboContextoAuth, changed);//Verify the updated CustomerType
            QBOHelper.VerifyCustomerType(changed, updated);
        }

        [TestMethod]
        public void CustomerTypeSparseUpdateTestUsingoAuth()
        {
            //Creating the CustomerType for Adding
            CustomerType customerType = QBOHelper.CreateCustomerType(qboContextoAuth);
            //Adding the CustomerType
            CustomerType added = Helper.Add<CustomerType>(qboContextoAuth, customerType);
            //Change the data of added entity
            CustomerType changed = QBOHelper.UpdateCustomerTypeSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            CustomerType updated = Helper.Update<CustomerType>(qboContextoAuth, changed);//Verify the updated CustomerType
            QBOHelper.VerifyCustomerTypeSparse(changed, updated);
        }

        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void CustomerTypeDeleteTestUsingoAuth()
        {
            //Creating the CustomerType for Adding
            CustomerType customerType = QBOHelper.CreateCustomerType(qboContextoAuth);
            //Adding the CustomerType
            CustomerType added = Helper.Add<CustomerType>(qboContextoAuth, customerType);
            //Delete the returned entity
            try
            {
                CustomerType deleted = Helper.Delete<CustomerType>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void CustomerTypeVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            CustomerType entity = QBOHelper.CreateCustomerType(qboContextoAuth);
            //Adding the entity
            CustomerType added = Helper.Add<CustomerType>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                CustomerType voided = Helper.Void<CustomerType>(qboContextoAuth, added);
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
        public void CustomerTypeCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CustomerTypeAddTestUsingoAuth();

            //Retrieving the CustomerType using CDC
            List<CustomerType> entities = Helper.CDC(qboContextoAuth, new CustomerType(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<CustomerType>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void CustomerTypeBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            CustomerType existing = Helper.FindOrAdd(qboContextoAuth, new CustomerType());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateCustomerType(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateCustomerType(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from CustomerType");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<CustomerType>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as CustomerType).Id));
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
        public void CustomerTypeQueryUsingoAuth()
        {
            QueryService<CustomerType> entityQuery = new QueryService<CustomerType>(qboContextoAuth);
            CustomerType existing = Helper.FindOrAdd<CustomerType>(qboContextoAuth, new CustomerType());
            int count = entityQuery.ExecuteIdsQuery("Select * from CustomerType where Id='" + existing.Id + "'").Count;
            Assert.IsTrue(count > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void CustomerTypeAddAsyncTestsUsingoAuth()
        {
            //Creating the CustomerType for Add
            CustomerType entity = QBOHelper.CreateCustomerType(qboContextoAuth);

            CustomerType added = Helper.AddAsync<CustomerType>(qboContextoAuth, entity);
            QBOHelper.VerifyCustomerType(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void CustomerTypeRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CustomerTypeAddTestUsingoAuth();

            //Retrieving the CustomerType using FindAll
            Helper.FindAllAsync<CustomerType>(qboContextoAuth, new CustomerType());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void CustomerTypeFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the CustomerType for Adding
            CustomerType entity = QBOHelper.CreateCustomerType(qboContextoAuth);
            //Adding the CustomerType
            CustomerType added = Helper.Add<CustomerType>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<CustomerType>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void CustomerTypeUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the CustomerType for Adding
            CustomerType entity = QBOHelper.CreateCustomerType(qboContextoAuth);
            //Adding the CustomerType
            CustomerType added = Helper.Add<CustomerType>(qboContextoAuth, entity);

            //Update the CustomerType
            CustomerType updated = QBOHelper.UpdateCustomerType(qboContextoAuth, added);
            //Call the service
            CustomerType updatedReturned = Helper.UpdateAsync<CustomerType>(qboContextoAuth, updated);
            //Verify updated CustomerType
            QBOHelper.VerifyCustomerType(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void CustomerTypeDeleteAsyncTestsUsingoAuth()
        {
            //Creating the CustomerType for Adding
            CustomerType entity = QBOHelper.CreateCustomerType(qboContextoAuth);
            //Adding the CustomerType
            CustomerType added = Helper.Add<CustomerType>(qboContextoAuth, entity);

            Helper.DeleteAsync<CustomerType>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void CustomerTypeVoidAsyncTestsUsingoAuth()
        {
            //Creating the CustomerType for Adding
            CustomerType entity = QBOHelper.CreateCustomerType(qboContextoAuth);
            //Adding the CustomerType
            CustomerType added = Helper.Add<CustomerType>(qboContextoAuth, entity);

            Helper.VoidAsync<CustomerType>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
