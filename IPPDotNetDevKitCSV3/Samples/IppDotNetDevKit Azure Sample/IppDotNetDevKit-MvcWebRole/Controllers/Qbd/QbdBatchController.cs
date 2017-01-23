using System.Web.Mvc;
using IppDotNetDevKit_MvcWebRole.Models;
using IppDotNetDevKit_MvcWebRole.Models.Qbd;
using IppDotNetDevKit_MvcWebRole.ServiceOperations;

namespace IppDotNetDevKit_MvcWebRole.Controllers.Qbd
{
    public class QbdBatchController : Controller
    {
        private ServiceOperations.ServiceOperations serviceOperations;

        public QbdBatchController()
        {
        }

        private ServiceOperations.ServiceOperations Validate()
        {
            //object value = Session["SettingsModel"];
            //if (value != null)
            //{
            //    if (!((SettingsModel)value).Qbd)
            //    {
            //        return null;
            //    }
            //}
            //else
            //{
            //    return null;
            //}

            return new ServiceOperations.ServiceOperations(ServiceType.QBD);
        }

        //
        // GET: /QbdBatch/

        public ActionResult Index()
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
            
            ViewBag.Load = false;
            return View();
        }

        [HttpPost]
        public ActionResult Index(BatchModel batchModel)
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

            this.serviceOperations.BatchExecute(batchModel);
            ViewBag.Load = true;
            return View(batchModel);
        }
    }
}
