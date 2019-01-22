using System;
using System.Collections.Generic;
using System.Configuration;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.Exception;
using Intuit.Ipp.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Linq;

namespace Intuit.Ipp.DataService.Test
{
    /// <summary>
    ///This is a test class for DataServiceTest and is intended
    ///to contain all DataServiceTest Unit Tests
    ///</summary>
    [TestClass()]
    public class AsyncDataServicesQboTest
    {
        private TestContext testContextInstance;
        private static DataService qboService;

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
            string accessTokenQBO = ConfigurationManager.AppSettings["AccessTokenQBO"];
            string accessTokenSecretQBO = ConfigurationManager.AppSettings["AccessTokenSecretQBO"];
            string consumerKeyQBO = ConfigurationManager.AppSettings["ConsumerKeyQBO"];
            string ConsumerSecretQBO = ConfigurationManager.AppSettings["ConsumerSecretQBO"];
            string realmIAQBO = ConfigurationManager.AppSettings["RealmIAQBO"];

            OAuthRequestValidator oAuthRequestValidatorQbo = new OAuthRequestValidator(accessTokenQBO, accessTokenSecretQBO, consumerKeyQBO, ConsumerSecretQBO);
            ServiceContext qboContext = new ServiceContext(realmIAQBO, IntuitServicesType.QBO, oAuthRequestValidatorQbo);
            qboService = new DataService(qboContext);
        }

        #region Add test

        [TestMethod()]
        public void AddQboTest()
        {
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
                qboService.OnAddAsyncCompleted = (sender, e) =>
                {
                    Assert.IsNotNull(e);
                    Customer addedCustomer = e.Entity as Customer;
                    Assert.IsTrue(!string.IsNullOrEmpty(addedCustomer.Id));
                    manualEvent.Set();
                };
                qboService.AddAsync(customer);
                manualEvent.WaitOne(30000);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]//INFO: Fails when run in group
        public void AddQboTestNullEntity()
        {
            Customer customer = null;
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnAddAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.AddAsync(customer);
            manualEvent.WaitOne(30000);
        }

        [TestMethod()]
        public void AddQboTestwithoutMandatoryFields()
        {
            Customer customer = new Customer();
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnAddAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.AddAsync(customer);
            manualEvent.WaitOne(30000);
        }

        #endregion

        #region Find All tests

        [TestMethod()]
        public void FindAllQboTest()
        {
            Customer customer = new Customer();
            try
            {
                // Used to signal the waiting test thread that a async operation have completed.    
                ManualResetEvent manualEvent = new ManualResetEvent(false);
                qboService.OnFindAllAsyncCompleted = (sender, e) =>
                {
                    Assert.IsNotNull(e);
                    Assert.IsNotNull(e.Entities);
                    Assert.IsTrue(e.Entities.Count <= 10);
                    manualEvent.Set();
                };
                qboService.FindAllAsync(customer, 1, 10);
                manualEvent.WaitOne(30000);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void FindAllTaxClassificationsTest()
        {
            TaxClassification taxClassification = new TaxClassification();
            try
            {
                // Used to signal the waiting test thread that a async operation have completed.    
                ManualResetEvent manualEvent = new ManualResetEvent(false);
                qboService.OnFindAllAsyncCompleted = (sender, e) =>
                {
                    Assert.IsNotNull(e);
                    Assert.IsNotNull(e.Entities);
                    Assert.IsTrue(e.Entities.Count <= 10);
                    manualEvent.Set();
                };
                qboService.FindAllAsync(taxClassification);
                manualEvent.WaitOne(30000);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void FindAllQboTestNullEntity()
        {
            Customer customer = null;
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnFindAllAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.FindAllAsync(customer);
            manualEvent.WaitOne(30000);
        }

        [TestMethod]
        public void FindAllQboTestsStartPositionZero()
        {
            Customer customer = new Customer();
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnFindAllAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.FindAllAsync(customer);
            qboService.FindAllAsync(customer, 0, 10);
            manualEvent.WaitOne(30000);
        }

        [TestMethod]
        public void FindAllQboTestMaxResultZero()
        {
            Customer customer = new Customer();
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnFindAllAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.FindAllAsync(customer);
            qboService.FindAllAsync(customer, 1, 0);
            manualEvent.WaitOne(30000);
        }

        [TestMethod]
        public void FindAllQboTestException()
        {
            Customer customer = new Customer();
            // Used to signal the waiting test thread that a async operation have completed.    
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnFindAllAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.FindAllAsync(customer, 999999999, 99999999);
            manualEvent.WaitOne(30000);
        }

        #endregion

        #region Find by Id test

        [TestMethod()]
        public void FindByIdQboTest()
        {
            Customer customer = new Customer();
            string guid = Guid.NewGuid().ToString("N");
            customer.GivenName = guid.Substring(0, 25);
            customer.Title = guid.Substring(0, 15);
            customer.MiddleName = guid.Substring(0, 5);
            customer.FamilyName = guid.Substring(0, 25);
            customer.DisplayName = guid.Substring(0, 20);

            ManualResetEvent manualEvent = new ManualResetEvent(false);
            Customer getcustomer = qboService.Add(customer);
            qboService.OnFindByIdAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Entity);
                Assert.IsTrue(getcustomer.Id == (e.Entity as Customer).Id);
                manualEvent.Set();
            };
            Intuit.Ipp.Data.IntuitEntity intuitEntity = getcustomer as Intuit.Ipp.Data.IntuitEntity;
            qboService.FindByIdAsync(getcustomer);
            manualEvent.WaitOne(30000);
        }

        [TestMethod()]//INFO: Fails when run in group
        public void FindByIdQboTestNullEntity()
        {
            Customer customer = null;
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnFindByIdAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.FindByIdAsync(customer);
            manualEvent.WaitOne(60000);
        }

        [TestMethod()]
        public void FindByIdQboTestInvalidId()
        {
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            Customer customer = new Customer();
            customer.Id = "00000000";
            qboService.OnFindByIdAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.FindByIdAsync(customer);
            manualEvent.WaitOne(30000);
        }

        #endregion

        #region Find by Level test

        [TestMethod()]
        public void FindByLevelQboTest()
        {
            List<TaxClassification> taxClassList = qboService.FindAll(new TaxClassification()).ToList();
            TaxClassification taxClassification = new TaxClassification
            {
                Level = taxClassList[0].Level
            };
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnFindByLevelAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Entities);
                Assert.IsTrue(e.Entities.Count > 0);
                manualEvent.Set();
            };
            qboService.FindByLevelAsync(taxClassification);
            manualEvent.WaitOne(30000);
        }

