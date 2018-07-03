using Intuit.Ipp.OAuth2PlatformClient;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net;
using Intuit.Ipp.Core;
using Intuit.Ipp.Data;
using Intuit.Ipp.QueryFilter;
using Intuit.Ipp.Security;
using System.Linq;
using System.Collections.Generic;
using Intuit.Ipp.Exception;

namespace MvcCodeFlowClientManual.Controllers
{
    public class AppController : Controller
    {
        public static string clientid = ConfigurationManager.AppSettings["clientid"];
        public static string clientsecret = ConfigurationManager.AppSettings["clientsecret"];
        public static string redirectUrl = ConfigurationManager.AppSettings["redirectUrl"];
        public static string environment = ConfigurationManager.AppSettings["appEnvironment"];

        public static OAuth2Client auth2Client = new OAuth2Client(clientid, clientsecret, redirectUrl, environment);

        /// <summary>
        /// Use the Index page of App controller to get all endpoints from discovery url
        /// </summary>
        public ActionResult Index()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            Session.Clear();
            Session.Abandon();
            Request.GetOwinContext().Authentication.SignOut("Cookies");
            return View();
        }

        /// <summary>
        /// Start Auth flow
        /// </summary>
        public ActionResult InitiateAuth(string submitButton)
        {
            switch (submitButton)
            {
                case "Connect to QuickBooks":
                    List<OidcScopes> scopes = new List<OidcScopes>();
                    scopes.Add(OidcScopes.Accounting);
                    string authorizeUrl = auth2Client.GetAuthorizationURL(scopes);
                    return Redirect(authorizeUrl);
                default:
                    return (View());
            }
        }

        /// <summary>
        /// QBO API Request
        /// </summary>
        public async Task<ActionResult> ApiCallService()
        {

            if (Session["realmId"] != null)
            {
                string realmId = Session["realmId"].ToString();
                try
                {
                    var principal = User as ClaimsPrincipal;
                    OAuth2RequestValidator oauthValidator = new OAuth2RequestValidator(principal.FindFirst("access_token").Value);

                    // Create a ServiceContext with Auth tokens and realmId
                    ServiceContext serviceContext = new ServiceContext(realmId, IntuitServicesType.QBO, oauthValidator);
                    serviceContext.IppConfiguration.MinorVersion.Qbo = "23";

                    // Create a QuickBooks QueryService using ServiceContext
                    QueryService<CompanyInfo> querySvc = new QueryService<CompanyInfo>(serviceContext);
                    CompanyInfo companyInfo = querySvc.ExecuteIdsQuery("SELECT * FROM CompanyInfo").FirstOrDefault();

                    string output = "Company Name: " + companyInfo.CompanyName + " Company Address: " + companyInfo.CompanyAddr.Line1 + ", " + companyInfo.CompanyAddr.City + ", " + companyInfo.CompanyAddr.Country + " " + companyInfo.CompanyAddr.PostalCode;
                    return View("ApiCallService", (object)("QBO API call Successful!! Response: " + output));
                }
                catch (IdsException ex)
                {
                    if (ex.Message == "Unauthorized-401")
                    {
                        // Expired tokens, refresh access token once to get new tokens and try QBO API call again.
                        var principal = User as ClaimsPrincipal;
                        var identity = User.Identity as ClaimsIdentity;
                        var tokenResp = await auth2Client.RefreshTokenAsync(principal.FindFirst("refresh_token").Value);
                        if (tokenResp.AccessToken != null && tokenResp.RefreshToken != null)
                        {
                            identity.RemoveClaim(principal.FindFirst("access_token"));
                            identity.AddClaim(new Claim("access_token", tokenResp.AccessToken));
                            identity.RemoveClaim(principal.FindFirst("refresh_token"));
                            identity.AddClaim(new Claim("refresh_token", tokenResp.RefreshToken));
                            await ApiCallService();
                            return View();
                        }
                        else
                        {
                            return View("ApiCallService", (object)("QBO API call Failed!" + " Error message: " + ex.Message));
                        }
                    }
                    else
                    {
                        return View("ApiCallService", (object)("QBO API call Failed!" + " Error message: " + ex.Message));
                    }
                }
                catch (Exception ex)
                {
                    return View("ApiCallService", (object)("QBO API call Failed!" + " Error message: " + ex.Message));
                }
            }
            else
                return View("ApiCallService", (object)"QBO API call Failed!");
        }

        /// <summary>
        /// Use the Index page of App controller to get all endpoints from discovery url
        /// </summary>
        public ActionResult Error()
        {
            return View("Error");
        }

        /// <summary>
        /// Action that takes redirection from Callback URL
        /// </summary>
        public ActionResult Tokens()
        {
            return View("Tokens");
        }
    }
}