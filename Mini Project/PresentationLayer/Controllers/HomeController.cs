using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;

using PresentationLayer.CustomFilter;
using Microsoft.AspNet.Identity;
using PresentationLayer.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using PresentationLayer.Models.ViewModels;
using BusinessLogicLayer;


namespace PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        UserManager<ApplicationUser> UserManager;
        ApplicationDbContext context;
        Customers existingCustomer;
        IValidation validation;
        //UserService userServices = new UserService();

        public HomeController()
        {
            context = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            validation = new Validation();
            //existingCustomer = validation.GetValidUserByEmail(UserManager.FindByName(User.Identity.Name).Email);
        }
        public HomeController(IValidation validationObject)
        {
            validation = validationObject;
            //Session["existingCustomer"] = customer;

            //add this.Session["amount"] and this.Session["uniqueID"]
        }
        //UserService userServices = new UserService();
        //AdminServices adminServices = new AdminServices();

        public void setSessionForUnitTesting(Customers customer)
        {
            this.Session["existingCustomer"] = customer;
            this.Session["amount"] = 123;
            this.Session["uniqueID"] = "12345678";
        }

        [HttpGet]
        public ActionResult Index()
        {
            existingCustomer = validation.ValidateGetUserByEmail(UserManager.FindByName(User.Identity.Name).Email);
            this.Session["existingCustomer"] = existingCustomer;
           
            return RedirectToAction("Details");
           
        }

        //[HttpPost]
        //public ActionResult Index(int customerID)
        //{
        //    Customers customers = userServices.GetUserByID(customerID);
        //    return View(customers);
        //}

        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return View();
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}

        /// <summary>
        /// Done View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details()
        {
            //existingCustomer = userServices.GetUserByEmail(UserManager.FindByName(User.Identity.Name).Email);
            existingCustomer = this.Session["existingCustomer"] as Customers;
            try
            {
                return View(existingCustomer);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        //[HttpPost]
        //public ActionResult Register(Customers customers)
        //{
        //    try
        //    {
        //        bool result = userServices.AddCustomer(customers);
        //        if (result == true)
        //            return RedirectToAction("Index");
        //        else
        //            return Content("Unable to add new customer");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        /// <summary>
        /// Get method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit()
        {
            existingCustomer = this.Session["existingCustomer"] as Customers;
            try
            {
                return View(existingCustomer);
            }
            catch
            {
                return Content("Unable to load edit page");
            }
        }

        /// <summary>
        /// Post method
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customers"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Customers customers)
        {
            existingCustomer = this.Session["existingCustomer"] as Customers;
            int result = 0;
            try
            {
                result = validation.UpdateValidUser(customers, existingCustomer.CustomerID);
                if (result > 0)
                    return RedirectToAction("Index");
                else
                    return Content("Unable to update customers");
            }
            catch(Exception e)
            {
                return Content(e.Message);
            }
        }

        /// <summary>
        /// Done View
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllVendorDetails()
        {
            try
            {
                existingCustomer = this.Session["existingCustomer"] as Customers;
                List<Vendors> getAllVendors = validation.GetVendors(existingCustomer.CustomerID);
                return View(getAllVendors);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        /// <summary>
        /// Get method
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AddBankDetails()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return Content("Cannot add more than one bank account");
            }
        }

        /// <summary>
        /// Post method
        /// </summary>
        /// <param name="accountDetails"></param>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddBankDetails(AccountDetails accountDetails)
        {
            try
            {
                existingCustomer = this.Session["existingCustomer"] as Customers;
                bool result = validation.AddBankDetails(accountDetails, existingCustomer.CustomerID);
                if (result)
                    return RedirectToAction("Index","Home");
                else
                    return Content("Unable to add bank details");
            }
            catch (Exception e)
            {
                return Content("Cannot add more than one bank account");
            }
        }

        /// <summary>
        /// Done View
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        /// 
        [AuthorizeFilter(Roles ="Customer")]
        public ActionResult GetBankDetails()
        {
            try
            {
                existingCustomer = this.Session["existingCustomer"] as Customers;
                AccountDetails accountDetails = validation.GetBankDetails(existingCustomer.CustomerID);
                return View(accountDetails);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }

        /// <summary>
        /// Done View
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public ActionResult GetPaymentDetails()
        {
            try
            {
                //this.Session["existingCustomer"] = new Customers
                //{
                //    CityID = 1,
                //    ContactInfo = { ContactNo = "8877778899", Email = "abc@fd.com" },
                //    CustomerAddress = "TestAddress",
                //    CustomerID = 1,
                //    CustomerName = "Test",
                //    Gender = "female",
                //    Password = "Test@123"
                //};
                existingCustomer = this.Session["existingCustomer"] as Customers;
                List<Transactions> paymentDetails = validation.GetTransactions(existingCustomer.CustomerID);
                return View(paymentDetails);
            }
            catch (Exception e)
            {

                return Content("Unable to load payment details");
                throw;
            }
        }


        [HttpGet]
        public ActionResult GetAmount()
        {
            try
            {
                ViewBag.Category = new SelectList(validation.GetCategories(), "CategoryID", "CategoryName");
              //  ViewBag.Vendor = new SelectList(userServices.GetVendors(existingCustomer.CustomerID), "VendorID", "VendorName");
                ViewBag.Vendor = new SelectList(validation.GetAllVendors(), "VendorID", "VendorName");
                return View();
            }
            catch
            {
                return Content("Unable to load vendor getAmount page");
            }
        }

        [HttpPost]
        public ActionResult ShowAmount(GetAmountViewModel model)
        {
            try
            {
                int amountDue = validation.GetValidVendorAmountDue(model.UniqueID, int.Parse(model.Vendors));
                if(amountDue == -1)
                {
                    throw new Exception("Invalid Details");
                }
                model.Amount = amountDue;
                this.Session["amount"] = Convert.ToInt32(model.Amount);
                this.Session["uniqueID"] = model.UniqueID;
                model.Vendors = validation.GetValidVendorByID(int.Parse(model.Vendors)).VendorName;
                model.Categories = validation.GetValidCategoryByID(int.Parse(model.Categories)).CategoryName;
                return View(model);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(Roles ="Customer")]
        public ActionResult MakePayment()
        {
            try
            {
                existingCustomer = this.Session["existingCustomer"] as Customers;
                AccountDetails account = validation.GetBankDetails(existingCustomer.CustomerID);
                if (account == null)
                {
                    return RedirectToAction("AddBankDetails","Home");
                }
                return View(account);
            }
            catch (Exception e)
            {

                return Content(e.Message);
            }
            
        }

        //[HttpPost] uselessssss
        //public ActionResult MakePayment(Transactions transaction)
        //{
        //    try
        //    {
        //        bool result = userServices.AddTransaction(transaction);
        //        if (result == true)
        //            return RedirectToAction("MakePayment");
        //        else
        //            return Content("Unable to make payment");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizeFilter(Roles ="Customer")]
        public ActionResult PaymentDetails()
        {
            try
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[10];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                var randomID = new String(stringChars);
                existingCustomer = this.Session["existingCustomer"] as Customers;
                Transactions currentTransaction = new Transactions { TransactionID =randomID ,Amount = int.Parse(this.Session["amount"].ToString()),TransactionDate=DateTime.Now,UniqueID=this.Session["uniqueID"].ToString() };
                validation.AddTransaction(currentTransaction);
                validation.UpdateAmountDue(this.Session["uniqueID"].ToString());
                validation.UpdateCustomerVendorForConsistency(this.Session["uniqueID"].ToString(), existingCustomer.CustomerID);
                return View(currentTransaction);
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
        

        /// <summary>
        /// Done View
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        [HttpGet]
        [AuthorizeFilter(Roles ="Customer")]
        public ActionResult PaymentHistory()
        {
            try
            {
                existingCustomer = this.Session["existingCustomer"] as Customers;
                List<Report> paymentHistory = validation.OverallPayments(existingCustomer.CustomerID);
                return View(paymentHistory);
            }
            catch (Exception)
            {
                return Content("Unable to fetch payment history");
            }
        }

    }
}