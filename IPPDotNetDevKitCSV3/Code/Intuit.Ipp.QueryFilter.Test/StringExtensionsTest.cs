using Intuit.Ipp.QueryFilter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Intuit.Ipp.QueryFilter.Test
{
    /// <summary>
    ///This is a test class for StringExtensionsTest and is intended
    ///to contain all StringExtensionsTest Unit Tests
    ///</summary>
    [Obsolete]
    [TestClass()]
    public class StringExtensionsTest
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
        ///A test for RemoveWhiteSpaces
        ///</summary>
        [TestMethod()]
        public void RemoveWhiteSpacesTest()
        {
            string value = "    Hello      World     ";
            string expected = " Hello World ";
            string actual = StringExtensions.RemoveWhiteSpaces(value);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for In
        ///</summary>
        [TestMethod()]
        public void InTest()
        {
            string value = "a";
            string[] values = new string[] { "a", "b", "c" };
            Assert.AreEqual(true, StringExtensions.In(value, values));
            Assert.AreEqual(false, StringExtensions.In("z", values));
        }
    }
}
