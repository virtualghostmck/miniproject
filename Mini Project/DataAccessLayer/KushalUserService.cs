using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccessLayer
{
    
    public class KushalUserService
    {
        public AppContext kushalContext;

        public KushalUserService()
        {
            kushalContext = new AppContext();
        }
        
        /// <summary>
        /// To Add New Customer
        /// </summary>
        /// <param name="customer"></param>
        public bool AddCustomer(Customers customer)
        {
            try
            {
                int count = kushalContext.Customers.Count();
                kushalContext.Customers.Add(customer);
                kushalContext.SaveChanges();
                if (kushalContext.Customers.Count() > count)
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

        public int UpdateUser(Customers customers, int customerID)
        {
            try
            {
                int result = 0;
                Customers existingCustomers = kushalContext.Customers.FirstOrDefault(customer => customer.CustomerID == customerID);

                if(existingCustomers != null)
                {
                    existingCustomers.ContactInfo.ContactNo = customers.ContactInfo.ContactNo;
                    result = kushalContext.SaveChanges();
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
        public Customers GetUserByID(int customerID)
        {
            try
            {
                Customers existingCustomer = kushalContext.Customers.FirstOrDefault(customer => customer.CustomerID == customerID);

                return existingCustomer;

            }
            catch (SqlException)
            {
                throw;
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        ///  Add Bank Details of Specific Customer
        /// </summary>
        /// <param name="accountDetails"></param>
        public bool AddBankDetails(AccountDetails accountDetails, int customerID)
        {
            try
            {
                int count = kushalContext.AccountDetails.Count();
                Customers existingCustomer = kushalContext.Customers.FirstOrDefault(customer => customer.CustomerID == customerID);
                if (existingCustomer != null)
                {
                    kushalContext.AccountDetails.Add(accountDetails);
                    kushalContext.SaveChanges();
                    if (kushalContext.AccountDetails.Count() > count)
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
        public void GetBankDetails(int customerID)
        {
            var getBankDetails =
               (
                   from bankDetails in kushalContext.AccountDetails
                   join customerDetails in kushalContext.Customers
                   on bankDetails.CustomerID equals customerDetails.CustomerID
                   where bankDetails.CustomerID == customerID
                   select new
                   {
                        customerName = customerDetails.CustomerName,
                        bankDetails
                   }
                   
               ).ToList();

            //return getBankDetails.Distinct().ToList();

            foreach (var userDetails in getBankDetails)
            {
                Console.WriteLine(getBankDetails);
            }
        }
        
    }
}
