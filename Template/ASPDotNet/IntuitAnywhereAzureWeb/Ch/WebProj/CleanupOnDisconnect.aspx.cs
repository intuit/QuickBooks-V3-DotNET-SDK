using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace $safeprojectname$
{
    public partial class CleanupOnDisconnect : System.Web.UI.Page
    {
        /// <summary>
        /// This page is called as a result of disconnect from workplace if this page is specified as landing page Url for disconnect. 
        /// Please perform all the required clean on this page in such scenario. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //perform the clean up here 

            // Redirect to Default.aspx when user clicks on ConenctToIntuit widget.
            object value = Session["RedirectToDefault"];
            if (value != null)
            {
                bool isTrue = (bool)value;
                if (isTrue)
                {
                    Response.Redirect("default.aspx");
                }
            }
        }
    }
}