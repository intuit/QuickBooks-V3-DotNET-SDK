using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Intuit.Ipp.Core;
using Intuit.Ipp.Security;
using Intuit.Ipp.Services;
using IntuitSampleMVC.utils;

namespace IntuitSampleMVC.Controllers
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
            DataServices commonService = new DataServices(context);

            try
            {
                // Specify a Request validator
                if (dataSourcetype.ToLower() == "qbd")
                {
                    Intuit.Ipp.Data.Qbd.CustomerQuery qbdCustomerQuery = new Intuit.Ipp.Data.Qbd.CustomerQuery();
                    qbdCustomerQuery.ItemElementName = Intuit.Ipp.Data.Qbd.ItemChoiceType4.StartPage;
                    qbdCustomerQuery.Item = "1";
                    qbdCustomerQuery.ChunkSize = "10";
                    IEnumerable<Intuit.Ipp.Data.Qbd.Customer> customers = qbdCustomerQuery.ExecuteQuery<Intuit.Ipp.Data.Qbd.Customer>
                    (context) as IEnumerable<Intuit.Ipp.Data.Qbd.Customer>;

                }
                else
                {
                    Intuit.Ipp.Data.Qbo.Customer qboCustomer = new Intuit.Ipp.Data.Qbo.Customer();
                    IEnumerable<Intuit.Ipp.Data.Qbo.Customer> customers = commonService.FindAll(qboCustomer, 1, 10) as IEnumerable<Intuit.Ipp.Data.Qbo.Customer>; 
                    ViewBag.MyCollection = customers;
                    ViewBag.CustomerCount = customers.Count();
                }
            }
            catch (Intuit.Ipp.Exception.InvalidTokenException exp)
            {
                //Remove the Oauth access token from the OauthAccessTokenStorage.xml
                OauthAccessTokenStorageHelper.RemoveInvalidOauthAccessToken(Session["FriendlyEmail"].ToString(), this);
                //show a message to the user that token is invalid
                string message = "<SCRIPT LANGUAGE='JavaScript' >alert('Your authorization to this application to access your quickbook data is no longer Valid.Please provide authorization again.')</SCRIPT>";
                // show user the connect to quickbook page again
                Response.Write(message);
                Redirect("/Home/index");

            }
            catch (System.Exception exp)
            {
                throw exp;
            }

            return View();
        }
    }
}
