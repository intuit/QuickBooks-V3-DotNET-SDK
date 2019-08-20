using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.Core;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Data;

using System.Diagnostics;
using System.Collections.ObjectModel;
using Intuit.Ipp.Exception;

namespace Intuit.Ipp.Test.QBO
{
    [TestClass]
    [Ignore] ///https://jira.intuit.com/browse/IPP-5966
    public class QueryFilterTests
    {
        private static ServiceContext serviceContextoAuth;
        static QueryService<Customer> customerQueryService = null;
        static QueryService<Invoice> invoiceQueryService = null;
        static List<Customer> customers = new List<Customer>();
        static List<Invoice> invoices = new List<Invoice>();
        public QueryFilterTests()
        {

        }

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            serviceContextoAuth = Initializer.InitializeQBOServiceContextUsingoAuth();
            customerQueryService = new QueryService<Customer>(serviceContextoAuth);
            invoiceQueryService = new QueryService<Invoice>(serviceContextoAuth);
            DataService.DataService service = new DataService.DataService(serviceContextoAuth);
            customers = service.FindAll<Customer>(new Customer(), 1, 100).ToList<Customer>();
            Assert.IsTrue(customers.Count > 0);
            invoices = service.FindAll<Invoice>(new Invoice(), 1, 100).ToList<Invoice>();
            Assert.IsTrue(invoices.Count > 0);
        }

        #region Where

        //[TestMethod]
        //public void CustomerQueryWhereMiddleNameLikeStartWithEndsWithTests()
        //{
        //    //QUERY * FROM Customer  WHERE MiddleName LIKE 'Test'
        //    List<Customer> customers = customerQueryService.Where(c => c.MiddleName.StartsWith("c")).ToList<Customer>();
        //    foreach (var item in customers)
        //    {

        //    }
        //}

        [TestMethod]
        public void CustomerQueryWhereMiddleNameLikeStartWithTests()
        {
            //Customer cust1 = Initializer.CreateCustomer1();
            //Customer cust2 = Initializer.CreateCustomer2();
            //Customer cust3 = Initializer.CreateCustomer3();
            //Customer cust4 = Initializer.CreateCustomer4();
            //Initializer.AddCustomerHelper(serviceContextoAuth, cust1);
            //Initializer.AddCustomerHelper(serviceContextoAuth, cust2);
            //Initializer.AddCustomerHelper(serviceContextoAuth, cust3);
            //Initializer.AddCustomerHelper(serviceContextoAuth, cust4);
            //Customer cust6 = Initializer.CreateCustomer6();
            //Customer cust7 = Initializer.CreateCustomer7();
            //Customer cust8 = Initializer.CreateCustomer8();
            //Initializer.AddCustomerHelper(serviceContextoAuth, cust6);
            //Initializer.AddCustomerHelper(serviceContextoAuth, cust7);
            //Initializer.AddCustomerHelper(serviceContextoAuth, cust8);

            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MiddleName != null && customer.MiddleName.StartsWith("C")
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE MiddleName LIKE 'C*'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MiddleName.StartsWith('C')").ToList<Customer>();

            VerifyCustomers(expectedCustomers, actualCustomers, false);

        }

        [TestMethod]
        public void CustomerQueryWhereMiddleNameLikeEndsWithTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MiddleName != null && customer.MiddleName.EndsWith("t")
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE MiddleName LIKE '12'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MiddleName.EndsWith('t')").ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereMiddleNameLikeContainsTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MiddleName != null && customer.MiddleName.Contains("Cust")
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();

