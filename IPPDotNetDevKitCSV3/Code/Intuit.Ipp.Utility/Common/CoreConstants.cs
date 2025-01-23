////*********************************************************
// <copyright file="CoreConstants.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2016 Intuit
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/
// 
// <summary>This file contains Constants.</summary>
////*********************************************************

//namespace Intuit.Ipp.Core  
namespace Intuit.Ipp.Utility
{
    /// <summary>
    /// Constants whose values do not change.
    /// </summary>
    public static class CoreConstants
    {
        /// <summary>
        /// Slash character.
        /// </summary>
        public const string SLASH_CHAR = "/";

        /// <summary>
        /// The Intuit Services Version.
        /// </summary>
        public const string VERSION = "v3";

        /// <summary>
        /// The Resource.
        /// </summary>
        public const string RESOURCE = "resource";

        /// <summary>
        /// Content type: text/xml.
        /// </summary>
        public const string CONTENTTYPE_TEXTXML = "text/xml";

        /// <summary>
        /// Content type: text/plain.
        /// </summary>
        public const string CONTENTTYPE_TEXTPLAIN = "text/plain";

        /// <summary>
        /// Content type: application/text.
        /// </summary>
        public const string CONTENTTYPE_APPLICATIONTEXT = "application/text";

        /// <summary>
        /// Content type: application/xml.
        /// </summary>
        public const string CONTENTTYPE_APPLICATIONXML = "application/xml";

        /// <summary>
        /// Content type: application/xml.
        /// </summary>
        public const string CONTENTTYPE_APPLICATIONJSON = "application/json";

        /// <summary>
        /// Content type: application/pdf.
        /// </summary>
        public const string CONTENTTYPE_APPLICATIONPDF = "application/pdf";

        /// <summary>
        /// Content type: application/pdf.
        /// </summary>
        public const string CONTENTTYPE_APPLICATIONOCTETSTREAM = " application/octet-stream";

        /// <summary>
        /// Content type: application/x-www-form-urlencoded.
        /// </summary>
        public const string CONTENTTYPE_URLFORMENCODED = "application/x-www-form-urlencoded";

        /// <summary>
        /// The Base Url for Oauth1 to Oauth2 tokens migration for Prod.
        /// </summary>
        public const string TOKEN_MIGRATION_URL_PROD = "https://developer.api.intuit.com/v2/oauth2/tokens/migrate";

        /// <summary>
        /// The Redirect url required by token migration endpoint.
        /// </summary>
        public const string TOKEN_MIGRATION_REDIRECT_URL= "https://developer.intuit.com/v2/OAuth2Playground/RedirectUrl";

        /// <summary>
        /// The Base Url for Oauth1 to Oauth2 tokens migration for sandbox.
        /// </summary>
        public const string TOKEN_MIGRATION_URL_SANDBOX = "https://developer-sandbox.api.intuit.com/v2/oauth2/tokens/migrate";
       
        /// <summary>
        /// The Base Url for IPS.
        /// </summary>
        public const string IPS_BASEURL = " https://appcenter.intuit.com/db/";

        /// <summary>
        /// The Base Url for QBO.
        /// </summary>
        public const string QBO_BASEURL = "https://quickbooks.api.intuit.com/";

        /// <summary>
        /// The Base Url for Entitlements API.
        /// </summary>
        public const string ENTITLEMENT_BASEURL = "https://quickbooks.api.intuit.com/manage";

        /// <summary>
        /// Intuit Workplace Uri. 
        /// </summary>
        public const string INTUIT_WORKPLACE = "https://appcenter.intuit.com/db";

        /// <summary>
        /// Intuit O Auth Access token Uri. 
        /// </summary>
        public const string OAUTHACCESSTOKENURL = "https://oauth.intuit.com/oauth/v1/get_access_token_by_intuit_pseudonym";

        /// <summary>
        /// Id Parameter Name.
        /// </summary>
        public const string Id = "Id";

        /// <summary>
        /// Sync Token Parameter Name.
        /// </summary>
        public const string SYNC_TOKEN = "SyncToken";

        /// <summary>
        /// Domain Parameter Name.
        /// </summary>
        public const string DOMAIN = "domain";

        /// <summary>
        /// MetaData Parameter Name.
        /// </summary>
        public const string METADATA = "MetaData";

        /// <summary>
        /// Sparse Parameter Name.
        /// </summary>
        public const string SPARSE = "sparse";

