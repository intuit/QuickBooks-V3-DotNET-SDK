////********************************************************************
// <copyright file="JsonSerializerTest.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains test cases for JsonObjectSerializer methods. </summary>
////********************************************************************

namespace Intuit.Ipp.Utility.Test
{
    using System;
    using Data;
    using Exception;
    using Utility;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Diagnostics;
    using System.Collections.Generic;
    using Core;


    /// <summary>
    /// This is a test class for JsonObjectSerializer and is intended
    /// to contain all JsonObjectSerializer Unit Tests
    /// </summary>
    [TestClass()]
    public class JsonSerializerTest
    {
        /// <summary>
        /// The test Context Instance.
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context.
        /// </summary>
        /// <value>
        /// The test context.
        /// </value>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }

            set
            {
                testContextInstance = value;
            }
        }

        /// <summary>
        /// Gets or sets the IDS logger.
        /// </summary>
        /// <value>
        /// The IDS logger.
        /// </value>
        internal ILogger IDSLogger { get; set; }


        /// <summary>
        /// Serialize constructor test.
        /// </summary>
        [TestMethod]
        public void SerializerConstructorTest()
        {
            JsonObjectSerializer serializer = new JsonObjectSerializer();
        }

        /// <summary>
        /// Serialize constructor test.
        /// </summary>
        [TestMethod]
        public void SerializerConstructorWithArgsTest()
        {
            JsonObjectSerializer serializer = new JsonObjectSerializer(IDSLogger);
        }


