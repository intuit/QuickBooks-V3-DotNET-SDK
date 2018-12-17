////******************************************************************************************************************************
// <copyright file="IntuitRetryPolicyValidationTest.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Validation Test cases for retry mechanism for unreliable actions and transient conditions.</summary>
////******************************************************************************************************************************

namespace Intuit.Ipp.Retry.Test
{
    using System;
    using Intuit.Ipp.Core;
    using Intuit.Ipp.Retry;
    using Microsoft.VisualStudio.TestTools.UnitTesting;  

    /// <summary>
    /// This is a test class for IntuitRetryPolicyTest and is intended to contain all IntuitRetryPolicyTest Unit Tests
    /// </summary>
    [TestClass()]
    public class IntuitRetryPolicyValidationTest
    {
        /// <summary>
        /// The test Context Instance
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which providesinformation about and functionality for the current test run.
        /// </summary>
        public TestContext TestContext
        {
            get
            {
                return this.testContextInstance;
            }

            set
            {
                this.testContextInstance = value;
            }
        }

        /// <summary>
        /// Intuits the retry policy constructor test for intuit retry policy retry.
        /// </summary>
        [TestMethod()]
        public void IntuitRetryPolicyConstructorTestForIntuitRetryPolicyRetry()
        {
            int retryCount = 1;
            TimeSpan minBackoff = TimeSpan.FromSeconds(1);
            TimeSpan maxBackoff = TimeSpan.FromSeconds(1);
            TimeSpan deltaBackoff = TimeSpan.FromSeconds(1);
            IntuitRetryPolicy target = new IntuitRetryPolicy(retryCount, minBackoff, maxBackoff, deltaBackoff);
        }

        /// <summary>
        /// Intuits the retry policy constructor test.
        /// </summary>
        [TestMethod()]
        public void IntuitRetryPolicyConstructorTest()
        {
            int retryCount = 1;
            TimeSpan minBackoff = TimeSpan.FromSeconds(1);
            TimeSpan maxBackoff = TimeSpan.FromSeconds(1);
            TimeSpan deltaBackoff = TimeSpan.FromSeconds(1);
            IntuitRetryPolicy target = new IntuitRetryPolicy(retryCount, minBackoff, maxBackoff, deltaBackoff);
        }

