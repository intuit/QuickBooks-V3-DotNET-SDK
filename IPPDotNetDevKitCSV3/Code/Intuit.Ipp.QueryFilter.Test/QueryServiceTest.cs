using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.Exception;
//using Intuit.Ipp.LinqExtender;
using Intuit.Ipp.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.ObjectModel;
using Intuit.Ipp.DataService;
using Intuit.Ipp.QueryFilter.Test.Common;

namespace Intuit.Ipp.QueryFilter.Test
{
    /// <summary>
    ///This is a test class for IppContextTest and is intended
    ///to contain all IppContextTest Unit Tests
    ///</summary>
    
    [TestClass()]
    public class QueryServiceTest
    {
        private TestContext testContextInstance;
        private ServiceContext serviceContext;
        private DateTime dateTime;
        private IDataService service;
        QueryService<Customer> customerContext;
        QueryService<Invoice> invoiceContext;
        QueryService<Account> accountContext;

        public QueryServiceTest()
        {
            dateTime = new DateTime(2012, 07, 10);
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();
            customerContext = new QueryService<Customer>(serviceContext);
            invoiceContext = new QueryService<Invoice>(serviceContext);
            accountContext = new QueryService<Account>(serviceContext);
            service = new DataService.DataService(serviceContext);
        }

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

        [TestMethod]
        [Obsolete]
        [Ignore]
        public void PredefinedPropertySetTest()
        {
            //QueryService<Customer> customerService = new QueryService<Customer>(serviceContext);
            //var query = customerContext.Select(c => new { c.GivenName, c.Id });
            //foreach (var item in query)
            //{

            //}
        }

        # region AccountQuery

        [TestMethod]
        public void AccountQueryForAccountType()
        {
            List<Account> accounts = service.FindAll<Account>(new Account(), 1, 100).ToList<Account>();
            Assert.IsTrue(accounts.Count > 0);

        }

        #endregion


        #region ExecuteMultipleEntityQueries

        [TestMethod]
        [Obsolete][Ignore]
        public void ExecuteMultipleEntityQueriesTest()
        {

            //Assert.Inconclusive("not supported by the service");
            //string customerQueryValue = customerContext.Where(c => c.MetaData.CreateTime > this.dateTime).ToIdsQuery();
            //string invoiceQueryValue = invoiceContext.Select(i => new { i.Id, i.status }).ToIdsQuery();
            //List<string> values = new List<string> { customerQueryValue, invoiceQueryValue };
            //try
            //{
            //    ReadOnlyCollection<ReadOnlyCollection<IEntity>> results = customerContext.ExecuteMultipleEntityQueries<IEntity>(values.AsReadOnly());
            //    foreach (var item in results)
            //    {

            //    }
            //}
            //catch (ValidationException)
            //{
            //}
            //catch (IdsException ex)
            //{
            //    Assert.Fail(ex.ToString());
            //}
        }

        [TestMethod]
        [Obsolete]
        [Ignore]
        [ExpectedException(typeof(InvalidParameterException))]
        public void ExecuteMultipleEntityQueriesNullParameterTest()
        {
            ReadOnlyCollection<ReadOnlyCollection<IEntity>> results = customerContext.ExecuteMultipleEntityQueries<IEntity>(null);
        }

        [TestMethod]
        [Obsolete]
        [Ignore]
        [ExpectedException(typeof(InvalidParameterException))]
        public void ExecuteMultipleEntityQueriesParameterCountZeroTest()
        {
            List<string> values = new List<string>();
            ReadOnlyCollection<ReadOnlyCollection<IEntity>> results = customerContext.ExecuteMultipleEntityQueries<IEntity>(values.AsReadOnly());
        }

        [TestMethod]
        [Obsolete]
        [Ignore]
        [ExpectedException(typeof(InvalidParameterException))]
        public void ExecuteMultipleEntityQueriesParameterCountGreaterThanFiveTest()
        {
            string customerQueryValue = "select * FROM Customer";
            string invoiceQueryValue = "select * FROM Invoice";
            List<string> values = new List<string> { customerQueryValue, invoiceQueryValue, customerQueryValue, invoiceQueryValue, customerQueryValue, invoiceQueryValue };
            ReadOnlyCollection<ReadOnlyCollection<IEntity>> results = customerContext.ExecuteMultipleEntityQueries<IEntity>(values.AsReadOnly());
        }

        #endregion

        #region LINQ

        #region Customer Query

