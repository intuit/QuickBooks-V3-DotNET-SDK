using System.Web.Mvc;

namespace IntuitSampleMVC.Controllers
{
    /// <summary>
    /// Home Controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Default View for website.
        /// </summary>
        /// <returns>Action Result.</returns>
        public ActionResult Index()
        {
            if (Session["show"] != null)
            {
                bool value = System.Convert.ToBoolean(Session["show"]);
                if (value)
                {
                    Session.Remove("show");

                    //show a message to the user that token is invalid
                    string message = "<SCRIPT LANGUAGE='JavaScript'>alert('Your authorization to this application to access your quickbook data is no longer Valid.Please provide authorization again.')</SCRIPT>";

                    // show user the connect to quickbook page again
                    Response.Write(message);
                }
            }

            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        /// <summary>
        /// About view for the website.
        /// </summary>
        /// <returns>Action Result.</returns>
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// Error view for the website.
        /// </summary>
        /// <returns>Action Result.</returns>
        public ActionResult Error()
        {
            ViewBag.Message = "There is an error on our website.";

            return View();
        }
    }
}
