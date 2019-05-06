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
 
using Intuit.Ipp.DataService;
using System.Collections.ObjectModel;
using Intuit.Ipp.QueryFilter;


namespace Intuit.Ipp.Test.Services.QBO
{
    [TestClass]
    public class CustomerTest
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
        public void CustomerAddTestUsingoAuth()
        {
            //Creating the Customer for Add
            Customer customer = QBOHelper.CreateCustomer(qboContextoAuth);
 
            //Adding the Customer
            Customer added = Helper.Add<Customer>(qboContextoAuth, customer);
            //Verify the added Customer
            QBOHelper.VerifyCustomer(customer, added);
        }

        [TestMethod][Ignore]
        public void CustomerAddTestsUsingoAuthInvalid()
        {
            try
            {

                //  IRequestValidator req = new OAuthRequestValidator("gjggdj", "hhh", "gjsgj", "mnv""access_token");
                OAuth2RequestValidator req = new OAuth2RequestValidator("xxxx");
                
                ServiceContext context = new ServiceContext("123146090856284", IntuitServicesType.QBO, req);

                //Creating the Customer for Add
                Customer customer = QBOHelper.CreateCustomer(context);

                Customer added = Helper.Add<Customer>(context, customer);
               
                Assert.Fail();
            }
            catch (InvalidTokenException ex)
            {
             
            }
          
        }

        #endregion

        #region Test cases for FindAll Operations

        [TestMethod]
        public void CustomerFindAllTestUsingoAuth()
        {
            //IRequestValidator req = new OAuth2RequestValidator("eyJlbmMiOiJBMTI4Q0JDLUhTMjU2IiwiYWxnIjoiZGlyIn0..OOFfh15AzfpEG48UctVT0Q.dQk--qs0FhjsIEtqGjIHvVUKAk8G-YkkqVMMsuK9Y8W4nbe1oy9cwByriTiC8bQJ4JdhR4ufc7bh2x7qmcIwsTmeDWIcLAwkqKe-d7uL4bV3DOjBB4UUXjftZbs-lf-vHMoRVa1pJrhBDRKDu58LYWbaTMXZnQ6JxVqIKFUNER-2sm-6roKFO52sY9p1_bQSdW08giLAt-z0pfUx4XT7tCWbPAKaKSaKQBBzqNCiZuyCmjzetAUDhq-CYMkG-znJujDk5JaUBUVmfuP-aCH21u3iXA81nSHCimbUfqjXmjBWkE9s0rLkOzWk3KtPUt6FeSjpZiqaTYOpuYPNNbmO1r9zDvobFjhZgTNIAKqFDhbkV0DeUgZaGiXHmRLUmLZbg956EyMfqMMP06E6hDvSlwJRyY28iE89qrZt5e8MBXy8mBrRAOiOI-L1fHIgzwu27fEu4QcbostguN6oySI2hssPdXI2hvXCTNKy74iZjyzzfQtycFjdQxmFBkoaqdU7qjJ6fjmURweGeKNiFqGUZT9_yEAibF-98ZZ8qiwQlFFY8rYZFWUva1qDCmtxiGdRxe7vn28FAWw906Bu3mwn9lxPjMyZdo-iD6RtY4tabVoYZR-24LjsoRqmMvBZreTp3uQ6xMALHBeE6qyK8-pjEldSI7Dkjbg-Q88GEa74sRM.PKBNr_m_A7swUlrepSOp2g");
            //ServiceContext context = new ServiceContext("123146090856284", IntuitServicesType.QBO, req);

            //Making sure that at least one entity is already present
            CustomerAddTestUsingoAuth();

            //Retrieving the Customer using FindAll
            List<Customer> customers = Helper.FindAll<Customer>(qboContextoAuth, new Customer(), 1, 500);
            Assert.IsNotNull(customers);
            Assert.IsTrue(customers.Count<Customer>() > 0);
        }