        [TestMethod()]
        [Obsolete]
        [Ignore]
        public void CustomerWhereSimpleLinqTest()
        {
            //IEnumerable<Customer> customers = customerContext.Where(c => c.MetaData.CreateTime >= this.dateTime);
            //List<Customer> listCustomers = customers.ToList();
            //Assert.IsNotNull(listCustomers);
        }

        [TestMethod()]
        [Obsolete]
        [Ignore]
        public void CustomerWhereOrderBySimpleLinqTest()
        {
            //IEnumerable<Customer> customers = customerContext.Where(c => c.MetaData.CreateTime >= this.dateTime).OrderBy(c => c.GivenName);
            //List<Customer> listCustomers = customers.ToList();
            //Assert.IsNotNull(listCustomers);
        }

        [TestMethod()]
        [Obsolete]
        [Ignore]
        public void CustomerWhereOrderBySelectSimpleLinqTest()
        {
            //var customers = customerContext
            //    .Where(c => c.MetaData.CreateTime >= this.dateTime)
            //    .OrderBy(c => c.GivenName)
            //    .Select(c => c.GivenName);
            //foreach (var item in customers)
            //{
            //}
        }

        [TestMethod()]
        [Obsolete]
        [Ignore]
        public void CustomerWhereSelectSimpleLinqTest()
        {
            //var customers = customerContext.Where(c => c.MetaData.CreateTime >= this.dateTime)
            //    .Select(c => new { c.GivenName, c.MetaData.CreateTime });
            //foreach (var item in customers)
            //{

            //}
        }

        [TestMethod()]
        [Obsolete]
        [Ignore]
        public void CustomerWhereCountLinqTest()
        {
            //int count = customerContext.Where(c => c.MetaData.CreateTime >= this.dateTime).Count();
        }

        [TestMethod()]
        [Obsolete]
        [Ignore]
        public void CustomerCountSimpleLinqTest()
        {
            //int count = customerContext.Count();
        }

        [TestMethod]
        [Obsolete]
        [Ignore]
        public void CustomerSelectLinqTest()
        {
            //var query = this.customerContext.Select(c => new { c, c.Id, c.MetaData.CreateTime });
            //foreach (var item in query)
            //{

            //}
        }

        #endregion

        #region Invoice Query

        [TestMethod()]
        [Obsolete]
        [Ignore]
        public void InvoiceWhereSimpleLinqTest()
        {
            //IEnumerable<Invoice> invoices = invoiceContext.Where(c => c.MetaData.CreateTime >= this.dateTime);
            //List<Invoice> listInvoices = invoices.ToList();
            //Assert.IsNotNull(listInvoices);
        }

        [TestMethod()]
        [Obsolete]
        [Ignore]
        public void InvoiceWhereOrderBySimpleLinqTest()
        {
            //IEnumerable<Invoice> invoices = invoiceContext.Where(c => c.MetaData.CreateTime >= this.dateTime).OrderByDescending(c => c.MetaData.CreateTime);
            //List<Invoice> listInvoices = invoices.ToList();
            //Assert.IsNotNull(listInvoices);
        }

        [TestMethod()]
        [Obsolete]
        [Ignore]
        public void InvoiceWhereOrderBySelectSimpleLinqTest()
        {
            //var invoices = invoiceContext
            //    .Where(c => c.MetaData.CreateTime >= this.dateTime)
            //    .OrderBy(c => c.MetaData.LastUpdatedTime)
            //    .Select(c => c.Id);
            //foreach (var item in invoices)
            //{
            //}
        }

        [TestMethod()]
        [Obsolete]
        [Ignore]
        public void InvoiceWhereSelectSimpleLinqTest()
        {
            //var invoices = invoiceContext.Where(c => c.MetaData.CreateTime >= this.dateTime)
            //    .Select(c => new { c.Id, c.Line });
            //foreach (var item in invoices)
            //{

            //}
        }

        [TestMethod()]
        [Obsolete]
        [Ignore]
        public void InvoiceWhereCountLinqTest()
        {
            //int count = invoiceContext.Where(c => c.MetaData.CreateTime >= this.dateTime).Count();
        }


        [TestMethod()]
        [Obsolete]
        [Ignore]
        public void InvoiceWhereDocNumberLinqTest()
        {
            //IEnumerable<Invoice> invoices = invoiceContext.Where(c => c.DocNumber == "12345");
            //List<Invoice> listInvoices = invoices.ToList();
        }