        /// <summary>
        /// Status Parameter Name.
        /// </summary>
        public const string STATUS = "status";

        /// <summary>
        /// Id Domain Query Parameter.
        /// </summary>
        public const string ID_DOMAIN_QUERY = "?idDomain=";

        /// <summary>
        /// Page Number Query Parameter.
        /// </summary>
        public const string PAGE_NUM = "PageNum=";

        /// <summary>
        /// Results Per page Query Parameter.
        /// </summary>
        public const string RESULTS_PERPAGE = "&ResultsPerPage=";

        /// <summary>
        /// API Action Header Key.
        /// </summary>
        public const string APIACTIONHEADER = "QUICKBASE-ACTION";

        /// <summary>
        /// XPath for IsQbo tag.
        /// </summary>
        public const string ISQBOXPATH = "//IsQBO";

        /// <summary>
        /// XPath for realm tag.
        /// </summary>
        public const string REALMXPATH = "//realm";

        /// <summary>
        /// Api for API_GetIsRealmQBO
        /// </summary>
        public const string API_GETISREALMQBO = "API_GetIsRealmQBO";

        /// <summary>
        /// Api for API_GetIDSRealm
        /// </summary>
        public const string API_GETIDSREALM = "API_GetIDSRealm";

        /// <summary>
        /// Authorization String For Header.
        /// </summary>
        public const string AUTHORIZATIONSTRING_FORHEADER = "Authorization";

        /// <summary>
        /// Intuit Auth header format.
        /// </summary>
        public const string INTUITAUTH_HEADERFORMAT = "INTUITAUTH  intuit-app-token=\"{0}\",intuit-token=\"{1}\"";

        /// <summary>
        /// Request File Name Format.
        /// </summary>
        public const string ADVANCED_ROLLING_FILE_FORMAT = "{0}{1}QBOApiLogs-{2}.txt";

        /// <summary>
        /// Request File Name Format.
        /// </summary>
        public const string REQUESTFILENAME_FORMAT = "{0}{1}Request-{2}.txt";

        /// <summary>
        /// Response File Name Format.
        /// </summary>
        public const string RESPONSEFILENAME_FORMAT = "{0}{1}Response-{2}.txt";

        /// <summary>
        /// Error Response File Name Format.
        /// </summary>
        public const string ERRORRESPONSEFILENAME_FORMAT = "{0}{1}Error-Response-{2}.txt";

        /// <summary>
        /// The o auth auth id pseudonym
        /// </summary>
        public const string XOAUTHAUTHIDPSEUDONYM = "xoauth_auth_id_pseudonym";

        /// <summary>
        /// The o auth realm id pseudonym
        /// </summary>
        public const string XOAUTHREALMIDPSEUDONYM = "xoauth_realm_id_pseudonym";

        /// <summary>
        /// The o auth service provider id
        /// </summary>
        public const string XOAUTHSERVICEPROVIDERID = "xoauth_service_provider_id";

        /// <summary>
        /// The Compression format of the request data.
        /// </summary>
        public const string CONTENTENCODING = "Content-Encoding";

        /// <summary>
        /// The Compression format of the response data.
        /// </summary>
        public const string ACCEPTENCODING = "Accept-Encoding";

        /// <summary>
        /// The Request source header value.
        /// </summary>
        public const string REQUESTSOURCEHEADER = "V3DotNetSDK14.7.0.0";

        /// <summary>
        /// multipart/form-data format
        /// </summary>
        public const string CONTENTTYPE_MULTIPARTFORMDATAFORMAT = "multipart/form-data; boundary={0}";

        /// <summary>
        /// content-disposition format for string data
        /// </summary>
        public const string CONTENTDISPOSITION_FORMAT = "--{0}\r\nContent-Disposition: form-data; name=\"{1}\"{2}\r\n{3}{4}\r\n";

        /// <summary>
        /// content-disposition format for Filename
        /// </summary>
        public const string CONTENTDISPOSITION_FILENAME_FORMAT = "; filename=\"{0}\"";

        /// <summary>
        /// content-disposition format for ContentType
        /// </summary>
        public const string CONTENTDISPOSITION_CONTENTTYPE_FORMAT = "Content-Type: {0}\r\n";

        /// <summary>
        /// content-disposition format for ContentType
        /// </summary>
        public const string CONTENTDISPOSITION_CONTENTTRANSFERENCODING_FORMAT = "Content-Transfer-Encoding: {0}\r\n";
    }
}