        [TestMethod()]//INFO: Fails when run in group
        public void FindByLevelQboTestNullEntity()
        {
            TaxClassification taxClassification = null;
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnFindByLevelAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.FindByLevelAsync(taxClassification);
            manualEvent.WaitOne(60000);
        }

        [TestMethod()]
        public void FindByLevelQboTestInvalidLevel()
        {
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            TaxClassification taxClassification = new TaxClassification
            {
                Level = "level"
            };
            qboService.OnFindByLevelAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.FindByLevelAsync(taxClassification);
            manualEvent.WaitOne(30000);
        }

        [TestMethod()]
        public void FindByLevelQboTestInvalidType()
        {
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            Customer customer = new Customer();
            qboService.OnFindByLevelAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.FindByLevelAsync(customer);
            manualEvent.WaitOne(30000);
        }

        #endregion

        #region Find by ParentId test

        [TestMethod()]
        public void FindByParentIdQboTest()
        {
            List<TaxClassification> taxClassList = qboService.FindAll(new TaxClassification()).ToList();
            TaxClassification taxClassification = new TaxClassification
            {
                ParentRef = taxClassList[0].ParentRef
            };
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnFindByParentIdAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Entities);
                Assert.IsTrue(e.Entities.Count > 0);
                manualEvent.Set();
            };
            qboService.FindByParentIdAsync(taxClassification);
            manualEvent.WaitOne(30000);
        }

        [TestMethod()]//INFO: Fails when run in group
        public void FindByParentIdQboTestNullEntity()
        {
            TaxClassification taxClassification = null;
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnFindByParentIdAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.FindByParentIdAsync(taxClassification);
            manualEvent.WaitOne(60000);
        }

        [TestMethod()]//INFO: Fails when run in group
        public void FindByParentIdQboTestNullParentRef()
        {
            TaxClassification taxClassification = new TaxClassification
            {
                ParentRef = null
            };
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnFindByParentIdAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.FindByParentIdAsync(taxClassification);
            manualEvent.WaitOne(60000);
        }

        [TestMethod()]
        public void FindByarentIdQboTestInvalidParentId()
        {
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            TaxClassification taxClassification = new TaxClassification
            {
                ParentRef = new ReferenceType
                {
                    Value = "parent"
                }
            };
            qboService.OnFindByParentIdAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.FindByParentIdAsync(taxClassification);
            manualEvent.WaitOne(30000);
        }

        [TestMethod()]
        public void FindByParentIdQboTestInvalidType()
        {
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            Customer customer = new Customer();
            qboService.OnFindByParentIdAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.FindByParentIdAsync(customer);
            manualEvent.WaitOne(30000);
        }

        #endregion

        #region update tests

        [TestMethod()]
        public void UpdateQboTest()
        {

            Customer customer = new Customer();
            string guid = Guid.NewGuid().ToString("N");
            customer.GivenName = guid.Substring(0, 25);
            customer.Title = guid.Substring(0, 15);
            customer.MiddleName = guid.Substring(0, 5);
            customer.FamilyName = guid.Substring(0, 25);
            customer.DisplayName = guid.Substring(0, 20);
            customer.Notes = "Omit this property during update";
            try
            {
                Customer addedCustomer = qboService.Add(customer);
                Customer newCustomer = new Customer();
                newCustomer = addedCustomer;
                addedCustomer.MiddleName = "ABC";

                ManualResetEvent manualEvent = new ManualResetEvent(false);
                qboService.OnUpdateAsyncCompleted = (sender, e) =>
                {
                    Assert.IsNotNull(e);
                    Assert.IsNotNull(e.Entity);
                    Customer updatedCustomer = e.Entity as Customer;
                    Assert.IsTrue(updatedCustomer.MiddleName == "ABC");
                    manualEvent.Set();
                };
                qboService.UpdateAsync(addedCustomer);
                manualEvent.WaitOne(30000);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]//INFO: Fails when run in group
        public void UpdateQboTestNullEntity()
        {
            Customer customer = null;
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnUpdateAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.UpdateAsync(customer);
            manualEvent.WaitOne(60000);
        }

        [TestMethod()]
        public void UpdateQboTestInvalidCustomer()
        {
            Customer customer = new Customer();
            customer.Id = "00000";
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnUpdateAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.UpdateAsync(customer);
            manualEvent.WaitOne(30000);
        }

        #endregion

        #region CDC tests

        [TestMethod()]
        public void CDCQboTest()
        {
            List<IEntity> entityList = new List<IEntity>();
            entityList.Add(new Customer());

            try
            {
                // Used to signal the waiting test thread that a async operation have completed.    
                ManualResetEvent manualEvent = new ManualResetEvent(false);
                qboService.OnCDCAsyncCompleted = (sender, e) =>
                {
                    Assert.IsNotNull(e);
                    Assert.IsNotNull(e.Entities);
                    List<Customer> custList = e.getEntity("Customer").Cast<Customer>().ToList();
                    Assert.IsTrue(custList.Count > 0);
                    manualEvent.Set();
                };
                qboService.CDCAsync(entityList, DateTime.Today.AddDays(-2));
                manualEvent.WaitOne(30000);
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
        public void VoidQboTest()
        {
            Customer customer = new Customer();
            string guid = Guid.NewGuid().ToString("N");
            customer.GivenName = guid.Substring(0, 25);
            customer.Title = guid.Substring(0, 15);
            customer.MiddleName = guid.Substring(0, 5);
            customer.FamilyName = guid.Substring(0, 25);
            customer.DisplayName = guid.Substring(0, 20);

            try
            {
                Customer result = qboService.Add(customer);
                ManualResetEvent manualEvent = new ManualResetEvent(false);
                qboService.OnDeleteAsyncCompleted = (sender, e) =>
                {
                    Assert.IsNotNull(e);
                    Assert.IsTrue((e.Entity as Intuit.Ipp.Data.Customer).status == Data.EntityStatusEnum.Voided);
                    manualEvent.Set();
                };
                qboService.VoidAsync(result);
                manualEvent.WaitOne(30000);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod()]
        public void VoidQboTestNullEntity()
        {
            Customer customer = null;
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnDeleteAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.VoidAsync(customer);
            manualEvent.WaitOne(30000);
        }

        [TestMethod()]
        public void VoidQboTestInvalidCustomer()
        {
            Customer customer = new Customer();
            customer.Id = "000000";
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnDeleteAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.VoidAsync(customer);
            manualEvent.WaitOne(30000);
        }

        #endregion
        */
        #region delete

       /* /// <summary>
        /// Test delete method.
        /// </summary>
        [TestMethod]
        public void DeleteQboTest()
        {
            Customer customer = new Customer();
            //customer.Id = "NG:2262139";
            string guid = Guid.NewGuid().ToString("N");
            customer.GivenName = guid.Substring(0, 25);
            customer.Title = guid.Substring(0, 15);
            customer.MiddleName = guid.Substring(0, 5);
            customer.FamilyName = guid.Substring(0, 25);
            customer.DisplayName = guid.Substring(0, 20);

            try
            {
                Customer result = qboService.Add(customer);
                ManualResetEvent manualEvent = new ManualResetEvent(false);
                qboService.OnDeleteAsyncCompleted = (sender, e) =>
                {
                    Assert.IsNotNull(e);
                    Assert.IsTrue((e.Entity as Intuit.Ipp.Data.Customer).status == Data.EntityStatusEnum.Deleted);
                    manualEvent.Set();
                };
                qboService.DeleteAsync(result);
                manualEvent.WaitOne(30000);
            }
            catch (System.Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        } */

        [TestMethod()]
        public void DeleteQboTestNullEntity()
        {
            Customer customer = null;
            ManualResetEvent manualEvent = new ManualResetEvent(false);
            qboService.OnDeleteAsyncCompleted = (sender, e) =>
            {
                Assert.IsNotNull(e);
                Assert.IsNotNull(e.Error);
                manualEvent.Set();
            };
            qboService.DeleteAsync(customer);
            manualEvent.WaitOne(30000);
        }

        #endregion
        
    }
}
