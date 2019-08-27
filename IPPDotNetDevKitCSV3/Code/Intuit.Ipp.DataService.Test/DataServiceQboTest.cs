using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading;
using Intuit.Ipp.Core;
using Intuit.Ipp.Core.Configuration;
using Intuit.Ipp.Data;
using Intuit.Ipp.Exception;
using Intuit.Ipp.Security;

using Intuit.Ipp.DataService.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using System.Collections;
using System.IO;
using System.Diagnostics;
using Intuit.Ipp.DataService.Test.Common;

namespace Intuit.Ipp.DataService.Test
{
    /// <summary>
    ///This is a test class for DataServiceQBOTest and is intended
    ///to contain all DataServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DataServiceQboTest
    {
        private TestContext testContextInstance;
        private static DataServiceTestCases dataServiceTestCases;
        private static ServiceContext context;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            context = Initializer.InitializeServiceContextQbo();
            dataServiceTestCases = new DataServiceTestCases(context);
            
        }

        #region Add Test

      
        [TestMethod()]
        public void AddTest()
        {
            try
            {
                Customer addedCustomer = dataServiceTestCases.AddEntity(DataServiceTestHelper.CreateCustomer()) as Customer;
                Assert.IsNotNull(addedCustomer.Id);                
                
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()][Ignore]
        public void AddBillPaymentTest_BillPaymentCheck()
        {
            //Creating the BillPayment for Add

            BillPayment billPaymentToAdd = DataServiceTestHelper.CreateBillPayment_CheckPayment();
            BillPayment billPaymentAdded = dataServiceTestCases.AddEntity(billPaymentToAdd) as BillPayment;

            DataServiceTestHelper.VerifyBillPayment_BillPaymentCheck(billPaymentAdded, billPaymentAdded);
            
        }

        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void AddTestNullEntity()
        {
            dataServiceTestCases.AddEntity(null);
            Assert.Fail();
        }

        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void AddTestwithoutMandatoryFields()
        {
            Customer customer = new Customer();
            Customer addedCustomer = dataServiceTestCases.AddEntity(customer) as Customer;
            Assert.Fail();
        }

        #endregion

        #region FindAll Tests

        [TestMethod()]
        public void FindAllTest()
        {
            try
            {
                IEnumerable<IEntity> accounts = dataServiceTestCases.FindAllEntities(new Account(), 1, 10);
                Assert.IsNotNull(accounts);
                Assert.IsTrue(accounts.Count() > 0);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }


        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void FindAllTestNullEntity()
        {
            IEnumerable<Customer> customers = dataServiceTestCases.FindAllEntities(null, 0, 10) as IEnumerable<Customer>;
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(IdsException))]
        public void FindAllTestsStartPositionZero()
        {
            IEnumerable<Customer> customers = dataServiceTestCases.FindAllEntities(new Customer(), 0, 10) as IEnumerable<Customer>;
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(IdsException))]
        public void FindAllTestMaxResultZero()
        {
            IEnumerable<Customer> customers = dataServiceTestCases.FindAllEntities(new Customer(), 1, 0) as IEnumerable<Customer>;
            Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(IdsException))]
        public void FindAllTestException()
        {
            IEnumerable<Customer> customers = dataServiceTestCases.FindAllEntities(new Customer(), 999999999, 99999999) as IEnumerable<Customer>;
            Assert.Fail();
        }

        [TestMethod]
        public void FindAllAccountTests()
        {
            IEnumerable<IEntity> accounts = dataServiceTestCases.FindAllEntities(new Account(), 1, 100);
            Assert.IsNotNull(accounts);
            Assert.IsTrue(accounts.Count() > 0);
            
        }

        [TestMethod()]
        public void FindAllTaxClassificationTest()
        {
            try
            {
                IEnumerable<IEntity> taxClassifications = dataServiceTestCases.FindAllEntities(new TaxClassification());
                Assert.IsNotNull(taxClassifications);
                Assert.IsTrue(taxClassifications.Count() > 0);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        #endregion

        #region FindById Test

        [TestMethod()]
        public void FindByIdTest()
        {
            Customer addedCustomer = dataServiceTestCases.AddEntity(DataServiceTestHelper.CreateCustomer()) as Customer;
            try
            {
                Customer foundCustomer = dataServiceTestCases.FindByIdEntity(addedCustomer) as Customer;
                Assert.IsNotNull(foundCustomer);
                Assert.IsTrue(foundCustomer.Id == addedCustomer.Id);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void FindByIdTestNullEntity()
        {
            Customer foundCustomer = dataServiceTestCases.FindByIdEntity(null) as Customer;
            Assert.Fail();
        }

        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void FindByIdTestInvalidId()
        {
            Customer customer = new Customer();
            customer.Id = "00000000";
            Customer foundCustomer = dataServiceTestCases.FindByIdEntity(customer) as Customer;
            Assert.Fail();
        }

        #endregion

        #region FindByLevel
        [TestMethod()]
        public void FindByLevelTest()
        {
            // List<TaxClassification> taxClassList = dataServiceTestCases.FindAllEntities(new TaxClassification()) as List<TaxClassification>;
            IEnumerable<IEntity> taxClassList = dataServiceTestCases.FindAllEntities(new TaxClassification()) ;
            try
            {
              
              
                //TaxClassification taxClassification = new TaxClassification
                //{
                //    Level = taxClassList[0].Level
                //};
                string lvl = "";
                foreach (TaxClassification tx in taxClassList)
                {
                    lvl = tx.Level;
                    break;
                }

                TaxClassification taxClassification = new TaxClassification
                {
                    Level = lvl
                };
                IEnumerable<IEntity> taxClassifications = dataServiceTestCases.FindByLevelEntities(taxClassification);
                Assert.IsNotNull(taxClassifications);
                Assert.IsTrue(taxClassifications.Count() > 0);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void FindByLevelTestNullEntity()
        {
            IEnumerable<TaxClassification> taxClassifications = dataServiceTestCases.FindByLevelEntities(null) as IEnumerable<TaxClassification>;
            Assert.Fail();
        }

        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void FindByLevelTestInvalidEntityType()
        {
            Customer customer = new Customer();
            IEnumerable<Customer> customers = dataServiceTestCases.FindByLevelEntities(customer) as IEnumerable<Customer>;
            Assert.Fail();
        }

        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void FindByLevelTestInvalidLevel()
        {

            TaxClassification taxClassification = new TaxClassification
            {
                Level = "level"
            };
            IEnumerable<TaxClassification> taxClassifications = dataServiceTestCases.FindByLevelEntities(taxClassification) as IEnumerable<TaxClassification>;
            Assert.Fail();
        }
        #endregion

        #region FindByParentId
        [TestMethod()]
        public void FindByParentIdTest()
        {
            // List<TaxClassification> taxClassList = dataServiceTestCases.FindAllEntities(new TaxClassification()) as List<TaxClassification>;
            IEnumerable<IEntity> taxClassList = dataServiceTestCases.FindAllEntities(new TaxClassification()) as IEnumerable<IEntity>;

            try
            {
                //TaxClassification taxClassification = new TaxClassification
                //{
                //    ParentRef = taxClassList[0].ParentRef
                //};
                ReferenceType pRef = new ReferenceType(); 
                foreach (TaxClassification tx in taxClassList)
                {
                    pRef = tx.ParentRef;
                    break;
                }

                TaxClassification taxClassification = new TaxClassification
                {
                    ParentRef = pRef
                };
                IEnumerable<IEntity> taxClassifications = dataServiceTestCases.FindByParentIdEntities(taxClassification);
                Assert.IsNotNull(taxClassifications);
                Assert.IsTrue(taxClassifications.Count() > 0);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void FindByParentIdTestNullEntity()
        {
            IEnumerable<TaxClassification> taxClassifications = dataServiceTestCases.FindByParentIdEntities(null) as IEnumerable<TaxClassification>;
            Assert.Fail();
        }

        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void FindByParentIdTestInvalidEntityType()
        {
            Customer customer = new Customer();
            IEnumerable<Customer> customers = dataServiceTestCases.FindByParentIdEntities(customer) as IEnumerable<Customer>;
            Assert.Fail();
        }

        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void FindByParentIdTestInvalidParentId()
        {
            TaxClassification taxClassification = new TaxClassification
            {
                ParentRef = new ReferenceType
                {
                    Value = "parent"
                }
            };
            IEnumerable<TaxClassification> taxClassifications = dataServiceTestCases.FindByParentIdEntities(taxClassification) as IEnumerable<TaxClassification>;
            Assert.Fail();
        }

        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void FindByParentIdTestNullParentId()
        {
            TaxClassification taxClassification = new TaxClassification
            {
                ParentRef = null
            };
            IEnumerable<TaxClassification> taxClassifications = dataServiceTestCases.FindByParentIdEntities(taxClassification) as IEnumerable<TaxClassification>;
            Assert.Fail();
        }
        #endregion

        #region Update Tests

        [TestMethod()]
        public void UpdateTest()
        {
            try
            {
                Customer addedCustomer = dataServiceTestCases.AddEntity(DataServiceTestHelper.CreateCustomer()) as Customer;
                string middleName = "ABC";
                Customer UpdatedCustomer = dataServiceTestCases.UpdateEntity(DataServiceTestHelper.UpdateCustomerMiddleName(addedCustomer, middleName)) as Customer;
                Assert.AreEqual(UpdatedCustomer.MiddleName, middleName);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void UpdateTestNullEntity()
        {
            Customer updatedCustomer = dataServiceTestCases.UpdateEntity(null) as Customer;
            Assert.Fail();
        }

        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void UpdateTestInvalidCustomer()
        {
            Customer customer = new Customer();
            customer.Id = "00000";
            Customer updatedCustomer = dataServiceTestCases.UpdateEntity(customer) as Customer;
            Assert.Fail();
        }


        [TestMethod()]
        public void SparseUpdateTest()
        {
            try
            {
                Customer addedCustomer = dataServiceTestCases.AddEntity(DataServiceTestHelper.CreateCustomer()) as Customer;
                addedCustomer.MiddleName = "ABC";
                addedCustomer.sparse = true;
                addedCustomer.sparseSpecified = true;
                Customer UpdatedCustomer = dataServiceTestCases.UpdateEntity(addedCustomer) as Customer;
                Assert.AreEqual(UpdatedCustomer.MiddleName, addedCustomer.MiddleName);
                Assert.AreEqual(UpdatedCustomer.GivenName, addedCustomer.GivenName);
                Assert.AreEqual(UpdatedCustomer.Title, addedCustomer.Title);
                Assert.AreEqual(UpdatedCustomer.FamilyName, addedCustomer.FamilyName);
                Assert.AreEqual(UpdatedCustomer.DisplayName, addedCustomer.DisplayName);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }


        #endregion

        // TODO: Uncomment this section when void operation is supported
        /*
        #region Void Tests

        /// <summary>
        /// Test Void method.
        /// </summary>
        [TestMethod]
        public void VoidTest()
        {
            try
            {
                Customer result = dataServiceTestCases.AddEntity(DataServiceTestHelper.CreateCustomer()) as Customer;
                Customer voidCustomer = new Customer();
                voidCustomer.Id = result.Id;
                dataServiceTestCases.VoidEntity(voidCustomer);
                Customer foundCustomer = dataServiceTestCases.FindByIdEntity(voidCustomer) as Customer;
                Assert.AreEqual(voidCustomer.status, foundCustomer.status);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void VoidTestNullEntity()
        {
            dataServiceTestCases.VoidEntity(null);
            Assert.Fail();
        }

        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void VoidTestInvalidCustomer()
        {
            Customer customer = new Customer();
            customer.Id = "000000";
            dataServiceTestCases.VoidEntity(customer);
            Assert.Fail();
        }

        #endregion
        */
        #region Delete Tests

        /* /// <summary>
        /// Test delete method.
        /// </summary>
        [TestMethod]
        public void DeleteTest()
        {
            try
            {
                Customer result = dataServiceTestCases.AddEntity(DataServiceTestHelper.CreateCustomer()) as Customer;
                Customer deleteCustomer = new Customer();
                deleteCustomer.Id = result.Id;
                dataServiceTestCases.DeleteEntity(deleteCustomer);
                Customer foundCustomer = dataServiceTestCases.FindByIdEntity(deleteCustomer) as Customer;
                Assert.AreEqual(deleteCustomer.status, foundCustomer.status);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        } */

        [TestMethod()]
        [ExpectedException(typeof(IdsException))]
        public void DeleteTestNullEntity()
        {
            dataServiceTestCases.DeleteEntity(null);
            Assert.Fail();
        }


        #endregion

        #region CDCTest

        [TestMethod()]//INFO: there is possibility that there is no change in past two days for customer or vendor, then CDC will not return any entities. FIX: make an operation for customer and vendor before calling CDC
        public void CDCTest()
        {
            Customer addedCustomer = dataServiceTestCases.AddEntity(DataServiceTestHelper.CreateCustomer()) as Customer;
            if (addedCustomer == null || addedCustomer.Id == null)
            {
                Assert.Inconclusive("Unable to add customer to verify CDC Test");
            }

            Vendor addedVendor = dataServiceTestCases.AddEntity(DataServiceTestHelper.CreateVendor()) as Vendor;
            if (addedVendor == null || addedVendor.Id == null)
            {
                Assert.Inconclusive("Unable to add vendor to verify CDC Test");
            }

            List<IEntity> entityList = new List<IEntity>() { new Customer(), new Vendor() };

            IntuitCDCResponse cdcResponse = dataServiceTestCases.CDCEntity(entityList, System.DateTime.Today.AddDays(-2));
            try
            {
                List<Customer> customerList = cdcResponse.getEntity("Customer").Cast<Customer>().ToList();
                Assert.IsNotNull(customerList);

                List<Vendor> vendorList = cdcResponse.getEntity("Vendor").Cast<Vendor>().ToList();
                Assert.IsNotNull(customerList);

            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        #endregion

        #region Attachable Test

        [DeploymentItem("Resources\\test.jpg", "Resources")]
        [DeploymentItem("Resources\\testWriteBack.jpg", "Resources")]
        [TestMethod()]
        public void UploadTest()
        {
            try
            {
                string testImagePath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "\\", "Resources\\test.jpg");
                string testWritebackImagePath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, "\\", "Resources\\testWriteBack.jpg");
                FileInfo file = new FileInfo(testImagePath);
                Attachable attachable = DataServiceTestHelper.CreateAttachable();

                using (FileStream fs = file.OpenRead())
                {
                    attachable.ContentType = "image/jpg";
                    attachable.FileName = file.Name;
                    attachable = dataServiceTestCases.Upload(attachable, fs);
                }
                Assert.IsNotNull(attachable.Id);
                //Attachable attachable = new Attachable() { FileAccessUri = "/v3/company/736116105/download/100000000000273888" };

                byte[] responseByte = dataServiceTestCases.Download(attachable);
                file = new FileInfo(testWritebackImagePath);
                using (FileStream fs = file.OpenWrite())
                {
                    fs.Write(responseByte, 0, (int)responseByte.Length);
                }
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        #endregion

        #region SendEmail Tests

        [TestMethod]
        public void EstimateEmailToSpecifiedEmailAddressTest()
        {
            List<Estimate> estimates = Helper.FindAll<Estimate>(dataServiceTestCases.GetContext(), new Estimate(), 1, 10);

            Assert.IsTrue(estimates.Count > 0);

            SalesTransaction returnedEntity = Helper.SendEmail<Estimate>(dataServiceTestCases.GetContext(), estimates[0], "mycompany@intuit.com");

            Assert.IsTrue(returnedEntity.EmailStatus == EmailStatusEnum.EmailSent);
        }

        [TestMethod]
        public void EstimateEmailToEmailAddressinBillEmailTest()
        {
            List<Estimate> estimates = Helper.FindAll<Estimate>(dataServiceTestCases.GetContext(), new Estimate(), 1, 10);

            Assert.IsTrue(estimates.Count > 0);

            estimates[0].BillEmail = new EmailAddress { Address = "mycompany@intuit.com" };

            Estimate updatedSalesReceipt = Helper.Update<Estimate>(dataServiceTestCases.GetContext(), estimates[0]);

            SalesTransaction returnedEntity = Helper.SendEmail<Estimate>(dataServiceTestCases.GetContext(), updatedSalesReceipt);

            Assert.IsTrue(returnedEntity.EmailStatus == EmailStatusEnum.EmailSent);
        }

        [TestMethod]
        public void EstimateEmailToInvalidEmailAddressTest()
        {
            try
            {
                List<Estimate> estimates = Helper.FindAll<Estimate>(dataServiceTestCases.GetContext(), new Estimate(), 1, 10);

                Assert.IsTrue(estimates.Count > 0);

                SalesTransaction returnedEntity = Helper.SendEmail<Estimate>(dataServiceTestCases.GetContext(), estimates[0], "mycompany@@intuit.com");

                Assert.IsTrue(returnedEntity.EmailStatus == EmailStatusEnum.EmailSent);

            }
            catch (IdsException idsEx)
            {
                Assert.IsTrue(idsEx.Message == "Email address is either not in valid format or is not provided");
            }
        }

        [TestMethod]
        public void EstimateEmailToEmptyEmailAddressinBillEmailTest()
        {
            try
            {
                List<Estimate> estimates = Helper.FindAll<Estimate>(dataServiceTestCases.GetContext(), new Estimate(), 1, 10);

                Assert.IsTrue(estimates.Count > 0);

                estimates[0].BillEmail = new EmailAddress { Address = "" };

                Estimate updatedSalesReceipt = Helper.Update<Estimate>(dataServiceTestCases.GetContext(), estimates[0]);

                SalesTransaction returnedEntity = Helper.SendEmail<Estimate>(dataServiceTestCases.GetContext(), updatedSalesReceipt);
            }
            catch (IdsException idsEx)
            {
                Assert.IsNotNull(idsEx);
            }
        }

        [TestMethod]
        public void EstimateEmailNullEntityTest()
        {
            try
            {
                SalesTransaction returnedEntity = Helper.SendEmail<Estimate>(dataServiceTestCases.GetContext(), null);
            }
            catch (IdsException idsEx)
            {
                Assert.IsTrue(idsEx.Message == "Parameter cannot be null.");
            }
        }

        [TestMethod]
        // Make sure that the idsEx.Message is corrent
        public void EstimateEmailEntityWithNullIdTest()
        {
            try
            {
                List<Estimate> estimates = Helper.FindAll<Estimate>(dataServiceTestCases.GetContext(), new Estimate(), 1, 10);
                Assert.IsTrue(estimates.Count > 0);

                estimates[0].Id = null;

                SalesTransaction returnedEntity = Helper.SendEmail<Estimate>(dataServiceTestCases.GetContext(), estimates[0]);

            }
            catch (IdsException idsEx)
            {
                Assert.IsTrue(idsEx.Message == "Only entitites of type SalesReceipt, Invoice and Estimate are supported for this operation.");
            }
        }

        [TestMethod]
        // Make sure that the idsEx.Message is corrent
        public void EstimateEmailNonSupportedEntityTest()
        {
            try
            {
                List<Customer> customers = Helper.FindAll<Customer>(dataServiceTestCases.GetContext(), new Customer(), 1, 10);
                Assert.IsTrue(customers.Count > 0);

                IEntity returnedEntity = Helper.SendEmail<Customer>(dataServiceTestCases.GetContext(), customers[0]);
            }
            catch (IdsException idsEx)
            {
                Assert.IsTrue(idsEx.Message == "Only entitites of type SalesReceipt, Invoice and Estimate are supported for this operation.");
            }
        }

        [TestMethod]
        public void EstimateEmailToggleSerializationTest()
        {
            try
            {
                if (dataServiceTestCases.GetContext().IppConfiguration.Message.Request.SerializationFormat == SerializationFormat.Xml)
                    dataServiceTestCases.GetContext().IppConfiguration.Message.Request.SerializationFormat = SerializationFormat.Json;

                if (dataServiceTestCases.GetContext().IppConfiguration.Message.Response.SerializationFormat == SerializationFormat.Xml)
                    dataServiceTestCases.GetContext().IppConfiguration.Message.Response.SerializationFormat = SerializationFormat.Json;

                List<Estimate> estimates = Helper.FindAll<Estimate>(dataServiceTestCases.GetContext(), new Estimate(), 1, 10);

                Assert.IsTrue(estimates.Count > 0);

                SalesTransaction returnedEntity = Helper.SendEmail<Estimate>(dataServiceTestCases.GetContext(), estimates[0], "mycompany@intuit.com");

                Assert.IsTrue(returnedEntity.EmailStatus == EmailStatusEnum.EmailSent);

            }
            catch (IdsException idsEx)
            {
                Assert.Fail(idsEx.Message);
            }
        }

        #endregion

        #region GetPdf Tests

        #region AsyncTests

        [TestMethod]
        [Ignore]//needs physical path access
        public void GetPdfByIdAsyncTest()
        {
            //Creating the SalesReceipt for Adding
            List<Invoice> invoices = Helper.FindAll<Invoice>(dataServiceTestCases.GetContext(), new Invoice(), 1, 10);

            Assert.IsTrue(invoices.Count > 0);
            //Get salesreceipt in pdf format for the passed in invoice object and verify
            Helper.GetPdfAsync<Invoice>(dataServiceTestCases.GetContext(), invoices[0]);
        }

        [TestMethod] // Issue with this test
        public void GetPdfAsyncNullEntityTest()
        {
            try
            {
                //Creating the SalesReceipt for Adding
                //List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(dataServiceTestCases.GetContext(), new SalesReceipt(), 1, 10);

                //Assert.IsTrue(salesReceipts.Count > 0);
                //Get salesreceipt in pdf format for the passed in estimate object and verify
                Helper.GetPdfAsyncNullEntity<SalesReceipt>(dataServiceTestCases.GetContext(), null);
            }
            catch (IdsException idsEx)
            {
                Assert.IsNotNull(idsEx);
            }
        }

        [TestMethod]
        public void GetPdfAsyncNullEntityId()
        {
            try
            {
                //Creating the SalesReceipt for Adding
                List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(dataServiceTestCases.GetContext(), new SalesReceipt(), 1, 10);

                Assert.IsTrue(salesReceipts.Count > 0);

                salesReceipts[0].Id = null;

                //Get salesreceipt in pdf format for the passed in estimate object and verify
                Helper.GetPdfAsyncNullEntity<SalesReceipt>(dataServiceTestCases.GetContext(), salesReceipts[0]);
            }
            catch (IdsException idsEx)
            {

                Assert.IsNotNull(idsEx);
            }
        }

        [TestMethod]
        public void GetPdfAsyncNonSupportedEntity()
        {
            try
            {
                //Creating the SalesReceipt for Adding
                List<Customer> customers = Helper.FindAll<Customer>(dataServiceTestCases.GetContext(), new Customer(), 1, 10);

                Assert.IsTrue(customers.Count > 0);

                customers[0].Id = null;

                //Get salesreceipt in pdf format for the passed in estimate object and verify
                Helper.GetPdfAsync<Customer>(dataServiceTestCases.GetContext(), customers[0]);
            }
            catch (IdsException idsEx)
            {

                Assert.IsNotNull(idsEx);
            }
        }

        #endregion

        #region Sync tests


        [TestMethod][Ignore]//needs physical path access
        public void GetPdfByIdTest()
        {
            List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(dataServiceTestCases.GetContext(), new SalesReceipt(), 1, 10);

            Assert.IsTrue(salesReceipts.Count > 0);

            Ipp.DataService.DataService service = new Ipp.DataService.DataService(dataServiceTestCases.GetContext());

            byte[] response = service.GetPdf<SalesReceipt>(salesReceipts[0]);

            //assert to make sure that data is returned
            Assert.IsTrue(response.Length > 0);

            string fileName = string.Format(@"C:\salesreceipt_{0}.pdf", Guid.NewGuid());

            System.IO.File.WriteAllBytes(fileName, response);

            //check if file exists
            Assert.IsTrue(File.Exists(fileName));

            //read the file from bytes and compare bytes
            byte[] readFromFile = File.ReadAllBytes(fileName);


            //bytes read from file should be greater than 0
            Assert.IsTrue(readFromFile.Length > 0);

            for (int i = 0; i < readFromFile.Length; i++)
                Assert.AreEqual(response[i], readFromFile[i]);


            //cleanup
            if (File.Exists(fileName))
                File.Delete(fileName);

        }

        [TestMethod]
        public void GetPdfNullEntityTest()
        {
            try
            {
                List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(dataServiceTestCases.GetContext(), new SalesReceipt(), 1, 10);

                Assert.IsTrue(salesReceipts.Count > 0);

                Ipp.DataService.DataService service = new Ipp.DataService.DataService(dataServiceTestCases.GetContext());

                byte[] response = service.GetPdf<SalesReceipt>(null);

            }
            catch (IdsException idsEx)
            {
                Assert.IsNotNull(idsEx);
            }
        }

        [TestMethod]
        public void GetPdfNullEntityIdTest()
        {
            try
            {
                List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(dataServiceTestCases.GetContext(), new SalesReceipt(), 1, 10);

                Assert.IsTrue(salesReceipts.Count > 0);

                Ipp.DataService.DataService service = new Ipp.DataService.DataService(dataServiceTestCases.GetContext());

                salesReceipts[0].Id = null;
                byte[] response = service.GetPdf<SalesReceipt>(salesReceipts[0]);

            }
            catch (IdsException idsEx)
            {
                Assert.IsNotNull(idsEx);
            }
        }

        [TestMethod]
        public void GetPdfNonSupportedEntityTest()
        {
            try
            {
                List<Customer> customers = Helper.FindAll<Customer>(dataServiceTestCases.GetContext(), new Customer(), 1, 10);

                Assert.IsTrue(customers.Count > 0);

                Ipp.DataService.DataService service = new Ipp.DataService.DataService(dataServiceTestCases.GetContext());

                byte[] response = service.GetPdf<Customer>(customers[0]);

            }
            catch (IdsException idsEx)
            {
                Assert.IsNotNull(idsEx);
            }
        }

        #endregion


        #endregion

        #region Send Email Tests

        [TestMethod]
        public void SalesReceiptEmailToSpecifiedEmailAddressTest()
        {
            List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(dataServiceTestCases.GetContext(), new SalesReceipt(), 1, 10);

            Assert.IsTrue(salesReceipts.Count > 0);

            Ipp.DataService.DataService service = new Ipp.DataService.DataService(dataServiceTestCases.GetContext());

            SalesTransaction returnedEntity = service.SendEmail<SalesReceipt>(salesReceipts[0], "mycompany@intuit.com");

            Assert.IsTrue(returnedEntity.EmailStatus == EmailStatusEnum.EmailSent);
        }

        [TestMethod]
        public void SalesReceiptEmailToEmailAddressInBillEmailTest()
        {
            try
            {
                List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(dataServiceTestCases.GetContext(), new SalesReceipt(), 1, 10);

                Assert.IsTrue(salesReceipts.Count > 0);

                salesReceipts[0].BillEmail = new EmailAddress { Address = "mycompany@intuit.com" };

                SalesReceipt updatedSalesReceipt = Helper.Update<SalesReceipt>(dataServiceTestCases.GetContext(), salesReceipts[0]);


                Ipp.DataService.DataService service = new Ipp.DataService.DataService(dataServiceTestCases.GetContext());


                SalesTransaction returnedEntity = service.SendEmail<SalesReceipt>(updatedSalesReceipt);

            }
            catch (IdsException idsEx)
            {
                Assert.IsTrue(idsEx.Message == "Email address is either not in valid format or is not provided");
            }
        }

        [TestMethod]
        public void SalesReceiptEmailToInvalidEmailAddressTest()
        {
            try
            {
                List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(dataServiceTestCases.GetContext(), new SalesReceipt(), 1, 10);

                Assert.IsTrue(salesReceipts.Count > 0);


                SalesReceipt updatedSalesReceipt = Helper.Update<SalesReceipt>(dataServiceTestCases.GetContext(), salesReceipts[0]);


                Ipp.DataService.DataService service = new Ipp.DataService.DataService(dataServiceTestCases.GetContext());


                SalesTransaction returnedEntity = service.SendEmail<SalesReceipt>(updatedSalesReceipt, "mycompany@@intuit.com");

            }
            catch (IdsException idsEx)
            {
                Assert.IsTrue(idsEx.Message == "Email address is either not in valid format or is not provided");
            }
        }

        [TestMethod]
        public void SalesReceiptEmailToEmptyEmailAddressInBillEmailTest()
        {
            try
            {
                List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(dataServiceTestCases.GetContext(), new SalesReceipt(), 1, 10);

                Assert.IsTrue(salesReceipts.Count > 0);

                salesReceipts[0].BillEmail = new EmailAddress { Address = "" };

                SalesReceipt updatedSalesReceipt = Helper.Update<SalesReceipt>(dataServiceTestCases.GetContext(), salesReceipts[0]);


                Ipp.DataService.DataService service = new Ipp.DataService.DataService(dataServiceTestCases.GetContext());


                SalesTransaction returnedEntity = service.SendEmail<SalesReceipt>(updatedSalesReceipt);

            }
            catch (IdsException idsEx)
            {
                Assert.IsNotNull(idsEx);
            }
        }

        [TestMethod]
        public void SalesReceiptEmailNullEntityTest()
        {
            try
            {
                Ipp.DataService.DataService service = new Ipp.DataService.DataService(dataServiceTestCases.GetContext());
                SalesTransaction returnedEntity = service.SendEmail<SalesReceipt>(null, "mycompany@intuit.com");
            }
            catch (IdsException idsEx)
            {
                Assert.IsTrue(idsEx.Message == "Parameter cannot be null.");
            }
        }

        [TestMethod]
        public void SalesReceiptEmailEntityWithNullIdTest()
        {
            try
            {
                List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(dataServiceTestCases.GetContext(), new SalesReceipt(), 1, 10);
                Assert.IsTrue(salesReceipts.Count > 0);
                SalesReceipt updatedSalesReceipt = Helper.Update<SalesReceipt>(dataServiceTestCases.GetContext(), salesReceipts[0]);

                updatedSalesReceipt.Id = null;

                Ipp.DataService.DataService service = new Ipp.DataService.DataService(dataServiceTestCases.GetContext());


                SalesTransaction returnedEntity = service.SendEmail<SalesReceipt>(updatedSalesReceipt);

            }
            catch (IdsException idsEx)
            {
                Assert.IsTrue(idsEx.Message == "Id of the entity cannot be null or empty.");
            }
        }

        [TestMethod]
        public void SalesReceiptEmailToggleSerializationTest()
        {
            try
            {
                if (dataServiceTestCases.GetContext().IppConfiguration.Message.Request.SerializationFormat == SerializationFormat.Xml)
                    dataServiceTestCases.GetContext().IppConfiguration.Message.Request.SerializationFormat = SerializationFormat.Json;

                if (dataServiceTestCases.GetContext().IppConfiguration.Message.Response.SerializationFormat == SerializationFormat.Xml)
                    dataServiceTestCases.GetContext().IppConfiguration.Message.Response.SerializationFormat = SerializationFormat.Json;
                
                List<SalesReceipt> salesReceipts = Helper.FindAll<SalesReceipt>(dataServiceTestCases.GetContext(), new SalesReceipt(), 1, 10);

                Assert.IsTrue(salesReceipts.Count > 0);

                Ipp.DataService.DataService service = new Ipp.DataService.DataService(dataServiceTestCases.GetContext());

                SalesTransaction returnedEntity = service.SendEmail<SalesReceipt>(salesReceipts[0], "mycompany@intuit.com");

                Assert.IsTrue(returnedEntity.EmailStatus == EmailStatusEnum.EmailSent);
            }
            catch (IdsException idsEx)
            {
                Assert.Fail(idsEx.Message);
            }
            
            
        }
        #endregion
    }
}
