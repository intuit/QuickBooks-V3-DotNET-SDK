// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Intuit.Ipp.OAuth2PlatformClient
{
    public static class OidcConstants
    {
        public static class AuthorizeRequest
        {
            //Authorize request query params
            public const string Scope               = "scope";
            public const string ResponseType        = "response_type";
            public const string ClientId            = "client_id";
            public const string RedirectUri         = "redirect_uri";
            public const string State               = "state";
           
        }

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

        //public static class EndSessionRequest
        //{
        //    //Session end params 
        //    public const string IdTokenHint           = "id_token_hint";
        //    public const string PostLogoutRedirectUri = "post_logout_redirect_uri";
        //    public const string State                 = "state";
        //    public const string Sid                   = "sid";
        //    public const string Issuer                = "iss";
        //}

        public static class TokenRequest
        {
            //Token request params
            public const string Code                = "code";
            public const string GrantType           = "grant_type";
            public const string RedirectUri         = "redirect_uri";

           // public static string ClientId            = "client_id";
            //public static string ClientSecret        = "client_secret";            
            public const string RefreshToken        = "refresh_token";
            //public static string Scope               = "scope";
            //public static string CodeVerifier        = "code_verifier";//Not used but if required for future, we can keep it
            //public static string TokenType           = "token_type";
            //public static string Algorithm           = "alg";
            //public static string Key                 = "key";
        }

        //public static class TokenRequestTypes
        //{
        //    //Token request types for header
        //    public const string Bearer = "bearer";
        //}

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

        public static class TokenTypes 
        {
            //Token types
            public const string AccessToken   = "access_token";
            public const string IdentityToken = "id_token";
            public const string RefreshToken  = "refresh_token";   
        }

        //public static class AuthenticationSchemes
        //{
        //    //Authentication schemes
        //    public const string AuthorizationHeaderBearer = "Bearer";
        //    //public const string FormPostBearer            = "access_token";
        //    //public const string QueryStringBearer         = "access_token";

        //}

        public static class GrantTypes
        {
           
            public const string AuthorizationCode = "authorization_code";
            public const string RefreshToken      = "refresh_token";
            //public const string Implicit          = "implicit";

        }



        //public static class ResponseTypes
        //{
        //    //Response types for header
        //    public const string Code = "code";
        //    public const string Token = "token";

        //}

        //public static class ResponseModes
        //{
        //    public const string FormPost = "form_post";
        //    public const string Query    = "query";
        //    public const string Fragment = "fragment";
        //}



        //public static class ProtectedResourceErrors
        //{
        //    public const string InvalidToken      = "invalid_token";
        //    public const string ExpiredToken      = "expired_token";
        //    public const string InvalidRequest    = "invalid_request";
        //    public const string InsufficientScope = "insufficient_scope";
        //}

        //public static class EndpointAuthenticationMethods
        //{
        //    public const string PostBody            = "client_secret_post";
        //    public const string BasicAuthentication = "client_secret_basic";

        //}



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
            public const string JwksUri                                     = "jwks_uri";
            
            public const string ResponseTypesSupported                      = "response_types_supported";
            public const string SubjectTypesSupported                       = "subject_types_supported";
            public const string ScopesSupported                             = "scopes_supported";
            public const string IdTokenSigningAlgValuesSupported            = "id_token_signing_alg_values_supported";
            public const string TokenEndpointAuthenticationMethodsSupported = "token_endpoint_auth_methods_supported";
            public const string ClaimsSupported                             = "claims_supported";



            
        }
    }
}