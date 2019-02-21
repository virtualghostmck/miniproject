using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IUserService
    {
        List<Vendors> GetVendors(int customerID);

        bool UpdateAmountDue(string uniqueID);

        bool AddTransaction(Transactions transaction);

        List<Transactions> GetTransactions(int customerID);

        Customers GetUserByEmail(string email);

        List<Categories> GetCategories();

        List<Vendors> GetVendorByCategory(int customerID, int categoryID);

        int GetVendorAmountDue(string uniqueID, int vendorID);

        bool AddCustomer(Customers customer);

        int UpdateUser(Customers customers, int customerID);

        Customers GetUserByID(int? customerID);

        bool AddBankDetails(AccountDetails accountDetails, int? customerID);

        AccountDetails GetBankDetails(int? customerID);

        int GetCityIDByName(string cityName);

        Vendors GetVendorByID(int id);

        Categories GetCategoryByID(int id);

        List<Report> OverallPayments(int customerID);

        bool UpdateCustomerVendorForConsistency(string uniqueID, int customerID);
    }
}