        /// <summary>
        /// Intuits the retry policy constructor test wit min backoff negative.
        /// </summary>
        [TestMethod()]
        public void IntuitRetryPolicyConstructorTestWitMinBackoffNegative()
        {
            int retryCount = 1;
            TimeSpan minBackoff = TimeSpan.FromSeconds(-1);
            TimeSpan maxBackoff = TimeSpan.FromSeconds(1);
            TimeSpan deltaBackoff = TimeSpan.FromSeconds(1);
            try
            {
                IntuitRetryPolicy target = new IntuitRetryPolicy(retryCount, minBackoff, maxBackoff, deltaBackoff);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// Intuits the retry policy constructor test wit max backoff negative.
        /// </summary>
        [TestMethod()]
        public void IntuitRetryPolicyConstructorTestWitMaxBackoffNegative()
        {
            int retryCount = 1;
            TimeSpan minBackoff = TimeSpan.FromSeconds(1);
            TimeSpan maxBackoff = TimeSpan.FromSeconds(-1);
            TimeSpan deltaBackoff = TimeSpan.FromSeconds(1);
            try
            {
                IntuitRetryPolicy target = new IntuitRetryPolicy(retryCount, minBackoff, maxBackoff, deltaBackoff);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// Intuits the retry policy constructor test wit delta backoff negative.
        /// </summary>
        [TestMethod()]
        public void IntuitRetryPolicyConstructorTestWitDeltaBackoffNegative()
        {
            int retryCount = 1;
            TimeSpan minBackoff = TimeSpan.FromSeconds(1);
            TimeSpan maxBackoff = TimeSpan.FromSeconds(1);
            TimeSpan deltaBackoff = TimeSpan.FromSeconds(-1);
            try
            {
                IntuitRetryPolicy target = new IntuitRetryPolicy(retryCount, minBackoff, maxBackoff, deltaBackoff);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// Intuits the retry policy constructor test wit min backoff greater than min backoff.
        /// </summary>
        [TestMethod()]
        public void IntuitRetryPolicyConstructorTestWitMinBackoffGreaterThanMinBackoff()
        {
            int retryCount = 1;
            TimeSpan minBackoff = TimeSpan.FromSeconds(2);
            TimeSpan maxBackoff = TimeSpan.FromSeconds(1);
            TimeSpan deltaBackoff = TimeSpan.FromSeconds(1);
            try
            {
                IntuitRetryPolicy target = new IntuitRetryPolicy(retryCount, minBackoff, maxBackoff, deltaBackoff);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// Intuits the retry policy constructor test for fixed.
        /// </summary>
        [TestMethod()]
        public void IntuitRetryPolicyConstructorTestForFixed()
        {
            int retryCount = 0;
            TimeSpan retryInterval = TimeSpan.FromSeconds(1);
            IntuitRetryPolicy target = new IntuitRetryPolicy(retryCount, retryInterval);
        }

        /// <summary>
        /// Intuits the retry policy constructor test with negative rery count.
        /// </summary>
        [TestMethod()]
        public void IntuitRetryPolicyConstructorTestWithNegativeReryCount()
        {
            int retryCount = -1;
            TimeSpan retryInterval = TimeSpan.FromSeconds(1);
            try
            {
                IntuitRetryPolicy target = new IntuitRetryPolicy(retryCount, retryInterval);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// Intuits the retry policy constructor test with negative retry interval.
        /// </summary>
        [TestMethod()]
        public void IntuitRetryPolicyConstructorTestWithNegativeRetryInterval()
        {
            int retryCount = 1;
            TimeSpan retryInterval = TimeSpan.FromSeconds(-1);
            try
            {
                IntuitRetryPolicy target = new IntuitRetryPolicy(retryCount, retryInterval);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// Intuits the retry policy constructor test for incremental.
        /// </summary>
        [TestMethod()]
        public void IntuitRetryPolicyConstructorTestForIncremental()
        {
            int retryCount = 0;
            TimeSpan initialInterval = TimeSpan.FromSeconds(1);
            TimeSpan increment = TimeSpan.FromSeconds(1);
            IntuitRetryPolicy target = new IntuitRetryPolicy(retryCount, initialInterval, increment);
        }

        /// <summary>
        /// Intuits the retry policy constructor test with negative retry count.
        /// </summary>
        [TestMethod()]
        public void IntuitRetryPolicyConstructorTestWithNegativeRetryCount()
        {
            int retryCount = -1;
            TimeSpan initialInterval = TimeSpan.FromSeconds(1);
            TimeSpan increment = TimeSpan.FromSeconds(1);
            try
            {
                IntuitRetryPolicy target = new IntuitRetryPolicy(retryCount, initialInterval, increment);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// Intuits the retry policy constructor test with negative initial interval.
        /// </summary>
        [TestMethod()]
        public void IntuitRetryPolicyConstructorTestWithNegativeInitialInterval()
        {
            int retryCount = -1;
            TimeSpan initialInterval = TimeSpan.FromSeconds(-1);
            TimeSpan increment = TimeSpan.FromSeconds(1);
            try
            {
                IntuitRetryPolicy target = new IntuitRetryPolicy(retryCount, initialInterval, increment);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
            }
        }

        /// <summary>
        /// Intuits the retry policy constructor test with negative increment.
        /// </summary>
        [TestMethod()]
        public void IntuitRetryPolicyConstructorTestWithNegativeIncrement()
        {
            int retryCount = -1;
            TimeSpan initialInterval = TimeSpan.FromSeconds(1);
            TimeSpan increment = TimeSpan.FromSeconds(-1);
            try
            {
                IntuitRetryPolicy target = new IntuitRetryPolicy(retryCount, initialInterval, increment);
                Assert.Fail();
            }
            catch (ArgumentException)
            {
            }
        }
    }
}
