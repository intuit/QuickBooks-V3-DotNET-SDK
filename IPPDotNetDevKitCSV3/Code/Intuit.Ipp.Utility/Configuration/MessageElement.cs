////*********************************************************
// <copyright file="MessageElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Message element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Configuration;

    /// <summary>
    /// Message element.
    /// </summary>
    public class MessageElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the Request element.
        /// </summary>
        [ConfigurationProperty("request")]
        public RequestElement Request
        {
            get
            {
                return (RequestElement)this["request"];
            }
        }

        /// <summary>
        /// Gets the Response element.
        /// </summary>
        [ConfigurationProperty("response")]
        public ResponseElement Response
        {
            get
            {
                return (ResponseElement)this["response"];
            }
        }

        /// <summary>
        /// Gets the CustomSerializer element.
        /// </summary>
        [ConfigurationProperty("customSerializer")]
        public CustomSerializerElement CustomSerializer
        {
            get
            {
                return (CustomSerializerElement)this["customSerializer"];
            }
        }
    }
}
