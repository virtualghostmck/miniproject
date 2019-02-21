using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class UserService:IUserService
    {
        IAppContext dataContext;
        public UserService()
        {
            dataContext = new AppContext();
        }
        public UserService(IAppContext appContext)
        {
            dataContext = appContext;
        }
        public List<Vendors> GetVendors(int customerID)
        {
            var vendorList = from vend in dataContext.Vendors
                             join custvend in dataContext.CustomerVendors
                             on vend.VendorID equals custvend.VendorID
                             where custvend.CustomerID == customerID
                             select vend;
            return vendorList.ToList();

        }
        //

        public bool UpdateAmountDue(string uniqueID)
        {
            CustomerVendor customerVendor = dataContext.CustomerVendors.First(cus => cus.UniqueID.Equals(uniqueID));
            customerVendor.AmountDue = 0;
            return dataContext.SaveChanges() > 0;
        }


        public bool AddTransaction(Transactions transact)
        {
            int count = dataContext.Transactions.Count();
            dataContext.Transactions.Add(transact);
            dataContext.SaveChanges();
            if (dataContext.Transactions.Count() > count)
            {
                return true;
            }
            return false;
        }


        public List<Transactions> GetTransactions(int customerID)
        {
            var transactList = from tran in dataContext.Transactions
                               join custven in dataContext.CustomerVendors
                               on tran.UniqueID equals custven.UniqueID
                               join cus in dataContext.Customers
                               on custven.CustomerID equals cus.CustomerID
                               where cus.CustomerID == customerID
                               select tran;
            return transactList.ToList();
        }

        public Customers GetUserByEmail(string email)
        {
            try
            {
                Customers existingCustomer = dataContext.Customers.FirstOrDefault(customer => customer.ContactInfo.Email.Equals(email));

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

        //global: even for admin
        public List<Categories> GetCategories()
        {
            return dataContext.Categories.ToList();
        }
        public List<Vendors> GetVendorByCategory(int customerID, int categoryID)
        {
            var vendorList = from ven in dataContext.Vendors
                             join cusven in dataContext.CustomerVendors
                             on ven.VendorID equals cusven.VendorID
                             where cusven.CustomerID == customerID && cusven.CategoryID == categoryID
                             select ven;
            return vendorList.Distinct().ToList();
        }

        //getAmount
        public int GetVendorAmountDue(string uniqueID, int vendorID)
        {

            try
            {
                var planList = from cv in dataContext.CustomerVendors
                               where cv.VendorID == vendorID && cv.UniqueID.Equals(uniqueID)
                               select cv.AmountDue;
                               

                return planList.First();
            }
            catch(InvalidOperationException e)
            {
                return -1;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        //public bool CheckValidUser(int uniqueID, int vendorID, int categoryID)
        //{
        ////    var user = dataContext.CustomerVendors
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// To Add New Customer
        /// </summary>
        /// <param name="customer"></param>
        /// 
        public bool AddCustomer(Customers customer)
        {
            try
            {
                int count = dataContext.Customers.Count();
                dataContext.Customers.Add(customer);
                dataContext.SaveChanges();
                if (dataContext.Customers.Count() > count)
                    return true;
                else
                    return false;
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

        /// <summary>
        ///  To update contact information of customer
        /// </summary>
        /// <param name="customers"></param>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public int UpdateUser(Customers customers, int customerID)
        {
            try
            {
                int result = 0;
                Customers existingCustomers = dataContext.Customers.FirstOrDefault(customer => customer.CustomerID == customerID);

                if (existingCustomers != null)
                {
                    existingCustomers.ContactInfo.ContactNo = customers.ContactInfo.ContactNo;
                    result = dataContext.SaveChanges();
                }
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


        /// <summary>
        ///  To Get Specific User
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public Customers GetUserByID(int? customerID)
        {
            try
            {
                Customers existingCustomer = dataContext.Customers.FirstOrDefault(customer => customer.CustomerID == customerID);

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

        /// <summary>
        ///  Add Bank Details of Specific Customer
        /// </summary>
        /// <param name="accountDetails"></param>
        public bool AddBankDetails(AccountDetails accountDetails, int? customerID)
        {
            try
            {
                int count = dataContext.AccountDetails.Count();
                Customers existingCustomer = dataContext.Customers.FirstOrDefault(customer => customer.CustomerID == customerID);
                if (existingCustomer != null)
                {
                    accountDetails.CustomerID = existingCustomer.CustomerID;
                    dataContext.AccountDetails.Add(accountDetails);
                    dataContext.SaveChanges();
                    if (dataContext.AccountDetails.Count() > count)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
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

        //List<AccountDetails>
        public AccountDetails GetBankDetails(int? customerID)
        {
            try
            {
                var getBankDetails =
               (
                   from bankDetails in dataContext.AccountDetails
                   join customerDetails in dataContext.Customers
                   on bankDetails.CustomerID equals customerDetails.CustomerID
                   where bankDetails.CustomerID == customerID
                   select bankDetails
               );

                return getBankDetails.First();
            }
            catch (Exception e)
            {

                return null;
            }
            
        }

        public int GetCityIDByName(string cityName)
        {
            var city = dataContext.City.FirstOrDefault(c => c.CityName.ToLower().Equals(cityName.ToLower()));
            return city.CityID;
        } 
        


        public Vendors GetVendorByID(int id)
        {
            return dataContext.Vendors.First(v=>v.VendorID==id);
        }
        public Categories GetCategoryByID(int id)
        {
            return dataContext.Categories.First(v => v.CategoryID== id);
        }

        public List<Report> OverallPayments(int customerID)
        {
            var query = from Transactions in dataContext.Transactions
                        join CustomerVendors in dataContext.CustomerVendors
                          on Transactions.UniqueID
                              equals CustomerVendors.UniqueID
                        join Categories in dataContext.Categories
                           on CustomerVendors.CategoryID
                           equals Categories.CategoryID
                        join Customers in dataContext.Customers
                         on CustomerVendors.CustomerID
                         equals Customers.CustomerID
                        join vendors in dataContext.Vendors
                        on CustomerVendors.VendorID
                        equals vendors.VendorID
                        join city in dataContext.City
                        on Customers.CityID equals city.CityID
                        where Customers.CustomerID==customerID
                        select new Report
                        {

                            TransactionID = Transactions.TransactionID,
                            TransactionDate = Transactions.TransactionDate,
                            UniqueID = CustomerVendors.UniqueID,
                            Amount = Transactions.Amount,
                            VendorName = vendors.VendorName,
                            CategoryName = Categories.CategoryName,
                            CustomerName = Customers.CustomerName,
                            City = city.CityName
                        };

            return query.ToList();
        }

        public bool UpdateCustomerVendorForConsistency(string uniqueID, int customerID)
        {
            CustomerVendor cv = dataContext.CustomerVendors.First(cus => cus.UniqueID.Equals(uniqueID));
            if (cv != null)
            {
                cv.CustomerID = customerID;
                return dataContext.SaveChanges() > 0;
            }
            else
                return false;
        }

    }
}
