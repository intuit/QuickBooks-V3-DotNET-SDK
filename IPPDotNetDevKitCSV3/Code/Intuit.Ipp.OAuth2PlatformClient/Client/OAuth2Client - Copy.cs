// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// Modified for Intuit's Oauth2 implementation


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
using IdentityModel.Client;
using IdentityModel;



namespace Intuit.Ipp.OAuth2PlatformClient
{   /// <summary>
    /// 
    /// </summary>
    public class OAuth2Client
    {   
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
        public string CSRFToken {get; set;}

        /// <summary>
        /// OAuth2Client constructor
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="clientSecret"></param>
        /// <param name="redirectURI"></param>
        /// <param name="environment"></param>
        public OAuth2Client(string clientID, string clientSecret, string redirectURI, string environment)
        {
            ClientID = clientID ?? throw new ArgumentNullException(nameof(clientID));
            ClientSecret = clientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
            RedirectURI = redirectURI ?? throw new ArgumentNullException(nameof(redirectURI)); 
            ApplicationEnvironment = (AppEnvironment)Enum.Parse(typeof(AppEnvironment), environment, true) ;
            //DiscoveryDoc = GetDiscoveryDoc();
            string appEnvironment = ApplicationEnvironment.ToString();
            DiscoveryDoc = GetDiscoveryDoc(appEnvironment);
        }

        /// <summary>
        /// Gets Discovery Doc
        /// </summary>
        /// <returns>DiscoverResponse</returns>
        public DiscoveryResponse GetDiscoveryDoc(string appEnvironment)
        {
            string discoveryUrl = "";
            string discoveryAuthority = "";
            if (appEnvironment == AppEnvironment.Production.ToString())
            {
                discoveryUrl = OidcConstants.Discovery.ProdDiscoveryEndpoint;
                discoveryAuthority = OidcConstants.Discovery.ProdAuthority;

            }
            else if (appEnvironment == AppEnvironment.E2EProduction.ToString())
            {
                discoveryUrl = OidcConstants.Discovery.E2EProdDiscoveryEndpoint;
                discoveryAuthority = OidcConstants.Discovery.E2EAuthority; ;
            }
            else if (appEnvironment == AppEnvironment.E2ESandbox.ToString())
            {
                discoveryUrl = OidcConstants.Discovery.E2ESandboxDiscoveryEndpoint;
                discoveryAuthority = OidcConstants.Discovery.E2EAuthority;
            }
            else
            {
                discoveryUrl = OidcConstants.Discovery.SandboxDiscoveryEndpoint;
                discoveryAuthority = OidcConstants.Discovery.ProdAuthority;
            }

            HttpClient discoveryClient = new HttpClient();
            HttpResponseMessage response = discoveryClient.GetAsync(discoveryAuthority+discoveryUrl).Result;

            //IdentityModel.Client.DiscoveryPolicy policy1 = new IdentityModel.Client.DiscoveryPolicy();
            //policy.Authority = discoveryAuthority;
            //policy.ValidateIssuerName = false;
            //policy.ValidateEndpoints = false;

            DiscoveryPolicy policy = new DiscoveryPolicy();
            policy.Authority = discoveryAuthority;
            policy.ValidateIssuerName = false;
            policy.ValidateEndpoints = false;

            //Parse raw response to DiscoveryResponse
            //IdentityModel.Client.DiscoveryResponse discoveryResponse = new IdentityModel.Client.DiscoveryResponse(response.Content.ReadAsStringAsync().Result, policy);
            DiscoveryResponse discoveryResponse = new DiscoveryResponse(response.Content.ReadAsStringAsync().Result, policy);
            return discoveryResponse;
        }

