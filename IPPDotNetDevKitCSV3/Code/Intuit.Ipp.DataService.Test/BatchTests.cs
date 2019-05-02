using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Intuit.Ipp.DataService.Test
{
    /// <summary>
    /// Summary description for BatchTests.
    /// </summary>

    [TestClass]
    public class BatchTests
    {

        [TestMethod()]
        public void BatchTest()
        {
            ServiceContext context = Initializer.InitializeServiceContextQbo();
            DataService service = new DataService(context);
            context.IppConfiguration.Message.Response.CompressionFormat = Intuit.Ipp.Core.Configuration.CompressionFormat.GZip;
            context.IppConfiguration.Message.Response.SerializationFormat = Intuit.Ipp.Core.Configuration.SerializationFormat.Json;
            Customer customer = new Customer();
            string guid = Guid.NewGuid().ToString("N");
            customer.GivenName = guid.Substring(0, 25);
            customer.Title = guid.Substring(0, 15);
            customer.MiddleName = guid.Substring(0, 5);
            customer.FamilyName = guid.Substring(0, 25);
            customer.DisplayName = guid.Substring(0, 20);

            try
            {
                Batch batch = service.CreateNewBatch();
                batch.Add(customer, "addCustomer", OperationEnum.create);
                batch.Add(new CDCQuery() { ChangedSince = DateTime.Now.AddDays(-1), ChangedSinceSpecified = true, Entities = "Customer" }, "cdcOpration");
                batch.Execute();
                IntuitBatchResponse addCustomerResponse = batch.IntuitBatchItemResponses[0];
                if (addCustomerResponse.ResponseType != ResponseType.Exception)
                {
                    Customer addedcustomer = addCustomerResponse.Entity as Customer;
                    Assert.IsNotNull(addedcustomer);
                    Assert.IsFalse(string.IsNullOrEmpty(addedcustomer.Id));
                }
                IntuitBatchResponse CDCQueryResponse = batch.IntuitBatchItemResponses[1];
                if (CDCQueryResponse.ResponseType != ResponseType.Exception)
                {
                    Dictionary<string, List<IEntity>> cdcCustomers = CDCQueryResponse.CDCResponse.entities;
                    Assert.IsNotNull(cdcCustomers);
                    Assert.IsTrue(cdcCustomers.Count > 0);
                    foreach (KeyValuePair<string, List<IEntity>> entry in cdcCustomers)
                    {
                        Assert.IsTrue(entry.Value.ElementAt(0).GetType() == new Customer().GetType());
                    }
                }
                else
                {
                    Assert.Fail();
                }
            }
            catch (Ipp.Exception.IdsException ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void batchtestall()
        {
            ServiceContext context = Initializer.InitializeServiceContextQbo();
            DataService service = new DataService(context);

            Customer customer1 = new Customer();
            string guid = Guid.NewGuid().ToString("n");
            customer1.GivenName = guid.Substring(0, 25);
            string GivenNameCustomer1 = customer1.GivenName;
            customer1.Title = guid.Substring(0, 15);
            customer1.MiddleName = guid.Substring(0, 5);
            customer1.FamilyName = guid.Substring(0, 25);
            customer1.DisplayName = guid.Substring(0, 20);

            Customer customer2 = new Customer();
            guid = Guid.NewGuid().ToString("n");
            customer2.GivenName = guid.Substring(0, 25);
            string GivenNameCustomer2 = customer2.GivenName;
            customer2.Title = guid.Substring(0, 15);
            customer2.MiddleName = guid.Substring(0, 5);
            customer2.FamilyName = guid.Substring(0, 25);
            customer2.DisplayName = guid.Substring(0, 20);


            try
            {
                Batch batch = service.CreateNewBatch();
                batch.Add(customer1, "addcustomer1", OperationEnum.create);

                batch.Add(customer2, "addcustomer2", OperationEnum.create);

                batch.Add("select * from Customer startPosition 0 maxResults 10", "queryCustomer");
                batch.Execute();

                IntuitBatchResponse inuititemresponse = batch.IntuitBatchItemResponses[0];
                Assert.IsTrue(inuititemresponse.ResponseType == ResponseType.Entity);
                Customer resultcustomer1 = inuititemresponse.Entity as Customer;
                Assert.IsFalse(string.IsNullOrEmpty(resultcustomer1.Id));
                Assert.IsTrue(resultcustomer1.GivenName == GivenNameCustomer1);

                inuititemresponse = batch.IntuitBatchItemResponses[1];
                Assert.IsTrue(inuititemresponse.ResponseType == ResponseType.Entity);
                Customer resultcustomer2 = inuititemresponse.Entity as Customer;
                Assert.IsFalse(string.IsNullOrEmpty(resultcustomer2.Id));
                Assert.IsTrue(resultcustomer2.GivenName == GivenNameCustomer2);

                inuititemresponse = batch.IntuitBatchItemResponses[2];
                Assert.IsTrue(inuititemresponse.ResponseType == ResponseType.Query);
                List<Customer> customers = inuititemresponse.Entities.ToList().ConvertAll(item => item as Customer);
                Assert.IsTrue(customers.Count >= 2);
            }
            catch (Ipp.Exception.IdsException ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void BatchTestAsync()
        {
            ServiceContext context = Initializer.InitializeServiceContextQbo();
            DataService service = new DataService(context);
            Customer customer = new Customer();
            string guid = Guid.NewGuid().ToString("N");
            customer.GivenName = guid.Substring(0, 25);
            customer.Title = guid.Substring(0, 15);
            customer.MiddleName = guid.Substring(0, 5);
            customer.FamilyName = guid.Substring(0, 25);
            customer.DisplayName = guid.Substring(0, 20);

            try
            {
                ManualResetEvent manualEvent = new ManualResetEvent(false);
                Batch batch = service.CreateNewBatch();
                batch.Add(customer, "addCustomer", OperationEnum.create);
                batch.Add("select * from Customer startPosition 0 maxResults 10", "queryCustomer");
                batch.OnBatchExecuteAsyncCompleted += (sender, e) =>
                {
                    Assert.IsNotNull(e);
                    if (e.Batch != null)
                    {
                        IntuitBatchResponse addCustomerResponse = e.Batch.IntuitBatchItemResponses[0];
                        if (addCustomerResponse.ResponseType != ResponseType.Exception)
                        {
                            Customer addedcustomer = addCustomerResponse.Entity as Customer;
                            Assert.IsNotNull(customer);
                            Assert.IsFalse(string.IsNullOrEmpty(addedcustomer.Id));
                        }
                        else
                        {
                            Assert.Fail();
                        }

                        IntuitBatchResponse queryCustomerResponse = e.Batch.IntuitBatchItemResponses[1];
                        if (queryCustomerResponse.ResponseType != ResponseType.Exception)
                        {
                            List<Customer> customers = queryCustomerResponse.Entities.ToList().ConvertAll(item => item as Customer);
                            Assert.IsNotNull(customers);
                            Assert.IsTrue(customers.Count > 1);
                        }
                        else
                        {
                            Assert.Fail();
                        }
                    }
                    else
                    {
                        Assert.Fail();
                    }

                    manualEvent.Set();
                };

                batch.ExecuteAsync();
                manualEvent.WaitOne(30000);
            }
            catch (Ipp.Exception.IdsException ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Intuit.Ipp.Exception.IdsException))]
        public void UseSameBatchIDTwice()
        {
            ServiceContext context = Initializer.InitializeServiceContextQbo();
            DataService service = new DataService(context);
            Customer customer = new Customer();
            string guid = Guid.NewGuid().ToString("N");
            customer.GivenName = guid.Substring(0, 25);
            customer.Title = guid.Substring(0, 15);
            customer.MiddleName = guid.Substring(0, 5);
            customer.FamilyName = guid.Substring(0, 25);
            customer.DisplayName = guid.Substring(0, 20);
            Batch batch = service.CreateNewBatch();
            batch.Add(customer, "Customer", OperationEnum.create);
            batch.Add("query * from Customer", "Customer");
        }

        [TestMethod]
        [ExpectedException(typeof(Intuit.Ipp.Exception.IdsException))]
        public void AddNullEntityInBatch()
        {
            ServiceContext context = Initializer.InitializeServiceContextQbo();
            DataService service = new DataService(context);
            Customer customer = null;
            Batch batch = service.CreateNewBatch();
            batch.Add(customer, "Customer", OperationEnum.create);
            batch.Add("query * from Customer", "Customer");
        }

        [TestMethod]
        [ExpectedException(typeof(Intuit.Ipp.Exception.IdsException))]
        public void AddMoreThanTwentyFiveItemsInBatch()
        {
            ServiceContext context = Initializer.InitializeServiceContextQbo();
            DataService service = new DataService(context);
            Batch batch = service.CreateNewBatch();
            for (int i = 0; i <= 26; i++)
            {
                Customer customer = new Customer();
                string guid = Guid.NewGuid().ToString("N");
                customer.GivenName = guid.Substring(0, 25);
                customer.Title = guid.Substring(0, 15);
                customer.MiddleName = guid.Substring(0, 5);
                customer.FamilyName = guid.Substring(0, 25);
                customer.DisplayName = guid.Substring(0, 20);
                batch.Add(customer, "Customer" + i.ToString(), OperationEnum.create);
            }
        }

        #region Add IDS Query Tests

        [TestMethod]
        [ExpectedException(typeof(Intuit.Ipp.Exception.IdsException))]
        public void AddEmptyQueryToBatch()
        {
            Batch batch = GetBatch();
            string query = string.Empty;
            batch.Add(query, "emptyQuery");
            batch.Add("query * from Customer", "customerQuery");
        }

        [TestMethod]
        [ExpectedException(typeof(Intuit.Ipp.Exception.IdsException))]
        public void AddEmptyIdToBatch()
        {
            Batch batch = GetBatch();
            string queryId = string.Empty;
            batch.Add("query * from Invoice", queryId);
            batch.Add("query * from Customer", "customerQuery");
        }

        [TestMethod]
        [ExpectedException(typeof(Intuit.Ipp.Exception.IdsException))]
        public void AddTwentyFivePlusToBatch()
        {
            Batch batch = GetBatch();
            string queryId = string.Empty;
            for (int i = 0; i <= 26; i++)
            {
                batch.Add("query * from Customer", "customerQuery");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Intuit.Ipp.Exception.IdsException))]
        public void AddDuplicateItemToBatch()
        {
            Batch batch = GetBatch();
            string queryId = string.Empty;
            batch.Add("query * from Invoice", "customerQuery");
            batch.Add("query * from Customer", "customerQuery");
        }

        [TestMethod]
        [ExpectedException(typeof(Intuit.Ipp.Exception.IdsException))]
        public void AddNullIdToBatch()
        {
            Batch batch = GetBatch();
            string queryId = string.Empty;
            batch.Add("query * from Invoice", null);
        }

        #endregion

        #region Add Entity Item Tests

        [TestMethod]
        [ExpectedException(typeof(Intuit.Ipp.Exception.IdsException))]
        public void AddEmptyQueryToBatchEntity()
        {
            Batch batch = GetBatch();
            Customer customer = null;
            batch.Add(customer, "emptyCustomer", OperationEnum.create);

        }

        [TestMethod]
        [ExpectedException(typeof(Intuit.Ipp.Exception.IdsException))]
        public void AddEmptyIdToBatchEntity()
        {
            Batch batch = GetBatch();
            string queryId = string.Empty;
            Customer customer = GetCustomer();
            batch.Add(customer, queryId, OperationEnum.create);

        }

        [TestMethod]
        [ExpectedException(typeof(Intuit.Ipp.Exception.IdsException))]
        public void AddTwentyFivePlusToBatchEntity()
        {
            Batch batch = GetBatch();
            string queryId = string.Empty;

            for (int i = 0; i <= 26; i++)
            {
                Customer customer = GetCustomer();
                batch.Add(customer, "Customer", OperationEnum.create);
            }

        }

        [TestMethod]
        [ExpectedException(typeof(Intuit.Ipp.Exception.IdsException))]
        public void AddDuplicateItemToBatchEntity()
        {
            Batch batch = GetBatch();
            Customer customer1 = GetCustomer();
            Customer customer2 = GetCustomer();
            batch.Add(customer1, "customerQuery", OperationEnum.create);
            batch.Add(customer2, "customerQuery", OperationEnum.create);
        }

        [TestMethod]
        [ExpectedException(typeof(Intuit.Ipp.Exception.IdsException))]
        public void AddNullIdToBatchEntity()
        {
            Batch batch = GetBatch();
            Customer customer = GetCustomer();
            batch.Add(customer, null, OperationEnum.create);
        }

        #endregion

        #region Remove Tests
        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void RemoveItem()
        {
            Batch batch = GetBatch();
            for (int i = 0; i <= 10; i++)
            {
                Customer customer = GetCustomer();
                batch.Add(customer, "Customer" + i.ToString(), OperationEnum.create);
            }
            batch.Remove("Customer0");
            IntuitBatchResponse item = batch["Customer0"] as IntuitBatchResponse;
        }

        [TestMethod]
        [ExpectedException(typeof(Intuit.Ipp.Exception.IdsException))]
        public void RemoveWrongItem()
        {
            Batch batch = GetBatch();
            for (int i = 0; i <= 10; i++)
            {
                Customer customer = GetCustomer();
                batch.Add(customer, "Customer" + i.ToString(), OperationEnum.create);
            }
            batch.Remove("Customer20");
        }

        [TestMethod]
        public void RemoveCorrectItem()
        {
            Batch batch = GetBatch();
            for (int i = 0; i <= 10; i++)
            {
                Customer customer = GetCustomer();
                batch.Add(customer, "Customer" + i.ToString(), OperationEnum.create);
            }
            batch.Remove("Customer0");

            if (batch.Count != 10)
                Assert.Fail();
        }

        [TestMethod]
        public void RemoveAll()
        {
            Batch batch = GetBatch();
            for (int i = 0; i <= 10; i++)
            {
                Customer customer = GetCustomer();
                batch.Add(customer, "Customer" + i.ToString(), OperationEnum.create);
            }
            batch.RemoveAll();

            if (batch.Count != 0)
                Assert.Fail();
        }


        #endregion

        #region Helper
        private Batch GetBatch()
        {
            ServiceContext context = Initializer.InitializeServiceContextQbo();
            DataService service = new DataService(context);
            return service.CreateNewBatch();
        }


        private Customer GetCustomer()
        {
            Customer customer = new Customer();
            string guid = Guid.NewGuid().ToString("N");
            customer.GivenName = guid.Substring(0, 25);
            customer.Title = guid.Substring(0, 15);
            customer.MiddleName = guid.Substring(0, 5);
            customer.FamilyName = guid.Substring(0, 25);
            customer.DisplayName = guid.Substring(0, 20);
            return customer;
        }
        #endregion

        [TestMethod]
        public void IntuitBatchResponsePropertiesTest()
        {
            IntuitBatchResponse response = new IntuitBatchResponse();
            response.Exception = new Exception.IdsException();
            response.Id = "addCustomer";
            Assert.AreEqual(response.Id, "addCustomer");
            Assert.ReferenceEquals(response.Exception, new Exception.IdsException());
        }



        [TestMethod()]
        public void BatchIDLengthTest()
        {

            ServiceContext context = Initializer.InitializeServiceContextQbo();
            DataService service = new DataService(context);
            Customer customer = new Customer();
            string guid = Guid.NewGuid().ToString("N");
            customer.GivenName = guid.Substring(0, 25);
            customer.Title = guid.Substring(0, 15);
            customer.MiddleName = guid.Substring(0, 5);
            customer.FamilyName = guid.Substring(0, 25);
            customer.DisplayName = guid.Substring(0, 20);

            try
            {
                Batch batch = service.CreateNewBatch();
                batch.Add(customer, Guid.NewGuid().ToString("N").Substring(0, 25), OperationEnum.create);
                batch.Add(new CDCQuery() { ChangedSince = DateTime.Now.AddDays(-1), ChangedSinceSpecified = true, Entities = "Customer" }, "cdcOpration");
                batch.Execute();
            }
            catch (Ipp.Exception.IdsException ex)
            {
                Assert.Fail(ex.ToString());
            }
        }


    }
}
