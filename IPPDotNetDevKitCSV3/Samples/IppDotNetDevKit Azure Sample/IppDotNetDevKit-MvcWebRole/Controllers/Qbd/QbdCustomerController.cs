using System.Linq;
using System.Web.Mvc;
using Intuit.Ipp.Data;
using IppDotNetDevKit_MvcWebRole.Models;
using IppDotNetDevKit_MvcWebRole.ServiceOperations;

namespace IppDotNetDevKit_MvcWebRole.Controllers.Qbd
{
    public class QbdCustomerController : Controller
    {
        private ServiceOperations.ServiceOperations serviceOperations;

        public QbdCustomerController()
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
        // GET: /QbdCustomer/
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

            if (Session["Operation"] != null)
            {
                ViewBag.Operation = Session["Operation"].ToString();
                Session.Remove("Operation");
                Customer customer = (Customer)Session["Entity"];
                return View(customer);
            }

            return View();
        }

        //
        // GET: /QbdCustomer/Details/5
        public ActionResult Details(string id)
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
            Customer customer = this.serviceOperations.FindByIdQbdCustomer(id.Replace('-', ':'));
            return View(customer);
        }

        //
        // GET: /QbdCustomer/Create
        public ActionResult Create()
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

        //
        // POST: /QbdCustomer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
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

                OperationStatus status = this.serviceOperations.CreateQbdCustomer(customer);
                if (status.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = status.Error;
                }
            }

            return View(customer);
        }

        //
        // GET: /QbdCustomer/Void/5
        public ActionResult Void(string id)
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

            if (string.IsNullOrWhiteSpace(id))
            {
                ViewBag.NoId = Constants.OperationRequired;
                return View();
            }

            Customer customer = this.serviceOperations.FindByIdQbdCustomer(id.Replace('-', ':'));
            IdTypeModel idTypeModel = new IdTypeModel
            {
                IdValue = customer.Id,
                DisplayName = customer.DisplayName,
                SyncToken = customer.SyncToken,
                Active = customer.Active,
                Status = customer.status.ToString()
            };

            return View(idTypeModel);
        }

        //
        // POST: /QbdCustomer/Void/5
        [HttpPost]
        public ActionResult Void(IdTypeModel idTypeModel)
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

            Customer customer = new Customer();
            customer.Id = idTypeModel.IdValue;
            customer.SyncToken = idTypeModel.SyncToken;
            OperationStatus status = this.serviceOperations.VoidQbdCustomer(customer);
            if (status.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                idTypeModel.Error = status.Error;
            }

            return View(idTypeModel);
        }

        //
        // GET: /QbdCustomer/Edit/5
        public ActionResult Edit(string id)
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

            if (string.IsNullOrWhiteSpace(id))
            {
                ViewBag.NoId = Constants.OperationRequired;
                return View();
            }

            Customer customer = this.serviceOperations.FindByIdQbdCustomer(id.Replace('-', ':'));
            UpdateModel model = new UpdateModel();
            model.SyncToken = customer.SyncToken;
            model.FamilyName = customer.FamilyName;
            model.GivenName = customer.GivenName;
            model.Id = customer.Id;
            model.MiddleName = customer.MiddleName;
            model.DisplayName = customer.DisplayName;
            model.Title = customer.Title;
            return View(model);
        }

        //
        // POST: /QbdCustomer/Edit/5
        [HttpPost]
        public ActionResult Edit(UpdateModel customer)
        {
            if (ModelState.IsValid)
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

                customer.Id = customer.Id.Replace('-', ':');
                OperationStatus status = this.serviceOperations.UpdateQbdCustomer(customer);
                if (status.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    customer.Error = status.Error;
                }
            }

            return View(customer);
        }

        //
        // GET: /QbdCustomer/Delete/5
        public ActionResult Delete(string id)
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

            if (string.IsNullOrWhiteSpace(id))
            {
                ViewBag.NoId = Constants.OperationRequired;
                return View();
            }

            Customer customer = this.serviceOperations.FindByIdQbdCustomer(id.Replace('-', ':'));
            IdTypeModel idTypeModel = new IdTypeModel 
            { 
                IdValue = customer.Id, 
                DisplayName = customer.DisplayName, 
                SyncToken = customer.SyncToken,
                Active = customer.Active,
                Status = customer.status.ToString()
            };
            return View(idTypeModel);
        }

        //
        // POST: /QbdCustomer/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(IdTypeModel idTypeModel)
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

            Customer customer = new Customer();
            customer.Id = idTypeModel.IdValue;
            customer.SyncToken = idTypeModel.SyncToken;
            OperationStatus status = this.serviceOperations.DeleteQbdCustomer(customer);
            if (status.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                idTypeModel.Error = status.Error;
            }

            return View(idTypeModel);
        }

        public ActionResult FindAll()
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
        public ActionResult FindAll(FindAllModel findAllModel)
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
            findAllModel.Customers = this.serviceOperations.FindAllQbdCustomer(1, 10, true);
            if (findAllModel.Customers == null || findAllModel.Customers.Count() == 0)
            {
                ViewBag.Results = "empty";
            }

            return View(findAllModel);
        }

        public ActionResult FindById()
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
        public ActionResult FindById(Customer customer)
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
            Customer foundCustomer = this.serviceOperations.FindByIdQbdCustomer(customer.Id);
            if (foundCustomer == null)
            {
                ViewBag.Results = "empty";
                return View(customer);
            }
            else
            {
                return View(foundCustomer);
            }

            return View(customer);
        }
    }
}
