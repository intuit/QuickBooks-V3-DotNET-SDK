////*********************************************************
// <copyright file="EntitlementAndUserRoleInfo.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains class that describes an entitlement and user role information.</summary>
////*********************************************************
namespace Intuit.Ipp.Data 
{
    using System.Collections.ObjectModel;
    using System.Xml;

    /// <summary>
    /// Describes an entitlement
    /// </summary>
    public class EntitlementAndUserRoleInfo
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitlementAndUserRoleInfo"/> class.
        /// </summary>
        /// <param name="entitlementAndRoleNode">The entitlement node.</param>
        public EntitlementAndUserRoleInfo(XmlNode entitlementAndRoleNode)
        {
            this.EntitlementInformation = new EntitlementInfo(entitlementAndRoleNode);
            XmlNodeList roleNodes = entitlementAndRoleNode.SelectNodes("//roles/role");
            this.RoleInformation = RoleInfo.ParseRoles(roleNodes);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the entitlement information.
        /// </summary>
        /// <value>
        /// The entitlement information.
        /// </value>
        public EntitlementInfo EntitlementInformation { get; private set; }

        /// <summary>
        /// Gets the role information.
        /// </summary>
        /// <value>
        /// The role information.
        /// </value>
        public Collection<RoleInfo> RoleInformation { get; private set; }

        #endregion
    }
}
