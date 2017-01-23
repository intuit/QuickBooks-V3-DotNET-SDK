////*********************************************************
// <copyright file="SecurityElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Security element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Security element.
    /// </summary>
    public class SecurityElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the Security mode.
        /// </summary>
        [ConfigurationProperty("mode")]
        public SecurityMode Mode
        {
            get
            {
                SecurityMode mode;
                if (Enum.TryParse(this["mode"].ToString(), true, out mode))
                {
                    return mode;
                }

                return SecurityMode.None;
            }
        }

        /// <summary>
        /// Gets the OAuth element.
        /// </summary>
        [ConfigurationProperty("oauth")]
        public OAuthElement OAuth
        {
            get
            {
                return (OAuthElement)this["oauth"];
            }
        }



        /// <summary>
        /// Gets the custom security element.
        /// </summary>
        [ConfigurationProperty("customSecurity")]
        public CustomSecurityElement CustomSecurity
        {
            get
            {
                return (CustomSecurityElement)this["customSecurity"];
            }
        }
    }
}
