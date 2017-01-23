using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace $safeprojectname$
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Get tokens from the AppSettings in config files
            string applicationToken = ConfigurationManager.AppSettings["applicationToken"];
            string consumerKey = ConfigurationManager.AppSettings["consumerKey"];
            string consumerSecret = ConfigurationManager.AppSettings["consumerSecret"];
            // Check whether the keys are null or empty.
            if (string.IsNullOrWhiteSpace(applicationToken) || string.IsNullOrWhiteSpace(consumerKey) || string.IsNullOrWhiteSpace(consumerSecret))
            {
                // Show Error message
                this.errorDiv.Visible = true;
                this.mainContetntDiv.Visible = false;
            }
            else
            {
                // Show main content
                this.errorDiv.Visible = false;
                this.mainContetntDiv.Visible = true;
            }

            if (Session["FriendlyName"] != null)
            {
                this.friendlyName.Text = Session["FriendlyName"].ToString();
            }

            // Read value from session and check flag which decides the display of blue dot menu
            object flag = Session["Flag"];
            if (flag != null)
            {
                bool flagValue = Convert.ToBoolean(flag.ToString());
                if (flagValue)
                {
                    // Show BlueDot widget
                    this.blueDotDiv.Visible = true;
                    this.logoutview.Style.Add(HtmlTextWriterStyle.MarginRight, "125px");
                }
                else
                {
                    // Disable BlueDot widget
                    this.blueDotDiv.Visible = false;
                    this.logoutview.Style.Add(HtmlTextWriterStyle.MarginRight, "0px");
                }
            }
            else
            {
                // Disable BlueDot widget
                this.blueDotDiv.Visible = false;
                this.logoutview.Style.Add(HtmlTextWriterStyle.MarginRight, "0px");
            }
        }
    }
}
