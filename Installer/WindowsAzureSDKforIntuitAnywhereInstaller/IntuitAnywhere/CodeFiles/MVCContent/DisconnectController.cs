using System;
using System.Configuration;
using System.Web.Mvc;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using IntuitSampleMVC.utils;

namespace IntuitSampleMVC.Controllers
{
    /// <summary>
    /// Invalidates the OAuth access token in the request, 
    /// thereby disconnecting the user from QuickBooks for this app. 
    /// Because accessing QuickBooks data requires a valid access token, 
    /// when the user is disconnected, your app cannot access the user's QuickBooks company data. 
    /// After disconnecting the user, your app should display the Connect to QuickBooks button.
    /// </summary>
    public class DisconnectController : Controller
    {
        /// <summary>
        /// Service response.
        /// </summary>
        private String txtServiceResponse = "";

        /// <summary>
        /// Disconnect Flag.
        /// </summary>
        protected String DisconnectFlg = "";

        /// <summary>
        /// Creates a HttpRequest with oAuthSession (OAuth Token) and gets the response with invalidating user
        /// from QuickBooks for this app
        /// For Authorization: The request header must include the OAuth parameters defined by OAuth Core 1.0 Revision A.
        /// If the disconnect is successful, then the HTTP status code is 200 and 
        /// the XML response includes the <ErrorCode> element with a 0 value.  
        /// If an HTTP error is detected, then the HTTP status code is not 200.  
        /// If an HTTP error is not detected but the disconnect is unsuccessful, 
        /// then the HTTP status code is 200 and the response XML includes the <ErrorCode> element with a non-zero value.   
        /// For example,  if the OAuth access token expires or is invalid for some other reason, then the value of <ErrorCode> is 270.
        /// </summary>
        /// <returns>Action Result</returns>
        public ActionResult Index()
        {
            OAuthConsumerContext consumerContext = new OAuthConsumerContext
            {
                ConsumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString(),
                SignatureMethod = SignatureMethod.HmacSha1,
                ConsumerSecret = ConfigurationManager.AppSettings["consumerSecret"].ToString()
            };

            OAuthSession oSession = new OAuthSession(consumerContext, Constants.OauthEndPoints.IdFedOAuthBaseUrl + Constants.OauthEndPoints.UrlRequestToken,
                                  Constants.OauthEndPoints.AuthorizeUrl,
                                  Constants.OauthEndPoints.IdFedOAuthBaseUrl + Constants.OauthEndPoints.UrlAccessToken);

            oSession.ConsumerContext.UseHeaderForOAuthParameters = true;
            if ((Session["accessToken"] + "").Length > 0)
            {
                oSession.AccessToken = new TokenBase
                {
                    Token = Session["accessToken"].ToString(),
                    ConsumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString(),
                    TokenSecret = Session["accessTokenSecret"].ToString()
                };

                IConsumerRequest conReq = oSession.Request();
                conReq = conReq.Get();
                conReq = conReq.ForUrl(Constants.IaEndPoints.DisconnectUrl);
                try
                {
                    conReq = conReq.SignWithToken();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                //Used just see the what header contains
                string header = conReq.Context.GenerateOAuthParametersForHeader();

                //This method will clean up the OAuth Token
                txtServiceResponse = conReq.ReadBody(); 
                
                //Reset All the Session Variables
                Session.Remove("oauthToken");

                // Dont remove the access token since this is required for Reconnect btn in the Blue dot menu
                // Session.Remove("accessToken");

                // Add the invalid access token into session for the display of the Disconnect btn
                Session["InvalidAccessToken"] = Session["accessToken"];

                // Dont Remove flag since we need to display the blue dot menu for Reconnect btn in the Blue dot menu
                // Session.Remove("Flag");

                ViewBag.DisconnectFlg = "User is Disconnected from QuickBooks!";

                //Remove the Oauth access token from the OauthAccessTokenStorage.xml
                OauthAccessTokenStorageHelper.RemoveInvalidOauthAccessToken(Session["FriendlyEmail"].ToString(), this);
            }

            return View();
        }
    }
}
