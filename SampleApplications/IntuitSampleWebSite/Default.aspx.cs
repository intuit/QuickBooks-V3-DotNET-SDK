using System;
using System.Web;

namespace IntuitSampleWebsite
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["show"] != null)
            {
                bool value = Convert.ToBoolean(Session["show"]);
                if (value)
                {
                    Session.Remove("show");
                    
                    //show a message to the user that token is invalid
                    string message = "<SCRIPT LANGUAGE='JavaScript'>alert('Your authorization to this application to access your quickbook data is no longer Valid.Please provide authorization again.')</SCRIPT>";

                    // show user the connect to quickbook page again
                    Response.Write(message);
                }
            }

            #region OpenId

            // Hide Connect to Quickbooks widget and show Sign in widget
            IntuitInfo.Visible = false;
            IntuitSignin.Visible = true;
            this.Master.FindControl("logoutview").Visible = false;
            // If Session has keys
            if (HttpContext.Current.Session.Keys.Count > 0)
            {
                // If there is a key OpenIdResponse
                if (HttpContext.Current.Session["OpenIdResponse"] != null)
                {
                    // Show the Sign in widget and disable the Connect to Quickbooks widget
                    IntuitSignin.Visible = false;
                    IntuitInfo.Visible = true;
                    this.Master.FindControl("logoutview").Visible = true;
                }

                // Sow information of the user if the keys are in the session
                if (Session["FriendlyIdentifier"] != null)
                {
                    friendlyIdentifier.Text = Session["friendlyIdentifier"].ToString();
                }
                if (Session["FriendlyName"] != null)
                {
                    friendlyName.Text = Session["FriendlyName"].ToString();
                }
                else
                {
                    friendlyName.Text = "User Didnt Login Via Open ID, look them up in your system";
                }

                if (Session["FriendlyEmail"] != null)
                {
                    friendlyEmail.Text = Session["FriendlyEmail"].ToString();
                }
                else
                {
                    friendlyEmail.Text = "User Didnt Login Via Open ID, look them up in your system";
                }
            }
            #endregion

            #region oAuth

            // If session has accesstoken and InvalidAccessToken is null
            if (HttpContext.Current.Session["accessToken"] != null && HttpContext.Current.Session["InvalidAccessToken"] == null)
            {
                // Show oAuthinfo(contains Get Customers Quickbooks List) and disable Connect to quickbooks widget
                oAuthinfo.Visible = true;
                connectToIntuitDiv.Visible = false;
            }

            #endregion
        }
    }

}
