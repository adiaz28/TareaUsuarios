using Cibertec.Models;
using Cibertec.Mvc.ActionFilters;
using Cibertec.Repositories.Dapper.Northwind;
using Cibertec.UnitOfWork;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cibertec.Mvc.Controllers
{
    [ErrorActionFilter]
    public class CustomerController : BaseController
    {
        public CustomerController(ILog log, IUnitOfWork unit): base(log, unit)
        {
        }
        public ActionResult Index()
        {
            _log.Info("Excecution of Customer Controller OK");
            return View(_unit.Customers.GetList());
        }

        public ActionResult Create() {
            return PartialView("_Create", new Customer());
        }

      

        [HttpPost]
        public ActionResult Create(Customer customer) {
            if (ModelState.IsValid) {
                _unit.Customers.Insert(customer);
                return RedirectToAction("Index");
            }
            return PartialView("_Create", customer);
        }

        public ActionResult Edit(int id) {
            return View(_unit.Customers.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Customer customer) {
            if (ModelState.IsValid) {
                _unit.Customers.Update(customer);
                return RedirectToAction("Index");
            }
            return PartialView("_Edit", customer);
        }

        public ActionResult Delete(int id) {
            return View(_unit.Customers.GetById(id));
        }

        [HttpPost]
        public ActionResult Delete(Customer customer) {
            if (_unit.Customers.Delete(customer))
                return RedirectToAction("Index");
            return View(customer);
        }

        public ActionResult Error() {
            throw new System.Exception("Test errorto validate actions");
        }
    }
}