        /// <summary>
        /// Get Authorization Url
        /// </summary>
        /// <param name="scopes"></param>
        /// <param name="CSRFToken"></param>
        /// <returns>string</returns>
        public string GetAuthorizationURL(List<OidcScopes> scopes, string CSRFToken)
        {
            string scopeValue = "";
            for (var index = 0; index < scopes.Count; index++)
            {
                scopeValue += scopes[index].GetStringValue() + " ";
            }
            scopeValue = scopeValue.TrimEnd();
            this.CSRFToken = CSRFToken;
            string authorizationRequest = string.Format("{0}?client_id={1}&response_type=code&scope={2}&redirect_uri={3}&state={4}",
                DiscoveryDoc.AuthorizeEndpoint,
                ClientID,
                Uri.EscapeDataString(scopeValue),
                Uri.EscapeDataString(RedirectURI),
                CSRFToken);
            return authorizationRequest;
        }

        /// <summary>
        /// Get Authorization Url
        /// </summary>
        /// <param name="scopes"></param>
        /// <returns>string</returns>
        public string GetAuthorizationURL(List<OidcScopes> scopes)
        {
            string scopeValue = "";
            for (var index = 0; index < scopes.Count; index++)
            {
                scopeValue += scopes[index].GetStringValue() + " ";
            }
            scopeValue = scopeValue.TrimEnd();

            CSRFToken = GenerateCSRFToken();
            string authorizationRequest = string.Format("{0}?client_id={1}&response_type=code&scope={2}&redirect_uri={3}&state={4}",
                DiscoveryDoc.AuthorizeEndpoint,
                ClientID,
                Uri.EscapeDataString(scopeValue),
                Uri.EscapeDataString(RedirectURI),
                CSRFToken);
            return authorizationRequest;
        }

        /// <summary>
        /// Gets Bearer token from Authorization code
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>TokenResponse</returns>
        public async Task<TokenResponse> GetBearerTokenAsync(string code, CancellationToken cancellationToken = default(CancellationToken))
        {
            var tokenClient = new IdentityModel.Client.TokenClient(DiscoveryDoc.TokenEndpoint, ClientID, ClientSecret);
            var res = await tokenClient.RequestAuthorizationCodeAsync(code, RedirectURI, cancellationToken: cancellationToken);
            return (TokenResponse)res;

        }

        /// <summary>
        /// Refreshes access token to get new access token
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="extra"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>TokenResponse</returns>
        public async Task<TokenResponse> RefreshTokenAsync(string refreshToken, object extra = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var tokenClient = new IdentityModel.Client.TokenClient(DiscoveryDoc.TokenEndpoint, ClientID, ClientSecret);
            return (TokenResponse)await tokenClient.RequestRefreshTokenAsync(refreshToken, cancellationToken);
            

        }

        /// <summary>
        /// Revoke token using either access or refresh token
        /// </summary>
        /// <param name="accessOrRefreshToken"></param>
        /// <param name="cancellationToken"></param
        /// <returns>TokenRevocationResponse</returns>
        public async Task<TokenRevocationResponse> RevokeTokenAsync(string accessOrRefreshToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            var revokeTokenClient = new IdentityModel.Client.TokenRevocationClient(DiscoveryDoc.RevocationEndpoint, ClientID, ClientSecret);
            return (TokenRevocationResponse)await revokeTokenClient.RevokeAsync(new IdentityModel.Client.TokenRevocationRequest
            {
                Token = accessOrRefreshToken,
            }, cancellationToken);

           
        }

        /// <summary>
        /// Get User Info
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>UserInfoResponse</returns>
        public async Task<UserInfoResponse> GetUserInfoAsync(string accessToken, CancellationToken cancellationToken = default(CancellationToken))
        {
            var userInfoClient = new IdentityModel.Client.UserInfoClient(DiscoveryDoc.UserInfoEndpoint);
            return (UserInfoResponse)await userInfoClient.GetAsync(accessToken, cancellationToken);
        }

        /// <summary>
        /// Validates ID token
        /// </summary>
        /// <param name="idToken"></param>
        /// <returns>bool</returns>
        public Task<bool> ValidateIDTokenAsync(String idToken)
        {
            IList<IdentityModel.Jwk.JsonWebKey> keys = DiscoveryDoc.KeySet.Keys;
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
        /// <returns>string</returns>
        public string GenerateCSRFToken()
        {
            return CryptoRandom.CreateUniqueId(); 
        }

      
    }
}
