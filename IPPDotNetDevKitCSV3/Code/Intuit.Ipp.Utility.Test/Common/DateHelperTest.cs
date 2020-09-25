////*********************************************************
// <copyright file="DateHelperTest.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains test cases for Date Helper functions.</summary>
////*********************************************************
namespace Intuit.Ipp.Utility.Test
{
    using System;
    using Utility;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for DateHelperTest and is intended to contain all DateHelperTest Unit Tests
    /// </summary>
    [TestClass()]
    public class DateHelperTest
    {
        /// <summary>
        /// The test Context Instance.
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
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
        /// A test for ParseDateTimeField
        /// </summary>
        [TestMethod()]
        public void ParseDateTimeFieldTest()
        {
            DateTime epochJanFirst1970Utc = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            long value = 23432213;
            DateTime expected = epochJanFirst1970Utc.AddMilliseconds(value).ToLocalTime();
            DateTime actual;
            actual = DateHelper.ParseDateTimeField(value.ToString());
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// A test for ParseDateTimeField
        /// </summary>
        [TestMethod()]
        public void ParseDateTimeFieldTestForNegativeValues()
        {
            DateTime epochJanFirst1970Utc = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            long value = -2343221334343;
            DateTime expected = epochJanFirst1970Utc.AddMilliseconds(value).ToLocalTime();
            DateTime actual;
            actual = DateHelper.ParseDateTimeField(value.ToString());
            Assert.AreEqual(expected, actual);
        }
    }
}
