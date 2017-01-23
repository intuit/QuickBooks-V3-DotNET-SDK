using System;
using System.Configuration;
using System.Web;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using $safeprojectname$.utils;

namespace $safeprojectname$
{
    /// <summary>
    /// An existing Customer when clicks Connect to QuickBooks button, this controller will initiate Autorization flow.
    /// 
    /// </summary>
    public partial class OauthGrant : System.Web.UI.Page
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
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event Args.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            oauth_callback_url = Request.Url.GetLeftPart(UriPartial.Authority) + ConfigurationManager.AppSettings["oauth_callback_url"];
            consumerKey = ConfigurationManager.AppSettings["consumerKey"];
            consumerSecret = ConfigurationManager.AppSettings["consumerSecret"];
            oauthLink = Constants.OauthEndPoints.IdFedOAuthBaseUrl;
            IToken token = (IToken)HttpContext.Current.Session["requestToken"];
            IOAuthSession session = CreateSession();
            IToken requestToken = session.GetRequestToken();
            HttpContext.Current.Session["requestToken"] = requestToken;
            RequestToken = requestToken.Token;
            TokenSecret = requestToken.TokenSecret;
            oauthLink = Constants.OauthEndPoints.AuthorizeUrl + "?oauth_token=" + RequestToken + "&oauth_callback=" + UriUtility.UrlEncode(oauth_callback_url);
            Response.Redirect(oauthLink);
        }
        
        /// <summary>
        /// Creates Session
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