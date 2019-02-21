using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PresentationLayer.Models.ViewModels;
using PresentationLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;

namespace PresentationLayer.Areas.Admin.Controllers
{
    public class AdminHimselfController : Controller
    {
        // GET: Admin/AdminHimself

        UserManager<ApplicationUser> UserManager;
        ApplicationDbContext context;
        //Customers existingCustomer;
        AdminViewModel admin;

        public object SessionManager { get; private set; }

        public AdminHimselfController()
        {
            context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser adminOrig = UserManager.FindByEmail("mck@gmail.com");
            admin = new AdminViewModel();
            admin.Name = adminOrig.UserName;
            admin.Email = adminOrig.Email;
        }

        public AdminHimselfController(AdminViewModel adminViewModel)
        {
            admin = adminViewModel;
        }

        // GET: Admin/AdminHimself/Details/5
        public ActionResult Details()
        {         
            return View(admin);
        }
        
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}
