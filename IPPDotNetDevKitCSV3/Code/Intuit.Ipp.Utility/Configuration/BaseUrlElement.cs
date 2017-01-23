////*********************************************************
// <copyright file="BaseUrlElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains base url element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System.Configuration;

    /// <summary>
    /// Base url element.
    /// </summary>
    public class BaseUrlElement : ConfigurationElement
    {
       
        /// <summary>
        /// Gets Url for QuickBooks Online Rest Service.
        /// </summary>
        [ConfigurationProperty("qbo")]
        public string Qbo
        {
            get
            {
                return this["qbo"].ToString();
            }
        }

        /// <summary>
        /// Gets Url for Platform Rest Service.
        /// </summary>
        [ConfigurationProperty("ips")]
        public string Ips
        {
            get
            {
                return this["ips"].ToString();
            }
        }

        /// <summary>
        /// Gets Url for OAuth Authentication server.
        /// </summary>
        [ConfigurationProperty("oAuthAccessTokenUrl")]
        public string OAuthAccessTokenUrl
        {
            get
            {
                return this["oAuthAccessTokenUrl"].ToString();
            }
        }

        /// <summary>
        /// Gets Url for UserName Authentication server.
        /// </summary>
        [ConfigurationProperty("userNameAuthentication")]
        public string UserNameAuthentication
        {
            get
            {
                return this["userNameAuthentication"].ToString();
            }
        }
    }
}
