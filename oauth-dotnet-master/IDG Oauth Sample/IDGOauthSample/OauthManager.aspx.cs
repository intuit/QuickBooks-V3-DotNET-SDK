/*
 * Copyright (c) 2015 Intuit, Inc.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 * Contributors:
 *    Author : Keneally, Jarred
 *    Intuit Developer Group - initial contribution 
 *
 */

#region <<Namespace>>
using System;
using System.Configuration;
using System.Web;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Provider;
using DevDefined.OAuth.Storage.Basic;
using System.Collections.Generic;

using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.DataService;
using Intuit.Ipp.LinqExtender;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using Intuit.Ipp.Exception;

using System.Linq;

using System.Web.Profile;
#endregion
//
namespace IDGOauthSample
{
    public partial class OauthManager : System.Web.UI.Page
    {
        #region <<App Properties >>
        public static String REQUEST_TOKEN_URL = ConfigurationManager.AppSettings["GET_REQUEST_TOKEN"];
        public static String ACCESS_TOKEN_URL = ConfigurationManager.AppSettings["GET_ACCESS_TOKEN"];
        public static String AUTHORIZE_URL = ConfigurationManager.AppSettings["AuthorizeUrl"];
        public static String OAUTH_URL = ConfigurationManager.AppSettings["OauthLink"];
        public String consumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
        public String consumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
        public string strrequestToken = string.Empty;
        public string tokenSecret = string.Empty;
        public string oauth_callback_url = "http://localhost:65281/OauthManager.aspx?";
        public string GrantUrl = "http://localhost:65281/OauthManager.aspx?connect=true";
        //public string GrantUrl = string.Empty;
        #endregion
        /// <summary>
        /// Page Load with initialization of properties.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                List<string> queryKeys = new List<string>(Request.QueryString.AllKeys);
                if (queryKeys.Contains("connect"))
                {
                    FireAuth();
                }
                if (queryKeys.Contains("oauth_token"))
                {
                    ReadToken();

                   
                }
            }
            else
            {
                if (HttpContext.Current.Session["accessToken"] == null && HttpContext.Current.Session["accessTokenSecret"] == null)
                {
                    c2qb.Visible = true;
                    disconnect.Visible = false;
                    lblDisconnect.Visible = false;
                }
                else
                {
                    c2qb.Visible = false;
                    disconnect.Visible = true;
                    //Disconnect();
                }
            }
        }
        /// <summary>
        /// Initiate the ouath screen.
        /// </summary>
        private void FireAuth()
        {
            HttpContext.Current.Session["consumerKey"] = consumerKey;
            HttpContext.Current.Session["consumerSecret"] = consumerSecret;
            CreateAuthorization();
            IToken token = (IToken)HttpContext.Current.Session["requestToken"];
            tokenSecret = token.TokenSecret;
            strrequestToken = token.Token;
        }
        /// <summary>
        /// Read the values from the query string.
        /// </summary>
        private void ReadToken()
        {
            HttpContext.Current.Session["oauthToken"] = Request.QueryString["oauth_token"].ToString(); ;
            HttpContext.Current.Session["oauthVerifyer"] = Request.QueryString["oauth_verifier"].ToString();
            HttpContext.Current.Session["realm"] = Request.QueryString["realmId"].ToString();
            HttpContext.Current.Session["dataSource"] = Request.QueryString["dataSource"].ToString();
            //Stored in a session for demo purposes.
            //Production applications should securely store the Access Token
            getAccessToken();
        }
        //
        #region <<Routines>>

        /// <summary>
        /// Create a session.
        /// </summary>
        /// <returns></returns>
        protected IOAuthSession CreateSession()
        {
            var consumerContext = new OAuthConsumerContext
                {
                    ConsumerKey = HttpContext.Current.Session["consumerKey"].ToString(),
                    ConsumerSecret = HttpContext.Current.Session["consumerSecret"].ToString(),
                    SignatureMethod = SignatureMethod.HmacSha1
                };
            return new OAuthSession(consumerContext,
                                    REQUEST_TOKEN_URL,
                                    HttpContext.Current.Session["oauthLink"].ToString(),
                                    ACCESS_TOKEN_URL);
        }
        /// <summary>
        /// Get Access token.
        /// </summary>
        private void getAccessToken()
        {
            IOAuthSession clientSession = CreateSession();
            IToken accessToken = clientSession.ExchangeRequestTokenForAccessToken((IToken)HttpContext.Current.Session["requestToken"], HttpContext.Current.Session["oauthVerifyer"].ToString());
            HttpContext.Current.Session["accessToken"] = accessToken.Token;
            HttpContext.Current.Session["accessTokenSecret"] = accessToken.TokenSecret;

            String realmId = HttpContext.Current.Session["accessTokenSecret"].ToString();
            OAuthRequestValidator oauthValidator = new OAuthRequestValidator(accessToken.Token, accessToken.TokenSecret, consumerKey, consumerSecret);
            ServiceContext serviceContext=  new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);

            
        }
        /// <summary>
        /// 
        /// </summary>
        protected void CreateAuthorization()
        {
            //Remember these for later.
            HttpContext.Current.Session["consumerKey"] = consumerKey;
            HttpContext.Current.Session["consumerSecret"] = consumerSecret;
            HttpContext.Current.Session["oauthLink"] = OAUTH_URL;
            //
            IOAuthSession session = CreateSession();
            IToken requestToken = session.GetRequestToken();
            HttpContext.Current.Session["requestToken"] = requestToken;
            tokenSecret = requestToken.TokenSecret;
            var authUrl = string.Format("{0}?oauth_token={1}&oauth_callback={2}", AUTHORIZE_URL, requestToken.Token, UriUtility.UrlEncode(oauth_callback_url));
            HttpContext.Current.Session["oauthLink"] = authUrl;
            Response.Redirect(authUrl);
        }
        #endregion
        /// <summary>
        /// Disconnect and call Session_End event of the page life cycle 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDisconnect_Click(object sender, EventArgs e)
        {
            //Clearing all session data
            Disconnect();
        }

        private void Disconnect()
        {
            try
            {
                Session.Clear();
                Session.Abandon();
                HttpContext.Current.Session["accessToken"] = null;
                HttpContext.Current.Session["accessTokenSecret"] = null;
                HttpContext.Current.Session["realm"] = null;
                HttpContext.Current.Session["dataSource"] = null;
                disconnect.Visible = false;
                lblDisconnect.Visible = true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.InnerException);
            }
        }
    }
}