using System.Web.Mvc;

namespace IntuitSampleMVC.Controllers
{
    public class CleanupOnDisconnectController : Controller
    {
        //
        // GET: /CleanupOnDisconnect/
        /// <summary>
        ///This page is called as a result of disconnect from workplace if this page is specified as landing page Url for disconnect. 
        ///Please perform all the required clean on this page in such scenario. 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //perform the clean up here 

            // Redirect to Default.aspx when user clicks on ConenctToIntuit widget.
            object value = Session["RedirectToDefault"];
            if (value != null)
            {
                bool isTrue = (bool)value;
                if (isTrue)
                {
                    Session.Remove("InvalidAccessToken");
                    Session.Remove("RedirectToDefault");
                    return Redirect("/Home/index");
                }
            }

            return View();
        }
    }
}
