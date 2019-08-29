////********************************************************************
// <copyright file="OAuth2RequestValidator.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Request validate contract.</summary>
////********************************************************************

namespace Intuit.Ipp.Security
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Net;
    using System.Net.Http;
    using System.Security.Cryptography;
    using System.Text.RegularExpressions;
    //using DevDefined.OAuth.Consumer;
    //using DevDefined.OAuth.Framework;
    using Intuit.Ipp.Exception;

    /// <summary>
    /// OAuth implementation for Request validate contract.
    /// </summary>
    public class OAuth2RequestValidator : IRequestValidator
    {
        /// <summary>
        /// The Authorization Header constant.
        /// </summary>
        private const string AuthorizationHeader = "Authorization";

        /// <summary>
        /// The O auth signature method.
        /// </summary>
        private string oauthSignatureMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="OAuth2RequestValidator"/> class.
        /// </summary>
        /// <param name="accessToken">The bearer access token.</param>
        public OAuth2RequestValidator(string accessToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new InvalidTokenException("Access token cannot be null or empty.");
            }
            
            if (!Regex.IsMatch(accessToken, @"^[\x20-\x7E]+$"))
            {
                throw new InvalidTokenException("Access token contains forbidden char.");
            }

            this.AccessToken = accessToken;
        }

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the additional parameters.
        /// </summary>
        /// <value>
        /// The additional parameters.
        /// </value>
        public NameValueCollection AdditionalParameters { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        /// <value>
        /// The asymmetric algorithm key.
        /// </value>
        public AsymmetricAlgorithm Key { get; set; }

        /// <summary>
        /// Authorizes the specified request.
        /// </summary>
        /// <param name="webRequest">The request.</param>
        /// <param name="requestBody">The requestBody if form encoded parameters.</param>
        public void Authorize(WebRequest webRequest, string requestBody)
        {
            //IOAuthConsumerContext consumerContext = this.CreateConsumerContext(true);
            //IOAuthSession oauthSession = CreateOAuthSessionWithConsumerContext(consumerContext);
            //oauthSession.AccessToken = this.CreateAccessToken();
            //string oauthHeader = this.GetOAuthHeaderForRequest(oauthSession, webRequest);
            string oauthHeader = string.Format("Bearer {0}", this.AccessToken);
            webRequest.Headers.Add(AuthorizationHeader, oauthHeader);
        }



        /// <summary>
        /// Authorizes the specified request.
        /// </summary>
        /// <param name="httpRequest">The request.</param>
        /// <param name="requestBody">The requestBody if form encoded parameters.</param>
        public void Authorize(HttpRequestMessage httpRequest, string requestBody)
        {
            string oauthHeader = string.Format("Bearer {0}", this.AccessToken);
           httpRequest.Headers.Add(AuthorizationHeader, oauthHeader);
        }
    }
}