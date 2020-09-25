////*********************************************************
// <copyright file="RequestXmlCollectionTest.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains test cases for RequestXmlCollection class functions.</summary>
////*********************************************************
namespace Intuit.Ipp.Utility.Test
{
    using System;
    using System.Globalization;
    using Utility;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    /// <summary>
    /// This is a test class for RequestXmlCollectionTest and is intended to contain all RequestXmlCollectionTest Unit Tests
    /// </summary>
    [TestClass()]
    public class RequestXmlCollectionTest
    {
        /// <summary>
        /// The test Context Instance.
        /// </summary>
        private TestContext testContextInstance;

        /// <summary>
        /// The Request Id.
        /// </summary>
        private string requestId = DateTime.UtcNow.Ticks.ToString(CultureInfo.CurrentCulture);

        /// <summary>
        /// The RequestXmlCollection object.
        /// </summary>
        private RequestXmlCollection target;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestXmlCollectionTest"/> class.
        /// </summary>
        public RequestXmlCollectionTest()
        {
            target = new RequestXmlCollection(requestId);
        }

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

        /// <summary>
        /// A test for RequestXmlCollection Constructor
        /// </summary>
        [TestMethod()]
        public void RequestXmlCollectionConstructorTest()
        {
            target.AddTextParameter("test", "test");
            Assert.IsTrue(target.InnerXml.Contains(requestId));
        }

        /// <summary>
        /// A test for AddTextParameter
        /// </summary>
        [TestMethod()]
        public void AddTextParameterTest()
        {
            string name = "xmlField";
            string value = "xmlValue";
            target.AddTextParameter(name, value);
            Assert.IsTrue(target.InnerXml.Contains("<" + name + ">" + value + "</" + name + ">"));
        }
    }
}
