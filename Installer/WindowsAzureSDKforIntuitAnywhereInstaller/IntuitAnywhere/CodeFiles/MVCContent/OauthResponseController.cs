using System;
using System.Configuration;
using System.Web.Mvc;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using IntuitSampleMVC.utils;

namespace IntuitSampleMVC.Controllers
{
    /// <summary>
    /// This flow is invoked when the the user selects a QuickBooks company and then clicks Authorize to 
    /// grant this app access to that company's data. 
    /// Behind the scenes, this app exchanges tokens with the Intuit OAuth service and then 
    /// stores the authorized access token in a session store. (use persistent store such as a database in real time scenarios.)  
    /// A valid access token indicates that the user has connected your app to a specific company.  
    /// (Connections are important because Intuit charges you according to how many active connections 
    /// users have made with your app.)  Later, when your app calls Data Services for QuickBooks, 
    /// it fetches the access token from the persistent store and includes the token in the 
    /// HTTP request header.  
    /// </summary>
    public class OauthResponseController : Controller
    {
        /// <summary>
        /// OAuthVerifyer, RealmId, DataSource
        /// </summary>
        private String _oauthVerifyer, _realmid, _dataSource;

        /// <summary>
        /// Action Results for Index, OAuthToken, OAuthVerifyer and RealmID is recieved as part of Response
        /// and are stored inside Session object for future references
        /// NOTE: Session storage is only used for demonstration purpose only.
        /// </summary>
        /// <returns>View Result.</returns>
        public ViewResult Index()
        {
            if (Request.QueryString.HasKeys())
            {
                // This value is used to Get Access Token.
                _oauthVerifyer = Request.QueryString["oauth_verifier"].ToString();

                _realmid = Request.QueryString["realmId"].ToString();
                Session["realm"] = _realmid;

                //If dataSource is QBO call QuickBooks Online Services, else call QuickBooks Desktop Services
                _dataSource = Request.QueryString["dataSource"].ToString();
                Session["dataSource"] = _dataSource;

                getAccessToken();

                //Production applications should securely store the Access Token.
                //In this template, encrypted Oauth access token is persisted in OauthAccessTokenStorage.xml
                OauthAccessTokenStorageHelper.StoreOauthAccessToken(this);

                // This value is used to redirect to Default.aspx from Cleanup page when user clicks on ConnectToInuit widget.
                Session["RedirectToDefault"] = true;
            }
            else
            {
                Response.Write("No oauth token was received");
            }

            return View();
        }

        /// <summary>
        /// Gets the OAuth Token
        /// </summary>
        private void getAccessToken()
        {
            IOAuthSession clientSession = CreateSession();
            try
            {
                IToken accessToken = clientSession.ExchangeRequestTokenForAccessToken((IToken)Session["requestToken"], _oauthVerifyer);
                Session["accessToken"] = accessToken.Token;

                // Add flag to session which tells that accessToken is in session
                Session["Flag"] = true;

                // Remove the Invalid Access token since we got the new access token
                Session.Remove("InvalidAccessToken");
                Session["accessTokenSecret"] = accessToken.TokenSecret;
            }
            catch (Exception ex)
            {
                //Handle Exception if token is rejected or exchange of Request Token for Access Token failed.
                throw ex;
            }

        }

        /// <summary>
        /// Creates User Session
        /// </summary>
        /// <returns>OAuth Session.</returns>
        private IOAuthSession CreateSession()
        {
            OAuthConsumerContext consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString(),
                ConsumerSecret = ConfigurationManager.AppSettings["consumerSecret"].ToString(),
                SignatureMethod = SignatureMethod.HmacSha1
            };

            return new OAuthSession(consumerContext,
                                            Constants.OauthEndPoints.IdFedOAuthBaseUrl + Constants.OauthEndPoints.UrlRequestToken,
                                            Constants.OauthEndPoints.IdFedOAuthBaseUrl,
                                             Constants.OauthEndPoints.IdFedOAuthBaseUrl + Constants.OauthEndPoints.UrlAccessToken);
        }
    }
}
