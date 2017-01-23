using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Intuit.Ipp.Core;
using Intuit.Ipp.Security;
using $safeprojectname$.utils;
using LinqExtender;

namespace $safeprojectname$.Controllers
{
    /// <summary>
    /// Controller which connects to QuickBooks and Pulls customer Info.
    /// This flow will make use of Data Service SDK V2 to create OAuthRequest and connect to 
    /// Customer Data under the service context and display data inside the grid.
    /// </summary>
    public class QuickBooksCustomersController : Controller
    {
        /// <summary>
        /// RealmId, AccessToken, AccessTokenSecret, ConsumerKey, ConsumerSecret, DataSourceType
        /// </summary>
        private String realmId, accessToken, accessTokenSecret, consumerKey, consumerSecret, dataSourcetype;

        /// <summary>
        /// Action Results for Index
        /// </summary>
        /// <returns>Action Result.</returns>
        public ActionResult Index()
        {
            realmId = Session["realm"].ToString();
            accessToken = Session["accessToken"].ToString();
            accessTokenSecret = Session["accessTokenSecret"].ToString();
            consumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString();
            consumerSecret = ConfigurationManager.AppSettings["consumerSecret"].ToString();
            dataSourcetype = Session["dataSource"].ToString();

            OAuthRequestValidator oauthValidator = Initializer.InitializeOAuthValidator(accessToken, accessTokenSecret, consumerKey, consumerSecret);
            ServiceContext context = Initializer.InitializeServiceContext(oauthValidator, realmId, string.Empty, string.Empty, dataSourcetype);
            try
            {
                Intuit.Ipp.QueryFilter.QueryService<Intuit.Ipp.Data.Customer> customerQueryService = new Intuit.Ipp.QueryFilter.QueryService<Intuit.Ipp.Data.Customer>(context);
                List<Intuit.Ipp.Data.Customer> customers = customerQueryService.Select(s => s).ToList();
                ViewBag.MyCollection = customers;
                ViewBag.CustomerCount = customers.Count();
            }
            catch (Intuit.Ipp.Exception.InvalidTokenException exp)
            {
                //Remove the Oauth access token from the OauthAccessTokenStorage.xml
                OauthAccessTokenStorageHelper.RemoveInvalidOauthAccessToken(Session["FriendlyEmail"].ToString(), this);

                Session["show"] = true;
                return Redirect("/Home/index");
            }
            catch (System.Exception exp)
            {
                throw exp;
            }

            return View();
        }
    }
}
