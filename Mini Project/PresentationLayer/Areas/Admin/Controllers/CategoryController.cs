using BusinessLogicLayer;
using Entities;
using PresentationLayer.CustomFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentationLayer.Areas.Admin.Controllers
{
   // [AuthorizeFilter(Roles = "Admin")]
    public class CategoryController : Controller
    {

        IValidation validation;

        public CategoryController()
        {
            validation = new Validation();
        }

        public CategoryController(IValidation validate)
        {
            validation = validate;
        }

        // GET: Admin/Category
        [AllowAnonymous]
        public ActionResult Index()
        {
            var CategoryList = validation.GetAllValidCategories();
            return View(CategoryList);
        }
        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: Admin/Category/Create
        [HttpPost]
        public ActionResult Create(Categories newCategory)
        {
            try
            {
                int result = 0;
                if (ModelState.IsValid)
                {
                    result = validation.AddValidCategory(newCategory);
                    if (result > 0)
                        return RedirectToAction("Index");
                    else
                        return Content("Unable to add category.");
                }
            }
            catch
            {
                return Content("Unable to add category.");
            }
            return View();
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Category/Edit/5
        [HttpPost]
        public ActionResult Edit(Categories existingCategory,int id)
        {
            try
            {

                int result = validation.UpdateValidCategory(existingCategory,id);
                if (result > 0)
                    return RedirectToAction("Index");
                else
                    return Content("Unable to update category.");
            }
            catch
            {
                return Content("Unable to update category.");
                
            }
        }

       
        
    }
}
