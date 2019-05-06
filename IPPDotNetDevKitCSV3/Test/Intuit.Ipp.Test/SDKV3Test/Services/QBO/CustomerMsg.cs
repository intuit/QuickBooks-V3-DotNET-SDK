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
    public class CustomerMsgTest
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
        public void CustomerMsgAddTestUsingoAuth()
        {
            //Creating the CustomerMsg for Add
            CustomerMsg customerMsg = QBOHelper.CreateCustomerMsg(qboContextoAuth);
            //Adding the CustomerMsg
            CustomerMsg added = Helper.Add<CustomerMsg>(qboContextoAuth, customerMsg);
            //Verify the added CustomerMsg
            QBOHelper.VerifyCustomerMsg(customerMsg, added);
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void CustomerMsgFindAllTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CustomerMsgAddTestUsingoAuth();

            //Retrieving the CustomerMsg using FindAll
            List<CustomerMsg> customerMsgs = Helper.FindAll<CustomerMsg>(qboContextoAuth, new CustomerMsg(), 1, 500);
            Assert.IsNotNull(customerMsgs);
            Assert.IsTrue(customerMsgs.Count<CustomerMsg>() > 0);
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void CustomerMsgFindbyIdTestUsingoAuth()
        {
            //Creating the CustomerMsg for Adding
            CustomerMsg customerMsg = QBOHelper.CreateCustomerMsg(qboContextoAuth);
            //Adding the CustomerMsg
            CustomerMsg added = Helper.Add<CustomerMsg>(qboContextoAuth, customerMsg);
            CustomerMsg found = Helper.FindById<CustomerMsg>(qboContextoAuth, added);
            QBOHelper.VerifyCustomerMsg(found, added);
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod]
        public void CustomerMsgUpdateTestUsingoAuth()
        {
            //Creating the CustomerMsg for Adding
            CustomerMsg customerMsg = QBOHelper.CreateCustomerMsg(qboContextoAuth);
            //Adding the CustomerMsg
            CustomerMsg added = Helper.Add<CustomerMsg>(qboContextoAuth, customerMsg);
            //Change the data of added entity
            CustomerMsg changed = QBOHelper.UpdateCustomerMsg(qboContextoAuth, added);
            //Update the returned entity data
            CustomerMsg updated = Helper.Update<CustomerMsg>(qboContextoAuth, changed);//Verify the updated CustomerMsg
            QBOHelper.VerifyCustomerMsg(changed, updated);
        }

        [TestMethod]
        public void CustomerMsgSparseUpdateTestUsingoAuth()
        {
            //Creating the CustomerMsg for Adding
            CustomerMsg customerMsg = QBOHelper.CreateCustomerMsg(qboContextoAuth);
            //Adding the CustomerMsg
            CustomerMsg added = Helper.Add<CustomerMsg>(qboContextoAuth, customerMsg);
            //Change the data of added entity
            CustomerMsg changed = QBOHelper.UpdateCustomerMsgSparse(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            CustomerMsg updated = Helper.Update<CustomerMsg>(qboContextoAuth, changed);//Verify the updated CustomerMsg
            QBOHelper.VerifyCustomerMsgSparse(changed, updated);
        }
        
        #endregion

        #region Test cases for Delete Operations

        [TestMethod]
        public void CustomerMsgDeleteTestUsingoAuth()
        {
            //Creating the CustomerMsg for Adding
            CustomerMsg customerMsg = QBOHelper.CreateCustomerMsg(qboContextoAuth);
            //Adding the CustomerMsg
            CustomerMsg added = Helper.Add<CustomerMsg>(qboContextoAuth, customerMsg);
            //Delete the returned entity
            try
            {
                CustomerMsg deleted = Helper.Delete<CustomerMsg>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void CustomerMsgVoidTestUsingoAuth()
        {
            //Creating the entity for Adding
            CustomerMsg entity = QBOHelper.CreateCustomerMsg(qboContextoAuth);
            //Adding the entity
            CustomerMsg added = Helper.Add<CustomerMsg>(qboContextoAuth, entity);
            //Void the returned entity
            try
            {
                CustomerMsg voided = Helper.Void<CustomerMsg>(qboContextoAuth, added);
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
        public void CustomerMsgCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CustomerMsgAddTestUsingoAuth();

            //Retrieving the CustomerMsg using CDC
            List<CustomerMsg> entities = Helper.CDC(qboContextoAuth, new CustomerMsg(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(entities);
            Assert.IsTrue(entities.Count<CustomerMsg>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void CustomerMsgBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            CustomerMsg existing = Helper.FindOrAdd(qboContextoAuth, new CustomerMsg());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateCustomerMsg(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateCustomerMsg(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from CustomerMsg");

            batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<CustomerMsg>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as CustomerMsg).Id));
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
        public void CustomerMsgQueryUsingoAuth()
        {
            QueryService<CustomerMsg> entityQuery = new QueryService<CustomerMsg>(qboContextoAuth);
            CustomerMsg existing = Helper.FindOrAdd<CustomerMsg>(qboContextoAuth, new CustomerMsg());
            //<CustomerMsg> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            int count = entityQuery.ExecuteIdsQuery("Select * from CustomerMsg where Id='" + existing.Id + "'").Count;
            Assert.IsTrue(count > 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void CustomerMsgAddAsyncTestsUsingoAuth()
        {
            //Creating the CustomerMsg for Add
            CustomerMsg entity = QBOHelper.CreateCustomerMsg(qboContextoAuth);

            CustomerMsg added = Helper.AddAsync<CustomerMsg>(qboContextoAuth, entity);
            QBOHelper.VerifyCustomerMsg(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void CustomerMsgRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CustomerMsgAddTestUsingoAuth();

            //Retrieving the CustomerMsg using FindAll
            Helper.FindAllAsync<CustomerMsg>(qboContextoAuth, new CustomerMsg());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void CustomerMsgFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the CustomerMsg for Adding
            CustomerMsg entity = QBOHelper.CreateCustomerMsg(qboContextoAuth);
            //Adding the CustomerMsg
            CustomerMsg added = Helper.Add<CustomerMsg>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<CustomerMsg>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void CustomerMsgUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the CustomerMsg for Adding
            CustomerMsg entity = QBOHelper.CreateCustomerMsg(qboContextoAuth);
            //Adding the CustomerMsg
            CustomerMsg added = Helper.Add<CustomerMsg>(qboContextoAuth, entity);

            //Update the CustomerMsg
            CustomerMsg updated = QBOHelper.UpdateCustomerMsg(qboContextoAuth, added);
            //Call the service
            CustomerMsg updatedReturned = Helper.UpdateAsync<CustomerMsg>(qboContextoAuth, updated);
            //Verify updated CustomerMsg
            QBOHelper.VerifyCustomerMsg(updated, updatedReturned);
        }

        #endregion

        #region Test Cases for Delete Operation

        [TestMethod]
        public void CustomerMsgDeleteAsyncTestsUsingoAuth()
        {
            //Creating the CustomerMsg for Adding
            CustomerMsg entity = QBOHelper.CreateCustomerMsg(qboContextoAuth);
            //Adding the CustomerMsg
            CustomerMsg added = Helper.Add<CustomerMsg>(qboContextoAuth, entity);

            Helper.DeleteAsync<CustomerMsg>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod]
        public void CustomerMsgVoidAsyncTestsUsingoAuth()
        {
            //Creating the CustomerMsg for Adding
            CustomerMsg entity = QBOHelper.CreateCustomerMsg(qboContextoAuth);
            //Adding the CustomerMsg
            CustomerMsg added = Helper.Add<CustomerMsg>(qboContextoAuth, entity);

            Helper.VoidAsync<CustomerMsg>(qboContextoAuth, added);
        }

        #endregion

        #endregion
        #endregion

    }
}
