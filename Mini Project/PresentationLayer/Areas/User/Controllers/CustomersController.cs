using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentationLayer.Areas.User.Controllers
{
    public class CustomersController : Controller
    {
        // GET: User/Customers
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Customers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Customers/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Customers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Customers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Customers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Customers/Delete/5
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
