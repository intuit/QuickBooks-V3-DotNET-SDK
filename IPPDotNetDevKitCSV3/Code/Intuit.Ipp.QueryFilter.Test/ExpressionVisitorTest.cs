using Intuit.Ipp.QueryFilter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Intuit.Ipp.LinqExtender.Ast;

namespace Intuit.Ipp.QueryFilter.Test
{
    /// <summary>
    ///This is a test class for ExpressionVisitorTest and is intended
    ///to contain all ExpressionVisitorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ExpressionVisitorTest
    {
        private TestContext testContextInstance;

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

        /// <summary>
        ///A test for VisitSelectCallExpression
        ///</summary>
        [TestMethod()]
        public void VisitSelectCallExpressionTest()
        {
            ExpressionVisitor target = new ExpressionVisitor();
            PrivateObject obj = new PrivateObject(target);
            var retVal = obj.Invoke("PrivateMethod");
            SelectExpression selectExpression = new SelectExpression("Name");
            SelectExpression actual = target.VisitSelectCallExpression(selectExpression) as SelectExpression;
            Assert.ReferenceEquals(selectExpression, actual);
        }

        /// <summary>
        ///A test for VisitTypeExpression
        ///</summary>
        [TestMethod()]
        public void VisitTypeExpressionTest()
        {
            ExpressionVisitor_Accessor target = new ExpressionVisitor_Accessor();
            TypeExpression typeExpression = null;
            Expression expected = null;
            Expression actual = target.VisitTypeExpression(typeExpression);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for VisitMethodCallExpression
        ///</summary>
        [TestMethod()]
        public void VisitMethodCallExpressionTest()
        {
            ExpressionVisitor_Accessor target = new ExpressionVisitor_Accessor();
            MethodCallExpression methodCallExpression = null;
            Expression expected = null;
            Expression actual = target.VisitMethodCallExpression(methodCallExpression);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for VisitLiteralExpression
        ///</summary>
        [TestMethod()]
        public void VisitLiteralExpressionTest()
        {
            ExpressionVisitor_Accessor target = new ExpressionVisitor_Accessor();
            LiteralExpression expression = null;
            Expression expected = null;
            Expression actual = target.VisitLiteralExpression(expression);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for VisitMemberExpression
        ///</summary>
        [TestMethod()]
        public void VisitMemberExpressionTest()
        {
            ExpressionVisitor_Accessor target = new ExpressionVisitor_Accessor();
            MemberExpression expression = null;
            Expression expected = null;
            Expression actual = target.VisitMemberExpression(expression);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for VisitOrderbyExpression
        ///</summary>
        [TestMethod()]
        public void VisitOrderbyExpressionTest()
        {
            ExpressionVisitor_Accessor target = new ExpressionVisitor_Accessor();
            OrderbyExpression expression = null;
            Expression expected = null;
            Expression actual = target.VisitOrderbyExpression(expression);
            Assert.AreEqual(expected, actual);
        }
    }
}
