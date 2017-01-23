using System.Web.Mvc;
using IppDotNetDevKit_MvcWebRole.Models;
using IppDotNetDevKit_MvcWebRole.ServiceOperations;

namespace IppDotNetDevKit_MvcWebRole.Controllers.PlatformServices
{
    public class PlatformServiceController : Controller
    {
        private ServiceOperations.ServiceOperations serviceOperations;

        public PlatformServiceController()
        {
        }

        private ServiceOperations.ServiceOperations Validate()
        {
            //object value = Session["SettingsModel"];
            //if (value != null)
            //{
            //    if (!((SettingsModel)value).Ipp)
            //    {
            //        return null;
            //    }
            //}
            //else
            //{
            //    return null;
            //}

            return new ServiceOperations.ServiceOperations(ServiceType.IPS);
        }

        public ActionResult GetIdsRealm()
        {
            if (this.serviceOperations == null)
            {
                ServiceOperations.ServiceOperations serviceOp = this.Validate();
                if (serviceOp == null)
                {
                    return RedirectToAction("Settings", "Home");
                }

                this.serviceOperations = serviceOp;
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult GetIdsRealm(PlatformModel model)
        {
            if (this.serviceOperations == null)
            {
                ServiceOperations.ServiceOperations serviceOp = this.Validate();
                if (serviceOp == null)
                {
                    return RedirectToAction("Settings", "Home");
                }

                this.serviceOperations = serviceOp;
            }
            
            this.serviceOperations.GetIdsRealm(model);
            return View(model);
        }

        public ActionResult GetIsRealmQbo()
        {
            if (this.serviceOperations == null)
            {
                ServiceOperations.ServiceOperations serviceOp = this.Validate();
                if (serviceOp == null)
                {
                    return RedirectToAction("Settings", "Home");
                }

                this.serviceOperations = serviceOp;
            }
            
            return View();
        }

        [HttpPost]
        public ActionResult GetIsRealmQbo(PlatformModel model)
        {
            if (this.serviceOperations == null)
            {
                ServiceOperations.ServiceOperations serviceOp = this.Validate();
                if (serviceOp == null)
                {
                    return RedirectToAction("Settings", "Home");
                }

                this.serviceOperations = serviceOp;
            }
            
            this.serviceOperations.GetIsRealmQbo(model);
            return View(model);
        }
    }
}
