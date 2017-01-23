
namespace Intuit.Ipp.Core.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Configuration;

    /// <summary>
    /// Summary description for TestHelper.
    /// </summary>

    public static class TestHelper
    {
        /// <summary>
        /// Creates a new IPP config node, attribute and set its value in App.config. New configuration is automatically loaded into the AppDomain
        /// </summary>
        /// <param name="nodeName">New IPP config node to be created. Reuses the config node, if already exists</param>
        /// <param name="attribute">New attribute to be created/modified under "nodeName". Reuses the attribute, if already exists</param>
        /// <param name="value">Value to be set in the attribute</param>
        public static void UpdateIppConfig(string nodeName, string attribute, string value)
        {
            string ippSectionName = "configuration/intuit/ipp";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            XmlNode configNode = xmlDoc.SelectSingleNode(ippSectionName);

            string[] nodes = nodeName.Split('/');
            foreach (string singleNode in nodes)
            {
                if (configNode.SelectSingleNode(singleNode) != null)
                {
                    configNode = configNode.SelectSingleNode(singleNode);
                }
                else
                {
                    configNode = configNode.AppendChild(xmlDoc.CreateElement(singleNode));
                }

            }

            XmlAttribute configAttribute = configNode.Attributes.Append(xmlDoc.CreateAttribute(attribute));
            configAttribute.Value = value;

            xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            ConfigurationManager.RefreshSection("intuit/ipp");
        }
    }
}
