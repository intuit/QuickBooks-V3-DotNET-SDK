using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web.Mvc;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using IntuitSampleMVC.utils;

namespace IntuitSampleMVC.Controllers
{
    /// <summary>
    /// Controller for Blue Dot Menu, Returns the HTML for the Intuit "blue dot" menu, 
    /// which shows the Intuit Anywhere apps available to the user.   
    /// </summary>
    public class MenuProxyController : Controller
    {
        /// <summary>
        /// Service response.
        /// </summary>
        private String txtServiceResponse = "";

        /// <summary>
        /// The request header must include the OAuth parameters defined by OAuth Core 1.0 Revision A.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            GetBlueDotMenu();
            ViewBag.ErrorMessage = txtServiceResponse;
            return View();
        }

        /// <summary>
        /// Core Logic for Blue Dot Menu
        /// Error Handling: If the OAuth access token has expired or is invalid for some other reason, 
        /// then the HTTP status code is 200, and the HTML returned shows the Connect to QuickBooks button within the Intuit "blue dot" menu.  
        /// If an internal error is detected, then the HTTP status code returned is not 2xx, and the HTML returned will display the following text in the menu: "We are sorry, but we cannot load the menu right now."
        /// </summary>
        protected void GetBlueDotMenu()
        {
            Session["serviceEndPoint"] = Constants.IaEndPoints.BlueDotAppMenuUrl;
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

            oSession.AccessToken = new TokenBase
            {
                Token = Session["accessToken"].ToString(),
                ConsumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString(),
                TokenSecret = Session["accessTokenSecret"].ToString()
            };

            IConsumerRequest conReq = oSession.Request();
            conReq = conReq.Get();
            conReq = conReq.ForUrl(Session["serviceEndPoint"].ToString());
            try
            {
                conReq = conReq.SignWithToken();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            string header = conReq.Context.GenerateOAuthParametersForHeader();
            try
            {
                txtServiceResponse = conReq.ReadBody();
                Response.Write(txtServiceResponse);
            }
            catch (WebException we)
            {
                HttpWebResponse rsp = (HttpWebResponse)we.Response;
                if (rsp != null)
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(rsp.GetResponseStream()))
                        {
                            txtServiceResponse = txtServiceResponse + rsp.StatusCode + " | " + reader.ReadToEnd();
                        }
                    }
                    catch (Exception)
                    {
                        txtServiceResponse = txtServiceResponse + "Status code: " + rsp.StatusCode;
                    }
                }
                else
                {
                    txtServiceResponse = txtServiceResponse + "Error Communicating with Intuit Anywhere" + we.Message;
                }
            }
        }
    }
}
