using System;
using System.Configuration;
using System.Web.Mvc;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using IntuitSampleMVC.utils;

namespace IntuitSampleMVC.Controllers
{
    /// <summary>
    /// An existing Customer when clicks Connect to QuickBooks button, this controller will initiate Autorization flow.
    /// 
    /// </summary>
    public class OauthGrantController : Controller
    {
        /// <summary>
        /// CosumerSecret, ConsumerKey, OAuthLink, RequestToken, TokenSecret, OAuthCallbackUrl
        /// </summary>
        private String consumerSecret, consumerKey, oauthLink, RequestToken, TokenSecret, oauth_callback_url;

        /// <summary>
        /// Action Result for Index, This flow will create OAuthConsumer Context using Consumer key and Consuler Secret key
        /// obtained when Application is added at intuit workspace. It creates OAuth Session out of OAuthConsumer and Calls 
        /// Intuit Workpsace endpoint for OAuth.
        /// </summary>
        /// <returns>Redirect Result.</returns>
        public RedirectResult Index()
        {
            oauth_callback_url = Request.Url.GetLeftPart(UriPartial.Authority) + ConfigurationManager.AppSettings["oauth_callback_url"];
            consumerKey = ConfigurationManager.AppSettings["consumerKey"];
            consumerSecret = ConfigurationManager.AppSettings["consumerSecret"];
            oauthLink = Constants.OauthEndPoints.IdFedOAuthBaseUrl;
            IToken token = (IToken)Session["requestToken"];
            IOAuthSession session = CreateSession();
            IToken requestToken = session.GetRequestToken();
            Session["requestToken"] = requestToken;
            RequestToken = requestToken.Token;
            TokenSecret = requestToken.TokenSecret;

            oauthLink = Constants.OauthEndPoints.AuthorizeUrl + "?oauth_token=" + RequestToken + "&oauth_callback=" + UriUtility.UrlEncode(oauth_callback_url);
            return Redirect(oauthLink);
        }

        /// <summary>
        /// Gets the Access Token
        /// </summary>
        /// <returns>Returns OAuth Session</returns>
        protected IOAuthSession CreateSession()
        {
            OAuthConsumerContext consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = consumerKey,
                ConsumerSecret = consumerSecret,
                SignatureMethod = SignatureMethod.HmacSha1
            };

            return new OAuthSession(consumerContext,
                                            Constants.OauthEndPoints.IdFedOAuthBaseUrl + Constants.OauthEndPoints.UrlRequestToken,
                                            oauthLink,
                                            Constants.OauthEndPoints.IdFedOAuthBaseUrl + Constants.OauthEndPoints.UrlAccessToken);
        }
    }
}
