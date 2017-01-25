////*********************************************************
// <copyright file="UserInfo.cs" company="Intuit">
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
// <summary>This file contains class wraps Ipp user data. Depending on user's access levels and which user you're querying, many of the fields might not be filled.</summary>
////*********************************************************

namespace Intuit.Ipp.Data
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Xml;
    using Intuit.Ipp.Utility;

    /// <summary>
    /// Wraps user information returned by the platform. Depending on your access levels and which user you're querying, many of the fields might not be filled.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// The Role Names.
        /// </summary>
        private string roleNames;

        /// <summary>
        /// The List of roles.
        /// </summary>
        private Collection<RoleInfo> roles;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInfo"/> class.
        /// </summary>
        /// <param name="singleUserNode">The single user node.</param>
        public UserInfo(XmlNode singleUserNode)
        {
            this.LastAccess = DateTime.MinValue;
            this.Id = singleUserNode.Attributes.GetNamedItem("id").InnerText;
            XmlNode xmlNode = singleUserNode.SelectSingleNode("./firstName");
            if (xmlNode != null)
            {
                this.FirstName = xmlNode.InnerText;
            }

            xmlNode = singleUserNode.SelectSingleNode("./name");
            if (xmlNode != null)
            {
                this.Name = xmlNode.InnerText;
            }

            xmlNode = singleUserNode.SelectSingleNode("./lastName");
            if (xmlNode != null)
            {
                this.LastName = xmlNode.InnerText;
            }

            xmlNode = singleUserNode.SelectSingleNode("./login");
            if (xmlNode != null)
            {
                this.Login = xmlNode.InnerText;
            }

            xmlNode = singleUserNode.SelectSingleNode("./email");
            if (xmlNode != null)
            {
                this.Email = xmlNode.InnerText;
            }

            xmlNode = singleUserNode.SelectSingleNode("./screenName");
            if (xmlNode != null)
            {
                this.ScreenName = xmlNode.InnerText;
            }

            xmlNode = singleUserNode.SelectSingleNode("./lastAccess");
            if (xmlNode != null)
            {
                this.LastAccess = DateHelper.ParseDateTimeField(xmlNode.InnerText);
            }

            xmlNode = singleUserNode.SelectSingleNode("./developerAccount/aid");
            if (xmlNode != null)
            {
                this.AccountId = xmlNode.InnerText;
            }

            XmlNodeList roleNodes = singleUserNode.SelectNodes("./roles/role");
            this.Roles = RoleInfo.ParseRoles(roleNodes);
        }

        /// <summary>
        /// Gets the account id.
        /// </summary>
        public string AccountId { get; private set; }

        /// <summary>
        /// Gets the Last time the user accessed the system.
        /// </summary>
        public DateTime LastAccess { get; private set; }

        /// <summary>
        /// Gets the User name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the User Id.
        /// </summary>
        public string ScreenName { get; private set; }

        /// <summary>
        /// Gets the Email address.
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the same as ScreenName.
        /// </summary>
        public string Login { get; private set; }

        /// <summary>
        /// Gets the Last name.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// Gets the First name.
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the Internal Id.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets the List of roles assigned to this user.
        /// </summary>
        public Collection<RoleInfo> Roles
        {
            get
            {
                return this.roles;
            }

            private set
            {
                if (value != this.roles)
                {
                    this.roles = value;
                    this.roleNames = null;
                }
            }
        }

        /// <summary>
        /// Gets the names of all the roles this user has, comma-separated.
        /// </summary>
        public string RoleNames
        {
            get
            {
                return this.roleNames ?? (this.roleNames = this.BuildRoleNamesString());
            }
        }

        /// <summary>
        /// Parse multiple user information.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <returns>Returns the users.</returns>
        public static Collection<UserInfo> ParseUsers(XmlNodeList nodes)
        {
            Collection<UserInfo> userInfo = new Collection<UserInfo>();
            if (nodes != null)
            {
                foreach (XmlNode n in nodes)
                {
                    userInfo.Add(new UserInfo(n));
                }
            }

            return userInfo;
        }

        /// <summary>
        /// Under rare (and probably invalid) circumstances, a user can have a role that's
        /// actually not part of the application's regular role definitions.
        /// This function will give you a map of all the roles used by users in the list, using the role Id as the key.
        /// In most cases it will overlap 100% with the list of roles reported by GetRoleInfo. In the above described situation,
        /// it might contain more.
        /// </summary>
        /// <param name="uis">a UserInfo collection, in most cases a list of all users in an instance</param>
        /// <returns>Map of role id to RoleInfo object for all roles assigned to these users</returns>
        public static IDictionary<string, RoleInfo> ExtractRolesUsedByUsers(IEnumerable<UserInfo> uis)
        {
            var roles = new Dictionary<string, RoleInfo>();
            foreach (UserInfo ui in uis)
            {
                foreach (RoleInfo ri in ui.Roles)
                {
                    if (!roles.ContainsKey(ri.Id))
                    {
                        roles.Add(ri.Id, ri);
                    }
                }
            }

            return roles;
        }

        /// <summary>
        /// Uses the output of ExtractRolesUsedByUsers and a list of app-defined roles to extract a list of roles
        /// assigned to users that does not exist in the app definition.
        /// </summary>
        /// <param name="rolesOfUsers">The roles of users.</param>
        /// <param name="appDefinedRoles">The app defined roles.</param>
        /// <returns>Returns the roles info.</returns>
        public static IList<RoleInfo> DiffActualRolesFromAppRoles(IDictionary<string, RoleInfo> rolesOfUsers, IList<RoleInfo> appDefinedRoles)
        {
            IList<RoleInfo> diff = new List<RoleInfo>();
            foreach (RoleInfo ri1 in rolesOfUsers.Values)
            {
                bool foundMatchingRoleInAppsRoleDefinitions = false;
                foreach (RoleInfo ri2 in appDefinedRoles)
                {
                    if (ri1.Id.Equals(ri2.Id))
                    {
                        foundMatchingRoleInAppsRoleDefinitions = true;
                    }
                }

                if (!foundMatchingRoleInAppsRoleDefinitions)
                {
                    diff.Add(ri1);
                }
            }

            return diff;
        }

        /// <summary>
        /// Builds the role names string.
        /// </summary>
        /// <returns>Returns the role names.</returns>
        private string BuildRoleNamesString()
        {
            var roleNames = new StringBuilder();
            foreach (RoleInfo roleInfo in this.Roles)
            {
                if (roleNames.Length > 0)
                {
                    roleNames.Append(",");
                }

                roleNames.Append(roleInfo.Name);
            }

            return roleNames.ToString();
        }
    }
}
