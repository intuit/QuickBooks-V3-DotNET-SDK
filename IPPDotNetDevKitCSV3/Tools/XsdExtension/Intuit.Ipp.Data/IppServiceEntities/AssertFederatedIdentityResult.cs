////*********************************************************
// <copyright file="AssertFederatedIdentityResult.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains class that asserts the Federated entity resultset.</summary>
////*********************************************************

namespace Intuit.Ipp.Data
{
    using System.Xml;

    /// <summary>
    /// Assert Federated Identity Result.
    /// </summary>
    public class AssertFederatedIdentityResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssertFederatedIdentityResult"/> class.
        /// </summary>
        /// <param name="singleUserNode">The Xml node.</param>
        internal AssertFederatedIdentityResult(XmlNode singleUserNode)
        {
            XmlNode actionNode = singleUserNode.SelectSingleNode("//action");
            if (actionNode != null)
            {
                this.Action = actionNode.InnerText;
            }

            XmlNode errCodeNode = singleUserNode.SelectSingleNode("//errcode");
            if (errCodeNode != null)
            {
                this.ErrorCode = errCodeNode.InnerText;
                if (this.ErrorCode.Equals("0"))
                {
                    this.IsSuccess = true;
                }
            }

            XmlNode errTextNode = singleUserNode.SelectSingleNode("//errtext");
            if (errTextNode != null)
            {
                this.ErrorText = errTextNode.InnerText;
            }

            XmlNode uDataNode = singleUserNode.SelectSingleNode("//udata");
            if (uDataNode != null)
            {
                this.UData = uDataNode.InnerText;
            }
        }

        /// <summary>
        /// Gets or sets whether the operation was success.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets the Action.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the Error code.
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the Error text.
        /// </summary>
        public string ErrorText { get; set; }

        /// <summary>
        /// Gets or sets the UData.
        /// </summary>
        public string UData { get; set; }

        /// <summary>
        /// Parses the xml node and returns AssertFederatedIdentityResult.
        /// </summary>
        /// <param name="node">The xml node.</param>
        /// <returns>Returns the AssertFederatedIdentityResult.</returns>
        public static AssertFederatedIdentityResult ParseAssertFederatedIdentityResult(XmlNode node)
        {
            return new AssertFederatedIdentityResult(node);
        }
    }
}
