using System.Web.Mvc;
using IppDotNetDevKit_MvcWebRole.Models;

namespace IppDotNetDevKit_MvcWebRole.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your quintessential contact page.";

            return View();
        }

        public ActionResult Logging()
        {
            return View();
        }

        public ActionResult Settings()
        {
            if (Session["SettingsModel"] != null)
            {
                return View(Session["SettingsModel"] as SettingsModel);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Settings(SettingsModel settingsModel)
        {
            settingsModel.Success = true;
            settingsModel.Error = string.Empty;
            if (settingsModel.Qbd)
            {
                if (string.IsNullOrWhiteSpace(settingsModel.QbdAccessToken))
                {
                    settingsModel.Success = false;
                    settingsModel.Error += "Please specify Qbd access token \n";
                }

                if (string.IsNullOrWhiteSpace(settingsModel.QbdAccessTokenSecret))
                {
                    settingsModel.Success = false;
                    settingsModel.Error += "Please specify Qbd access token secret \n";
                }

                if (string.IsNullOrWhiteSpace(settingsModel.QbdConsumerKey))
                {
                    settingsModel.Success = false;
                    settingsModel.Error += "Please specify Qbd consumer key \n";
                }

                if (string.IsNullOrWhiteSpace(settingsModel.QbdConsumerSecret))
                {
                    settingsModel.Success = false;
                    settingsModel.Error += "Please specify Qbd consumer secret \n";
                }

                if (string.IsNullOrWhiteSpace(settingsModel.QbdRealmId))
                {
                    settingsModel.Success = false;
                    settingsModel.Error += "Please specify Qbd realm id \n";
                }
            }

            if (settingsModel.Ipp)
            {
                if (string.IsNullOrWhiteSpace(settingsModel.IppAccessToken))
                {
                    settingsModel.Success = false;
                    settingsModel.Error += "Please specify Ipp access token \n";
                }

                if (string.IsNullOrWhiteSpace(settingsModel.IppAccessTokenSecret))
                {
                    settingsModel.Success = false;
                    settingsModel.Error += "Please specify Ipp access token secret \n";
                }

                if (string.IsNullOrWhiteSpace(settingsModel.IppConsumerKey))
                {
                    settingsModel.Success = false;
                    settingsModel.Error += "Please specify Ipp consumer key \n";
                }

                if (string.IsNullOrWhiteSpace(settingsModel.IppConsumerSecret))
                {
                    settingsModel.Success = false;
                    settingsModel.Error += "Please specify Ipp consumer secret \n";
                }

                if (string.IsNullOrWhiteSpace(settingsModel.AppToken))
                {
                    settingsModel.Success = false;
                    settingsModel.Error += "Please specify Ipp app token \n";
                }

                if (string.IsNullOrWhiteSpace(settingsModel.AppDbid))
                {
                    settingsModel.Success = false;
                    settingsModel.Error += "Please specify Ipp app dbid \n";
                }
            }

            if (settingsModel.Success)
            {
                Session["SettingsModel"] = settingsModel;
                return RedirectToAction("Index");
            }

            return View(settingsModel);
        }
    }
}
