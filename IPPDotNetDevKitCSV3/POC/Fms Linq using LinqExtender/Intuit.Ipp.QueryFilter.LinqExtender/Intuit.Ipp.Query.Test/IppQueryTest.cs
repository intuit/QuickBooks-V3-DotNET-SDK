using LinqExtender;
using LinqExtender.Ast;
using System;
using Intuit.Ipp.Data;
using Intuit.Ipp.Query;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Intuit.Ipp.Query.Test
{
    [TestClass]
    public class IppQueryTest
    {
        IppContext<Customer> customerContext;
        IppContext<Invoice> invoiceContext;

        public IppQueryTest()
        {
            this.customerContext = new IppContext<Customer>();
            this.invoiceContext = new IppContext<Invoice>();
        }

        [TestMethod]
        public void TestingQueryServicesTest()
        {
            //string expected = "QUERY * FROM Customer  WHERE MiddleName LIKE 'C%'";
            //string actual = customerContext
            //    //.Where(c => c.MiddleName.StartsWith("C"))
            //    //.OrderBy(c => c.FamilyName)
            //    .OrderByDescending(c => c.MiddleName)
            //    //.Skip(4)
            //    .Take(4)
            //    .Select(c => c.Id)
            //    .ToIdsQuery();
            //Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        #region Where

        [TestMethod]
        public void CustomerQueryWhereMiddleNameLikeStartWithTests()
        {
            string expected = "QUERY * FROM Customer  WHERE MiddleName LIKE 'C%'";
            string actual = customerContext.Where(c => c.MiddleName.StartsWith("C")).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereMiddleNameLikeEndsWithTests()
        {
            string actual = "QUERY * FROM Customer  WHERE MiddleName LIKE '%t'";
            string expected = customerContext.Where(c => c.MiddleName.EndsWith("t")).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereMiddleNameLikeContainsTests()
        {
            string actual = "QUERY * FROM Customer  WHERE MiddleName LIKE 'C%t'";
            string expected = customerContext.Where(c => c.MiddleName.Contains("C%t")).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereMiddleNameEQTests()
        {
            string actual = "QUERY * FROM Customer  WHERE MiddleName EQ 'Cust'";
            string expected = customerContext.Where(c => c.MiddleName == "Cust").ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereGivenNameLikeStartWithTests()
        {
            string actual = "QUERY * FROM Customer  WHERE GivenName LIKE 'C%'";
            string expected = customerContext.Where(c => c.GivenName.StartsWith("C")).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereGivenNameLikeEndsWithTests()
        {
            string actual = "QUERY * FROM Customer  WHERE GivenName LIKE '%t'";
            string expected = customerContext.Where(c => c.GivenName.EndsWith("t")).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereGivenNameLikeContainsTests()
        {
            string actual = "QUERY * FROM Customer  WHERE GivenName LIKE 'C%t'";
            string expected = customerContext.Where(c => c.GivenName.Contains("C%t")).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereGivenNameContainsTests()
        {
            string actual = "QUERY * FROM Customer  WHERE GivenName LIKE 'est'";
            string expected = customerContext.Where(c => c.GivenName.Contains("est")).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereGivenNameEQTests()
        {
            string actual = "QUERY * FROM Customer  WHERE GivenName EQ 'Cust'";
            string expected = customerContext.Where(c => c.GivenName == "Cust").ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereFamilyNameLikeStartWithTests()
        {
            string actual = "QUERY * FROM Customer  WHERE FamilyName LIKE 'C%'";
            string expected = customerContext.Where(c => c.FamilyName.StartsWith("C")).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereFamilyNameLikeEndsWithTests()
        {
            string actual = "QUERY * FROM Customer  WHERE FamilyName LIKE '%t'";
            string expected = customerContext.Where(c => c.FamilyName.EndsWith("t")).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereFamilyNameLikeContainsTests()
        {
            string actual = "QUERY * FROM Customer  WHERE FamilyName LIKE 'C%t'";
            string expected = customerContext.Where(c => c.FamilyName.Contains("C%t")).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereFamilyNameEQTests()
        {
            string actual = "QUERY * FROM Customer  WHERE FamilyName EQ 'Cust'";
            string expected = customerContext.Where(c => c.FamilyName == "Cust").ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereActiveTests()
        {
            string actual = "QUERY * FROM Customer  WHERE Active EQ True";
            string expected = customerContext.Where(c => c.Active == true).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereStatusTests()
        {
            string actual = "QUERY * FROM Customer  WHERE status EQ 'Pending'";
            string expected = customerContext.Where(customer => customer.status == EntityStatusEnum.Pending).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereBalanceEQTests()
        {
            string actual = "QUERY * FROM Customer  WHERE Balance EQ '1000'";
            string expected = customerContext.Where(c => c.Balance == 1000).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereBalanceLTTests()
        {
            string actual = "QUERY * FROM Customer  WHERE Balance LT '1000'";
            string expected = customerContext.Where(c => c.Balance < 1000).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereBalanceGTTests()
        {
            string actual = "QUERY * FROM Customer  WHERE Balance GT '1000'";
            string expected = customerContext.Where(c => c.Balance > 1000).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereBalanceLETests()
        {
            string actual = "QUERY * FROM Customer  WHERE Balance LTE '1000'";
            string expected = customerContext.Where(c => c.Balance <= 1000).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereBalanceGETests()
        {
            string actual = "QUERY * FROM Customer  WHERE Balance GTE '1000'";
            string expected = customerContext.Where(c => c.Balance >= 1000).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereCreateTimeEQNowTests()
        {
            string actual = "QUERY * FROM Customer  WHERE MetaData.CreateTime EQ '2012-07-16T18:30:00Z'";
            string expected = customerContext.Where(c => c.MetaData.CreateTime == (new DateTime(2012, 07, 17)).ToUniversalTime()).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereCreateTimeEQTodayTests()
        {
            string actual = "QUERY * FROM Customer  WHERE MetaData.CreateTime EQ '2012-07-16T18:30:00Z'";
            string expected = customerContext.Where(c => c.MetaData.CreateTime == (new DateTime(2012, 07, 17)).ToUniversalTime()).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereIDEQTests()
        {
            string actual = "QUERY * FROM Customer  WHERE Id EQ 'QB:33'";
            string expected = customerContext.Where(c => c.Id == "QB:33").ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereIDLikeTests()
        {
            string actual = "QUERY * FROM Customer  WHERE Id LIKE 'NG%'";
            string expected = customerContext.Where(c => c.Id.StartsWith("NG")).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereMiddleGivenFamilyNameLikeStartWithTests()
        {
            string actual = "QUERY * FROM Customer  WHERE MiddleName LIKE 'C%'  AND FamilyName LIKE 'C%'  AND GivenName LIKE 'Test%'";
            string expected = customerContext.Where(c => c.MiddleName.StartsWith("C") && c.FamilyName.StartsWith("C") && c.GivenName.StartsWith("Test")).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereMiddleGivenFamilyNameLikeEndsWithTests()
        {
            string actual = "QUERY * FROM Customer  WHERE MiddleName LIKE '%C'  AND FamilyName LIKE '%C'  AND GivenName LIKE '%Test'";
            string expected = customerContext.Where(c => c.MiddleName.EndsWith("C") && c.FamilyName.EndsWith("C") && c.GivenName.EndsWith("Test")).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereMiddleGivenFamilyNameLikeContainsTests()
        {
            string actual = "QUERY * FROM Customer  WHERE MiddleName LIKE 'C%t'  AND FamilyName LIKE 'T%1'  AND GivenName LIKE 'T%1'";
            string expected = customerContext.Where(c => c.MiddleName.Contains("C%t") && c.FamilyName.Contains("T%1") && c.GivenName.Contains("T%1")).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        #endregion

        #region Select

        [TestMethod]
        public void CustomerQuerySelectTests()
        {
            string actual = "QUERY Id,GivenName FROM Customer";
            string expected = customerContext.Select(c => new { c.Id, c.GivenName }).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void InvoiceQuerySelectTests()
        {
            string actual = "QUERY Id,DocNumber FROM Invoice";
            string expected = invoiceContext.Select(i => new { i.Id, i.DocNumber }).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void InvoiceQueryComplexSelectTests()
        {
            string actual = "QUERY Id,Line.* FROM Invoice";
            string expected = invoiceContext.Select(i => new { i.Id, i.Line }).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void InvoiceQueryComplexSelectTests1()
        {
            string actual = "QUERY Line.Id FROM Invoice";
            string expected = invoiceContext.Select(i => i.Line[0].Id).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        #endregion

        #region OrderBy

        [TestMethod]
        public void CustomerQueryOrderByTests()
        {
            string actual = "QUERY * FROM Customer  ORDER BY  FamilyName";
            string expected = customerContext.OrderBy(c => c.FamilyName).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryOrderByDescTests()
        {
            string actual = "QUERY * FROM Customer  ORDER BY  FamilyName  DESC";
            string expected = customerContext.OrderByDescending(c => c.FamilyName).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        #endregion

        #region Where + OrderBy

        [TestMethod]
        public void CustomerQueryWhereOrderByTests()
        {
            string actual = "QUERY * FROM Customer  WHERE Id IN ('QB:9','QB:30','QB:10')  ORDER BY  GivenName";
            string[] values = { "QB:9", "QB:30", "QB:10" };
            string expected = customerContext.Where(c => c.Id.In(values)).OrderBy(c => c.GivenName).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        [TestMethod]
        public void CustomerQueryWhereOrderByDescTests()
        {
            string actual = "QUERY * FROM Customer  WHERE Id IN ('QB:9','QB:30','QB:10')  ORDER BY  GivenName  DESC";
            string[] values = { "QB:9", "QB:30", "QB:10" };
            string expected = customerContext.Where(c => c.Id.In(values)).OrderByDescending(c => c.GivenName).ToIdsQuery();
            Assert.AreEqual(expected.RemoveWhitespaces(), actual.RemoveWhitespaces());
        }

        #endregion
    }
}
