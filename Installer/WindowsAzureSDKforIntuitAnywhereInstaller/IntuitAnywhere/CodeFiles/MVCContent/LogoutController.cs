using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntuitSampleMVC.Controllers
{
    public class LogoutController : Controller
    {
        //
        // GET: /Logout/

        public ActionResult Index()
        {
            Session.RemoveAll();
            //Redirect user to the Home page
           return Redirect("/Home/index"); 
            
        }

    }
}
