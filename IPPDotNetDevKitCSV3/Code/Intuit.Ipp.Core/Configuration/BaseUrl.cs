////*********************************************************
// <copyright file="BaseUrl.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Base Url.</summary>
////*********************************************************

namespace Intuit.Ipp.Core.Configuration
{
    /// <summary>
    /// Base Urls for QBO and IPS.
    /// </summary>
    public class BaseUrl
    {
        

        /// <summary>
        /// Gets or sets the url for QuickBooks Online Rest Service.
        /// </summary>
        public string Qbo { get; set; }

        /// <summary>
        /// Gets or sets the url for Platform Rest Service.
        /// </summary>
        public string Ips { get; set; }

        /// <summary>
        /// Gets or sets the url for OAuth Authentication server.
        /// </summary>
        public string OAuthAccessTokenUrl { get; set; }

        /// <summary>
        /// Gets or sets the url for UserName Authentication server.
        /// </summary>
        public string UserNameAuthentication { get; set; }
    }
}
