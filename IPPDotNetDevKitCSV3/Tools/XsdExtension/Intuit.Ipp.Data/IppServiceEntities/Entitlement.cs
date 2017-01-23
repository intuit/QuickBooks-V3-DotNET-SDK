////*********************************************************
// <copyright file="Entitlement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains class that describes individual entitlement.</summary>
////*********************************************************
namespace Intuit.Ipp.Data
{
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// Describes individual entitlement.
    /// </summary>
    public class Entitlement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Entitlement"/> class.
        /// Entitlement constructor based on API XML response
        /// </summary>
        /// <param name="singleEntitlementNode">The single entitlement node.</param>
        public Entitlement(XmlNode singleEntitlementNode)
        {
            this.Id = singleEntitlementNode.Attributes.GetNamedItem("id").InnerText;
            XmlNode n = singleEntitlementNode.SelectSingleNode("./name");
            if (n != null)
            {
                this.Name = n.InnerText;
            }

            n = singleEntitlementNode.SelectSingleNode("./term");

            if (n != null)
            {
                this.TermId = n.Attributes.GetNamedItem("id").InnerText;
                this.Term = n.InnerText;
            }
        }

        /// <summary>
        /// Gets unique identifier of entitlement.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the term.
        /// </summary>
        public string Term { get; private set; }

        /// <summary>
        /// Gets the term id.
        /// </summary>
        public string TermId { get; private set; }

        /// <summary>
        /// Parses all the entitlement elements of the API_GetEntitlementValues response.
        /// </summary>
        /// <param name="node">The xml node.</param>
        /// <returns>
        /// Returns list of entitlements.
        /// </returns>
        public static List<Entitlement> ParseEntitlements(XmlNode node)
        {
            List<Entitlement> entitlements = new List<Entitlement>();
            XmlNodeList entitlementNodes = node.SelectNodes("./entitlements/entitlement");
            if (entitlementNodes != null)
            {
                foreach (XmlNode e in entitlementNodes)
                {
                    entitlements.Add(new Entitlement(e));
                }
            }

            return entitlements;
        }
    }
}