        [TestMethod()]
        [Obsolete]
        [Ignore]
        public void InvoiceWhereIdLinqTest()
        {
            //IEnumerable<Invoice> invoices = invoiceContext.Where(c => c.Id == "2691");
            //List<Invoice> listInvoices = invoices.ToList();
        }

        [TestMethod()]
        [Obsolete]
        [Ignore]
        public void InvoiceCountSimpleLinqTest()
        {
            //int count = invoiceContext.Count();
        }

        [TestMethod]
        [Obsolete]
        [Ignore]
        public void InvoiceSelectLinqTest()
        {
            //var query = this.invoiceContext.Select(c => new { c.Id, c.MetaData.CreateTime, c.Line });
            //foreach (var item in query)
            //{

            //}
        }

        #endregion

        #region Report

        [TestMethod]
        [Obsolete]
        [Ignore]
        public void ProfitandLossReportLinqTest()
        {
            //Assert.Inconclusive("This test case will not pass since services are not yet released.");

            //QueryService<ProfitAndLoss> pal = new QueryService<ProfitAndLoss>(serviceContext);
            //var report = pal.Where(r => r.StartTransactionDate == new DateTime(2012, 01, 01));
            //foreach (var item in report)
            //{

            //}
        }

        #endregion

        #region Change Data

        [TestMethod][Ignore] //https://jira.intuit.com/browse/IPP-3918
        [Obsolete]
     
        public void ChangeDataLinqTest()
        {
            ////Assert.Inconclusive("This test case will not pass since services are not yet released.");
            //QueryService<ChangeData> changeData = new QueryService<ChangeData>(serviceContext);
            //var changeDataQuery = changeData.Where(c => c.Entities.In(new string[] { "customer", "vendor" }));
            //foreach (var item in changeDataQuery)
            //{

            //}
        }

        #endregion

        #endregion

        #region ExecuteIdsQuery

        #region Customer Query
        
