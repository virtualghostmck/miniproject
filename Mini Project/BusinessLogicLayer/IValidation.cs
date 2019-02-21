using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace BusinessLogicLayer
{
    public interface IValidation
    {
        List<Categories> GetAllValidCategories();
        Customers ValidateGetUserByEmail(string email);
        int AddValidCategory(Categories category);
        int UpdateValidCategory(Categories category, int categoryId);
        List<City> GetAllValidCities();
        int AddValidCity(City city);
        int UpdateValidCity(City city, int cityId);
        List<Customers> getAllValidUsersByList();
        List<Report> OverallValidPayments(DateTime? Start = null, DateTime? End = null);
        List<Report> ValidCategoryWisePayment(DateTime? Start = null, DateTime? End = null);
        List<Report> ValidCityWisePayment(DateTime? Start = null, DateTime? End = null);
        List<Report> ValidVendorWisePayment(DateTime? Start = null, DateTime? End = null);
        List<Vendors> GetVendors(int customerID);
        List<Vendors> GetAllValidVendors();
        int AddValidVendor(Vendors vendor);
        bool AddBankDetails(AccountDetails accountDetails, int customerID);
        int UpdateValidVendor(Vendors vendor, int vendorId);
        AccountDetails GetBankDetails(int customerID);
        Customers GetValidUserByEmail(string email);
        List<Transactions> GetTransactions(int customerID);
        List<Categories> GetCategories();
        List<Vendors> GetAllVendors();
        int UpdateValidUser(Customers customers, int customerID);
        List<Vendors> GetValidVendors(int customerID);
        bool AddCustomer(Customers customer);
        bool AddValidBankDetails(AccountDetails accountDetails, int? customerID);
        AccountDetails GetValidBankDetails(int? customerID);
        List<Transactions> GetValidTransactions(int customerID);
        List<Categories> GetValidCategories();
        int GetValidVendorAmountDue(string uniqueID, int vendorID);
        bool AddValidTransaction(Transactions transact);
        bool UpdateValidAmountDue(string uniqueID);
        List<Report> OverallValidPayments(int customerID);
        Vendors GetValidVendorByID(int id);
        bool UpdateAmountDue(string uniqueID);
        bool AddTransaction(Transactions currentTransaction);
        Categories GetValidCategoryByID(int id);
        List<Report> OverallPayments(int customerID);
        bool UpdateCustomerVendorForConsistency(string uniqueID, int customerID);

    }
}
