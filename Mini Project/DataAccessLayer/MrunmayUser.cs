using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DataAccessLayer
{
    public class MrunmayUser
    {
        AppContext dataContext;
        public MrunmayUser()
        {
            dataContext = new AppContext();
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
        public bool AddTransaction(Transactions transact)
        {
            int count = dataContext.Transactions.Count();
            dataContext.Transactions.Add(transact);
            dataContext.SaveChanges();
            if (dataContext.Transactions.Count()>count)
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
        //global: even for admin
        public List<Categories> GetCategories()
        {
            return dataContext.Categories.ToList();
        }
        public List<Vendors> GetVendorByCategory(int customerID,int categoryID)
        {
            var vendorList = from ven in dataContext.Vendors
                             join cusven in dataContext.CustomerVendors
                             on ven.VendorID equals cusven.VendorID
                             where cusven.CustomerID == customerID && cusven.CategoryID == categoryID
                             select ven;
            return vendorList.Distinct().ToList();
        }


        public VendorCategory GetVendorPlanAmount(string uniqueID)
        {
            var planList = from vc in dataContext.VendorCategory
                           from cv in dataContext.CustomerVendors
                           where vc.VendorID == cv.VendorID && vc.CategoryID == cv.CategoryID && cv.UniqueID.Equals(uniqueID)
                           select vc;
            return planList.First();
        }

    }
}
