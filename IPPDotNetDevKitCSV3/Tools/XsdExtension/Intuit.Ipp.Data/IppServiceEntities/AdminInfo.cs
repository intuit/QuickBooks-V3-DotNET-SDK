////*********************************************************
// <copyright file="AdminInfo.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains class that encapsulates subscriber information as returned by API_GetAdminsForAllProducts.</summary>
////*********************************************************
namespace Intuit.Ipp.Data 
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Reflection;
    using System.Xml;
    using Intuit.Ipp.Utility;

    /// <summary>
    /// Encapsulates subscriber information as returned by API_GetAdminsForAllProducts.
    /// </summary>
    public class AdminInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminInfo"/> class.
        /// </summary>
        /// <param name="singleUserNode">The Xml node.</param>
        internal AdminInfo(XmlNode singleUserNode)
        {
            XmlNode n = singleUserNode.SelectSingleNode("./uid");
            if (n != null)
            {
                this.Uid = n.InnerText;
            }

            n = singleUserNode.SelectSingleNode("./firstName");
            if (n != null)
            {
                this.FirstName = n.InnerText;
            }

            n = singleUserNode.SelectSingleNode("./lastName");
            if (n != null)
            {
                this.LastName = n.InnerText;
            }

            n = singleUserNode.SelectSingleNode("./email");
            if (n != null)
            {
                this.Email = n.InnerText;
            }

            n = singleUserNode.SelectSingleNode("./productId");
            if (n != null)
            {
                this.ProductID = n.InnerText;
            }

            n = singleUserNode.SelectSingleNode("./applicationName");
            if (n != null)
            {
                this.ApplicationName = n.InnerText;
            }

            n = singleUserNode.SelectSingleNode("./applicationId");
            if (n != null)
            {
                this.ApplicationId = n.InnerText;
            }
        }

        #region Properties

        /// <summary>
        /// Gets the uid.
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string Uid { get; private set; }

        /// <summary>
        /// Gets the first name.
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string FirstName { get; private set; }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string LastName { get; private set; }

        /// <summary>
        /// Gets the Email address
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string Email { get; private set; }

        /// <summary>
        /// Gets the Product Id
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string ProductID { get; private set; }

        /// <summary>
        /// Gets the Application name
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string ApplicationName { get; private set; }

        /// <summary>
        /// Gets the Application Id
        /// </summary>
        [Obfuscation(Exclude = true, Feature = "renaming", StripAfterObfuscation = true)]
        [Obfuscation(Exclude = false, Feature = "trigger", StripAfterObfuscation = true)]
        public string ApplicationId { get; private set; }

        #endregion

        /// <summary>
        /// Parses the xml node and returns collections of admin information.
        /// </summary>
        /// <param name="node">The xml node.</param>
        /// <returns>Returns the collection of admin info objects.</returns>
        public static Collection<AdminInfo> ParseAdmins(XmlNode node)
        {
            Collection<AdminInfo> adminCollections = new Collection<AdminInfo>();
            XmlNodeList admins = node.SelectNodes("./admins/admin");
            if (admins != null)
            {
                foreach (XmlNode admin in admins)
                {
                    adminCollections.Add(new AdminInfo(admin));
                }
            }

            return adminCollections;
        }
    }
}