        [TestMethod()]
        public void CustomerWhereSimpleTest()
        {
            string idsQuery = string.Format("select * FROM Customer WHERE Metadata.CreateTime >= '{0}'", dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK"));
            IEnumerable<Customer> customers = customerContext.ExecuteIdsQuery(idsQuery);
            Assert.IsNotNull(customers);
        }

        [TestMethod()]
        public void CustomerWhereOrderBySimpleTest()
        {
            string idsQuery = string.Format("select * FROM Customer WHERE Metadata.CreateTime >= '{0}' ORDER BY GivenName", dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK"));
            IEnumerable<Customer> customers = customerContext.ExecuteIdsQuery(idsQuery);
            Assert.IsNotNull(customers);
        }

        [TestMethod()]
        public void CustomerWhereOrderBySelectSimpleTest()
        {
            string idsQuery = string.Format("select GivenName FROM Customer WHERE Metadata.CreateTime >= '{0}' ORDER BY GivenName", dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK"));
            IEnumerable<Customer> customers = customerContext.ExecuteIdsQuery(idsQuery);
            Assert.IsNotNull(customers);
        }

        [TestMethod()]
        public void CustomerWhereSelectSimpleTest()
        {
            string idsQuery = string.Format("select GivenName, Metadata.CreateTime FROM Customer WHERE Metadata.CreateTime >= '{0}'", dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK"));
            IEnumerable<Customer> customers = customerContext.ExecuteIdsQuery(idsQuery);
            Assert.IsNotNull(customers);
        }

        [TestMethod()]
        public void CustomerWhereCountTest()
        {
            string idsQuery = string.Format("select COUNT(*) FROM Customer WHERE MetaData.CreateTime >= '{0}'", dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK"));
            int count = customerContext.ExecuteIdsQuery(idsQuery).Count();
        }

        [TestMethod()]
        public void CustomerCountSimpleTest()
        {
            string idsQuery = "select COUNT(*) FROM Customer";
            int count = customerContext.ExecuteIdsQuery(idsQuery).Count();
        }

        [TestMethod]
        public void CustomerSelectTest()
        {
            string idsQuery = "Select Id, MetaData.CreateTime FROM Customer";
            IEnumerable<Customer> customers = customerContext.ExecuteIdsQuery(idsQuery);
            Assert.IsNotNull(customers);
        }

        [TestMethod][Ignore]
        [ExpectedException(typeof(InvalidParameterException))]
        public void CustomerNullIdsQueryTest()
        {
            IEnumerable<Customer> customers = customerContext.ExecuteIdsQuery(null);
        }

        [TestMethod][Ignore]//INFO: Exception thrown is of type ValidationException which extends IdsException while ExpectedException is not accepting it
        [ExpectedException(typeof(ValidationException))]
        public void CustomerInvalidIdsQueryTest()
        {
           IEnumerable<Customer> customers = customerContext.ExecuteIdsQuery("select *");
        }

        #endregion

        #region Invoice Query

        [TestMethod()]
        public void InvoiceWhereSimpleTest()
        {
            string idsQuery = string.Format("select * FROM Invoice WHERE Metadata.CreateTime >= '{0}'", dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK"));
            IEnumerable<Invoice> invoices = invoiceContext.ExecuteIdsQuery(idsQuery);
            Assert.IsNotNull(invoices);
        }

        [TestMethod()]
        public void InvoiceWhereOrderBySimpleTest()
        {
            string idsQuery = string.Format("select * FROM Invoice WHERE Metadata.CreateTime >= '{0}' ORDER BY MetaData.CreateTime DESC", dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK"));
            IEnumerable<Invoice> invoices = invoiceContext.ExecuteIdsQuery(idsQuery);
            Assert.IsNotNull(invoices);
        }

        [TestMethod()]
        public void InvoiceWhereOrderBySelectSimpleTest()
        {
            string idsQuery = string.Format("select Id FROM Invoice WHERE Metadata.CreateTime >= '{0}' ORDER BY MetaData.CreateTime", dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK"));
            IEnumerable<Invoice> invoices = invoiceContext.ExecuteIdsQuery(idsQuery);
            Assert.IsNotNull(invoices);
        }

        [TestMethod()]
        public void InvoiceWhereSelectSimpleTest()
        {
            string idsQuery = string.Format("select Id, Line.* FROM Invoice WHERE Metadata.CreateTime >= '{0}'", dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK"));
            IEnumerable<Invoice> invoices = invoiceContext.ExecuteIdsQuery(idsQuery);
            Assert.IsNotNull(invoices);
        }

        [TestMethod()]
        public void InvoiceWhereCountTest()
        {
            string idsQuery = string.Format("select COUNT(*) FROM Invoice WHERE Metadata.CreateTime >= '{0}'", dateTime.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK"));
            int count = invoiceContext.ExecuteIdsQuery(idsQuery).Count();
        }

        [TestMethod()]
        public void InvoiceCountSimpleTest()
        {
            string idsQuery = "select COUNT(*) FROM Invoice";
            int count = invoiceContext.ExecuteIdsQuery(idsQuery).Count();
        }

        [TestMethod]
        public void InvoiceSelectTest()
        {
            string idsQuery = string.Format("select Id, Metadata.CreateTime, Line.* FROM Invoice");
            IEnumerable<Invoice> invoices = invoiceContext.ExecuteIdsQuery(idsQuery);
            Assert.IsNotNull(invoices);
        }

        #endregion

        #region Report

        [TestMethod]
        public void ProfitandLossReportTest()
        {
            Assert.Inconclusive("This test case will not pass since services are not yet released.");

            string reportQuery = "";
            QueryService<ProfitAndLoss> pal = new QueryService<ProfitAndLoss>(serviceContext);
            var reports = pal.ExecuteIdsQuery(reportQuery, QueryOperationType.report);
        }

        #endregion

        #region Change Data

        [TestMethod]
        public void ChangeDataTest()
        {
            ServiceContext serviceContext = Initializer.InitializeServiceContextQbo();

            DataService.DataService service = new DataService.DataService(serviceContext);
            List<IEntity> entityList = new List<IEntity>();
            entityList.Add(new Customer());

            IntuitCDCResponse response = service.CDC(entityList, DateTime.Today.AddDays(-100));
            List<Customer> found = response.getEntity(new Customer().GetType().Name).Cast<Customer>().ToList();
            Assert.IsTrue(found.Count > 0);
        }

        #endregion

        #endregion

        #region ToIdsQuery

        [TestMethod]
        public void CustomerBooleanToIdsQueryTest()
        {
            //string expected = "Select * FROM Customer  WHERE Active = True";
            //string actual = this.customerContext.Where(c => c.Active == true).ToIdsQuery();
            //Assert.AreEqual(expected.RemoveWhiteSpaces(), actual.RemoveWhiteSpaces());
        }

        [TestMethod]
        public void CustomerEnumToIdsQueryTest()
        {
            //string expected = "Select * FROM Customer  WHERE status = 'Synchronized'";
            //string actual = this.customerContext.Where(c => c.status == EntityStatusEnum.Synchronized).ToIdsQuery();
            //Assert.AreEqual(expected.RemoveWhiteSpaces(), actual.RemoveWhiteSpaces());
        }

        [TestMethod]
        public void CustomerLikeToIdsQueryTest()
        {
            //string expected = "Select * FROM Customer  WHERE MiddleName like 'a%' AND FamilyName like '%z' ";
            //string actual = this.customerContext.Where(c => c.MiddleName.StartsWith("a") && c.FamilyName.EndsWith("z")).ToIdsQuery();
            //Assert.AreEqual(expected.RemoveWhiteSpaces(), actual.RemoveWhiteSpaces());
        }

        [TestMethod]
        public void CustomerInToIdsQueryTest()
        {
            //string expected = "Select * FROM Customer  WHERE Id IN ('NG:001','NG:002') ";
            //string actual = this.customerContext.Where(c => c.Id.In(new string[] { "NG:001", "NG:002", })).ToIdsQuery();
            //Assert.AreEqual(expected.RemoveWhiteSpaces(), actual.RemoveWhiteSpaces());
        }

        [TestMethod]
        public void CustomerSkipTakeToIdsQueryTest()
        {
            //string expected = "Select * FROM Customer  WHERE status = 'Synchronized' startPosition 6 maxResults 3 ";
            //string actual = this.customerContext.Where(c => c.status == EntityStatusEnum.Synchronized).Skip(5).Take(3).ToIdsQuery();
            //Assert.AreEqual(expected.RemoveWhiteSpaces(), actual.RemoveWhiteSpaces());
        }

        [TestMethod]
        public void ReportToIdsQueryTest()
        {
            //string expected = "Select *  ProfitAndLossDetail  WHERE StartTransactionDate > '2012-07-10T00:00:00'";
            //QueryService<ProfitAndLossDetail> pal = new QueryService<ProfitAndLossDetail>(serviceContext);
            //string actual = pal.Where(c => c.StartTransactionDate > this.dateTime).ToIdsQuery();
            //Assert.AreEqual(expected.RemoveWhiteSpaces(), actual.RemoveWhiteSpaces());
        }

        [TestMethod]
        public void ChangeDataToIdsQueryTest()
        {
            //string expected = "Select *  WHERE LastModifiedTime <= '2012-07-10T00:00:00'";
            //QueryService<ChangeData> cd = new QueryService<ChangeData>(serviceContext);
            //string actual = cd.Where(c => c.LastModifiedTime <= this.dateTime).ToIdsQuery();
            //Assert.AreEqual(expected.RemoveWhiteSpaces(), actual.RemoveWhiteSpaces());
        }

        [TestMethod]
        public void CustomerAllBinaryOperatorsToIdsQueryTest()
        {
            //string actual = this.customerContext.Where(c =>
            //    c.Active == true &&
            //    c.MetaData.CreateTime >= this.dateTime &&
            //    c.MetaData.LastUpdatedTime <= this.dateTime).ToIdsQuery();
            //Assert.IsFalse(string.IsNullOrWhiteSpace(actual.RemoveWhiteSpaces()));
        }

        [TestMethod]
        public void NotOperatorInTest()
        {
            //string expected = "Select *  FROM Customer WHERE  NOT Id IN ('a','b') ";
            //string actual = customerContext.Where(c => !(c.Id.In(new string[] { "a", "b" }))).ToIdsQuery();
            //Assert.AreEqual(expected.RemoveWhiteSpaces(), actual.RemoveWhiteSpaces());
        }

        [TestMethod]
        public void NotOperatorTest()
        {
            //string expected = "Select *  FROM Customer  WHERE CustomField.Name = 'sdf' AND  NOT  MiddleName like 'asdf%' ";
            //string actual = customerContext.Where(c => (c.CustomField[0].Name == "sdf") && !(c.MiddleName.StartsWith("asdf"))).ToIdsQuery();
            //Assert.AreEqual(expected.RemoveWhiteSpaces(), actual.RemoveWhiteSpaces());
        }

        [TestMethod]
        public void NotOperatorDateTimeEnumTest()
        {
            //string expected = "Select * FROM Customer WHERE NOT status != 'Deleted' AND NOT MetaData.CreateTime < '2012-07-10T00:00:00'";
            //string actual = customerContext.Where(c => !(c.status != EntityStatusEnum.Deleted) && !(c.MetaData.CreateTime < this.dateTime)).ToIdsQuery();
            //Assert.AreEqual(expected.RemoveWhiteSpaces(), actual.RemoveWhiteSpaces());
        }

        #endregion

      
    }
}
