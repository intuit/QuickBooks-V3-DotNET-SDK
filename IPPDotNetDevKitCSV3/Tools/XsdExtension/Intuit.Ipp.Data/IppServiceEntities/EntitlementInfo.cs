////*********************************************************
// <copyright file="EntitlementInfo.cs" company="Intuit">
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
// <summary>This file contains class that describes an entitlement.</summary>
////*********************************************************
namespace Intuit.Ipp.Data 
{
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// Describes an entitlement
    /// </summary>
    public class EntitlementInfo
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitlementInfo"/> class.
        /// Constructor that parses XML returned by API
        /// </summary>
        /// <param name="entitlementNode">The entitlement node.</param>
        public EntitlementInfo(XmlNode entitlementNode)
        {
            XmlNode n = entitlementNode.SelectSingleNode("//QboCompany");
            if (n != null)
            {
                this.QboCompany = bool.Parse(n.InnerText);
            }

            n = entitlementNode.SelectSingleNode("//PlanName");
            if (n != null)
            {
                this.PlanName = n.InnerText;
            }

            n = entitlementNode.SelectSingleNode("//MaxUsers");
            if (n != null)
            {
                this.MaxUsers = int.Parse(n.InnerText);
            }

            n = entitlementNode.SelectSingleNode("//CurrentUsers");
            if (n != null)
            {
                this.CurrentUsers = int.Parse(n.InnerText);
            }

            n = entitlementNode.SelectSingleNode("//DaysRemainingTrial");
            if (n != null)
            {
                this.DaysRemaining = int.Parse(n.InnerText);
            }

            this.Entitlements = Entitlement.ParseEntitlements(entitlementNode);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Check if the company is a QuickBooks Online company. false is returned if not a QuickBooks Online company, the company exists in the Intuit ecosystem, but is not a QuickBooks Online company, or the company is a QuickBooks Online company, but the current user does not belong to the company.
        /// </summary>
        public bool QboCompany { get; private set; }
       
        /// <summary>
        /// Gets the name of the plan.
        /// </summary>
        public string PlanName { get; private set; }

        /// <summary>
        /// Gets the max users.
        /// </summary>
        public int MaxUsers { get; private set; }

        /// <summary>
        /// Gets the current users.
        /// </summary>
        public int CurrentUsers { get; private set; }

        /// <summary>
        /// Gets the days remaining.
        /// </summary>
        public int DaysRemaining { get; private set; }

        /// <summary>
        /// Gets the entitlements.
        /// </summary>
        public IList<Entitlement> Entitlements { get; private set; }

        #endregion
    }
}
