using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using Intuit.Ipp.Core;
using Intuit.Ipp.Security;
using Intuit.Ipp.Services;
using IntuitSampleWebsite.utils;

namespace IntuitSampleWebsite 
{
    /// <summary>
    /// Controller which connects to QuickBooks and Pulls customer Info.
    /// This flow will make use of Data Service SDK V2 to create OAuthRequest and connect to 
    /// Customer Data under the service context and display data inside the grid.
    /// </summary>
    public partial class QuickBooksCustomers : System.Web.UI.Page
    {
        /// <summary>
        /// RealmId, AccessToken, AccessTokenSecret, ConsumerKey, ConsumerSecret, DataSourceType
        /// </summary>
        private String realmId, accessToken, accessTokenSecret, consumerKey, consumerSecret, dataSourcetype;

        /// <summary>
        /// Page Load Event, pulls data from QuickBooks using SDK and Binds it to Grid
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event Args.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session.Keys.Count > 0)
            {
                realmId = HttpContext.Current.Session["realm"].ToString();
                accessToken = HttpContext.Current.Session["accessToken"].ToString();
                accessTokenSecret = HttpContext.Current.Session["accessTokenSecret"].ToString();
                consumerKey = ConfigurationManager.AppSettings["consumerKey"].ToString();
                consumerSecret = ConfigurationManager.AppSettings["consumerSecret"].ToString();
                dataSourcetype = HttpContext.Current.Session["dataSource"].ToString();

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
                        GridView1.DataSource = customers;
                    }
                    else
                    {
                        Intuit.Ipp.Data.Qbo.Customer qboCustomer = new Intuit.Ipp.Data.Qbo.Customer();
                        IEnumerable<Intuit.Ipp.Data.Qbo.Customer> customers = commonService.FindAll(qboCustomer, 1, 10) as IEnumerable<Intuit.Ipp.Data.Qbo.Customer>;
                        GridView1.DataSource = customers;
                    }

                    GridView1.DataBind();
                    if (GridView1.Rows.Count > 0)
                    {
                        GridLocation.Visible = true;
                        MessageLocation.Visible = false;
                    }
                    else
                    {
                        GridLocation.Visible = false;
                        MessageLocation.Visible = true;
                    }
                }
                catch (Intuit.Ipp.Exception.InvalidTokenException exp)
                {
                    //Remove the Oauth access token from the OauthAccessTokenStorage.xml
                   OauthAccessTokenStorageHelper.RemoveInvalidOauthAccessToken(Session["FriendlyEmail"].ToString(),Page);
                    //show a message to the user that token is invalid
                    string message="<SCRIPT LANGUAGE='JavaScript' >alert('Your authorization to this application to access your quickbook data is no longer Valid.Please provide authorization again.')</SCRIPT>";
                    // show user the connect to quickbook page again
                    Response.Write(message);
                    Response.Redirect("default.aspx");
                    
                }
                catch (System.Exception exp)
                {
                    throw exp;
                }
            }
        }
    }
}