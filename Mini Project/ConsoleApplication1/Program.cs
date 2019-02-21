using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Entities;
namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            UserService userOps = new UserService();

            List<Vendors> vendorsOfCustomer = userOps.GetVendors(22);

        //    userOps.AddTransaction(new Transactions { Amount = 222, UniqueID = "3799177237", TransactionDate = DateTime.Now, TransactionID = "101" });

            //List<Transactions> tra = userOps.GetTransactions(26);

            List<Categories> cat =  userOps.GetCategories();

            vendorsOfCustomer=  userOps.GetVendorByCategory(22, 4);


            ////CHECK this method
            //List<VendorCategory> vc = userOps.GetVendorPlanAmount("3799177237");

           // userOps.AddCustomer(new Customers { CityID=5, CustomerID=31,CustomerName = "Katekar", ContactInfo = new ContactComplexType { ContactNo = "9832782387" } });

            Customers cus = userOps.GetUserByID(30);
            //
            userOps.AddBankDetails(new AccountDetails { BankAcNo = 5461245789, BankName = "Allahabad", IFSC = 12345678912, CustomerID = 22 },22);
            Console.ReadKey();
        }
    }
}
