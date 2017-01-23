using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Intuit.Ipp.Core.Configuration;
using Intuit.Ipp.Utility;
using System.Xml;
using System.Configuration;

namespace Intuit.Ipp.Core.Test
{
    /// <summary>
    /// Summary description for LocalConfigReaderTest.
    /// </summary>

    [TestClass]
    public class LocalConfigReaderTest
    {
        string origConfig = string.Empty;

        [TestInitialize]
        public void Setup()
        {
            if (origConfig == string.Empty)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
                origConfig = xmlDoc.InnerXml;
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (origConfig == string.Empty)
                return;

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(origConfig);
            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            ConfigurationManager.RefreshSection("intuit/ipp");
        }

        /// <summary>
        /// Test for Linear retry policy configuration
        /// </summary>
        [TestMethod]
        public void ConfigTest_LinearRetry()
        {
            TestHelper.UpdateIppConfig("retry", "mode", RetryMode.Linear.ToString());
            IppConfigurationSection ippConfigurationSection = IppConfigurationSection.Instance;
            Assert.AreEqual(ippConfigurationSection.Retry.Mode, RetryMode.Linear);

            LocalConfigReader reader = new LocalConfigReader();            
            TestHelper.UpdateIppConfig("retry/linearRetry", "retryCount", "1");
            TestHelper.UpdateIppConfig("retry/linearRetry", "retryInterval", new TimeSpan(20).ToString());
            IppConfiguration ippConfig = reader.ReadConfiguration();
            Assert.IsNotNull(ippConfig.RetryPolicy);
        }

        /// <summary>
        /// Test for Exponential retry policy configuration
        /// </summary>
        [TestMethod]
        public void ConfigTest_ExponentialRetry()
        {
            TestHelper.UpdateIppConfig("retry", "mode", RetryMode.Exponential.ToString());
            IppConfigurationSection ippConfigurationSection = IppConfigurationSection.Instance;
            Assert.AreEqual(ippConfigurationSection.Retry.Mode, RetryMode.Exponential);

            LocalConfigReader reader = new LocalConfigReader();
            TestHelper.UpdateIppConfig("retry/exponentialRetry", "retryCount", "2");
            TestHelper.UpdateIppConfig("retry/exponentialRetry", "minBackoff", new TimeSpan(20).ToString());
            TestHelper.UpdateIppConfig("retry/exponentialRetry", "maxBackoff", new TimeSpan(20).ToString());
            TestHelper.UpdateIppConfig("retry/exponentialRetry", "deltaBackoff", new TimeSpan(20).ToString());
            IppConfiguration ippConfig = reader.ReadConfiguration();
            Assert.IsNotNull(ippConfig.RetryPolicy);
        }

        /// <summary>
        /// Test for Incremental retry policy configuration
        /// </summary>
        [TestMethod]
        public void ConfigTest_IncrementalRetry()
        {
            TestHelper.UpdateIppConfig("retry", "mode", RetryMode.Incremental.ToString());
            IppConfigurationSection ippConfigurationSection = IppConfigurationSection.Instance;
            Assert.AreEqual(ippConfigurationSection.Retry.Mode, RetryMode.Incremental);

            LocalConfigReader reader = new LocalConfigReader();
            TestHelper.UpdateIppConfig("retry/incrementalRetry", "retryCount", "1");
            TestHelper.UpdateIppConfig("retry/incrementalRetry", "initialInterval", new TimeSpan(20).ToString());
            TestHelper.UpdateIppConfig("retry/incrementalRetry", "increment", new TimeSpan(20).ToString());
            IppConfiguration ippConfig = reader.ReadConfiguration();
            Assert.IsNotNull(ippConfig.RetryPolicy);
        }        
    }
}
