using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using Entities;

namespace PresentationLayer.Areas.User.Controllers
{
    public class CustomersController : Controller
    {
        UserService userServices = new UserService();
        // GET: User/Customer
        public ActionResult Index()
        {

            return View();
        }

        // GET: User/Customer/Details/5
        public ActionResult Details(int id)
        {
            try
            {
               Customers existingCustomer = userServices.GetUserByID(id);
                return View(existingCustomer);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: User/Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Customer/Create
        [HttpPost]
        public ActionResult Create(Customers customers)
        {
            try
            {
                bool result = userServices.AddCustomer(customers);
                if (result == true)
                    return RedirectToAction("Index");
                else
                    return Content("Unable to add new customer");                
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customers customers)
        {
            int result = 0;
            try
            {
                result = userServices.UpdateUser(customers, id);
                if (result > 0)
                    return RedirectToAction("Index");
                else
                    return Content("Unable to update customers");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
