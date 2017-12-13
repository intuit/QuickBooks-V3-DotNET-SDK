////*********************************************************
// <copyright file="OAuth1ToOAuth2TokenMigrationHelper.cs" company="Intuit">
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

namespace Intuit.Ipp.Security
{

    using DevDefined.OAuth.Consumer;
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using Intuit.Ipp.Core;
    using System.IO;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Oauth1 to Oauth2 Token Migration Helper class
    /// </summary>
    public class OAuth1ToOAuth2TokenMigrationHelper
    {

        /// <summary>
        ///  Authorizes the specified request.
        /// </summary>
        /// <param name="scopes"></param>
        /// <param name="redirectUrl"></param>
        /// <param name="oauthRequestValidator"></param>
        /// <returns></returns>
        public MigratedTokenResponse GetOAuth2Tokens(string scopes, string redirectUrl, OAuthRequestValidator oauthRequestValidator)
        {
            HttpWebResponse httpWebResponse;
            if (redirectUrl == null || redirectUrl == "")
            {
                redirectUrl = CoreConstants.TOKEN_MIGRATION_REDIRECT_URL;
            }

            try
            {
                //Get post details for the migration request
                var formFields = new Dictionary<string, string>
            {

                { "scope", scopes },
                { "redirect_uri", redirectUrl }
            };

                string requestBody = JsonConvert.SerializeObject(formFields);
                string migrateUrl = CoreConstants.TOKEN_MIGRATION_URL;
                HttpWebRequest httpWebRequest = WebRequest.Create(migrateUrl) as HttpWebRequest;
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                oauthRequestValidator.Authorize(httpWebRequest, requestBody);


                UTF8Encoding encoding = new UTF8Encoding();
                byte[] content = encoding.GetBytes(requestBody.ToString());
                using (var stream = httpWebRequest.GetRequestStream())
                {
                    stream.Write(content, 0, content.Length);
                }

                //Get response for the migrate tokens call
                httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;


            }
            catch (Exception ex)
            {

                //Return exception response
                return new MigratedTokenResponse(ex);
            }

            if (httpWebResponse.StatusCode == HttpStatusCode.OK || httpWebResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                //Deserialize the migrated token response
                using (Stream data = httpWebResponse.GetResponseStream())
                {
                    string content = new StreamReader(data).ReadToEnd();
                    return new MigratedTokenResponse(content);
                }
            }
            else
            {

                string errorDetail = "";

                WebHeaderCollection headers = httpWebResponse.Headers;
                if (headers["WwwAuthenticate"] != null)
                {
                    errorDetail = headers["WwwAuthenticate"].ToString();
                }


                if (errorDetail != null && errorDetail != "")
                {
                    return new MigratedTokenResponse(httpWebResponse.StatusCode, httpWebResponse.StatusDescription + ": " + errorDetail);

                }
                else
                {
                    return new MigratedTokenResponse(httpWebResponse.StatusCode, httpWebResponse.StatusDescription);
                }

            }


        }

    }
}
