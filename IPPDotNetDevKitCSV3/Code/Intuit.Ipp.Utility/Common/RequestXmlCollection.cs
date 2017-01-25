////********************************************************************
// <copyright file="RequestXmlCollection.cs" company="Intuit">
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
// <summary>This file contains Request Xml Document for http web request payload.</summary>
////********************************************************************
namespace Intuit.Ipp.Utility
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml;
    using Intuit.Ipp.Utility.Properties;

    /// <summary>
    /// A helper class to build API requests.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1058:TypesShouldNotExtendCertainBaseTypes", Justification = "It make sense here, RequestXmlCollection is wrapper on XmlDocument classs.")]
    [SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface", Justification = "It make sense here, RequestXmlCollection is wrapper on XmlDocument classs.")]
    public class RequestXmlCollection : XmlDocument
    {
        /// <summary>
        /// Request Id.
        /// </summary>
        private readonly string requestId;

        /// <summary>
        /// Quick book database API Element.
        /// </summary>
        private XmlElement qdbapiElement;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestXmlCollection"/> class.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        public RequestXmlCollection(string requestId)
        {
            this.requestId = requestId;
        }

        /// <summary>
        /// Gets the Quick book database API element.
        /// </summary>
        private XmlElement QdbapiElement
        {
            get
            {
                if (this.qdbapiElement == null)
                {
                    this.qdbapiElement = this.CreateElement(UtilityConstants.QDBAPI);
                    this.AppendChild(this.QdbapiElement);
                    this.AddTextParameter(UtilityConstants.ENCODINGATTR, UtilityConstants.ENCODINGATTRVALUE);
                    this.AddTextParameter(UtilityConstants.UDATA, this.requestId);
                }

                return this.qdbapiElement;
            }
        }
       
        /// <summary>
        /// Add an API parameter of type Text
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of parameter.</param>
        public void AddTextParameter(string name, string value)
        {
            this.AddNode(this.QdbapiElement, name, this.CreateTextNode(value));
        }

        /// <summary>
        /// Creates a new element with the given <paramref name="name"/>, appends the <paramref name="node"/> to that new element, and appends the new element to <paramref name="appendTo"/>.
        /// </summary>
        /// <param name="appendTo">The append to.</param>
        /// <param name="name">The name of the child node.</param>
        /// <param name="node">The child node.</param>
        /// <returns>Returns xml element.</returns>
        private XmlElement AddNode(XmlElement appendTo, string name, XmlNode node)
        {
            XmlElement elem = CreateElement(name);
            elem.AppendChild(node);
            appendTo.AppendChild(elem);
            return elem;
        }
    }
}
