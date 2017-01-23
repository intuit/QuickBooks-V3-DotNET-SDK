////*********************************************************
// <copyright file="RoleInfo.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains class that encapsulates the information about a given role.</summary>
////*********************************************************

namespace Intuit.Ipp.Data
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Xml;
    using Intuit.Ipp.Utility;

    /// <summary>
    /// Encapsulates the information about a given role.
    /// </summary>
    public class RoleInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleInfo"/> class. Prevents a default instance of the <see cref="RoleInfo"/> class from being created.
        /// </summary>
        /// <param name="singleRoleNode">The single role node.</param>
        private RoleInfo(XmlNode singleRoleNode)
        {
            this.Id = singleRoleNode.Attributes.GetNamedItem("id").InnerText;
            XmlNode n = singleRoleNode.SelectSingleNode("./name");
            if (n != null)
            {
                this.Name = n.InnerText;
            }

            n = singleRoleNode.SelectSingleNode("./access");
            if (n != null)
            {
                XmlNode idAttr = n.Attributes.GetNamedItem("id");
                if (idAttr != null)
                {
                    this.AccessId = idAttr.InnerText;
                }

                this.Access = n.InnerText;
            }
        }

        /// <summary>
        /// Gets the access level (e.g. "Basic Access" or "Administrator").
        /// </summary>
        public string Access { get; private set; }

        /// <summary>
        /// Gets the access id.
        /// </summary>
        public string AccessId { get; private set; }

        /// <summary>
        /// Gets the name of the role as defined by the developer of the application.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the Id.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Parses the xml node and returns collections of role information.
        /// </summary>
        /// <param name="roleNodes">The xml node containing role information nodes.</param>
        /// <returns>Returns collection of role info objects.</returns>
        public static Collection<RoleInfo> ParseRoles(XmlNodeList roleNodes)
        {
            Collection<RoleInfo> rolesCollection = new Collection<RoleInfo>();
            if (roleNodes != null)
            {
                foreach (XmlNode role in roleNodes)
                {
                    rolesCollection.Add(new RoleInfo(role));
                }
            }

            return rolesCollection;
        }
    }
}
