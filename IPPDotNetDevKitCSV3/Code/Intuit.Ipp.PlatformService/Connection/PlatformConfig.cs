////*********************************************************
// <copyright file="PlatformConfig.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains PlatformConfig for platform URLs. These URLs can be reset by users. </summary>
////*********************************************************
namespace Intuit.Ipp.PlatformService
{

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    /// <summary>
    /// This class contains platform serveice URLs which can be reset by users.
    /// </summary>
    class PlatformConfig
    {
        /// <summary>
        /// The Disconnect URL.
        /// </summary>
        private static string disconnectUrl = "https://appcenter.intuit.com/api/v1/connection/disconnect";
        /// <summary>
        /// The Get Current User URL.
        /// </summary>
        private static string currentUserUrl = "https://appcenter.intuit.com/api/v1/user/current";
        /// <summary>
        /// The Reconnect URL.
        /// </summary>
        private static string reconnectUrl = "https://appcenter.intuit.com/api/v1/connection/reconnect";

        /// <summary>
        /// Get the disconnect URL.
        /// </summary>
        public static string GetDisconnectUrl() {
            return disconnectUrl;
        }
        /// <summary>
        /// Set the disconnect URL.
        /// </summary>
        public static void SetDisconnectUrl(string url)
        {
            disconnectUrl = url;
        }
        /// <summary>
        /// Get the current user URL.
        /// </summary>
        public static string GetCurrentUserUrl()
        {
            return currentUserUrl;
        }
        /// <summary>
        /// Set the current user URL.
        /// </summary>
        public static void SetCurrentUserUrl(string url)
        {
            currentUserUrl = url;
        }
        /// <summary>
        /// Get the reconnect URL.
        /// </summary>
        public static string GetReconnectUrl()
        {
            return reconnectUrl;
        }
        /// <summary>
        /// Set the reconnect URL.
        /// </summary>
        public static void SetReconnectUrl(string url)
        {
            reconnectUrl = url;
        }

    }
}
