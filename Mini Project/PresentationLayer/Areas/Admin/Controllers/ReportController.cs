using PresentationLayer.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using PresentationLayer.CustomFilter; 
using BusinessLogicLayer;

namespace PresentationLayer.Areas.Admin.Controllers
{
    [AuthorizeFilter(Roles ="Admin")]
    public class ReportController : Controller
    {
        IValidation validation ;
        public ReportController()
        {
            validation=new Validation();
        }
        public ReportController(IValidation valid)
        {
            validation = valid;
        }
        // GET: Admin/Report
        public ActionResult Index()
        {
            ReportFilter filter = new ReportFilter { FilterType = type.None, Reports = validation.OverallValidPayments(null,null) };
            return View(filter);
        }
        [HttpPost]
        public ActionResult Index(ReportFilter filterModel)
        {
            DateTime? start = filterModel.StartDate;
            DateTime? end = filterModel.EndDate;
            if (start==DateTime.MinValue)
            {
                start = null;
            }
            if (end== DateTime.MinValue)
            {
                end = null;
            }
            if (filterModel.FilterType==type.None)
            {
                filterModel.Reports = validation.OverallValidPayments(start,end);
            }
            if (filterModel.FilterType==type.Category)
            {
                filterModel.Reports = validation.ValidCategoryWisePayment(start,end);
            }
            if (filterModel.FilterType == type.City)
            {
                filterModel.Reports = validation.ValidCityWisePayment(start, end);
            }
            if (filterModel.FilterType == type.Vendor)
            {
                filterModel.Reports = validation.ValidVendorWisePayment(start, end);
            }
            
            return View(filterModel);
        }
    }
}