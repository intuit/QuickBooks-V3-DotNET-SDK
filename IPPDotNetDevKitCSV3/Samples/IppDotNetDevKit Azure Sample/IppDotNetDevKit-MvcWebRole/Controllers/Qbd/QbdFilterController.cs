using System.Linq;
using System.Web.Mvc;
using IppDotNetDevKit_MvcWebRole.Models;
using IppDotNetDevKit_MvcWebRole.ServiceOperations;
using Intuit.Ipp.Exception;

namespace IppDotNetDevKit_MvcWebRole.Controllers.Qbd
{
    public class QbdFilterController : Controller
    {
        private ServiceOperations.ServiceOperations serviceOperations;

        public QbdFilterController()
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

            return new ServiceOperations.ServiceOperations(ServiceType.Query);
        }

        //
        // GET: /QbdFilter/

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

            return View();
        }

        public ActionResult Where()
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

            ViewBag.Load = true;
            return View();
        }

        [HttpPost]
        public ActionResult Where(QueryModel.WhereQuery whereQuery)
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
            whereQuery = this.serviceOperations.WhereQbdInvoiceQuery(whereQuery);
            if (whereQuery.Entities == null || whereQuery.Entities.Count() == 0)
            {
                ViewBag.Results = "empty";
            }

            return View(whereQuery);
        }

        public ActionResult Select()
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

            ViewBag.Load = true;
            return View();
        }

        [HttpPost]
        public ActionResult Select(QueryModel.SelectQuery selectQuery)
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
            selectQuery = this.serviceOperations.SelectQbdInvoiceQuery(selectQuery);
            return View(selectQuery);
        }

        public ActionResult Count()
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

            ViewBag.Load = true;
            return View();
        }

        [HttpPost]
        public ActionResult Count(QueryModel.CountQuery countQuery)
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

            return View(this.serviceOperations.CountQbdInvoiceQuery(countQuery));
        }

        public ActionResult Pagination()
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

            ViewBag.Load = true;
            return View();
        }

        [HttpPost]
        public ActionResult Pagination(FindAllModel findAllModel)
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
            findAllModel.Invoices = this.serviceOperations.FindAllQbdInvoice(findAllModel);
            if (findAllModel.Invoices == null || findAllModel.Invoices.Count() == 0)
            {
                ViewBag.Results = "empty";
            }

            return View(findAllModel);
        }

        public ActionResult OrderBy()
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

            ViewBag.Load = true;
            return View();
        }

        [HttpPost]
        public ActionResult OrderBy(QueryModel.OrderByModel orderByModel)
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
            orderByModel.Entities = this.serviceOperations.OrderByQbdInvoice(orderByModel);
            if (orderByModel.Entities == null || orderByModel.Entities.Count() == 0)
            {
                ViewBag.Results = "empty";
            }

            return View(orderByModel);
        }

        public ActionResult Complex()
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

            ViewBag.Load = true;
            return View();
        }

        [HttpPost]
        public ActionResult Complex(QueryModel.ComplexQueryModel complexModel)
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
            complexModel.Entities = this.serviceOperations.ComplexQueryQbdInvoice(complexModel);
            if (complexModel.Entities == null || complexModel.Entities.Count() == 0)
            {
                ViewBag.Results = "empty";
            }

            return View(complexModel);
        }

        public ActionResult QueryString()
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
        public ActionResult QueryString(QueryModel.QueryStringModel queryStringModel)
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

            if (string.IsNullOrWhiteSpace(queryStringModel.SimpleQuery))
            {
                queryStringModel.Success = false;
                queryStringModel.Error = "Enter the invoice query string";
                return View(queryStringModel);
            }

            return View(this.serviceOperations.ExecuteIdsQuery(queryStringModel));
        }

        public ActionResult MultipleQueries()
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
        public ActionResult MultipleQueries(QueryModel.MultipleQueriesModel queryStringModel)
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

            if (string.IsNullOrWhiteSpace(queryStringModel.Query1) || string.IsNullOrWhiteSpace(queryStringModel.Query2))
            {
                queryStringModel.Success = false;
                queryStringModel.Error = "Enter both the invoice query string.";
                return View(queryStringModel);
            }

            return View(this.serviceOperations.ExecuteMultipleQuery(queryStringModel));
        }

    }
}