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
    using System;
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
            XmlNode n = entitlementNode.SelectSingleNode("//appId");
            if (n != null)
            {
                this.AppId = n.InnerText;
            }

            n = entitlementNode.SelectSingleNode("//productId");
            if (n != null)
            {
                this.ProductId = n.InnerText;
            }

            n = entitlementNode.SelectSingleNode("//planName");
            if (n != null)
            {
                this.PlanName = n.InnerText;
            }

            n = entitlementNode.SelectSingleNode("//planType");
            if (n != null)
            {
                this.PlanType = n.InnerText;
            }

            n = entitlementNode.SelectSingleNode("//maxUsers");
            if (n != null)
            {
                this.MaxUsers = int.Parse(n.InnerText);
            }

            n = entitlementNode.SelectSingleNode("//currentUsers");
            if (n != null)
            {
                this.CurrentUsers = int.Parse(n.InnerText);
            }

            n = entitlementNode.SelectSingleNode("//daysRemainingTrial");
            if (n != null)
            {
                this.DaysRemaining = int.Parse(n.InnerText);
            }

            n = entitlementNode.SelectSingleNode("//fee");
            if (n != null)
            {
                this.Fee = double.Parse(n.InnerText);
            }

            n = entitlementNode.SelectSingleNode("//betaExpirationDate");
            if (n != null)
            {
                // comes in longMonth DD, YYYY  format (e.g. June 10,2010)
                this.BetaExpirationDate = DateTime.Parse(n.InnerText);
            }

            n = entitlementNode.SelectSingleNode("//currentFileUsage");
            if (n != null)
            {
                this.CurrentFileUsage = long.Parse(n.InnerText);
            }

            this.Entitlements = Entitlement.ParseEntitlements(entitlementNode);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the app id.
        /// </summary>
        public string AppId { get; private set; }

        /// <summary>
        /// Gets the product id.
        /// </summary>
        public string ProductId { get; private set; }

        /// <summary>
        /// Gets the name of the plan.
        /// </summary>
        /// <value>
        /// The name of the plan.
        /// </value>
        public string PlanName { get; private set; }

        /// <summary>
        /// Gets the type of the plan.
        /// </summary>
        /// <value>
        /// The type of the plan.
        /// </value>
        public string PlanType { get; private set; }

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
        /// Gets the fee.
        /// </summary>
        public double Fee { get; private set; }

        /// <summary>
        /// Gets the beta expiration date.
        /// </summary>
        public DateTime BetaExpirationDate { get; private set; }

        /// <summary>
        /// Gets the current file usage.
        /// </summary>
        public long CurrentFileUsage { get; private set; }

        /// <summary>
        /// Gets the entitlements.
        /// </summary>
        public IList<Entitlement> Entitlements { get; private set; }

        #endregion
    }
}
