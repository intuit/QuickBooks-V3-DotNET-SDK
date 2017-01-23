////********************************************************************
// <copyright file="IRequestValidator.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Request validate contract.</summary>
////********************************************************************

namespace Intuit.Ipp.Security
{
    using System.Net;

    /// <summary>
    /// Interface for request validate
    /// </summary>
    public interface IRequestValidator
    {
        /// <summary>
        /// Authorizes the web request.
        /// </summary>
        /// <param name="webRequest">The web request.</param>
        /// <param name="requestBody">The request body.</param>
        void Authorize(WebRequest webRequest, string requestBody);
    }
}
