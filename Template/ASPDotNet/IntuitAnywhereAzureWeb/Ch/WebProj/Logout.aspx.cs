using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace $safeprojectname$
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session.Keys.Count > 0)
            {
                HttpContext.Current.Session.RemoveAll();
                //Redirect user to the Home page
                Response.Redirect("default.aspx"); 
            }
        }
    }
}