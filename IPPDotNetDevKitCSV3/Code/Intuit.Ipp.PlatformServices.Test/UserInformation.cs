////*********************************************************
// <copyright file="UserInformation.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains class wraps user information </summary>
////*********************************************************

namespace Intuit.Ipp.PlatformServices.Test
{
    /// <summary>
    /// User Information
    /// </summary>
    internal class UserInformation
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        internal string Email { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        internal string UserId { get; set; }

        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        /// <value>
        /// The role id.
        /// </value>
        internal string RoleId { get; set; }
    }
}
