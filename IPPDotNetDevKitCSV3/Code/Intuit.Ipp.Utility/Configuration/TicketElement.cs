////*********************************************************
// <copyright file="TicketElement.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains ticket element.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System.Configuration;

    /// <summary>
    /// Ticket element.
    /// </summary>
    public class TicketElement : ConfigurationElement
    {
        /// <summary>
        /// Gets the ticket value.
        /// </summary>
        [ConfigurationProperty("ticket")]
        public string Ticket
        {
            get
            {
                return this["ticket"].ToString();
            }
        }
    }
}
