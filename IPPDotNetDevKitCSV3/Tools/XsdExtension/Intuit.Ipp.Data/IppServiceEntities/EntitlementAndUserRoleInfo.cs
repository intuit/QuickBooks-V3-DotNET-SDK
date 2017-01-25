////*********************************************************
// <copyright file="EntitlementAndUserRoleInfo.cs" company="Intuit">
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