        //[TestMethod]
        //public void CustomerRetrieveTestsNoEntityAvailableUsingoAuth()
        //{
        //    bool moreAvailable = true;
        //    //Find and delete all Customer
        //    while (moreAvailable)
        //    {
        //        List<Customer> customerList = Helper.FindAll<Customer>(qboContextoAuth, new Customer(), 1, 500);
        //        if (customerList.Count > 0)
        //        {
        //            foreach (Customer customer in customerList)
        //            {
        //                Helper.Delete<Customer>(qboContextoAuth, customer);
        //            }
        //        }
        //        else
        //        {
        //            moreAvailable = false;
        //        }
        //    }
        //    try
        //    {
        //        Helper.FindAll<Customer>(qboContextoAuth, new Customer(), 1, 500);
        //        Assert.Fail();
        //    }
        //    catch (IdsException)
        //    {
        //    }
        //}

        [TestMethod]
        public void CustomerRetrieveAsyncTestsNullEntityUsingoAuth()
        {
            try
            {
                Helper.FindAll<Customer>(qboContextoAuth, null);
                Assert.Fail();
            }
            catch (IdsException)
            {

            }
        }

        #endregion

        #region Test cases for FindbyId Operations

        [TestMethod]
        public void CustomerFindbyIdTestUsingoAuth()
        {
            //Creating the Customer for Adding
            Customer customer = QBOHelper.CreateCustomer(qboContextoAuth);
            //Adding the Customer
            Customer added = Helper.Add<Customer>(qboContextoAuth, customer);
            Customer found = Helper.FindById<Customer>(qboContextoAuth, added);
            QBOHelper.VerifyCustomer(found, added);
        }

        //[TestMethod]
        //public void CustomerFindbyIdTestDeletedEntityUsingoAuth()
        //{
        //    //Creating the Customer for Adding
        //    Customer customer = QBOHelper.CreateCustomer(qboContextoAuth);
        //    //Adding the Customer
        //    Customer added = Helper.Add<Customer>(qboContextoAuth, customer);

        //    Helper.Delete<Customer>(qboContextoAuth, added);

        //    try
        //    {
        //        Customer found = Helper.FindById<Customer>(qboContextoAuth, added);
        //        Assert.Fail();
        //    }
        //    catch (IdsException)
        //    {

        //    }

        //}

        [TestMethod]
        public void CustomerFindByIdTestsNullEntityUsingoAuth()
        {
            try
            {
                Helper.FindById<Customer>(qboContextoAuth, null);
                Assert.Fail();
            }
            catch (IdsException)
            {

            }
        }

        #endregion

        #region Test cases for Update Operations

        [TestMethod] [Ignore]  //IgnoreReason:  CDC operations where Create is not supported removed for build
        public void CustomerUpdateTestUsingoAuth()
        {
            //Creating the Customer for Adding
            Customer customer = QBOHelper.CreateCustomer(qboContextoAuth);
            //Adding the Customer
            Customer added = Helper.Add<Customer>(qboContextoAuth, customer);
            //Change the data of added entity
            Customer updated = QBOHelper.UpdateCustomer(qboContextoAuth, added);
            //Update the returned entity data
            Customer updatedreturned = Helper.Update<Customer>(qboContextoAuth, updated);
            //Verify the updated Customer
            QBOHelper.VerifyCustomer(updated, updatedreturned);
        }

        
        [TestMethod]
        public void CustomerSparseUpdateTestUsingoAuth()
        {
            //Creating the Customer for Adding
            Customer customer = QBOHelper.CreateCustomer(qboContextoAuth);
            //Adding the Customer
            Customer added = Helper.Add<Customer>(qboContextoAuth, customer);
            //Change the data of added entity
            Customer updated = QBOHelper.SparseUpdateCustomer(qboContextoAuth, added.Id, added.SyncToken);
            //Update the returned entity data
            Customer updatedreturned = Helper.Update<Customer>(qboContextoAuth, updated);
            //Verify the updated Customer
            QBOHelper.VerifyCustomerSparseUpdate(updated, updatedreturned);
        }

        [TestMethod]
        public void CustomerUpdateTestsNullEntityUsingoAuth()
        {
            try
            {
                Customer updatedReturned = Helper.Update<Customer>(qboContextoAuth, null);
                Assert.Fail();
            }
            catch (IdsException)
            {
            }

        }

        #endregion

        #region  Test cases for Updateaccountontxns Operations

