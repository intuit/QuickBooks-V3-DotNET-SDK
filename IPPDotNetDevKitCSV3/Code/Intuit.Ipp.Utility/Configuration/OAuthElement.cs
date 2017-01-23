////*********************************************************
// <copyright file="OAuthElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains oAuth element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System.Configuration;

    /// <summary>
    /// OAuth element.
    /// </summary>
    public class OAuthElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the Access Token.
        /// </summary>
        [ConfigurationProperty("accessToken")]
        public string AccessToken
        {
            get
            {
                return this["accessToken"].ToString();
            }
        }

        /// <summary>
        /// Gets the Access Token Secret.
        /// </summary>
        [ConfigurationProperty("accessTokenSecret")]
        public string AccessTokenSecret
        {
            get
            {
                return this["accessTokenSecret"].ToString();
            }
        }

        /// <summary>
        /// Gets the Consumer Key.
        /// </summary>
        [ConfigurationProperty("consumerKey")]
        public string ConsumerKey
        {
            get
            {
                return this["consumerKey"].ToString();
            }
        }

        /// <summary>
        /// Gets the Consumer Secret.
        /// </summary>
        [ConfigurationProperty("consumerSecret")]
        public string ConsumerSecret
        {
            get
            {
                return this["consumerSecret"].ToString();
            }
        }
    }
}
