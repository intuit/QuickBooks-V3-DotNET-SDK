////*********************************************************
// <copyright file="AssertFederatedIdentityResult.cs" company="Intuit">
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
