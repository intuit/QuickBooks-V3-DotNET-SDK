// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// Modified for Intuit's Oauth2 implementation

using Intuit.Ipp.OAuth2PlatformClient.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Reflection;
using System.Linq;
using Intuit.Ipp.Core.Rest;
using Intuit.Ipp.Exception;
using System.IO;
using System.Globalization;

namespace Intuit.Ipp.OAuth2PlatformClient
{   /// <summary>
    /// 
    /// </summary>
    public class OAuth2Client
    {
        /// <summary>
        /// Advanced Logger for OAuth2 calls
        /// </summary>
        public static AdvancedLogging AdvancedLogger;
        

        /// <summary>
        /// request logging location.
        /// </summary>
        private string serviceRequestLoggingLocationForFile;

        /// <summary>
        /// request Azure Document DB url.
        /// </summary>
        private Uri serviceRequestAzureDocumentDBUrl;

        /// <summary>
        /// request Azure Document DB Secure Key
        /// </summary>
        private string serviceRequestAzureDocumentDBSecureKey;

        ///// <summary>
        ///// request TTL-time to live for all logs 
        ///// </summary>
        //public double ServiceRequestAzureDocumentDBTTL { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable reqeust response logging for Debug logs.
        /// </summary>
        public bool EnableSerilogRequestResponseLoggingForDebug { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether to enable reqeust response logging for Trace logs.
        /// </summary>
        public bool EnableSerilogRequestResponseLoggingForTrace { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether to enable reqeust response logging for Console logs.
        /// </summary>
        public bool EnableSerilogRequestResponseLoggingForConsole { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether to enable reqeust response logging for Rolling logs.
        /// </summary>
        public bool EnableSerilogRequestResponseLoggingForRollingFile { get; set; }



        ///// <summary>
        ///// Gets or sets a value indicating whether to enable reqeust response logging for Rolling logs.
        ///// </summary>
        //public bool EnableSerilogRequestResponseLoggingForFile { get; set; }


        ///// <summary>
        ///// Gets or sets a value indicating whether to enable reqeust response logging for Azure Doc DB logs.
        ///// </summary>
        //public bool EnableSerilogRequestResponseLoggingForAzureDocumentDB { get; set; }




        /// <summary>
        /// Gets or sets the service request logging location for File, Rolling File.
        /// </summary>
        public string ServiceRequestLoggingLocationForFile
        {
            get
            {
                return this.serviceRequestLoggingLocationForFile;
            }

            set
            {
                if (!Directory.Exists(value))
                {
                    IdsException exception = new IdsException(Properties.Resources.ValidDirectoryPathMessage, new DirectoryNotFoundException());
                    IdsExceptionManager.HandleException(exception);
                }

                this.serviceRequestLoggingLocationForFile = value;
            }
        }

        ///// <summary>
        ///// Gets or sets the service request logging location for File, Rolling File.
        ///// </summary>
        //public Uri ServiceRequestAzureDocumentDBUrl
        //{
        //    get
        //    {
        //        return this.serviceRequestAzureDocumentDBUrl;
        //    }

        //    set
        //    {
        //        if (EnableSerilogRequestResponseLoggingForAzureDocumentDB == true)
        //        {
        //            if (value == null)
        //            {
        //                IdsException exception = new IdsException(Properties.Resources.AzureDocumentDBUrlNullEmptyMessage, new ArgumentNullException());
        //                IdsExceptionManager.HandleException(exception);
        //            }
        //        }

        //        this.serviceRequestAzureDocumentDBUrl = value;

        //    }
        //}

        ///// <summary>
        ///// Gets or sets the service request logging location for File, Rolling File.
        ///// </summary>
        //public string ServiceRequestAzureDocumentDBSecureKey
        //{
        //    get
        //    {
        //        return this.serviceRequestAzureDocumentDBSecureKey;
        //    }

        //    set
        //    {
        //        if (EnableSerilogRequestResponseLoggingForAzureDocumentDB == true)
        //        {
        //            if (value == null && value == "")
        //            {
        //                IdsException exception = new IdsException(Properties.Resources.AzureDocumentDBSecureKeyNullEmptyMessage, new ArgumentNullException());
        //                IdsExceptionManager.HandleException(exception);
        //            }
        //        }

        //        this.serviceRequestAzureDocumentDBSecureKey = value;
        //    }
        //}


        /// <summary>
        /// ClientId
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// ClientSecret
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// RedirectUri
        /// </summary>
        public string RedirectURI { get; set; }

        /// <summary>
        /// DiscoveryDoc
        /// </summary>
        public DiscoveryResponse DiscoveryDoc { get; set; }

        /// <summary>
        /// AppEnvironment
        /// </summary>
        public AppEnvironment ApplicationEnvironment { get; set; }

        /// <summary>
        /// CSRFToken
        /// </summary>
        public string CSRFToken { get; set; }

        /// <summary>
        /// DiscoveryUrl
        /// </summary>
        public string DiscoveryUrl { get; set; }



        /// <summary>
        /// OAuth2Client constructor
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectURI"></param>
        /// <param name="environment">This can either be sandbox, production or an actual discovery url</param>
        public OAuth2Client(string clientID, string clientSecret, string redirectURI, string environment)
        {
            ClientID = clientID ?? throw new ArgumentNullException(nameof(clientID));
            ClientSecret = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
            RedirectURI = redirectURI ?? throw new ArgumentNullException(nameof(redirectURI));
            if (environment != null && environment != "")
            {
                try
                {
                    ApplicationEnvironment = (AppEnvironment)Enum.Parse(typeof(AppEnvironment), environment, true);
                }
                catch (System.Exception ex)
                {
                    ApplicationEnvironment = AppEnvironment.Default;
                    DiscoveryUrl = environment;
                }
          

             }
               
           

            DiscoveryDoc = GetDiscoveryDoc();

        }


        /// <summary>
        /// Gets Discovery Doc
        /// </summary>
        /// <returns></returns>
        public DiscoveryResponse GetDiscoveryDoc()
        {
            


            DiscoveryClient discoveryClient;
            if (ApplicationEnvironment == AppEnvironment.Default)
            {
                discoveryClient = new DiscoveryClient(DiscoveryUrl);
            }
            else
            {
                discoveryClient = new DiscoveryClient(ApplicationEnvironment);
            }
            return discoveryClient.Get();
        }

        /// <summary>
        /// Get Authorization Url
        /// </summary>
        /// <param name="scopes"></param>
        /// <param name="CSRFToken"></param>
        /// <returns></returns>
        public string GetAuthorizationURL(List<OidcScopes> scopes, string CSRFToken)
        {
            //Intialize Logger
            AdvancedLogger = LogHelper.GetAdvancedLogging(enableSerilogRequestResponseLoggingForDebug: this.EnableSerilogRequestResponseLoggingForDebug, enableSerilogRequestResponseLoggingForTrace: this.EnableSerilogRequestResponseLoggingForTrace, enableSerilogRequestResponseLoggingForConsole: this.EnableSerilogRequestResponseLoggingForConsole, enableSerilogRequestResponseLoggingForRollingFile: this.EnableSerilogRequestResponseLoggingForRollingFile, serviceRequestLoggingLocationForFile: this.ServiceRequestLoggingLocationForFile);

            string scopeValue = "";
            for (var index = 0; index < scopes.Count; index++)
            {
                scopeValue += scopes[index].GetStringValue() + " ";
            }
            scopeValue = scopeValue.TrimEnd();
            this.CSRFToken = CSRFToken;

            //builiding authorization request
            string authorizationRequest = string.Format("{0}?client_id={1}&response_type=code&scope={2}&redirect_uri={3}&state={4}",
                DiscoveryDoc.AuthorizeEndpoint,
                ClientID,
                Uri.EscapeDataString(scopeValue),
                Uri.EscapeDataString(RedirectURI),
                CSRFToken);

            //Logging authorization request
            AdvancedLogger.Log("Logging AuthorizationRequest:" + authorizationRequest);
            
            return authorizationRequest;
        }

        /// <summary>
        /// Get Authorization Url
        /// </summary>
        /// <param name="scopes"></param>
        /// <returns></returns>
        public string GetAuthorizationURL(List<OidcScopes> scopes)
        {
            //Intialize Logger
            AdvancedLogger = LogHelper.GetAdvancedLogging(enableSerilogRequestResponseLoggingForDebug: this.EnableSerilogRequestResponseLoggingForDebug, enableSerilogRequestResponseLoggingForTrace: this.EnableSerilogRequestResponseLoggingForTrace, enableSerilogRequestResponseLoggingForConsole: this.EnableSerilogRequestResponseLoggingForConsole, enableSerilogRequestResponseLoggingForRollingFile: this.EnableSerilogRequestResponseLoggingForRollingFile, serviceRequestLoggingLocationForFile: this.ServiceRequestLoggingLocationForFile);


            string scopeValue = "";
            for (var index = 0; index < scopes.Count; index++)
            {
                scopeValue += scopes[index].GetStringValue() + " ";
            }
            scopeValue = scopeValue.TrimEnd();

            //creating CSRF token since client did not send one
            CSRFToken = GenerateCSRFToken();

            //builiding authorization request
            string authorizationRequest = string.Format("{0}?client_id={1}&response_type=code&scope={2}&redirect_uri={3}&state={4}",
                DiscoveryDoc.AuthorizeEndpoint,
                ClientID,
                Uri.EscapeDataString(scopeValue),
                Uri.EscapeDataString(RedirectURI),
                CSRFToken);

            //Logging authorization request
            AdvancedLogger.Log("Logging AuthorizationRequest:" + authorizationRequest);
            return authorizationRequest;
        }

        #region realmId changes to be included in later release

        ///// <summary>
        ///// Get Authorization Url
        ///// </summary>
        ///// <param name="scopes"></param>
        ///// <param name="CSRFToken"></param>
        ///// <param name="getRealmId"></param>
        ///// <returns></returns>
        //public string GetAuthorizationURL(List<OidcScopes> scopes, string CSRFToken, bool getRealmId )
        //{
        //    string scopeValue = "";
        //    string realmIdJson = OidcConstants.AuthorizeRequest.IdToken_ReamId;
        //    string authorizationRequest = "";
        //    for (var index = 0; index < scopes.Count; index++)
        //    {
        //        scopeValue += scopes[index].GetStringValue() + " ";
        //    }
        //    scopeValue = scopeValue.TrimEnd();
        //    this.CSRFToken = CSRFToken;
        //    if (getRealmId == true)
        //    {
        //        authorizationRequest = string.Format("{0}?client_id={1}&response_type=code&scope={2}&redirect_uri={3}&state={4}&claims={5}",
        //            DiscoveryDoc.AuthorizeEndpoint,
        //            ClientID,
        //            Uri.EscapeDataString(scopeValue),
        //            Uri.EscapeDataString(RedirectURI),
        //            Uri.EscapeDataString(realmIdJson),
        //            CSRFToken);
        //    }
        //    else
        //    {
        //        authorizationRequest = string.Format("{0}?client_id={1}&response_type=code&scope={2}&redirect_uri={3}&state={4}",
        //            DiscoveryDoc.AuthorizeEndpoint,
        //            ClientID,
        //            Uri.EscapeDataString(scopeValue),
        //            Uri.EscapeDataString(RedirectURI),
        //            CSRFToken);
        //    }
        //    return authorizationRequest;
        //}

        ///// <summary>
        ///// Get Authorization Url
        ///// </summary>
        ///// <param name="scopes"></param>
        ///// <param name="CSRFToken"></param>
        ///// <param name="getRealmId"></param>
        ///// <returns></returns>
        //public string GetAuthorizationURL(List<OidcScopes> scopes, bool getRealmId)
        //{
        //    string scopeValue = "";
        //    string realmIdJson = OidcConstants.AuthorizeRequest.IdToken_ReamId;
        //    string authorizationRequest = "";
        //    for (var index = 0; index < scopes.Count; index++)
        //    {
        //        scopeValue += scopes[index].GetStringValue() + " ";
        //    }
        //    scopeValue = scopeValue.TrimEnd();
        //    this.CSRFToken = GenerateCSRFToken();
        //    if (getRealmId == true)
        //    {
        //        authorizationRequest = string.Format("{0}?client_id={1}&response_type=code&scope={2}&redirect_uri={3}&state={4}&claims={5}",
        //            DiscoveryDoc.AuthorizeEndpoint,
        //            ClientID,
        //            Uri.EscapeDataString(scopeValue),
        //            Uri.EscapeDataString(RedirectURI),
        //            Uri.EscapeDataString(realmIdJson),
        //            CSRFToken);
        //    }
        //    else
        //    {
        //        authorizationRequest = string.Format("{0}?client_id={1}&response_type=code&scope={2}&redirect_uri={3}&state={4}",
        //            DiscoveryDoc.AuthorizeEndpoint,
        //            ClientID,
        //            Uri.EscapeDataString(scopeValue),
        //            Uri.EscapeDataString(RedirectURI),
        //            CSRFToken);
        //    }
        //    return authorizationRequest;
        //}


        ///// <summary>
        ///// Validates ID token
        ///// </summary>
        ///// <param name="idToken"></param>
        ///// <returns></returns>
        //public string GetRealmIdFromIDTokenAsync(string idToken)
        //{

        //    if (idToken != null)
        //    {
        //        string[] splitValues = idToken.Split('.');


        //        if (splitValues[1] != null)
        //        {
        //            var payloadJson = Encoding.UTF8.GetString(Base64Url.Decode(splitValues[1].ToString()));
        //            IdTokenJWTClaimTypes payloadData = JsonConvert.DeserializeObject<IdTokenJWTClaimTypes>(payloadJson);

        //            if (payloadData.RealmId != null)
        //            {
        //                return payloadData.RealmId.ToString();
        //            }

        //        }

        //    }

        //    return null;

        //}

        #endregion



        /// <summary>
        /// Gets Bearer token from Authorization code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TokenResponse> GetBearerTokenAsync(string code, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Intialize Logger
            AdvancedLogger = LogHelper.GetAdvancedLogging(enableSerilogRequestResponseLoggingForDebug: this.EnableSerilogRequestResponseLoggingForDebug, enableSerilogRequestResponseLoggingForTrace: this.EnableSerilogRequestResponseLoggingForTrace, enableSerilogRequestResponseLoggingForConsole: this.EnableSerilogRequestResponseLoggingForConsole, enableSerilogRequestResponseLoggingForRollingFile: this.EnableSerilogRequestResponseLoggingForRollingFile, serviceRequestLoggingLocationForFile: this.ServiceRequestLoggingLocationForFile);

            var tokenClient = new TokenClient(DiscoveryDoc.TokenEndpoint, ClientID, ClientSecret);
            return await tokenClient.RequestTokenFromCodeAsync(code, RedirectURI, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Refreshes access token to get new access token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="extra"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken, object extra = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Intialize Logger
            AdvancedLogger = LogHelper.GetAdvancedLogging(enableSerilogRequestResponseLoggingForDebug: this.EnableSerilogRequestResponseLoggingForDebug, enableSerilogRequestResponseLoggingForTrace: this.EnableSerilogRequestResponseLoggingForTrace, enableSerilogRequestResponseLoggingForConsole: this.EnableSerilogRequestResponseLoggingForConsole, enableSerilogRequestResponseLoggingForRollingFile: this.EnableSerilogRequestResponseLoggingForRollingFile, serviceRequestLoggingLocationForFile: this.ServiceRequestLoggingLocationForFile);

            var tokenClient = new TokenClient(DiscoveryDoc.TokenEndpoint, ClientID, ClientSecret);
            return await tokenClient.RequestRefreshTokenAsync(refreshToken, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Revoke token using either access or refresh token
        /// </summary>
        /// <param name="accessOrRefreshToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TokenRevocationResponse> RevokeTokenAsync(string accessOrRefreshToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Intialize Logger
            AdvancedLogger = LogHelper.GetAdvancedLogging(enableSerilogRequestResponseLoggingForDebug: this.EnableSerilogRequestResponseLoggingForDebug, enableSerilogRequestResponseLoggingForTrace: this.EnableSerilogRequestResponseLoggingForTrace, enableSerilogRequestResponseLoggingForConsole: this.EnableSerilogRequestResponseLoggingForConsole, enableSerilogRequestResponseLoggingForRollingFile: this.EnableSerilogRequestResponseLoggingForRollingFile, serviceRequestLoggingLocationForFile: this.ServiceRequestLoggingLocationForFile);

            TokenRevocationClient revokeTokenClient = new TokenRevocationClient(DiscoveryDoc.RevocationEndpoint, ClientID, ClientSecret);
            return await revokeTokenClient.RevokeAsync(new TokenRevocationRequest
            {
                Token = accessOrRefreshToken,
            }, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Get User Info
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UserInfoResponse> GetUserInfoAsync(string accessToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            //Intialize Logger
            AdvancedLogger = LogHelper.GetAdvancedLogging(enableSerilogRequestResponseLoggingForDebug: this.EnableSerilogRequestResponseLoggingForDebug, enableSerilogRequestResponseLoggingForTrace: this.EnableSerilogRequestResponseLoggingForTrace, enableSerilogRequestResponseLoggingForConsole: this.EnableSerilogRequestResponseLoggingForConsole, enableSerilogRequestResponseLoggingForRollingFile: this.EnableSerilogRequestResponseLoggingForRollingFile, serviceRequestLoggingLocationForFile: this.ServiceRequestLoggingLocationForFile);

            UserInfoClient userInfoClient = new UserInfoClient(DiscoveryDoc.UserInfoEndpoint);
            return await userInfoClient.GetAsync(accessToken, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Validates ID token
        /// </summary>
        /// <param name="idToken"></param>
        /// <returns></returns>
        public Task<bool> ValidateIDTokenAsync(String idToken)
        {
            IList<JsonWebKey> keys = DiscoveryDoc.KeySet.Keys;
            string mod = "";
            string exponent = "";
            if (keys != null)
            {
                foreach (var key in keys)
                {
                    if (key.N != null)
                    {
                        mod = key.N;
                    }
                    if (key.N != null)
                    {
                        exponent = key.E;
                    }
                }

                if (idToken != null)
                {
                    string[] splitValues = idToken.Split('.');
                    if (splitValues[0] != null)
                    {
                        var headerJson = Encoding.UTF8.GetString(Base64Url.Decode(splitValues[0].ToString()));
                        IdTokenHeader headerData = JsonConvert.DeserializeObject<IdTokenHeader>(headerJson);

                        if (headerData.Kid == null)
                        {
                            return Task.FromResult(false);
                        }

                        if (headerData.Alg == null)
                        {
                            return Task.FromResult(false);
                        }
                    }

                    if (splitValues[1] != null)
                    {
                        var payloadJson = Encoding.UTF8.GetString(Base64Url.Decode(splitValues[1].ToString()));
                        IdTokenJWTClaimTypes payloadData = JsonConvert.DeserializeObject<IdTokenJWTClaimTypes>(payloadJson);

                        if (payloadData.Aud != null)
                        {
                            if (payloadData.Aud[0].ToString() != ClientID)
                            {
                                return Task.FromResult(false);
                            }
                        }
                        else
                        {
                            return Task.FromResult(false);
                        }

                        if (payloadData.Auth_time == null)
                        {
                            return Task.FromResult(false);
                        }

                        if (payloadData.Exp != null)
                        {
                            long expiration = Convert.ToInt64(payloadData.Exp);
                            long currentEpochTime = EpochTimeExtensions.ToEpochTime(DateTime.UtcNow);

                            if ((expiration - currentEpochTime) <= 0)
                            {
                                return Task.FromResult(false);
                            }
                        }

                        if (payloadData.Iat == null)
                        {
                            return Task.FromResult(false);
                        }

                        if (payloadData.Iss != null)
                        {
                            if (payloadData.Iss.ToString() != DiscoveryDoc.Issuer)
                            {

                                return Task.FromResult(false);
                            }
                        }
                        else
                        {
                            return Task.FromResult(false);
                        }

                        if (payloadData.Sub == null)
                        {
                            return Task.FromResult(false);
                        }
                    }

                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                    rsa.ImportParameters(
                      new RSAParameters()
                      {
                          Modulus = Base64Url.Decode(mod),
                          Exponent = Base64Url.Decode(exponent)
                      });

                    SHA256 sha256 = SHA256.Create();
                    byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(splitValues[0] + '.' + splitValues[1]));

                    RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                    rsaDeformatter.SetHashAlgorithm("SHA256");
                    if (rsaDeformatter.VerifySignature(hash, Base64Url.Decode(splitValues[2])))
                    {
                        return Task.FromResult(true);
                    }
                }
            }
            return Task.FromResult(false);
        }

        

        /// <summary>
        /// Generate random CSRF token
        /// </summary>
        /// <returns></returns>
        public string GenerateCSRFToken()
        {
            return CryptoRandom.CreateUniqueId();
        }
    }
}