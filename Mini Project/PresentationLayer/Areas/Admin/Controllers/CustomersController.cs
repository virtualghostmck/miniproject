using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using Entities;
using PresentationLayer.CustomFilter;
using BusinessLogicLayer;

namespace PresentationLayer.Areas.Admin.Controllers
{
    [AuthorizeFilter(Roles = "Admin")]
    public class CustomersController : Controller
    {
        IValidation validation;

        public CustomersController()
        {
            validation = new Validation();
        }
        public CustomersController(IValidation validate)
        {
            validation = validate;
        }
        // GET: Admin/Customers
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Done View
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllUsersByList()
        {
                var usersLists = validation.getAllValidUsersByList();
                return View(usersLists);
        }
        

    }
}
