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
    
    public class CityController : Controller
    {
        //add update //get city

        IValidation validation;

        public CityController()
        {
             validation = new Validation();
        }


        public CityController(IValidation validate)
        {
            validation = validate;
        }

        // GET: Admin/City
        
        public ActionResult Index()
        {
            var cityList = validation.GetAllValidCities();
            return View(cityList);
        }

    

        // GET: Admin/City/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/City/Create
        [HttpPost]
        public ActionResult Create(City cities)
        {
            try
            {
                    int result = 0;
                    result = validation.AddValidCity(cities);
                    if (result > 0)
                        return RedirectToAction("Index");
                    else
                        return Content("Unable to add city.");
                
            }
            catch
            {
                return Content("Unable to add city.");
            }
        }

        // GET: Admin/City/Edit/5
        public ActionResult Edit(int id)
        {
            
            return View();
        }

        // POST: Admin/City/Edit/5
        [HttpPost]
        public ActionResult Edit(City existingCities ,int id)
        {
            try
            {
              
                   int result = validation.UpdateValidCity(existingCities, id);
                    if (result > 0)
                        return RedirectToAction("Index");
                    else
                        return Content("Unable to update city.");
          
              
            }
            catch
            {
                return Content("Unable to update city.");
            }
        }

     
    }
}
