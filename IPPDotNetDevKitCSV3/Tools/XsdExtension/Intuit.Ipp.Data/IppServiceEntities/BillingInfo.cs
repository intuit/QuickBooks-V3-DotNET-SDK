////*********************************************************
// <copyright file="BillingInfo.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains class that encapsulates a billing info describing the billing state of an application.</summary>
////*********************************************************

namespace Intuit.Ipp.Data
{
    using System;
    using System.Xml;
    using Intuit.Ipp.Utility;

    /// <summary>
    /// Encapsulates a billing info describing the billing state of an application.
    /// </summary>
    public class BillingInfo
    {
        /// <summary>
        /// Application is in "GRACE" billing status.
        /// </summary>
        public const string StatusGrace = "GRACE";

        /// <summary>
        /// Application is in "OK" billing status.
        /// </summary>
        public const string StatusOk = "OK";

        /// <summary>
        /// Initializes a new instance of the <see cref="BillingInfo"/> class.
        /// </summary>
        /// <param name="billingNode">The billing node.</param>
        public BillingInfo(XmlNode billingNode)
        {
            XmlNode node = billingNode.SelectSingleNode("//status");
            if (node != null)
            {
                this.Status = node.InnerText;
            }

            node = billingNode.SelectSingleNode("//lastPaymentDate");
            if (node != null)
            {
                this.LastPaymentDate = DateHelper.ParseDateTimeField(node.InnerText);
            }

            node = billingNode.SelectSingleNode("//dbid");
            if (node != null)
            {
                this.DbId = node.InnerText;
            }
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public string Status { get; private set; }

        /// <summary>
        /// Gets the last payment date.
        /// </summary>
        public DateTime LastPaymentDate { get; private set; }

        /// <summary>
        /// Gets the db id.
        /// </summary>
        public string DbId { get; private set; }

        /// <summary>
        /// Determines whether Billing info has status grace
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has status grace]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasStatusGrace()
        {
            return this.Status == StatusGrace;
        }

        /// <summary>
        /// Determines whether [has status ok].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [has status ok]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasStatusOk()
        {
            return this.Status == StatusOk;
        }
    }
}
