////*********************************************************
// <copyright file="PaidSubscription.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2016 Intuit
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/
// <summary>This file contains SdkException.</summary>
// <summary>This file contains class that encapsulates subscriber information as returend by API_GetAdminsForAllProducts.</summary>
////*********************************************************
namespace Intuit.Ipp.Data 
{
    using System.Collections.ObjectModel;
    using System.Reflection;
    using System.Xml;

    /// <summary>
    /// Encapsulates Paid Subscription information as returend by API_IPPDevCustomerDetail.
    /// </summary>
    public class PaidSubscription 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaidSubscription"/> class.
        /// </summary>
        /// <param name="singleUserNode">The single user node.</param>
        public PaidSubscription(XmlNode singleUserNode)
        {
            XmlNode xmlNode = singleUserNode.SelectSingleNode("./startDate");
            if (xmlNode != null)
            {
                this.StartDate = xmlNode.InnerText;
            }

            xmlNode = singleUserNode.SelectSingleNode("./customerName");
            if (xmlNode != null)
            {
                this.CustomerName = xmlNode.InnerText;
            }

            xmlNode = singleUserNode.SelectSingleNode("./customerEmail");
            if (xmlNode != null)
            {
                this.CustomerEmail = xmlNode.InnerText;
            }

            xmlNode = singleUserNode.SelectSingleNode("./customerPhone");
            if (xmlNode != null)
            {
                this.CustomerPhone = xmlNode.InnerText;
            }

            xmlNode = singleUserNode.SelectSingleNode("./realm");
            if (xmlNode != null)
            {
                this.Realm = xmlNode.InnerText;
            }

            xmlNode = singleUserNode.SelectSingleNode("./applicationName");
            if (xmlNode != null)
            {
                this.ApplicationName = xmlNode.InnerText;
            }

            xmlNode = singleUserNode.SelectSingleNode("./planName");
            if (xmlNode != null)
            {
                this.PlanName = xmlNode.InnerText;
            }

            xmlNode = singleUserNode.SelectSingleNode("./monthlyFee");
            if (xmlNode != null)
            {
                this.MonthlyFee = xmlNode.InnerText;
            }

            xmlNode = singleUserNode.SelectSingleNode("./mbHours");
            if (xmlNode != null)
            {
                this.MbHours = xmlNode.InnerText;
            }

            xmlNode = singleUserNode.SelectSingleNode("./lastSyncTime");
            if (xmlNode != null)
            {
                this.LastSyncTime = xmlNode.InnerText;
            }
        }

        #region Properties

        /// <summary>
        /// Gets the start date.
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string StartDate { get; private set; }

        /// <summary>
        /// Gets the name of the customer.
        /// </summary>
        /// <value>
        /// The name of the customer.
        /// </value>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string CustomerName { get; private set; }

        /// <summary>
        /// Gets the customer email.
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string CustomerEmail { get; private set; }

        /// <summary>
        /// Gets the customer phone.
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string CustomerPhone { get; private set; }

        /// <summary>
        /// Gets the realm.
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string Realm { get; private set; }

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string ApplicationName { get; private set; }

        /// <summary>
        /// Gets the name of the plan.
        /// </summary>
        /// <value>
        /// The name of the plan.
        /// </value>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string PlanName { get; private set; }

        /// <summary>
        /// Gets the monthly fee.
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string MonthlyFee { get; private set; }

        /// <summary>
        /// Gets the mb hours.
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string MbHours { get; private set; }

        /// <summary>
        /// Gets the last sync time.
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string LastSyncTime { get; private set; }
        #endregion

        /// <summary>
        /// Parses the paid subscription.
        /// </summary>
        /// <param name="node">The xml node.</param>
        /// <returns>Returns the paid subscriptions.</returns>
        public static Collection<PaidSubscription> ParsePaidSubscription(XmlNode node)
        {
            Collection<PaidSubscription> paidSubscriptionCollections = new Collection<PaidSubscription>();
            XmlNodeList admins = node.SelectNodes("./paidSubscription");
            if (admins != null)
            {
                foreach (XmlNode admin in admins)
                {
                    paidSubscriptionCollections.Add(new PaidSubscription(admin));
                }
            }

            return paidSubscriptionCollections;
        }
    }
}