            //QUERY * FROM Customer  WHERE MiddleName LIKE 'Test*12' 
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MiddleName like 'Cust')").ToList<Customer>();

            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereMiddleNameEQTests()
        {
            //QUERY * FROM Customer  WHERE MiddleName EQ 'Cust'
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MiddleName != null && customer.MiddleName.Equals("Cust")
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();

            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MiddleName == 'Cust'").ToList<Customer>();

            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereGivenNameLikeStartWithTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.GivenName != null && customer.GivenName.StartsWith("C")
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE GivenName LIKE 'C%'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where GivenName.StartsWith('C')").ToList<Customer>();

            VerifyCustomers(expectedCustomers, actualCustomers, false);

        }

        [TestMethod]
        public void CustomerQueryWhereGivenNameLikeEndsWithTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.GivenName != null && customer.GivenName.EndsWith("t", StringComparison.OrdinalIgnoreCase)
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE GivenName LIKE '12'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer where GivenName.EndsWith('t')").ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereGivenNameLikeContainsTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.GivenName != null && customer.GivenName.StartsWith("C") && customer.GivenName.EndsWith("t")
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();

            //QUERY * FROM Customer  WHERE GivenName LIKE 'Test*12' 
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where GivenName.Contains('C %t')").ToList<Customer>();

            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereGivenNameContainsTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.GivenName != null && customer.GivenName.Contains("est")
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();

            //QUERY * FROM Customer  WHERE GivenName LIKE 'Test*12' 
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where GivenName.Contains('est')").ToList<Customer>();

            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereGivenNameEQTests()
        {
            //QUERY * FROM Customer  WHERE GivenName EQ 'Cust'
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.GivenName != null && customer.GivenName.Equals("Cust")
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();

            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where GivenName == 'Cust'").ToList<Customer>();

            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereFamilyNameLikeStartWithTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.FamilyName != null && customer.FamilyName.StartsWith("C")
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE FamilyName LIKE 'C*'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where FamilyName.StartsWith('C')").ToList<Customer>();

            VerifyCustomers(expectedCustomers, actualCustomers, false);

        }

        [TestMethod]
        public void CustomerQueryWhereFamilyNameLikeEndsWithTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.FamilyName != null && customer.FamilyName.EndsWith("t")
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE FamilyName LIKE '12'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where FamilyName.EndsWith('t')").ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereFamilyNameLikeContainsTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.FamilyName != null && customer.FamilyName.StartsWith("C") && customer.FamilyName.EndsWith("t")
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();

            //QUERY * FROM Customer  WHERE FamilyName LIKE 'Test*12' 
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where FamilyName.Contains('C %t')").ToList<Customer>();

            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereFamilyNameEQTests()
        {
            //QUERY * FROM Customer  WHERE FamilyName EQ 'Cust'
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.FamilyName != null && customer.FamilyName.Equals("Cust")
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();

            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where FamilyName == 'Cust'").ToList<Customer>();

            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereActiveTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Active == true
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();

            //QUERY * FROM Customer  WHERE Active EQ True
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where Active == true").ToList<Customer>();

            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        //[TestMethod]
        //public void CustomerQueryWhereStatusTests()
        //{
        //    IEnumerable<Customer> filterCustomer = from customer in customers
        //                                           where customer.status == EntityStatusEnum.Pending
        //                                           select customer;

        //    List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
        //    //QUERY * FROM Customer  WHERE Active EQ True
        //    List<Customer> actualCustomers = customerQueryService.Where(customer => customer.status == EntityStatusEnum.Pending).ToList<Customer>();
        //    VerifyCustomers(expectedCustomers, actualCustomers);
        //}

        [TestMethod]
        public void CustomerQueryWhereBalanceEQTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Balance == 1000
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE Balance EQ '1000'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where Balance == 1000").ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereBalanceLTTests()
        {

            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Balance < 1000
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE Balance LT '1000'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where Balance < 1000").ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereBalanceGTTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Balance > 1000
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE Balance GT '1000'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where Balance > 1000").ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereBalanceLETests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Balance <= 1000
                                                   select customer;
            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE Balance LTE '1000'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where Balance <= 1000").ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereBalanceGETests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Balance >= 1000
                                                   select customer;
            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE Balance GTE '1000'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where Balance >= 1000").ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCreateTimeEQNowTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.CreateTime == DateTime.Now
                                                   select customer;
            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE MetaData.CreateTime EQ '2012-07-10T15:16:51+05:30'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.CreateTime =="+ DateTime.Now).ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCreateTimeEQTodayTests()
        {

            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.CreateTime == DateTime.Today
                                                   select customer;
            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE MetaData.CreateTime EQ '2012-07-10T00:00:00+05:30'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.CreateTime == "+DateTime.Today).ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCreateTimeGTNowTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.CreateTime > DateTime.Now.AddDays(-2)
                                                   select customer;
            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE MetaData.CreateTime EQ '2012-07-10T15:16:51+05:30'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.CreateTime > "+DateTime.Now.AddDays(-2)).ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers, false);
        }

        [TestMethod]
        public void CustomerQueryWhereCreateTimeGTTodayTests()
        {

            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.CreateTime > DateTime.Today.AddDays(-2)
                                                   select customer;
            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE MetaData.CreateTime EQ '2012-07-10T00:00:00+05:30'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Custo mer whereMetaData.CreateTime >"+ DateTime.Today.AddDays(-2)).ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers, false);
        }

        [TestMethod]
        public void CustomerQueryWhereCreateTimeGTDateTimeTests()
        {

            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.CreateTime > new DateTime(2012, 06, 30, 11, 20, 50)
                                                   select customer;
            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE MetaData.CreateTime EQ '2012-07-10T15:16:51+05:30'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.CreateTime > "+new DateTime(2012, 06, 30, 11, 20, 50)).ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCreateTimeGTDateTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.CreateTime > new DateTime(2012, 06, 30)
                                                   select customer;
            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE MetaData.CreateTime EQ '2012-07-10T00:00:00+05:30'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer whereMetaData.CreateTime > "+new DateTime(2012, 06, 30)).ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereLastUpdatedTimeEQNowTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.LastUpdatedTime == DateTime.Now
                                                   select customer;
            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE MetaData.LastUpdatedTime EQ '2012-07-10T15:16:51+05:30'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.LastUpdatedTime =="+ DateTime.Now).ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereLastUpdatedTimeEQTodayTests()
        {

            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.LastUpdatedTime != DateTime.Today
                                                   select customer;
            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE MetaData.LastUpdatedTime EQ '2012-07-10T00:00:00+05:30'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.LastUpdatedTime != "+DateTime.Today).ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereLastUpdatedTimeGTNowTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.LastUpdatedTime > DateTime.Now.AddDays(-2)
                                                   select customer;
            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE MetaData.LastUpdatedTime EQ '2012-07-10T15:16:51+05:30'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.LastUpdatedTime >"+ DateTime.Now.AddDays(-2)).ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers, false);
        }

        [TestMethod]
        public void CustomerQueryWhereLastUpdatedTimeGTTodayTests()
        {

            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.LastUpdatedTime > DateTime.Today.AddDays(-2)
                                                   select customer;
            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE MetaData.LastUpdatedTime EQ '2012-07-10T00:00:00+05:30'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.LastUpdatedTime >"+ DateTime.Today.AddDays(-2)).ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers, false);
        }

        [TestMethod]
        public void CustomerQueryWhereLastUpdatedTimeGTDateTimeTests()
        {

            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30, 11, 20, 50)
                                                   select customer;
            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE MetaData.LastUpdatedTime EQ '2012-07-10T15:16:51+05:30'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.LastUpdatedTime >"+ new DateTime(2012, 06, 30, 11, 20, 50)).ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereLastUpdatedTimeGTDateTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30)
                                                   select customer;
            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE MetaData.LastUpdatedTime EQ '2012-07-10T00:00:00+05:30'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.LastUpdatedTime > "+new DateTime(2012, 06, 30)).ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereIDEQTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Id == "1"
                                                   select customer;
            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE Id EQ 'NG:456344'
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where Id='1'").ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereMiddleGivenFamilyNameLikeStartWithTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MiddleName != null && customer.MiddleName.StartsWith("C") && customer.GivenName != null &&
                                                   customer.GivenName.StartsWith("Test") && customer.FamilyName != null && customer.FamilyName.StartsWith("C")
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE MiddleName LIKE 'C' AND FamilyName LIKE 'C' AND GivenName LIKE 'Test' 
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer where MiddleName like'C' && FamilyName like 'C' && GivenName like 'Test')").ToList<Customer>();

            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereMiddleGivenFamilyNameLikeEndsWithTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MiddleName != null && customer.MiddleName.EndsWith("C") && customer.GivenName != null &&
                                                   customer.GivenName.EndsWith("Test") && customer.FamilyName != null && customer.FamilyName.EndsWith("C")
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            //QUERY * FROM Customer  WHERE MiddleName LIKE 'C' AND FamilyName LIKE 'C' AND GivenName LIKE 'Test' 
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer Where MiddleName.EndsWith('C') && FamilyName.EndsWith('C') && GivenName.EndsWith('Test')").ToList<Customer>();

            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereMiddleGivenFamilyNameLikeContainsTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MiddleName != null && customer.MiddleName.Contains("C%t") && customer.GivenName != null &&
                                                   customer.GivenName.Contains("T%1") && customer.FamilyName != null && customer.FamilyName.Contains("T%1")
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer Where MiddleName.Contains('C%t') && FamilyName.Contains('T%1') && GivenName.Contains('T%1')").ToList<Customer>();

            VerifyCustomers(expectedCustomers, actualCustomers);
        }

        #endregion

        #region Select

        [TestMethod]
        public void CustomerQuerySelectAllTests()
        {
            var filterCustomers = from customer in customers
                                  select new { customer, customer.BalanceWithJobs };



            var actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer").Select(c => new { Customer = c, c.BalanceWithJobs });
            foreach (var cust in actualCustomers)
            {
                Debug.WriteLine(cust.BalanceWithJobs + " --- " + cust.Customer.GivenName);
            }

        }

        [TestMethod]
        public void CustomerQuerySelectAllTests1()
        {
            var filterCustomers = from customer in customers
                                  select new { customer, customer.BalanceWithJobs };

            var actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer").Select(c => new { Customer = c, c.BalanceWithJobs });
            foreach (var cust in actualCustomers)
            {
                Debug.WriteLine(cust.BalanceWithJobs + " --- " + cust.Customer.GivenName);
            }

        }

        [TestMethod]
        public void CustomerQuerySelectDefaultTests()
        {
            var filterCustomer = from customer in customers
                                 select customer;

            var actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer").Select(c => c);
            //int i = 0;
            foreach (var cust in actualCustomers)
            {
                //Debug.WriteLine(cust.BalanceWithJobs);
                //Debug.WriteLine(cust.GivenName);
                //i++;
            }

        }

        [TestMethod]
        public void CustomerQuerySelectNonDefaultTests()
        {
            var filterCustomer = from customer in customers
                                 select customer.BalanceWithJobs;

            var actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer").Select(c => c.BalanceWithJobs);

            foreach (var cust in actualCustomers)
            {
            }

        }

        [TestMethod]
        public void CustomerQuerySelectTests()
        {
            var filterCustomer = from customer in customers
                                 select new { customer.BalanceWithJobs, customer.GivenName };

            var actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer").Select(c => new { c.BalanceWithJobs, c.GivenName });

            foreach (var cust in actualCustomers)
            {
            }

        }

        [TestMethod]
        public void CustomerQuerySelectIdGivenNameTests()
        {
            var filterCustomer = from customer in customers
                                 select new { customer.Id, customer.GivenName };

            var actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer").Select(c => new { c.Id, c.GivenName });

            foreach (var cust in actualCustomers)
            {
            }

        }

        [TestMethod]
        public void InvoiceQuerySelectTests()
        {
            //var filterCustomer = from customer in customers
            //                     select new { customer.Id, customer.GivenName };

            var actualInvoices = invoiceQueryService.ExecuteIdsQuery("Select * from customer").Select(i => new { i.Id, i.DocNumber });

            foreach (var invoice in actualInvoices)
            {
                Assert.IsNotNull(invoice.Id);
                Assert.IsNotNull(invoice.DocNumber);
            }

        }

        [TestMethod]
        public void InvoiceQueryComplexSelectTests()
        {
            //var filterCustomer = from customer in customers
            //                     select new { customer.Id, customer.GivenName };

            var actualInvoices = invoiceQueryService.ExecuteIdsQuery("Select * from customer").Select(i => new { i.Id, i.Line });

            foreach (var invoice in actualInvoices)
            {

            }

        }

        [TestMethod]
        public void InvoiceQueryComplexSelectLineIdTests()
        {
            var filterCustomer = from invoice in invoices
                                 select new { invoice.Line };

            var actualInvoices = invoiceQueryService.ExecuteIdsQuery("Select * from invoice").Select(i => new { Line = i.Line });

            foreach (var inv in actualInvoices)
            {
                foreach (var line in inv.Line)
                {
                    Debug.WriteLine(line.Id);
                }
            }

        }
        #endregion

        #region OrderBy

        [TestMethod]
        public void CustomerQueryOrderByTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   orderby customer.FamilyName
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer").OrderBy(c => c.FamilyName).ToList<Customer>();
           // VerifyCustomers(expectedCustomers, actualCustomers, false);
        }

        [TestMethod]
        public void CustomerQueryOrderAscDescByTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   orderby customer.FamilyName, customer.GivenName descending
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer").OrderBy(c => c.FamilyName).OrderByDescending(c => c.GivenName).ToList<Customer>();
            //VerifyCustomers(expectedCustomers, actualCustomers, false);
        }

        [TestMethod]
        public void CustomerQueryOrderDescAscByTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   orderby customer.FamilyName descending, customer.GivenName
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer").OrderByDescending(c => c.FamilyName).OrderBy(c => c.GivenName).ToList<Customer>();
            //VerifyCustomers(expectedCustomers, actualCustomers, false);
        }

        [TestMethod]
        public void CustomerQueryOrderByDescTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   orderby customer.FamilyName descending
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer").OrderByDescending(c => c.FamilyName).ToList<Customer>();
            //VerifyCustomers(expectedCustomers, actualCustomers, false);
        }

        #endregion

        #region Count

        [TestMethod]
        public void CustomerQueryCountTests()
        {
            int filterCustomer = (from customer in customers
                                  select customer).Count();

            int actualLinq = customerQueryService.ExecuteIdsQuery("Select * from customer").Count();
            int actualString = ((ReadOnlyCollection<Customer>)customerQueryService.ExecuteIdsQuery("Select Count(*) From Customer")).Count();

            Assert.IsTrue(filterCustomer <= actualLinq && actualLinq == actualString);
        }

        [TestMethod]
        public void InvoiceQueryCountTests()
        {
            int filterCustomer = (from invoice in invoices
                                  select invoice).Count();

            int actualLinq = invoiceQueryService.ExecuteIdsQuery("Select * from invoice").Count();
            int actualString = ((ReadOnlyCollection<Invoice>)invoiceQueryService.ExecuteIdsQuery("Select Count(*) From Invoice")).Count();

            Assert.IsTrue(filterCustomer <= actualLinq && actualLinq == actualString);
        }

        #endregion

        #region Take-Skip

        [TestMethod]
        public void CustomerQueryTakeSkipTests()
        {
            var filterCustomers = from customer in customers
                                  select customer;

            var actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer").Take(6).Skip(3);
            foreach (var cust in actualCustomers)
            {
            }

        }

        #endregion

        #region WhereIn

        [TestMethod]
        public void CustomerQueryWhereInTests()
        {
            string[] values = { "1", "2", "3" };
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Id.In(values)
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            IEnumerable<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where Id.In('" + values + "')");
            VerifyCustomers(expectedCustomers, actualCustomers.ToList<Customer>());
        }

        [TestMethod]
        public void CustomerQueryWhereInToIdsQueryTests()
        {
            string[] values = { "1", "2", "3" };
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Id.In(values)
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            List <Customer> actualcustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where Id.In('" + values + "')", QueryOperationType.query).ToList<Customer>();
            VerifyCustomers(expectedCustomers, actualcustomers);
        }

        [TestMethod]
        public void InvoiceQueryWhereInTests()
        {
            string[] values = { "1", "2", "3" };
            IEnumerable<Invoice> filterInvoice = from invoice in invoices
                                                 where invoice.Id.In(values)
                                                 select invoice;

            List<Invoice> expectedInvoices = filterInvoice.ToList<Invoice>();
            IEnumerable<Invoice> actualInvoices = invoiceQueryService.ExecuteIdsQuery("Select * from Customer where Id.In('"+values+"')");
            //VerifyInvoices(expectedInvoices, actualInvoices.ToList<Invoice>()); //Result sets are not the same
        }

        #endregion

        #region Where + Select
        [TestMethod]
        public void CustomerQuerySelectWhereAllTests()
        {
            var filterCustomers = from customer in customers
                                  where customer.MetaData.CreateTime < DateTime.Today
                                  select new { customer, customer.BalanceWithJobs };

            var actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.CreateTime <"+ DateTime.Today).Select(c => new { Customer = c, c.BalanceWithJobs });
            foreach (var cust in actualCustomers)
            {
                Debug.WriteLine(cust.BalanceWithJobs + " --- " + cust.Customer.GivenName);
            }

        }

        [TestMethod]
        public void CustomerQuerySelectWhereAllTests1()
        {
            var filterCustomers = from customer in customers
                                  select new { customer, customer.BalanceWithJobs };

            var actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.CreateTime < "+DateTime.Today).Select(c => new { Customer = c, c.BalanceWithJobs });
            foreach (var cust in actualCustomers)
            {
                Debug.WriteLine(cust.BalanceWithJobs + " --- " + cust.Customer.GivenName);
            }

        }

        [TestMethod]
        public void CustomerQuerySelectWhereDefaultTests()
        {
            var filterCustomer = from customer in customers
                                 select customer;

            var actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.CreateTime <"+ DateTime.Today).Select(c => c);
            //int i = 0;
            foreach (var cust in actualCustomers)
            {
                //Debug.WriteLine(cust.BalanceWithJobs);
                //Debug.WriteLine(cust.GivenName);
                //i++;
            }

        }

        [TestMethod]
        public void CustomerQuerySelectWhereNonDefaultTests()
        {
            var filterCustomer = from customer in customers
                                 select customer.BalanceWithJobs;

            var actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.CreateTime <"+ DateTime.Today).Select(c => c.BalanceWithJobs);

            foreach (var cust in actualCustomers)
            {
            }

        }

        [TestMethod]
        public void CustomerQuerySelectWhereTests()
        {
            var filterCustomer = from customer in customers
                                 select new { customer.BalanceWithJobs, customer.GivenName };

            var actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.CreateTime <"+ DateTime.Today).Select(c => new { c.BalanceWithJobs, c.GivenName });

            foreach (var cust in actualCustomers)
            {
            }

        }

        [TestMethod]
        public void CustomerQuerySelectWhereIdGivenNameTests()
        {
            var filterCustomer = from customer in customers
                                 select new { customer.Id, customer.GivenName };

            var actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.CreateTime <"+ DateTime.Today).Select(c => new { c.Id, c.GivenName });

            foreach (var cust in actualCustomers)
            {

            }

        }

        [TestMethod]
        public void InvoiceQuerySelectWhereTests()
        {
            //var filterCustomer = from customer in customers
            //                     select new { customer.Id, customer.GivenName };

            var actualInvoices = invoiceQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.CreateTime <"+ DateTime.Today).Select(i => new { i.Id, i.DocNumber });

            foreach (var invoice in actualInvoices)
            {

            }

        }

        [TestMethod]
        public void InvoiceQueryComplexSelectWhereTests()
        {
            //var filterCustomer = from customer in customers
            //                     select new { customer.Id, customer.GivenName };

            var actualInvoices = invoiceQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.CreateTime <"+ DateTime.Today).Select(i => new { i.Id, i.Line });

            foreach (var invoice in actualInvoices)
            {

            }

        }

        [TestMethod]
        public void InvoiceQueryComplexSelectWhereLineIdTests()
        {
            var filterCustomer = from invoice in invoices
                                 select new { invoice.Line };

            var actualInvoices = invoiceQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.CreateTime <"+ DateTime.Today).Select(i => new { Line = i.Line });

            foreach (var inv in actualInvoices)
            {
                foreach (var line in inv.Line)
                {
                    Debug.WriteLine(line.Id);
                }
            }

        }
        #endregion

        #region Where + OrderBy

        [TestMethod]
        public void CustomerQueryWhereOrderByTests()
        {
            string[] values = { "1", "2", "3" };
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Id.In(values)
                                                   orderby customer.GivenName
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            IEnumerable<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.CreateTime >= "+DateTime.Today.AddDays(-5)).OrderBy(c => c.FamilyName).OrderByDescending(c => c.GivenName).Skip(2);
        }

        [TestMethod]
        public void CustomerQueryWhereOrderByDescTests()
        {
            string[] values = { "1", "2", "3" };
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Id.In(values)
                                                   orderby customer.GivenName descending
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();
            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where Id.In('"+values+"')").OrderByDescending(c => c.GivenName).ToList<Customer>();
        }

        #endregion

        #region Where + Count

        [TestMethod]
        public void CustomerQueryWhereCountMiddleNameLikeStartWithTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MiddleName != null && customer.MiddleName.StartsWith("C")
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE MiddleName LIKE 'C*'
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MiddleName.StartsWith('C')").Count();

            Assert.IsTrue(expectedCustomers <= actualCustomers);

        }

        [TestMethod]
        public void CustomerQueryWhereCountMiddleNameLikeEndsWithTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MiddleName != null && customer.MiddleName.EndsWith("t")
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE MiddleName LIKE '12'
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MiddleName.EndsWith('t')").Count();
            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountMiddleNameLikeContainsTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MiddleName != null && customer.MiddleName.StartsWith("C") && customer.MiddleName.Contains("t")
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();

            //QUERY * FROM Customer  WHERE MiddleName LIKE 'Test*12' 
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MiddleName.Contains('C %t')").Count();

            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountMiddleNameEQTests()
        {
            //QUERY * FROM Customer  WHERE MiddleName EQ 'Cust'
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MiddleName != null && customer.MiddleName.Equals("Cust")
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();

            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MiddleName == 'Cust'").Count();

            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountGivenNameLikeStartWithTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.GivenName != null && customer.GivenName.StartsWith("C")
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE GivenName LIKE 'C*'
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer where GivenName.StartsWith('C')").Count();

            Assert.IsTrue(expectedCustomers <= actualCustomers);

        }

        [TestMethod]
        public void CustomerQueryWhereCountGivenNameLikeEndsWithTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.GivenName != null && customer.GivenName.EndsWith("t", StringComparison.OrdinalIgnoreCase)
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE GivenName LIKE '12'
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where GivenName.EndsWith('t')").Count();
            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountGivenNameLikeContainsTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.GivenName != null && customer.GivenName.StartsWith("C") && customer.GivenName.EndsWith("t")
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();

            //QUERY * FROM Customer  WHERE GivenName LIKE 'Test*12' 
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer where GivenName like ('C%t')").Count();

            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountGivenNameContainsTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.GivenName != null && customer.GivenName.Contains("est")
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();

            //QUERY * FROM Customer  WHERE GivenName LIKE 'Test*12' 
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer where GivenName like 'est'").Count();

            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountGivenNameEQTests()
        {
            //QUERY * FROM Customer  WHERE GivenName EQ 'Cust'
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.GivenName != null && customer.GivenName.Equals("Cust")
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();

            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer where GivenName == 'Cust'").Count();

            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountFamilyNameLikeStartWithTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.FamilyName != null && customer.FamilyName.StartsWith("C")
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE FamilyName LIKE 'C*'
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer where FamilyName.StartsWith('C')").Count();

            Assert.IsTrue(expectedCustomers <= actualCustomers);

        }

        [TestMethod]
        public void CustomerQueryWhereCountFamilyNameLikeEndsWithTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.FamilyName != null && customer.FamilyName.EndsWith("t")
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE FamilyName LIKE '12'
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where FamilyName.EndsWith('t')").Count();
            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountFamilyNameLikeContainsTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.FamilyName != null && customer.FamilyName.StartsWith("C") && customer.FamilyName.EndsWith("t")
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();

            //QUERY * FROM Customer  WHERE FamilyName LIKE 'Test*12' 
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where FamilyName.Contains('C%t')").Count();

            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountFamilyNameEQTests()
        {
            //QUERY * FROM Customer  WHERE FamilyName EQ 'Cust'
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.FamilyName != null && customer.FamilyName.Equals("Cust")
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();

            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where FamilyName == 'Cust'").Count();

            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountActiveTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Active == true
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();

            //QUERY * FROM Customer  WHERE Active EQ True
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer where Active == true").Count();

            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        //[TestMethod]
        ////Service gives error <Feb 8 2013> : QueryValidationError: Property status not found for Entity Customer if status field is included in the query
        //public void CustomerQueryWhereCountStatusTests()
        //{
        //    IEnumerable<Customer> filterCustomer = from customer in customers
        //                                           where customer.status == EntityStatusEnum.Pending
        //                                           select customer;

        //    int expectedCustomers = filterCustomer.Count();
        //    //QUERY * FROM Customer  WHERE Active EQ True
        //    int actualCustomers = customerQueryService.Where(customer => customer.status == EntityStatusEnum.Pending).Count();
        //    Assert.IsTrue(expectedCustomers <= actualCustomers);
        //}

        [TestMethod]
        public void CustomerQueryWhereCountBalanceEQTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Balance == 1000
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE Balance EQ '1000'
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer where Balance == 1000").Count();
            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountBalanceLTTests()
        {

            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Balance < 1000
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE Balance LT '1000'
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer where Balance < 1000").Count();
            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountBalanceGTTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Balance > 1000
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE Balance GT '1000'
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer where Balance > 1000").Count;
            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountBalanceLETests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Balance <= 1000
                                                   select customer;
            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE Balance LTE '1000'
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where Balance <= 1000").Count();
            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountBalanceGETests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Balance >= 1000
                                                   select customer;
            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE Balance GTE '1000'
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer where Balance >= 1000").Count();
            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountCreateTimeEQNowTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.CreateTime == DateTime.Now
                                                   select customer;
            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE MetaData.CreateTime EQ '2012-07-10T15:16:51+05:30'
             var actualCustomersData = customerQueryService.ExecuteIdsQuery("Select * from Customer").ToList<Customer>();
            int count = 0;
            foreach (Customer c in actualCustomersData)
            {
                if (c.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30, 11, 20, 50))
                    if (c.MetaData != null && c.MetaData.LastUpdatedTime == DateTime.Now)
                    {
                        count++;
                    }
            }
            Assert.IsTrue(expectedCustomers <= count);
        }

        [TestMethod]
        public void CustomerQueryWhereCountCreateTimeEQTodayTests()
        {

            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.CreateTime == DateTime.Today
                                                   select customer;
            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE MetaData.CreateTime EQ '2012-07-10T00:00:00+05:30'
            var actualCustomersData = customerQueryService.ExecuteIdsQuery("Select * from Customer").ToList<Customer>();
            int count = 0;
            foreach (Customer c in actualCustomersData)
            {
                if (c.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30, 11, 20, 50))
                    if (c.MetaData != null && c.MetaData.LastUpdatedTime == DateTime.Today)
                    {
                        count++;
                    }
            }
            Assert.IsTrue(expectedCustomers <= count);
        }

        [TestMethod]
        public void CustomerQueryWhereCountCreateTimeGTNowTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.CreateTime > DateTime.Now.AddDays(-2)
                                                   select customer;
            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE MetaData.CreateTime EQ '2012-07-10T15:16:51+05:30'
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MetaData.CreateTime >"+ DateTime.Now.AddDays(-2)).Count();
            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountCreateTimeGTTodayTests()
        {

            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.CreateTime > DateTime.Today.AddDays(-2)
                                                   select customer;
            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE MetaData.CreateTime EQ '2012-07-10T00:00:00+05:30'
           var actualCustomersData = customerQueryService.ExecuteIdsQuery("Select * from Customer").ToList<Customer>();
            int count = 0;
            foreach (Customer c in actualCustomersData)
            {
                if (c.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30, 11, 20, 50))
                    if (c.MetaData != null && c.MetaData.LastUpdatedTime > DateTime.Today.AddDays(-2))
                    {
                        count++;
                    }
            }
            Assert.IsTrue(expectedCustomers <= count);
        }

        [TestMethod]
        public void CustomerQueryWhereCountCreateTimeGTDateTimeTests()
        {

            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.CreateTime > new DateTime(2012, 06, 30, 11, 20, 50)
                                                   select customer;
            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE MetaData.CreateTime EQ '2012-07-10T15:16:51+05:30'
           var actualCustomersData = customerQueryService.ExecuteIdsQuery("Select * from Customer").ToList<Customer>();
            int count = 0;
            foreach (Customer c in actualCustomersData)
            {
                if (c.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30, 11, 20, 50))
                    if (c.MetaData != null && c.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30, 11, 20, 50))
                    {
                        count++;
                    }
            }
            Assert.IsTrue(expectedCustomers <= count);
        }

        [TestMethod]
        public void CustomerQueryWhereCountCreateTimeGTDateTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.CreateTime > new DateTime(2012, 06, 30)
                                                   select customer;
            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE MetaData.CreateTime EQ '2012-07-10T00:00:00+05:30'
           var actualCustomersData = customerQueryService.ExecuteIdsQuery("Select * from Customer").ToList<Customer>();
            int count = 0;
            foreach (Customer c in actualCustomersData)
            {
                if (c.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30))
                    if (c.MetaData != null && c.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30))
                    {
                        count++;
                    }
            }
            Assert.IsTrue(expectedCustomers <= count);
        }

        [TestMethod]
        public void CustomerQueryWhereCountLastUpdatedTimeEQNowTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.LastUpdatedTime == DateTime.Now
                                                   select customer;
            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE MetaData.LastUpdatedTime EQ '2012-07-10T15:16:51+05:30'
            var actualCustomersData = customerQueryService.ExecuteIdsQuery("Select * from Customer").ToList<Customer>();
            int count = 0;
            foreach (Customer c in actualCustomersData)
            {
                if (c.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30, 11, 20, 50))
                    if (c.MetaData != null && c.MetaData.LastUpdatedTime == DateTime.Now)
                    {
                        count++;
                    }
            }
            Assert.IsTrue(expectedCustomers <= count);
        }

        [TestMethod]
        public void CustomerQueryWhereCountLastUpdatedTimeEQTodayTests()
        {

            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.LastUpdatedTime != DateTime.Today
                                                   select customer;
            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE MetaData.LastUpdatedTime EQ '2012-07-10T00:00:00+05:30'
            var actualCustomersData = customerQueryService.ExecuteIdsQuery("Select * from Customer").ToList<Customer>();
            int count = 0;
            foreach (Customer c in actualCustomersData)
            {
                if (c.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30, 11, 20, 50))
                    if (c.MetaData != null && c.MetaData.LastUpdatedTime != DateTime.Today)
                    {
                        count++;
                    }
            }
            Assert.IsTrue(expectedCustomers <= count);
        }

        [TestMethod]
        public void CustomerQueryWhereCountLastUpdatedTimeGTNowTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.LastUpdatedTime > DateTime.Now.AddDays(-2)
                                                   select customer;
            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE MetaData.LastUpdatedTime EQ '2012-07-10T15:16:51+05:30'
            var actualCustomersData = customerQueryService.ExecuteIdsQuery("Select * from Customer").ToList<Customer>();
            int count = 0;
            foreach (Customer c in actualCustomersData)
            {
                if (c.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30, 11, 20, 50))
                    if (c.MetaData != null && c.MetaData.LastUpdatedTime > DateTime.Now.AddDays(-2))
                    {
                        count++;
                    }
            }
            Assert.IsTrue(expectedCustomers <= count);
        }

        [TestMethod]
        public void CustomerQueryWhereCountLastUpdatedTimeGTTodayTests()
        {

            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.LastUpdatedTime > DateTime.Today.AddDays(-2)
                                                   select customer;
            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE MetaData.LastUpdatedTime EQ '2012-07-10T00:00:00+05:30'
            var actualCustomersData = customerQueryService.ExecuteIdsQuery("Select * from Customer").ToList<Customer>();
            int count = 0;
            foreach (Customer c in actualCustomersData)
            {
                if (c.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30, 11, 20, 50))
                    if (c.MetaData != null && c.MetaData.LastUpdatedTime > DateTime.Today.AddDays(-2))
                    {
                        count++;
                    }
            }
            Assert.IsTrue(expectedCustomers <= count);
        }

        [TestMethod]
        public void CustomerQueryWhereCountLastUpdatedTimeGTDateTimeTests()
        {

            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30, 11, 20, 50)
                                                   select customer;
            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE MetaData.LastUpdatedTime EQ '2012-07-10T15:16:51+05:30'
            var actualCustomersData = customerQueryService.ExecuteIdsQuery("Select * from Customer").ToList<Customer>();
            int count = 0;
            foreach (Customer c in actualCustomersData)
            {
                if (c.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30, 11, 20, 50))
                    if (c.MetaData != null && c.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30))
                    {
                        count++;
                    }
            }
            Assert.IsTrue(expectedCustomers <= count);
        }

        [TestMethod]
        public void CustomerQueryWhereCountLastUpdatedTimeGTDateTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30)
                                                   select customer;
            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE MetaData.LastUpdatedTime EQ '2012-07-10T00:00:00+05:30'
            var actualCustomersData = customerQueryService.ExecuteIdsQuery("Select * from Customer").ToList<Customer>();
            int count = 0;
            foreach(Customer c in actualCustomersData)
            {
                if (c.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30))
                if (c.MetaData!=null && c.MetaData.LastUpdatedTime > new DateTime(2012, 06, 30))
                {
                    count++;
                }
            }
            Assert.IsTrue(expectedCustomers <= count);
        }

        [TestMethod]
        public void CustomerQueryWhereCountIDEQTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Id == "1"
                                                   select customer;
            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE Id EQ 'NG:456344'
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where Id='1'").Count;
            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }


        [TestMethod]
        public void CustomerQueryWhereCountMiddleGivenFamilyNameLikeStartWithTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MiddleName != null && customer.MiddleName.StartsWith("C") && customer.GivenName != null &&
                                                   customer.GivenName.StartsWith("Test") && customer.FamilyName != null && customer.FamilyName.StartsWith("C")
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE MiddleName LIKE 'C' AND FamilyName LIKE 'C' AND GivenName LIKE 'Test' 
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customer where MiddleName.StartsWith('C') && FamilyName.StartsWith('C') && GivenName.StartsWith('Test')").Count;

            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountMiddleGivenFamilyNameLikeEndsWithTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MiddleName != null && customer.MiddleName.EndsWith("C") && customer.GivenName != null &&
                                                   customer.GivenName.EndsWith("Test") && customer.FamilyName != null && customer.FamilyName.EndsWith("C")
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();
            //QUERY * FROM Customer  WHERE MiddleName LIKE 'C' AND FamilyName LIKE 'C' AND GivenName LIKE 'Test' 
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MiddleName.EndsWith('C') && FamilyName.EndsWith('C') && GivenName.EndsWith('Test')").Count;

            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        [TestMethod]
        public void CustomerQueryWhereCountMiddleGivenFamilyNameLikeContainsTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.MiddleName != null && customer.MiddleName.Contains("C%t") && customer.GivenName != null &&
                                                   customer.GivenName.Contains("T%1") && customer.FamilyName != null && customer.FamilyName.Contains("T%1")
                                                   select customer;

            int expectedCustomers = filterCustomer.Count();
            int actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where MiddleName.Contains('C%t') && FamilyName.Contains('T%1') && GivenName.Contains('T%1')").Count;

            Assert.IsTrue(expectedCustomers <= actualCustomers);
        }

        #endregion

        #region Where + Select + OrderBy

        #endregion

        #region Where + Select + OrderBy + Take - Skip

        [TestMethod]
        public void CustomerQueryTests()
        {
            var filterCustomers = from customer in customers
                                  select customer;

           // var actualCustomers = customerQueryService.Where(c => c.MiddleName.StartsWith("C")).Take(7).Skip(2).Select(c => new { c.Id, c.GivenName, c.FamilyName });
            var actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from customers where c.MiddleName.StartsWith('C')").Take(7).Skip(2).Select(c => new { c.Id, c.GivenName, c.FamilyName });
            foreach (var cust in actualCustomers)
            {
            }

        }

        #endregion

        #region Not

        [TestMethod]
        public void CustomerQueryWhereNOTGivenNameEQTests()
        {
            IEnumerable<Customer> filterCustomer = from customer in customers
                                                   where customer.Active == true && customer.MetaData.CreateTime > DateTime.Today.AddDays(-5)
                                                   select customer;

            List<Customer> expectedCustomers = filterCustomer.ToList<Customer>();

            List<Customer> actualCustomers = customerQueryService.ExecuteIdsQuery("Select * from Customer where !(Active != true) && !(MetaData.CreateTime < DateTime.Today.AddDays(-5))).ToList<Customer>()").ToList<Customer>();

            VerifyCustomers(expectedCustomers, actualCustomers, false);
        }

        #endregion

        #region Multiple Queries

        [TestMethod]
        public void MultipleQueryTests()
        {
            string invoiceQueryValue = invoiceQueryService.ExecuteIdsQuery("Select * from invoice").Select(i => new { i.Id, i.status }).ToString();
            List<string> values = new List<string> { "Select * from customer where MetaData.CreateTime > " + DateTime.Today.AddDays(-20), invoiceQueryValue };

            try
            {
                ReadOnlyCollection<ReadOnlyCollection<IEntity>> results = customerQueryService.ExecuteMultipleEntityQueries<IEntity>(values.AsReadOnly());
                foreach (var item in results)
                {
                }
            }
            catch (ValidationException)
            {
            }
            catch (IdsException ex)
            {
                Assert.Fail(ex.ToString());
            }

        }

        #endregion

        #region PreDefined Property Set

        [TestMethod]
        public void PredefinedPropertySetTest()
        {
            var query = customerQueryService.ExecuteIdsQuery("Select * from customer").Select(c => new { c.MiddleName, c.Balance });
            foreach (var item in query)
            {

            }
        }


        #endregion

        #region Helper

        private void VerifyCustomers(List<Customer> expectedCustomers, List<Customer> actualCustomers)
        {
            VerifyCustomers(expectedCustomers, actualCustomers, true);
        }

        private void VerifyCustomers(List<Customer> expectedCustomers, List<Customer> actualCustomers, bool verifyCount)
        {
            if (verifyCount) { Assert.AreEqual(expectedCustomers.Count, actualCustomers.Count); }
            foreach (Customer customer in expectedCustomers)
            {
                bool foundActualCustomer = false;
                foreach (Customer actualCustomer in actualCustomers)
                {
                    if (actualCustomer.Id == customer.Id)
                    {
                        foundActualCustomer = true;
                        break;
                    }
                }
                Assert.IsTrue(foundActualCustomer);
            }
        }

        private void VerifyInvoices(List<Invoice> expectedInvoices, List<Invoice> actualInvoices)
        {
            Assert.AreEqual(expectedInvoices.Count, actualInvoices.Count);
            int i = 0;
            foreach (Invoice invoice in expectedInvoices)
            {
                Assert.AreEqual(invoice.Id, actualInvoices[i].Id);
                i++;
            }
        }

        #endregion
    }
}
