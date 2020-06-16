// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
// Modified for Intuit's Oauth2 implementation

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// Open id connect constants class
    /// </summary>
    public static class OidcConstants
    {
        /// <summary>
        /// Authorize request query params
        /// </summary>
        public static class AuthorizeRequest
        {
            //Authorize request query params
            public const string Scope               = "scope";
            public const string ResponseType        = "response_type";
            public const string ClientId            = "client_id";
            public const string RedirectUri         = "redirect_uri";
            public const string State               = "state";
            public const string IdToken_ReamId      = "{\"id_token\":{\"realmId\":null}}";

        }

        /// <summary>
        /// AuthorizeErrors class
        /// </summary>
        public static class AuthorizeErrors
        {
            // OAuth2 errors
            public const string InvalidRequest          = "invalid_request";
            public const string UnauthorizedClient      = "unauthorized_client";
            public const string AccessDenied            = "access_denied";
            public const string UnsupportedResponseType = "unsupported_response_type";
            public const string InvalidScope            = "invalid_scope";
            public const string ServerError             = "server_error";
            public const string TemporarilyUnavailable  = "temporarily_unavailable";

            //// OIDC errors 
            //public const string InteractionRequired      = "interaction_required";
            //public const string LoginRequired            = "login_required";
            //public const string AccountSelectionRequired = "account_selection_required";
            //public const string ConsentRequired          = "consent_required";
            //public const string InvalidRequestUri        = "invalid_request_uri";
            //public const string InvalidRequestObject     = "invalid_request_object";
            //public const string RequestNotSupported      = "request_not_supported";
            //public const string RequestUriNotSupported   = "request_uri_not_supported";
            //public const string RegistrationNotSupported = "registration_not_supported";
        }

        /// <summary>
        /// AuthorizeResponse class
        /// </summary>
        public static class AuthorizeResponse
        {
            //Authorize response params 
            public static string Code             = "code";
            public static string State            = "state";
            public static string RealmId          = "realmId";
            public static string Url              = "url";
            public static string Error            = "error";
            public static string ErrorDescription = "error_description";
        }

      

        /// <summary>
        /// TokenRequest class
        /// </summary>
        public static class TokenRequest
        {
            //Token request params
            public const string Code                = "code";
            public const string GrantType           = "grant_type";
            public const string RedirectUri         = "redirect_uri";                    
            public const string RefreshToken        = "refresh_token";
            
        }

        

        /// <summary>
        /// TokenErrors class
        /// </summary>
        public static class TokenErrors
        {
            //Token Error types
            public const string InvalidRequest          = "invalid_request";
            public const string InvalidClient           = "invalid_client";
            public const string InvalidGrant            = "invalid_grant";
            public const string UnauthorizedClient      = "unauthorized_client";
            public const string UnsupportedGrantType    = "unsupported_grant_type";
            public const string UnsupportedResponseType = "unsupported_response_type";
            public const string InvalidScope            = "invalid_scope";
        }

        /// <summary>
        /// TokenResponse class
        /// </summary>
        public static class TokenResponse 
        {
            //Token response params
            public const string AccessToken             = "access_token";
            public const string AccessTokenExpiresIn    = "expires_in";
            public const string TokenType               = "token_type";
            public const string RefreshToken            = "refresh_token";
            public const string RefreshTokenExpiresIn   = "x_refresh_token_expires_in";
            public const string IdentityToken           = "id_token";
            public const string Error                   = "error";
            public const string ErrorDescription        = "error_description";
            //public const string BearerTokenType         = "Bearer";
        }

        /// <summary>
        /// TokenTypes class
        /// </summary>
        public static class TokenTypes 
        {
            //Token types
            public const string AccessToken   = "access_token";
            public const string IdentityToken = "id_token";
            public const string RefreshToken  = "refresh_token";   
        }

        /// <summary>
        /// GratTypes class
        /// </summary>
        public static class GrantTypes
        {

            public const string AuthorizationCode = "authorization_code";
            public const string RefreshToken = "refresh_token";
            //public const string Implicit          = "implicit";

        }


        /// <summary>
        /// Discovery class
        /// </summary>
        public static class Discovery
        {
            public const string Issuer                                      = "issuer";

            // endpoints
            public const string AuthorizationEndpoint                       = "authorization_endpoint";
            public const string TokenEndpoint                               = "token_endpoint";
            public const string UserInfoEndpoint                            = "userinfo_endpoint";
            public const string IntrospectionEndpoint                       = "introspection_endpoint";
            public const string RevocationEndpoint                          = "revocation_endpoint";
            public const string ProdDiscoveryEndpoint                       = ".well-known/openid_configuration";
            public const string SandboxDiscoveryEndpoint                    = ".well-known/openid_sandbox_configuration";


            
          
            public const string ProdAuthority                               = "https://developer.api.intuit.com/";
 

            public const string JwksUri                                     = "jwks_uri";
            
            public const string ResponseTypesSupported                      = "response_types_supported";
            public const string SubjectTypesSupported                       = "subject_types_supported";
            public const string ScopesSupported                             = "scopes_supported";
            public const string IdTokenSigningAlgValuesSupported            = "id_token_signing_alg_values_supported";
            public const string TokenEndpointAuthenticationMethodsSupported = "token_endpoint_auth_methods_supported";
            public const string ClaimsSupported                             = "claims_supported";
            public const string DiscoveryUrlSandbox                         = "https://developer.api.intuit.com/.well-known/openid_sandbox_configuration";
            public const string DiscoveryUrlProduction                      = "https://developer.api.intuit.com/.well-known/openid_configuration";
            public const string IssuerUrl                                   = "https://oauth.platform.intuit.com/op/v1";
           





        }
    }
}