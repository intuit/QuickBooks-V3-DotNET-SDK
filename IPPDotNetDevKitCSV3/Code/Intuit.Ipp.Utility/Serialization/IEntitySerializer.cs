////*********************************************************
// <copyright file="IEntitySerializer.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains IEntity serialize contract.</summary>
////*********************************************************

namespace Intuit.Ipp.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Entity serialize contract.
    /// </summary>
    public interface IEntitySerializer
    {
        /// <summary>
        /// Serializes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Returns the serialize entity in string format.</returns>
        string Serialize(object entity);

        /// <summary>
        /// DeSerializes the message to Type T.
        /// </summary>
        /// <typeparam name="T">The type to be  serailse to</typeparam>
        /// <param name="message">The message.</param>
        /// <returns>
        /// Returns the deserialized message.
        /// </returns>
        object Deserialize<T>(string message);
    }
}