        [TestMethod]
        public void  CustomerUpdateAccountOnTxnsTestUsingoAuthFrance()
        {
            //Creating the Customer for Adding
            Customer customer = QBOHelper.CreateCustomerFrance(qboContextoAuth);
            //Adding the Customer
            Customer added = Helper.Add<Customer>(qboContextoAuth, customer);
            //Change the data of added entity
           // Customer updated = QBOHelper.UpdateCustomer(qboContextoAuth, added);
            Customer updated = QBOHelper.UpdateCustomerFrance(qboContextoAuth, added);
            //Update the returned entity data
            Customer updatedreturned = Helper.UpdateAccountOnTxnsFrance<Customer>(qboContextoAuth, updated);
            //Verify the updated Customer
            QBOHelper.VerifyCustomerFrance(updated, updatedreturned);
        }


        #endregion
        
        #region Test cases for Delete Operations

        [TestMethod] [Ignore] //TestComment: Returns Operation Not Supported:  DevKit tracker Item #150
        public void CustomerDeleteTestUsingoAuth()
        {
            //Creating the Customer for Adding
            Customer customer = QBOHelper.CreateCustomer(qboContextoAuth);
            //Adding the Customer
            Customer added = Helper.Add<Customer>(qboContextoAuth, customer);
            //Delete the returned entity
            try
            {
                Customer deleted = Helper.Delete<Customer>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Deleted, deleted.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod] [Ignore]
        public void CustomerVoidTestUsingoAuth()
        {
            //Creating the Customer for Adding
            Customer customer = QBOHelper.CreateCustomer(qboContextoAuth);
            //Adding the Customer
            Customer added = Helper.Add<Customer>(qboContextoAuth, customer);
            //Void the returned entity
            try
            {
                Customer voided = Helper.Void<Customer>(qboContextoAuth, added);
                Assert.AreEqual(EntityStatusEnum.Voided, voided.status);
            }
            catch (IdsException ex)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void CustomerDeleteTestsNullEntityUsingoAuth()
        {
            try
            {
                Helper.Delete<Customer>(qboContextoAuth, null);
            }
            catch (IdsException)
            {
            }
        }

        #endregion

        #region Test cases for CDC Operations

        [TestMethod]
        public void CustomerCDCTestUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CustomerAddTestUsingoAuth();

            //Retrieving the Customer using FindAll
            List<Customer> customers = Helper.CDC(qboContextoAuth, new Customer(), DateTime.Today.AddDays(-1));
            Assert.IsNotNull(customers);
            Assert.IsTrue(customers.Count<Customer>() > 0);
        }

        #endregion

        #region Test cases for Batch

        [TestMethod]
        public void CustomerBatchUsingoAuth()
        {
            Dictionary<OperationEnum, object> batchEntries = new Dictionary<OperationEnum, object>();

            Customer existing = Helper.FindOrAdd(qboContextoAuth, new Customer());

            batchEntries.Add(OperationEnum.create, QBOHelper.CreateCustomer(qboContextoAuth));

            batchEntries.Add(OperationEnum.update, QBOHelper.UpdateCustomer(qboContextoAuth, existing));

            batchEntries.Add(OperationEnum.query, "select * from Customer");

            //batchEntries.Add(OperationEnum.delete, existing);

            ReadOnlyCollection<IntuitBatchResponse> batchResponses = Helper.BatchTest<Customer>(qboContextoAuth, batchEntries);

            int position = 0;
            foreach (IntuitBatchResponse resp in batchResponses)
            {

                if (resp.ResponseType == ResponseType.Exception)
                {
                    Assert.Fail(resp.Exception.ToString());
                }

                if (resp.ResponseType == ResponseType.Entity)
                {
                    Assert.IsFalse(string.IsNullOrEmpty((resp.Entity as Customer).Id));
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
        public void CustomerQueryUsingoAuth()
        {
            QueryService<Customer> entityQuery = new QueryService<Customer>(qboContextoAuth);
            Customer existing = Helper.FindOrAdd<Customer>(qboContextoAuth, new Customer());
            //List<Customer> entities = entityQuery.Where(c => c.Id == existing.Id).ToList();
            List<Customer> entities = entityQuery.ExecuteIdsQuery("SELECT * FROM Customer where Id='" + existing.Id + "'").ToList<Customer>();

            Assert.IsTrue(entities.Count() > 0);
        }

        [TestMethod]
        public void CustomerQueryWithSpecialCharacterUsingoAuth()
        {
            QueryService<Customer> entityQuery = new QueryService<Customer>(qboContextoAuth);
            //List<Customer> entities = entityQuery.Where(c => c.DisplayName == "Customer\\'s Business").ToList();
            int count=entityQuery.ExecuteIdsQuery("Select * from Customer where DisplayName='Customer\\'s Business'").Count;
            Assert.IsTrue(count >= 0);
        }

        #endregion

        #endregion

        #region ASync Methods

        #region Test Cases for Add Operation

        [TestMethod]
        public void CustomerAddAsyncTestsUsingoAuth()
        {
            //Creating the Customer for Add
            Customer entity = QBOHelper.CreateCustomer(qboContextoAuth);

            Customer added = Helper.AddAsync<Customer>(qboContextoAuth, entity);
            QBOHelper.VerifyCustomer(entity, added);
        }

        #endregion

        #region Test Cases for FindAll Operation

        [TestMethod]
        public void CustomerRetrieveAsyncTestsUsingoAuth()
        {
            //Making sure that at least one entity is already present
            CustomerAddTestUsingoAuth();

            //Retrieving the Customer using FindAll
            Helper.FindAllAsync<Customer>(qboContextoAuth, new Customer());
        }

        #endregion

        #region Test Cases for FindById Operation

        [TestMethod]
        public void CustomerFindByIdAsyncTestsUsingoAuth()
        {
            //Creating the Customer for Adding
            Customer entity = QBOHelper.CreateCustomer(qboContextoAuth);
            //Adding the Customer
            Customer added = Helper.Add<Customer>(qboContextoAuth, entity);

            //FindById and verify
            Helper.FindByIdAsync<Customer>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Update Operation

        [TestMethod]
        public void CustomerUpdatedAsyncTestsUsingoAuth()
        {
            //Creating the Customer for Adding
            Customer entity = QBOHelper.CreateCustomer(qboContextoAuth);
            //Adding the Customer
            Customer added = Helper.Add<Customer>(qboContextoAuth, entity);

            //Update the Customer
            Customer updated = QBOHelper.UpdateCustomer(qboContextoAuth, added);
            //Call the service
            Customer updatedReturned = Helper.UpdateAsync<Customer>(qboContextoAuth, updated);
            //Verify updated Customer
            QBOHelper.VerifyCustomer(updated, updatedReturned);
        }

        #endregion
         
        #region  Test cases for UpdateAccountOnTxns Operations

        [TestMethod]
        public void CustomerUpdateAccountOnTxnsAsyncTestUsingoAuthFrance()
        {
            //Creating the Customer for Adding
            Customer customer = QBOHelper.CreateCustomerFrance(qboContextoAuth);
            //Adding the Customer
            Customer added = Helper.Add<Customer>(qboContextoAuth, customer);
            //Change the data of added entity
            // Customer updated = QBOHelper.UpdateCustomer(qboContextoAuth, added);
            Customer updated = QBOHelper.UpdateCustomerFrance(qboContextoAuth, added);
            //Update the returned entity data

            // Customer updatedreturned = Helper.updateaccountontxnsAsync<Customer>(qboContextoAuth, updated);
            Customer updatedreturned = Helper.UpdateAccountOnTxnsAsyncFrance<Customer>(qboContextoAuth, updated);
            //Verify the updated Customer
            QBOHelper.VerifyCustomerFrance(updated, updatedreturned);
        }


        #endregion
       
        #region Test Cases for Delete Operation

        [TestMethod] [Ignore] //TestComment: Returns Operation Not Supported:  DevKit tracker Item #150
        public void CustomerDeleteAsyncTestsUsingoAuth()
        {
            //Creating the Customer for Adding
            Customer entity = QBOHelper.CreateCustomer(qboContextoAuth);
            //Adding the Customer
            Customer added = Helper.Add<Customer>(qboContextoAuth, entity);

            Helper.DeleteAsync<Customer>(qboContextoAuth, added);
        }

        #endregion

        #region Test Cases for Void Operation

        [TestMethod] [Ignore]
        public void CustomerVoidAsyncTestsUsingoAuth()
        {
            //Creating the Customer for Adding
            Customer entity = QBOHelper.CreateCustomer(qboContextoAuth);
            //Adding the Customer
            Customer added = Helper.Add<Customer>(qboContextoAuth, entity);

            Helper.VoidAsync<Customer>(qboContextoAuth, added);
        }

        #endregion

        #endregion

        #endregion

    }
}
