
////*********************************************************
// <copyright file="User.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains User class for platform calls </summary>
////*********************************************************


namespace Intuit.Ipp.PlatformService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// User class for PlatfromService
    /// </summary>
    public class User
    {
        /// <summary>
        /// FirstName
        /// </summary>

        public string FirstName { get; set; }

        /// <summary>
        /// LastName
        /// </summary>

        public string LastName{ get; set; }

        /// <summary>
        /// EmailAddress
        /// </summary>

        public string EmailAddress { get; set; }

        /// <summary>
        /// IsVerified
        /// </summary>

        public bool IsVerified { get; set;}
    }
}