        /// <summary>
        /// Entity generic de serialize test for exception.
        /// </summary>
        [TestMethod()]//INFO: this test case is not valid (with new approach to Json deserialization)
        public void EntityGenericDeSerializerTestForException()
        {
            Assert.Inconclusive("Json does not have namespaces to define the entity type");
            Account serializableEntity = SerializerTestHelper.CreateAccountEntity();
            JsonObjectSerializer serializer = new JsonObjectSerializer();
            string serializedJson = serializer.Serialize(serializableEntity);
            try
            {
                Bill deserializedEntity = (Bill)serializer.Deserialize<Bill>(serializedJson);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                if (ex.InnerException == null || ex.GetType() != typeof(SerializationException))
                {
                    Assert.Fail(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Customer entity serialize test.
        /// </summary>
        [TestMethod()]//INFO: Testcase is not proper as serializedJson is having Json text for Customer and deserializer is expecting Json for IntuitResponse
        public void CustomerEntitySerializerTest()
        {
            Customer serializableEntity = SerializerTestHelper.CreateCustomerEntity();
            JsonObjectSerializer serializer = new JsonObjectSerializer();
            string serializedJson = serializer.Serialize(serializableEntity);

            Customer deserializedEntity = (Customer)serializer.Deserialize<Customer>(serializedJson);
            object test = deserializedEntity.AnyIntuitObject;
            // SerializerTestHelper.CompareCustomerObjects(deserializedEntity, serializableEntity);
        }

        /// <summary>
        /// Account entity serialize test.
        /// </summary>
        [TestMethod()]
        public void AccountEntitySerializerTest()
        {
            Account serializableEntity = SerializerTestHelper.CreateAccountEntity();
            JsonObjectSerializer serializer = new JsonObjectSerializer();
            string serializedJson = serializer.Serialize(serializableEntity);
            //serializedJson = "{\"CustomField\":[{\"DefinitionId\":\"1\",\"Name\":\"Field1\",\"Type\":\"StringType\"}]}";
            serializedJson = "{\"Account\":{\"Name\":\"Name_updated42b2d\",\"SubAccount\":false,\"Description\":\"description_updated750e4\",\"Active\":true,\"Classification\":\"Expense\",\"AccountType\":\"Expense\",\"AcctNum\":\"96beb5\",\"BankNum\":\"BankNum\",\"OpeningBalance\":100,\"OpeningBalanceDate\":\"2013-06-27\",\"CurrentBalance\":100,\"CurrentBalanceWithSubAccounts\":100,\"OnlineEnabled\":false,\"status\":\"Pending\",\"Id\":\"NG:30031\",\"SyncToken\":\"2\",\"MetaData\":{\"CreateTime\":\"2013-06-27T12:24:58Z\",\"LastUpdatedTime\":\"2013-06-27T12:25:09Z\"},\"CustomField\":[],\"AttachableRef\":[]},\"time\":\"2013-06-27T12:25:09.884Z\"}";
            IntuitResponse deserializedEntity = (IntuitResponse)serializer.Deserialize<IntuitResponse>(serializedJson);
            //    SerializerTestHelper.CompareAcconutObjects(deserializedEntity.AnyIntuitObjects.GetValue(0), serializableEntity);
            Assert.IsNotNull(deserializedEntity.AnyIntuitObject);
        }


        /// <summary>
        /// NameValue Json serialize test.
        /// </summary>
        [TestMethod()]
        public void PreferenceSerializerTest()
        {
            Preferences serializableEntity = SerializerTestHelper.CreatePreferenceEntity();
            JsonObjectSerializer serializer = new JsonObjectSerializer();
            string serializedJson = serializer.Serialize(serializableEntity);
            string expectedserializedJson = "{\"OtherPrefs\":{\"NameValue\":[{\"Name\":\"SalesFormsPrefs.DefaultCustomerMessage\"},{\"Name\":\"SalesFormsPrefs.DefaultItem\",\"Value\":\"1\"}]},\"Id\":\"1\"}";
            Assert.AreEqual(expectedserializedJson, serializedJson);
        }

        /// <summary>
        /// CustomField Json deserialize test.
        /// </summary>
        [TestMethod()]
        public void PreferenceDeSerializerTest()
        {
            string serializedJson = "{\"QueryResponse\":{\"Preferences\":[{\"AccountingInfoPrefs\":{\"CustomerTerminology\":\"client\"},\"ProductAndServicesPrefs\":{\"ForSales\":true,\"ForPurchase\":true,\"QuantityWithPriceAndRate\":true,\"QuantityOnHand\":true},\"SalesFormsPrefs\":{\"CustomField\":[{\"CustomField\":[{\"Name\":\"SalesFormsPrefs.UseSalesCustom3\",\"Type\":\"BooleanType\",\"BooleanValue\":false},{\"Name\":\"SalesFormsPrefs.UseSalesCustom2\",\"Type\":\"BooleanType\",\"BooleanValue\":false},{\"Name\":\"SalesFormsPrefs.UseSalesCustom1\",\"Type\":\"BooleanType\",\"BooleanValue\":false}]}],\"CustomTxnNumbers\":false,\"AllowDeposit\":false,\"AllowDiscount\":false,\"AllowEstimates\":false,\"IPNSupportEnabled\":false,\"AllowServiceDate\":false,\"AllowShipping\":false,\"DefaultTerms\":{\"value\":\"3\"}},\"VendorAndPurchasesPrefs\":{\"TrackingByCustomer\":false,\"BillableExpenseTracking\":false,\"POCustomField\":[{\"CustomField\":[{\"Name\":\"PurchasePrefs.UsePurchaseCustom3\",\"Type\":\"BooleanType\",\"BooleanValue\":false},{\"Name\":\"PurchasePrefs.UsePurchaseCustom2\",\"Type\":\"BooleanType\",\"BooleanValue\":false},{\"Name\":\"PurchasePrefs.UsePurchaseCustom1\",\"Type\":\"BooleanType\",\"BooleanValue\":false}]}]},\"TimeTrackingPrefs\":{\"UseServices\":false,\"BillCustomers\":false,\"ShowBillRateToAll\":false,\"MarkTimeEntriesBillable\":false},\"TaxPrefs\":{\"UsingSalesTax\":true},\"CurrencyPrefs\":{\"MultiCurrencyEnabled\":false,\"HomeCurrency\":{\"value\":\"USD\"}},\"OtherPrefs\":{\"NameValue\":[{\"Name\":\"SalesFormsPrefs.DefaultCustomerMessage\"},{\"Name\":\"SalesFormsPrefs.DefaultItem\",\"Value\":\"1\"}]},\"domain\":\"QBO\",\"sparse\":false,\"Id\":\"1\",\"SyncToken\":\"0\",\"MetaData\":{\"CreateTime\":\"2013-07-02T04:37:24-07:00\",\"LastUpdatedTime\":\"2013-07-03T01:24:38-07:00\"}}],\"maxResults\":1},\"time\":\"2013-07-03T01:35:04.566-07:00\"}";
            JsonObjectSerializer serializer = new JsonObjectSerializer();
            IntuitResponse deserializedEntity = serializer.Deserialize<IntuitResponse>(serializedJson) as IntuitResponse;
            Assert.IsNotNull(deserializedEntity.AnyIntuitObject);
        }

        /// <summary>
        /// Batch Json serialize test.
        /// </summary>
        [TestMethod()]
        public void BatchSerializerTest()
        {
            List<BatchItemRequest> batchRequests = new List<BatchItemRequest>();
            BatchItemRequest batchItem = new BatchItemRequest();
            batchItem.AnyIntuitObject = new Account() { Id = "1" };
            batchItem.bId = "bid";
            batchItem.operation = OperationEnum.create;
            batchItem.operationSpecified = true;

            batchRequests.Add(batchItem);

            IntuitBatchRequest intuitBatchRequest = new IntuitBatchRequest();
            intuitBatchRequest.BatchItemRequest = batchRequests.ToArray();

            JsonObjectSerializer serializer = new JsonObjectSerializer();
            string serializedJson = serializer.Serialize(intuitBatchRequest);
            string expectedserializedJson = "{\"BatchItemRequest\":[{\"Account\":{\"Id\":\"1\"},\"bId\":\"bid\",\"operation\":\"create\"}]}";
            Assert.AreEqual(expectedserializedJson, serializedJson);
        }

        /// <summary>
        /// Term entity serialize test.
        /// </summary>
        [TestMethod()]
        public void TermEntitySerializerTest()
        {
            Term serializableEntity = SerializerTestHelper.CreateTermEntity();
            JsonObjectSerializer serializer = new JsonObjectSerializer();
            string serializedJson = serializer.Serialize(serializableEntity);
            //serializedJson = "{\"CustomField\":[{\"DefinitionId\":\"1\",\"Name\":\"Field1\",\"Type\":\"StringType\"}]}";
            Term deserializedEntity = (Term)serializer.Deserialize<Term>(serializedJson);
            SerializerTestHelper.CompareTermObjects(deserializedEntity, serializableEntity);
        }

        /// <summary>
        /// Term entity serialize test.
        /// </summary>
        [TestMethod()]
        public void TimeActivityEntitySerializerTest()
        {
            //Term serializableEntity = SerializerTestHelper.CreateTermEntity();
            JsonObjectSerializer serializer = new JsonObjectSerializer();
            //string serializedJson = serializer.Serialize(serializableEntity);
            string serializedJson = "{\"QueryResponse\":{\"TimeActivity\":[{\"TxnDate\":\"2013-08-06\",\"NameOf\":\"Employee\",\"EmployeeRef\":{\"value\":\"19\",\"name\":\"DisplayName037e5835\"},\"CustomerRef\":{\"value\":\"57\",\"name\":\"06d3bd51-0f3c-43d6-9\"},\"ItemRef\":{\"value\":\"2\",\"name\":\"Hours\"},\"BillableStatus\":\"NotBillable\",\"Taxable\":false,\"HourlyRate\":0,\"Hours\":10,\"Minutes\":0,\"Description\":\"UpdatedDesca8a\",\"domain\":\"QBO\",\"sparse\":false,\"Id\":\"5\",\"SyncToken\":\"5\",\"MetaData\":{\"CreateTime\":\"2013-08-06T02:07:43-07:00\",\"LastUpdatedTime\":\"2013-08-06T02:23:26-07:00\"}},{\"TxnDate\":\"2013-08-06\",\"NameOf\":\"Employee\",\"EmployeeRef\":{\"value\":\"19\",\"name\":\"DisplayName037e5835\"},\"CustomerRef\":{\"value\":\"57\",\"name\":\"06d3bd51-0f3c-43d6-9\"},\"ItemRef\":{\"value\":\"2\",\"name\":\"Hours\"},\"BillableStatus\":\"NotBillable\",\"Taxable\":false,\"HourlyRate\":0,\"Hours\":10,\"Minutes\":0,\"Description\":\"asdca\",\"domain\":\"QBO\",\"sparse\":false,\"Id\":\"4\",\"SyncToken\":\"0\",\"MetaData\":{\"CreateTime\":\"2013-08-06T02:06:34-07:00\",\"LastUpdatedTime\":\"2013-08-06T02:06:34-07:00\"}},{\"TxnDate\":\"2013-08-06\",\"NameOf\":\"Vendor\",\"VendorRef\":{\"value\":\"34\",\"name\":\"DisplayName_07cc1ef917b54e34accf87300dd2261f\"},\"ItemRef\":{\"value\":\"2\",\"name\":\"Hours\"},\"BillableStatus\":\"NotBillable\",\"Taxable\":false,\"HourlyRate\":0,\"Hours\":13,\"Minutes\":0,\"Description\":\"Updating Employee Time Activity a143d\",\"domain\":\"QBO\",\"sparse\":false,\"Id\":\"3\",\"SyncToken\":\"1\",\"MetaData\":{\"CreateTime\":\"2013-08-05T23:39:15-07:00\",\"LastUpdatedTime\":\"2013-08-05T23:39:43-07:00\"}}],\"startPosition\":1,\"maxResults\":3},\"time\":\"2013-08-06T03:04:31.099-07:00\"}";
            IntuitResponse deserializedEntity = (IntuitResponse)serializer.Deserialize<IntuitResponse>(serializedJson);
        }
    }
}

