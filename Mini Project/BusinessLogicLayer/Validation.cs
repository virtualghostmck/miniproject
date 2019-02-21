using DataAccessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace BusinessLogicLayer
{
    public class Validation:IValidation
    {

        IAdminServices services;
        IUserService userServices;
        public Validation()
        {
            services = new AdminServices();
            userServices = new UserService();
        }

        public Validation(IAdminServices adminService,IUserService userService)
        {
            services = adminService;
            userServices = userService;
        }
        public List<Categories> GetAllValidCategories()
        {
            try
            {
                List<Categories> categoryList = services.GetAllCategories();
                return categoryList;
            }
            catch (SqlException)
            {
                throw;
            }

            catch (Exception)
            {
                throw;
            }
        }


        public Customers ValidateGetUserByEmail(string email)
        {
            try
            {
                return userServices.GetUserByEmail(email);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public int AddValidCategory(Categories category)
        {
            try
            {
                int result = services.AddCategory(category);
                return result;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int UpdateValidCategory(Categories category, int categoryId)
        {
            try
            {
                int result = services.UpdateCategory(category,categoryId);
                return result;
            }
            catch (SqlException)
            {
                throw;
            }

            catch (Exception)
            {
                throw;
            }
        }



        public List<City> GetAllValidCities()
        {
            try
            {
                List<City> cities = services.GetAllCities();
                return cities;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int AddValidCity(City city)
        {
            try
            {
                int result = services.AddCity(city);
                return result;
            }
            catch (SqlException)
            {
                throw;
            }

            catch (Exception)
            {
                throw;
            }

        }


        public int UpdateValidCity(City city, int cityId)
        {
            try
            {
                int result = services.UpdateCity(city,cityId);           
                return result;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Customers> getAllValidUsersByList()
        {
            try
            {
                List<Customers> customerDetails = services.getAllUsersByList();
                return customerDetails;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Report> OverallValidPayments(DateTime? Start = null, DateTime? End = null)
        {
            try
            {
                List<Report> result = services.OverallPayments(Start, End);
                return result;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Report> ValidCategoryWisePayment(DateTime? Start = null, DateTime? End = null)
        {
            try
            {
                List<Report> result = services.CategoryWisePayment(Start, End);
                return result;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Report> ValidCityWisePayment(DateTime? Start = null, DateTime? End = null)
        {
            try
            {
                List<Report> result = services.CityWisePayment(Start, End);
                return result;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Report> ValidVendorWisePayment(DateTime? Start = null, DateTime? End = null)
        {
            try
            {
                List<Report> result = services.VendorWisePayment(Start,End);
                return result;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Vendors> GetVendors(int customerID)
        {
            try
            {
                return userServices.GetVendors(customerID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Vendors> GetAllValidVendors()
        {
            try
            {
                List<Vendors> vendors = services.GetAllVendors();
                return vendors;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public int AddValidVendor(Vendors vendor)
        {
            try
            {
                int result = services.AddVendor(vendor);
                return result;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool AddBankDetails(AccountDetails accountDetails, int customerID)
        {
            try
            {
                return userServices.AddBankDetails(accountDetails, customerID);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public int UpdateValidVendor(Vendors vendor, int vendorId)
        {
            try
            {
                int result = services.UpdateVendor(vendor,vendorId);
                return result;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public AccountDetails GetBankDetails(int customerID)
        {
            try
            {
                return userServices.GetBankDetails(customerID);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public Customers GetValidUserByEmail(string email)
        {
            try
            {
                Customers existingCustomer = userServices.GetUserByEmail(email);

                return existingCustomer;

            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Transactions> GetTransactions(int customerID)
        {
            try
            {
                return userServices.GetTransactions(customerID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Categories> GetCategories()
        {
            try
            {
               return userServices.GetCategories();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Vendors> GetAllVendors()
        {
            try
            {
                return services.GetAllVendors();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int UpdateValidUser(Customers customers, int customerID)
        {
            try
            {
                int result = 0;
                result = userServices.UpdateUser(customers,customerID);
                return result;
            }
            catch (SqlException)
            {

                throw;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<Vendors> GetValidVendors(int customerID)
        {
            List<Vendors> vendors = userServices.GetVendors(customerID);
            return vendors;

        }

        public bool AddCustomer(Customers customer)
        {
            try
            {
                return userServices.AddCustomer(customer);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool AddValidBankDetails(AccountDetails accountDetails, int? customerID)
        {
            try
            {
                bool result = userServices.AddBankDetails(accountDetails, customerID);
                return result;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public AccountDetails GetValidBankDetails(int? customerID)
        {
            AccountDetails result = userServices.GetBankDetails(customerID);
            return result;
        }

        //Same Method repeated above
        public List<Transactions> GetValidTransactions(int customerID)
        {
            List<Transactions> result = userServices.GetTransactions(customerID);
            return result;
        }
        //Same Method repeated above
        public List<Categories> GetValidCategories()
        {
            List<Categories> result = userServices.GetCategories();
            return result;
        }


        public int GetValidVendorAmountDue(string uniqueID, int vendorID)
        {
            try
            {
                int result = userServices.GetVendorAmountDue(uniqueID, vendorID);
                return result;
            }
            catch(InvalidOperationException e)
            {
                return 0;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public bool AddValidTransaction(Transactions transact)
        {
            bool result = userServices.AddTransaction(transact);
            return result;
        }
        //not used anywhere
        public bool UpdateValidAmountDue(string uniqueID)
        {
            bool result = userServices.UpdateAmountDue(uniqueID);
            return result;
        }

        //not used in project anywhere
        public List<Report> OverallValidPayments(int customerID)
        {
            List<Report> result = userServices.OverallPayments(customerID);
            return result;
        }

        public Vendors GetValidVendorByID(int id)
        {
            Vendors result = userServices.GetVendorByID(id);
            return result;
        }

        public bool UpdateAmountDue(string uniqueID)
        {
            try
            {
                return userServices.UpdateAmountDue(uniqueID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        //Same Method repeated above
        public bool AddTransaction(Transactions currentTransaction)
        {
            try
            {
                return userServices.AddTransaction(currentTransaction);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Categories GetValidCategoryByID(int id)
        {
            Categories result = userServices.GetCategoryByID(id);
            return result;
        }

        public List<Report> OverallPayments(int customerID)
        {
            try
            {
                return userServices.OverallPayments(customerID);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateCustomerVendorForConsistency(string uniqueID, int customerID)
        {
            try
            {
                return userServices.UpdateCustomerVendorForConsistency(uniqueID, customerID);

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
