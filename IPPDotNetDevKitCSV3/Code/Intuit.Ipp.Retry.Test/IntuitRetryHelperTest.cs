////*********************************************************
// <copyright file="IntuitRetryHelperTest.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains test cases for IntuitRetryHelper .</summary>
////*********************************************************
namespace Intuit.Ipp.Retry.Test
{
    using System;
    using Retry;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This is a test class for IntuitRetryHelperTest and is intended
    /// to contain all IntuitRetryHelperTest Unit Tests
    /// </summary>
    [TestClass()]
    public class IntuitRetryHelperTest
    {
        /// <summary>
        /// The test Context Instance.
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// Gets or sets the test context which provides information about and functionality for the current test run.
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

        ///// <summary>
        ///// A test for ArgumentNotNull
        ///// </summary>
        //[TestMethod()]
        //public void ArgumentNotNullTest()
        //{
        //    object argumentValue = null;
        //    string argumentName = "Null Argument";
        //    bool actual;
        //    try
        //    {
        //        actual = IntuitRetryHelper_Accessor.IsArgumentNull(argumentValue, argumentName);
        //        Assert.Fail();
        //    }
        //    catch (ArgumentNullException)
        //    { 
        //    }
        //}

        ///// <summary>
        ///// A test for ArgumentNotNegativeValue For Integer
        ///// </summary>
        //[TestMethod()]
        //public void ArgumentNotNegativeValueTestForInt()
        //{
        //    int argumentValue = -5;
        //    string argumentName = "Negative Argument";
        //    try
        //    {
        //        IntuitRetryHelper_Accessor.ArgumentNotNegativeValue(argumentValue, argumentName);
        //    }
        //    catch (ArgumentOutOfRangeException)
        //    {
        //    }
        //}

        ///// <summary>
        ///// A test for ArgumentNotNegativeValue For Long
        ///// </summary>
        //[TestMethod()]
        //public void ArgumentNotNegativeValueTestForLong()
        //{
        //    long argumentValue = -465465546565;
        //    string argumentName = "Negative Argument";
        //    try
        //    {
        //        IntuitRetryHelper_Accessor.ArgumentNotNegativeValue(argumentValue, argumentName);
        //    }
        //    catch (ArgumentOutOfRangeException)
        //    {
        //    }
        //}

        ///// <summary>
        ///// A test for ArgumentNotGreaterThan
        ///// </summary>
        //[TestMethod()]
        //public void ArgumentNotGreaterThanTest()
        //{
        //    double argumentValue = 5F; 
        //    double ceilingValue = 0F; 
        //    string argumentName = "ArgumentNotGreater";
        //    try
        //    {
        //        IntuitRetryHelper_Accessor.ArgumentNotGreaterThan(argumentValue, ceilingValue, argumentName);
        //    }
        //    catch (ArgumentOutOfRangeException)
        //    {
        //    }
        //}
    }
}
